using OrphanageService.DataContext;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrphanageService.Services.Interfaces
{
    public interface IFatherDbService
    {
        Task<OrphanageDataModel.Persons.Father> GetFather(int Fid);

        Task<IEnumerable<OrphanageDataModel.Persons.Father>> GetFathers(int pageSize, int pageNum);

        Task<IEnumerable<OrphanageDataModel.Persons.Orphan>> GetOrphans(int Fid);

        Task<int> GetFathersCount();

        /// <summary>
        /// add new father object to the database
        /// </summary>
        /// <param name="father">the father object</param>
        /// <param name="forceAdd">added even if it's exist</param>
        /// <returns></returns>
        Task<bool> AddFather(OrphanageDataModel.Persons.Father father, OrphanageDBC orphanageDBC, bool forceAdd);

        Task<bool> SaveFather(OrphanageDataModel.Persons.Father father);

        Task<bool> DeleteFather(int Fid);

        Task<bool> IsExist(OrphanageDataModel.Persons.Father father);

        Task<byte[]> GetFatherPhoto(int Fid);

        Task<byte[]> GetFatherDeathCertificate(int Fid);
    }
}
