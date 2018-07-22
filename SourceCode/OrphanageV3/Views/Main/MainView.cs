using OrphanageV3.Extensions;
using OrphanageV3.Views.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Windows.Forms;
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

            this.DoubleBuffered = true;
            foreach (System.Windows.Forms.Control c in this.Controls)
                if (c is System.Windows.Forms.MdiClient)
                {
                    System.Windows.Forms.MdiClient ctl = (System.Windows.Forms.MdiClient)c;
                    ctl.Paint += this.DrawBackground;
                    ctl.Click += this.ClickBackground;
                }
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

            mnuAppAqua.Text = Properties.Resources.ThemeAqua;
            mnuAppBreaze.Text = Properties.Resources.ThemeBreeze;
            mnuAppDefault.Text = Properties.Resources.ThemeDefault;
            mnuAppDesert.Text = Properties.Resources.ThemeDesert;
            mnuAppHighCont.Text = Properties.Resources.ThemeHightConstrast;
            mnuAppMetro.Text = Properties.Resources.ThemeMetro;
            mnuAppMetroBlue.Text = Properties.Resources.ThemeMetroBlue;
            mnuAppMetroTouch.Text = Properties.Resources.ThemeMetroBlueTouch;
            mnuAppOffice2007Black.Text = Properties.Resources.ThemeOffice2007Black;
            mnuAppOffice2007Silver.Text = Properties.Resources.ThemeOffice2007Silver;
            mnuAppOffice2010Black.Text = Properties.Resources.ThemeOffice2010Black;
            mnuAppOffice2010Blue.Text = Properties.Resources.ThemeOffice2010Blue;
            mnuAppOffice2010Silver.Text = Properties.Resources.ThemeOffice2010Silver;
            mnuAppOffice2013Dark.Text = Properties.Resources.ThemeOffice2013Dark;
            mnuAppOffice2013Light.Text = Properties.Resources.ThemeOffice2013Light;
            mnuAppVSDark.Text = Properties.Resources.ThemeVSDark;
            mnuAppVSLight.Text = Properties.Resources.ThemeVSLight;
            mnuAppWin7.Text = Properties.Resources.ThemeWindows7;
            mnuAppWin8.Text = Properties.Resources.ThemeWindows8;
            mnuApperance.Text = Properties.Resources.Appearance;

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
            catch (HttpRequestException requestException)
            {
                if (requestException.InnerException is WebException)
                {
                    WebException webException = (WebException)requestException.InnerException;
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
            LoadLastTheme();
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

        #region "Appearance"

        private void ClickBackground(object sender, EventArgs e)
        {

        }

        private void DrawBackground(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            Color LowColor = Color.Black;
            Color HightColor = Color.LightGreen;
            float angel = 90.0f;
            System.Windows.Forms.MdiClient ctl = (System.Windows.Forms.MdiClient)sender;
            MethodInfo mi = typeof(MdiClient).GetMethod("SetStyle", BindingFlags.Instance | BindingFlags.NonPublic);
            mi.Invoke(ctl, new object[] { ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true });
            Graphics g = e.Graphics;
            Bitmap img = (Bitmap)(Properties.Resources.Logo);
            int ImH = 150;
            int ImW = 420;
            Point center = new Point(Convert.ToInt32(((this.Width) / 2) - (ImW / 2)), Convert.ToInt32(((this.Height) / 2) - ImH));
            Color col = img.GetPixel(10, 10);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            img.MakeTransparent(Color.Red);
            //g.Clear(Color.Black);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            LinearGradientBrush Lbrush = new LinearGradientBrush(new Rectangle(0, 0, this.Width, this.Height), HightColor, LowColor, angel, true);
            g.FillRectangle(Lbrush, new Rectangle(0, 0, this.Width, this.Height));
            g.DrawImage(img, new Rectangle(center, new Size(ImW, ImH)));
            angel += 5;
            if (angel >= 360)
                angel = 0;
            if (System.DateTime.Now.Hour >= 2 && System.DateTime.Now.Hour <= 3)
            {
                LowColor = Color.Black;
                HightColor = Color.White;
            }
            else if (System.DateTime.Now.Hour >= 4 && System.DateTime.Now.Hour <= 7)
            {
                LowColor = Color.Black;
                HightColor = Color.LightYellow;
            }
            else if (System.DateTime.Now.Hour >= 8 && DateTime.Now.Hour <= 11)
            {
                LowColor = Color.GreenYellow;
                HightColor = Color.LightYellow;
            }
            else if (System.DateTime.Now.Hour >= 12 && DateTime.Now.Hour <= 15)
            {
                LowColor = Color.OrangeRed;
                HightColor = Color.LightYellow;
            }
            else if (System.DateTime.Now.Hour >= 16 && DateTime.Now.Hour <= 17)
            {
                LowColor = Color.RosyBrown;
                HightColor = Color.DarkOrange;
            }
            else if (System.DateTime.Now.Hour >= 18 && DateTime.Now.Hour <= 19)
            {
                LowColor = Color.Black;
                HightColor = Color.Brown;
            }
            else if (System.DateTime.Now.Hour >= 20 && DateTime.Now.Hour <= 23)
            {
                LowColor = Color.Black;
                HightColor = Color.Black;
            }

            //g.Dispose();
            img.Dispose();
            GC.Collect();
            //ctl.Dispose();
        }

        private void themeChange_Click(object sender, EventArgs e)
        {

            var senderMeunThemeItem = (RadMenuItem)sender;
            Telerik.WinControls.ThemeResolutionService.ClearTheme(ThemeResolutionService.ApplicationThemeName);
            if (senderMeunThemeItem == mnuAppDesert)
            {
                Telerik.WinControls.ThemeResolutionService.ApplicationThemeName = desertTheme1.ThemeName;
            }
            else if (senderMeunThemeItem == mnuAppAqua)
            {
                Telerik.WinControls.ThemeResolutionService.ApplicationThemeName = aquaTheme1.ThemeName;
            }
            else if (senderMeunThemeItem == mnuAppBreaze)
            {
                Telerik.WinControls.ThemeResolutionService.ApplicationThemeName = breezeTheme1.ThemeName;
            }
            else if (senderMeunThemeItem == mnuAppDefault)
            {
                Telerik.WinControls.ThemeResolutionService.ApplicationThemeName = null;
            }
            else if (senderMeunThemeItem == mnuAppHighCont)
            {
                Telerik.WinControls.ThemeResolutionService.ApplicationThemeName = highContrastBlackTheme1.ThemeName;
            }
            else if (senderMeunThemeItem == mnuAppMetro)
            {
                Telerik.WinControls.ThemeResolutionService.ApplicationThemeName = telerikMetroTheme1.ThemeName;
            }
            else if (senderMeunThemeItem == mnuAppMetroBlue)
            {
                Telerik.WinControls.ThemeResolutionService.ApplicationThemeName = telerikMetroBlueTheme1.ThemeName;
            }
            else if (senderMeunThemeItem == mnuAppMetroTouch)
            {
                Telerik.WinControls.ThemeResolutionService.ApplicationThemeName = telerikMetroTouchTheme1.ThemeName;
            }
            else if (senderMeunThemeItem == mnuAppOffice2007Black)
            {
                Telerik.WinControls.ThemeResolutionService.ApplicationThemeName = office2007BlackTheme1.ThemeName;
            }
            else if (senderMeunThemeItem == mnuAppOffice2007Silver)
            {
                Telerik.WinControls.ThemeResolutionService.ApplicationThemeName = office2007SilverTheme1.ThemeName;
            }
            else if (senderMeunThemeItem == mnuAppOffice2010Black)
            {
                Telerik.WinControls.ThemeResolutionService.ApplicationThemeName = office2010BlackTheme1.ThemeName;
            }
            else if (senderMeunThemeItem == mnuAppOffice2010Blue)
            {
                Telerik.WinControls.ThemeResolutionService.ApplicationThemeName = office2010BlueTheme1.ThemeName;
            }
            else if (senderMeunThemeItem == mnuAppOffice2010Silver)
            {
                Telerik.WinControls.ThemeResolutionService.ApplicationThemeName = office2010SilverTheme1.ThemeName;
            }
            else if (senderMeunThemeItem == mnuAppOffice2013Dark)
            {
                Telerik.WinControls.ThemeResolutionService.ApplicationThemeName = office2013DarkTheme1.ThemeName;
            }
            else if (senderMeunThemeItem == mnuAppOffice2013Light)
            {
                Telerik.WinControls.ThemeResolutionService.ApplicationThemeName = office2013LightTheme1.ThemeName;
            }
            else if (senderMeunThemeItem == mnuAppVSDark)
            {
                Telerik.WinControls.ThemeResolutionService.ApplicationThemeName = visualStudio2012DarkTheme1.ThemeName;
            }
            else if (senderMeunThemeItem == mnuAppVSLight)
            {
                Telerik.WinControls.ThemeResolutionService.ApplicationThemeName = visualStudio2012LightTheme1.ThemeName;
            }
            else if (senderMeunThemeItem == mnuAppWin7)
            {
                Telerik.WinControls.ThemeResolutionService.ApplicationThemeName = windows7Theme1.ThemeName;
            }
            else if (senderMeunThemeItem == mnuAppWin8)
            {
                Telerik.WinControls.ThemeResolutionService.ApplicationThemeName = windows8Theme1.ThemeName;
            }
            Properties.Settings.Default.ThemeName = ThemeResolutionService.ApplicationThemeName;
            Properties.Settings.Default.Save();
            foreach (var mnuItem in mnuApperance.Items)
            {
                if (mnuItem is RadMenuItem)
                    ((RadMenuItem)mnuItem).IsChecked = false;
            }
            senderMeunThemeItem.IsChecked = true;
        }

        private void LoadLastTheme()
        {
            Telerik.WinControls.ThemeResolutionService.AllowAnimations = false;
            foreach (var mnuItem in mnuApperance.Items)
            {
                if (mnuItem is RadMenuItem)
                    ((RadMenuItem)mnuItem).IsChecked = false;
            }
            Telerik.WinControls.ThemeResolutionService.ClearTheme(Properties.Settings.Default.ThemeName);
            Telerik.WinControls.ThemeResolutionService.ApplicationThemeName = Properties.Settings.Default.ThemeName;
            if (Properties.Settings.Default.ThemeName == desertTheme1.ThemeName)
            {
                mnuAppDesert.IsChecked = true;
            }
            else if (Properties.Settings.Default.ThemeName == aquaTheme1.ThemeName)
            {
                mnuAppAqua.IsChecked = true;
            }
            else if (Properties.Settings.Default.ThemeName == breezeTheme1.ThemeName)
            {
                mnuAppBreaze.IsChecked = true;
            }
            else if (Properties.Settings.Default.ThemeName == null)
            {
                mnuAppDefault.IsChecked = true;
            }
            else if (Properties.Settings.Default.ThemeName == highContrastBlackTheme1.ThemeName)
            {
                mnuAppHighCont.IsChecked = true;
            }
            else if (Properties.Settings.Default.ThemeName == telerikMetroTheme1.ThemeName)
            {
                mnuAppMetro.IsChecked = true;
            }
            else if (Properties.Settings.Default.ThemeName == telerikMetroBlueTheme1.ThemeName)
            {
                mnuAppMetroBlue.IsChecked = true;
            }
            else if (Properties.Settings.Default.ThemeName == telerikMetroTouchTheme1.ThemeName)
            {
                mnuAppMetroTouch.IsChecked = true;
            }
            else if (Properties.Settings.Default.ThemeName == office2007BlackTheme1.ThemeName)
            {
                mnuAppOffice2007Black.IsChecked = true;
            }
            else if (Properties.Settings.Default.ThemeName == office2007SilverTheme1.ThemeName)
            {
                mnuAppOffice2007Silver.IsChecked = true;
            }
            else if (Properties.Settings.Default.ThemeName == office2010BlackTheme1.ThemeName)
            {
                mnuAppOffice2010Black.IsChecked = true;
            }
            else if (Properties.Settings.Default.ThemeName == office2010BlueTheme1.ThemeName)
            {
                mnuAppOffice2010Blue.IsChecked = true;
            }
            else if (Properties.Settings.Default.ThemeName == office2010SilverTheme1.ThemeName)
            {
                mnuAppOffice2010Silver.IsChecked = true;
            }
            else if (Properties.Settings.Default.ThemeName == office2013DarkTheme1.ThemeName)
            {
                mnuAppOffice2013Dark.IsChecked = true;
            }
            else if (Properties.Settings.Default.ThemeName == office2013LightTheme1.ThemeName)
            {
                mnuAppOffice2013Light.IsChecked = true;
            }
            else if (Properties.Settings.Default.ThemeName == visualStudio2012DarkTheme1.ThemeName)
            {
                mnuAppVSDark.IsChecked = true;
            }
            else if (Properties.Settings.Default.ThemeName == visualStudio2012LightTheme1.ThemeName)
            {
                mnuAppVSLight.IsChecked = true;
            }
            else if (Properties.Settings.Default.ThemeName == windows7Theme1.ThemeName)
            {
                mnuAppWin7.IsChecked = true;
            }
            else if (Properties.Settings.Default.ThemeName == windows8Theme1.ThemeName)
            {
                mnuAppWin8.IsChecked = true;
            }
            Telerik.WinControls.ThemeResolutionService.AllowAnimations = false;
        }

        #endregion
    }
}