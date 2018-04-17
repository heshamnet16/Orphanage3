using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrphanageService.Services.Interfaces
{
    public interface IAuthenticationService : IDisposable
    {
        Task<IdentityResult> RigsterUser(OrphanageDataModel.Persons.User userModel);

        Task<IdentityUser> FindUser(string userName, string password);
    }
}
