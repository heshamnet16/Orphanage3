using OrphanageService.DataContext.Persons;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrphanageService.Services.Interfaces
{
    public interface IFatherDbService
    {
        Task<FatherDC> GetFather(int Fid);

        Task<IEnumerable<FatherDC>> GetFathers(int pageSize, int pageNum);

        Task<IEnumerable<OrphanDC>> GetOrphans(int Fid);

        Task<int> GetFathersCount();

        Task<byte[]> GetFatherPhoto(int Fid);

        Task<byte[]> GetFatherDeathCertificate(int Fid);
    }
}
