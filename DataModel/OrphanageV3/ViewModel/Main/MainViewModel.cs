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

        public void ShowOrphansView()
        {
            ShowView(new Views.Orphan.OrphansView());
        }

        private void ShowView(RadForm frm)
        {
            frm.MdiParent = _MainView;
            frm.Show();
        }
    }
}