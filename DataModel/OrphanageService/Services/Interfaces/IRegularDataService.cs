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
        /// save Name object and return true when success otherwise false
        /// </summary>
        /// <param name="name"> the name object</param>
        /// <param name="orphanageDBC">reference to the current data context</param>
        /// <returns>return true when success otherwise false</returns>
        Task<int> SaveName(OrphanageDataModel.RegularData.Name nameId, OrphanageDbCNoBinary orphanageDBC);

        /// <summary>
        /// delete Name object and return true when success otherwise false
        /// </summary>
        /// <param name="name"> the name object</param>
        /// <param name="orphanageDBC">reference to the current data context</param>
        /// <returns>return true when success otherwise false</returns>
        Task<bool> DeleteName(int nameId, OrphanageDbCNoBinary orphanageDBC);

        /// <summary>
        /// add address object and return the ID of the new address otherwise return -1
        /// </summary>
        /// <param name="address"> the address object</param>
        /// <param name="orphanageDBC">reference to the current data context</param>
        /// <returns>return the ID of the new address otherwise return -1</returns>
        Task<int> AddAddress(OrphanageDataModel.RegularData.Address address, OrphanageDbCNoBinary orphanageDBC);

        /// <summary>
        /// save address object and return true when success otherwise false
        /// </summary>
        /// <param name="address"> the address object</param>
        /// <param name="orphanageDBC">reference to the current data context</param>
        /// <returns>return true when success otherwise false</returns>
        Task<int> SaveAddress(OrphanageDataModel.RegularData.Address address, OrphanageDbCNoBinary orphanageDBC);

        /// <summary>
        /// delete address object and return true when success otherwise false
        /// </summary>
        /// <param name="address"> the address object</param>
        /// <param name="orphanageDBC">reference to the current data context</param>
        /// <returns>return true when success otherwise false</returns>
        Task<bool> DeleteAddress(int addressId, OrphanageDbCNoBinary orphanageDBC);

        /// <summary>
        /// add study object and return the ID of the new study otherwise return -1
        /// </summary>
        /// <param name="study"> the study object</param>
        /// <param name="orphanageDBC">reference to the current data context</param>
        /// <returns>return the ID of the new study otherwise return -1</returns>
        Task<int> AddStudy(OrphanageDataModel.RegularData.Study study, OrphanageDbCNoBinary orphanageDBC);

        /// <summary>
        /// save study object and return true when success otherwise false
        /// </summary>
        /// <param name="study"> the study object</param>
        /// <param name="orphanageDBC">reference to the current data context</param>
        /// <returns>return true when success otherwise false</returns>
        Task<int> SaveStudy(OrphanageDataModel.RegularData.Study study, OrphanageDbCNoBinary orphanageDBC);

        /// <summary>
        /// delete study object and return true when success otherwise false
        /// </summary>
        /// <param name="study"> the study object</param>
        /// <param name="orphanageDBC">reference to the current data context</param>
        /// <returns>return true when success otherwise false</returns>
        Task<bool> DeleteStudy(int studyId, OrphanageDbCNoBinary orphanageDBC);

        /// <summary>
        /// add health object and return the ID of the new health otherwise return -1
        /// </summary>
        /// <param name="health"> the health object</param>
        /// <param name="orphanageDBC">reference to the current data context</param>
        /// <returns>return the ID of the new health otherwise return -1</returns>
        Task<int> AddHealth(OrphanageDataModel.RegularData.Health health, OrphanageDbCNoBinary orphanageDBC);

        /// <summary>
        /// save health object and return true when success otherwise false
        /// </summary>
        /// <param name="health"> the health object</param>
        /// <param name="orphanageDBC">reference to the current data context</param>
        /// <returns>return true when success otherwise false</returns>
        Task<int> SaveHealth(OrphanageDataModel.RegularData.Health health, OrphanageDbCNoBinary orphanageDBC);

        /// <summary>
        /// delete health object and return true when success otherwise false
        /// </summary>
        /// <param name="health"> the health object</param>
        /// <param name="orphanageDBC">reference to the current data context</param>
        /// <returns>return true when success otherwise false</returns>
        Task<bool> DeleteHealth(int healthId, OrphanageDbCNoBinary orphanageDBC);
    }
}