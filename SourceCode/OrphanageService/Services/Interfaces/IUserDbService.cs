using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrphanageService.Services.Interfaces
{
    public interface IUserDbService
    {
        Task<OrphanageDataModel.Persons.User> GetUser(int Uid);

        Task<IEnumerable<OrphanageDataModel.Persons.User>> GetUsers(int pageSize, int pageNum);

        Task<int> GetUsersCount();

        Task<IEnumerable<OrphanageDataModel.FinancialData.Bail>> GetBails(int Uid);

        Task<IEnumerable<OrphanageDataModel.FinancialData.Bail>> GetBails(int Uid, int pageSize, int pageNum);

        Task<int> GetBailsCount(int Uid);

        Task<IEnumerable<OrphanageDataModel.Persons.Guarantor>> GetGuarantors(int Uid);

        Task<IEnumerable<OrphanageDataModel.Persons.Guarantor>> GetGuarantors(int Uid, int pageSize, int pageNum);

        Task<int> GetGuarantorsCount(int Uid);

        Task<IEnumerable<OrphanageDataModel.FinancialData.Account>> GetAccounts(int Uid);

        Task<IEnumerable<OrphanageDataModel.FinancialData.Account>> GetAccounts(int Uid, int pageSize, int pageNum);

        Task<int> GetAccountsCount(int Uid);

        Task<IEnumerable<OrphanageDataModel.Persons.Father>> GetFathers(int Uid);

        Task<IEnumerable<OrphanageDataModel.Persons.Father>> GetFathers(int Uid, int pageSize, int pageNum);

        Task<int> GetFathersCount(int Uid);

        Task<IEnumerable<OrphanageDataModel.Persons.Mother>> GetMothers(int Uid);

        Task<IEnumerable<OrphanageDataModel.Persons.Mother>> GetMothers(int Uid, int pageSize, int pageNum);

        Task<int> GetMothersCount(int Uid);

        Task<IEnumerable<OrphanageDataModel.Persons.Orphan>> GetOrphans(int Uid);

        Task<IEnumerable<OrphanageDataModel.Persons.Orphan>> GetOrphans(int Uid, int pageSize, int pageNum);

        Task<int> GetOrphansCount(int Uid);

        Task<IEnumerable<OrphanageDataModel.Persons.Caregiver>> GetCaregivers(int Uid);

        Task<IEnumerable<OrphanageDataModel.Persons.Caregiver>> GetCaregivers(int Uid, int pageSize, int pageNum);

        Task<int> GetCaregiversCount(int Uid);

        Task<IEnumerable<OrphanageDataModel.RegularData.Family>> GetFamilies(int Uid);

        Task<IEnumerable<OrphanageDataModel.RegularData.Family>> GetFamilies(int Uid, int pageSize, int pageNum);

        Task<int> GetFamiliesCount(int Uid);
    }
}