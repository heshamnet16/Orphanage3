using Microsoft.AspNet.Identity.EntityFramework;
using OrphanageService.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrphanageService.DataContext
{
    public class OrphanageAuthContext : IdentityDbContext<IdentityUser> 
    {
        public OrphanageAuthContext() : base(Settings.Default.ConnectionString + "; Password=OrphansApp3")
        {

        }
    }
}
