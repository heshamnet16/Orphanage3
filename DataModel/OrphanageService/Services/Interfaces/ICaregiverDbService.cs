using OrphanageService.DataContext;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrphanageService.Services.Interfaces
{
    public interface ICaregiverDbService
    {
        Task<OrphanageDataModel.Persons.Caregiver> GetCaregiver(int Cid);

        Task<IEnumerable<OrphanageDataModel.Persons.Caregiver>> GetCaregivers(int pageSize, int pageNum);

        Task<IEnumerable<OrphanageDataModel.Persons.Orphan>> GetOrphans(int Cid);

        Task<bool> SaveCaregiver(OrphanageDataModel.Persons.Caregiver caregiver);

        Task<OrphanageDataModel.Persons.Caregiver> AddCaregiver(OrphanageDataModel.Persons.Caregiver caregiver);

        Task<bool> DeleteCaregiver(int Cid);

        Task<int> GetCaregiversCount();

        Task<byte[]> GetIdentityCardFace(int Cid);

        Task<byte[]> GetIdentityCardBack(int Cid);

        Task SetIdentityCardFace(int Cid, byte[] data);

        Task SetIdentityCardBack(int Cid, byte[] data);

        /// <summary>
        /// get caregivers with the same name object
        /// </summary>
        /// <param name="nameObject"></param>
        /// <param name="orphanageDbCNo"></param>
        /// <returns></returns>
        IEnumerable<OrphanageDataModel.Persons.Caregiver> GetCaregiversByName(OrphanageDataModel.RegularData.Name nameObject, OrphanageDbCNoBinary orphanageDbCNo);

        /// <summary>
        /// get caregivers with the same address object
        /// </summary>
        /// <param name="addressObject"></param>
        /// <param name="orphanageDbCNo"></param>
        /// <returns></returns>
        IEnumerable<OrphanageDataModel.Persons.Caregiver> GetCaregiversByAddress(OrphanageDataModel.RegularData.Address addressObject, OrphanageDbCNoBinary orphanageDbCNo);
    }
}