using OrphanageV3.Views.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls;
using Unity;

namespace OrphanageV3.Views.Main
{
    public partial class MainView : Telerik.WinControls.UI.RadForm
    {
        private ViewModel.Main.MainViewModel _mainViewModel = Program.Factory.Resolve<ViewModel.Main.MainViewModel>();

        public MainView()
        {
            InitializeComponent();
            TranslateControls();
            _mainViewModel.MainView = this;
        }

        private void TranslateControls()
        {
            mnuNew.Text = Properties.Resources.New;
            mnuNewOrphan.Text = Properties.Resources.OrphanEditViewTitle;
            mnuNewFamily.Text = Properties.Resources.Family;
            mnuShow.Text = Properties.Resources.Show;
            mnuShowOrphans.Text = Properties.Resources.Orphans;
            mnuShowUnBailedOrphans.Text = Properties.Resources.UnBaild;
            mnuShowBailedOrphans.Text = Properties.Resources.Baild;
            mnuShowALLOrphans.Text = Properties.Resources.All;
            mnuShowExcludedOrphans.Text = Properties.Resources.Excluded;
            mnuShowFamilies.Text = Properties.Resources.Families;
            mnuShowUnBailedFamilies.Text = Properties.Resources.UnBailedFamilies;
            mnuShowBailedFamilies.Text = Properties.Resources.BailedFamilies;
            mnuShowExcludedFamilies.Text = Properties.Resources.ExcludedFamilies;
            mnuTools.Text = Properties.Resources.Tools;
            mnuShowSetting.Text = Properties.Resources.Setting;
            mnuShowBails.Text = Properties.Resources.Bails;
            mnuNewBail.Text = Properties.Resources.Bail;
            mnuShowAllFamilies.Text = Properties.Resources.All;
            mnuShowGuarantors.Text = Properties.Resources.Guarantors;
            mnuNewGuarantor.Text = Properties.Resources.Guarantor;
            lblConnectionStatus.Text = Properties.Resources.Disconnected;
        }

        private void mnuNewOrphan_Click(object sender, EventArgs e)
        {
            _mainViewModel.ShowNewOrphanView();
        }

        private void mnuNewFamily_Click(object sender, EventArgs e)
        {
            _mainViewModel.ShowNewFamily();
        }

        private void mnuShowExcludedOrphans_Click(object sender, EventArgs e)
        {
            _mainViewModel.ShowExcludedOrphan();
        }

        private void mnuShowBailedOrphans_Click(object sender, EventArgs e)
        {
            _mainViewModel.ShowBailedOrphan();
        }

        private void mnuShowUnBailedOrphans_Click(object sender, EventArgs e)
        {
            _mainViewModel.ShowUnBailedOrphan();
        }

        private void mnuShowALLOrphans_Click(object sender, EventArgs e)
        {
            _mainViewModel.ShowOrphansView();
        }

        private void mnuShowUnBailedFamilies_Click(object sender, EventArgs e)
        {
            _mainViewModel.ShowUnBailedFamiliesView();
        }

        private void mnuShowBailedFamilies_Click(object sender, EventArgs e)
        {
            _mainViewModel.ShowBailedFamiliesView();
        }

        private void mnuShowAllFamilies_Click(object sender, EventArgs e)
        {
            _mainViewModel.ShowFamiliesView();
        }

        private void mnuShowExcludedFams_Click(object sender, EventArgs e)
        {
            _mainViewModel.ShowExcludedFamilies();
        }

        private void mnuShowFathers_Click(object sender, EventArgs e)
        {
            _mainViewModel.ShowFathersView();
        }

        private void mnuShowMothers_Click(object sender, EventArgs e)
        {
            _mainViewModel.ShowMothersView();
        }

        private void mnuShowBonds_Click(object sender, EventArgs e)
        {
            _mainViewModel.ShowCaregiversView();
        }

        private void mnuShowSetting_Click(object sender, EventArgs e)
        {
            SettingView settingView = new SettingView();
            settingView.ShowDialog();
        }

        private async void timer1_Tick(object sender, EventArgs e)
        {
            var connectionValue = await CheckConnection();
            lblConnectionStatus.Text = connectionValue ? Properties.Resources.Connected : Properties.Resources.Disconnected;
            lblConnectionStatus.ForeColor = connectionValue ? Color.Green : Color.Red;
        }

        private async Task<bool> CheckConnection()
        {
            var url = Properties.Settings.Default.OrphanageServiceURL;

            if (!url.EndsWith("/")) url += "/";

            url += "api/info/version";

            var client_ = new System.Net.Http.HttpClient();
            try
            {
                using (var request_ = new System.Net.Http.HttpRequestMessage())
                {
                    request_.Method = new System.Net.Http.HttpMethod("GET");
                    request_.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    request_.RequestUri = new System.Uri(url, System.UriKind.RelativeOrAbsolute);

                    var response_ = await client_.SendAsync(request_, System.Net.Http.HttpCompletionOption.ResponseHeadersRead);
                    try
                    {
                        var status_ = ((int)response_.StatusCode).ToString();
                        if (status_ == "200")
                        {
                            return true;
                        }
                    }
                    catch { return false; }
                    finally
                    {
                        if (response_ != null)
                            response_.Dispose();
                    }
                }
            }
            catch (System.Exception)
            {
                return false;
            }
            finally
            {
                if (client_ != null)
                    client_.Dispose();
            }
            return false;
        }

        private void mnuShowBails_Click(object sender, EventArgs e)
        {
            _mainViewModel.ShowBailsView();
        }

        private void mnuNewBail_Click(object sender, EventArgs e)
        {
            _mainViewModel.ShowNewBail();
        }

        private void mnuShowGuarantors_Click(object sender, EventArgs e)
        {
            _mainViewModel.ShowGuarantors();
        }

        private void mnuNewGuarantor_Click(object sender, EventArgs e)
        {
            _mainViewModel.ShowNewGuarantor();
        }
    }
}