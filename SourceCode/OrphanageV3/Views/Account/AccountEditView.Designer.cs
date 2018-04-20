namespace OrphanageV3.Views.Account
{
    partial class AccountEditView
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
            this.cmbCurrency = new Telerik.WinControls.UI.RadDropDownList();
            this.accountBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.numAmount = new Telerik.WinControls.UI.RadSpinEditor();
            this.txtAccountName = new Telerik.WinControls.UI.RadTextBox();
            this.txtCurrencyShortcut = new Telerik.WinControls.UI.RadTextBox();
            this.txtNote = new Telerik.WinControls.UI.RadTextBox();
            this.chkCanNotBeNegative = new Telerik.WinControls.UI.RadCheckBox();
            this.btnCancel = new Telerik.WinControls.UI.RadButton();
            this.btnSave = new Telerik.WinControls.UI.RadButton();
            this.lblCurrencyName = new Telerik.WinControls.UI.RadLabel();
            this.lblAmount = new Telerik.WinControls.UI.RadLabel();
            this.lblCurrencyShortcut = new Telerik.WinControls.UI.RadLabel();
            this.lblAccountName = new Telerik.WinControls.UI.RadLabel();
            this.lblCannotBeNegativ = new Telerik.WinControls.UI.RadLabel();
            this.lblNote = new Telerik.WinControls.UI.RadLabel();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.cmbCurrency)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.accountBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAccountName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCurrencyShortcut)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNote)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkCanNotBeNegative)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCurrencyName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCurrencyShortcut)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblAccountName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCannotBeNegativ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNote)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbCurrency
            // 
            this.cmbCurrency.AutoSizeItems = true;
            this.cmbCurrency.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.accountBindingSource, "Currency", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.cmbCurrency.Location = new System.Drawing.Point(12, 47);
            this.cmbCurrency.Name = "cmbCurrency";
            this.cmbCurrency.SelectNextOnDoubleClick = true;
            this.cmbCurrency.Size = new System.Drawing.Size(164, 20);
            this.cmbCurrency.SortStyle = Telerik.WinControls.Enumerations.SortStyle.Ascending;
            this.cmbCurrency.TabIndex = 16;
            this.cmbCurrency.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.cmbCurrency_SelectedIndexChanged);
            this.cmbCurrency.Enter += new System.EventHandler(this.changeToArabic_Enter);
            this.cmbCurrency.Leave += new System.EventHandler(this.returnToSavedLanguage_Leave);
            // 
            // accountBindingSource
            // 
            this.accountBindingSource.DataSource = typeof(OrphanageDataModel.FinancialData.Account);
            // 
            // numAmount
            // 
            this.numAmount.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.accountBindingSource, "Amount", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.numAmount.DecimalPlaces = 2;
            this.numAmount.Location = new System.Drawing.Point(12, 117);
            this.numAmount.Maximum = new decimal(new int[] {
            1316134911,
            2328,
            0,
            0});
            this.numAmount.Minimum = new decimal(new int[] {
            1316134911,
            2328,
            0,
            -2147483648});
            this.numAmount.Name = "numAmount";
            this.numAmount.ShowBorder = false;
            this.numAmount.Size = new System.Drawing.Size(164, 20);
            this.numAmount.TabIndex = 18;
            this.numAmount.TabStop = false;
            this.numAmount.ThousandsSeparator = true;
            // 
            // txtAccountName
            // 
            this.txtAccountName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.accountBindingSource, "AccountName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtAccountName.Location = new System.Drawing.Point(12, 12);
            this.txtAccountName.Name = "txtAccountName";
            this.txtAccountName.Size = new System.Drawing.Size(164, 20);
            this.txtAccountName.TabIndex = 15;
            this.txtAccountName.Enter += new System.EventHandler(this.changeToArabic_Enter);
            this.txtAccountName.Leave += new System.EventHandler(this.returnToSavedLanguage_Leave);
            // 
            // txtCurrencyShortcut
            // 
            this.txtCurrencyShortcut.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.accountBindingSource, "CurrencyShortcut", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtCurrencyShortcut.Location = new System.Drawing.Point(12, 82);
            this.txtCurrencyShortcut.MaxLength = 3;
            this.txtCurrencyShortcut.Name = "txtCurrencyShortcut";
            this.txtCurrencyShortcut.Size = new System.Drawing.Size(164, 20);
            this.txtCurrencyShortcut.TabIndex = 17;
            this.txtCurrencyShortcut.TextChanged += new System.EventHandler(this.txtCurrencySho_TextChanged);
            // 
            // txtNote
            // 
            this.txtNote.AutoSize = false;
            this.txtNote.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.accountBindingSource, "Note", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtNote.Location = new System.Drawing.Point(12, 187);
            this.txtNote.Multiline = true;
            this.txtNote.Name = "txtNote";
            this.txtNote.Size = new System.Drawing.Size(164, 62);
            this.txtNote.TabIndex = 20;
            this.txtNote.Enter += new System.EventHandler(this.changeToArabic_Enter);
            this.txtNote.Leave += new System.EventHandler(this.returnToSavedLanguage_Leave);
            // 
            // chkCanNotBeNegative
            // 
            this.chkCanNotBeNegative.DataBindings.Add(new System.Windows.Forms.Binding("IsChecked", this.accountBindingSource, "CanNotBeNegative", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkCanNotBeNegative.Location = new System.Drawing.Point(161, 155);
            this.chkCanNotBeNegative.Name = "chkCanNotBeNegative";
            this.chkCanNotBeNegative.Size = new System.Drawing.Size(15, 15);
            this.chkCanNotBeNegative.TabIndex = 19;
            this.chkCanNotBeNegative.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.chkCanNotBeNegative_ToggleStateChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(137, 286);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(71, 24);
            this.btnCancel.TabIndex = 22;
            this.btnCancel.Text = "عودة";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(60, 286);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(71, 24);
            this.btnSave.TabIndex = 21;
            this.btnSave.Text = "حفظ";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblCurrencyName
            // 
            this.lblCurrencyName.AutoSize = false;
            this.lblCurrencyName.Location = new System.Drawing.Point(179, 48);
            this.lblCurrencyName.Name = "lblCurrencyName";
            this.lblCurrencyName.Size = new System.Drawing.Size(76, 40);
            this.lblCurrencyName.TabIndex = 9;
            this.lblCurrencyName.Text = "اسم العملة :";
            this.lblCurrencyName.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblAmount
            // 
            this.lblAmount.AutoSize = false;
            this.lblAmount.Location = new System.Drawing.Point(179, 118);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(76, 40);
            this.lblAmount.TabIndex = 10;
            this.lblAmount.Text = "المبلغ :";
            this.lblAmount.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblCurrencyShortcut
            // 
            this.lblCurrencyShortcut.AutoSize = false;
            this.lblCurrencyShortcut.Location = new System.Drawing.Point(179, 83);
            this.lblCurrencyShortcut.Name = "lblCurrencyShortcut";
            this.lblCurrencyShortcut.Size = new System.Drawing.Size(76, 40);
            this.lblCurrencyShortcut.TabIndex = 11;
            this.lblCurrencyShortcut.Text = "اختصار العملة :";
            this.lblCurrencyShortcut.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblAccountName
            // 
            this.lblAccountName.AutoSize = false;
            this.lblAccountName.Location = new System.Drawing.Point(179, 12);
            this.lblAccountName.Name = "lblAccountName";
            this.lblAccountName.Size = new System.Drawing.Size(76, 40);
            this.lblAccountName.TabIndex = 12;
            this.lblAccountName.Text = "اسم الحساب :";
            this.lblAccountName.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblCannotBeNegativ
            // 
            this.lblCannotBeNegativ.AutoSize = false;
            this.lblCannotBeNegativ.Location = new System.Drawing.Point(179, 154);
            this.lblCannotBeNegativ.Name = "lblCannotBeNegativ";
            this.lblCannotBeNegativ.Size = new System.Drawing.Size(76, 40);
            this.lblCannotBeNegativ.TabIndex = 13;
            this.lblCannotBeNegativ.Text = "حساب جاري :";
            this.lblCannotBeNegativ.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblNote
            // 
            this.lblNote.AutoSize = false;
            this.lblNote.Location = new System.Drawing.Point(179, 197);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(76, 40);
            this.lblNote.TabIndex = 14;
            this.lblNote.Text = "ملاحظات :";
            this.lblNote.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // AccountEditView
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(269, 322);
            this.Controls.Add(this.cmbCurrency);
            this.Controls.Add(this.numAmount);
            this.Controls.Add(this.txtAccountName);
            this.Controls.Add(this.txtCurrencyShortcut);
            this.Controls.Add(this.txtNote);
            this.Controls.Add(this.chkCanNotBeNegative);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblCurrencyName);
            this.Controls.Add(this.lblAmount);
            this.Controls.Add(this.lblCurrencyShortcut);
            this.Controls.Add(this.lblAccountName);
            this.Controls.Add(this.lblCannotBeNegativ);
            this.Controls.Add(this.lblNote);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "AccountEditView";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "AccountEditView";
            this.Load += new System.EventHandler(this.AccountEditView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cmbCurrency)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.accountBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAccountName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCurrencyShortcut)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNote)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkCanNotBeNegative)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCurrencyName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCurrencyShortcut)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblAccountName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCannotBeNegativ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNote)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal Telerik.WinControls.UI.RadDropDownList cmbCurrency;
        internal Telerik.WinControls.UI.RadSpinEditor numAmount;
        internal Telerik.WinControls.UI.RadTextBox txtAccountName;
        internal Telerik.WinControls.UI.RadTextBox txtCurrencyShortcut;
        internal Telerik.WinControls.UI.RadTextBox txtNote;
        internal Telerik.WinControls.UI.RadCheckBox chkCanNotBeNegative;
        internal Telerik.WinControls.UI.RadButton btnCancel;
        internal Telerik.WinControls.UI.RadButton btnSave;
        internal Telerik.WinControls.UI.RadLabel lblCurrencyName;
        internal Telerik.WinControls.UI.RadLabel lblAmount;
        internal Telerik.WinControls.UI.RadLabel lblCurrencyShortcut;
        internal Telerik.WinControls.UI.RadLabel lblAccountName;
        internal Telerik.WinControls.UI.RadLabel lblCannotBeNegativ;
        internal Telerik.WinControls.UI.RadLabel lblNote;
        private System.Windows.Forms.BindingSource accountBindingSource;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}
