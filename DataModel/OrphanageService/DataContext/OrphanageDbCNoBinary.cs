using OrphanageService.Properties;
using System.Data.Entity;

namespace OrphanageService.DataContext
{
    public partial class OrphanageDbCNoBinary : DbContext
    {
        public OrphanageDbCNoBinary()
            : base(Settings.Default.ConnectionString + ";Password=OrphansApp3")
        {
            System.Data.Entity.Database.SetInitializer<OrphanageDBC>(new CreateDatabaseIfNotExists<OrphanageDBC>());
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }

        public virtual DbSet<OrphanageDataModel.RegularData.Address> Addresses { get; set; }
        public virtual DbSet<OrphanageDataModel.FinancialData.Bail> Bails { get; set; }
        public virtual DbSet<OrphanageDataModel.Persons.Caregiver> Caregivers { get; set; }
        public virtual DbSet<OrphanageDataModel.FinancialData.Account> Accounts { get; set; }
        public virtual DbSet<OrphanageDataModel.RegularData.Family> Families { get; set; }
        public virtual DbSet<OrphanageDataModel.Persons.Father> Fathers { get; set; }
        public virtual DbSet<OrphanageDataModel.RegularData.Health> Healthies { get; set; }
        public virtual DbSet<OrphanageDataModel.Persons.Mother> Mothers { get; set; }
        public virtual DbSet<OrphanageDataModel.RegularData.Name> Names { get; set; }
        public virtual DbSet<OrphanageDataModel.Persons.Orphan> Orphans { get; set; }
        public virtual DbSet<OrphanageDataModel.RegularData.Study> Studies { get; set; }
        public virtual DbSet<OrphanageDataModel.Persons.Guarantor> Guarantors { get; set; }
        public virtual DbSet<OrphanageDataModel.Persons.User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrphanageDataModel.Persons.Orphan>()
                .Ignore(o => o.BirthCertificatePhotoData)
                .Ignore(o => o.FacePhotoData)
                .Ignore(o => o.FamilyCardPagePhotoData)
                .Ignore(o => o.FullPhotoData);
            modelBuilder.Entity<OrphanageDataModel.RegularData.Address>()
                .HasMany(e => e.Caregivers)
                .WithOptional(e => e.Address)
                .HasForeignKey(e => e.AddressId);

            modelBuilder.Entity<OrphanageDataModel.RegularData.Address>()
                .HasMany(e => e.Families)
                .WithOptional(e => e.PrimaryAddress)
                .HasForeignKey(e => e.AddressId);

            modelBuilder.Entity<OrphanageDataModel.RegularData.Address>()
                .HasMany(e => e.FamliesAlternativeAddresses)
                .WithOptional(e => e.AlternativeAddress)
                .HasForeignKey(e => e.AlternativeAddressId);

            modelBuilder.Entity<OrphanageDataModel.RegularData.Address>()
                .HasMany(e => e.Mothers)
                .WithOptional(e => e.Address)
                .HasForeignKey(e => e.AddressId);

            modelBuilder.Entity<OrphanageDataModel.RegularData.Address>()
                .HasMany(e => e.Guarantors)
                .WithOptional(e => e.Address)
                .HasForeignKey(e => e.AddressId);

            modelBuilder.Entity<OrphanageDataModel.RegularData.Address>()
                .HasMany(e => e.Users)
                .WithOptional(e => e.Address)
                .HasForeignKey(e => e.AddressId);

            modelBuilder.Entity<OrphanageDataModel.FinancialData.Bail>()
                .Property(e => e.Amount)
                .HasPrecision(29, 4);

            modelBuilder.Entity<OrphanageDataModel.FinancialData.Bail>()
                .HasMany(e => e.Families)
                .WithOptional(e => e.Bail)
                .HasForeignKey(e => e.BailId);

            modelBuilder.Entity<OrphanageDataModel.FinancialData.Bail>()
                .HasMany(e => e.Orphans)
                .WithOptional(e => e.Bail)
                .HasForeignKey(e => e.BailId);

            modelBuilder.Entity<OrphanageDataModel.Persons.Caregiver>()
                .Ignore(c => c.IdentityCardPhotoBackData)
                .Ignore(c => c.IdentityCardPhotoFaceData);

            modelBuilder.Entity<OrphanageDataModel.Persons.Caregiver>()
                .Property(e => e.Income)
                .HasPrecision(29, 4);

