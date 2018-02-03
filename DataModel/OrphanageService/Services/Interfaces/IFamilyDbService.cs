using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrphanageService.Services.Interfaces
{
    public interface IFamilyDbService
    {
        Task<OrphanageDataModel.RegularData.Family> GetFamily(int FamId);

        Task<IEnumerable<OrphanageDataModel.RegularData.Family>> GetFamilies(int pageSize, int pageNum);

        Task<IEnumerable<OrphanageDataModel.Persons.Orphan>> GetOrphans(int FamId);

        Task<int> GetFamiliesCount();

        Task<byte[]> GetFamilyCardPage1(int FamId);

        Task<byte[]> GetFamilyCardPage2(int FamId);

        Task SetFamilyCardPage1(int FamId,byte[] data);

        Task SetFamilyCardPage2(int FamId,byte[] data);

        /// <summary>
        /// add new family object to the database
        /// </summary>
        /// <param name="family">the family object</param>
        /// <returns></returns>
        Task<int> AddFamily(OrphanageDataModel.RegularData.Family family);

        Task<bool> SaveFamily(OrphanageDataModel.RegularData.Family family);

        Task<bool> DeleteFamily(int Famid);

        Task<bool> IsExist(OrphanageDataModel.RegularData.Family family);
    }
}