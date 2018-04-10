using OrphanageDataModel.RegularData;
using OrphanageService.DataContext;
using OrphanageService.Services.Exceptions;
using OrphanageService.Services.Interfaces;
using OrphanageService.Utilities;
using OrphanageService.Utilities.Interfaces;
using OrphanageV3.Extensions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace OrphanageService.Services
{
    public class GuarantorDbService : IGuarantorDbService
    {
        private readonly ISelfLoopBlocking _selfLoopBlocking;
        private readonly IUriGenerator _uriGenerator;
        private readonly IRegularDataService _regularDataService;
        private readonly ILogger _logger;
        private readonly IOrphanDbService _orphanDbService;
        private readonly IBailDbService _bailDbService;

        public GuarantorDbService(ISelfLoopBlocking selfLoopBlocking, IUriGenerator uriGenerator, IRegularDataService regularDataService
            , ILogger logger, IOrphanDbService orphanDbService, IBailDbService bailDbService)
        {
            _selfLoopBlocking = selfLoopBlocking;
            _uriGenerator = uriGenerator;
            _regularDataService = regularDataService;
            _logger = logger;
            _orphanDbService = orphanDbService;
            _bailDbService = bailDbService;
        }

        public async Task<OrphanageDataModel.Persons.Guarantor> AddGuarantor(OrphanageDataModel.Persons.Guarantor guarantor)
        {
            _logger.Information($"Trying to add new Guarantor");
            if (guarantor == null)
            {
                _logger.Error($"the parameter object guarantorToAdd is null, NullReferenceException will be thrown");
                throw new NullReferenceException();
            }
            if (guarantor.Name == null)
            {
                _logger.Error($"the Name object of the parameter object guarantorToAdd is null, NullReferenceException will be thrown");
                throw new NullReferenceException();
            }
            if (guarantor.AccountId <= 0)
            {
                _logger.Error($"the AccountId of the parameter object guarantorToAdd equals {guarantor.AccountId}, NullReferenceException will be thrown");
                throw new NullReferenceException();
            }
            using (var orphanageDBC = new OrphanageDbCNoBinary())
            {
                using (var Dbt = orphanageDBC.Database.BeginTransaction())
                {
                    if (!Properties.Settings.Default.ForceAdd)
                    {
                        _logger.Information($"ForceAdd option is not activated");
                        if (Properties.Settings.Default.CheckName)
                        {
                            _logger.Information($"CheckName option is activated, trying to get the equal names from database");
                            var retguarantors = GetGuarantorByName(guarantor.Name, orphanageDBC).FirstOrDefault();
                            if (retguarantors != null)
                            {
                                _logger.Error($"guarantor with id({retguarantors.Id}) has the same name, DuplicatedObjectException will be thrown");
                                throw new DuplicatedObjectException(guarantor.GetType(), retguarantors.GetType(), retguarantors.Id);
                            }
                            else
                            {
                                _logger.Information($"didn't found any similar names to ({guarantor.Name.FullName()}) in the database");
                            }
                        }
                        if (Properties.Settings.Default.CheckContactData)
                        {
                            _logger.Information($"CheckContactData option is activated, trying to get the equal contact data for the guarantor address from database");
                            var retguarantors = GetGuarantorByAddress(guarantor.Address, orphanageDBC).FirstOrDefault();
                            if (retguarantors != null)
                            {
                                _logger.Error($"guarantor with id({retguarantors.Id}) has the same address, DuplicatedObjectException will be thrown");
                                throw new DuplicatedObjectException(guarantor.GetType(), retguarantors.GetType(), retguarantors.Id);
                            }
                            else
                            {
                                _logger.Information($"didn't found any similar contact data to guarantor address in the database");
                            }
                        }
                    }
                    var nameId = await _regularDataService.AddName(guarantor.Name, orphanageDBC);
                    if (nameId == -1)
                    {
                        Dbt.Rollback();
                        _logger.Warning($"Name object has not been added, nothing will be added, null will be returned");
                        return null;
                    }
                    guarantor.NameId = nameId;
                    if (guarantor.Address != null)
                    {
                        var addressId = await _regularDataService.AddAddress(guarantor.Address, orphanageDBC);
                        if (addressId == -1)
                        {
                            Dbt.Rollback();
                            _logger.Warning($"Address object has not been added, nothing will be added, null will be returned");
                            return null;
                        }
                        guarantor.AddressId = addressId;
                    }
                    if (guarantor.Orphans != null || guarantor.Orphans.Count > 0) guarantor.Orphans = null;
                    if (guarantor.Account != null) guarantor.Account = null;
                    orphanageDBC.Guarantors.Add(guarantor);
                    var ret = await orphanageDBC.SaveChangesAsync();
                    if (ret >= 1)
                    {
                        Dbt.Commit();
                        _logger.Information($"new guarantor object with id {guarantor.Id} has been added");
                        _selfLoopBlocking.BlockGuarantorSelfLoop(ref guarantor);
                        _logger.Information($"the guarantor object with id {guarantor.Id}  will be returned");
                        return guarantor;
                    }
                    else
                    {
                        Dbt.Rollback();
                        _logger.Warning($"something went wrong, nothing was added, null will be returned");
                        return null;
                    }
                }
            }
        }

        public async Task<bool> DeleteGuarantor(int Gid, bool forceDelete)
        {
            _logger.Information($"trying to delete Guarantor with id({Gid})");
            if (Gid <= 0)
            {
                _logger.Error($"the integer parameter Guarantor ID is less than zero, false will be returned");
                return false;
            }
            using (var orphanageDb = new OrphanageDbCNoBinary())
            {
                using (var dbT = orphanageDb.Database.BeginTransaction())
                {
                    int ret = 0;
                    var guarantor = await orphanageDb.Guarantors.Where(c => c.Id == Gid)
                        .Include(c => c.Name)
                        .Include(c => c.Address)
                        .Include(c => c.Bails)
                        .Include(c => c.Orphans)
                        .FirstOrDefaultAsync();

                    if (guarantor == null)
                    {
                        _logger.Error($"the original guarantor object with id {Gid} object is not founded, ObjectNotFoundException will be thrown");
                        throw new Exceptions.ObjectNotFoundException();
                    }
                    if (guarantor.Orphans != null && guarantor.Orphans.Count > 0)
                    {
                        //the guarantor has another orphans
                        if (forceDelete)
                        {
                            _logger.Warning($"the guarantor object with id {Gid} has not null foreign key on Orphans table, all related orphan object will be not bailed");
                            var orphansIds = guarantor.Orphans.Select(o => o.Id).ToList();
                            var succeeded = await _orphanDbService.BailOrphans(-1, orphansIds);
                            if (succeeded)
                            {
                                _logger.Information($"all related orphans to the guarantor with id ({Gid}) are set to not bailed");
                            }
                            else
                            {
                                _logger.Error($"failed to set orphans to not bailed");
                                dbT.Rollback();
                                return false;
                            }
                        }
                        else
                        {
                            _logger.Error($"the guarantor object with id {Gid} has not null foreign key on Orphans table, HasForeignKeyException will be thrown");
                            throw new HasForeignKeyException(typeof(OrphanageDataModel.Persons.Guarantor), typeof(OrphanageDataModel.Persons.Orphan));
                        }
                    }
                    if (guarantor.Bails != null && guarantor.Bails.Count > 0)
                    {
                        // the guarantor has bails foreign keys
                        if (forceDelete)
                        {
                            _logger.Warning($"the guarantor object with id {Gid} has not null foreign key on Bails table, all related bails objects will be deleted");
                            foreach (var bail in guarantor.Bails)
                            {
                                var deleted = await _bailDbService.DeleteBail(bail.Id, forceDelete);
                                if (!deleted)
                                {
                                    _logger.Error($"cannot delete guarantor with the id ({Gid}) because it has a undeleted records on bail table, false will be returned");
                                    dbT.Rollback();
                                    return false;
                                }
                            }
                        }
                        else
                        {
                            _logger.Error($"the guarantor object with id {Gid} has not null foreign key on Bails table, HasForeignKeyException will be thrown");
                            throw new HasForeignKeyException(typeof(OrphanageDataModel.Persons.Guarantor), typeof(OrphanageDataModel.FinancialData.Bail));
                        }
                    }
                    var guarantorName = guarantor.Name;
                    var guarantorAdderss = guarantor.Address;
                    orphanageDb.Guarantors.Remove(guarantor);
                    ret += await orphanageDb.SaveChangesAsync();
                    ret += await _regularDataService.DeleteName(guarantorName.Id, orphanageDb) ? 1 : 0;
                    if (guarantorAdderss != null)
                        ret += await _regularDataService.DeleteAddress(guarantorAdderss.Id, orphanageDb) ? 1 : 0;
                    if (ret > 0)
                    {
                        dbT.Commit();
                        _logger.Information($"guarantor with id({Gid}) has been successfully deleted from the database");
                        return true;
                    }
                    else
                    {
                        dbT.Rollback();
                        _logger.Information($"nothing has changed, false will be returned");
                        return false;
                    }
                }
            }
        }

        public async Task<IEnumerable<OrphanageDataModel.FinancialData.Bail>> GetBails(int Gid)
        {
            _logger.Information($"Trying to get Bails that related to the Guarantor with id {Gid}");
            IList<OrphanageDataModel.FinancialData.Bail> returnedBails = new List<OrphanageDataModel.FinancialData.Bail>();
            using (var dbContext = new OrphanageDbCNoBinary())
            {
                var bails = await (from bail in dbContext.Bails.AsNoTracking()
                                   where bail.GuarantorID == Gid
                                   select bail)
                                     .Include(b => b.Account)
                              .ToListAsync();

                if (bails == null || bails.Count == 0)
                {
                    _logger.Information($"guarantor with id({Gid}) has no bails to return, null will be returned");
                    return null;
                }
                foreach (var bail in bails)
                {
                    var bailToFill = bail;
                    _selfLoopBlocking.BlockBailSelfLoop(ref bailToFill);
                    _logger.Information($"adding bail with id({bail.Id}) to the returned List");
                    returnedBails.Add(bailToFill);
                }
            }
            _logger.Information($"({returnedBails.Count}) records of bails will be returned");
            return returnedBails;
        }

        public async Task<int> GetBailsCount(int Gid)
        {
            _logger.Information($"Trying to get the count of bails that related to the Guarantor with id {Gid}");
            using (var dbContext = new OrphanageDbCNoBinary())
            {
                var bailsCount = await (from bail in dbContext.Bails.AsNoTracking()
                                        where bail.GuarantorID == Gid
                                        select bail)
                              .CountAsync();

                _logger.Information($"({bailsCount}) is the count of bails that related to the Guarantor with id {Gid}");
                return bailsCount;
            }
        }

        public async Task<IEnumerable<int>> GetBailsIds(int Gid)
        {
            _logger.Information($"Trying to get the ids of Bails that related to the Guarantor with id {Gid}");
            using (var dbContext = new OrphanageDbCNoBinary())
            {
                var bailsIds = await (from bail in dbContext.Bails.AsNoTracking()
                                      where bail.GuarantorID == Gid
                                      select bail.Id)
                              .ToListAsync();

                if (bailsIds == null || bailsIds.Count == 0)
                {
                    _logger.Information($"guarantor with id({Gid}) has no bails to return, null will be returned");
                    return null;
                }

                _logger.Information($"({bailsIds.Count}) integer value of bails ids will be returned");
                return bailsIds;
            }
        }

        public async Task<IEnumerable<OrphanageDataModel.RegularData.Family>> GetFamilies(int Gid)
        {
            _logger.Information($"Trying to get families that related to the Guarantor with id {Gid}");
            IList<OrphanageDataModel.RegularData.Family> returnedFamilies = new List<OrphanageDataModel.RegularData.Family>();
            using (var dbContext = new OrphanageDbCNoBinary())
            {
                var families = await (from family in dbContext.Families.AsNoTracking()
                                      join ba in dbContext.Bails.AsNoTracking()
                                       on family.BailId equals ba.Id
                                      where ba.GuarantorID == Gid
                                      select family)
                                    .Include(f => f.AlternativeAddress)
                                    .Include(f => f.Bail)
                                    .Include(f => f.Father)
                                    .Include(f => f.Father.Name)
                                    .Include(f => f.Mother)
                                    .Include(f => f.Mother.Name)
                                    .Include(f => f.Mother.Address)
                                    .Include(f => f.Orphans)
                                    .Include(f => f.PrimaryAddress)
                              .ToListAsync();

                if (families == null || families.Count == 0)
                {
                    _logger.Information($"guarantor with id({Gid}) has no families to return, null will be returned");
                    return null;
                }
                foreach (var family in families)
                {
                    var familyToFill = family;
                    _selfLoopBlocking.BlockFamilySelfLoop(ref familyToFill);
                    _logger.Information($"adding family with id({family.Id}) to the returned List");
                    returnedFamilies.Add(familyToFill);
                }
            }
            _logger.Information($"({returnedFamilies.Count}) records of families will be returned");
            return returnedFamilies;
        }

        public async Task<int> GetFamiliesCount(int Gid)
        {
            _logger.Information($"Trying to get the count of families that related to the Guarantor with id {Gid}");
            IList<OrphanageDataModel.RegularData.Family> returnedFamilies = new List<OrphanageDataModel.RegularData.Family>();
            using (var dbContext = new OrphanageDbCNoBinary())
            {
                var familiesCount = await (from family in dbContext.Families.AsNoTracking()
                                           join ba in dbContext.Bails.AsNoTracking()
                                            on family.BailId equals ba.Id
                                           where ba.GuarantorID == Gid
                                           select family)
                              .CountAsync();

                _logger.Information($"({familiesCount}) is the count of families that related to the Guarantor with id {Gid}");
                return familiesCount;
            }
        }

        public async Task<IEnumerable<int>> GetFamiliesIds(int Gid)
        {
            _logger.Information($"Trying to get the ids of families that related to the Guarantor with id {Gid}");
            using (var dbContext = new OrphanageDbCNoBinary())
            {
                var familiesIds = await (from family in dbContext.Families.AsNoTracking()
                                         join ba in dbContext.Bails.AsNoTracking()
                                          on family.BailId equals ba.Id
                                         where ba.GuarantorID == Gid
                                         select family.Id)
                              .ToListAsync();

                if (familiesIds == null || familiesIds.Count == 0)
                {
                    _logger.Information($"guarantor with id({Gid}) has no families ids to return, null will be returned");
                    return null;
                }

                _logger.Information($"({familiesIds}) integer records of families ids will be returned");
                return familiesIds;
            }
        }

        public async Task<OrphanageDataModel.Persons.Guarantor> GetGuarantor(int Gid)
        {
            _logger.Information($"Trying to get Guarantor with id {Gid}");
            using (var dbContext = new OrphanageDbCNoBinary())
            {
                var guarantor = await dbContext.Guarantors.AsNoTracking()
                    .Include(g => g.Address)
                    .Include(c => c.Name)
                    .Include(g => g.Account)
                    .FirstOrDefaultAsync(c => c.Id == Gid);
                if (guarantor == null)
                {
                    _logger.Warning($"Guarantor with id{Gid} cannot be found null is returned");
                    return null;
                }
                _selfLoopBlocking.BlockGuarantorSelfLoop(ref guarantor);
                guarantor.Address = guarantor.Address.Clean();
                _logger.Information($"returned Guarantor with id {Gid}");
                return guarantor;
            }
        }

        public IEnumerable<OrphanageDataModel.Persons.Guarantor> GetGuarantorByAddress(Address addressObject, OrphanageDbCNoBinary orphanageDbCNo)
        {
            _logger.Information($"trying to get guarantor with the similar address");
            if (addressObject == null)
            {
                _logger.Error($"address object is null, NullReferenceException will be thrown");
                throw new NullReferenceException();
            }

            var guarantors = orphanageDbCNo.Guarantors
            .Include(m => m.Address)
            .ToArray();

            var Foundedguarantors = guarantors.Where(n => n.Address.Equals(addressObject));

            foreach (var guarantor in Foundedguarantors)
            {
                _logger.Information($"guarantor with id({guarantor.Id}) has the same address");
                guarantor.Address = guarantor.Address.Clean();
                yield return guarantor;
            }
        }

        public IEnumerable<OrphanageDataModel.Persons.Guarantor> GetGuarantorByName(Name nameObject, OrphanageDbCNoBinary orphanageDbCNo)
        {
            _logger.Information($"trying to get guarantor with the similar name");
            if (nameObject == null)
            {
                _logger.Error($"name object is null, NullReferenceException will be thrown");
                throw new NullReferenceException();
            }

            var guarantors = orphanageDbCNo.Guarantors
            .Include(m => m.Name)
            .ToArray();

            var Foundedguarantors = guarantors.Where(n =>
            n.Name.Equals(nameObject));

            if (Foundedguarantors == null) yield return null;

            foreach (var guarantor in Foundedguarantors)
            {
                _logger.Information($"guarantor with id({guarantor.Id}) has the same Name as ({nameObject.FullName()})");
                yield return guarantor;
            }
        }

        public async Task<IEnumerable<OrphanageDataModel.Persons.Guarantor>> GetGuarantors(int pageSize, int pageNum)
        {
            _logger.Information($"Trying to get Guarantors with pageSize {pageSize} and pageNumber {pageNum}");
            IList<OrphanageDataModel.Persons.Guarantor> guarantorsList = new List<OrphanageDataModel.Persons.Guarantor>();
            using (var _orphanageDBC = new OrphanageDbCNoBinary())
            {
                int totalSkiped = pageSize * pageNum;

                int guarantorsCount = await _orphanageDBC.Guarantors.AsNoTracking().CountAsync();

                if (guarantorsCount < totalSkiped)
                {
                    _logger.Warning($"Total skipped guarantors({totalSkiped}) are more than the count of all guarantors ({guarantorsCount})");
                    totalSkiped = guarantorsCount - pageSize;
                }
                if (totalSkiped < 0)
                {
                    _logger.Warning($"Total skipped guarantors({totalSkiped}) are less than zero");
                    totalSkiped = 0;
                }
                var guarantors = await _orphanageDBC.Guarantors.AsNoTracking()
                    .OrderBy(c => c.Id).Skip(() => totalSkiped).Take(() => pageSize)
                    .Include(g => g.Address)
                    .Include(c => c.Name)
                    .Include(g => g.Account)
                    .ToListAsync();

                if (guarantors != null && guarantors.Count > 0)
                {
                    foreach (var guarantor in guarantors)
                    {
                        OrphanageDataModel.Persons.Guarantor guarantorToFill = guarantor;
                        _selfLoopBlocking.BlockGuarantorSelfLoop(ref guarantorToFill);
                        guarantorsList.Add(guarantorToFill);
                    }
                }
                else
                {
                    _logger.Warning($"the returned guarantors are null, empty list will be returned");
                }
            }
            return guarantorsList;
        }

        public async Task<int> GetGuarantorsCount()
        {
            _logger.Information($"Trying to get guarantors count");
            using (var _orphanageDBC = new OrphanageDbCNoBinary())
            {
                int guarantorsCount = await _orphanageDBC.Guarantors.AsNoTracking().CountAsync();
                _logger.Information($"all guarantors count ({guarantorsCount}) as integer value will be returned");
                return guarantorsCount;
            }
        }

        public async Task<IEnumerable<OrphanageDataModel.Persons.Orphan>> GetOrphans(int Gid)
        {
            _logger.Information($"Trying to get orphans those are related to the guarantor id {Gid}");
            IList<OrphanageDataModel.Persons.Orphan> returnedOrphans = new List<OrphanageDataModel.Persons.Orphan>();
            using (var dbContext = new OrphanageDbCNoBinary())
            {
                var orphans = await (from orp in dbContext.Orphans.AsNoTracking()
                                     join bail in dbContext.Bails.AsNoTracking() on orp.BailId equals bail.Id
                                     join guar in dbContext.Guarantors.AsNoTracking() on bail.GuarantorID equals guar.Id
                                     where guar.Id == Gid
                                     select orp)
                                     .Include(o => o.Education)
                                     .Include(o => o.Name)
                                     .Include(o => o.Caregiver.Name)
                                     .Include(o => o.Caregiver.Address)
                                     .Include(o => o.Family.Father.Name)
                                     .Include(o => o.Family.Mother.Name)
                                     .Include(o => o.Family.PrimaryAddress)
                                     .Include(o => o.Family.AlternativeAddress)
                                     .Include(o => o.Guarantor.Name)
                                     .Include(o => o.Bail)
                                     .Include(o => o.HealthStatus)
                              .ToListAsync();

                if (orphans != null && orphans.Count > 0)
                {
                    foreach (var orphan in orphans)
                    {
                        var orpToFill = orphan;
                        _selfLoopBlocking.BlockOrphanSelfLoop(ref orpToFill);
                        _uriGenerator.SetOrphanUris(ref orpToFill);
                        returnedOrphans.Add(orpToFill);
                    }
                }
                else
                {
                    _logger.Warning("zero or null orphans records were being got from the database");
                }
            }
            _logger.Information($"{returnedOrphans.Count} records of orphans will be returned");
            return returnedOrphans;
        }

        public async Task<int> GetOrphansCount(int Gid)
        {
            _logger.Information($"Trying to get th count of orphans those are related to the guarantor id {Gid}");
            IList<OrphanageDataModel.Persons.Orphan> returnedOrphans = new List<OrphanageDataModel.Persons.Orphan>();
            using (var dbContext = new OrphanageDbCNoBinary())
            {
                var orphansCount = await (from orp in dbContext.Orphans.AsNoTracking()
                                          join bail in dbContext.Bails.AsNoTracking() on orp.BailId equals bail.Id
                                          join guar in dbContext.Guarantors.AsNoTracking() on bail.GuarantorID equals guar.Id
                                          where guar.Id == Gid
                                          select orp).CountAsync();

                _logger.Information($"{orphansCount} is the count of orphans those are related to the guarantor id {Gid}");
                return orphansCount;
            }
        }

        public async Task<IEnumerable<int>> GetOrphansIds(int Gid)
        {
            _logger.Information($"Trying to get the ids of orphans those are related to the guarantor id {Gid}");
            IList<OrphanageDataModel.Persons.Orphan> returnedOrphans = new List<OrphanageDataModel.Persons.Orphan>();
            using (var dbContext = new OrphanageDbCNoBinary())
            {
                var orphansIds = await (from orp in dbContext.Orphans.AsNoTracking()
                                        join bail in dbContext.Bails.AsNoTracking() on orp.BailId equals bail.Id
                                        join guar in dbContext.Guarantors.AsNoTracking() on bail.GuarantorID equals guar.Id
                                        where guar.Id == Gid
                                        select orp.Id)
                              .ToListAsync();

                if (orphansIds == null || orphansIds.Count == 0)
                {
                    _logger.Information($"guarantor with id({Gid}) has no orphans Ids  to return, null will be returned");
                    return null;
                }
                _logger.Information($"{orphansIds} integer records of orphans ids will be returned");
                return orphansIds;
            }
        }

        public async Task<bool> SaveGuarantor(OrphanageDataModel.Persons.Guarantor guarantor)
        {
            _logger.Information($"Trying to save guarantor");
            if (guarantor == null)
            {
                _logger.Error($"the parameter object guarantor is null, NullReferenceException will be thrown");
                throw new NullReferenceException();
            }
            if (guarantor.AccountId <= 0)
            {
                _logger.Error($"the AccountId of the parameter object guarantor equals {guarantor.AccountId}, NullReferenceException will be thrown");
                throw new NullReferenceException();
            }
            if (guarantor.NameId <= 0)
            {
                _logger.Error($"the NameID of the parameter object guarantor equals {guarantor.NameId}, NullReferenceException will be thrown");
                throw new NullReferenceException();
            }
            using (OrphanageDbCNoBinary orphanageDc = new OrphanageDbCNoBinary())
            {
                int ret = 0;
                orphanageDc.Configuration.LazyLoadingEnabled = true;
                orphanageDc.Configuration.ProxyCreationEnabled = true;
                orphanageDc.Configuration.AutoDetectChangesEnabled = true;

                var orginalGuarantor = await orphanageDc.Guarantors.
                    Include(m => m.Address).
                    Include(c => c.Name).
                    FirstOrDefaultAsync(m => m.Id == guarantor.Id);

                if (orginalGuarantor == null)
                {
                    _logger.Error($"the original Guarantor object with id {guarantor.Id} object is not founded, ObjectNotFoundException will be thrown");
                    throw new Exceptions.ObjectNotFoundException();
                }

                _logger.Information($"processing the address object of the Guarantor with id({guarantor.Id})");
                if (guarantor.Address != null)
                    if (orginalGuarantor.Address != null)
                        //edit existing caregiver address
                        ret += await _regularDataService.SaveAddress(guarantor.Address, orphanageDc);
                    else
                    {
                        //create new address for the caregiver
                        var addressId = await _regularDataService.AddAddress(guarantor.Address, orphanageDc);
                        orginalGuarantor.AddressId = addressId;
                        ret++;
                    }
                else
                    if (orginalGuarantor.Address != null)
                {
                    //delete existing caregiver address
                    int alAdd = orginalGuarantor.AddressId.Value;
                    orginalGuarantor.AddressId = null;
                    await orphanageDc.SaveChangesAsync();
                    await _regularDataService.DeleteAddress(alAdd, orphanageDc);
                }
                _logger.Information($"processing the name object of the Guarantor with id({guarantor.Id})");
                ret += await _regularDataService.SaveName(guarantor.Name, orphanageDc);
                orginalGuarantor.AccountId = guarantor.AccountId;
                orginalGuarantor.ColorMark = guarantor.ColorMark;
                orginalGuarantor.IsPayingMonthly = guarantor.IsPayingMonthly;
                orginalGuarantor.IsStillPaying = guarantor.IsStillPaying;
                orginalGuarantor.IsSupportingOnlyFamilies = guarantor.IsSupportingOnlyFamilies;
                orginalGuarantor.Note = guarantor.Note;
                ret += await orphanageDc.SaveChangesAsync();
                if (ret > 0)
                {
                    _logger.Information($"Guarantor with id({guarantor.Id}) has been successfully saved to the database, {ret} changes have been made");
                    return true;
                }
                else
                {
                    _logger.Information($"nothing has changed, false will be returned");
                    return false;
                }
            }
        }

        public async Task SetGuarantorColor(int Gid, int? value)
        {
            _logger.Information($"trying to set the color value ({value ?? -1}) to the guarantor with Id({Gid})");
            using (var _orphanageDBC = new OrphanageDBC())
            {
                _orphanageDBC.Configuration.AutoDetectChangesEnabled = true;
                _orphanageDBC.Configuration.LazyLoadingEnabled = true;
                _orphanageDBC.Configuration.ProxyCreationEnabled = true;

                var guarantor = await _orphanageDBC.Guarantors.Where(m => m.Id == Gid).FirstOrDefaultAsync();

                if (guarantor == null)
                {
                    _logger.Error($"guarantor with id ({Gid}) has not been found, ObjectNotFoundException will be thrown");
                    throw new ObjectNotFoundException();
                }
                guarantor.ColorMark = value;

                if (await _orphanageDBC.SaveChangesAsync() > 0)
                {
                    _logger.Information($"color value ({value ?? -1}) has been set successfully to the guarantor with id({Gid})");
                }
                else
                {
                    _logger.Warning($"color value ({value ?? -1}) has not been set to the guarantor with id({Gid}), nothing has changed");
                }
            }
        }
    }
}