            modelBuilder.Entity<OrphanageDataModel.Persons.Caregiver>()
                .HasMany(e => e.Orphans)
                .WithRequired(e => e.Caregiver)
                .HasForeignKey(e => e.CaregiverId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OrphanageDataModel.FinancialData.Account>()
                .Property(e => e.Amount)
                .HasPrecision(29, 4);

            modelBuilder.Entity<OrphanageDataModel.FinancialData.Account>()
                .HasMany(e => e.Bails)
                .WithRequired(e => e.Account)
                .HasForeignKey(e => e.AccountID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OrphanageDataModel.FinancialData.Account>()
                .HasMany(e => e.Guarantors)
                .WithRequired(e => e.Account)
                .HasForeignKey(e => e.AccountId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OrphanageDataModel.RegularData.Family>()
                    .Ignore(f => f.FamilyCardImagePage1Data)
                    .Ignore(f => f.FamilyCardImagePage2Data);

            modelBuilder.Entity<OrphanageDataModel.RegularData.Family>()
                .HasMany(e => e.Orphans)
                .WithRequired(e => e.Family)
                .HasForeignKey(e => e.FamilyId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OrphanageDataModel.Persons.Father>()
                .Ignore(f => f.DeathCertificatePhotoData)
                .Ignore(f => f.PhotoData);

            modelBuilder.Entity<OrphanageDataModel.Persons.Father>()
                .HasMany(e => e.Families)
                .WithRequired(e => e.Father)
                .HasForeignKey(e => e.FatherId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OrphanageDataModel.RegularData.Health>()
                .Ignore(h => h.ReporteFileData);

            modelBuilder.Entity<OrphanageDataModel.RegularData.Health>()
                .Property(e => e.Cost)
                .HasPrecision(29, 4);

            modelBuilder.Entity<OrphanageDataModel.RegularData.Health>()
                .HasMany(e => e.Orphans)
                .WithOptional(e => e.HealthStatus)
                .HasForeignKey(e => e.HealthId);

            modelBuilder.Entity<OrphanageDataModel.Persons.Mother>()
                .Ignore(m => m.IdentityCardPhotoBackData)
                .Ignore(m => m.IdentityCardPhotoFaceData);

            modelBuilder.Entity<OrphanageDataModel.Persons.Mother>()
                .Property(e => e.Salary)
                .HasPrecision(29, 4);

            modelBuilder.Entity<OrphanageDataModel.Persons.Mother>()
                .HasMany(e => e.Families)
                .WithRequired(e => e.Mother)
                .HasForeignKey(e => e.MotherId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OrphanageDataModel.RegularData.Name>()
                .HasMany(e => e.Caregivers)
                .WithRequired(e => e.Name)
                .HasForeignKey(e => e.NameId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OrphanageDataModel.RegularData.Name>()
                .HasMany(e => e.Fathers)
                .WithRequired(e => e.Name)
                .HasForeignKey(e => e.NameId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OrphanageDataModel.RegularData.Name>()
                .HasMany(e => e.Mothers)
                .WithRequired(e => e.Name)
                .HasForeignKey(e => e.NameId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OrphanageDataModel.RegularData.Name>()
                .HasMany(e => e.Orphans)
                .WithRequired(e => e.Name)
                .HasForeignKey(e => e.NameId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OrphanageDataModel.RegularData.Name>()
                .HasMany(e => e.Guarantors)
                .WithRequired(e => e.Name)
                .HasForeignKey(e => e.NameId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OrphanageDataModel.RegularData.Name>()
                .HasMany(e => e.Users)
                .WithOptional(e => e.Name)
                .HasForeignKey(e => e.NameId);

            modelBuilder.Entity<OrphanageDataModel.RegularData.Study>()
                .Ignore(s => s.CertificatePhotoBack)
                .Ignore(s => s.CertificatePhotoFront);

            modelBuilder.Entity<OrphanageDataModel.RegularData.Study>()
                .Property(e => e.MonthlyCost)
                .HasPrecision(29, 4);

            modelBuilder.Entity<OrphanageDataModel.RegularData.Study>()
                .Property(e => e.DegreesRate)
                .HasPrecision(29, 4);

            modelBuilder.Entity<OrphanageDataModel.RegularData.Study>()
                .HasMany(e => e.Orphans)
                .WithOptional(e => e.Education)
                .HasForeignKey(e => e.EducationId);

            modelBuilder.Entity<OrphanageDataModel.Persons.Guarantor>()
                .HasMany(e => e.Bails)
                .WithRequired(e => e.Guarantor)
                .HasForeignKey(e => e.GuarantorID);

            modelBuilder.Entity<OrphanageDataModel.Persons.Guarantor>()
                .HasMany(e => e.Orphans)
                .WithOptional(e => e.Guarantor)
                .HasForeignKey(e => e.GuarantorId);

            modelBuilder.Entity<OrphanageDataModel.Persons.User>()
                .HasMany(e => e.Bails)
                .WithRequired(e => e.ActingUser)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OrphanageDataModel.Persons.User>()
                .HasMany(e => e.Caregivers)
                .WithRequired(e => e.ActingUser)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OrphanageDataModel.Persons.User>()
                .HasMany(e => e.Accounts)
                .WithRequired(e => e.ActingUser)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OrphanageDataModel.Persons.User>()
                .HasMany(e => e.Famlies)
                .WithRequired(e => e.ActingUser)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OrphanageDataModel.Persons.User>()
                .HasMany(e => e.Fathers)
                .WithRequired(e => e.ActingUser)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OrphanageDataModel.Persons.User>()
                .HasMany(e => e.Mothers)
                .WithRequired(e => e.ActingUser)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OrphanageDataModel.Persons.User>()
                .HasMany(e => e.Orphans)
                .WithRequired(e => e.ActingUser)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OrphanageDataModel.Persons.User>()
                .HasMany(e => e.Guarantors)
                .WithRequired(e => e.ActingUser)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);
        }
    }
}