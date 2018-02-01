using OrphanageService.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrphanageService.Services.Interfaces
{
    public interface IRegularDataService
    {
        Task<int> AddName(OrphanageDataModel.RegularData.Name name, OrphanageDBC orphanageDBC);

        Task<int> AddAddress(OrphanageDataModel.RegularData.Address address, OrphanageDBC orphanageDBC);

        Task<int> AddStudy(OrphanageDataModel.RegularData.Study study, OrphanageDBC orphanageDBC);

        Task<int> AddHealth(OrphanageDataModel.RegularData.Health health, OrphanageDBC orphanageDBC);

        Task<int> AddFamily(OrphanageDataModel.RegularData.Family family, OrphanageDBC orphanageDBC);
    }
}
