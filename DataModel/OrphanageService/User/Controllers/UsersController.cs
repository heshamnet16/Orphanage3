using OrphanageService.Services.Interfaces;
using OrphanageService.Utilities.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace OrphanageService.User.Controllers
{
    [RoutePrefix("api/user")]
    public class UsersController : ApiController
    {
        private IUserDbService _userDBService;
        private readonly IHttpResponseMessageConfiguerer _httpResponseMessageConfiguerer;

        public UsersController(IUserDbService userDBService, IHttpResponseMessageConfiguerer httpResponseMessageConfiguerer)
        {
            _userDBService = userDBService;
            _httpResponseMessageConfiguerer = httpResponseMessageConfiguerer;
        }

        //api/user/account/{id}
        [HttpGet]
        [Route("accounts/{uid}")]
        public async Task<IEnumerable<OrphanageDataModel.FinancialData.Account>> GetAccounts(int Uid)
        {
            return await _userDBService.GetAccounts(Uid);
        }

        [HttpGet]
        [Route("accounts/{uid}/{pageSize}/{pageNum}")]
        public async Task<IEnumerable<OrphanageDataModel.FinancialData.Account>> GetAccounts(int Uid, int pageSize, int pageNum)
        {
            return await _userDBService.GetAccounts(Uid, pageSize, pageNum);
        }

        [HttpGet]
        [Route("accounts/count/{uid}")]
        public async Task<int> GetAccountsCount(int Uid)
        {
            return await _userDBService.GetAccountsCount(Uid);
        }

        [HttpGet]
        [Route("bails/{uid}")]
        public async Task<IEnumerable<OrphanageDataModel.FinancialData.Bail>> GetBails(int Uid)
        {
            return await _userDBService.GetBails(Uid);
        }

        [HttpGet]
        [Route("bails/{uid}/{pageSize}/{pageNum}")]
        public async Task<IEnumerable<OrphanageDataModel.FinancialData.Bail>> GetBails(int Uid, int pageSize, int pageNum)
        {
            return await _userDBService.GetBails(Uid, pageSize, pageNum);
        }

        [HttpGet]
        [Route("bails/count/{uid}")]
        public async Task<int> GetBailsCount(int Uid)
        {
            return await _userDBService.GetBailsCount(Uid);
        }

        [HttpGet]
        [Route("caregivers/{uid}")]
        public async Task<IEnumerable<OrphanageDataModel.Persons.Caregiver>> GetCaregivers(int Uid)
        {
            return await _userDBService.GetCaregivers(Uid);
        }

        [HttpGet]
        [Route("caregivers/{uid}/{pageSize}/{pageNum}")]
        public async Task<IEnumerable<OrphanageDataModel.Persons.Caregiver>> GetCaregivers(int Uid, int pageSize, int pageNum)
        {
            return await _userDBService.GetCaregivers(Uid, pageSize, pageNum);
        }

        [HttpGet]
        [Route("caregivers/count/{uid}")]
        public async Task<int> GetCaregiversCount(int Uid)
        {
            return await _userDBService.GetCaregiversCount(Uid);
        }

        [HttpGet]
        [Route("families/{uid}")]
        public async Task<IEnumerable<OrphanageDataModel.RegularData.Family>> GetFamilies(int Uid)
        {
            return await _userDBService.GetFamilies(Uid);
        }

        [HttpGet]
        [Route("families/{uid}/{pageSize}/{pageNum}")]
        public async Task<IEnumerable<OrphanageDataModel.RegularData.Family>> GetFamilies(int Uid, int pageSize, int pageNum)
        {
            return await _userDBService.GetFamilies(Uid, pageSize, pageNum);
        }

        [HttpGet]
        [Route("families/count/{uid}")]
        public async Task<int> GetFamiliesCount(int Uid)
        {
            return await _userDBService.GetFamiliesCount(Uid);
        }

        [HttpGet]
        [Route("fathers/{uid}")]
        public async Task<IEnumerable<OrphanageDataModel.Persons.Father>> GetFathers(int Uid)
        {
            return await _userDBService.GetFathers(Uid);
        }

        [HttpGet]
        [Route("fathers/{uid}/{pageSize}/{pageNum}")]
        public async Task<IEnumerable<OrphanageDataModel.Persons.Father>> GetFathers(int Uid, int pageSize, int pageNum)
        {
            return await _userDBService.GetFathers(Uid, pageSize, pageNum);
        }

        [HttpGet]
        [Route("fathers/count/{uid}")]
        public async Task<int> GetFathersCount(int Uid)
        {
            return await _userDBService.GetFathersCount(Uid);
        }

        [HttpGet]
        [Route("guarantors/{uid}")]
        public async Task<IEnumerable<OrphanageDataModel.Persons.Guarantor>> GetGuarantors(int Uid)
        {
            return await _userDBService.GetGuarantors(Uid);
        }

        [HttpGet]
        [Route("guarantors/{uid}/{pageSize}/{pageNum}")]
        public async Task<IEnumerable<OrphanageDataModel.Persons.Guarantor>> GetGuarantors(int Uid, int pageSize, int pageNum)
        {
            return await _userDBService.GetGuarantors(Uid, pageSize, pageNum);
        }

        [HttpGet]
        [Route("guarantors/count/{uid}")]
        public async Task<int> GetGuarantorsCount(int Uid)
        {
            return await _userDBService.GetGuarantorsCount(Uid);
        }

        [HttpGet]
        [Route("mothers/{uid}")]
        public async Task<IEnumerable<OrphanageDataModel.Persons.Mother>> GetMothers(int Uid)
        {
            return await _userDBService.GetMothers(Uid);
        }

        [HttpGet]
        [Route("mothers/{uid}/{pageSize}/{pageNum}")]
        public async Task<IEnumerable<OrphanageDataModel.Persons.Mother>> GetMothers(int Uid, int pageSize, int pageNum)
        {
            return await _userDBService.GetMothers(Uid, pageSize, pageNum);
        }

        [HttpGet]
        [Route("mothers/count/{uid}/{pageSize}/{pageNum}")]
        public async Task<int> GetMothersCount(int Uid)
        {
            return await _userDBService.GetMothersCount(Uid);
        }

        [HttpGet]
        [Route("orphans/{uid}")]
        public async Task<IEnumerable<OrphanageDataModel.Persons.Orphan>> GetOrphans(int Uid)
        {
            return await _userDBService.GetOrphans(Uid);
        }

        [HttpGet]
        [Route("orphans/{uid}/{pageSize}/{pageNum}")]
        public async Task<IEnumerable<OrphanageDataModel.Persons.Orphan>> GetOrphans(int Uid, int pageSize, int pageNum)
        {
            return await _userDBService.GetOrphans(Uid, pageSize, pageNum);
        }

        [HttpGet]
        [Route("orphans/count/{uid}")]
        public async Task<int> GetOrphansCount(int Uid)
        {
            return await _userDBService.GetOrphansCount(Uid);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<OrphanageDataModel.Persons.User> GetUser(int id)
        {
            return await _userDBService.GetUser(id);
        }

        [HttpGet]
        [Route("{pageSize}/{pageNum}")]
        public async Task<IEnumerable<OrphanageDataModel.Persons.User>> GetUsers(int pageSize, int pageNum)
        {
            return await _userDBService.GetUsers(pageSize, pageNum);
        }

        [HttpGet]
        [Route("count")]
        public async Task<int> GetUsersCount()
        {
            return await _userDBService.GetUsersCount();
        }
    }
}