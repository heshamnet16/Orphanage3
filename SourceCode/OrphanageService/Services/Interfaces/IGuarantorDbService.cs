using OrphanageService.DataContext;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrphanageService.Services.Interfaces
{
    public interface IGuarantorDbService
    {
        Task<OrphanageDataModel.Persons.Guarantor> GetGuarantor(int Gid);

        Task<IEnumerable<OrphanageDataModel.Persons.Guarantor>> GetGuarantors(int pageSize, int pageNum);

        Task<IEnumerable<OrphanageDataModel.Persons.Orphan>> GetOrphans(int Gid);

        Task<IEnumerable<int>> GetOrphansIds(int Gid);

        Task<IEnumerable<OrphanageDataModel.RegularData.Family>> GetFamilies(int Gid);

        Task<IEnumerable<int>> GetFamiliesIds(int Gid);

        Task<int> GetGuarantorsCount();

        Task<int> GetFamiliesCount(int Gid);

        Task<int> GetOrphansCount(int Gid);

        Task<int> GetBailsCount(int Gid);

        Task<IEnumerable<OrphanageDataModel.FinancialData.Bail>> GetBails(int Gid);

        Task<IEnumerable<int>> GetBailsIds(int Gid);

        Task<bool> SaveGuarantor(OrphanageDataModel.Persons.Guarantor guarantor);

        Task<OrphanageDataModel.Persons.Guarantor> AddGuarantor(OrphanageDataModel.Persons.Guarantor guarantor);

        Task<bool> DeleteGuarantor(int Gid, bool forceDelete);

        /// <summary>
        /// get Guarantor with the same name object
        /// </summary>
        /// <param name="nameObject"></param>
        /// <param name="orphanageDbCNo"></param>
        /// <returns></returns>
        IEnumerable<OrphanageDataModel.Persons.Guarantor> GetGuarantorByName(OrphanageDataModel.RegularData.Name nameObject, OrphanageDbCNoBinary orphanageDbCNo);

        Task SetGuarantorColor(int Fid, int? value);

        /// <summary>
        /// get Guarantor with the same address object
        /// </summary>
        /// <param name="addressObject"></param>
        /// <param name="orphanageDbCNo"></param>
        /// <returns></returns>
        IEnumerable<OrphanageDataModel.Persons.Guarantor> GetGuarantorByAddress(OrphanageDataModel.RegularData.Address addressObject, OrphanageDbCNoBinary orphanageDbCNo);
    }
}