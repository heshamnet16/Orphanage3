using OrphanageDataModel.RegularData;
using OrphanageService.DataContext;
using OrphanageService.Services.Interfaces;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace OrphanageService.Services
{
    public class RegularDataService : IRegularDataService
    {
        public async Task<int> AddAddress(Address address, OrphanageDbCNoBinary orphanageDBC)
        {
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

        public async Task<bool> SaveAddress(Address address, OrphanageDbCNoBinary orphanageDBC)
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
            return ret > 0 ? true : false;
        }

        public async Task<bool> SaveHalth(Health health, OrphanageDbCNoBinary orphanageDBC)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SaveName(Name name, OrphanageDbCNoBinary orphanageDBC)
        {
            var orginalName = await orphanageDBC.Names.Where(a => a.Id == name.Id).FirstOrDefaultAsync();
            orginalName.First = name.First;
            orginalName.Father = name.Father;
            orginalName.Last = name.Last;
            orginalName.EnglishFirst = name.EnglishFirst;
            orginalName.EnglishFather = name.EnglishFather;
            orginalName.EnglishLast = name.EnglishLast;
            var ret = await orphanageDBC.SaveChangesAsync();
            return ret > 0 ? true : false;
        }

        public async Task<bool> SaveStudy(Study study, OrphanageDbCNoBinary orphanageDBC)
        {
            throw new NotImplementedException();
        }
    }
}