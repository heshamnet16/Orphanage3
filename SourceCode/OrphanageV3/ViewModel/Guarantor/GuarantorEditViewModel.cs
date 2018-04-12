﻿using OrphanageV3.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrphanageV3.ViewModel.Guarantor
{
    public class GuarantorEditViewModel
    {
        private readonly IApiClient _apiClient;
        private readonly Account.AccountsViewModel _accountsViewModel;
        private OrphanageDataModel.Persons.Guarantor _CurrentGuarantor = null;

        public IEnumerable<Account.AccountModel> Accounts = null;

        public event EventHandler AccountsLoaded;

        public GuarantorEditViewModel(IApiClient apiClient, Account.AccountsViewModel accountsViewModel)
        {
            _apiClient = apiClient;
            _accountsViewModel = accountsViewModel;
            _accountsViewModel.DataLoaded += AccountsDataLoaded;
            _accountsViewModel.LoadAccounts();
        }

        private void AccountsDataLoaded(object sender, EventArgs e)
        {
            Accounts = _accountsViewModel.Accounts;
            AccountsLoaded?.Invoke(null, null);
        }

        public async Task<bool> Save(OrphanageDataModel.Persons.Guarantor guarantor)
        {
            await _apiClient.GuarantorsController_PutAsync(guarantor);
            return true;
        }

        public async Task<OrphanageDataModel.Persons.Guarantor> getGuarantor(int Bid)
        {
            var returnedGuarantor = await _apiClient.GuarantorsController_GetAsync(Bid);
            _CurrentGuarantor = returnedGuarantor;
            return returnedGuarantor;
        }

        public async Task<bool> Save()
        {
            return await Save(_CurrentGuarantor);
        }

        public async Task<OrphanageDataModel.Persons.Guarantor> Add(OrphanageDataModel.Persons.Guarantor guarantor)
        {
            try
            {
                guarantor.UserId = Program.CurrentUser.Id;
                var retBail = (OrphanageDataModel.Persons.Guarantor)await _apiClient.GuarantorsController_PostAsync(guarantor);
            }
            catch (ApiClientException apiEx)
            {
                //Created
                if (apiEx.StatusCode == "201")
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<OrphanageDataModel.Persons.Guarantor>(apiEx.Response) ?? null;
                return null;
            }
            return null;
        }

        public OrphanageDataModel.FinancialData.Account GetSourceAccount(int AccountId) => _accountsViewModel.GetSourceAccount(AccountId);
    }
}