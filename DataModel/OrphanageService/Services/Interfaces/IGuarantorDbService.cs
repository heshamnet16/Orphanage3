using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrphanageService.Services.Interfaces
{
    public interface IGuarantorDbService
    {
        Task<OrphanageDataModel.Persons.Guarantor> GetGuarantor(int Gid);

        Task<IEnumerable<OrphanageDataModel.Persons.Guarantor>> GetGuarantors(int pageSize, int pageNum);

        Task<IEnumerable<OrphanageDataModel.Persons.Orphan>> GetOrphans(int Gid);

        Task<int> GetGuarantorsCount();

        Task<IEnumerable<OrphanageDataModel.FinancialData.Bail>> GetBails(int Gid);

        Task<bool> SaveGuarantor(OrphanageDataModel.Persons.Guarantor guarantor);

        Task<int> AddGuarantor(OrphanageDataModel.Persons.Guarantor guarantor);

        Task<bool> DeleteGuarantor(int Gid);
    }
}