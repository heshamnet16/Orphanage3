using OrphanageService.DataContext.Persons;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace OrphanageService.Services.Interfaces
{
    interface IMotherDbService
    {
        Task<MotherDC> GetMother(int Fid);

        Task<IEnumerable<MotherDC>> GetMothers(int pageSize, int pageNum);

        Task<IEnumerable<OrphanDC>> GetOrphans(int Fid);

        Task<int> GetMotherCount();

        Task<byte[]> GetFatherPhoto(int Fid);

        Task<byte[]> GetFatherDeathCertificate(int Fid);
    }
}
