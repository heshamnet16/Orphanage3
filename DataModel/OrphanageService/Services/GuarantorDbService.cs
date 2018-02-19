using OrphanageDataModel.Persons;
using OrphanageDataModel.RegularData;
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
    public class GuarantorDbService : IGuarantorDbService
    {
        private readonly ISelfLoopBlocking _selfLoopBlocking;
        private readonly IUriGenerator _uriGenerator;
        private readonly IRegularDataService _regularDataService;

        public GuarantorDbService(ISelfLoopBlocking selfLoopBlocking, IUriGenerator uriGenerator, IRegularDataService regularDataService)
        {
            _selfLoopBlocking = selfLoopBlocking;
            _uriGenerator = uriGenerator;
            _regularDataService = regularDataService;
        }

        public async Task<int> AddGuarantor(OrphanageDataModel.Persons.Guarantor guarantor)
        {
            if (guarantor == null) throw new NullReferenceException();
            if (guarantor.Name == null) throw new NullReferenceException();
            if (guarantor.AccountId <= 0) throw new NullReferenceException();
            using (var orphanageDBC = new OrphanageDbCNoBinary())
            {
                using (var Dbt = orphanageDBC.Database.BeginTransaction())
                {
                    if (!Properties.Settings.Default.ForceAdd)
                    {
                        if (Properties.Settings.Default.CheckName)
                        {
                            var retguarantors = GetGuarantorByName(guarantor.Name, orphanageDBC).FirstOrDefault();
                            if (retguarantors != null)
                            {
                                throw new DuplicatedObjectException(guarantor.GetType(), retguarantors.GetType(), retguarantors.Id);
                            }
                        }
                        if (Properties.Settings.Default.CheckContactData)
                        {
                            var retguarantors = GetGuarantorByAddress(guarantor.Address, orphanageDBC).FirstOrDefault();
                            if (retguarantors != null)
                            {
                                throw new DuplicatedObjectException(guarantor.GetType(), retguarantors.GetType(), retguarantors.Id);
                            }
                        }
                    }
                    var nameId = await _regularDataService.AddName(guarantor.Name, orphanageDBC);
                    if (nameId == -1)
                    {
                        Dbt.Rollback();
                        return -1;
                    }
                    guarantor.NameId = nameId;
                    if (guarantor.Orphans != null || guarantor.Orphans.Count > 0) guarantor.Orphans = null;
                    if (guarantor.Account != null) guarantor.Account = null;
                    orphanageDBC.Guarantors.Add(guarantor);
                    var ret = await orphanageDBC.SaveChangesAsync();
                    if (ret >= 1)
                    {
                        Dbt.Commit();
                        return guarantor.Id;
                    }
                    else
                    {
                        Dbt.Rollback();
                        return -1;
                    }
                }
            }
        }

        public async Task<bool> DeleteGuarantor(int Gid)
        {
            if (Gid <= 0) throw new NullReferenceException();
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

                    if (guarantor == null) throw new ObjectNotFoundException();
                    if (guarantor.Orphans.Count > 0)
                    {
                        //the caregiver has another orphans
                        throw new HasForeignKeyException(typeof(OrphanageDataModel.Persons.Guarantor), typeof(OrphanageDataModel.Persons.Orphan));
                    }
                    if (guarantor.Bails != null && guarantor.Bails.Count > 0)
                    {
                        // the guarantor has bails foreign keys
                        throw new HasForeignKeyException(typeof(OrphanageDataModel.Persons.Guarantor), typeof(OrphanageDataModel.FinancialData.Bail));
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
                        return true;
                    }
                    else
                    {
                        dbT.Rollback();
                        return false;
                    }
                }
            }
        }

        public async Task<IEnumerable<OrphanageDataModel.FinancialData.Bail>> GetBails(int Gid)
        {
            IList<OrphanageDataModel.FinancialData.Bail> returnedBails = new List<OrphanageDataModel.FinancialData.Bail>();
            using (var dbContext = new OrphanageDbCNoBinary())
            {
                var bails = await (from bail in dbContext.Bails.AsNoTracking()
                                   where bail.GuarantorID == Gid
                                   select bail)
                                     .Include(b => b.Account)
                              .ToListAsync();
                foreach (var bail in bails)
                {
                    var bailToFill = bail;
                    _selfLoopBlocking.BlockBailSelfLoop(ref bailToFill);
                    returnedBails.Add(bailToFill);
                }
            }
            return returnedBails;
        }

        public async Task<OrphanageDataModel.Persons.Guarantor> GetGuarantor(int Gid)
        {
            using (var dbContext = new OrphanageDbCNoBinary())
            {
                var guarantor = await dbContext.Guarantors.AsNoTracking()
                    .Include(g => g.Address)
                    .Include(c => c.Name)
                    .Include(g => g.Account)
                    .FirstOrDefaultAsync(c => c.Id == Gid);
                if (guarantor == null) return null;
                _selfLoopBlocking.BlockGuarantorSelfLoop(ref guarantor);
                return guarantor;
            }
        }

        public IEnumerable<OrphanageDataModel.Persons.Guarantor> GetGuarantorByAddress(Address addressObject, OrphanageDbCNoBinary orphanageDbCNo)
        {
            if (addressObject == null) throw new NullReferenceException();

            var guarantors = orphanageDbCNo.Guarantors
            .Include(m => m.Address)
            .ToArray();

            var Foundedguarantors = guarantors.Where(n => n.Address.Equals(addressObject));

            foreach (var guarantor in Foundedguarantors)
            {
                yield return guarantor;
            }
        }

        public IEnumerable<OrphanageDataModel.Persons.Guarantor> GetGuarantorByName(Name nameObject, OrphanageDbCNoBinary orphanageDbCNo)
        {
            if (nameObject == null) throw new NullReferenceException();

            var guarantors = orphanageDbCNo.Guarantors
            .Include(m => m.Name)
            .ToArray();

            var Foundedguarantors = guarantors.Where(n =>
            n.Name.Equals(nameObject));

            if (Foundedguarantors == null) yield return null;

            foreach (var guarantor in Foundedguarantors)
            {
                yield return guarantor;
            }
        }

        public async Task<IEnumerable<OrphanageDataModel.Persons.Guarantor>> GetGuarantors(int pageSize, int pageNum)
        {
            IList<OrphanageDataModel.Persons.Guarantor> guarantorsList = new List<OrphanageDataModel.Persons.Guarantor>();
            using (var _orphanageDBC = new OrphanageDbCNoBinary())
            {
                int totalSkiped = pageSize * pageNum;
                int guarantorsCount = await _orphanageDBC.Guarantors.AsNoTracking().CountAsync();
                if (guarantorsCount < totalSkiped)
                {
                    totalSkiped = guarantorsCount - pageSize;
                }
                if (totalSkiped < 0) totalSkiped = 0;
                var guarantors = await _orphanageDBC.Guarantors.AsNoTracking()
                    .OrderBy(c => c.Id).Skip(() => totalSkiped).Take(() => pageSize)
                    .Include(g => g.Address)
                    .Include(c => c.Name)
                    .Include(g => g.Account)
                    .ToListAsync();

                foreach (var guarantor in guarantors)
                {
                    OrphanageDataModel.Persons.Guarantor guarantorToFill = guarantor;
                    _selfLoopBlocking.BlockGuarantorSelfLoop(ref guarantorToFill);
                    guarantorsList.Add(guarantorToFill);
                }
            }
            return guarantorsList;
        }

        public async Task<int> GetGuarantorsCount()
        {
            using (var _orphanageDBC = new OrphanageDbCNoBinary())
            {
                int guarantorsCount = await _orphanageDBC.Guarantors.AsNoTracking().CountAsync();
                return guarantorsCount;
            }
        }

        public async Task<IEnumerable<OrphanageDataModel.Persons.Orphan>> GetOrphans(int Gid)
        {
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
                var FamOrphans = await (from fam in dbContext.Families.AsNoTracking()
                                        join bail in dbContext.Bails.AsNoTracking() on fam.BailId equals bail.Id
                                        join gura in dbContext.Guarantors.AsNoTracking() on bail.GuarantorID equals gura.Id
                                        where gura.Id == Gid
                                        select fam)
                                        .Include(f => f.Orphans)
                                        .ToListAsync();
                foreach (var fam in FamOrphans)
                    orphans.AddRange(fam.Orphans);
                foreach (var orphan in orphans)
                {
                    var orpToFill = orphan;
                    _selfLoopBlocking.BlockOrphanSelfLoop(ref orpToFill);
                    _uriGenerator.SetOrphanUris(ref orpToFill);
                    returnedOrphans.Add(orpToFill);
                }
            }
            return returnedOrphans;
        }

        public async Task<bool> SaveGuarantor(OrphanageDataModel.Persons.Guarantor guarantor)
        {
            if (guarantor == null) throw new NullReferenceException();
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

                if (orginalGuarantor == null) throw new ObjectNotFoundException();

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
                ret += await _regularDataService.SaveName(guarantor.Name, orphanageDc);
                orginalGuarantor.AccountId = guarantor.AccountId;
                orginalGuarantor.ColorMark = guarantor.ColorMark;
                orginalGuarantor.IsPayingMonthly = guarantor.IsPayingMonthly;
                orginalGuarantor.IsStillPaying = guarantor.IsStillPaying;
                orginalGuarantor.IsSupportingTheWholeFamily = guarantor.IsSupportingTheWholeFamily;
                orginalGuarantor.Note = guarantor.Note;
                ret += await orphanageDc.SaveChangesAsync();
                return ret > 0 ? true : false;
            }
        }
    }
}