using System.Web.Http;
using Newtonsoft.Json;

namespace OrdersWebApp
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Конфигурация и службы веб-API

            // Маршруты веб-API
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new {id = RouteParameter.Optional}
            );

            // Формат вывода
            var json = config.Formatters.JsonFormatter;
            json.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            json.SerializerSettings.PreserveReferencesHandling =
                PreserveReferencesHandling.None;
#if DEBUG
            json.Indent = true;
#endif

            config.Formatters.Remove(config.Formatters.XmlFormatter);
        }
    }
}