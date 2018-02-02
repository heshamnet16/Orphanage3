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
        Task<int> AddFather(OrphanageDataModel.Persons.Father father, OrphanageDbCNoBinary orphanageDBC);

        Task<bool> SaveFather(OrphanageDataModel.Persons.Father father);

        Task<bool> DeleteFather(int Fid, OrphanageDbCNoBinary orphanageDbCNoBinary);

        Task<bool> IsExist(int Fid);

        Task<byte[]> GetFatherPhoto(int Fid);

        Task<byte[]> GetFatherDeathCertificate(int Fid);
    }
}