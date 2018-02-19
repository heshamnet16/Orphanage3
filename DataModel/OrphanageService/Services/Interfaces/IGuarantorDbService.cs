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

        Task<int> GetGuarantorsCount();

        Task<IEnumerable<OrphanageDataModel.FinancialData.Bail>> GetBails(int Gid);

        Task<bool> SaveGuarantor(OrphanageDataModel.Persons.Guarantor guarantor);

        Task<int> AddGuarantor(OrphanageDataModel.Persons.Guarantor guarantor);

        Task<bool> DeleteGuarantor(int Gid);

        /// <summary>
        /// get Guarantor with the same name object
        /// </summary>
        /// <param name="nameObject"></param>
        /// <param name="orphanageDbCNo"></param>
        /// <returns></returns>
        IEnumerable<OrphanageDataModel.Persons.Guarantor> GetGuarantorByName(OrphanageDataModel.RegularData.Name nameObject, OrphanageDbCNoBinary orphanageDbCNo);

        /// <summary>
        /// get Guarantor with the same address object
        /// </summary>
        /// <param name="addressObject"></param>
        /// <param name="orphanageDbCNo"></param>
        /// <returns></returns>
        IEnumerable<OrphanageDataModel.Persons.Guarantor> GetGuarantorByAddress(OrphanageDataModel.RegularData.Address addressObject, OrphanageDbCNoBinary orphanageDbCNo);

    }
}