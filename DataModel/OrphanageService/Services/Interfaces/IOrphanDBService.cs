using OrphanageService.DataContext;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrphanageService.Services.Interfaces
{
    public interface IOrphanDbService
    {
        Task<OrphanageDataModel.Persons.Orphan> GetOrphan(int id);

        Task<IEnumerable<OrphanageDataModel.Persons.Orphan>> GetOrphans(IList<int> ids);

        Task<IEnumerable<OrphanageDataModel.Persons.Orphan>> GetOrphans(int pageSize, int pageNum);

        Task<IEnumerable<OrphanageDataModel.Persons.Orphan>> GetExcludedOrphans();

        Task<IEnumerable<OrphanageDataModel.Persons.Orphan>> GetBrothers(int Oid);

        Task<int> GetBrothersCount(int Oid);

        Task<int> GetOrphansCount();

        /// <summary>
        /// add new orphan object to the database
        /// </summary>
        /// <param name="orphan">the orphan object</param>
        /// <returns></returns>
        Task<OrphanageDataModel.Persons.Orphan> AddOrphan(OrphanageDataModel.Persons.Orphan orphan, OrphanageDbCNoBinary orphanageDBC);

        /// <summary>
        /// add new orphan object to the database
        /// </summary>
        /// <param name="orphan">the orphan object</param>
        /// <param name="orphanageDBC">the database context></param>
        /// <returns></returns>
        Task<OrphanageDataModel.Persons.Orphan> AddOrphan(OrphanageDataModel.Persons.Orphan orphan);

        Task<bool> SaveOrphan(OrphanageDataModel.Persons.Orphan orphan);

        Task SetOrphanColor(int Fid, int? value);

        Task SetOrphanExclude(int Fid, bool value);

        Task<bool> DeleteOrphan(int Oid);

        Task<bool> DeleteOrphan(int Oid, OrphanageDbCNoBinary orphanageDbCNoBinary);

        Task<byte[]> GetOrphanFaceImage(int Oid);

        Task<byte[]> GetOrphanBirthCertificate(int Oid);

        Task<byte[]> GetOrphanFamilyCardPagePhoto(int Oid);

        Task<byte[]> GetOrphanFullPhoto(int Oid);

        Task<byte[]> GetOrphanCertificate(int Oid);

        Task<byte[]> GetOrphanCertificate2(int Oid);

        Task<byte[]> GetOrphanHealthReporte(int Oid);

        Task<bool> SetOrphanFaceImage(int Oid, byte[] data);

        Task<bool> SetOrphanBirthCertificate(int Oid, byte[] data);

        Task<bool> SetOrphanFamilyCardPagePhoto(int Oid, byte[] data);

        Task<bool> SetOrphanFullPhoto(int Oid, byte[] data);

        Task<bool> SetOrphanCertificate(int Oid, byte[] data);

        Task<bool> SetOrphanCertificate2(int Oid, byte[] data);

        Task<bool> SetOrphanHealthReporte(int Oid, byte[] data);

        /// <summary>
        /// get orphans with the same name object
        /// </summary>
        /// <param name="motherObject"></param>
        /// <param name="orphanageDbCNo"></param>
        /// <returns></returns>
        IEnumerable<OrphanageDataModel.Persons.Orphan> GetOrphansByName(OrphanageDataModel.RegularData.Name nameObject, OrphanageDbCNoBinary orphanageDbCNo);
    }
}