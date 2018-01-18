using OrphanageDataModel.FinancialData;
using OrphanageDataModel.Persons;
using OrphanageDataModel.RegularData;
using OrphanageService.Properties;
using System.Data.Entity;

namespace OrphanageService.DataContext
{
    public partial class OrphanageDBC : DbContext
    {
        public OrphanageDBC()
            : base(Settings.Default.ConnectionString + ";Password=OrphansApp3")
        {
            Database.SetInitializer<OrphanageDBC>(new CreateDatabaseIfNotExists<OrphanageDBC>());
            //this.Configuration.ProxyCreationEnabled = false;
            this.Configuration.LazyLoadingEnabled = false;

        }

        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Bail> Bails { get; set; }
        public virtual DbSet<Caregiver> Caregivers { get; set; }
        public virtual DbSet<Account> Accounts{ get; set; }
        public virtual DbSet<Family> Families { get; set; }
        public virtual DbSet<Father> Fathers { get; set; }
        public virtual DbSet<Health> Healthies { get; set; }
        public virtual DbSet<Mother> Mothers { get; set; }
        public virtual DbSet<Name> Names { get; set; }
        public virtual DbSet<OrphanageDataModel.Persons.Orphan> Orphans { get; set; }
        public virtual DbSet<Study> Studies { get; set; }
        public virtual DbSet<Guarantor> Guarantors { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>()
                .HasMany(e => e.Caregivers)
                .WithOptional(e => e.Address)
                .HasForeignKey(e => e.AddressId);

            modelBuilder.Entity<Address>()
                .HasMany(e => e.Families)
                .WithOptional(e => e.PrimaryAddress)
                .HasForeignKey(e => e.AddressId);

            modelBuilder.Entity<Address>()
                .HasMany(e => e.FamliesAlternativeAddresses)
                .WithOptional(e => e.AlternativeAddress)
                .HasForeignKey(e => e.AlternativeAddressId);

            modelBuilder.Entity<Address>()
                .HasMany(e => e.Mothers)
                .WithOptional(e => e.Address)
                .HasForeignKey(e => e.AddressId);

            modelBuilder.Entity<Address>()
                .HasMany(e => e.Guarantors)
                .WithOptional(e => e.Address)
                .HasForeignKey(e => e.AddressId);

            modelBuilder.Entity<Address>()
                .HasMany(e => e.Users)
                .WithOptional(e => e.Address)
                .HasForeignKey(e => e.AddressId);

            modelBuilder.Entity<Bail>()
                .Property(e => e.Amount)
                .HasPrecision(29, 4);

            modelBuilder.Entity<Bail>()
                .HasMany(e => e.Families)
                .WithOptional(e => e.Bail)
                .HasForeignKey(e => e.BailId);

            modelBuilder.Entity<Bail>()
                .HasMany(e => e.Orphans)
                .WithOptional(e => e.Bail)
                .HasForeignKey(e => e.BailId);

            modelBuilder.Entity<Caregiver>()
                .Property(e => e.Income)
                .HasPrecision(29, 4);

            modelBuilder.Entity<Caregiver>()
                .HasMany(e => e.Orphans)
                .WithRequired(e => e.Caregiver)
                .HasForeignKey(e => e.CaregiverId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.Amount)
                .HasPrecision(29, 4);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.Bails)
                .WithRequired(e => e.Account)
                .HasForeignKey(e => e.AccountID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.Guarantors)
                .WithRequired(e => e.Account)
                .HasForeignKey(e => e.AccountId)
                .WillCascadeOnDelete(false);


            modelBuilder.Entity<Family>()
                .HasMany(e => e.Orphans)
                .WithRequired(e => e.Family)
                .HasForeignKey(e => e.FamilyId)
                .WillCascadeOnDelete(false);


            modelBuilder.Entity<Father>()
                .HasMany(e => e.Families)
                .WithRequired(e => e.Father)
                .HasForeignKey(e => e.FatherId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Health>()
                .Property(e => e.Cost)
                .HasPrecision(29, 4);

            modelBuilder.Entity<Health>()
                .HasMany(e => e.Orphans)
                .WithOptional(e => e.HealthStatus)
                .HasForeignKey(e => e.HealthId);

            modelBuilder.Entity<Mother>()
                .Property(e => e.Salary)
                .HasPrecision(29, 4);

            modelBuilder.Entity<Mother>()
                .HasMany(e => e.Families)
                .WithRequired(e => e.Mother)
                .HasForeignKey(e => e.MotherId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Name>()
                .HasMany(e => e.Caregivers)
                .WithRequired(e => e.Name)
                .HasForeignKey(e => e.NameId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Name>()
                .HasMany(e => e.Fathers)
                .WithRequired(e => e.Name)
                .HasForeignKey(e => e.NameId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Name>()
                .HasMany(e => e.Mothers)
                .WithRequired(e => e.Name)
                .HasForeignKey(e => e.NameId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Name>()
                .HasMany(e => e.Orphans)
                .WithRequired(e => e.Name )
                .HasForeignKey(e => e.NameId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Name>()
                .HasMany(e => e.Guarantors)
                .WithRequired(e => e.Name)
                .HasForeignKey(e => e.NameId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Name>()
                .HasMany(e => e.Users)
                .WithOptional(e => e.Name)
                .HasForeignKey(e => e.NameId);


            modelBuilder.Entity<Study>()
                .Property(e => e.MonthlyCost)
                .HasPrecision(29, 4);

            modelBuilder.Entity<Study>()
                .Property(e => e.DegreesRate)
                .HasPrecision(29, 4);

            modelBuilder.Entity<Study>()
                .HasMany(e => e.Orphans)
                .WithOptional(e => e.Education)
                .HasForeignKey(e => e.EducationId);

            modelBuilder.Entity<Guarantor>()
                .HasMany(e => e.Bails)
                .WithOptional(e => e.Guarantor)
                .HasForeignKey(e => e.GuarantorID);

            modelBuilder.Entity<Guarantor>()
                .HasMany(e => e.Orphans)
                .WithOptional(e => e.Guarantor)
                .HasForeignKey(e => e.GuarantorId);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Bails)
                .WithRequired(e => e.ActingUser)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);


            modelBuilder.Entity<User>()
                .HasMany(e => e.Caregivers)
                .WithRequired(e => e.ActingUser)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Accounts)
                .WithRequired(e => e.ActingUser)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Famlies)
                .WithRequired(e => e.ActingUser)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Fathers)
                .WithRequired(e => e.ActingUser)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Mothers)
                .WithRequired(e => e.ActingUser)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Orphans)
                .WithRequired(e => e.ActingUser)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Guarantors)
                .WithRequired(e => e.ActingUser)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);

        }
    }
}
