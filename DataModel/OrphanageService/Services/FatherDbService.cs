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
    public class FatherDbService : IFatherDbService
    {
        private readonly ISelfLoopBlocking _selfLoopBlocking;
        private readonly IUriGenerator _uriGenerator;

        public FatherDbService(ISelfLoopBlocking selfLoopBlocking, IUriGenerator uriGenerator)
        {
            _selfLoopBlocking = selfLoopBlocking;
            _uriGenerator = uriGenerator;
        }

        public async Task<OrphanageDataModel.Persons.Father> GetFather(int Fid)
        {
            using (var dbContext = new OrphanageDbCNoBinary())
            {
                var father = await dbContext.Fathers.AsNoTracking()
                    .Include(f => f.Families)
                    .Include(f => f.Name)
                    .FirstOrDefaultAsync(f => f.Id == Fid);

                _selfLoopBlocking.BlockFatherSelfLoop(ref father);
                setFatherEntities(ref father, dbContext);
                _uriGenerator.SetFatherUris(ref father);
                return father;
            }
        }

        public async Task<byte[]> GetFatherDeathCertificate(int Fid)
        {
            using (var _orphanageDBC = new OrphanageDBC())
            {
                var img = await _orphanageDBC.Fathers.AsNoTracking().Where(f => f.Id == Fid).Select(f => new { f.DeathCertificatePhotoData }).FirstOrDefaultAsync();
                return img?.DeathCertificatePhotoData;
            }
        }

        public async Task<byte[]> GetFatherPhoto(int Fid)
        {
            using (var _orphanageDBC = new OrphanageDBC())
            {
                var img = await _orphanageDBC.Fathers.AsNoTracking().Where(f => f.Id == Fid).Select(f => new { f.PhotoData }).FirstOrDefaultAsync();
                return img?.PhotoData;
            }
        }

        public async Task<IEnumerable<OrphanageDataModel.Persons.Father>> GetFathers(int pageSize, int pageNum)
        {
            IList<OrphanageDataModel.Persons.Father> fathersList = new List<OrphanageDataModel.Persons.Father>();
            using (var _orphanageDBC = new OrphanageDbCNoBinary())
            {
                int totalSkiped = pageSize * pageNum;
                int FathersCount = await _orphanageDBC.Fathers.AsNoTracking().CountAsync();
                if (FathersCount < totalSkiped)
                {
                    totalSkiped = FathersCount - pageSize;
                }
                if (totalSkiped < 0) totalSkiped = 0;
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
                    _uriGenerator.SetFatherUris(ref fatherToFill);
                    fathersList.Add(fatherToFill);
                }
            }
            return fathersList;
        }

        public async Task<int> GetFathersCount()
        {
            using (var _orphanageDBC = new OrphanageDbCNoBinary())
            {
                int orphansCount = await _orphanageDBC.Fathers.AsNoTracking().CountAsync();
                return orphansCount;
            }
        }

        public static void setFatherEntities(ref OrphanageDataModel.Persons.Father father, DbContext dbContext)
        {
            OrphanageDbCNoBinary dbCNoBinary = (OrphanageDbCNoBinary)dbContext;
            foreach (var fam in father.Families)
            {
                var moth = dbCNoBinary.Mothers.
                    Include(m => m.Name).
                    Include(m => m.Address).
                    FirstOrDefault(m => m.Id == fam.MotherId);
                fam.Mother = moth;
            }
        }

        public async Task<IEnumerable<OrphanageDataModel.Persons.Orphan>> GetOrphans(int Fid)
        {
            IList<OrphanageDataModel.Persons.Orphan> returnedOrphans = new List<OrphanageDataModel.Persons.Orphan>();
            using (var dbContext = new OrphanageDbCNoBinary())
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
                    _uriGenerator.SetOrphanUris(ref orpToFill);
                    returnedOrphans.Add(orpToFill);
                }
            }
            return returnedOrphans;
        }

        ///<inheritdoc/>
        public async Task<int> AddFather(OrphanageDataModel.Persons.Father father, OrphanageDbCNoBinary orphanageDBC)
        {
            if (father == null) throw new NullReferenceException();
            //TODO #32 check the father data (name)
            //TODO use forceadd in the settings
            orphanageDBC.Fathers.Add(father);

            if (await orphanageDBC.SaveChangesAsync() == 1)
                return father.Id;
            else
                return -1;
        }

        public async Task<bool> SaveFather(OrphanageDataModel.Persons.Father father)
        {
            if (father == null) throw new NullReferenceException();
            using (OrphanageDBC orphanageDc = new OrphanageDBC())
            {
                orphanageDc.Configuration.AutoDetectChangesEnabled = true;
                var fatherToReplace = await orphanageDc.Fathers.Where(f => f.Id == father.Id).FirstAsync();
                if (fatherToReplace == null) throw new ObjectNotFoundException();
                fatherToReplace = father;
                await orphanageDc.SaveChangesAsync();
            }
            return true;
        }

        public async Task<bool> DeleteFather(int Fid,OrphanageDbCNoBinary orphanageDb)
        {
            if (Fid == 0) throw new NullReferenceException();
            var father = await orphanageDb.Fathers.Where(f => f.Id == Fid).FirstOrDefaultAsync();
            if(father.Families.Count > 0)
            {
                //the father has another family
                return true;
            }
            var fatherName = father.Name;
            orphanageDb.Fathers.Remove(father);
            orphanageDb.Names.Remove(fatherName);
            if (await orphanageDb.SaveChangesAsync() > 1)
                return true;
            else
                return false;
        }

        public async Task<bool> IsExist(OrphanageDataModel.Persons.Father father)
        {
            if (father == null) throw new NullReferenceException();
            using (var orphangeDC = new OrphanageDbCNoBinary())
            {
                return await orphangeDC.Fathers.Where(f => f.Id == father.Id).AnyAsync();
            }
        }
    }
}