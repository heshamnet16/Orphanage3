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
    public class OrphanDbService : IOrphanDbService
    {
        private readonly ISelfLoopBlocking _loopBlocking;
        private readonly IUriGenerator _uriGenerator;
        private readonly IRegularDataService _regularDataService;

        public OrphanDbService(ISelfLoopBlocking loopBlocking, IUriGenerator uriGenerator, IRegularDataService regularDataService)
        {
            _loopBlocking = loopBlocking;
            _uriGenerator = uriGenerator;
            _regularDataService = regularDataService;
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

                if (orphan == null) throw new ObjectNotFoundException();

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
                    throw new ObjectNotFoundException();

                orphan.FacePhotoData = data;
                var ret = await _orphanageDBC.SaveChangesAsync();
                if (ret > 0)
                {
                    return true;
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
                    throw new ObjectNotFoundException();

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
                    throw new ObjectNotFoundException();

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
                    throw new ObjectNotFoundException();

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
                    Include(o => o.Education)
                    .Where(o => o.Id == Oid).FirstOrDefaultAsync();

                if (orphan == null || orphan.Education == null)
                    throw new ObjectNotFoundException();

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
                    throw new ObjectNotFoundException();

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
                    throw new ObjectNotFoundException();

                orphan.HealthStatus.ReporteFileData = data;
                var ret = await _orphanageDBC.SaveChangesAsync();
                if (ret > 0)
                {
                    return true;
                }
                else { return false; }
            }
        }

        ///<inheritdoc/>
        public async Task<int> AddOrphan(OrphanageDataModel.Persons.Orphan orphan, OrphanageDbCNoBinary orphanageDBC)
        {
            if (orphan.FamilyId <= 0) return -1;
            if (orphan.CaregiverId <= 0) return -1;
            if (orphan.Name == null) return -1;

            if (!Properties.Settings.Default.ForceAdd)
            {
                if (Properties.Settings.Default.CheckName)
                {
                    var ret = GetOrphansByName(orphan.Name, orphanageDBC).FirstOrDefault();
                    if (ret != null)
                    {
                        throw new DuplicatedObjectException(orphan.GetType(), ret.GetType(), ret.Id);
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
                return orphan.Id;
            }
            else
            {
                return -1;
            }
        }

        public async Task<bool> SaveOrphan(OrphanageDataModel.Persons.Orphan orphan)
        {
            if (orphan.FamilyId <= 0) return false;
            if (orphan.CaregiverId <= 0) return false;
            if (orphan.Name == null) return false;
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
                if (orginalOrphan == null) throw new ObjectNotFoundException();
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
                    return true;
                else
                    return false;
            }
        }

        public async Task<bool> DeleteOrphan(int Oid, OrphanageDbCNoBinary orphanageDbCNoBinary)
        {
            var orphanTodelete = await orphanageDbCNoBinary.Orphans.
                    Include(o => o.Education).
                    Include(o => o.HealthStatus).
                    Include(o => o.Name).FirstOrDefaultAsync(o => o.Id == Oid);

            if (orphanTodelete == null)
                throw new ObjectNotFoundException();
            if (orphanTodelete.Education != null)
            {
                var eduId = orphanTodelete.EducationId.Value;
                orphanTodelete.EducationId = null;
                await orphanageDbCNoBinary.SaveChangesAsync();
                if (!await _regularDataService.DeleteStudy(eduId, orphanageDbCNoBinary))
                {
                    return false;
                }
            }
            if (orphanTodelete.HealthStatus != null)
            {
                var healthId = orphanTodelete.HealthId.Value;
                orphanTodelete.HealthId = null;
                await orphanageDbCNoBinary.SaveChangesAsync();

                if (!await _regularDataService.DeleteHealth(healthId, orphanageDbCNoBinary))
                {
                    return false;
                }
            }
            orphanageDbCNoBinary.Orphans.Remove(orphanTodelete);

            if (await orphanageDbCNoBinary.SaveChangesAsync() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<int> AddOrphan(OrphanageDataModel.Persons.Orphan orphan)
        {
            if (orphan.FamilyId <= 0) return -1;
            if (orphan.CaregiverId <= 0) return -1;
            if (orphan.Name == null) return -1;
            using (var orphanageDbc = new OrphanageDbCNoBinary())
            {
                using (var dbT = orphanageDbc.Database.BeginTransaction())
                {
                    if (!Properties.Settings.Default.ForceAdd)
                    {
                        if (Properties.Settings.Default.CheckName)
                        {
                            var ret = GetOrphansByName(orphan.Name, orphanageDbc).FirstOrDefault();
                            if (ret != null)
                            {
                                throw new DuplicatedObjectException(orphan.GetType(), ret.GetType(), ret.Id);
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
                        dbT.Commit();
                        return orphan.Id;
                    }
                    else
                    {
                        dbT.Rollback();
                        return -1;
                    }
                }
            }
        }

        public async Task<bool> DeleteOrphan(int Oid)
        {
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
                        throw new ObjectNotFoundException();
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
                        if(deleteEducation)
                        {
                            if (!await _regularDataService.DeleteStudy(eduId.Value, orphanageDbc))
                            {
                                dbT.Rollback();
                                return false;
                            }
                        }
                        if(deleteHealth)
                        {
                            if (!await _regularDataService.DeleteHealth(healthId.Value, orphanageDbc))
                            {
                                dbT.Rollback();
                                return false;
                            }
                        }
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

        public IEnumerable<OrphanageDataModel.Persons.Orphan> GetOrphansByName(Name nameObject, OrphanageDbCNoBinary orphanageDbCNo)
        {
            if (nameObject == null) throw new NullReferenceException();

            var orphans = orphanageDbCNo.Orphans
            .Include(m => m.Name)
            .ToArray();

            var Foundedorphans = orphans.Where(n =>
            n.Name.Equals(nameObject));

            if (Foundedorphans == null) yield return null;

            foreach (var family in Foundedorphans)
            {
                yield return family;
            }
        }

        public async Task<IEnumerable<OrphanageDataModel.Persons.Orphan>> GetOrphans(IList<int> ids)
        {
            IList<OrphanageDataModel.Persons.Orphan> orphansList = new List<OrphanageDataModel.Persons.Orphan>();
            try
            {
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

                    foreach (var orphan in orphans)
                    {
                        var orphanTofill = orphan;
                        _loopBlocking.BlockOrphanSelfLoop(ref orphanTofill);
                        _uriGenerator.SetOrphanUris(ref orphanTofill);
                        orphansList.Add(orphanTofill);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return orphansList;
        }
    }
}