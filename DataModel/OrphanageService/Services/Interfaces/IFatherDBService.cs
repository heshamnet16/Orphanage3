using OrphanageService.DataContext.Persons;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrphanageService.Services.Interfaces
{
    public interface IFatherDbService
    {
        Task<FatherDto> GetFather(int Fid);

        Task<IEnumerable<FatherDto>> GetFathers(int pageSize, int pageNum);

        Task<IEnumerable<OrphanDto>> GetOrphans(int Fid);

        Task<int> GetFathersCount();

        Task<byte[]> GetFatherPhoto(int Fid);

        Task<byte[]> GetFatherDeathCertificate(int Fid);
    }
}
