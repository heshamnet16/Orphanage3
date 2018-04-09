namespace OrphanageV3.Views.Tools
{
    partial class SettingView
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
            this.pnlConnection = new Telerik.WinControls.UI.RadCollapsiblePanel();
            this.btnCheck = new Telerik.WinControls.UI.RadButton();
            this.radWaitingBar1 = new Telerik.WinControls.UI.RadWaitingBar();
            this.dotsLineWaitingBarIndicatorElement1 = new Telerik.WinControls.UI.DotsLineWaitingBarIndicatorElement();
            this.txtVersion = new Telerik.WinControls.UI.RadTextBox();
            this.txtBaseUrl = new Telerik.WinControls.UI.RadTextBox();
            this.lblStatusCircle = new Telerik.WinControls.UI.RadLabel();
            this.lblStatus = new Telerik.WinControls.UI.RadLabel();
            this.lblVersion = new Telerik.WinControls.UI.RadLabel();
            this.lblBaseUrl = new Telerik.WinControls.UI.RadLabel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlAppearance = new Telerik.WinControls.UI.RadCollapsiblePanel();
            this.numRowsCount = new System.Windows.Forms.NumericUpDown();
            this.chkShowHidden = new Telerik.WinControls.UI.RadCheckBox();
            this.chkUseBackgroundColor = new Telerik.WinControls.UI.RadCheckBox();
            this.lblShowHidden = new Telerik.WinControls.UI.RadLabel();
            this.lblRecordsNumber = new Telerik.WinControls.UI.RadLabel();
            this.lblUseBackgroundColor = new Telerik.WinControls.UI.RadLabel();
            this.radScrollablePanel1 = new Telerik.WinControls.UI.RadScrollablePanel();
            this.lblUseColors = new Telerik.WinControls.UI.RadLabel();
            this.chkUseColors = new Telerik.WinControls.UI.RadCheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pnlConnection)).BeginInit();
            this.pnlConnection.PanelContainer.SuspendLayout();
            this.pnlConnection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnCheck)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radWaitingBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVersion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBaseUrl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblStatusCircle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblVersion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblBaseUrl)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlAppearance)).BeginInit();
            this.pnlAppearance.PanelContainer.SuspendLayout();
            this.pnlAppearance.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRowsCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowHidden)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkUseBackgroundColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblShowHidden)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblRecordsNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblUseBackgroundColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radScrollablePanel1)).BeginInit();
            this.radScrollablePanel1.PanelContainer.SuspendLayout();
            this.radScrollablePanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lblUseColors)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkUseColors)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlConnection
            // 
            this.pnlConnection.AnimationEasingType = Telerik.WinControls.RadEasingType.InOutBack;
            this.pnlConnection.AnimationFrames = 20;
            this.pnlConnection.AnimationType = Telerik.WinControls.UI.CollapsiblePanelAnimationType.Slide;
            this.pnlConnection.HeaderText = "ConnectionSettings";
            this.pnlConnection.HorizontalHeaderAlignment = Telerik.WinControls.UI.RadHorizontalAlignment.Stretch;
            this.pnlConnection.Location = new System.Drawing.Point(3, 3);
            this.pnlConnection.Name = "pnlConnection";
            this.pnlConnection.OwnerBoundsCache = new System.Drawing.Rectangle(15, 3, 649, 116);
            // 
            // pnlConnection.PanelContainer
            // 
            this.pnlConnection.PanelContainer.Controls.Add(this.btnCheck);
            this.pnlConnection.PanelContainer.Controls.Add(this.radWaitingBar1);
            this.pnlConnection.PanelContainer.Controls.Add(this.txtVersion);
            this.pnlConnection.PanelContainer.Controls.Add(this.txtBaseUrl);
            this.pnlConnection.PanelContainer.Controls.Add(this.lblStatusCircle);
            this.pnlConnection.PanelContainer.Controls.Add(this.lblStatus);
            this.pnlConnection.PanelContainer.Controls.Add(this.lblVersion);
            this.pnlConnection.PanelContainer.Controls.Add(this.lblBaseUrl);
            this.pnlConnection.PanelContainer.Size = new System.Drawing.Size(655, 115);
            this.pnlConnection.Size = new System.Drawing.Size(657, 143);
            this.pnlConnection.TabIndex = 0;
            this.pnlConnection.Text = "radCollapsiblePanel1";
            this.pnlConnection.VerticalHeaderAlignment = Telerik.WinControls.UI.RadVerticalAlignment.Center;
            // 
            // btnCheck
            // 
            this.btnCheck.Location = new System.Drawing.Point(255, 84);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(110, 24);
            this.btnCheck.TabIndex = 3;
            this.btnCheck.Text = "Check Connection";
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // radWaitingBar1
            // 
            this.radWaitingBar1.Location = new System.Drawing.Point(13, 84);
            this.radWaitingBar1.Name = "radWaitingBar1";
            this.radWaitingBar1.Size = new System.Drawing.Size(130, 24);
            this.radWaitingBar1.TabIndex = 2;
            this.radWaitingBar1.Text = "radWaitingBar1";
            this.radWaitingBar1.WaitingIndicators.Add(this.dotsLineWaitingBarIndicatorElement1);
            this.radWaitingBar1.WaitingSpeed = 80;
            this.radWaitingBar1.WaitingStyle = Telerik.WinControls.Enumerations.WaitingBarStyles.DotsLine;
            // 
            // dotsLineWaitingBarIndicatorElement1
            // 
            this.dotsLineWaitingBarIndicatorElement1.ElementColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(0)))), ((int)(((byte)(47)))));
            this.dotsLineWaitingBarIndicatorElement1.Name = "dotsLineWaitingBarIndicatorElement1";
            // 
            // txtVersion
            // 
            this.txtVersion.Location = new System.Drawing.Point(13, 49);
            this.txtVersion.Name = "txtVersion";
            this.txtVersion.ReadOnly = true;
            this.txtVersion.Size = new System.Drawing.Size(515, 20);
            this.txtVersion.TabIndex = 1;
            // 
            // txtBaseUrl
            // 
            this.txtBaseUrl.Location = new System.Drawing.Point(13, 10);
            this.txtBaseUrl.Name = "txtBaseUrl";
            this.txtBaseUrl.Size = new System.Drawing.Size(515, 20);
            this.txtBaseUrl.TabIndex = 1;
            this.txtBaseUrl.TextChanged += new System.EventHandler(this.txtBaseUrl_TextChanged);
            // 
            // lblStatusCircle
            // 
            this.lblStatusCircle.AutoSize = false;
            this.lblStatusCircle.BackColor = System.Drawing.Color.Lime;
            this.lblStatusCircle.Location = new System.Drawing.Point(497, 84);
            this.lblStatusCircle.Name = "lblStatusCircle";
            this.lblStatusCircle.Size = new System.Drawing.Size(31, 24);
            this.lblStatusCircle.TabIndex = 0;
            this.lblStatusCircle.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = false;
            this.lblStatus.Location = new System.Drawing.Point(534, 90);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(109, 18);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.Text = "radLabel1";
            this.lblStatus.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = false;
            this.lblVersion.Location = new System.Drawing.Point(534, 51);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(109, 18);
            this.lblVersion.TabIndex = 0;
            this.lblVersion.Text = "radLabel1";
            this.lblVersion.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblBaseUrl
            // 
            this.lblBaseUrl.AutoSize = false;
            this.lblBaseUrl.Location = new System.Drawing.Point(534, 12);
            this.lblBaseUrl.Name = "lblBaseUrl";
            this.lblBaseUrl.Size = new System.Drawing.Size(109, 18);
            this.lblBaseUrl.TabIndex = 0;
            this.lblBaseUrl.Text = "radLabel1";
            this.lblBaseUrl.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.pnlConnection);
            this.flowLayoutPanel1.Controls.Add(this.pnlAppearance);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(663, 403);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // pnlAppearance
            // 
            this.pnlAppearance.AnimationEasingType = Telerik.WinControls.RadEasingType.InOutBack;
            this.pnlAppearance.AnimationFrames = 20;
            this.pnlAppearance.AnimationType = Telerik.WinControls.UI.CollapsiblePanelAnimationType.Slide;
            this.pnlAppearance.HeaderText = "Appearance";
            this.pnlAppearance.HorizontalHeaderAlignment = Telerik.WinControls.UI.RadHorizontalAlignment.Stretch;
            this.pnlAppearance.Location = new System.Drawing.Point(3, 152);
            this.pnlAppearance.Name = "pnlAppearance";
            this.pnlAppearance.OwnerBoundsCache = new System.Drawing.Rectangle(15, 3, 649, 116);
            // 
            // pnlAppearance.PanelContainer
            // 
            this.pnlAppearance.PanelContainer.Controls.Add(this.numRowsCount);
            this.pnlAppearance.PanelContainer.Controls.Add(this.chkShowHidden);
            this.pnlAppearance.PanelContainer.Controls.Add(this.chkUseColors);
            this.pnlAppearance.PanelContainer.Controls.Add(this.chkUseBackgroundColor);
            this.pnlAppearance.PanelContainer.Controls.Add(this.lblShowHidden);
            this.pnlAppearance.PanelContainer.Controls.Add(this.lblUseColors);
            this.pnlAppearance.PanelContainer.Controls.Add(this.lblRecordsNumber);
            this.pnlAppearance.PanelContainer.Controls.Add(this.lblUseBackgroundColor);
            this.pnlAppearance.PanelContainer.Size = new System.Drawing.Size(655, 79);
            this.pnlAppearance.Size = new System.Drawing.Size(657, 107);
            this.pnlAppearance.TabIndex = 0;
            this.pnlAppearance.Text = "Appearance";
            this.pnlAppearance.VerticalHeaderAlignment = Telerik.WinControls.UI.RadVerticalAlignment.Center;
            // 
            // numRowsCount
            // 
            this.numRowsCount.Location = new System.Drawing.Point(472, 51);
            this.numRowsCount.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numRowsCount.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numRowsCount.Name = "numRowsCount";
            this.numRowsCount.Size = new System.Drawing.Size(55, 20);
            this.numRowsCount.TabIndex = 5;
            this.numRowsCount.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numRowsCount.ValueChanged += new System.EventHandler(this.numRowsCount_ValueChanged);
            // 
            // chkShowHidden
            // 
            this.chkShowHidden.Location = new System.Drawing.Point(13, 24);
            this.chkShowHidden.Name = "chkShowHidden";
            this.chkShowHidden.Size = new System.Drawing.Size(15, 15);
            this.chkShowHidden.TabIndex = 4;
            this.chkShowHidden.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.chkShowHidden_ToggleStateChanged);
            // 
            // chkUseBackgroundColor
            // 
            this.chkUseBackgroundColor.Location = new System.Drawing.Point(13, 56);
            this.chkUseBackgroundColor.Name = "chkUseBackgroundColor";
            this.chkUseBackgroundColor.Size = new System.Drawing.Size(15, 15);
            this.chkUseBackgroundColor.TabIndex = 4;
            this.chkUseBackgroundColor.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.chkUseBackgroundColor_ToggleStateChanged);
            // 
            // lblShowHidden
            // 
            this.lblShowHidden.AutoSize = false;
            this.lblShowHidden.Location = new System.Drawing.Point(34, 21);
            this.lblShowHidden.Name = "lblShowHidden";
            this.lblShowHidden.Size = new System.Drawing.Size(109, 18);
            this.lblShowHidden.TabIndex = 0;
            this.lblShowHidden.Text = "hqihv hglthk hglordm";
            this.lblShowHidden.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblRecordsNumber
            // 
            this.lblRecordsNumber.AutoSize = false;
            this.lblRecordsNumber.Location = new System.Drawing.Point(534, 53);
            this.lblRecordsNumber.Name = "lblRecordsNumber";
            this.lblRecordsNumber.Size = new System.Drawing.Size(109, 18);
            this.lblRecordsNumber.TabIndex = 0;
            this.lblRecordsNumber.Text = "hsjo+hl g,k hgogrdm";
            this.lblRecordsNumber.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblUseBackgroundColor
            // 
            this.lblUseBackgroundColor.AutoSize = false;
            this.lblUseBackgroundColor.Location = new System.Drawing.Point(34, 53);
            this.lblUseBackgroundColor.Name = "lblUseBackgroundColor";
            this.lblUseBackgroundColor.Size = new System.Drawing.Size(109, 18);
            this.lblUseBackgroundColor.TabIndex = 0;
            this.lblUseBackgroundColor.Text = "hsjo+hl g,k hgogrdm";
            this.lblUseBackgroundColor.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            // 
            // radScrollablePanel1
            // 
            this.radScrollablePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radScrollablePanel1.Location = new System.Drawing.Point(0, 0);
            this.radScrollablePanel1.Name = "radScrollablePanel1";
            // 
            // radScrollablePanel1.PanelContainer
            // 
            this.radScrollablePanel1.PanelContainer.Controls.Add(this.flowLayoutPanel1);
            this.radScrollablePanel1.PanelContainer.Size = new System.Drawing.Size(663, 403);
            this.radScrollablePanel1.Size = new System.Drawing.Size(665, 405);
            this.radScrollablePanel1.TabIndex = 2;
            this.radScrollablePanel1.Text = "radScrollablePanel1";
            // 
            // lblUseColors
            // 
            this.lblUseColors.AutoSize = false;
            this.lblUseColors.Location = new System.Drawing.Point(533, 21);
            this.lblUseColors.Name = "lblUseColors";
            this.lblUseColors.Size = new System.Drawing.Size(109, 18);
            this.lblUseColors.TabIndex = 0;
            this.lblUseColors.Text = "hsjo+hl g,k hgogrdm";
            this.lblUseColors.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            // 
            // chkUseColors
            // 
            this.chkUseColors.Location = new System.Drawing.Point(512, 24);
            this.chkUseColors.Name = "chkUseColors";
            this.chkUseColors.Size = new System.Drawing.Size(15, 15);
            this.chkUseColors.TabIndex = 4;
            this.chkUseColors.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.chkUseColors_ToggleStateChanged);
            // 
            // SettingView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(665, 405);
            this.Controls.Add(this.radScrollablePanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "SettingView";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "SettingView";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingView_FormClosing);
            this.pnlConnection.PanelContainer.ResumeLayout(false);
            this.pnlConnection.PanelContainer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlConnection)).EndInit();
            this.pnlConnection.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnCheck)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radWaitingBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVersion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBaseUrl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblStatusCircle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblVersion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblBaseUrl)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.pnlAppearance.PanelContainer.ResumeLayout(false);
            this.pnlAppearance.PanelContainer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlAppearance)).EndInit();
            this.pnlAppearance.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numRowsCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowHidden)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkUseBackgroundColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblShowHidden)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblRecordsNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblUseBackgroundColor)).EndInit();
            this.radScrollablePanel1.PanelContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radScrollablePanel1)).EndInit();
            this.radScrollablePanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lblUseColors)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkUseColors)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadCollapsiblePanel pnlConnection;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Telerik.WinControls.UI.RadLabel lblBaseUrl;
        private Telerik.WinControls.UI.RadTextBox txtBaseUrl;
        private Telerik.WinControls.UI.RadTextBox txtVersion;
        private Telerik.WinControls.UI.RadLabel lblVersion;
        private Telerik.WinControls.UI.RadLabel lblStatus;
        private Telerik.WinControls.UI.RadWaitingBar radWaitingBar1;
        private Telerik.WinControls.UI.DotsLineWaitingBarIndicatorElement dotsLineWaitingBarIndicatorElement1;
        private Telerik.WinControls.UI.RadLabel lblStatusCircle;
        private Telerik.WinControls.UI.RadButton btnCheck;
        private Telerik.WinControls.UI.RadScrollablePanel radScrollablePanel1;
        private Telerik.WinControls.UI.RadLabel lblShowHidden;
        private Telerik.WinControls.UI.RadLabel lblUseBackgroundColor;
        private Telerik.WinControls.UI.RadCollapsiblePanel pnlAppearance;
        private Telerik.WinControls.UI.RadCheckBox chkShowHidden;
        private Telerik.WinControls.UI.RadCheckBox chkUseBackgroundColor;
        private System.Windows.Forms.NumericUpDown numRowsCount;
        private Telerik.WinControls.UI.RadLabel lblRecordsNumber;
        private Telerik.WinControls.UI.RadCheckBox chkUseColors;
        private Telerik.WinControls.UI.RadLabel lblUseColors;
    }
}
