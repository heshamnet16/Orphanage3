using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using OrphanageService.Services;
using OrphanageService.Services.Interfaces;
using Owin;
using System;
using System.Web.Http.ExceptionHandling;
using Unity;

namespace OrphanageService
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var httpConfiguration = WepApiConfig.Register();
            httpConfiguration.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionsHandlers.GlobalExceptionsHandler());
            httpConfiguration.EnsureInitialized();
            appBuilder.Use<GlobalExceptionsHandlers.GlobalExceptionMiddleware>();
            ConfigureOAuth(appBuilder);
            appBuilder.UseWebApi(httpConfiguration);
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            var logger = UnityConfig.GetConfiguredContainer().Resolve<ILogger>();
            logger.Information("trying to configure Authorization server");
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                ApplicationCanDisplayErrors = true,
                //AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/Token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(1),
                Provider = new AuthorizationService()
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
            logger.Information("Authorization server is up and running");
        }
    }
}