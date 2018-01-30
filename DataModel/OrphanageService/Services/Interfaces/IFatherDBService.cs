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

        Task<byte[]> GetFatherPhoto(int Fid);

        Task<byte[]> GetFatherDeathCertificate(int Fid);
    }
}
