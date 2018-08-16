using System.Web.Http;

namespace WebApi_2
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "get-values",
                routeTemplate: "{id}/{*catchall}",
                defaults: new { controller = "Values", action = "Get", id = RouteParameter.Optional },
                constraints: new { id = @"(\d+)?" } );

            config.Routes.MapHttpRoute(
                name: "aou-values",
                routeTemplate: "actions/addorupdate/{type}",
                defaults: new { controller = "Values", action = "AddOrUpdate" },
                constraints: new { type = @"^(true|false)$" });

            config.Routes.MapHttpRoute(
                name: "delete-values",
                routeTemplate: "actions/delete",
                defaults: new { controller = "Values", action = "Delete" });
        }
    }
}
