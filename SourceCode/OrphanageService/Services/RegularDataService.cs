using OrphanageDataModel.RegularData;
using OrphanageService.DataContext;
using OrphanageService.Services.Interfaces;
using OrphanageService.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace OrphanageService.Services
{
    public class RegularDataService : IRegularDataService
    {
        private readonly ICheckerService _checkerService;
        private readonly ILogger _logger;

        public RegularDataService(ICheckerService checkerService, ILogger logger)
        {
            _checkerService = checkerService;
            this._logger = logger;
        }

        public async Task<int> AddAddress(Address address, OrphanageDbCNoBinary orphanageDBC)
        {
            _logger.Information($"trying to add new Address");
            orphanageDBC.Addresses.Add(address.Clean());
            await orphanageDBC.SaveChangesAsync();
            if (address.Id > 0)
            {
                _logger.Information($"address with id ({address.Id}) has been created");
                return address.Id;
            }
            else
            {
                _logger.Warning($"address cannot be added");
                return -1;
            }
        }

        public async Task<int> AddHealth(Health health, OrphanageDbCNoBinary orphanageDBC)
        {
            _logger.Information($"trying to add new health object");
            orphanageDBC.Healthies.Add(health);
            await orphanageDBC.SaveChangesAsync();
            if (health.Id > 0)
            {
                _logger.Information($"health with id ({health.Id}) has been created");
                return health.Id;
            }
            else
            {
                _logger.Warning($"health cannot be added");
                return -1;
            }
        }

        public async Task<int> AddName(Name name, OrphanageDbCNoBinary orphanageDBC)
        {
            _logger.Information($"trying to add new name object");
            orphanageDBC.Names.Add(name);
            await orphanageDBC.SaveChangesAsync();
            if (name.Id > 0)
            {
                _logger.Information($"name with id ({name.Id}) has been created");
                return name.Id;
            }
            else
            {
                _logger.Warning($"name cannot be added");
                return -1;
            }
        }

        public async Task<int> AddStudy(Study study, OrphanageDbCNoBinary orphanageDBC)
        {
            _logger.Information($"trying to add new study object");
            orphanageDBC.Studies.Add(study);
            await orphanageDBC.SaveChangesAsync();
            if (study.Id > 0)
            {
                _logger.Information($"study with id ({study.Id}) has been created");
                return study.Id;
            }
            else
            {
                _logger.Warning($"study cannot be added");
                return -1;
            }
        }

        public async Task<IEnumerable<Address>> CleanAddresses()
        {
            _logger.Information($"trying to clean all addresses");
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
                    {
                        _logger.Information($"there are no dead addresses, null will be returned");
                        return null;
                    }

                    foreach (var add in addresses)
                    {
                        orphanageDbc.Addresses.Remove(add);
                    }
                    var ret = await orphanageDbc.SaveChangesAsync();
                    if (ret > 0)
                    {
                        _logger.Information($"there are {ret} dead addresses, they were be deleted");
                        dbT.Commit();
                    }
                    else
                    {
                        _logger.Information($"nothing has changed, no dead addresses has been deleted");
                    }
                    return addresses;
                }
            }
        }

        public async Task<IEnumerable<Health>> CleanHealthies()
        {
            _logger.Information($"trying to clean all Healths");
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
                    {
                        _logger.Information($"there are no dead healths, null will be returned");
                        return null;
                    }

                    foreach (var health in healths)
                    {
                        orphanageDbc.Healthies.Remove(health);
                    }
                    var ret = await orphanageDbc.SaveChangesAsync();
                    if (ret > 0)
                    {
                        _logger.Information($"there are {ret} dead healths, they were be deleted");
                        dbT.Commit();
                    }
                    else
                    {
                        _logger.Information($"nothing has changed, no dead healths has been deleted");
                    }

                    return healths;
                }
            }
        }

        public async Task<IEnumerable<Name>> CleanNames()
        {
            _logger.Information($"trying to clean all Names");
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
                    {
                        _logger.Information($"there are no dead Names, null will be returned");
                        return null;
                    }

                    foreach (var name in names)
                    {
                        orphanageDbc.Names.Remove(name);
                    }
                    var ret = await orphanageDbc.SaveChangesAsync();
                    if (ret > 0)
                    {
                        _logger.Information($"there are {ret} dead Names, they were be deleted");
                        dbT.Commit();
                    }
                    else
                    {
                        _logger.Information($"nothing has changed, no dead Names has been deleted");
                    }
                    return names;
                }
            }
        }

        public async Task<IEnumerable<Study>> CleanStudies()
        {
            _logger.Information($"trying to clean all Studies");
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
                    {
                        _logger.Information($"there are no dead Studies, null will be returned");
                        return null;
                    }

                    foreach (var study in studies)
                    {
                        orphanageDbc.Studies.Remove(study);
                    }
                    var ret = await orphanageDbc.SaveChangesAsync();
                    if (ret > 0)
                    {
                        _logger.Information($"there are {ret} dead Studies, they were be deleted");
                        dbT.Commit();
                    }
                    else
                    {
                        _logger.Information($"nothing has changed, no dead Studies has been deleted");
                    }
                    return studies;
                }
            }
        }

        public async Task<bool> DeleteAddress(int addressId, OrphanageDbCNoBinary orphanageDBC)
        {
            _logger.Information($"trying to delete address with id({addressId})");
            var addressTodelete = await orphanageDBC.Addresses.FirstOrDefaultAsync(a => a.Id == addressId);
            if (addressTodelete == null)
            {
                _logger.Error($"address with id({addressId}) cannot be found, false will be returned");
                return false;
            }
            orphanageDBC.Addresses.Remove(addressTodelete);
            var ret = await orphanageDBC.SaveChangesAsync() > 0 ? true : false;
            if (ret)
            {
                _logger.Information($"address with id({addressId}) has been deleted, true will be returned");
            }
            else
            {
                _logger.Warning($"address with id({addressId}) has been not deleted, false will be returned");
            }
            return ret;
        }

        public async Task<bool> DeleteHealth(int healthId, OrphanageDbCNoBinary orphanageDBC)
        {
            _logger.Information($"trying to delete Health with id({healthId})");
            var healthTodelete = await orphanageDBC.Healthies.FirstOrDefaultAsync(a => a.Id == healthId);
            if (healthTodelete == null)
            {
                _logger.Error($"Health with id({healthId}) cannot be found, false will be returned");
                return false;
            }
            if (healthTodelete.Orphans == null || healthTodelete.Orphans.Count == 0)
            {
                orphanageDBC.Healthies.Remove(healthTodelete);
                var ret = await orphanageDBC.SaveChangesAsync() > 0 ? true : false;
                if (ret)
                {
                    _logger.Information($"Health with id({healthId}) has been deleted, true will be returned");
                }
                else
                {
                    _logger.Warning($"Health with id({healthId}) has been not deleted, false will be returned");
                }
                return ret;
            }
            else
            {
                _logger.Warning($"Health with id({healthId}) has been not deleted, it has foreign key on orphans, false will be returned");
                return false;
            }
        }

        public async Task<bool> DeleteName(int nameId, OrphanageDbCNoBinary orphanageDBC)
        {
            _logger.Information($"trying to delete name with id({nameId})");
            var nameTodelete = await orphanageDBC.Names.FirstOrDefaultAsync(a => a.Id == nameId);
            if (nameTodelete == null)
            {
                _logger.Error($"name with id({nameId}) cannot be found, false will be returned");
                return false;
            }
            orphanageDBC.Names.Remove(nameTodelete);
            var ret = await orphanageDBC.SaveChangesAsync() > 0 ? true : false;
            if (ret)
            {
                _logger.Information($"name with id({nameId}) has been deleted, true will be returned");
            }
            else
            {
                _logger.Warning($"name with id({nameId}) has been not deleted, false will be returned");
            }
            return ret;
        }

        public async Task<bool> DeleteStudy(int studyId, OrphanageDbCNoBinary orphanageDBC)
        {
            _logger.Information($"trying to delete study with id({studyId})");
            var studyTodelete = await orphanageDBC.Studies.Include(s => s.Orphans).FirstOrDefaultAsync(a => a.Id == studyId);
            if (studyTodelete == null)
            {
                _logger.Error($"study with id({studyId}) cannot be found, false will be returned");
                return false;
            }
            if (studyTodelete.Orphans == null || studyTodelete.Orphans.Count == 0)
            {
                orphanageDBC.Studies.Remove(studyTodelete);
                var ret = await orphanageDBC.SaveChangesAsync() > 0 ? true : false;
                if (ret)
                {
                    _logger.Information($"study with id({studyId}) has been deleted, true will be returned");
                }
                else
                {
                    _logger.Warning($"study with id({studyId}) has been not deleted, false will be returned");
                }
                return ret;
            }
            else
            {
                _logger.Warning($"study with id({studyId}) has been not deleted, it has foreign key on orphans, false will be returned");
                return false;
            }
        }

        public async Task<int> SaveAddress(Address address, OrphanageDbCNoBinary orphanageDBC)
        {
            _logger.Information($"trying to save address with id({address.Id})");
            var orginalAddress = await orphanageDBC.Addresses.Where(a => a.Id == address.Id).FirstOrDefaultAsync();
            if (orginalAddress == null)
            {
                _logger.Error($"address with id({address.Id}) cannot be found, NullReferenceException will be thrown");
                throw new NullReferenceException();
            }
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
            _logger.Information($"({ret}) changes has been made to address with id({address.Id})");
            return ret;
        }

        public async Task<int> SaveHealth(Health health, OrphanageDbCNoBinary orphanageDBC)
        {
            _logger.Information($"trying to save health with id({health.Id})");
            var orginalHealth = await orphanageDBC.Healthies.FirstOrDefaultAsync(a => a.Id == health.Id);
            if (orginalHealth == null)
            {
                _logger.Error($"health with id({health.Id}) cannot be found, NullReferenceException will be thrown");
                throw new NullReferenceException();
            }
            orginalHealth.Medicine = health.Medicine;
            orginalHealth.Note = health.Note;
            orginalHealth.SicknessName = health.SicknessName;
            orginalHealth.SupervisorDoctor = health.SupervisorDoctor;
            orginalHealth.Cost = health.Cost;
            var ret = await orphanageDBC.SaveChangesAsync();
            _logger.Information($"({ret}) changes has been made to health with id({health.Id})");
            return ret;
        }

        public async Task<int> SaveName(Name name, OrphanageDbCNoBinary orphanageDBC)
        {
            _logger.Information($"trying to save name with id({name.Id})");
            var orginalName = await orphanageDBC.Names.Where(a => a.Id == name.Id).FirstOrDefaultAsync();
            if (orginalName == null)
            {
                _logger.Error($"name with id({name.Id}) cannot be found, NullReferenceException will be thrown");
                throw new NullReferenceException();
            }
            orginalName.First = name.First;
            orginalName.Father = name.Father;
            orginalName.Last = name.Last;
            orginalName.EnglishFirst = name.EnglishFirst;
            orginalName.EnglishFather = name.EnglishFather;
            orginalName.EnglishLast = name.EnglishLast;
            var ret = await orphanageDBC.SaveChangesAsync();
            _logger.Information($"({ret}) changes has been made to name with id({name.Id})");
            return ret;
        }

        public async Task<int> SaveStudy(Study study, OrphanageDbCNoBinary orphanageDBC)
        {
            _logger.Information($"trying to save study with id({study.Id})");
            var orginalStudy = await orphanageDBC.Studies.FirstOrDefaultAsync(a => a.Id == study.Id);
            if (orginalStudy == null)
            {
                _logger.Error($"study with id({study.Id}) cannot be found, NullReferenceException will be thrown");
                throw new NullReferenceException();
            }
            orginalStudy.Collage = study.Collage;
            orginalStudy.DegreesRate = study.DegreesRate;
            orginalStudy.MonthlyCost = study.MonthlyCost;
            orginalStudy.Note = study.Note;
            orginalStudy.Reasons = study.Reasons;
            orginalStudy.School = study.School;
            orginalStudy.Stage = study.Stage;
            orginalStudy.Univercity = study.Univercity;
            var ret = await orphanageDBC.SaveChangesAsync();
            _logger.Information($"({ret}) changes has been made to study with id({study.Id})");
            return ret;
        }
    }
}