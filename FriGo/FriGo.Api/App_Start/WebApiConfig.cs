using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;
using FluentValidation.WebApi;
using FriGo.Api.Filters;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;

namespace FriGo.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration configuration)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            configuration.SuppressDefaultHostAuthentication();
            configuration.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            configuration.Filters.Add(new ValidateModelStateFilter());

            configuration.Formatters.JsonFormatter.SupportedMediaTypes
                .Add(new MediaTypeHeaderValue("text/html"));

            // Web API routes
            configuration.MapHttpAttributeRoutes();

            configuration.Routes.MapHttpRoute(
                "DefaultApi",
                "api/{controller}/{id}",
                new { id = RouteParameter.Optional }
            );

            ConfigureCors(configuration);

            FluentValidationModelValidatorProvider.Configure(configuration);
        }

        private static void ConfigureCors(HttpConfiguration configuration)
        {
            string corsWildcard = Properties.Resources.CorsAllowAllWildcard;
            var cors = new EnableCorsAttribute(corsWildcard, corsWildcard, corsWildcard);
            configuration.EnableCors(cors);
        }
    }
}
