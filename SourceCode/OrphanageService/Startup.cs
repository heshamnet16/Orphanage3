using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using OrphanageService.Services;
using Owin;
using System;
using System.Web.Http.Dispatcher;
using System.Web.Http.ExceptionHandling;

namespace OrphanageService
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var httpConfiguration = WepApiConfig.Register();
            httpConfiguration.Services.Replace(typeof(IHttpControllerSelector), new HttpAreaSelector(httpConfiguration));
            httpConfiguration.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionsHandlers.GlobalExceptionsHandler());
            httpConfiguration.EnsureInitialized();
            appBuilder.Use<GlobalExceptionsHandlers.GlobalExceptionMiddleware>();
            ConfigureOAuth(appBuilder);
            appBuilder.UseWebApi(httpConfiguration);
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new AuthorizationService()
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}