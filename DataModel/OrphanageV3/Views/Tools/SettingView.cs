using OrphanageV3.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;

namespace OrphanageV3.Views.Tools
{
    public partial class SettingView : Telerik.WinControls.UI.RadForm
    {
        public SettingView()
        {
            InitializeComponent();
            CreateCircleLabel();
            TranslateControls();
            LoadSettings();
            lblStatusCircle.BackColor = Color.Gray;
        }

        private void CreateCircleLabel()
        {
            lblStatusCircle.Width = lblStatusCircle.Height;
            var path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddEllipse(0, 0, lblStatusCircle.Width, lblStatusCircle.Height);

            this.lblStatusCircle.Region = new Region(path);
        }

        private void TranslateControls()
        {
            pnlConnection.HeaderText = Properties.Resources.Connection;
            lblBaseUrl.Text = Properties.Resources.RemoteAddress.getDobblePunkt();
            lblStatus.Text = Properties.Resources.Status.getDobblePunkt();
            lblVersion.Text = Properties.Resources.Version.getDobblePunkt();
            btnCheck.Text = Properties.Resources.CheckConnection;
        }

        private async void btnCheck_Click(object sender, EventArgs e)
        {
            startStopWaiting(true);
            var url = getBaseUrl(txtBaseUrl.Text);

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
                        var headers_ = System.Linq.Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        foreach (var item_ in response_.Content.Headers)
                            headers_[item_.Key] = item_.Value;

                        var status_ = ((int)response_.StatusCode).ToString();
                        if (status_ == "200")
                        {
                            var responseData_ = await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            string result_;
                            try
                            {
                                result_ = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(responseData_);
                                SetConnectionResult(true, result_);
                            }
                            catch (System.Exception)
                            {
                                SetConnectionResult(false, "");
                            }
                        }
                        else
                        if (status_ != "200" && status_ != "204")
                        {
                            SetConnectionResult(false, "");
                        }
                    }
                    finally
                    {
                        if (response_ != null)
                            response_.Dispose();
                    }
                }
            }
            catch (System.Exception)
            {
                SetConnectionResult(false, "");
            }
            finally
            {
                if (client_ != null)
                    client_.Dispose();
                startStopWaiting(false);
            }
        }

        private void startStopWaiting(bool value)
        {
            if (value)
            {
                radWaitingBar1.StartWaiting();
                radWaitingBar1.Visible = true;
                lblStatusCircle.BackColor = Color.Gray;
            }
            else
            {
                radWaitingBar1.StopWaiting();
                radWaitingBar1.Visible = false;
            }
        }

        private string getHostName(string url)
        {
            int portIndex = url.LastIndexOf(":");
            int hostNameIndex = url.IndexOf("://") + 3;
            return url.Substring(hostNameIndex, portIndex - hostNameIndex);
        }

        private string getBaseUrl(string hostName)
        {
            return "http://" + hostName + ":1515/";
        }

        private void SetConnectionResult(bool value, string version)
        {
            lblStatusCircle.BackColor = value ? Color.LightGreen : Color.Red;
            txtVersion.Text = value ? version : ".............";
        }

        private void LoadSettings()
        {
            txtBaseUrl.Text = getHostName(Properties.Settings.Default.OrphanageServiceURL);
        }
    }
}