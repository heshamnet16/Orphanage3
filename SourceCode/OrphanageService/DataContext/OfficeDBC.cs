using OrphanageDataModel.RegularData.DTOs;
using OrphanageService.Properties;
using System.Data.Entity;

namespace OrphanageService.DataContext
{
    public class OfficeDBC : DbContext
    {
        public OfficeDBC() : base(Settings.Default.ConnectionString + ";Password=OrphansApp3")
        {
            System.Data.Entity.Database.SetInitializer<OfficeDBC>(new CreateDatabaseIfNotExists<OfficeDBC>());
        }

        public virtual DbSet<OrphanageDataModel.RegularData.DTOs.WordTemplete> WordTemplates { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WordTemplete>()
                .HasRequired(e => e.WordPageData)
                .WithRequiredPrincipal(e => e.Template);
        }
    }
}