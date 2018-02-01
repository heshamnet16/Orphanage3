using OrphanageService.DataContext;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace OrphanageService.Services.Interfaces
{
    public interface IMotherDbService
    {
        Task<OrphanageDataModel.Persons.Mother> GetMother(int Mid);

        Task<IEnumerable<OrphanageDataModel.Persons.Mother>> GetMothers(int pageSize, int pageNum);

        Task<IEnumerable<OrphanageDataModel.Persons.Orphan>> GetOrphans(int Mid);

        Task<int> GetMotherCount();

        Task<byte[]> GetMotherIdPhotoFace(int Mid);

        Task<byte[]> GetMotherIdPhotoBack(int Mid);

        /// <summary>
        /// add new mother object to the database
        /// </summary>
        /// <param name="mother">the mother object</param>
        /// <param name="forceAdd">added even if it's exist</param>
        /// <returns></returns>
        Task<bool> AddMother(OrphanageDataModel.Persons.Mother mother, OrphanageDBC orphanageDBC, bool forceAdd);

        Task<bool> SaveMother(OrphanageDataModel.Persons.Mother mother);

        Task<bool> DeleteMother(int Mid);

        Task<bool> IsExist(OrphanageDataModel.Persons.Mother mother);
    }
}
