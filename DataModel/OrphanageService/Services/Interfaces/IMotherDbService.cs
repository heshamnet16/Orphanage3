using OrphanageService.DataContext;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrphanageService.Services.Interfaces
{
    public interface IMotherDbService
    {
        Task<OrphanageDataModel.Persons.Mother> GetMother(int Mid);

        Task<IEnumerable<OrphanageDataModel.Persons.Mother>> GetMothers(int pageSize, int pageNum);

        Task<IEnumerable<OrphanageDataModel.Persons.Mother>> GetMothers(IList<int> motherIds);

        Task<IEnumerable<OrphanageDataModel.Persons.Orphan>> GetOrphans(int Mid);

        Task<int> GetOrphansCount(int Mid);

        Task<int> GetMotherCount();

        Task<byte[]> GetMotherIdPhotoFace(int Mid);

        Task<byte[]> GetMotherIdPhotoBack(int Mid);

        Task SetMotherIdPhotoFace(int Mid, byte[] data);

        Task SetMotherIdPhotoBack(int Mid, byte[] data);

        /// <summary>
        /// add new mother object to the database
        /// </summary>
        /// <param name="mother">the mother object</param>
        /// <param name="forceAdd">added even if it's exist</param>
        /// <returns></returns>
        Task<int> AddMother(OrphanageDataModel.Persons.Mother mother, OrphanageDbCNoBinary orphanageDBC);

        Task<int> SaveMother(OrphanageDataModel.Persons.Mother mother);

        Task<bool> DeleteMother(int Mid, OrphanageDbCNoBinary orphanageDbCNoBinary);

        Task<bool> IsExist(int Mid);

        /// <summary>
        /// get mothers with the same name object
        /// </summary>
        /// <param name="motherObject"></param>
        /// <param name="orphanageDbCNo"></param>
        /// <returns></returns>
        IEnumerable<OrphanageDataModel.Persons.Mother> GetMothersByName(OrphanageDataModel.RegularData.Name nameObject, OrphanageDbCNoBinary orphanageDbCNo);

        /// <summary>
        /// get mothers with the same address object
        /// </summary>
        /// <param name="motherObject"></param>
        /// <param name="orphanageDbCNo"></param>
        /// <returns></returns>
        IEnumerable<OrphanageDataModel.Persons.Mother> GetMothersByAddress(OrphanageDataModel.RegularData.Address addressObject, OrphanageDbCNoBinary orphanageDbCNo);
    }
}