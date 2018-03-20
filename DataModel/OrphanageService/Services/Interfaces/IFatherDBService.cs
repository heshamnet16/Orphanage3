using OrphanageService.DataContext;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrphanageService.Services.Interfaces
{
    public interface IFatherDbService
    {
        Task<OrphanageDataModel.Persons.Father> GetFather(int Fid);

        Task<IEnumerable<OrphanageDataModel.Persons.Father>> GetFathers(int pageSize, int pageNum);

        Task<IEnumerable<OrphanageDataModel.Persons.Father>> GetFathers(IList<int> fathersIds);

        Task<IEnumerable<OrphanageDataModel.Persons.Orphan>> GetOrphans(int Fid);

        Task<int> GetFathersCount();

        Task<int> GetOrphansCount(int FatherId);

        /// <summary>
        /// add new father object to the database
        /// </summary>
        /// <param name="father">the father object</param>
        /// <returns></returns>
        Task<int> AddFather(OrphanageDataModel.Persons.Father father, OrphanageDbCNoBinary orphanageDBC);

        Task<int> SaveFather(OrphanageDataModel.Persons.Father father);

        Task<bool> DeleteFather(int Fid, OrphanageDbCNoBinary orphanageDbCNoBinary);

        Task<bool> IsExist(int Fid);

        Task<byte[]> GetFatherPhoto(int Fid);

        Task SetFatherPhoto(int Fid, byte[] data);

        Task<byte[]> GetFatherDeathCertificate(int Fid);

        Task SetFatherDeathCertificate(int Fid, byte[] data);

        /// <summary>
        /// get Fathers with the same name object
        /// </summary>
        /// <param name="motherObject"></param>
        /// <param name="orphanageDbCNo"></param>
        /// <returns></returns>
        IEnumerable<OrphanageDataModel.Persons.Father> GetFathersByName(OrphanageDataModel.RegularData.Name nameObject, OrphanageDbCNoBinary orphanageDbCNo);
    }
}