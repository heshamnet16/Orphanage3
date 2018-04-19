using OrphanageService.DataContext;
using OrphanageService.Services.Exceptions;
using OrphanageService.Services.Interfaces;
using OrphanageService.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace OrphanageService.Services
{
    public class BailDbService : IBailDbService
    {
        private readonly ISelfLoopBlocking _selfLoopBlocking;
        private readonly IUriGenerator _uriGenerator;
        private readonly ILogger _logger;

        public BailDbService(ISelfLoopBlocking selfLoopBlocking, IUriGenerator uriGenerator, ILogger logger)
        {
            _selfLoopBlocking = selfLoopBlocking;
            _uriGenerator = uriGenerator;
            _logger = logger;
        }

        public async Task<OrphanageDataModel.FinancialData.Bail> GetBailDC(int Bid)
        {
            _logger.Information($"Trying to get Bail with id {Bid}");
            using (var dbContext = new OrphanageDbCNoBinary())
            {
                var bail = await dbContext.Bails.AsNoTracking()
                    .Include(b => b.Account)
                    .Include(b => b.Guarantor)
                    .Include(b => b.Guarantor.Name)
                    .FirstOrDefaultAsync(b => b.Id == Bid);

                if (bail == null)
                {
                    _logger.Warning($"Bail with id{Bid} cannot be found null is returned");
                    return null;
                }
                _selfLoopBlocking.BlockBailSelfLoop(ref bail);
                _logger.Information($"returned Bail with id {Bid}");
                return bail;
            }
        }

        public async Task<IEnumerable<OrphanageDataModel.FinancialData.Bail>> GetBails(int pageSize, int pageNum)
        {
            _logger.Information($"Trying to get Bails with pageSize {pageSize} and pageNumber {pageNum}");
            using (var _orphanageDBC = new OrphanageDbCNoBinary())
            {
                int totalSkiped = pageSize * pageNum;
                int bailsCount = 0;

                bailsCount = await _orphanageDBC.Bails.AsNoTracking().CountAsync();

                if (bailsCount < totalSkiped)
                {
                    _logger.Warning($"Total skipped bails({totalSkiped}) are more than the count of all bails ({bailsCount})");
                    totalSkiped = bailsCount - pageSize;
                }
                if (totalSkiped < 0)
                {
                    _logger.Warning($"Total skipped bails({totalSkiped}) are less than zero");
                    totalSkiped = 0;
                }
                var bails = await _orphanageDBC.Bails.AsNoTracking()
                    .OrderBy(c => c.Id).Skip(() => totalSkiped).Take(() => pageSize)
                    .Include(b => b.Account)
                    .Include(b => b.Guarantor)
                    .Include(b => b.Guarantor.Name)
                    .ToListAsync();

                return prepareBailsList(bails);
            }
        }

        public async Task<int> GetBailsCount()
        {
            _logger.Information($"Trying to get Bails count");
            using (var _orphanageDBC = new OrphanageDbCNoBinary())
            {
                int bailsCount = await _orphanageDBC.Bails.AsNoTracking().CountAsync();
                _logger.Information($"all bails count ({bailsCount}) as integer value will be returned");
                return bailsCount;
            }
        }

        public async Task<IEnumerable<OrphanageDataModel.RegularData.Family>> GetFamilies(int Bid)
        {
            _logger.Information($"Trying to get families those are related to the bail id {Bid}");
            IList<OrphanageDataModel.RegularData.Family> returnedFamilies = new List<OrphanageDataModel.RegularData.Family>();
            using (var dbContext = new OrphanageDbCNoBinary())
            {
                var families = await (from orp in dbContext.Families.AsNoTracking()
                                      where orp.BailId == Bid
                                      select orp)
                                    .Include(f => f.AlternativeAddress)
                                    .Include(f => f.Bail)
                                    .Include(f => f.Father)
                                    .Include(f => f.Mother)
                                    .Include(f => f.Orphans)
                                    .Include(f => f.PrimaryAddress)
                              .ToListAsync();
                if (families != null && families.Count > 0)
                {
                    _logger.Information($"{families.Count} records of families already being got from the database");
                    foreach (var fam in families)
                    {
                        var famToFill = fam;
                        _selfLoopBlocking.BlockFamilySelfLoop(ref famToFill);
                        _uriGenerator.SetFamilyUris(ref famToFill);
                        returnedFamilies.Add(famToFill);
                    }
                }
                else
                {
                    _logger.Warning("zero or null families records were being got from the database");
                }
            }
            _logger.Information($"{returnedFamilies.Count} records of families will be returned");
            return returnedFamilies;
        }

        public async Task<IEnumerable<OrphanageDataModel.Persons.Orphan>> GetOrphans(int Bid)
        {
            _logger.Information($"Trying to get orphans those are related to the bail id {Bid}");
            IList<OrphanageDataModel.Persons.Orphan> returnedOrphans = new List<OrphanageDataModel.Persons.Orphan>();
            using (var dbContext = new OrphanageDbCNoBinary())
            {
                var orphans = await (from orp in dbContext.Orphans.AsNoTracking()
                                     where orp.BailId == Bid
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
                    _logger.Information($"{orphans.Count} records of orphans already being got from the database");
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

        public async Task<int> GetFamiliesCount(int Bid)
        {
            _logger.Information($"Trying to get the count families those are related to the bail id {Bid}");
            using (var dbContext = new OrphanageDbCNoBinary())
            {
                var bailsCount = await (from orp in dbContext.Families.AsNoTracking()
                                        where orp.BailId == Bid
                                        select orp)
                              .CountAsync();
                _logger.Information($"count of families those related to bail with id ({Bid}) is ({bailsCount}), and it will be returned");
                return bailsCount;
            }
        }

        public async Task<int> GetOrphansCount(int Bid)
        {
            _logger.Information($"Trying to get the count orphans those are related to the bail id {Bid}");
            using (var dbContext = new OrphanageDbCNoBinary())
            {
                var bailsCount = await (from orp in dbContext.Orphans.AsNoTracking()
                                        where orp.BailId == Bid
                                        select orp)
                              .CountAsync();
                _logger.Information($"count of orphans those related to bail with id ({Bid}) is ({bailsCount}), and it will be returned");
                return bailsCount;
            }
        }

        public async Task<IEnumerable<int>> GetFamiliesIds(int Bid)
        {
            _logger.Information($"Trying to get the families ids those are related to the bail id {Bid}");
            using (var dbContext = new OrphanageDbCNoBinary())
            {
                var familiesIds = await (from fam in dbContext.Families.AsNoTracking()
                                         where fam.BailId == Bid
                                         select fam.Id)
                              .ToListAsync();
                _logger.Information($"({familiesIds}) records of families ids those related to bail with id ({Bid}), will be returned");
                return familiesIds;
            }
        }

        public async Task<IEnumerable<int>> GetOrphansIds(int Bid)
        {
            _logger.Information($"Trying to get the orphans ids those are related to the bail id {Bid}");
            using (var dbContext = new OrphanageDbCNoBinary())
            {
                var orphansIds = await (from orp in dbContext.Orphans.AsNoTracking()
                                        where orp.BailId == Bid
                                        select orp.Id)
                              .ToListAsync();
                _logger.Information($"({orphansIds}) records of orphans ids those related to bail with id ({Bid}), will be returned");
                return orphansIds;
            }
        }

        public async Task<OrphanageDataModel.FinancialData.Bail> AddBail(OrphanageDataModel.FinancialData.Bail bailToAdd)
        {
            _logger.Information($"Trying to add new Bail");
            if (bailToAdd == null)
            {
                _logger.Error($"the parameter object bailToAdd is null, NullReferenceException will be thrown");
                throw new NullReferenceException();
            }
            if (bailToAdd.AccountID <= 0)
            {
                _logger.Error($"the AccountId of the parameter object bailToAdd equals {bailToAdd.AccountID}, NullReferenceException will be thrown");
                throw new NullReferenceException();
            }
            if (bailToAdd.GuarantorID <= 0)
            {
                _logger.Error($"the GuarantorID of the parameter object bailToAdd equals {bailToAdd.GuarantorID}, NullReferenceException will be thrown");
                throw new NullReferenceException();
            }
            using (var orphanageDBC = new OrphanageDbCNoBinary())
            {
                using (var Dbt = orphanageDBC.Database.BeginTransaction())
                {
                    if (bailToAdd.Account != null)
                        bailToAdd.Account = null;

                    if (bailToAdd.ActingUser != null)
                        bailToAdd = null;

                    if (bailToAdd.Guarantor != null)
                        bailToAdd.Guarantor = null;

                    orphanageDBC.Bails.Add(bailToAdd);
                    var ret = await orphanageDBC.SaveChangesAsync();
                    if (ret >= 1)
                    {
                        Dbt.Commit();
                        _logger.Information($"new bail object with id{bailToAdd.Id} has been added");
                        _selfLoopBlocking.BlockBailSelfLoop(ref bailToAdd);
                        _logger.Information($"the bail object with id{bailToAdd.Id} will be returned");
                        return bailToAdd;
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

        public async Task<bool> SaveBail(OrphanageDataModel.FinancialData.Bail bailToSave)
        {
            _logger.Information($"Trying to save Bail");
            if (bailToSave == null)
            {
                _logger.Error($"the parameter object bailToSave is null, NullReferenceException will be thrown");
                throw new NullReferenceException();
            }
            if (bailToSave.AccountID <= 0)
            {
                _logger.Error($"the AccountId of the parameter object bailToSave equals {bailToSave.AccountID}, NullReferenceException will be thrown");
                throw new NullReferenceException();
            }
            if (bailToSave.GuarantorID <= 0)
            {
                _logger.Error($"the GuarantorID of the parameter object bailToSave equals {bailToSave.GuarantorID}, NullReferenceException will be thrown");
                throw new NullReferenceException();
            }
            using (OrphanageDbCNoBinary orphanageDc = new OrphanageDbCNoBinary())
            {
                int ret = 0;
                orphanageDc.Configuration.LazyLoadingEnabled = true;
                orphanageDc.Configuration.ProxyCreationEnabled = true;
                orphanageDc.Configuration.AutoDetectChangesEnabled = true;

                var orginalBail = await orphanageDc.Bails.
                    Include(m => m.Account).
                    Include(c => c.Guarantor).
                    FirstOrDefaultAsync(m => m.Id == bailToSave.Id);

                if (orginalBail == null)
                {
                    _logger.Error($"the original bail object with id {bailToSave.Id} object is not founded, ObjectNotFoundException will be thrown");
                    throw new Exceptions.ObjectNotFoundException();
                }

                orginalBail.AccountID = bailToSave.AccountID;
                orginalBail.Amount = bailToSave.Amount;
                orginalBail.EndDate = bailToSave.EndDate;
                orginalBail.GuarantorID = bailToSave.GuarantorID;
                orginalBail.IsExpired = bailToSave.IsExpired;
                orginalBail.IsFamilyBail = bailToSave.IsFamilyBail;
                orginalBail.IsMonthlyBail = bailToSave.IsMonthlyBail;
                orginalBail.StartDate = bailToSave.StartDate;
                orginalBail.Note = bailToSave.Note;
                ret += await orphanageDc.SaveChangesAsync();
                if (ret > 0)
                {
                    _logger.Information($"the object bail with id {orginalBail.Id} has been saved successfully, {ret} changes has been made ");
                    return true;
                }
                else
                {
                    _logger.Information($"the object bail with id {orginalBail.Id} has been saved successfully, nothing has changed");
                    return false;
                }
            }
        }

        public async Task<bool> DeleteBail(int bailID, bool forceDelete)
        {
            _logger.Information($"trying to delete bail with id({bailID})");
            if (bailID <= 0)
            {
                _logger.Error($"the integer parameter bailID less than zero, false will be returned");
                return false;
            }
            using (var orphanageDb = new OrphanageDbCNoBinary())
            {
                using (var dbT = orphanageDb.Database.BeginTransaction())
                {
                    var bail = await orphanageDb.Bails.Where(c => c.Id == bailID)
                        .Include(b => b.Families)
                        .Include(b => b.Orphans)
                        .FirstOrDefaultAsync();

                    if (bail == null)
                    {
                        _logger.Error($"the original bail object with id {bailID} object is not founded, ObjectNotFoundException will be thrown");
                        throw new Exceptions.ObjectNotFoundException();
                    }
                    if (bail.Orphans != null && bail.Orphans.Count > 0)
                    {
                        //the bail has another orphans
                        if (forceDelete)
                        {
                            _logger.Warning($"the bail object with id {bailID} has not null foreign key on Orphans table, all related orphan object will be not bailed");
                            int orphansCount = bail.Orphans.Count;
                            for (int i = 0; i < orphansCount; i++)
                            {
                                var orphan = bail.Orphans.ToArray()[0];
                                _logger.Warning($"trying to set orphan ({orphan.Id}) to not bailed");
                                try
                                {
                                    orphan.IsBailed = false;
                                    orphan.Bail = null;
                                    orphan.BailId = null;
                                    await orphanageDb.SaveChangesAsync();
                                }
                                catch
                                {
                                    _logger.Error($"failed to set orphan ({orphan.Id}) to not bailed");
                                    dbT.Rollback();
                                    return false;
                                }
                            }
                        }
                        else
                        {
                            _logger.Error($"the bail object with id {bailID} has not null foreign key on Orphans table, HasForeignKeyException will be thrown");
                            throw new HasForeignKeyException(typeof(OrphanageDataModel.FinancialData.Bail), typeof(OrphanageDataModel.Persons.Orphan));
                        }
                    }
                    if (bail.Families != null && bail.Families.Count > 0)
                    {
                        //the bail has another orphans
                        if (forceDelete)
                        {
                            _logger.Error($"the bail object with id {bailID} has not null foreign key on Families table, all related families object will be not bailed");
                            int FamilesCount = bail.Families.Count;
                            for (int i = 0; i < FamilesCount; i++)
                            {
                                var family = bail.Families.ToArray()[0];
                                _logger.Warning($"trying to set family ({family.Id}) to not bailed");
                                try
                                {
                                    family.IsBailed = false;
                                    family.Bail = null;
                                    family.BailId = null;
                                    await orphanageDb.SaveChangesAsync();
                                }
                                catch
                                {
                                    _logger.Error($"failed to set family ({family.Id}) to not bailed");
                                }
                            }
                        }
                        else
                        {
                            _logger.Error($"the bail object with id {bailID} has not null foreign key on Families table, HasForeignKeyException will be thrown");
                            throw new HasForeignKeyException(typeof(OrphanageDataModel.FinancialData.Bail), typeof(OrphanageDataModel.RegularData.Family));
                        }
                    }
                    orphanageDb.Bails.Remove(bail);
                    if (await orphanageDb.SaveChangesAsync() > 0)
                    {
                        dbT.Commit();
                        _logger.Information($"the bail object with id {bailID} has been successfully removed");
                        return true;
                    }
                    else
                    {
                        dbT.Rollback();
                        _logger.Information($"something went wrong, bail with id ({bailID}) was not be removed, false will be returned");
                        return false;
                    }
                }
            }
        }

        public async Task<bool> UnBailEverything(int bailID)
        {
            _logger.Information($"trying to set all related orphans and families of the bail with id({bailID}) to null");
            if (bailID <= 0)
            {
                _logger.Error($"the integer parameter bailID less than zero, false will be returned");
                return false;
            }
            using (var orphanageDb = new OrphanageDbCNoBinary())
            {
                using (var dbT = orphanageDb.Database.BeginTransaction())
                {
                    var bail = await orphanageDb.Bails.Where(c => c.Id == bailID)
                        .Include(b => b.Families)
                        .Include(b => b.Orphans)
                        .FirstOrDefaultAsync();

                    if (bail == null)
                    {
                        _logger.Error($"the original bail object with id {bailID} object is not founded, ObjectNotFoundException will be thrown");
                        throw new Exceptions.ObjectNotFoundException();
                    }
                    if (bail.Orphans != null && bail.Orphans.Count > 0)
                    {
                        //the bail has another orphans
                        foreach (var orphan in bail.Orphans)
                        {
                            _logger.Information($"trying to set orphan ({orphan.Id}) to not bailed");
                            try
                            {
                                orphan.IsBailed = false;
                                orphan.Bail = null;
                                orphan.BailId = null;
                                await orphanageDb.SaveChangesAsync();
                            }
                            catch
                            {
                                _logger.Error($"failed to set orphan ({orphan.Id}) to not bailed");
                                dbT.Rollback();
                                return false;
                            }
                        }
                    }
                    if (bail.Families != null && bail.Families.Count > 0)
                    {
                        //the bail has another families
                        foreach (var family in bail.Families)
                        {
                            _logger.Information($"trying to set family ({family.Id}) to not bailed");
                            try
                            {
                                family.IsBailed = false;
                                family.Bail = null;
                                family.BailId = null;
                                await orphanageDb.SaveChangesAsync();
                            }
                            catch
                            {
                                _logger.Error($"failed to set family ({family.Id}) to not bailed");
                            }
                        }
                    }
                    if (await orphanageDb.SaveChangesAsync() > 1)
                    {
                        dbT.Commit();
                        _logger.Information($"all orphans and families that related to the bail object with id {bailID} has been successfully set to null");
                        return true;
                    }
                    else
                    {
                        dbT.Rollback();
                        _logger.Information($"nothing was changed, true will be returned because no errors");
                        return true;
                    }
                }
            }
        }

        public async Task<IEnumerable<OrphanageDataModel.FinancialData.Bail>> GetBails(bool isFamily)
        {
            _logger.Information($"Trying to get Bails with isFamily equals {isFamily}");
            using (var _orphanageDBC = new OrphanageDbCNoBinary())
            {
                var bails = await _orphanageDBC.Bails.AsNoTracking()
                    .Include(b => b.Account)
                    .Include(b => b.Guarantor)
                    .Include(b => b.Guarantor.Name)
                    .Where(b => b.IsFamilyBail == isFamily)
                    .ToListAsync();
                return prepareBailsList(bails);
            }
        }

        public async Task<IEnumerable<OrphanageDataModel.FinancialData.Bail>> GetBails(IList<int> bailsIds)
        {
            _logger.Information($"trying to get bails with the given Id list");
            if (bailsIds == null || bailsIds.Count() == 0)
            {
                _logger.Information($"the given Id list is null or empty, null will be returned");
                return null;
            }
            using (var _orphanageDBC = new OrphanageDbCNoBinary())
            {
                var bails = await _orphanageDBC.Bails.AsNoTracking()
                    .Where(f => bailsIds.Contains(f.Id))
                    .Include(b => b.Account)
                    .Include(b => b.Guarantor)
                    .Include(b => b.Guarantor.Name)
                    .ToListAsync();

                return prepareBailsList(bails);
            }
        }

        private IEnumerable<OrphanageDataModel.FinancialData.Bail> prepareBailsList(IEnumerable<OrphanageDataModel.FinancialData.Bail> bailsList)
        {
            IList<OrphanageDataModel.FinancialData.Bail> returnedbailsList = new List<OrphanageDataModel.FinancialData.Bail>();
            if (bailsList != null && bailsList.Count() > 0)
            {
                foreach (var bail in bailsList)
                {
                    OrphanageDataModel.FinancialData.Bail bailsToFill = bail;
                    _selfLoopBlocking.BlockBailSelfLoop(ref bailsToFill);
                    returnedbailsList.Add(bailsToFill);
                }
            }
            else
            {
                _logger.Warning($"the returned bails are null, empty list will be returned");
            }
            _logger.Information($"{returnedbailsList.Count} records of bails will be returned");
            return returnedbailsList;
        }
    }
}