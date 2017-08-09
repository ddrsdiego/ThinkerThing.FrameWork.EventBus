using EventBus;
using EventBus.Abstractions;
using EventBus.RabbitMQ;
using MediatR;
using Newtonsoft.Json;
using Polly;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using Rydo.Framework.MediatR.Eventos;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EventBusRabbitMQ
{
    public class EventBusRabbitMQ : IEventBus, IDisposable
    {
        private const string CONTENT_TYPE = "application/json";

        private IModel _consumerChannel;
        private Dictionary<string, Type> _subsManager;
        private readonly IMediator _mediator;
        private readonly IRabbitMQPersistentConnection _persistentConnection;

        public EventBusRabbitMQ(IRabbitMQPersistentConnection persistentConnection, IMediator mediator)
        {
            _mediator = mediator;
            _persistentConnection = persistentConnection;
            _subsManager = new Dictionary<string, Type>();
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
                basicProperties.Type = @event.GetType().AssemblyQualifiedName;

                policy.Execute(() =>
                {
                    channel.BasicPublish(exchange: exchangeName,
                                         routingKey: string.Empty,
                                         basicProperties: basicProperties,
                                         body: body);
                });
            }
        }

        public void Subscribe<T>()
            where T : IntegrationEvent
        {
            var eventName = typeof(T).FullName;

            DoInternalSubscription(eventName);

            _subsManager.Add(eventName, typeof(T));
        }

        public void Subscribe<T>(Action<IEndpointSpecificationConfigurator> configuration) where T : IntegrationEvent
        {
            VerifyConnection();

            using (var channel = _persistentConnection.CreateModel())
            {
                var messageConfiguration = new MessageConfiguration();
                configuration(messageConfiguration);

                DoInternalSubscription(messageConfiguration);
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
                await ProcessEvent(ea);

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

        private async Task ProcessEvent(BasicDeliverEventArgs ea)
        {
            try
            {
                var message = Encoding.UTF8.GetString(ea.Body);
                var eventType = Type.GetType(ea.BasicProperties.Type);
                var integrationEvent = (IntegrationEvent)JsonConvert.DeserializeObject(message, eventType);

                await _mediator.Send(integrationEvent);
            }
            catch (Exception ex)
            {

            }
        }

        private void DoInternalSubscription(MessageConfiguration messageConfiguration)
        {
            var containsKey = _subsManager.ContainsKey("");

            if (!containsKey)
            {
                var queueName = messageConfiguration.QueueName;
                //var queueName = $"QL.{eventName}";
                //var exchangeName = $"EX.{eventName}";

                VerifyConnection();

                using (var channel = _persistentConnection.CreateModel())
                {

                    //channel.ExchangeDeclare(exchange: exchangeName,
                    //                        type: ExchangeType.Direct,
                    //                        durable: false,
                    //                        autoDelete: false,
                    //                        arguments: null);

                    //channel.QueueDeclare(queue: queueName,
                    //                     durable: false,
                    //                     exclusive: false,
                    //                     autoDelete: false,
                    //                     arguments: null);

                    //channel.QueueBind(queue: queueName,
                    //                  exchange: exchangeName,
                    //                  routingKey: string.Empty,
                    //                  arguments: null);

                    //_consumerChannel = CreateConsumerChannel(queueName);
                }
            }
        }

        private void DoInternalSubscription(string eventName)
        {
            var containsKey = _subsManager.ContainsKey(eventName);

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