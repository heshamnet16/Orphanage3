namespace OrphanageV3.Views.Login
{
    partial class LoginView
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
            this.lblUserName = new Telerik.WinControls.UI.RadLabel();
            this.lblTitle = new Telerik.WinControls.UI.RadLabel();
            this.lblPassword = new Telerik.WinControls.UI.RadLabel();
            this.txtUserName = new Telerik.WinControls.UI.RadTextBox();
            this.txtPassword = new Telerik.WinControls.UI.RadTextBox();
            this.btnLogin = new Telerik.WinControls.UI.RadButton();
            this.lblStatus = new Telerik.WinControls.UI.RadLabel();
            this.roundRectShape1 = new Telerik.WinControls.RoundRectShape(this.components);
            this.btnSetting = new Telerik.WinControls.UI.RadButton();
            this.grpSetBaseUri = new Telerik.WinControls.UI.RadGroupBox();
            this.btnCheck = new Telerik.WinControls.UI.RadButton();
            this.radWaitingBar1 = new Telerik.WinControls.UI.RadWaitingBar();
            this.dotsLineWaitingBarIndicatorElement1 = new Telerik.WinControls.UI.DotsLineWaitingBarIndicatorElement();
            this.txtVersion = new Telerik.WinControls.UI.RadTextBox();
            this.txtBaseUrl = new Telerik.WinControls.UI.RadTextBox();
            this.lblStatusCircle = new Telerik.WinControls.UI.RadLabel();
            this.lblConnectionStatus = new Telerik.WinControls.UI.RadLabel();
            this.lblVersion = new Telerik.WinControls.UI.RadLabel();
            this.lblBaseUrl = new Telerik.WinControls.UI.RadLabel();
            this.btnClose = new Telerik.WinControls.UI.RadButton();
            this.radWaitingBar2 = new Telerik.WinControls.UI.RadWaitingBar();
            this.dotsRingWaitingBarIndicatorElement1 = new Telerik.WinControls.UI.DotsRingWaitingBarIndicatorElement();
            ((System.ComponentModel.ISupportInitialize)(this.lblUserName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblTitle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblPassword)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnLogin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSetting)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpSetBaseUri)).BeginInit();
            this.grpSetBaseUri.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnCheck)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radWaitingBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVersion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBaseUrl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblStatusCircle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblConnectionStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblVersion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblBaseUrl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radWaitingBar2)).BeginInit();
            this.SuspendLayout();
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = false;
            this.lblUserName.Location = new System.Drawing.Point(3, 56);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(371, 18);
            this.lblUserName.TabIndex = 0;
            this.lblUserName.Text = ".";
            this.lblUserName.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            this.lblUserName.Click += new System.EventHandler(this.HideConnectionGroup);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = false;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lblTitle.Location = new System.Drawing.Point(3, 1);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(371, 49);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Login";
            this.lblTitle.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            this.lblTitle.Click += new System.EventHandler(this.HideConnectionGroup);
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = false;
            this.lblPassword.Location = new System.Drawing.Point(3, 124);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(371, 18);
            this.lblPassword.TabIndex = 0;
            this.lblPassword.Text = ".";
            this.lblPassword.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            this.lblPassword.Click += new System.EventHandler(this.HideConnectionGroup);
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(125, 80);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(128, 20);
            this.txtUserName.TabIndex = 2;
            this.txtUserName.Text = "مدير";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(125, 148);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(128, 20);
            this.txtPassword.TabIndex = 2;
            this.txtPassword.Text = "0000";
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(206, 272);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(96, 24);
            this.btnLogin.TabIndex = 3;
            this.btnLogin.Text = "login";
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = false;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.Color.Red;
            this.lblStatus.Location = new System.Drawing.Point(3, 204);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(371, 40);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.Text = "Access Denied";
            this.lblStatus.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            this.lblStatus.Visible = false;
            this.lblStatus.Click += new System.EventHandler(this.HideConnectionGroup);
            // 
            // roundRectShape1
            // 
            this.roundRectShape1.Radius = 15;
            // 
            // btnSetting
            // 
            this.btnSetting.Image = global::OrphanageV3.Properties.Resources.SettingPic;
            this.btnSetting.ImageAlignment = System.Drawing.ContentAlignment.TopRight;
            this.btnSetting.Location = new System.Drawing.Point(12, 272);
            this.btnSetting.Name = "btnSetting";
            this.btnSetting.Size = new System.Drawing.Size(24, 24);
            this.btnSetting.TabIndex = 3;
            this.btnSetting.Click += new System.EventHandler(this.btnSetting_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSetting.GetChildAt(0))).Image = global::OrphanageV3.Properties.Resources.SettingPic;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSetting.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.TopRight;
            ((Telerik.WinControls.Primitives.ImagePrimitive)(this.btnSetting.GetChildAt(0).GetChildAt(1).GetChildAt(0))).StretchHorizontally = true;
            ((Telerik.WinControls.Primitives.ImagePrimitive)(this.btnSetting.GetChildAt(0).GetChildAt(1).GetChildAt(0))).StretchVertically = true;
            ((Telerik.WinControls.Primitives.ImagePrimitive)(this.btnSetting.GetChildAt(0).GetChildAt(1).GetChildAt(0))).ImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            ((Telerik.WinControls.Primitives.ImagePrimitive)(this.btnSetting.GetChildAt(0).GetChildAt(1).GetChildAt(0))).ImageScaling = Telerik.WinControls.Enumerations.ImageScaling.SizeToFit;
            ((Telerik.WinControls.Primitives.ImagePrimitive)(this.btnSetting.GetChildAt(0).GetChildAt(1).GetChildAt(0))).ClipDrawing = true;
            // 
            // grpSetBaseUri
            // 
            this.grpSetBaseUri.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this.grpSetBaseUri.Controls.Add(this.btnCheck);
            this.grpSetBaseUri.Controls.Add(this.radWaitingBar1);
            this.grpSetBaseUri.Controls.Add(this.txtVersion);
            this.grpSetBaseUri.Controls.Add(this.txtBaseUrl);
            this.grpSetBaseUri.Controls.Add(this.lblStatusCircle);
            this.grpSetBaseUri.Controls.Add(this.lblConnectionStatus);
            this.grpSetBaseUri.Controls.Add(this.lblVersion);
            this.grpSetBaseUri.Controls.Add(this.lblBaseUrl);
            this.grpSetBaseUri.HeaderAlignment = Telerik.WinControls.UI.HeaderAlignment.Center;
            this.grpSetBaseUri.HeaderText = "radGroupBox1";
            this.grpSetBaseUri.Location = new System.Drawing.Point(40, 47);
            this.grpSetBaseUri.Name = "grpSetBaseUri";
            this.grpSetBaseUri.Size = new System.Drawing.Size(290, 219);
            this.grpSetBaseUri.TabIndex = 4;
            this.grpSetBaseUri.Text = "radGroupBox1";
            this.grpSetBaseUri.Visible = false;
            // 
            // btnCheck
            // 
            this.btnCheck.Location = new System.Drawing.Point(94, 173);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(99, 24);
            this.btnCheck.TabIndex = 11;
            this.btnCheck.Text = "Check Connection";
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // radWaitingBar1
            // 
            this.radWaitingBar1.Location = new System.Drawing.Point(16, 116);
            this.radWaitingBar1.Name = "radWaitingBar1";
            this.radWaitingBar1.Size = new System.Drawing.Size(47, 24);
            this.radWaitingBar1.TabIndex = 10;
            this.radWaitingBar1.Text = "radWaitingBar1";
            this.radWaitingBar1.WaitingIndicators.Add(this.dotsLineWaitingBarIndicatorElement1);
            this.radWaitingBar1.WaitingSpeed = 80;
            this.radWaitingBar1.WaitingStyle = Telerik.WinControls.Enumerations.WaitingBarStyles.DotsLine;
            // 
            // dotsLineWaitingBarIndicatorElement1
            // 
            this.dotsLineWaitingBarIndicatorElement1.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.dotsLineWaitingBarIndicatorElement1.ElementColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(0)))), ((int)(((byte)(47)))));
            this.dotsLineWaitingBarIndicatorElement1.Name = "dotsLineWaitingBarIndicatorElement1";
            this.dotsLineWaitingBarIndicatorElement1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // txtVersion
            // 
            this.txtVersion.Location = new System.Drawing.Point(16, 77);
            this.txtVersion.Name = "txtVersion";
            this.txtVersion.ReadOnly = true;
            this.txtVersion.Size = new System.Drawing.Size(165, 20);
            this.txtVersion.TabIndex = 8;
            // 
            // txtBaseUrl
            // 
            this.txtBaseUrl.Location = new System.Drawing.Point(16, 34);
            this.txtBaseUrl.Name = "txtBaseUrl";
            this.txtBaseUrl.Size = new System.Drawing.Size(165, 20);
            this.txtBaseUrl.TabIndex = 9;
            this.txtBaseUrl.TextChanged += new System.EventHandler(this.txtBaseUrl_TextChanged);
            // 
            // lblStatusCircle
            // 
            this.lblStatusCircle.AutoSize = false;
            this.lblStatusCircle.BackColor = System.Drawing.Color.Lime;
            this.lblStatusCircle.Location = new System.Drawing.Point(161, 116);
            this.lblStatusCircle.Name = "lblStatusCircle";
            this.lblStatusCircle.Size = new System.Drawing.Size(20, 24);
            this.lblStatusCircle.TabIndex = 4;
            this.lblStatusCircle.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblConnectionStatus
            // 
            this.lblConnectionStatus.AutoSize = false;
            this.lblConnectionStatus.Location = new System.Drawing.Point(187, 122);
            this.lblConnectionStatus.Name = "lblConnectionStatus";
            this.lblConnectionStatus.Size = new System.Drawing.Size(98, 18);
            this.lblConnectionStatus.TabIndex = 5;
            this.lblConnectionStatus.Text = "radLabel1";
            this.lblConnectionStatus.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = false;
            this.lblVersion.Location = new System.Drawing.Point(187, 79);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(98, 18);
            this.lblVersion.TabIndex = 6;
            this.lblVersion.Text = "radLabel1";
            this.lblVersion.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblBaseUrl
            // 
            this.lblBaseUrl.AutoSize = false;
            this.lblBaseUrl.Location = new System.Drawing.Point(187, 36);
            this.lblBaseUrl.Name = "lblBaseUrl";
            this.lblBaseUrl.Size = new System.Drawing.Size(98, 18);
            this.lblBaseUrl.TabIndex = 7;
            this.lblBaseUrl.Text = "radLabel1";
            this.lblBaseUrl.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(71, 272);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(96, 24);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "exit";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // radWaitingBar2
            // 
            this.radWaitingBar2.Location = new System.Drawing.Point(146, 235);
            this.radWaitingBar2.Name = "radWaitingBar2";
            this.radWaitingBar2.Size = new System.Drawing.Size(70, 70);
            this.radWaitingBar2.TabIndex = 10;
            this.radWaitingBar2.Text = "radWaitingBar1";
            this.radWaitingBar2.Visible = false;
            this.radWaitingBar2.WaitingIndicators.Add(this.dotsRingWaitingBarIndicatorElement1);
            this.radWaitingBar2.WaitingSpeed = 20;
            this.radWaitingBar2.WaitingStyle = Telerik.WinControls.Enumerations.WaitingBarStyles.DotsRing;
            // 
            // dotsRingWaitingBarIndicatorElement1
            // 
            this.dotsRingWaitingBarIndicatorElement1.Name = "dotsRingWaitingBarIndicatorElement1";
            // 
            // LoginView
            // 
            this.AcceptButton = this.btnLogin;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkGray;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(377, 308);
            this.Controls.Add(this.grpSetBaseUri);
            this.Controls.Add(this.btnSetting);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.lblUserName);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.radWaitingBar2);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.btnClose);
            this.Name = "LoginView";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Shape = this.roundRectShape1;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LoginView";
            this.Click += new System.EventHandler(this.HideConnectionGroup);
            ((System.ComponentModel.ISupportInitialize)(this.lblUserName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblTitle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblPassword)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnLogin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSetting)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpSetBaseUri)).EndInit();
            this.grpSetBaseUri.ResumeLayout(false);
            this.grpSetBaseUri.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnCheck)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radWaitingBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVersion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBaseUrl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblStatusCircle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblConnectionStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblVersion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblBaseUrl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radWaitingBar2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Telerik.WinControls.UI.RadLabel lblUserName;
        private Telerik.WinControls.UI.RadLabel lblTitle;
        private Telerik.WinControls.UI.RadLabel lblPassword;
        private Telerik.WinControls.UI.RadTextBox txtUserName;
        private Telerik.WinControls.UI.RadTextBox txtPassword;
        private Telerik.WinControls.UI.RadButton btnLogin;
        private Telerik.WinControls.UI.RadLabel lblStatus;
        private Telerik.WinControls.RoundRectShape roundRectShape1;
        private Telerik.WinControls.UI.RadButton btnSetting;
        private Telerik.WinControls.UI.RadGroupBox grpSetBaseUri;
        private Telerik.WinControls.UI.RadButton btnCheck;
        private Telerik.WinControls.UI.RadWaitingBar radWaitingBar1;
        private Telerik.WinControls.UI.DotsLineWaitingBarIndicatorElement dotsLineWaitingBarIndicatorElement1;
        private Telerik.WinControls.UI.RadTextBox txtVersion;
        private Telerik.WinControls.UI.RadTextBox txtBaseUrl;
        private Telerik.WinControls.UI.RadLabel lblStatusCircle;
        private Telerik.WinControls.UI.RadLabel lblConnectionStatus;
        private Telerik.WinControls.UI.RadLabel lblVersion;
        private Telerik.WinControls.UI.RadLabel lblBaseUrl;
        private Telerik.WinControls.UI.RadButton btnClose;
        private Telerik.WinControls.UI.RadWaitingBar radWaitingBar2;
        private Telerik.WinControls.UI.DotsRingWaitingBarIndicatorElement dotsRingWaitingBarIndicatorElement1;
    }
}
