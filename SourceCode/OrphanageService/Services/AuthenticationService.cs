using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using OrphanageService.DataContext;
using OrphanageService.Services.Interfaces;
using System.Threading.Tasks;

namespace OrphanageService.Services
{
    //this class will be needed, when more security methods will be implemented
    public class AuthenticationService : IAuthenticationService
    {
        private OrphanageAuthContext _ctx;

        private UserManager<IdentityUser> _userManager;

        public async Task<IdentityUser> FindUser(string userName, string password)
        {
            IdentityUser user = await _userManager.FindAsync(userName, password);

            return user;
        }

        public async Task<IdentityResult> RigsterUser(OrphanageDataModel.Persons.User userModel)
        {
            IdentityUser user = new IdentityUser
            {
                UserName = userModel.UserName
            };

            var result = await _userManager.CreateAsync(user, userModel.Password);

            return result;
        }

        #region IDisposable Support

        public void Dispose()
        {
            _ctx?.Dispose();
            _userManager?.Dispose();
        }

        #endregion IDisposable Support
    }
}