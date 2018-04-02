using OrphanageV3.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.WinControls.UI;

namespace OrphanageV3.ViewModel.Main
{
    public class MainViewModel
    {
        private RadForm _MainView;
        private readonly IApiClient _apiClient;

        public RadForm MainView
        {
            get { return _MainView; }
            set { _MainView = value; }
        }

        public MainViewModel(IApiClient apiClient)
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

        private void ShowView(RadForm frm)
        {
            frm.MdiParent = _MainView;
            frm.ThemeClassName = _MainView.ThemeClassName;
            frm.ThemeName = _MainView.ThemeName;
            frm.Show();
        }
    }
}