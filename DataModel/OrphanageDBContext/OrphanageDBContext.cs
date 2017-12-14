using OrphanageDataModel.FinancialData;
using OrphanageDataModel.Persons;
using OrphanageDataModel.RegularData;
using OrphanageDBContext.Properties;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrphanageDBContext
{
    public class OrphanageDBContext : DbContext
    {

        public OrphanageDBContext() :base(Settings.Default.ConnectionString)
        {
            Database.SetInitializer<OrphanageDBContext>(new CreateDatabaseIfNotExists<OrphanageDBContext>());
        }

        public virtual DbSet<Caregiver> Caregivers { get; set; }
        public virtual DbSet<Father> Fathers { get; set; }
        public virtual DbSet<Guarantor> Guarantors { get; set; }
        public virtual DbSet<Mother> Mothers { get; set; }
        public virtual DbSet<Orphan> Orphans { get; set; }
        public virtual DbSet<User> Users { get; set; }


        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Family> Families { get; set; }
        public virtual DbSet<Health> Healthes{ get; set; }
        public virtual DbSet<Name> Names { get; set; }
        public virtual DbSet<Study> Studies { get; set; }

        public virtual DbSet<Bail> Bails { get; set; }
        public virtual DbSet<Credit> Credits { get; set; }


    }
}
