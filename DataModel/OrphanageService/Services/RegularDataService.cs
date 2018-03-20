using OrphanageDataModel.RegularData;
using OrphanageService.DataContext;
using OrphanageService.Services.Interfaces;
using OrphanageService.Utilities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace OrphanageService.Services
{
    public class RegularDataService : IRegularDataService
    {
        private readonly ICheckerService _checkerService;

        public RegularDataService(ICheckerService checkerService)
        {
            _checkerService = checkerService;
        }

        public async Task<int> AddAddress(Address address, OrphanageDbCNoBinary orphanageDBC)
        {
            orphanageDBC.Addresses.Add(address.Clean());
            await orphanageDBC.SaveChangesAsync();
            if (address.Id > 0)
                return address.Id;
            else
                return -1;
        }

        public async Task<int> AddHealth(Health health, OrphanageDbCNoBinary orphanageDBC)
        {
            orphanageDBC.Healthies.Add(health);
            await orphanageDBC.SaveChangesAsync();
            if (health.Id > 0)
                return health.Id;
            else
                return -1;
        }

        public async Task<int> AddName(Name name, OrphanageDbCNoBinary orphanageDBC)
        {
            orphanageDBC.Names.Add(name);
            await orphanageDBC.SaveChangesAsync();
            if (name.Id > 0)
                return name.Id;
            else
                return -1;
        }

        public async Task<int> AddStudy(Study study, OrphanageDbCNoBinary orphanageDBC)
        {
            orphanageDBC.Studies.Add(study);
            await orphanageDBC.SaveChangesAsync();
            if (study.Id > 0)
                return study.Id;
            else
                return -1;
        }

        public async Task<IEnumerable<Address>> CleanAddresses()
        {
            using (var orphanageDbc = new OrphanageDbCNoBinary())
            {
                using (var dbT = orphanageDbc.Database.BeginTransaction())
                {
                    orphanageDbc.Configuration.LazyLoadingEnabled = true;
                    orphanageDbc.Configuration.ProxyCreationEnabled = true;
                    var addresses = await orphanageDbc.Addresses.Where(add =>
                                          (add.Caregivers.Count() == 0) &&
                                          (add.Families.Count() == 0) &&
                                          (add.FamliesAlternativeAddresses.Count() == 0) &&
                                          (add.Guarantors.Count() == 0) &&
                                          (add.Mothers.Count() == 0) &&
                                          (add.Users.Count() == 0)).ToListAsync();

                    if (addresses == null || addresses.Count <= 0)
                        return null;

                    foreach (var add in addresses)
                    {
                        orphanageDbc.Addresses.Remove(add);
                    }
                    await orphanageDbc.SaveChangesAsync();
                    dbT.Commit();
                    return addresses;
                }
            }
        }

        public async Task<IEnumerable<Health>> CleanHealthies()
        {
            using (var orphanageDbc = new OrphanageDbCNoBinary())
            {
                using (var dbT = orphanageDbc.Database.BeginTransaction())
                {
                    orphanageDbc.Configuration.LazyLoadingEnabled = true;
                    orphanageDbc.Configuration.ProxyCreationEnabled = true;

                    var healths = await (from health in orphanageDbc.Healthies
                                         where (health.Orphans.Count() <= 0)
                                         select health).ToListAsync();

                    if (healths == null || healths.Count <= 0)
                        return null;

                    foreach (var health in healths)
                    {
                        orphanageDbc.Healthies.Remove(health);
                    }
                    await orphanageDbc.SaveChangesAsync();
                    dbT.Commit();
                    return healths;
                }
            }
        }

        public async Task<IEnumerable<Name>> CleanNames()
        {
            using (var orphanageDbc = new OrphanageDbCNoBinary())
            {
                orphanageDbc.Configuration.LazyLoadingEnabled = true;
                orphanageDbc.Configuration.ProxyCreationEnabled = true;
                using (var dbT = orphanageDbc.Database.BeginTransaction())
                {
                    var names = await (from name in orphanageDbc.Names
                                       where (name.Caregivers.Count() <= 0) &&
                                             (name.Fathers.Count() <= 0) &&
                                             (name.Orphans.Count() <= 0) &&
                                             (name.Guarantors.Count() <= 0) &&
                                             (name.Mothers.Count() <= 0) &&
                                             (name.Users.Count() <= 0)
                                       select name).ToListAsync();

                    if (names == null || names.Count <= 0)
                        return null;

                    foreach (var name in names)
                    {
                        orphanageDbc.Names.Remove(name);
                    }
                    await orphanageDbc.SaveChangesAsync();
                    dbT.Commit();
                    return names;
                }
            }
        }

        public async Task<IEnumerable<Study>> CleanStudies()
        {
            using (var orphanageDbc = new OrphanageDbCNoBinary())
            {
                orphanageDbc.Configuration.LazyLoadingEnabled = true;
                orphanageDbc.Configuration.ProxyCreationEnabled = true;
                using (var dbT = orphanageDbc.Database.BeginTransaction())
                {
                    var studies = await (from name in orphanageDbc.Studies
                                         where (name.Orphans.Count() <= 0)
                                         select name).ToListAsync();

                    if (studies == null || studies.Count <= 0)
                        return null;

                    foreach (var study in studies)
                    {
                        orphanageDbc.Studies.Remove(study);
                    }
                    await orphanageDbc.SaveChangesAsync();
                    dbT.Commit();
                    return studies;
                }
            }
        }

        public async Task<bool> DeleteAddress(int addressId, OrphanageDbCNoBinary orphanageDBC)
        {
            var addressTodelete = await orphanageDBC.Addresses.FirstOrDefaultAsync(a => a.Id == addressId);
            orphanageDBC.Addresses.Remove(addressTodelete);
            return await orphanageDBC.SaveChangesAsync() > 0 ? true : false;
        }

        public async Task<bool> DeleteHealth(int healthId, OrphanageDbCNoBinary orphanageDBC)
        {
            var healthTodelete = await orphanageDBC.Healthies.FirstOrDefaultAsync(a => a.Id == healthId);
            if (healthTodelete.Orphans == null || healthTodelete.Orphans.Count == 0)
            {
                orphanageDBC.Healthies.Remove(healthTodelete);
                return await orphanageDBC.SaveChangesAsync() > 0 ? true : false;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> DeleteName(int nameId, OrphanageDbCNoBinary orphanageDBC)
        {
            var nameTodelete = await orphanageDBC.Names.FirstOrDefaultAsync(a => a.Id == nameId);
            orphanageDBC.Names.Remove(nameTodelete);
            return await orphanageDBC.SaveChangesAsync() > 0 ? true : false;
        }

        public async Task<bool> DeleteStudy(int studyId, OrphanageDbCNoBinary orphanageDBC)
        {
            var studyTodelete = await orphanageDBC.Studies.Include(s => s.Orphans).FirstOrDefaultAsync(a => a.Id == studyId);
            if (studyTodelete.Orphans == null || studyTodelete.Orphans.Count == 0)
            {
                orphanageDBC.Studies.Remove(studyTodelete);
                return await orphanageDBC.SaveChangesAsync() > 0 ? true : false;
            }
            else
            {
                return false;
            }
        }

        public async Task<int> SaveAddress(Address address, OrphanageDbCNoBinary orphanageDBC)
        {
            var orginalAddress = await orphanageDBC.Addresses.Where(a => a.Id == address.Id).FirstOrDefaultAsync();
            var cleanAddress = address.Clean();
            orginalAddress.Note = cleanAddress.Note;
            orginalAddress.CellPhone = cleanAddress.CellPhone;
            orginalAddress.City = cleanAddress.City;
            orginalAddress.Country = cleanAddress.Country;
            orginalAddress.Facebook = cleanAddress.Facebook;
            orginalAddress.HomePhone = cleanAddress.HomePhone;
            orginalAddress.Street = cleanAddress.Street;
            orginalAddress.Town = cleanAddress.Town;
            orginalAddress.Twitter = cleanAddress.Twitter;
            orginalAddress.WorkPhone = cleanAddress.WorkPhone;
            orginalAddress.Fax = cleanAddress.Fax;
            orginalAddress.Email = cleanAddress.Email;
            var ret = await orphanageDBC.SaveChangesAsync();
            return ret;
        }

        public async Task<int> SaveHealth(Health health, OrphanageDbCNoBinary orphanageDBC)
        {
            var orginalHealth = await orphanageDBC.Healthies.FirstOrDefaultAsync(a => a.Id == health.Id);
            orginalHealth.Medicine = health.Medicine;
            orginalHealth.Note = health.Note;
            orginalHealth.SicknessName = health.SicknessName;
            orginalHealth.SupervisorDoctor = health.SupervisorDoctor;
            orginalHealth.Cost = health.Cost;
            return await orphanageDBC.SaveChangesAsync();
        }

        public async Task<int> SaveName(Name name, OrphanageDbCNoBinary orphanageDBC)
        {
            var orginalName = await orphanageDBC.Names.Where(a => a.Id == name.Id).FirstOrDefaultAsync();
            orginalName.First = name.First;
            orginalName.Father = name.Father;
            orginalName.Last = name.Last;
            orginalName.EnglishFirst = name.EnglishFirst;
            orginalName.EnglishFather = name.EnglishFather;
            orginalName.EnglishLast = name.EnglishLast;
            var ret = await orphanageDBC.SaveChangesAsync();
            return ret;
        }

        public async Task<int> SaveStudy(Study study, OrphanageDbCNoBinary orphanageDBC)
        {
            var orginalStudy = await orphanageDBC.Studies.FirstOrDefaultAsync(a => a.Id == study.Id);
            orginalStudy.Collage = study.Collage;
            orginalStudy.DegreesRate = study.DegreesRate;
            orginalStudy.MonthlyCost = study.MonthlyCost;
            orginalStudy.Note = study.Note;
            orginalStudy.Reasons = study.Reasons;
            orginalStudy.School = study.School;
            orginalStudy.Stage = study.Stage;
            orginalStudy.Univercity = study.Univercity;
            return await orphanageDBC.SaveChangesAsync();
        }
    }
}