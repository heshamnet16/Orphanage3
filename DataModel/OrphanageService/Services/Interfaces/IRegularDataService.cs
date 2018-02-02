using OrphanageService.DataContext;
using System.Threading.Tasks;

namespace OrphanageService.Services.Interfaces
{
    public interface IRegularDataService
    {
        /// <summary>
        /// add Name object and return the ID of the new Name otherwise return -1
        /// </summary>
        /// <param name="name"> the name object</param>
        /// <param name="orphanageDBC">reference to the current data context</param>
        /// <returns>return the ID of the new Name otherwise return -1</returns>
        Task<int> AddName(OrphanageDataModel.RegularData.Name name, OrphanageDbCNoBinary orphanageDBC);

        /// <summary>
        /// add address object and return the ID of the new address otherwise return -1
        /// </summary>
        /// <param name="address"> the address object</param>
        /// <param name="orphanageDBC">reference to the current data context</param>
        /// <returns>return the ID of the new address otherwise return -1</returns>
        Task<int> AddAddress(OrphanageDataModel.RegularData.Address address, OrphanageDbCNoBinary orphanageDBC);

        /// <summary>
        /// add study object and return the ID of the new study otherwise return -1
        /// </summary>
        /// <param name="study"> the study object</param>
        /// <param name="orphanageDBC">reference to the current data context</param>
        /// <returns>return the ID of the new study otherwise return -1</returns>
        Task<int> AddStudy(OrphanageDataModel.RegularData.Study study, OrphanageDbCNoBinary orphanageDBC);

        /// <summary>
        /// add health object and return the ID of the new health otherwise return -1
        /// </summary>
        /// <param name="health"> the health object</param>
        /// <param name="orphanageDBC">reference to the current data context</param>
        /// <returns>return the ID of the new health otherwise return -1</returns>
        Task<int> AddHealth(OrphanageDataModel.RegularData.Health health, OrphanageDbCNoBinary orphanageDBC);

        /// <summary>
        /// add family object and return the ID of the new family otherwise return -1
        /// </summary>
        /// <param name="family"> the family object</param>
        /// <param name="orphanageDBC">reference to the current data context</param>
        /// <returns>return the ID of the new family otherwise return -1</returns>
        Task<int> AddFamily(OrphanageDataModel.RegularData.Family family, OrphanageDbCNoBinary orphanageDBC);
    }
}