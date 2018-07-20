using OrphanageV3.Extensions;
using OrphanageV3.Views.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using Unity;

namespace OrphanageV3.Views.Main
{
    public partial class MainView : Telerik.WinControls.UI.RadForm
    {
        private ViewModel.Main.MainViewModel _mainViewModel = Program.Factory.Resolve<ViewModel.Main.MainViewModel>();
        private IList<SecurityProtocolType> _ProtocolsList = new List<SecurityProtocolType>();

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
            mnuShowAccounts.Text = Properties.Resources.Accounts;
            mnuShowDownload.Text = Properties.Resources.ShowDownloads;
            mnuCustomSQL.Text = Properties.Resources.RunCustomSQL;
            lblRemainingTime.Text = Properties.Resources.RemainingTime.getDobblePunkt();
            lblRemainingTime.TextAlignment = ContentAlignment.MiddleLeft;
            lblRemainingTimeValue.TextAlignment = ContentAlignment.MiddleRight;
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
            disableEnableMenus(RadMenu1.Items, connectionValue);
            lblRemainingTimeValue.Text = Services.ApiClientTokenProvider.RemainTime.ToString("g").Trim();
            var hasIview = hasIView();
            btnTrabslateToWord.Enabled = hasIview;
            btnTranslateToExcel.Enabled = hasIview;
        }

        private bool hasIView()
        {
            var view = _mainViewModel.getActiveView();
            return view != null;
        }

        private void disableEnableMenus(RadItemOwnerCollection radItemOwnerCollection, bool value)
        {
            if (radItemOwnerCollection != null && radItemOwnerCollection.Count > 0)
            {
                foreach (object itmObjct in radItemOwnerCollection)
                {
                    try
                    {
                        RadMenuItem itm = (RadMenuItem)itmObjct;
                        disableEnableMenus(itm.Items, value);
                        if (itm is RadMenuItem)
                        {
                            RadMenuItem radMenuItem = (RadMenuItem)itm;
                            if (radMenuItem.Name != "mnuTools" && radMenuItem.Name != "mnuShowSetting")
                            {
                                radMenuItem.Enabled = value;
                            }
                        }
                    }
                    catch { }
                }
            }
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
            catch(HttpRequestException requestException)
            {
                if(requestException.InnerException is WebException)
                {
                    WebException webException = (WebException) requestException.InnerException;
                    if (webException.HResult == -2146233079)
                    {
                        //SecurityProtocol is false
                        var protocols = Enum.GetValues(typeof(SecurityProtocolType));
                        foreach (SecurityProtocolType pro in protocols)
                        {
                            if (pro != ServicePointManager.SecurityProtocol && !_ProtocolsList.Contains(pro))
                            {
                                ServicePointManager.SecurityProtocol = pro;
                                _ProtocolsList.Add(pro);
                                break;
                            }
                        }
                    }
                }
                return false;
            }
            catch (System.Exception ex)
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

        private void mnuShowAccounts_Click(object sender, EventArgs e)
        {
            _mainViewModel.ShowAccounts();
        }

        private void mnuNewAccount_Click(object sender, EventArgs e)
        {
            _mainViewModel.ShowNewAccount();
        }

        private void MainView_Load(object sender, EventArgs e)
        {
            _mainViewModel.ShowLoginDialog();
        }

        private void btnTranslateToExcel_Click(object sender, EventArgs e)
        {
            _mainViewModel.ConvertToExcel();
        }

        private void mnuShowDownload_Click(object sender, EventArgs e)
        {
            _mainViewModel.ShowDownloadView();
        }

        private void mnuCustomSQL_Click(object sender, EventArgs e)
        {
            _mainViewModel.ShowCustomSQLView();
        }
    }
}