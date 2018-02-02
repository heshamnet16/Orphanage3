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
    public class MotherDbService : IMotherDbService
    {
        private readonly ISelfLoopBlocking _selfLoopBlocking;
        private readonly IUriGenerator _uriGenerator;
        private readonly IRegularDataService _regularDataService;

        public MotherDbService(ISelfLoopBlocking selfLoopBlocking, IUriGenerator uriGenerator, IRegularDataService regularDataService)
        {
            _selfLoopBlocking = selfLoopBlocking;
            _uriGenerator = uriGenerator;
            _regularDataService = regularDataService;
        }

        public static void setMotherEntities(ref OrphanageDataModel.Persons.Mother mother, DbContext dbContext)
        {
            OrphanageDbCNoBinary dbCNoBinary = (OrphanageDbCNoBinary)dbContext;
            foreach (var fam in mother.Families)
            {
                var father = dbCNoBinary.Fathers.
                    Include(m => m.Name).
                    FirstOrDefault(m => m.Id == fam.MotherId);
                fam.Father = father;
            }
        }

        public async Task<int> AddMother(OrphanageDataModel.Persons.Mother mother, OrphanageDbCNoBinary orphanageDBC)
        {
            if (mother == null) throw new NullReferenceException();
            //TODO #32 check the mother data (name)
            //TODO use forceadd in the settings
            orphanageDBC.Mothers.Add(mother);

            if (await orphanageDBC.SaveChangesAsync() == 1)
                return mother.Id;
            else
                return -1;
        }

        public async Task<bool> DeleteMother(int Mid, OrphanageDbCNoBinary orphanageDb)
        {
            if (Mid == 0) throw new NullReferenceException();

            var mother = await orphanageDb.Mothers.Where(m => m.Id == Mid).FirstOrDefaultAsync();
            if (mother.Families.Count > 0)
            {
                //the mother has another family
                return true;
            }
            var motherName = mother.Name;
            var motherAddress = mother.Address;
            orphanageDb.Mothers.Remove(mother);
            orphanageDb.Names.Remove(motherName);
            orphanageDb.Addresses.Remove(motherAddress);
            if (await orphanageDb.SaveChangesAsync() > 1)
                return true;
            else
                return false;
        }

        public async Task<OrphanageDataModel.Persons.Mother> GetMother(int Mid)
        {
            using (var dbContext = new OrphanageDbCNoBinary())
            {
                var mother = await dbContext.Mothers.AsNoTracking()
                    .Include(m => m.Families)
                    .Include(m => m.Name)
                    .FirstOrDefaultAsync(m => m.Id == Mid);

                _selfLoopBlocking.BlockMotherSelfLoop(ref mother);
                setMotherEntities(ref mother, dbContext);
                _uriGenerator.SetMotherUris(ref mother);
                return mother;
            }
        }

        public async Task<int> GetMotherCount()
        {
            using (var _orphanageDBC = new OrphanageDbCNoBinary())
            {
                int mothersCount = await _orphanageDBC.Mothers.AsNoTracking().CountAsync();
                return mothersCount;
            }
        }

        public async Task<byte[]> GetMotherIdPhotoBack(int Mid)
        {
            using (var _orphanageDBC = new OrphanageDBC())
            {
                var img = await _orphanageDBC.Mothers.AsNoTracking().Where(m => m.Id == Mid).Select(m => new { m.IdentityCardPhotoBackData }).FirstOrDefaultAsync();
                return img?.IdentityCardPhotoBackData;
            }
        }

        public async Task<byte[]> GetMotherIdPhotoFace(int Mid)
        {
            using (var _orphanageDBC = new OrphanageDBC())
            {
                var img = await _orphanageDBC.Mothers.AsNoTracking().Where(m => m.Id == Mid).Select(m => new { m.IdentityCardPhotoFaceData }).FirstOrDefaultAsync();
                return img?.IdentityCardPhotoFaceData;
            }
        }

        public async Task<IEnumerable<OrphanageDataModel.Persons.Mother>> GetMothers(int pageSize, int pageNum)
        {
            IList<OrphanageDataModel.Persons.Mother> mothersList = new List<OrphanageDataModel.Persons.Mother>();
            using (var _orphanageDBC = new OrphanageDbCNoBinary())
            {
                int totalSkiped = pageSize * pageNum;
                int MothersCount = await _orphanageDBC.Mothers.AsNoTracking().CountAsync();
                if (MothersCount < totalSkiped)
                {
                    totalSkiped = MothersCount - pageSize;
                }
                if (totalSkiped < 0) totalSkiped = 0;
                var mothers = await _orphanageDBC.Mothers.AsNoTracking()
                    .OrderBy(o => o.Id).Skip(() => totalSkiped).Take(() => pageSize)
                    .Include(f => f.Families)
                    .Include(f => f.Name)
                    .ToListAsync();

                foreach (var mother in mothers)
                {
                    OrphanageDataModel.Persons.Mother motherToFill = mother;
                    setMotherEntities(ref motherToFill, _orphanageDBC);
                    _selfLoopBlocking.BlockMotherSelfLoop(ref motherToFill);
                    _uriGenerator.SetMotherUris(ref motherToFill);
                    mothersList.Add(motherToFill);
                }
            }
            return mothersList;
        }

        public async Task<IEnumerable<OrphanageDataModel.Persons.Orphan>> GetOrphans(int Mid)
        {
            IList<OrphanageDataModel.Persons.Orphan> returnedOrphans = new List<OrphanageDataModel.Persons.Orphan>();
            using (var dbContext = new OrphanageDbCNoBinary())
            {
                var orphans = await (from orp in dbContext.Orphans.AsNoTracking()
                                     join fam in dbContext.Families.AsNoTracking() on orp.Family.MotherId equals fam.MotherId
                                     where orp.Family.MotherId == Mid
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

        public async Task<bool> IsExist(int Mid)
        {
            if (Mid <= 0) throw new NullReferenceException();
            using (var orphangeDC = new OrphanageDbCNoBinary())
            {
                return await orphangeDC.Mothers.Where(m => m.Id == Mid).AnyAsync();
            }
        }

        public async Task<bool> SaveMother(OrphanageDataModel.Persons.Mother mother)
        {
            if (mother == null) throw new NullReferenceException();
            using (OrphanageDbCNoBinary orphanageDc = new OrphanageDbCNoBinary())
            {
                orphanageDc.Configuration.AutoDetectChangesEnabled = true;
                var motherToReplace = await orphanageDc.Mothers.Where(m => m.Id == mother.Id).FirstAsync();
                if (motherToReplace == null) throw new ObjectNotFoundException();
                await _regularDataService.SaveAddress(mother.Address, orphanageDc);
                await _regularDataService.SaveName(mother.Name, orphanageDc);
                motherToReplace.AddressId = mother.AddressId;
                motherToReplace.Birthday = mother.Birthday;
                motherToReplace.ColorMark = mother.ColorMark;
                motherToReplace.DateOfDeath = mother.DateOfDeath;
                motherToReplace.HasSheOrphans = mother.HasSheOrphans;
                motherToReplace.HusbandName = mother.HusbandName;
                motherToReplace.IsDead = mother.IsDead;
                motherToReplace.IsMarried = mother.IsMarried;
                motherToReplace.Jop = mother.Jop;
                motherToReplace.NameId = mother.NameId;
                motherToReplace.Note = mother.Note;
                motherToReplace.Salary = mother.Salary;
                motherToReplace.Story = mother.Story;
                await orphanageDc.SaveChangesAsync();
            }
            return true;
        }
    }
}