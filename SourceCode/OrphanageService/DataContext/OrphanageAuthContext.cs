using Microsoft.AspNet.Identity.EntityFramework;
using OrphanageService.Properties;

namespace OrphanageService.DataContext
{
    public class OrphanageAuthContext : IdentityDbContext<IdentityUser>
    {
        public OrphanageAuthContext() : base(Settings.Default.ConnectionString + "; Password=OrphansApp3")
        {
        }
    }
}