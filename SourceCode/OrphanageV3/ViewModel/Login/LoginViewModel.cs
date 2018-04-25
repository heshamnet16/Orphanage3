using OrphanageV3.Services;
using OrphanageV3.ViewModel.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrphanageV3.ViewModel.Login
{
    public class LoginViewModel
    {
        private readonly IApiClient _apiClient;
        private readonly MainViewModel _mainViewModel;

        public LoginViewModel(IApiClient apiClient, MainViewModel mainViewModel)
        {
            _apiClient = apiClient;
            _mainViewModel = mainViewModel;
        }

        public async Task<bool> Login(string UserName, string Password)
        {
            if (UserName == null || UserName.Length == 0)
            {
                MessageBox.Show(Properties.Resources.ErrorMessageUserName, System.AppDomain.CurrentDomain.FriendlyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (Password == null || Password.Length == 0)
            {
                MessageBox.Show(Properties.Resources.ErrorMessagePassword, System.AppDomain.CurrentDomain.FriendlyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            try
            {
                int previousUserId = -1;
                previousUserId = Program.CurrentUser == null ? -1 : Program.CurrentUser.Id;
                await ApiClientProvider.SetToken(UserName, Password);
                Program.CurrentUser = await _apiClient.UsersController_AuthenticateAsync(UserName, Password);
                if (ApiClientProvider.AccessToken != null && Program.CurrentUser != null)
                {
                    if (previousUserId > 0 && previousUserId != Program.CurrentUser.Id)
                    {
                        _mainViewModel.CloseAllwindows();
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Program.HandleApiExceptions(ex);
                return false;
            }
        }
    }
}