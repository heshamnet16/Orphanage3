using OrphanageDataModel.RegularData;
using OrphanageService.DataContext;
using OrphanageService.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public Task<int> AddFamily(OrphanageDataModel.RegularData.Family family, OrphanageDbCNoBinary orphanageDBC)
        {
            throw new NotImplementedException();
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
    }
}
