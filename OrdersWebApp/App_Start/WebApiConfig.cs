using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

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
                defaults: new { id = RouteParameter.Optional }
            );

            // Формат вывода
            var json = config.Formatters.JsonFormatter;
            json.SerializerSettings.PreserveReferencesHandling =
                Newtonsoft.Json.PreserveReferencesHandling.None;
#if DEBUG
            json.Indent = true;
#endif

            config.Formatters.Remove(config.Formatters.XmlFormatter);
        }
    }
}
