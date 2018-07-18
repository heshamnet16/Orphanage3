namespace ServiceConfigurer
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.radPageView1 = new Telerik.WinControls.UI.RadPageView();
            this.pgeDatabase = new Telerik.WinControls.UI.RadPageViewPage();
            this.grpBackupRestore = new Telerik.WinControls.UI.RadGroupBox();
            this.btnRestore = new Telerik.WinControls.UI.RadButton();
            this.btnBackup = new Telerik.WinControls.UI.RadButton();
            this.grpDatabase = new Telerik.WinControls.UI.RadGroupBox();
            this.btnFix = new Telerik.WinControls.UI.RadButton();
            this.btnCheck = new Telerik.WinControls.UI.RadButton();
            this.radRadialGauge1 = new Telerik.WinControls.UI.Gauges.RadRadialGauge();
            this.radialGaugeArc1 = new Telerik.WinControls.UI.Gauges.RadialGaugeArc();
            this.radialGaugeArc2 = new Telerik.WinControls.UI.Gauges.RadialGaugeArc();
            this.radialGaugeNeedle1 = new Telerik.WinControls.UI.Gauges.RadialGaugeNeedle();
            this.pgeService = new Telerik.WinControls.UI.RadPageViewPage();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.togServiceState = new Telerik.WinControls.UI.RadToggleSwitch();
            this.grpCertificateState = new Telerik.WinControls.UI.RadGroupBox();
            this.btnInstallCertificate = new Telerik.WinControls.UI.RadButton();
            this.btnLoadCertificate = new Telerik.WinControls.UI.RadButton();
            this.grpServiceState = new Telerik.WinControls.UI.RadGroupBox();
            this.grpServiceInstall = new Telerik.WinControls.UI.RadGroupBox();
            this.btnInstall = new Telerik.WinControls.UI.RadButton();
            this.btnUninstall = new Telerik.WinControls.UI.RadButton();
            this.lblState = new Telerik.WinControls.UI.RadLabel();
            this.lblStartDate = new Telerik.WinControls.UI.RadLabel();
            this.lblEndDate = new Telerik.WinControls.UI.RadLabel();
            this.lblStateText = new Telerik.WinControls.UI.RadLabel();
            this.lblStartDateText = new Telerik.WinControls.UI.RadLabel();
            this.lblEndDateText = new Telerik.WinControls.UI.RadLabel();
            this.btnRefresh = new Telerik.WinControls.UI.RadButton();
            ((System.ComponentModel.ISupportInitialize)(this.radPageView1)).BeginInit();
            this.radPageView1.SuspendLayout();
            this.pgeDatabase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpBackupRestore)).BeginInit();
            this.grpBackupRestore.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnRestore)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnBackup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpDatabase)).BeginInit();
            this.grpDatabase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnFix)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCheck)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radRadialGauge1)).BeginInit();
            this.pgeService.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.togServiceState)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpCertificateState)).BeginInit();
            this.grpCertificateState.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnInstallCertificate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnLoadCertificate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpServiceState)).BeginInit();
            this.grpServiceState.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpServiceInstall)).BeginInit();
            this.grpServiceInstall.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnInstall)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnUninstall)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblState)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblStartDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblEndDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblStateText)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblStartDateText)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblEndDateText)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnRefresh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // radPageView1
            // 
            this.radPageView1.Controls.Add(this.pgeDatabase);
            this.radPageView1.Controls.Add(this.pgeService);
            this.radPageView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radPageView1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.radPageView1.Location = new System.Drawing.Point(0, 0);
            this.radPageView1.Name = "radPageView1";
            this.radPageView1.SelectedPage = this.pgeService;
            this.radPageView1.Size = new System.Drawing.Size(423, 226);
            this.radPageView1.TabIndex = 0;
            this.radPageView1.Text = "radPageView1";
            ((Telerik.WinControls.UI.RadPageViewStripElement)(this.radPageView1.GetChildAt(0))).StripButtons = Telerik.WinControls.UI.StripViewButtons.None;
            ((Telerik.WinControls.UI.RadPageViewStripElement)(this.radPageView1.GetChildAt(0))).ItemAlignment = Telerik.WinControls.UI.StripViewItemAlignment.Center;
            ((Telerik.WinControls.UI.RadPageViewStripElement)(this.radPageView1.GetChildAt(0))).ItemFitMode = ((Telerik.WinControls.UI.StripViewItemFitMode)((Telerik.WinControls.UI.StripViewItemFitMode.Shrink | Telerik.WinControls.UI.StripViewItemFitMode.Fill)));
            ((Telerik.WinControls.UI.RadPageViewStripElement)(this.radPageView1.GetChildAt(0))).ItemSizeMode = Telerik.WinControls.UI.PageViewItemSizeMode.EqualHeight;
            // 
            // pgeDatabase
            // 
            this.pgeDatabase.Controls.Add(this.grpBackupRestore);
            this.pgeDatabase.Controls.Add(this.grpDatabase);
            this.pgeDatabase.Controls.Add(this.radRadialGauge1);
            this.pgeDatabase.ItemSize = new System.Drawing.SizeF(205F, 28F);
            this.pgeDatabase.Location = new System.Drawing.Point(10, 37);
            this.pgeDatabase.Name = "pgeDatabase";
            this.pgeDatabase.Size = new System.Drawing.Size(402, 178);
            this.pgeDatabase.Text = "radPageViewPage1";
            // 
            // grpBackupRestore
            // 
            this.grpBackupRestore.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this.grpBackupRestore.Controls.Add(this.btnRestore);
            this.grpBackupRestore.Controls.Add(this.btnBackup);
            this.grpBackupRestore.HeaderAlignment = Telerik.WinControls.UI.HeaderAlignment.Center;
            this.grpBackupRestore.HeaderText = "radGroupBox1";
            this.grpBackupRestore.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.grpBackupRestore.Location = new System.Drawing.Point(119, 81);
            this.grpBackupRestore.Name = "grpBackupRestore";
            this.grpBackupRestore.Size = new System.Drawing.Size(280, 77);
            this.grpBackupRestore.TabIndex = 7;
            this.grpBackupRestore.Text = "radGroupBox1";
            // 
            // btnRestore
            // 
            this.btnRestore.Location = new System.Drawing.Point(27, 27);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.Size = new System.Drawing.Size(110, 35);
            this.btnRestore.TabIndex = 0;
            this.btnRestore.Text = "radButton1";
            this.btnRestore.Click += new System.EventHandler(this.btnRestore_Click);
            // 
            // btnBackup
            // 
            this.btnBackup.Location = new System.Drawing.Point(143, 27);
            this.btnBackup.Name = "btnBackup";
            this.btnBackup.Size = new System.Drawing.Size(110, 35);
            this.btnBackup.TabIndex = 0;
            this.btnBackup.Text = "radButton1";
            this.btnBackup.Click += new System.EventHandler(this.btnBackup_Click);
            // 
            // grpDatabase
            // 
            this.grpDatabase.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this.grpDatabase.Controls.Add(this.btnFix);
            this.grpDatabase.Controls.Add(this.btnCheck);
            this.grpDatabase.HeaderAlignment = Telerik.WinControls.UI.HeaderAlignment.Center;
            this.grpDatabase.HeaderText = "asd";
            this.grpDatabase.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.grpDatabase.Location = new System.Drawing.Point(119, 3);
            this.grpDatabase.Name = "grpDatabase";
            this.grpDatabase.Size = new System.Drawing.Size(280, 72);
            this.grpDatabase.TabIndex = 7;
            this.grpDatabase.Text = "asd";
            // 
            // btnFix
            // 
            this.btnFix.Location = new System.Drawing.Point(27, 25);
            this.btnFix.Name = "btnFix";
            this.btnFix.Size = new System.Drawing.Size(110, 35);
            this.btnFix.TabIndex = 0;
            this.btnFix.Text = "radButton1";
            this.btnFix.Click += new System.EventHandler(this.btnFix_Click);
            // 
            // btnCheck
            // 
            this.btnCheck.Location = new System.Drawing.Point(143, 25);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(110, 35);
            this.btnCheck.TabIndex = 0;
            this.btnCheck.Text = "radButton1";
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // radRadialGauge1
            // 
            this.radRadialGauge1.BackColor = System.Drawing.Color.Transparent;
            this.radRadialGauge1.CausesValidation = false;
            this.radRadialGauge1.ForeColor = System.Drawing.Color.Black;
            this.radRadialGauge1.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.radialGaugeArc1,
            this.radialGaugeArc2,
            this.radialGaugeNeedle1});
            this.radRadialGauge1.Location = new System.Drawing.Point(3, 3);
            this.radRadialGauge1.Name = "radRadialGauge1";
            this.radRadialGauge1.Size = new System.Drawing.Size(110, 166);
            this.radRadialGauge1.StartAngle = 180D;
            this.radRadialGauge1.SweepAngle = 180D;
            this.radRadialGauge1.TabIndex = 6;
            this.radRadialGauge1.Text = "radRadialGauge1";
            this.toolTip1.SetToolTip(this.radRadialGauge1, "fg");
            this.radRadialGauge1.Value = 0F;
            // 
            // radialGaugeArc1
            // 
            this.radialGaugeArc1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(190)))), ((int)(((byte)(79)))));
            this.radialGaugeArc1.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(121)))), ((int)(((byte)(191)))), ((int)(((byte)(80)))));
            this.radialGaugeArc1.BindEndRange = true;
            this.radialGaugeArc1.Name = "radialGaugeArc1";
            this.radialGaugeArc1.RangeEnd = 0D;
            this.radialGaugeArc1.Width = 40D;
            // 
            // radialGaugeArc2
            // 
            this.radialGaugeArc2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(193)))), ((int)(((byte)(193)))));
            this.radialGaugeArc2.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(194)))), ((int)(((byte)(194)))));
            this.radialGaugeArc2.BindStartRange = true;
            this.radialGaugeArc2.Name = "radialGaugeArc2";
            this.radialGaugeArc2.RangeEnd = 100D;
            this.radialGaugeArc2.Width = 40D;
            // 
            // radialGaugeNeedle1
            // 
            this.radialGaugeNeedle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.radialGaugeNeedle1.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.radialGaugeNeedle1.BackLenghtPercentage = 0D;
            this.radialGaugeNeedle1.BindValue = true;
            this.radialGaugeNeedle1.InnerPointRadiusPercentage = 0D;
            this.radialGaugeNeedle1.LenghtPercentage = 94D;
            this.radialGaugeNeedle1.Name = "radialGaugeNeedle1";
            this.radialGaugeNeedle1.Thickness = 5D;
            this.radialGaugeNeedle1.Value = 0F;
            // 
            // pgeService
            // 
            this.pgeService.Controls.Add(this.grpCertificateState);
            this.pgeService.Controls.Add(this.grpServiceInstall);
            this.pgeService.Controls.Add(this.grpServiceState);
            this.pgeService.ItemSize = new System.Drawing.SizeF(205F, 28F);
            this.pgeService.Location = new System.Drawing.Point(10, 37);
            this.pgeService.Name = "pgeService";
            this.pgeService.Size = new System.Drawing.Size(402, 178);
            this.pgeService.Text = "radPageViewPage1";
            // 
            // togServiceState
            // 
            this.togServiceState.Location = new System.Drawing.Point(12, 27);
            this.togServiceState.Name = "togServiceState";
            this.togServiceState.OffText = "";
            this.togServiceState.OnText = "";
            this.togServiceState.Size = new System.Drawing.Size(112, 25);
            this.togServiceState.TabIndex = 0;
            this.togServiceState.Text = "radToggleSwitch1";
            ((Telerik.WinControls.UI.ToggleSwitchPartElement)(this.togServiceState.GetChildAt(0).GetChildAt(0))).BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(245)))), ((int)(((byte)(229)))));
            ((Telerik.WinControls.UI.ToggleSwitchPartElement)(this.togServiceState.GetChildAt(0).GetChildAt(0))).BackColor3 = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(244)))), ((int)(((byte)(225)))));
            ((Telerik.WinControls.UI.ToggleSwitchPartElement)(this.togServiceState.GetChildAt(0).GetChildAt(0))).BackColor4 = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(241)))), ((int)(((byte)(40)))));
            ((Telerik.WinControls.UI.ToggleSwitchPartElement)(this.togServiceState.GetChildAt(0).GetChildAt(0))).Text = "";
            ((Telerik.WinControls.UI.ToggleSwitchPartElement)(this.togServiceState.GetChildAt(0).GetChildAt(0))).BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(241)))), ((int)(((byte)(40)))));
            // 
            // grpCertificateState
            // 
            this.grpCertificateState.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this.grpCertificateState.Controls.Add(this.lblEndDate);
            this.grpCertificateState.Controls.Add(this.lblStartDate);
            this.grpCertificateState.Controls.Add(this.lblEndDateText);
            this.grpCertificateState.Controls.Add(this.lblStartDateText);
            this.grpCertificateState.Controls.Add(this.lblStateText);
            this.grpCertificateState.Controls.Add(this.lblState);
            this.grpCertificateState.Controls.Add(this.btnInstallCertificate);
            this.grpCertificateState.Controls.Add(this.btnRefresh);
            this.grpCertificateState.Controls.Add(this.btnLoadCertificate);
            this.grpCertificateState.HeaderAlignment = Telerik.WinControls.UI.HeaderAlignment.Center;
            this.grpCertificateState.HeaderText = "radGroupBox1";
            this.grpCertificateState.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.grpCertificateState.Location = new System.Drawing.Point(2, 81);
            this.grpCertificateState.Name = "grpCertificateState";
            this.grpCertificateState.Size = new System.Drawing.Size(397, 94);
            this.grpCertificateState.TabIndex = 8;
            this.grpCertificateState.Text = "radGroupBox1";
            // 
            // btnInstallCertificate
            // 
            this.btnInstallCertificate.Location = new System.Drawing.Point(264, 54);
            this.btnInstallCertificate.Name = "btnInstallCertificate";
            this.btnInstallCertificate.Size = new System.Drawing.Size(60, 35);
            this.btnInstallCertificate.TabIndex = 0;
            this.btnInstallCertificate.Text = "Install";
            // 
            // btnLoadCertificate
            // 
            this.btnLoadCertificate.Location = new System.Drawing.Point(264, 13);
            this.btnLoadCertificate.Name = "btnLoadCertificate";
            this.btnLoadCertificate.Size = new System.Drawing.Size(60, 35);
            this.btnLoadCertificate.TabIndex = 0;
            this.btnLoadCertificate.Text = "Load";
            // 
            // grpServiceState
            // 
            this.grpServiceState.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this.grpServiceState.Controls.Add(this.togServiceState);
            this.grpServiceState.HeaderAlignment = Telerik.WinControls.UI.HeaderAlignment.Center;
            this.grpServiceState.HeaderText = "asd";
            this.grpServiceState.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.grpServiceState.Location = new System.Drawing.Point(3, 3);
            this.grpServiceState.Name = "grpServiceState";
            this.grpServiceState.Size = new System.Drawing.Size(136, 72);
            this.grpServiceState.TabIndex = 9;
            this.grpServiceState.Text = "asd";
            // 
            // grpServiceInstall
            // 
            this.grpServiceInstall.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this.grpServiceInstall.Controls.Add(this.btnUninstall);
            this.grpServiceInstall.Controls.Add(this.btnInstall);
            this.grpServiceInstall.HeaderAlignment = Telerik.WinControls.UI.HeaderAlignment.Center;
            this.grpServiceInstall.HeaderText = "asd";
            this.grpServiceInstall.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.grpServiceInstall.Location = new System.Drawing.Point(145, 3);
            this.grpServiceInstall.Name = "grpServiceInstall";
            this.grpServiceInstall.Size = new System.Drawing.Size(254, 72);
            this.grpServiceInstall.TabIndex = 9;
            this.grpServiceInstall.Text = "asd";
            // 
            // btnInstall
            // 
            this.btnInstall.Location = new System.Drawing.Point(139, 21);
            this.btnInstall.Name = "btnInstall";
            this.btnInstall.Size = new System.Drawing.Size(110, 35);
            this.btnInstall.TabIndex = 0;
            this.btnInstall.Text = "insta";
            // 
            // btnUninstall
            // 
            this.btnUninstall.Location = new System.Drawing.Point(23, 21);
            this.btnUninstall.Name = "btnUninstall";
            this.btnUninstall.Size = new System.Drawing.Size(110, 35);
            this.btnUninstall.TabIndex = 0;
            this.btnUninstall.Text = "uninstal";
            // 
            // lblState
            // 
            this.lblState.AutoSize = false;
            this.lblState.Location = new System.Drawing.Point(178, 21);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(80, 18);
            this.lblState.TabIndex = 1;
            this.lblState.Text = "state";
            this.lblState.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblStartDate
            // 
            this.lblStartDate.AutoSize = false;
            this.lblStartDate.Location = new System.Drawing.Point(178, 45);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(80, 18);
            this.lblStartDate.TabIndex = 1;
            this.lblStartDate.Text = "startDate";
            this.lblStartDate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblEndDate
            // 
            this.lblEndDate.AutoSize = false;
            this.lblEndDate.Location = new System.Drawing.Point(178, 69);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new System.Drawing.Size(80, 18);
            this.lblEndDate.TabIndex = 1;
            this.lblEndDate.Text = "EndDate";
            this.lblEndDate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            this.lblEndDate.Click += new System.EventHandler(this.lblEndDate_Click);
            // 
            // lblStateText
            // 
            this.lblStateText.AutoSize = false;
            this.lblStateText.BorderVisible = true;
            this.lblStateText.Location = new System.Drawing.Point(13, 21);
            this.lblStateText.Name = "lblStateText";
            this.lblStateText.Size = new System.Drawing.Size(159, 18);
            this.lblStateText.TabIndex = 1;
            this.lblStateText.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblStartDateText
            // 
            this.lblStartDateText.AutoSize = false;
            this.lblStartDateText.BorderVisible = true;
            this.lblStartDateText.Location = new System.Drawing.Point(13, 45);
            this.lblStartDateText.Name = "lblStartDateText";
            this.lblStartDateText.Size = new System.Drawing.Size(159, 18);
            this.lblStartDateText.TabIndex = 1;
            this.lblStartDateText.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblEndDateText
            // 
            this.lblEndDateText.AutoSize = false;
            this.lblEndDateText.BorderVisible = true;
            this.lblEndDateText.Location = new System.Drawing.Point(13, 69);
            this.lblEndDateText.Name = "lblEndDateText";
            this.lblEndDateText.Size = new System.Drawing.Size(159, 18);
            this.lblEndDateText.TabIndex = 1;
            this.lblEndDateText.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(330, 13);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(60, 74);
            this.btnRefresh.TabIndex = 0;
            this.btnRefresh.Text = "Refresh";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(423, 226);
            this.Controls.Add(this.radPageView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "Service Configurer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radPageView1)).EndInit();
            this.radPageView1.ResumeLayout(false);
            this.pgeDatabase.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpBackupRestore)).EndInit();
            this.grpBackupRestore.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnRestore)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnBackup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpDatabase)).EndInit();
            this.grpDatabase.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnFix)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCheck)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radRadialGauge1)).EndInit();
            this.pgeService.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.togServiceState)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpCertificateState)).EndInit();
            this.grpCertificateState.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnInstallCertificate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnLoadCertificate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpServiceState)).EndInit();
            this.grpServiceState.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpServiceInstall)).EndInit();
            this.grpServiceInstall.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnInstall)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnUninstall)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblState)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblStartDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblEndDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblStateText)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblStartDateText)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblEndDateText)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnRefresh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadPageView radPageView1;
        private Telerik.WinControls.UI.RadPageViewPage pgeDatabase;
        private Telerik.WinControls.UI.RadPageViewPage pgeService;
        private Telerik.WinControls.UI.Gauges.RadRadialGauge radRadialGauge1;
        private Telerik.WinControls.UI.Gauges.RadialGaugeArc radialGaugeArc1;
        private Telerik.WinControls.UI.Gauges.RadialGaugeArc radialGaugeArc2;
        private Telerik.WinControls.UI.Gauges.RadialGaugeNeedle radialGaugeNeedle1;
        private System.Windows.Forms.ToolTip toolTip1;
        private Telerik.WinControls.UI.RadGroupBox grpDatabase;
        private Telerik.WinControls.UI.RadGroupBox grpBackupRestore;
        private Telerik.WinControls.UI.RadButton btnRestore;
        private Telerik.WinControls.UI.RadButton btnBackup;
        private Telerik.WinControls.UI.RadButton btnFix;
        private Telerik.WinControls.UI.RadButton btnCheck;
        private Telerik.WinControls.UI.RadGroupBox grpCertificateState;
        private Telerik.WinControls.UI.RadButton btnInstallCertificate;
        private Telerik.WinControls.UI.RadButton btnLoadCertificate;
        private Telerik.WinControls.UI.RadGroupBox grpServiceState;
        private Telerik.WinControls.UI.RadToggleSwitch togServiceState;
        private Telerik.WinControls.UI.RadGroupBox grpServiceInstall;
        private Telerik.WinControls.UI.RadButton btnUninstall;
        private Telerik.WinControls.UI.RadButton btnInstall;
        private Telerik.WinControls.UI.RadLabel lblEndDate;
        private Telerik.WinControls.UI.RadLabel lblStartDate;
        private Telerik.WinControls.UI.RadLabel lblEndDateText;
        private Telerik.WinControls.UI.RadLabel lblStartDateText;
        private Telerik.WinControls.UI.RadLabel lblStateText;
        private Telerik.WinControls.UI.RadLabel lblState;
        private Telerik.WinControls.UI.RadButton btnRefresh;
    }
}