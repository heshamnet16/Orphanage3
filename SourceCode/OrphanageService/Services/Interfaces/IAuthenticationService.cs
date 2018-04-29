using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Threading.Tasks;

namespace OrphanageService.Services.Interfaces
{
    public interface IAuthenticationService : IDisposable
    {
        Task<IdentityResult> RigsterUser(OrphanageDataModel.Persons.User userModel);

        Task<IdentityUser> FindUser(string userName, string password);
    }
}