using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using System.Web.Http.Dispatcher;

namespace SalesAnalysis
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes(new CustomDirectRouteProvider());

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Services.Replace(typeof(IHttpControllerTypeResolver),
                new IgnoreControllerTypeResolver(new[] { typeof(C1.Web.Api.DataEngine.DataEngineController) }));
        }
    }
}
