using OrphanageV3.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using OrphanageV3.Services.Interfaces;
using Unity;
namespace OrphanageV3.Views.Summary
{
    public partial class SummaryView : Telerik.WinControls.UI.RadForm
    {
        private readonly IApiClient _apiClient;
        private readonly ITranslateService _translateService;
        private IList<string> translatedColumns = new List<string>();
        private IList<string> ColumnsToDelete = new List<string>();
        int i = 0;
        public SummaryView()
        {
            _apiClient = Program.Factory.Resolve<IApiClient>();
            _translateService = new TranslateService();
            InitializeComponent();
        }

        private void SummaryView_Load(object sender, EventArgs e)
        {

        }

        private void SummaryView_Click(object sender, EventArgs e)
        {
            var orphansCount = _apiClient.OrphansController_GetOrphansCountAsync().Result;
            if (i * 2500 > orphansCount) i = 0;
            var orphansTask = _apiClient.OrphansController_GetAllAsync(2500, i++);
            gridOrphans.GridView.DataSource = orphansTask.Result;
        }

        private void gridOrphans_CreateRow(object sender, GridViewCreateRowEventArgs e)
        {

        }

        private void gridOrphans_CreateCell(object sender, GridViewCreateCellEventArgs e)
        {

        }

        private void gridOrphans_ColumnChooserCreated(object sender, ColumnChooserCreatedEventArgs e)
        {

        }

        private void gridOrphans_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {

        }
    }
}
