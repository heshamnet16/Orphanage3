﻿using OrphanageService.DataContext;
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
            IList<OrphanageDataModel.FinancialData.Bail> bailsList = new List<OrphanageDataModel.FinancialData.Bail>();
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

                if (bails != null && bails.Count > 0)
                {
                    foreach (var bail in bails)
                    {
                        OrphanageDataModel.FinancialData.Bail bailsToFill = bail;
                        _selfLoopBlocking.BlockBailSelfLoop(ref bailsToFill);
                        bailsList.Add(bailsToFill);
                    }
                }
                else
                {
                    _logger.Warning($"the returned bails are null, empty list will be returned");
                }
            }
            _logger.Information($"{bailsList.Count} records of bails will be returned");
            return bailsList;
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
                        _logger.Information($"something went wrong, nothing was added, null will be returned");
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
            if (bailID <= 0)
            {
                _logger.Error($"the integer parameter bailID less than zero, NullReferenceException will be thrown");
                throw new NullReferenceException();
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
                            _logger.Error($"the bail object with id {bailID} has not null foreign key on Orphans table, all related orphan object will be not bailed");
                            foreach (var orphan in bail.Orphans)
                            {
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
                            foreach (var family in bail.Families)
                            {
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
                    if (await orphanageDb.SaveChangesAsync() > 1)
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
    }
}