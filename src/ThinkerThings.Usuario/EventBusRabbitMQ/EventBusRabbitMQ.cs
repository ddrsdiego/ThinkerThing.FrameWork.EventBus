using EventBus.Abstractions;
using MediatR;
using Newtonsoft.Json;
using Polly;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using Rydo.Framework.MediatR.Eventos;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EventBusRabbitMQ
{
    public class EventBusRabbitMQ : IEventBus, IDisposable
    {
        private const string CONTENT_TYPE = "application/json";

        private IModel _consumerChannel;
        private readonly IMediator _mediator;
        private readonly IEventBusSubscriptionsManager _subsManager;
        private readonly IRabbitMQPersistentConnection _persistentConnection;

        public EventBusRabbitMQ(IRabbitMQPersistentConnection persistentConnection, IEventBusSubscriptionsManager subsManager, IMediator mediator)
        {
            _mediator = mediator;
            _subsManager = subsManager;
            _persistentConnection = persistentConnection;
        }

        public void Publish(IntegrationEvent @event)
        {
            VerifyConnection();

            var policy = Policy.Handle<BrokerUnreachableException>()
                                    .Or<SocketException>()
                                    .WaitAndRetry(5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) => { });

            using (var channel = _persistentConnection.CreateModel())
            {
                var eventName = @event.GetType().FullName;

                var queueName = $"QL.{eventName}";
                var exchangeName = $"EX.{eventName}";

                var message = JsonConvert.SerializeObject(@event);
                var body = Encoding.UTF8.GetBytes(message);

                var basicProperties = channel.CreateBasicProperties();
                basicProperties.ContentType = CONTENT_TYPE;

                policy.Execute(() =>
                {
                    channel.BasicPublish(exchange: exchangeName,
                                         routingKey: string.Empty,
                                         basicProperties: basicProperties,
                                         body: body);
                });
            }
        }

        public void Dispose()
        {
            if (_consumerChannel != null)
            {
                _consumerChannel.Dispose();
            }

            _subsManager.Clear();
        }

        private void VerifyConnection()
        {
            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }
        }

        private IModel CreateConsumerChannel(string queueName)
        {
            VerifyConnection();

            var channel = _persistentConnection.CreateModel();

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (model, ea) =>
            {
                var eventName = GetEventName(ea);
                var message = Encoding.UTF8.GetString(ea.Body);

                await ProcessEvent(eventName, message);

                channel.BasicQos(0, 5, false);
            };

            channel.BasicConsume(queue: queueName,
                                 autoAck: true,
                                 consumer: consumer);

            channel.CallbackException += (sender, ea) =>
            {
                _consumerChannel.Dispose();
                _consumerChannel = CreateConsumerChannel(queueName);
            };

            return channel;
        }

        private string GetEventName(BasicDeliverEventArgs ea)
        {
            var eventName = string.IsNullOrEmpty(ea.RoutingKey) ? ea.Exchange : ea.RoutingKey;

            eventName = eventName.Substring(3, eventName.Length - 3);

            return eventName;
        }

        private async Task ProcessEvent(string eventName, string message)
        {
            try
            {
                var eventType = _subsManager.GetEventTypeByName(eventName);
                var integrationEvent = (IntegrationEvent)JsonConvert.DeserializeObject(message, eventType);
                await _mediator.Send(integrationEvent);
            }
            catch (Exception ex)
            {

            }
        }

        public void Subscribe<T>()
            where T : IntegrationEvent
        {
            var eventName = _subsManager.GetEventKey<T>();

            DoInternalSubscription(eventName);

            _subsManager.AddSubscription<T>(eventName);
        }

        private void DoInternalSubscription(string eventName)
        {
            var containsKey = _subsManager.HasSubscriptionsForEvent(eventName);

            if (!containsKey)
            {
                var queueName = $"QL.{eventName}";
                var exchangeName = $"EX.{eventName}";

                VerifyConnection();

                using (var channel = _persistentConnection.CreateModel())
                {

                    channel.ExchangeDeclare(exchange: exchangeName,
                                            type: ExchangeType.Direct,
                                            durable: false,
                                            autoDelete: false,
                                            arguments: null);

                    channel.QueueDeclare(queue: queueName,
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    channel.QueueBind(queue: queueName,
                                      exchange: exchangeName,
                                      routingKey: string.Empty,
                                      arguments: null);

                    _consumerChannel = CreateConsumerChannel(queueName);
                }
            }
        }
    }
}