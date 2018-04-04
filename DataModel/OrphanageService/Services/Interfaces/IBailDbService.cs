using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrphanageService.Services.Interfaces
{
    public interface IBailDbService
    {
        Task<OrphanageDataModel.FinancialData.Bail> GetBailDC(int Bid);

        Task<IEnumerable<OrphanageDataModel.FinancialData.Bail>> GetBails(int pageSize, int pageNum);

        Task<IEnumerable<OrphanageDataModel.Persons.Orphan>> GetOrphans(int Bid);

        Task<IEnumerable<OrphanageDataModel.RegularData.Family>> GetFamilies(int Bid);

        Task<int> GetBailsCount();

        Task<OrphanageDataModel.FinancialData.Bail> AddBail(OrphanageDataModel.FinancialData.Bail bailToAdd);

        Task<bool> SaveBail(OrphanageDataModel.FinancialData.Bail bailToSave);

        Task<bool> DeleteBail(int bailID, bool forceDelete);
    }
}