using OrphanageV3.Services;
using System.Linq;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace OrphanageV3.ViewModel.Main
{
    public class MainViewModel
    {
        private RadForm _MainView;
        private IApiClient _apiClient;

        public RadForm MainView
        {
            get { return _MainView; }
            set { _MainView = value; }
        }

        public MainViewModel(IApiClient apiClient)
        {
            _apiClient = apiClient;
            Services.ApiClientProvider.AccessTokenExpired += ApiClientProvider_MustLogin;
            Services.ApiClientProvider.MustLogin += ApiClientProvider_MustLogin;
        }

        private void ApiClientProvider_MustLogin(object sender, System.EventArgs e)
        {
            _MainView.Invoke(new MethodInvoker(() => { ShowLoginDialog(); }));
        }

        public void UpdateApiClient(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public void ShowNewOrphanView()
        {
            ShowView(new Views.Orphan.AddOrphanView());
        }

        public void ShowNewFamily()
        {
            ShowView(new Views.Family.AddFamilyView());
        }

        public void ShowOrphansView()
        {
            ShowView(new Views.Orphan.OrphansView());
        }

        public async void ShowExcludedOrphan()
        {
            var orphanList = await _apiClient.OrphansController_GetExcludedAsync();
            ShowView(new Views.Orphan.OrphansView(orphanList));
        }

        public async void ShowBailedOrphan()
        {
            var orphanCount = await _apiClient.OrphansController_GetOrphansCountAsync();
            var orphans = await _apiClient.OrphansController_GetAllAsync(orphanCount, 0);
            var bailedOrphans = orphans.Where(o => o.IsBailed || o.Family.IsBailed || o.BailId.HasValue || o.Family.BailId.HasValue).Select(o => o.Id)
                .ToList();
            ShowView(new Views.Orphan.OrphansView(bailedOrphans));
        }

        public async void ShowUnBailedOrphan()
        {
            var orphanCount = await _apiClient.OrphansController_GetOrphansCountAsync();
            var orphans = await _apiClient.OrphansController_GetAllAsync(orphanCount, 0);
            var bailedOrphans = orphans.Where(o => o.IsBailed == false && o.Family.IsBailed == false
                            && !o.BailId.HasValue && !o.Family.BailId.HasValue).Select(o => o.Id).ToList();
            ShowView(new Views.Orphan.OrphansView(bailedOrphans));
        }

        public void ShowFamiliesView()
        {
            ShowView(new Views.Family.FimiliesView());
        }

        public async void ShowExcludedFamilies()
        {
            var familiesList = await _apiClient.FamiliesController_GetExcludedAsync();
            ShowView(new Views.Family.FimiliesView(familiesList));
        }

        public async void ShowBailedFamiliesView()
        {
            var familiesCount = await _apiClient.FamiliesController_GetFamiliesCountAsync();
            var allFamilies = await _apiClient.FamiliesController_GetAllAsync(familiesCount, 0);
            var returnedFamilies = allFamilies.Where(f => f.IsBailed == true || f.BailId.HasValue).Select(f => f.Id).ToList();

            ShowView(new Views.Family.FimiliesView(returnedFamilies));
        }

        public async void ShowUnBailedFamiliesView()
        {
            var familiesCount = await _apiClient.FamiliesController_GetFamiliesCountAsync();
            var allFamilies = await _apiClient.FamiliesController_GetAllAsync(familiesCount, 0);
            var returnedFamilies = allFamilies.Where(f => f.IsBailed == false && !f.BailId.HasValue).Select(f => f.Id).ToList();

            ShowView(new Views.Family.FimiliesView(returnedFamilies));
        }

        public void ShowFathersView()
        {
            ShowView(new Views.Father.FathersView());
        }

        public void ShowMothersView()
        {
            ShowView(new Views.Mother.MothersView());
        }

        public void ShowCaregiversView()
        {
            ShowView(new Views.Caregiver.CaregiversView());
        }

        public void ShowBailsView()
        {
            ShowView(new Views.Bail.BailsView());
        }

        public void ShowNewBail()
        {
            ShowView(new Views.Bail.BailEditView());
        }

        public void ShowGuarantors()
        {
            ShowView(new Views.Guarantor.GuarantorsView());
        }

        public void ShowNewGuarantor()
        {
            ShowView(new Views.Guarantor.GuarantorEditView());
        }

        public void ShowAccounts()
        {
            ShowView(new Views.Account.AccountsView());
        }

        public void ShowNewAccount()
        {
            ShowView(new Views.Account.AccountEditView());
        }

        public void ShowOrphanEditView(int OrphanId)
        {
            ShowView(new Views.Orphan.OrphanEditView(OrphanId));
        }

        public void ShowBailEditView(int BailId)
        {
            ShowView(new Views.Bail.BailEditView(BailId));
        }

        public void ShowCaregiverEditView(int CaregiverId)
        {
            ShowView(new Views.Caregiver.CaregiverEditView(CaregiverId));
        }

        public void ShowFamilyEditView(int FamilyId)
        {
            ShowView(new Views.Family.FamilyEditView(FamilyId));
        }

        public void ShowGuarantorEditView(int GuarantorId)
        {
            ShowView(new Views.Guarantor.GuarantorEditView(GuarantorId));
        }

        public void ShowLoginDialog()
        {
            var frm = new Views.Login.LoginView();
            //frm.MdiParent = _MainView;
            //frm.ThemeClassName = _MainView.ThemeClassName;
            frm.ThemeName = _MainView.ThemeName;

            var overlayForm = new RadForm();
            overlayForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            overlayForm.ShowInTaskbar = false;
            overlayForm.Width = _MainView.Width;
            overlayForm.Height = _MainView.Height;
            overlayForm.Top = _MainView.Top;
            overlayForm.Left = _MainView.Left;
            overlayForm.StartPosition = _MainView.StartPosition;
            // Set the opacity to 75%.
            overlayForm.Opacity = .75;
            overlayForm.MdiParent = _MainView;
            overlayForm.Show();
            frm.ShowDialog();
            overlayForm.Close();
            overlayForm.Dispose();
        }

        public void CloseAllwindows()
        {
            if (this.MainView.MdiChildren != null && this.MainView.MdiChildren.Length > 0)
            {
                foreach (var frm in this.MainView.MdiChildren)
                {
                    frm.Close();
                }
            }
        }

        private void ShowView(RadForm frm)
        {
            frm.MdiParent = _MainView;
            frm.ThemeClassName = _MainView.ThemeClassName;
            frm.ThemeName = _MainView.ThemeName;
            frm.Show();
        }
    }
}