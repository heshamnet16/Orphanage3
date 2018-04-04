using OrphanageDataModel.RegularData;
using OrphanageService.DataContext;
using OrphanageService.Services.Exceptions;
using OrphanageService.Services.Interfaces;
using OrphanageService.Utilities.Interfaces;
using OrphanageV3.Extensions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace OrphanageService.Services
{
    public class OrphanDbService : IOrphanDbService
    {
        private readonly ISelfLoopBlocking _loopBlocking;
        private readonly IUriGenerator _uriGenerator;
        private readonly IRegularDataService _regularDataService;
        private readonly ILogger _logger;

        public OrphanDbService(ISelfLoopBlocking loopBlocking, IUriGenerator uriGenerator, IRegularDataService regularDataService, ILogger logger)
        {
            _loopBlocking = loopBlocking;
            _uriGenerator = uriGenerator;
            _regularDataService = regularDataService;
            _logger = logger;
        }

        public async Task<OrphanageDataModel.Persons.Orphan> GetOrphan(int id)
        {
            _logger.Information($"trying to get orphan with id ({id})");
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

                if (orphan == null)
                {
                    _logger.Error($"orphan with id ({id}) has not been found, ObjectNotFoundException will be thrown");
                    throw new ObjectNotFoundException();
                }

                _loopBlocking.BlockOrphanSelfLoop(ref orphan);
                _uriGenerator.SetOrphanUris(ref orphan);
                _logger.Information($"orphan object with id({id}) will be returned");
                return orphan;
            }
        }

        public async Task<IEnumerable<OrphanageDataModel.Persons.Orphan>> GetExcludedOrphans()
        {
            _logger.Information($"trying to get all excluded orphans");
            IList<OrphanageDataModel.Persons.Orphan> orphansList = new List<OrphanageDataModel.Persons.Orphan>();
            using (var _orphanageDBC = new OrphanageDbCNoBinary())
            {
                var orphans = await _orphanageDBC.Orphans.AsNoTracking()
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
                    .Where(o => o.IsExcluded.HasValue && o.IsExcluded == true)
                    .ToListAsync();

                return prepareOrphansList(orphans);
            }
        }

        private IEnumerable<OrphanageDataModel.Persons.Orphan> prepareOrphansList(IList<OrphanageDataModel.Persons.Orphan> orphansList)
        {
            _logger.Information($"trying to prepare orphans list to return it");
            if (orphansList == null || orphansList.Count == 0)
            {
                _logger.Information($"there is no orphan list to return, null will be returned");
                return null;
            }
            IList<OrphanageDataModel.Persons.Orphan> returnedOrphansList = new List<OrphanageDataModel.Persons.Orphan>();
            foreach (var orphan in orphansList)
            {
                var orphanTofill = orphan;
                _loopBlocking.BlockOrphanSelfLoop(ref orphanTofill);
                _uriGenerator.SetOrphanUris(ref orphanTofill);
                returnedOrphansList.Add(orphanTofill);
            }
            _logger.Information($"{returnedOrphansList.Count} records of orphans are ready and they will be returned");
            return returnedOrphansList;
        }

        public async Task<IEnumerable<OrphanageDataModel.Persons.Orphan>> GetOrphans(int pageSize, int pageNum)
        {
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

                return prepareOrphansList(orphans);
            }
        }

        public async Task<int> GetOrphansCount()
        {
            _logger.Information($"trying to get all orphans count");
            using (var _orphanageDBC = new OrphanageDbCNoBinary())
            {
                int orphansCount = await _orphanageDBC.Orphans.AsNoTracking().CountAsync();
                _logger.Information($"orphans count({orphansCount}) will be returned");
                return orphansCount;
            }
        }

        public async Task<byte[]> GetOrphanFaceImage(int Oid)
        {
            _logger.Information($"trying to get orphan with id ({Oid}) face image");
            using (var _orphanageDBC = new OrphanageDBC())
            {
                var img = await _orphanageDBC.Orphans.AsNoTracking().Where(o => o.Id == Oid).Select(o => new { o.FacePhotoData }).FirstOrDefaultAsync();
                if (img?.FacePhotoData == null)
                {
                    _logger.Information($"orphan with id({Oid}) face image is null");
                    return null;
                }
                else
                {
                    _logger.Information($"return orphan with id({Oid}) face image, count of bytes are {img?.FacePhotoData.Length}");
                    return img?.FacePhotoData;
                }
            }
        }

        public async Task<byte[]> GetOrphanBirthCertificate(int Oid)
        {
            _logger.Information($"trying to get orphan with id ({Oid}) Birth Certificate");
            using (var _orphanageDBC = new OrphanageDBC())
            {
                var img = await _orphanageDBC.Orphans.AsNoTracking().Where(o => o.Id == Oid).Select(o => new { o.BirthCertificatePhotoData }).FirstOrDefaultAsync();
                if (img?.BirthCertificatePhotoData == null)
                {
                    _logger.Information($"orphan with id({Oid}) Birth Certificate is null");
                    return null;
                }
                else
                {
                    _logger.Information($"return orphan with id({Oid}) Birth Certificate, count of bytes are {img?.BirthCertificatePhotoData.Length}");
                    return img?.BirthCertificatePhotoData;
                }
            }
        }

        public async Task<byte[]> GetOrphanFamilyCardPagePhoto(int Oid)
        {
            _logger.Information($"trying to get orphan with id ({Oid}) FamilyCard Page Photo");
            using (var _orphanageDBC = new OrphanageDBC())
            {
                var img = await _orphanageDBC.Orphans.AsNoTracking().Where(o => o.Id == Oid).Select(o => new { o.FamilyCardPagePhotoData }).FirstOrDefaultAsync();
                if (img?.FamilyCardPagePhotoData == null)
                {
                    _logger.Information($"orphan with id({Oid}) FamilyCard Page Photo is null");
                    return null;
                }
                else
                {
                    _logger.Information($"return orphan with id({Oid}) FamilyCard Page Photo, count of bytes are {img?.FamilyCardPagePhotoData.Length}");
                    return img?.FamilyCardPagePhotoData;
                }
            }
        }

        public async Task<byte[]> GetOrphanFullPhoto(int Oid)
        {
            _logger.Information($"trying to get orphan with id ({Oid}) Full Photo");
            using (var _orphanageDBC = new OrphanageDBC())
            {
                var img = await _orphanageDBC.Orphans.AsNoTracking().Where(o => o.Id == Oid).Select(o => new { o.FullPhotoData }).FirstOrDefaultAsync();
                if (img?.FullPhotoData == null)
                {
                    _logger.Information($"orphan with id({Oid}) Full Photo is null");
                    return null;
                }
                else
                {
                    _logger.Information($"return orphan with id({Oid}) Full Photo, count of bytes are {img?.FullPhotoData.Length}");
                    return img?.FullPhotoData;
                }
            }
        }

        public async Task<byte[]> GetOrphanCertificate(int Oid)
        {
            _logger.Information($"trying to get orphan with id ({Oid}) education certificate first photo");
            using (var _orphanageDBC = new OrphanageDBC())
            {
                var img = await _orphanageDBC.Orphans.AsNoTracking().Where(o => o.Id == Oid && o.EducationId.HasValue).Select(o => new { o.Education.CertificatePhotoFront }).FirstOrDefaultAsync();
                if (img?.CertificatePhotoFront == null)
                {
                    _logger.Information($"orphan with id({Oid}) education certificate first photo is null");
                    return null;
                }
                else
                {
                    _logger.Information($"return orphan with id({Oid}) education certificate first photo, count of bytes are {img?.CertificatePhotoFront.Length}");
                    return img?.CertificatePhotoFront;
                }
            }
        }

        public async Task<byte[]> GetOrphanCertificate2(int Oid)
        {
            _logger.Information($"trying to get orphan with id ({Oid}) education certificate second photo");
            using (var _orphanageDBC = new OrphanageDBC())
            {
                var img = await _orphanageDBC.Orphans.AsNoTracking().Where(o => o.Id == Oid && o.EducationId.HasValue).Select(o => new { o.Education.CertificatePhotoBack }).FirstOrDefaultAsync();
                if (img?.CertificatePhotoBack == null)
                {
                    _logger.Information($"orphan with id({Oid}) education certificate second photo is null");
                    return null;
                }
                else
                {
                    _logger.Information($"return orphan with id({Oid}) education certificate second photo, count of bytes are {img?.CertificatePhotoBack.Length}");
                    return img?.CertificatePhotoBack;
                }
            }
        }

        public async Task<byte[]> GetOrphanHealthReporte(int Oid)
        {
            _logger.Information($"trying to get orphan with id ({Oid}) health report photo");
            using (var _orphanageDBC = new OrphanageDBC())
            {
                var img = await _orphanageDBC.Orphans.AsNoTracking().Where(o => o.Id == Oid && o.HealthId.HasValue).Select(o => new { o.HealthStatus.ReporteFileData }).FirstOrDefaultAsync();
                if (img?.ReporteFileData == null)
                {
                    _logger.Information($"orphan with id({Oid}) health report photo is null");
                    return null;
                }
                else
                {
                    _logger.Information($"return orphan with id({Oid}) health report photo, count of bytes are {img?.ReporteFileData.Length}");
                    return img?.ReporteFileData;
                }
            }
        }

        public async Task<int> GetBrothersCount(int Oid)
        {
            _logger.Information($"trying to get the count of brothers to the orphan with id ({Oid})");
            using (var _orphanageDBC = new OrphanageDbCNoBinary())
            {
                IList<OrphanageDataModel.Persons.Orphan> brothers = new List<OrphanageDataModel.Persons.Orphan>();
                var orphan = await _orphanageDBC.Orphans.AsNoTracking()
                    .Include(o => o.Family)
                    .FirstOrDefaultAsync(o => o.Id == Oid);

                if (orphan == null)
                {
                    _logger.Error($"orphan with id ({Oid}) has not been found, ObjectNotFoundException will be thrown");
                    throw new ObjectNotFoundException();
                }

                var brotherFM = await _orphanageDBC.Orphans.AsNoTracking()
                    .Include(o => o.Family)
                    .Where(o => o.Family.FatherId == orphan.Family.FatherId || o.Family.MotherId == orphan.Family.MotherId)
                    .ToListAsync();

                if (brotherFM.Count == 1)
                {
                    _logger.Error($"orphan with id({Oid}) has no brothers 0 will be returned");
                    return 0;
                }
                foreach (var bro in brotherFM)
                {
                    if (!brothers.Contains(bro) && bro.Id != orphan.Id)
                    {
                        brothers.Add(bro);
                    }
                }
                _logger.Information($"orphan with id({Oid}) has {brothers.Count} brothers");
                return brothers.Count;
            }
        }

        public async Task<IEnumerable<OrphanageDataModel.Persons.Orphan>> GetBrothers(int Oid)
        {
            _logger.Information($"trying to get the brothers orphan objects to the orphan with id ({Oid})");
            using (var _orphanageDBC = new OrphanageDbCNoBinary())
            {
                IList<OrphanageDataModel.Persons.Orphan> brothers = new List<OrphanageDataModel.Persons.Orphan>();
                var orphan = await _orphanageDBC.Orphans.AsNoTracking()
                    .Include(o => o.Family)
                    .FirstOrDefaultAsync(o => o.Id == Oid);

                if (orphan == null)
                {
                    _logger.Error($"orphan with id ({Oid}) has not been found, ObjectNotFoundException will be thrown");
                    throw new ObjectNotFoundException();
                }

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

                if (brotherFM.Count == 1)
                {
                    _logger.Error($"orphan with id({Oid}) has no brothers null will be returned");
                    return null;
                }

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
                _logger.Information($"orphan with id({Oid}) has {brothers.Count} orphans objects as brothers");
                return brothers;
            }
        }

        public async Task<bool> SetOrphanFaceImage(int Oid, byte[] data)
        {
            _logger.Information($"trying to set orphan with id ({Oid}) face image");
            using (var _orphanageDBC = new OrphanageDBC())
            {
                _orphanageDBC.Configuration.AutoDetectChangesEnabled = true;
                _orphanageDBC.Configuration.LazyLoadingEnabled = true;
                _orphanageDBC.Configuration.ProxyCreationEnabled = true;

                var orphan = await _orphanageDBC.Orphans.
                    Where(o => o.Id == Oid).FirstOrDefaultAsync();

                if (orphan == null)
                {
                    _logger.Error($"orphan with id ({Oid}) has not been found, ObjectNotFoundException will be thrown");
                    throw new ObjectNotFoundException();
                }

                orphan.FacePhotoData = data;
                var ret = await _orphanageDBC.SaveChangesAsync();
                if (ret > 0)
                {
                    _logger.Information($"new face image has been set successfully to the orphan with id ({Oid}), true will be returned");
                    return true;
                }
                else
                {
                    _logger.Warning($"something went wrong , cannot set new face image to the orphan with id ({Oid}), false will be returned");
                    return false;
                }
            }
        }

        public async Task<bool> SetOrphanBirthCertificate(int Oid, byte[] data)
        {
            _logger.Information($"trying to set orphan with id ({Oid}) Birth Certificate image");
            using (var _orphanageDBC = new OrphanageDBC())
            {
                _orphanageDBC.Configuration.AutoDetectChangesEnabled = true;
                _orphanageDBC.Configuration.LazyLoadingEnabled = true;
                _orphanageDBC.Configuration.ProxyCreationEnabled = true;

                var orphan = await _orphanageDBC.Orphans.
                    Where(o => o.Id == Oid).FirstOrDefaultAsync();

                if (orphan == null)
                {
                    _logger.Error($"orphan with id ({Oid}) has not been found, ObjectNotFoundException will be thrown");
                    throw new ObjectNotFoundException();
                }

                orphan.BirthCertificatePhotoData = data;
                var ret = await _orphanageDBC.SaveChangesAsync();
                if (ret > 0)
                {
                    _logger.Information($"new Birth Certificate image has been set successfully to the orphan with id ({Oid}), true will be returned");
                    return true;
                }
                else
                {
                    _logger.Warning($"something went wrong , cannot set new Birth Certificate image to the orphan with id ({Oid}), false will be returned");
                    return false;
                }
            }
        }

        public async Task<bool> SetOrphanFamilyCardPagePhoto(int Oid, byte[] data)
        {
            _logger.Information($"trying to set orphan with id ({Oid}) FamilyCard Page Photo");
            using (var _orphanageDBC = new OrphanageDBC())
            {
                _orphanageDBC.Configuration.AutoDetectChangesEnabled = true;
                _orphanageDBC.Configuration.LazyLoadingEnabled = true;
                _orphanageDBC.Configuration.ProxyCreationEnabled = true;

                var orphan = await _orphanageDBC.Orphans.
                    Where(o => o.Id == Oid).FirstOrDefaultAsync();

                if (orphan == null)
                {
                    _logger.Error($"orphan with id ({Oid}) has not been found, ObjectNotFoundException will be thrown");
                    throw new ObjectNotFoundException();
                }

                orphan.FamilyCardPagePhotoData = data;
                var ret = await _orphanageDBC.SaveChangesAsync();
                if (ret > 0)
                {
                    _logger.Information($"new FamilyCard Page Photo has been set successfully to the orphan with id ({Oid}), true will be returned");
                    return true;
                }
                else
                {
                    _logger.Warning($"something went wrong , cannot set new FamilyCard Page Photo to the orphan with id ({Oid}), false will be returned");
                    return false;
                }
            }
        }

        public async Task<bool> SetOrphanFullPhoto(int Oid, byte[] data)
        {
            _logger.Information($"trying to set orphan with id ({Oid}) Full Photo");
            using (var _orphanageDBC = new OrphanageDBC())
            {
                _orphanageDBC.Configuration.AutoDetectChangesEnabled = true;
                _orphanageDBC.Configuration.LazyLoadingEnabled = true;
                _orphanageDBC.Configuration.ProxyCreationEnabled = true;

                var orphan = await _orphanageDBC.Orphans.
                    Where(o => o.Id == Oid).FirstOrDefaultAsync();

                if (orphan == null)
                {
                    _logger.Error($"orphan with id ({Oid}) has not been found, ObjectNotFoundException will be thrown");
                    throw new ObjectNotFoundException();
                }

                orphan.FullPhotoData = data;
                var ret = await _orphanageDBC.SaveChangesAsync();
                if (ret > 0)
                {
                    _logger.Information($"new  Full Photo has been set successfully to the orphan with id ({Oid}), true will be returned");
                    return true;
                }
                else
                {
                    _logger.Warning($"something went wrong , cannot set new  Full Photo to the orphan with id ({Oid}), false will be returned");
                    return false;
                }
            }
        }

        public async Task<bool> SetOrphanCertificate(int Oid, byte[] data)
        {
            _logger.Information($"trying to set orphan with id ({Oid}) education certificate first photo");
            using (var _orphanageDBC = new OrphanageDBC())
            {
                _orphanageDBC.Configuration.AutoDetectChangesEnabled = true;
                _orphanageDBC.Configuration.LazyLoadingEnabled = true;
                _orphanageDBC.Configuration.ProxyCreationEnabled = true;

                var orphan = await _orphanageDBC.Orphans.
                    Include(o => o.Education)
                    .Where(o => o.Id == Oid).FirstOrDefaultAsync();

                if (orphan == null)
                {
                    _logger.Error($"orphan with id ({Oid}) has not been found, ObjectNotFoundException will be thrown");
                    throw new ObjectNotFoundException();
                }
                if (orphan.Education == null)
                {
                    _logger.Error($"Education object of the orphan with id ({Oid}) is null, NullReferenceException will be thrown");
                    throw new NullReferenceException();
                }
                orphan.Education.CertificatePhotoFront = data;
                var ret = await _orphanageDBC.SaveChangesAsync();
                if (ret > 0)
                {
                    _logger.Information($"new  education certificate first photo has been set successfully to the orphan with id ({Oid}), true will be returned");
                    return true;
                }
                else
                {
                    _logger.Warning($"something went wrong , cannot set new  education certificate first photo to the orphan with id ({Oid}), false will be returned");
                    return false;
                }
            }
        }

        public async Task<bool> SetOrphanCertificate2(int Oid, byte[] data)
        {
            _logger.Information($"trying to set orphan with id ({Oid}) education certificate second photo");
            using (var _orphanageDBC = new OrphanageDBC())
            {
                _orphanageDBC.Configuration.AutoDetectChangesEnabled = true;
                _orphanageDBC.Configuration.LazyLoadingEnabled = true;
                _orphanageDBC.Configuration.ProxyCreationEnabled = true;

                var orphan = await _orphanageDBC.Orphans.
                    Include(o => o.Education)
                    .Where(o => o.Id == Oid).FirstOrDefaultAsync();

                if (orphan == null)
                {
                    _logger.Error($"orphan with id ({Oid}) has not been found, ObjectNotFoundException will be thrown");
                    throw new ObjectNotFoundException();
                }

                if (orphan.Education == null)
                {
                    _logger.Error($"Education object of the orphan with id ({Oid}) is null, NullReferenceException will be thrown");
                    throw new NullReferenceException();
                }
                orphan.Education.CertificatePhotoBack = data;
                var ret = await _orphanageDBC.SaveChangesAsync();
                if (ret > 0)
                {
                    _logger.Information($"new  education certificate second photo has been set successfully to the orphan with id ({Oid}), true will be returned");
                    return true;
                }
                else
                {
                    _logger.Warning($"something went wrong , cannot set new  education certificate second photo to the orphan with id ({Oid}), false will be returned");
                    return false;
                }
            }
        }

        public async Task<bool> SetOrphanHealthReporte(int Oid, byte[] data)
        {
            _logger.Information($"trying to set orphan with id ({Oid}) health report photo");
            using (var _orphanageDBC = new OrphanageDBC())
            {
                _orphanageDBC.Configuration.AutoDetectChangesEnabled = true;
                _orphanageDBC.Configuration.LazyLoadingEnabled = true;
                _orphanageDBC.Configuration.ProxyCreationEnabled = true;

                var orphan = await _orphanageDBC.Orphans.
                    Include(o => o.HealthStatus)
                    .Where(o => o.Id == Oid).FirstOrDefaultAsync();

                if (orphan == null)
                {
                    _logger.Error($"orphan with id ({Oid}) has not been found, ObjectNotFoundException will be thrown");
                    throw new ObjectNotFoundException();
                }

                if (orphan.HealthStatus == null)
                {
                    _logger.Error($"healthStatus object of the orphan with id ({Oid}) is null, NullReferenceException will be thrown");
                    throw new NullReferenceException();
                }
                orphan.HealthStatus.ReporteFileData = data;
                var ret = await _orphanageDBC.SaveChangesAsync();
                if (ret > 0)
                {
                    _logger.Information($"new  health report photo has been set successfully to the orphan with id ({Oid}), true will be returned");
                    return true;
                }
                else
                {
                    _logger.Warning($"something went wrong , cannot set new health report photo to the orphan with id ({Oid}), false will be returned");
                    return false;
                }
            }
        }

        ///<inheritdoc/>
        public async Task<OrphanageDataModel.Persons.Orphan> AddOrphan(OrphanageDataModel.Persons.Orphan orphan, OrphanageDbCNoBinary orphanageDBC)
        {
            _logger.Information($"trying to add new orphan");
            if (orphan.FamilyId <= 0)
            {
                _logger.Warning($"family id is equal or less than zero, null will be returned");
                return null;
            }
            if (orphan.CaregiverId <= 0)
            {
                _logger.Warning($"Caregiver Id is equal or less than zero, null will be returned");
                return null;
            }
            if (orphan.Name == null)
            {
                _logger.Warning($"Name object is null, null will be returned");
                return null;
            }

            if (!Properties.Settings.Default.ForceAdd)
            {
                _logger.Information($"ForceAdd option is not activated");
                if (Properties.Settings.Default.CheckName)
                {
                    _logger.Information($"CheckName option is activated, trying to get the equal names from database");
                    var ret = GetOrphansByName(orphan.Name, orphanageDBC).FirstOrDefault();
                    if (ret != null)
                    {
                        _logger.Error($"orphan with id({ret.Id}) has the same name, DuplicatedObjectException will be thrown");
                        throw new DuplicatedObjectException(orphan.GetType(), ret.GetType(), ret.Id);
                    }
                    else
                    {
                        _logger.Information($"didn't found any similar names to ({orphan.Name.FullName()}) in the database");
                    }
                }
            }
            orphan.NameId = await _regularDataService.AddName(orphan.Name, orphanageDBC);
            if (orphan.Education != null)
            {
                orphan.EducationId = await _regularDataService.AddStudy(orphan.Education, orphanageDBC);
            }
            if (orphan.HealthStatus != null)
            {
                orphan.HealthId = await _regularDataService.AddHealth(orphan.HealthStatus, orphanageDBC);
            }
            orphanageDBC.Orphans.Add(orphan);
            if (await orphanageDBC.SaveChangesAsync() > 0)
            {
                _uriGenerator.SetOrphanUris(ref orphan);
                _logger.Information($"orphan with id({orphan.Id}) has been successfully added to the database");
                return orphan;
            }
            else
            {
                _logger.Information($"nothing has changed, orphan has not added, null will be returned");
                return null;
            }
        }

        public async Task<bool> SaveOrphan(OrphanageDataModel.Persons.Orphan orphan)
        {
            _logger.Information($"trying to save orphan with id({orphan.Id})");
            if (orphan.FamilyId <= 0)
            {
                _logger.Warning($"family id is equal or less than zero, false will be returned");
                return false;
            }
            if (orphan.CaregiverId <= 0)
            {
                _logger.Warning($"Caregiver Id is equal or less than zero, false will be returned");
                return false;
            }
            if (orphan.Name == null)
            {
                _logger.Warning($"Name object is null, false will be returned");
                return false;
            }
            using (var orphanageDbc = new OrphanageDbCNoBinary())
            {
                int ret = 0;
                orphanageDbc.Configuration.LazyLoadingEnabled = true;
                orphanageDbc.Configuration.ProxyCreationEnabled = true;
                orphanageDbc.Configuration.AutoDetectChangesEnabled = true;

                var orginalOrphan = await orphanageDbc.Orphans
                    .Include(o => o.Education)
                    .Include(o => o.Name)
                    .Include(o => o.HealthStatus)
                    .FirstOrDefaultAsync(o => o.Id == orphan.Id);

                if (orginalOrphan == null)
                {
                    _logger.Error($"orphan with id ({orphan.Id}) has not been found, ObjectNotFoundException will be thrown");
                    throw new ObjectNotFoundException();
                }

                orginalOrphan.IdentityCardNumber = orphan.IdentityCardNumber;
                orginalOrphan.BailId = orphan.BailId;
                orginalOrphan.Birthday = orphan.Birthday;
                orginalOrphan.CivilRegisterNumber = orphan.CivilRegisterNumber;
                orginalOrphan.ColorMark = orphan.ColorMark;
                orginalOrphan.ConsanguinityToCaregiver = orphan.ConsanguinityToCaregiver;
                orginalOrphan.FamilyId = orphan.FamilyId;
                orginalOrphan.FootSize = orphan.FootSize;
                orginalOrphan.Gender = orphan.Gender;
                orginalOrphan.IsBailed = orphan.IsBailed;
                orginalOrphan.IsExcluded = orphan.IsExcluded;
                orginalOrphan.PlaceOfBirth = orphan.PlaceOfBirth;
                orginalOrphan.Story = orphan.Story;
                orginalOrphan.Tallness = orphan.Tallness;
                orginalOrphan.Weight = orphan.Weight;
                orginalOrphan.CaregiverId = orphan.CaregiverId;
                orginalOrphan.GuarantorId = orphan.GuarantorId;
                ret += await _regularDataService.SaveName(orphan.Name, orphanageDbc);
                if (orphan.Education != null)
                {
                    if (orginalOrphan.Education == null)
                    {
                        //adding education object
                        orginalOrphan.EducationId = await _regularDataService.AddStudy(orphan.Education, orphanageDbc);
                    }
                    else
                    {
                        ret += await _regularDataService.SaveStudy(orphan.Education, orphanageDbc);
                    }
                }
                else
                {
                    if (orginalOrphan.Education != null)
                    {
                        int eduId = orginalOrphan.EducationId.Value;
                        orginalOrphan.EducationId = null;
                        await orphanageDbc.SaveChangesAsync();
                        await _regularDataService.DeleteStudy(eduId, orphanageDbc);
                    }
                }
                if (orphan.HealthStatus != null)
                {
                    if (orginalOrphan.HealthStatus == null)
                    {
                        //adding Health object
                        orginalOrphan.HealthId = await _regularDataService.AddHealth(orphan.HealthStatus, orphanageDbc);
                    }
                    else
                    {
                        ret += await _regularDataService.SaveHealth(orphan.HealthStatus, orphanageDbc);
                    }
                }
                else
                {
                    if (orginalOrphan.HealthStatus != null)
                    {
                        int healthId = orginalOrphan.HealthId.Value;
                        orginalOrphan.HealthId = null;
                        await orphanageDbc.SaveChangesAsync();
                        await _regularDataService.DeleteHealth(healthId, orphanageDbc);
                    }
                }
                ret += await orphanageDbc.SaveChangesAsync();
                if (ret > 0)
                {
                    _logger.Information($"orphan with id({orphan.Id}) has been successfully saved to the database, {ret} changes have been made");
                    return true;
                }
                else
                {
                    _logger.Information($"nothing has changed, false will be returned");
                    return false;
                }
            }
        }

        public async Task<bool> DeleteOrphan(int Oid, OrphanageDbCNoBinary orphanageDbCNoBinary)
        {
            _logger.Information($"trying to delete orphan with id({Oid})");
            bool deleteEducation = false;
            bool deleteHealth = false;
            int? healthId = null, eduId = null;

            var orphanTodelete = await orphanageDbCNoBinary.Orphans.
                    Include(o => o.Education).
                    Include(o => o.HealthStatus).
                    Include(o => o.Name).FirstOrDefaultAsync(o => o.Id == Oid);

            if (orphanTodelete == null)
            {
                _logger.Error($"orphan with id ({Oid}) has not been found, ObjectNotFoundException will be thrown");
                throw new ObjectNotFoundException();
            }

            if (orphanTodelete.Education != null)
            {
                deleteEducation = true;
                eduId = orphanTodelete.EducationId.Value;
                orphanTodelete.EducationId = null;
            }
            if (orphanTodelete.HealthStatus != null)
            {
                deleteHealth = true;
                healthId = orphanTodelete.HealthId.Value;
                orphanTodelete.HealthId = null;
            }

            orphanageDbCNoBinary.Orphans.Remove(orphanTodelete);

            if (await orphanageDbCNoBinary.SaveChangesAsync() > 0)
            {
                if (deleteEducation)
                {
                    if (!await _regularDataService.DeleteStudy(eduId.Value, orphanageDbCNoBinary))
                    {
                        return false;
                    }
                }
                if (deleteHealth)
                {
                    if (!await _regularDataService.DeleteHealth(healthId.Value, orphanageDbCNoBinary))
                    {
                        return false;
                    }
                }
                _logger.Information($"orphan with id({Oid}) has been successfully deleted from the database");
                return true;
            }
            else
            {
                _logger.Information($"nothing has changed, false will be returned");
                return false;
            }
        }

        public async Task<OrphanageDataModel.Persons.Orphan> AddOrphan(OrphanageDataModel.Persons.Orphan orphan)
        {
            _logger.Information($"trying to add new orphan");
            if (orphan.FamilyId <= 0)
            {
                _logger.Warning($"family id is equal or less than zero, null will be returned");
                return null;
            }
            if (orphan.CaregiverId <= 0)
            {
                _logger.Warning($"Caregiver Id is equal or less than zero, null will be returned");
                return null;
            }
            if (orphan.Name == null)
            {
                _logger.Warning($"Name object is null, null will be returned");
                return null;
            }
            using (var orphanageDbc = new OrphanageDbCNoBinary())
            {
                using (var dbT = orphanageDbc.Database.BeginTransaction())
                {
                    if (!Properties.Settings.Default.ForceAdd)
                    {
                        _logger.Information($"ForceAdd option is not activated");
                        if (Properties.Settings.Default.CheckName)
                        {
                            _logger.Information($"CheckName option is activated, trying to get the equal names from database");
                            var ret = GetOrphansByName(orphan.Name, orphanageDbc).FirstOrDefault();
                            if (ret != null)
                            {
                                _logger.Error($"orphan with id({ret.Id}) has the same name, DuplicatedObjectException will be thrown");
                                throw new DuplicatedObjectException(orphan.GetType(), ret.GetType(), ret.Id);
                            }
                            else
                            {
                                _logger.Information($"didn't found any similar names to ({orphan.Name.FullName()}) in the database");
                            }
                        }
                    }
                    orphan.NameId = await _regularDataService.AddName(orphan.Name, orphanageDbc);
                    if (orphan.Education != null)
                    {
                        orphan.EducationId = await _regularDataService.AddStudy(orphan.Education, orphanageDbc);
                    }
                    if (orphan.HealthStatus != null)
                    {
                        orphan.HealthId = await _regularDataService.AddHealth(orphan.HealthStatus, orphanageDbc);
                    }
                    orphanageDbc.Orphans.Add(orphan);
                    if (await orphanageDbc.SaveChangesAsync() > 0)
                    {
                        _uriGenerator.SetOrphanUris(ref orphan);
                        _logger.Information($"orphan with id({orphan.Id}) has been successfully added to the database");
                        return orphan;
                    }
                    else
                    {
                        _logger.Information($"nothing has changed, orphan has not added, null will be returned");
                        return null;
                    }
                }
            }
        }

        public async Task<bool> DeleteOrphan(int Oid)
        {
            _logger.Information($"trying to delete orphan with id({Oid})");
            using (var orphanageDbc = new OrphanageDbCNoBinary())
            {
                using (var dbT = orphanageDbc.Database.BeginTransaction())
                {
                    bool deleteEducation = false;
                    bool deleteHealth = false;
                    int? healthId = null, eduId = null;
                    var orphanTodelete = await orphanageDbc.Orphans.
                        Include(o => o.Education).
                        Include(o => o.HealthStatus).
                        Include(o => o.Name).FirstOrDefaultAsync(o => o.Id == Oid);

                    if (orphanTodelete == null)
                    {
                        _logger.Error($"orphan with id ({Oid}) has not been found, ObjectNotFoundException will be thrown");
                        throw new ObjectNotFoundException();
                    }

                    if (orphanTodelete.Education != null)
                    {
                        deleteEducation = true;
                        eduId = orphanTodelete.EducationId.Value;
                        orphanTodelete.EducationId = null;
                    }
                    if (orphanTodelete.HealthStatus != null)
                    {
                        deleteHealth = true;
                        healthId = orphanTodelete.HealthId.Value;
                        orphanTodelete.HealthId = null;
                    }

                    orphanageDbc.Orphans.Remove(orphanTodelete);

                    if (await orphanageDbc.SaveChangesAsync() > 0)
                    {
                        if (deleteEducation)
                        {
                            if (!await _regularDataService.DeleteStudy(eduId.Value, orphanageDbc))
                            {
                                dbT.Rollback();
                                return false;
                            }
                        }
                        if (deleteHealth)
                        {
                            if (!await _regularDataService.DeleteHealth(healthId.Value, orphanageDbc))
                            {
                                dbT.Rollback();
                                return false;
                            }
                        }
                        dbT.Commit();
                        _logger.Information($"orphan with id({Oid}) has been successfully deleted from the database");
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

        public IEnumerable<OrphanageDataModel.Persons.Orphan> GetOrphansByName(Name nameObject, OrphanageDbCNoBinary orphanageDbCNo)
        {
            _logger.Information($"trying to get orphan with the similar name");
            if (nameObject == null)
            {
                _logger.Error($"name object is null, NullReferenceException will be thrown");
                throw new NullReferenceException();
            }

            var orphans = orphanageDbCNo.Orphans
            .Include(m => m.Name)
            .ToArray();

            var Foundedorphans = orphans.Where(n =>
            n.Name.Equals(nameObject));

            if (Foundedorphans == null) yield return null;

            foreach (var orphan in Foundedorphans)
            {
                _logger.Information($"orphan with id({orphan.Id}) has the same Name as ({nameObject.FullName()})");
                yield return orphan;
            }
        }

        public async Task<IEnumerable<OrphanageDataModel.Persons.Orphan>> GetOrphans(IList<int> ids)
        {
            _logger.Information($"trying to get orphans with the given Id list");
            if (ids == null || ids.Count == 0)
            {
                _logger.Information($"the given Id list is null or empty, null will be returned");
                return null;
            }
            using (var _orphanageDBC = new OrphanageDbCNoBinary())
            {
                var orphans = await _orphanageDBC.Orphans.AsNoTracking()
                    .Where(o => ids.Contains(o.Id))
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

                return prepareOrphansList(orphans);
            }
        }

        public async Task SetOrphanColor(int Oid, int? value)
        {
            _logger.Information($"trying to set the color value ({value ?? -1}) to the orphan with Id({Oid})");
            using (var _orphanageDBC = new OrphanageDBC())
            {
                _orphanageDBC.Configuration.AutoDetectChangesEnabled = true;
                _orphanageDBC.Configuration.LazyLoadingEnabled = true;
                _orphanageDBC.Configuration.ProxyCreationEnabled = true;

                var orphan = await _orphanageDBC.Orphans
                    .Where(o => o.Id == Oid).FirstOrDefaultAsync();

                if (orphan == null)
                {
                    _logger.Error($"orphan with id ({Oid}) has not been found, ObjectNotFoundException will be thrown");
                    throw new ObjectNotFoundException();
                }

                orphan.ColorMark = value;
                if (await _orphanageDBC.SaveChangesAsync() > 0)
                {
                    _logger.Information($"color value ({value ?? -1}) has been set successfully to the orphan with id({Oid})");
                }
                else
                {
                    _logger.Warning($"color value ({value ?? -1}) has not been set to the orphan with id({Oid}), nothing has changed");
                }
            }
        }

        public async Task SetOrphanExclude(int Oid, bool value)
        {
            _logger.Information($"trying to set the isExcluded value ({value.ToString()}) to the orphan with Id({Oid})");
            using (var _orphanageDBC = new OrphanageDBC())
            {
                _orphanageDBC.Configuration.AutoDetectChangesEnabled = true;
                _orphanageDBC.Configuration.LazyLoadingEnabled = true;
                _orphanageDBC.Configuration.ProxyCreationEnabled = true;

                var orphan = await _orphanageDBC.Orphans
                    .Where(o => o.Id == Oid).FirstOrDefaultAsync();

                if (orphan == null)
                {
                    _logger.Error($"orphan with id ({Oid}) has not been found, ObjectNotFoundException will be thrown");
                    throw new ObjectNotFoundException();
                }

                orphan.IsExcluded = value;
                if (await _orphanageDBC.SaveChangesAsync() > 0)
                {
                    _logger.Information($"isExcluded value ({value}) has been set successfully to the orphan with id({Oid})");
                }
                else
                {
                    _logger.Warning($"isExcluded value ({value}) has not been set to the orphan with id({Oid}), nothing has changed");
                }
            }
        }

        public async Task<bool> BailOrphans(int BailId, IList<int> ids)
        {
            bool ret = true;
            _logger.Information($"trying to set Bail with id({BailId}) to the orphans with the given Id list");
            _logger.Information($"trying to get orphans with the given Id list");
            if (ids == null || ids.Count == 0)
            {
                _logger.Information($"the given Id list is null or empty, null will be returned");
                return false;
            }
            using (var _orphanageDBC = new OrphanageDbCNoBinary())
            {
                _orphanageDBC.Configuration.AutoDetectChangesEnabled = true;
                _orphanageDBC.Configuration.LazyLoadingEnabled = true;
                _orphanageDBC.Configuration.ProxyCreationEnabled = true;

                var orphans = await _orphanageDBC.Orphans
                    .Where(o => ids.Contains(o.Id))
                    .ToListAsync();

                if (orphans == null || orphans.Count == 0)
                {
                    _logger.Warning($"there is no orphans founded in the given ids list, false will be returned");
                    return false;
                }
                OrphanageDataModel.FinancialData.Bail bail = null;
                if (BailId > 0)
                {
                    await _orphanageDBC.Bails.AsNoTracking().FirstOrDefaultAsync(b => b.Id == BailId);
                    if (bail == null)
                    {
                        _logger.Error($"bail with id ({BailId}) has not been found, ObjectNotFoundException will be thrown");
                        throw new ObjectNotFoundException();
                    }
                }
                foreach (var orp in orphans)
                {
                    if (BailId > 0)
                    {
                        _logger.Warning($"trying to set value ({BailId}) to bailId property for the orphan with the id ({orp.Id})");
                        orp.IsBailed = true;
                        orp.BailId = BailId;
                    }
                    else
                    {
                        _logger.Warning($"trying to set bailId property to NULL for the orphan with the id ({orp.Id})");
                        orp.IsBailed = false;
                        orp.BailId = null;
                    }
                    if (await _orphanageDBC.SaveChangesAsync() > 0)
                    {
                        _logger.Information($"bailId property has been set successfully to the value ({BailId}) for the orphan with the id ({orp.Id})");
                    }
                    else
                    {
                        _logger.Warning($"bailId property has not been set to the value ({BailId}) for the orphan with the id ({orp.Id})");
                        ret = false;
                    }
                }
            }
            return ret;
        }
    }
}