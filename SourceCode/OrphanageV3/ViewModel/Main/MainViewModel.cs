using OrphanageV3.Services;
using OrphanageV3.ViewModel.Tools;
using OrphanageV3.Views.Helper.Interfaces;
using OrphanageV3.Views.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace OrphanageV3.ViewModel.Main
{
    public class MainViewModel
    {
        private RadForm _MainView;
        private IApiClient _apiClient;
        private readonly IRadGridHelper _radHelper;
        private readonly DownloadViewModel _downloadViewModel;

        public RadForm MainView
        {
            get { return _MainView; }
            set { _MainView = value; }
        }

        public MainViewModel(IApiClient apiClient, IRadGridHelper radHelper, DownloadViewModel downloadViewModel)
        {
            _apiClient = apiClient;
            _radHelper = radHelper;
            _downloadViewModel = downloadViewModel;
            Services.ApiClientTokenProvider.AccessTokenExpired += ApiClientProvider_MustLogin;
            Services.ApiClientTokenProvider.MustLogin += ApiClientProvider_MustLogin;
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
            var orphanList = await _apiClient.Orphans_GetExcludedAsync();
            ShowView(new Views.Orphan.OrphansView(orphanList));
        }

        public async void ShowBailedOrphan()
        {
            var orphanCount = await _apiClient.Orphans_GetOrphansCountAsync();
            var orphans = await _apiClient.Orphans_GetAllAsync(orphanCount, 0);
            var bailedOrphans = orphans.Where(o => o.IsBailed || o.Family.IsBailed || o.BailId.HasValue || o.Family.BailId.HasValue).Select(o => o.Id)
                .ToList();
            ShowView(new Views.Orphan.OrphansView(bailedOrphans));
        }

        public async void ShowUnBailedOrphan()
        {
            var orphanCount = await _apiClient.Orphans_GetOrphansCountAsync();
            var orphans = await _apiClient.Orphans_GetAllAsync(orphanCount, 0);
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
            var familiesList = await _apiClient.Families_GetExcludedAsync();
            ShowView(new Views.Family.FimiliesView(familiesList));
        }

        public async void ShowBailedFamiliesView()
        {
            var familiesCount = await _apiClient.Families_GetFamiliesCountAsync();
            var allFamilies = await _apiClient.Families_GetAllAsync(familiesCount, 0);
            var returnedFamilies = allFamilies.Where(f => f.IsBailed == true || f.BailId.HasValue).Select(f => f.Id).ToList();

            ShowView(new Views.Family.FimiliesView(returnedFamilies));
        }

        public async void ShowUnBailedFamiliesView()
        {
            var familiesCount = await _apiClient.Families_GetFamiliesCountAsync();
            var allFamilies = await _apiClient.Families_GetAllAsync(familiesCount, 0);
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

        public void ShowDownloadView()
        {
            ShowView(new Views.Tools.DownloadView());
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

        public void ConvertToExcel()
        {
            var view = getActiveView();
            _radHelper.GridView = view.GetOrphanageGridView().GridView;
            var data = _radHelper.GetSelectedData(view.GetOrphanageGridView().SelectedRows);
            var ret = _apiClient.Excel_CreateXlsxAsync(new OrphanageDataModel.RegularData.DTOs.ExportData() { Data = data });
            var downloadDataModel = new DownloadDataModel()
            {
                DataType = FileExtentionEnum.xlsx,
                Name = view.GetTitle() + " (Excel)"
            };
            _downloadViewModel.Add(downloadDataModel, ret);
        }

        public IView getActiveView()
        {
            var activeForm = MainView.ActiveMdiChild;
            if (activeForm is IView)
            {
                return (IView)MainView.ActiveMdiChild;
            }
            else
            {
                return null;
            }
        }

        public IList<IView> getViews()

        {
            var views = new List<IView>();
            foreach (var child in MainView.MdiChildren)
            {
                if (child is IView)
                {
                    views.Add((IView)child);
                }
            }
            return views;
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