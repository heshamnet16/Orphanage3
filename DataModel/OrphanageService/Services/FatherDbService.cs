using OrphanageService.DataContext.Persons;
using OrphanageService.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using AutoMapper;
using OrphanageDataModel.Persons;
using OrphanageService.DataContext;
using OrphanageService.Utilities.Interfaces;

namespace OrphanageService.Services
{
    public class FatherDbService : IFatherDBService
    {
        private readonly ISelfLoopBlocking _selfLoopBlocking;
        private readonly IUriGenerator _uriGenerator;

        public FatherDbService(ISelfLoopBlocking selfLoopBlocking, IUriGenerator uriGenerator)
        {
            _selfLoopBlocking = selfLoopBlocking;
            _uriGenerator = uriGenerator;
        }

        public async Task<FatherDC> GetFather(int Fid)
        {
            using (var dbContext = new OrphanageDBC(true))
            {
                var father = await dbContext.Fathers.AsNoTracking()
                    .Include(f => f.Families)
                    .Include(f => f.Name)
                    .FirstOrDefaultAsync(f => f.Id == Fid);

                _selfLoopBlocking.BlockFatherSelfLoop(ref father);
                setFatherEntities(ref father, dbContext);
                FatherDC fatherDC = Mapper.Map<FatherDC>(father);
                _uriGenerator.SetFatherUris(ref fatherDC);
                return fatherDC;
            }
        }

        public async Task<byte[]> GetFatherDeathCertificate(int Fid)
        {
            using (var _orphanageDBC = new OrphanageDBC(false))
            {
                var img = await _orphanageDBC.Fathers.Where(f => f.Id == Fid).Select(f => new { f.DeathCertificatePhotoData }).FirstOrDefaultAsync();
                return img.DeathCertificatePhotoData;
            }
        }

        public async Task<byte[]> GetFatherPhoto(int Fid)
        {
            using (var _orphanageDBC = new OrphanageDBC(false))
            {
                var img = await _orphanageDBC.Fathers.Where(f => f.Id == Fid).Select(f => new { f.PhotoData }).FirstOrDefaultAsync();
                return img.PhotoData;
            }
        }

        public async Task<IEnumerable<FatherDC>> GetFathers(int pageSize, int pageNum)
        {
            IList<FatherDC> fathersList = new List<FatherDC>();
            using (var _orphanageDBC = new OrphanageDBC(true))
            {
                int totalSkiped = pageSize * pageNum;
                int FathersCount = await _orphanageDBC.Fathers.AsNoTracking().CountAsync();
                if (FathersCount < totalSkiped)
                {
                    totalSkiped = FathersCount - pageSize;
                }
                var fathers = await _orphanageDBC.Fathers.AsNoTracking()
                    .OrderBy(o => o.Id).Skip(() => totalSkiped).Take(() => pageSize)
                    .Include(f => f.Families)
                    .Include(f => f.Name)
                    .ToListAsync();

                foreach (var father in fathers)
                {
                    OrphanageDataModel.Persons.Father fatherToFill = father;
                    setFatherEntities(ref fatherToFill, _orphanageDBC);
                    _selfLoopBlocking.BlockFatherSelfLoop(ref fatherToFill);
                    FatherDC fatherDC = Mapper.Map<FatherDC>(fatherToFill);
                    _uriGenerator.SetFatherUris(ref fatherDC);
                    fathersList.Add(fatherDC);
                }
            }
            return fathersList;
        }

        public async Task<int> GetFathersCount()
        {
            using (var _orphanageDBC = new OrphanageDBC(true))
            {
                int orphansCount = await _orphanageDBC.Fathers.AsNoTracking().CountAsync();
                return orphansCount;
            }
        }

        public static void setFatherEntities(ref OrphanageDataModel.Persons.Father father, OrphanageDBC dbContext)
        {
            foreach (var fam in father.Families)
            {
                var moth = dbContext.Mothers.
                    Include(m => m.Name).
                    Include(m => m.Address).
                    FirstOrDefault(m => m.Id == fam.MotherId);
                fam.Mother = moth;
            }
        }

        public async Task<IEnumerable<OrphanDC>> GetOrphans(int Fid)
        {
            IList<OrphanDC> returnedOrphans = new List<OrphanDC>();
            using (var dbContext = new OrphanageDBC(true))
            {
                var orphans = await (from orp in dbContext.Orphans.AsNoTracking()
                                     join fath in dbContext.Families.AsNoTracking() on orp.Family.FatherId equals fath.FatherId
                                     where orp.Family.FatherId == Fid
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
                    var orphanDC = Mapper.Map<OrphanageDataModel.Persons.Orphan, OrphanDC>(orpToFill);
                    _uriGenerator.SetOrphanUris(ref orphanDC);
                    returnedOrphans.Add(orphanDC);
                }
            }
            return returnedOrphans;
        }
    }
}
