using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security.OAuth;
using OrphanageService.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace OrphanageService.Services
{
    public class AuthorizationService : OAuthAuthorizationServerProvider
    {
        private IUserDbService _userDbService = null;

        public AuthorizationService()
        {
            _userDbService = UnityConfig.GetConfiguredContainer().Resolve<IUserDbService>();
        }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            await Task.FromResult(context.Validated());
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            var user = await _userDbService.AuthenticateUser(context.UserName, context.Password);
            if (user == null)
            {
                context.SetError("invalid_grant", Properties.Resources.Error_AccessDenied);
                return;
            }
            var identity = setClaimsIdentity(user, context.Options.AuthenticationType);
            context.Validated(identity);
        }

        private ClaimsIdentity setClaimsIdentity(OrphanageDataModel.Persons.User user, string AuthenticationType)
        {
            var identity = new ClaimsIdentity(AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
            if (user.IsAdmin)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, "Admin"));
                identity.AddClaim(new Claim(ClaimTypes.Role, "CanAdd"));
                identity.AddClaim(new Claim(ClaimTypes.Role, "CanDelete"));
                identity.AddClaim(new Claim(ClaimTypes.Role, "CanDeposit"));
                identity.AddClaim(new Claim(ClaimTypes.Role, "CanDraw"));
                identity.AddClaim(new Claim(ClaimTypes.Role, "CanRead"));
            }
            else
            {
                if (user.CanAdd)
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, "CanAdd"));
                }
                if (user.CanDelete)
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, "CanDelete"));
                }
                if (user.CanDeposit)
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, "CanDeposit"));
                }
                if (user.CanDraw)
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, "CanDraw"));
                }
                if (user.CanRead)
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, "CanRead"));
                }
            }

            return identity;
        }
    }
}