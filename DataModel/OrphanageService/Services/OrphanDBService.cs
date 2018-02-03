using OrphanageDataModel.Persons;
using OrphanageService.DataContext;
using OrphanageService.Services.Interfaces;
using OrphanageService.Utilities.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace OrphanageService.Services
{
    public class OrphanDbService : IOrphanDbService
    {
        private readonly ISelfLoopBlocking _loopBlocking;
        private readonly IUriGenerator _uriGenerator;

        public OrphanDbService(ISelfLoopBlocking loopBlocking, IUriGenerator uriGenerator)
        {
            _loopBlocking = loopBlocking;
            _uriGenerator = uriGenerator;
        }

        public async Task<OrphanageDataModel.Persons.Orphan> GetOrphan(int id)
        {
            using (var _orphanageDBC = new OrphanageDbCNoBinary())
            {
                var orphan = await _orphanageDBC.Orphans.AsNoTracking()
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
                    .FirstOrDefaultAsync(o => o.Id == id);

                _loopBlocking.BlockOrphanSelfLoop(ref orphan);
                _uriGenerator.SetOrphanUris(ref orphan);
                return orphan;
            }
        }

        public async Task<IEnumerable<OrphanageDataModel.Persons.Orphan>> GetOrphans(int pageSize, int pageNum)
        {
            IList<OrphanageDataModel.Persons.Orphan> orphansList = new List<OrphanageDataModel.Persons.Orphan>();
            using (var _orphanageDBC = new OrphanageDbCNoBinary())
            {
                int totalSkiped = pageSize * pageNum;
                int orphansCount = await _orphanageDBC.Orphans.AsNoTracking().CountAsync();
                if (orphansCount < totalSkiped)
                {
                    totalSkiped = orphansCount - pageSize;
                }
                if (totalSkiped < 0) totalSkiped = 0;
                var orphans = await _orphanageDBC.Orphans.AsNoTracking()
                    .OrderBy(o => o.Id).Skip(() => totalSkiped).Take(() => pageSize)
                    .Include(o => o.Name)
                    .Include(o => o.Caregiver.Name)
                    .Include(o => o.Caregiver.Address)
                    .Include(o => o.Family.Father.Name)
                    .Include(o => o.Family.Mother.Name)
                    .Include(o => o.Family.PrimaryAddress)
                    .Include(o => o.Family.AlternativeAddress)
                    .Include(o => o.Guarantor.Name)
                    .ToListAsync();

                foreach (var orphan in orphans)
                {
                    var orphanTofill = orphan;
                    _loopBlocking.BlockOrphanSelfLoop(ref orphanTofill);
                    _uriGenerator.SetOrphanUris(ref orphanTofill);
                    orphansList.Add(orphanTofill);
                }
            }
            return orphansList;
        }

        public async Task<int> GetOrphansCount()
        {
            using (var _orphanageDBC = new OrphanageDbCNoBinary())
            {
                int orphansCount = await _orphanageDBC.Orphans.AsNoTracking().CountAsync();
                return orphansCount;
            }
        }

        public async Task<byte[]> GetOrphanFaceImage(int Oid)
        {
            using (var _orphanageDBC = new OrphanageDBC())
            {
                var img = await _orphanageDBC.Orphans.AsNoTracking().Where(o => o.Id == Oid).Select(o => new { o.FacePhotoData }).FirstOrDefaultAsync();
                return img?.FacePhotoData;
            }
        }

        public async Task<byte[]> GetOrphanBirthCertificate(int Oid)
        {
            using (var _orphanageDBC = new OrphanageDBC())
            {
                var img = await _orphanageDBC.Orphans.AsNoTracking().Where(o => o.Id == Oid).Select(o => new { o.BirthCertificatePhotoData }).FirstOrDefaultAsync();
                return img?.BirthCertificatePhotoData;
            }
        }

        public async Task<byte[]> GetOrphanFamilyCardPagePhoto(int Oid)
        {
            using (var _orphanageDBC = new OrphanageDBC())
            {
                var img = await _orphanageDBC.Orphans.AsNoTracking().Where(o => o.Id == Oid).Select(o => new { o.FamilyCardPagePhotoData }).FirstOrDefaultAsync();
                return img?.FamilyCardPagePhotoData;
            }
        }

        public async Task<byte[]> GetOrphanFullPhoto(int Oid)
        {
            using (var _orphanageDBC = new OrphanageDBC())
            {
                var img = await _orphanageDBC.Orphans.AsNoTracking().Where(o => o.Id == Oid).Select(o => new { o.FullPhotoData }).FirstOrDefaultAsync();
                return img?.FullPhotoData;
            }
        }

        public async Task<byte[]> GetOrphanCertificate(int Oid)
        {
            using (var _orphanageDBC = new OrphanageDBC())
            {
                var img = await _orphanageDBC.Orphans.AsNoTracking().Where(o => o.Id == Oid && o.EducationId.HasValue).Select(o => new { o.Education.CertificatePhotoFront }).FirstOrDefaultAsync();
                return img?.CertificatePhotoFront;
            }
        }

        public async Task<byte[]> GetOrphanCertificate2(int Oid)
        {
            using (var _orphanageDBC = new OrphanageDBC())
            {
                var img = await _orphanageDBC.Orphans.AsNoTracking().Where(o => o.Id == Oid && o.EducationId.HasValue).Select(o => new { o.Education.CertificatePhotoBack }).FirstOrDefaultAsync();
                return img?.CertificatePhotoBack;
            }
        }

        public async Task<byte[]> GetOrphanHealthReporte(int Oid)
        {
            using (var _orphanageDBC = new OrphanageDBC())
            {
                var img = await _orphanageDBC.Orphans.AsNoTracking().Where(o => o.Id == Oid && o.HealthId.HasValue).Select(o => new { o.HealthStatus.ReporteFileData }).FirstOrDefaultAsync();
                return img?.ReporteFileData;
            }
        }

        public async Task<int> GetBrothersCount(int Oid)
        {
            using (var _orphanageDBC = new OrphanageDbCNoBinary())
            {
                IList<OrphanageDataModel.Persons.Orphan> brothers = new List<OrphanageDataModel.Persons.Orphan>();
                var orphan = await _orphanageDBC.Orphans.AsNoTracking()
                    .Include(o => o.Family)
                    .FirstOrDefaultAsync(o => o.Id == Oid);

                var brotherFM = await _orphanageDBC.Orphans.AsNoTracking()
                    .Include(o => o.Family)
                    .Where(o => o.Family.FatherId == orphan.Family.FatherId || o.Family.MotherId == orphan.Family.MotherId)
                    .ToListAsync();

                foreach (var bro in brotherFM)
                {
                    if (!brothers.Contains(bro) && bro.Id != orphan.Id)
                    {
                        brothers.Add(bro);
                    }
                }
                return brothers.Count;
            }
        }

        public async Task<IEnumerable<OrphanageDataModel.Persons.Orphan>> GetBrothers(int Oid)
        {
            using (var _orphanageDBC = new OrphanageDbCNoBinary())
            {
                IList<OrphanageDataModel.Persons.Orphan> brothers = new List<OrphanageDataModel.Persons.Orphan>();              
                var orphan = await _orphanageDBC.Orphans.AsNoTracking()                    
                    .Include(o => o.Family)
                    .FirstOrDefaultAsync(o => o.Id == Oid);

                var brotherFM = await _orphanageDBC.Orphans.AsNoTracking()
                    .Include(o => o.Name)
                    .Include(o => o.Caregiver.Name)
                    .Include(o => o.Caregiver.Address)
                    .Include(o => o.Family.Father.Name)
                    .Include(o => o.Family.Mother.Name)
                    .Include(o => o.Family.PrimaryAddress)
                    .Include(o => o.Family.AlternativeAddress)
                    .Include(o => o.Guarantor.Name)
                    .Where(o => o.Family.FatherId == orphan.Family.FatherId || o.Family.MotherId == orphan.Family.MotherId)
                    .ToListAsync();

                foreach (var bro in brotherFM)
                {
                    if (!brothers.Contains(bro) && bro.Id != orphan.Id)
                    {
                        var broToEdit = bro;
                        _loopBlocking.BlockOrphanSelfLoop(ref broToEdit);
                        _uriGenerator.SetOrphanUris(ref broToEdit);
                        brothers.Add(broToEdit);
                    }
                }
                return brothers;
            }
        }

        public async Task<bool> SetOrphanFaceImage(int Oid, byte[] data)
        {
            using (var _orphanageDBC = new OrphanageDBC())
            {
                _orphanageDBC.Configuration.AutoDetectChangesEnabled = true;
                _orphanageDBC.Configuration.LazyLoadingEnabled = true;
                _orphanageDBC.Configuration.ProxyCreationEnabled = true;

                var orphan = await _orphanageDBC.Orphans.
                    Where(o => o.Id == Oid).FirstOrDefaultAsync();

                if (orphan == null)
                    return false;

                orphan.FacePhotoData = data;
                var ret = await _orphanageDBC.SaveChangesAsync();
                if (ret > 0)
                { return true;
                }
                else { return false; }
            }
        }

        public async Task<bool> SetOrphanBirthCertificate(int Oid, byte[] data)
        {
            using (var _orphanageDBC = new OrphanageDBC())
            {
                _orphanageDBC.Configuration.AutoDetectChangesEnabled = true;
                _orphanageDBC.Configuration.LazyLoadingEnabled = true;
                _orphanageDBC.Configuration.ProxyCreationEnabled = true;

                var orphan = await _orphanageDBC.Orphans.
                    Where(o => o.Id == Oid).FirstOrDefaultAsync();

                if (orphan == null)
                    return false;

                orphan.BirthCertificatePhotoData = data;
                var ret = await _orphanageDBC.SaveChangesAsync();
                if (ret > 0)
                {
                    return true;
                }
                else { return false; }

            }
        }

        public async Task<bool> SetOrphanFamilyCardPagePhoto(int Oid, byte[] data)
        {
            using (var _orphanageDBC = new OrphanageDBC())
            {
                _orphanageDBC.Configuration.AutoDetectChangesEnabled = true;
                _orphanageDBC.Configuration.LazyLoadingEnabled = true;
                _orphanageDBC.Configuration.ProxyCreationEnabled = true;

                var orphan = await _orphanageDBC.Orphans.
                    Where(o => o.Id == Oid).FirstOrDefaultAsync();

                if (orphan == null)
                    return false;

                orphan.FamilyCardPagePhotoData = data;
                var ret = await _orphanageDBC.SaveChangesAsync();
                if (ret > 0)
                {
                    return true;
                }
                else { return false; }

            }
        }

        public async Task<bool> SetOrphanFullPhoto(int Oid, byte[] data)
        {
            using (var _orphanageDBC = new OrphanageDBC())
            {
                _orphanageDBC.Configuration.AutoDetectChangesEnabled = true;
                _orphanageDBC.Configuration.LazyLoadingEnabled = true;
                _orphanageDBC.Configuration.ProxyCreationEnabled = true;

                var orphan = await _orphanageDBC.Orphans.
                    Where(o => o.Id == Oid).FirstOrDefaultAsync();

                if (orphan == null)
                    return false;

                orphan.FullPhotoData = data;
                var ret = await _orphanageDBC.SaveChangesAsync();
                if (ret > 0)
                {
                    return true;
                }
                else { return false; }

            }
        }

        public async Task<bool> SetOrphanCertificate(int Oid, byte[] data)
        {
            using (var _orphanageDBC = new OrphanageDBC())
            {
                _orphanageDBC.Configuration.AutoDetectChangesEnabled = true;
                _orphanageDBC.Configuration.LazyLoadingEnabled = true;
                _orphanageDBC.Configuration.ProxyCreationEnabled = true;

                var orphan = await _orphanageDBC.Orphans.
                    Include(o=>o.Education)
                    .Where(o => o.Id == Oid).FirstOrDefaultAsync();

                if (orphan == null || orphan.Education == null)
                    return false;

                orphan.Education.CertificatePhotoFront = data;
                var ret = await _orphanageDBC.SaveChangesAsync();
                if (ret > 0)
                {
                    return true;
                }
                else { return false; }

            }
        }

        public async Task<bool> SetOrphanCertificate2(int Oid, byte[] data)
        {
            using (var _orphanageDBC = new OrphanageDBC())
            {
                _orphanageDBC.Configuration.AutoDetectChangesEnabled = true;
                _orphanageDBC.Configuration.LazyLoadingEnabled = true;
                _orphanageDBC.Configuration.ProxyCreationEnabled = true;

                var orphan = await _orphanageDBC.Orphans.
                    Include(o => o.Education)
                    .Where(o => o.Id == Oid).FirstOrDefaultAsync();

                if (orphan == null || orphan.Education == null)
                    return false;

                orphan.Education.CertificatePhotoBack = data;
                var ret = await _orphanageDBC.SaveChangesAsync();
                if (ret > 0)
                {
                    return true;
                }
                else { return false; }
            }
        }

        public async Task<bool> SetOrphanHealthReporte(int Oid, byte[] data)
        {
            using (var _orphanageDBC = new OrphanageDBC())
            {
                _orphanageDBC.Configuration.AutoDetectChangesEnabled = true;
                _orphanageDBC.Configuration.LazyLoadingEnabled = true;
                _orphanageDBC.Configuration.ProxyCreationEnabled = true;

                var orphan = await _orphanageDBC.Orphans.
                    Include(o => o.HealthStatus)
                    .Where(o => o.Id == Oid).FirstOrDefaultAsync();

                if (orphan == null || orphan.HealthStatus == null)
                    return false;

                orphan.HealthStatus.ReporteFileData = data;
                var ret = await _orphanageDBC.SaveChangesAsync();
                if (ret > 0)
                {
                    return true;
                }
                else { return false; }
            }
        }
    }
}