using Newtonsoft.Json.Serialization;
using System.Web.Http;

namespace ThinkerThings.Usuarios.Command.Api.Helpers
{
    public static class WebApiFormattersConfigurationEx
    {
        public static void ConfigureWebApi(this HttpConfiguration config)
        {
            var formatters = config.Formatters;
            formatters.Remove(formatters.XmlFormatter);

            var jsonSettings = formatters.JsonFormatter.SerializerSettings;
            jsonSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            jsonSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}