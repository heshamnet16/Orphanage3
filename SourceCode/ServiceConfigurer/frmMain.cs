﻿using ServiceConfigurer.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI.Gauges;

namespace ServiceConfigurer
{
    public partial class frmMain : Telerik.WinControls.UI.RadForm
    {

        private DatabaseService _databaseService;
        private InstallerService _installerService;

        private Timer _timer;
        
        private bool _canClose = true; 

        private int _timerStepValue;
        private bool _timerIsIncrement;
        public frmMain()
        {
            InitializeComponent();
            setTextandImages();
            _databaseService = new DatabaseService();
            _installerService = new InstallerService();

            _databaseService.ProgressPercent += _databaseService_ProgressPercent;
            _timerStepValue = 0;
            _timerIsIncrement = true;
            _timer = new Timer();
            _timer.Interval = 300;
            _timer.Tick += _timer_Tick;
        }

        private void _databaseService_ProgressPercent(int perc)
        {
            ApplyValueToGauge(perc);
            Application.DoEvents();
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            if (_timerIsIncrement)
                _timerStepValue += 25;
            else
                _timerStepValue -= 25;

            if (_timerStepValue >= 100)
                _timerIsIncrement = false;
            if (_timerStepValue <= 0)
                _timerIsIncrement = true;

            ApplyValueToGauge(_timerStepValue);
        }

        private void ApplyValueToGauge(float value)
        {
            AnimatedPropertySetting setting = new AnimatedPropertySetting(RadRadialGaugeElement.ValueProperty,
                value, _timerIsIncrement ? value + 10 : value - 10, 15, 30);
            setting.ApplyEasingType = RadEasingType.InOutBounce;
            setting.ApplyValue(radRadialGauge1.GaugeElement);
            Application.DoEvents();
        }
        private void setTextandImages()
        {
            var picSize = new Size(32, 32);
            pgeDatabase.Image = new Bitmap(Properties.Resources.database_Pic, picSize);
            pgeService.Image = new Bitmap(Properties.Resources.service_Pic, picSize);
            pgeDatabase.Text = Properties.Resources.Database;
            pgeService.Text = Properties.Resources.Service;
            toolTip1.SetToolTip(this.radRadialGauge1, Properties.Resources.INFO_Processing);
            grpDatabase.Text = Properties.Resources.CheckAndFix;
            grpBackupRestore.Text = Properties.Resources.BackupRestore;
            btnBackup.Text = Properties.Resources.Backup;
            btnRestore.Text = Properties.Resources.Restore;
            btnCheck.Text = Properties.Resources.Check;
            btnFix.Text = Properties.Resources.FixDatabase;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            CheckState();
        }

        private void setFaildState()
        {
            radialGaugeArc1.BackColor = radialGaugeArc1.BackColor2 = Color.Red;
            radialGaugeArc2.BackColor = radialGaugeArc2.BackColor2 = Color.Red;
            radRadialGauge1.Value = 100;
            _timer.Stop();
        }

        private void setNormalState()
        {
            radialGaugeArc1.BackColor = radialGaugeArc1.BackColor2 = Color.FromArgb(119, 190, 79);
            radialGaugeArc2.BackColor = radialGaugeArc2.BackColor2 = Color.FromArgb(193, 193, 193);
            radRadialGauge1.Value = 0;
        }
        private async void CheckState()
        {
            ApplyValueToGauge(0);
            grpBackupRestore.Enabled = false;
            _timerStepValue = 0;
            _timerIsIncrement = true;
            _timer.Start();
            if (await _databaseService.CheckSQLServerExists())
            {
                if (await _databaseService.CheckDatabaseExists())
                {
                    int value = await _databaseService.CheckDatabase();
                    _timer.Stop();

                    ApplyValueToGauge(value);
                    if (radRadialGauge1.Value >= 100)
                    {
                        toolTip1.SetToolTip(this.radRadialGauge1, Properties.Resources.INFO_State_Good);
                    }
                    else
                    {
                        toolTip1.SetToolTip(this.radRadialGauge1, Properties.Resources.INFO_State + " " + Math.Ceiling(radRadialGauge1.Value) + "%");
                    }
                }
                else
                {
                    toolTip1.SetToolTip(this.radRadialGauge1, Properties.Resources.Error_Database_Dont_Exist);
                    toolTip1.Show(Properties.Resources.Error_Database_Dont_Exist, this, radRadialGauge1.Left + radRadialGauge1.Width, radRadialGauge1.Top + 100, 3000);
                    setFaildState();
                }
            }
            else
            {
                toolTip1.SetToolTip(this.radRadialGauge1, Properties.Resources.Error_Server_Dont_Exist);
                toolTip1.Show(Properties.Resources.Error_Server_Dont_Exist, this, radRadialGauge1.Left + radRadialGauge1.Width, radRadialGauge1.Top + 100, 3000);
                setFaildState();
            }
            grpBackupRestore.Enabled = true;
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            CheckState();
        }

        private void btnFix_Click(object sender, EventArgs e)
        {
            _databaseService.UpdateDataBase();
            CheckState();
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "ODB|*.odb";
                sfd.RestoreDirectory = true;
                sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                sfd.CheckPathExists = true;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if(System.IO.File.Exists(sfd.FileName))
                    {
                        System.IO.File.Delete(sfd.FileName);
                    }
                    grpBackupRestore.Enabled = false;
                    grpDatabase.Enabled = false;
                    setNormalState();
                    this.UseWaitCursor = true;
                    _canClose = false;
                    Application.DoEvents();
                    _databaseService.Backup(sfd.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                setFaildState();
            }
            finally
            {
                grpBackupRestore.Enabled = true;
                grpDatabase.Enabled = true;
                this.UseWaitCursor = false;
                _canClose = true;
            }
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "ODB|*.odb";
                ofd.RestoreDirectory = true;
                ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                ofd.CheckPathExists = true;
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    if (System.IO.File.Exists(ofd.FileName))
                    {
                        grpBackupRestore.Enabled = false;
                        grpDatabase.Enabled = false;
                        setNormalState();
                        this.UseWaitCursor = true;
                        _canClose = false;
                        Application.DoEvents();
                        _databaseService.Restore(ofd.FileName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                setFaildState();
            }
            finally
            {
                grpBackupRestore.Enabled = true;
                grpDatabase.Enabled = true;
                this.UseWaitCursor = false;
                _canClose = true;
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_canClose)
            {
                e.Cancel = true;
                MessageBox.Show(Properties.Resources.Error_Cannot_Close, "Service Configurer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void lblEndDate_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           togServiceState.Value =  _installerService.IsRunning();
        }

        private void togServiceState_Click(object sender, EventArgs e)
        {
            if (!togServiceState.Value)
                _installerService.StartService();
            else
                _installerService.StopService();
        }
    }
}
