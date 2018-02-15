using OrphanageDataModel.RegularData;
using OrphanageService.DataContext;
using OrphanageService.Services.Exceptions;
using OrphanageService.Services.Interfaces;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace OrphanageService.Services
{
    public class RegularDataService : IRegularDataService
    {
        private readonly ICheckerService _checkerService;

        public RegularDataService(ICheckerService checkerService)
        {
            _checkerService = checkerService;
        }

        public async Task<int> AddAddress(Address address, OrphanageDbCNoBinary orphanageDBC)
        {
            if (!Properties.Settings.Default.ForceAdd)
            {
                if (Properties.Settings.Default.CheckContactData)
                {
                    var ret = await _checkerService.IsContactDataExist(address, orphanageDBC);
                    if (ret != null)
                    {
                        throw new DuplicatedObjectException(address.GetType(), ret.ObjectType, ret.Id);
                    }
                }
            }
            orphanageDBC.Addresses.Add(address);
            await orphanageDBC.SaveChangesAsync();
            if (address.Id > 0)
                return address.Id;
            else
                return -1;
        }

        public async Task<int> AddHealth(Health health, OrphanageDbCNoBinary orphanageDBC)
        {
            orphanageDBC.Healthies.Add(health);
            await orphanageDBC.SaveChangesAsync();
            if (health.Id > 0)
                return health.Id;
            else
                return -1;
        }

        public async Task<int> AddName(Name name, OrphanageDbCNoBinary orphanageDBC)
        {
            if (!Properties.Settings.Default.ForceAdd)
            {
                if (Properties.Settings.Default.CheckName)
                {
                    var ret = await _checkerService.IsNameExist(name, orphanageDBC);
                    if (ret != null)
                    {
                        throw new DuplicatedObjectException(name.GetType(), ret.ObjectType, ret.Id);
                    }
                }
            }
            orphanageDBC.Names.Add(name);
            await orphanageDBC.SaveChangesAsync();
            if (name.Id > 0)
                return name.Id;
            else
                return -1;
        }

        public async Task<int> AddStudy(Study study, OrphanageDbCNoBinary orphanageDBC)
        {
            orphanageDBC.Studies.Add(study);
            await orphanageDBC.SaveChangesAsync();
            if (study.Id > 0)
                return study.Id;
            else
                return -1;
        }

        public async Task<bool> DeleteAddress(int addressId, OrphanageDbCNoBinary orphanageDBC)
        {
            var addressTodelete = await orphanageDBC.Addresses.FirstOrDefaultAsync(a => a.Id == addressId);
            orphanageDBC.Addresses.Remove(addressTodelete);
            return await orphanageDBC.SaveChangesAsync() > 0 ? true : false;
        }

        public async Task<bool> DeleteHealth(int healthId, OrphanageDbCNoBinary orphanageDBC)
        {
            var healthTodelete = await orphanageDBC.Healthies.FirstOrDefaultAsync(a => a.Id == healthId);
            if (healthTodelete.Orphans == null || healthTodelete.Orphans.Count == 0)
            {
                orphanageDBC.Healthies.Remove(healthTodelete);
                return await orphanageDBC.SaveChangesAsync() > 0 ? true : false;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> DeleteName(int nameId, OrphanageDbCNoBinary orphanageDBC)
        {
            var nameTodelete = await orphanageDBC.Names.FirstOrDefaultAsync(a => a.Id == nameId);
            orphanageDBC.Names.Remove(nameTodelete);
            return await orphanageDBC.SaveChangesAsync() > 0 ? true : false;
        }

        public async Task<bool> DeleteStudy(int studyId, OrphanageDbCNoBinary orphanageDBC)
        {
            var studyTodelete = await orphanageDBC.Studies.FirstOrDefaultAsync(a => a.Id == studyId);
            if (studyTodelete.Orphans == null || studyTodelete.Orphans.Count == 0)
            {
                orphanageDBC.Studies.Remove(studyTodelete);
                return await orphanageDBC.SaveChangesAsync() > 0 ? true : false;
            }
            else
            {
                return false;
            }
        }

        public async Task<int> SaveAddress(Address address, OrphanageDbCNoBinary orphanageDBC)
        {
            var orginalAddress = await orphanageDBC.Addresses.Where(a => a.Id == address.Id).FirstOrDefaultAsync();
            orginalAddress.Note = address.Note;
            orginalAddress.CellPhone = address.CellPhone;
            orginalAddress.City = address.City;
            orginalAddress.Country = address.Country;
            orginalAddress.Facebook = address.Facebook;
            orginalAddress.HomePhone = address.HomePhone;
            orginalAddress.Street = address.Street;
            orginalAddress.Town = address.Town;
            orginalAddress.Twitter = address.Twitter;
            orginalAddress.WorkPhone = address.WorkPhone;
            var ret = await orphanageDBC.SaveChangesAsync();
            return ret;
        }

        public async Task<int> SaveHealth(Health health, OrphanageDbCNoBinary orphanageDBC)
        {
            var orginalHealth = await orphanageDBC.Healthies.FirstOrDefaultAsync(a => a.Id == health.Id);
            orginalHealth.Medicine = health.Medicine;
            orginalHealth.Note = health.Note;
            orginalHealth.SicknessName = health.SicknessName;
            orginalHealth.SupervisorDoctor = health.SupervisorDoctor;
            return await orphanageDBC.SaveChangesAsync();
        }

        public async Task<int> SaveName(Name name, OrphanageDbCNoBinary orphanageDBC)
        {
            var orginalName = await orphanageDBC.Names.Where(a => a.Id == name.Id).FirstOrDefaultAsync();
            orginalName.First = name.First;
            orginalName.Father = name.Father;
            orginalName.Last = name.Last;
            orginalName.EnglishFirst = name.EnglishFirst;
            orginalName.EnglishFather = name.EnglishFather;
            orginalName.EnglishLast = name.EnglishLast;
            var ret = await orphanageDBC.SaveChangesAsync();
            return ret;
        }

        public async Task<int> SaveStudy(Study study, OrphanageDbCNoBinary orphanageDBC)
        {
            var orginalStudy = await orphanageDBC.Studies.FirstOrDefaultAsync(a => a.Id == study.Id);
            orginalStudy.Collage = study.Collage;
            orginalStudy.DegreesRate = study.DegreesRate;
            orginalStudy.MonthlyCost = study.MonthlyCost;
            orginalStudy.Note = study.Note;
            orginalStudy.Reasons = study.Reasons;
            orginalStudy.School = study.School;
            orginalStudy.Stage = study.Stage;
            orginalStudy.Univercity = study.Univercity;
            return await orphanageDBC.SaveChangesAsync();
        }
    }
}