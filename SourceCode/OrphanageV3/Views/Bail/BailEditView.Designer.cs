namespace OrphanageV3.Views.Bail
{
    partial class BailEditView
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
            this.btnCancel = new Telerik.WinControls.UI.RadButton();
            this.btnSave = new Telerik.WinControls.UI.RadButton();
            this.btnChooseGuarantor = new Telerik.WinControls.UI.RadButton();
            this.dteEndDate = new Telerik.WinControls.UI.RadDateTimePicker();
            this.bailBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dteStartDate = new Telerik.WinControls.UI.RadDateTimePicker();
            this.numAmount = new Telerik.WinControls.UI.RadSpinEditor();
            this.txtNote = new Telerik.WinControls.UI.RadTextBox();
            this.txtGuarantorName = new Telerik.WinControls.UI.RadTextBox();
            this.lblGuarantorName = new Telerik.WinControls.UI.RadLabel();
            this.lblNote = new Telerik.WinControls.UI.RadLabel();
            this.lblIsExpired = new Telerik.WinControls.UI.RadLabel();
            this.lblIsFamily = new Telerik.WinControls.UI.RadLabel();
            this.lblCurrency = new Telerik.WinControls.UI.RadLabel();
            this.lblNoLimit = new Telerik.WinControls.UI.RadLabel();
            this.lblIsMonthly = new Telerik.WinControls.UI.RadLabel();
            this.lblTo = new Telerik.WinControls.UI.RadLabel();
            this.chkIsExpired = new Telerik.WinControls.UI.RadCheckBox();
            this.lblFrom = new Telerik.WinControls.UI.RadLabel();
            this.chkIsFamilyBail = new Telerik.WinControls.UI.RadCheckBox();
            this.chkNoTime = new Telerik.WinControls.UI.RadCheckBox();
            this.lblAmount = new Telerik.WinControls.UI.RadLabel();
            this.chkIsMonthlyBail = new Telerik.WinControls.UI.RadCheckBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnChooseGuarantor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteEndDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bailBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteStartDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNote)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGuarantorName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblGuarantorName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNote)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblIsExpired)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblIsFamily)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCurrency)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNoLimit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblIsMonthly)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsExpired)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsFamilyBail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkNoTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsMonthlyBail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(136, 318);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 24);
            this.btnCancel.TabIndex = 38;
            this.btnCancel.Text = "cv";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(47, 318);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(80, 24);
            this.btnSave.TabIndex = 37;
            this.btnSave.Text = "cv";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnChooseGuarantor
            // 
            this.btnChooseGuarantor.Enabled = false;
            this.btnChooseGuarantor.Location = new System.Drawing.Point(12, 12);
            this.btnChooseGuarantor.Name = "btnChooseGuarantor";
            this.btnChooseGuarantor.Size = new System.Drawing.Size(24, 20);
            this.btnChooseGuarantor.TabIndex = 36;
            this.btnChooseGuarantor.Text = "...";
            this.btnChooseGuarantor.Click += new System.EventHandler(this.btnChooseGuarantor_Click);
            // 
            // dteEndDate
            // 
            this.dteEndDate.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.bailBindingSource, "EndDate", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dteEndDate.Enabled = false;
            this.dteEndDate.Location = new System.Drawing.Point(42, 124);
            this.dteEndDate.Name = "dteEndDate";
            this.dteEndDate.Size = new System.Drawing.Size(154, 20);
            this.dteEndDate.TabIndex = 35;
            this.dteEndDate.TabStop = false;
            this.dteEndDate.Text = "Samstag, 31. Mai 2014";
            this.dteEndDate.Value = new System.DateTime(2014, 5, 31, 12, 4, 52, 559);
            // 
            // bailBindingSource
            // 
            this.bailBindingSource.AllowNew = true;
            this.bailBindingSource.DataSource = typeof(OrphanageDataModel.FinancialData.Bail);
            // 
            // dteStartDate
            // 
            this.dteStartDate.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.bailBindingSource, "StartDate", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dteStartDate.Enabled = false;
            this.dteStartDate.Location = new System.Drawing.Point(42, 96);
            this.dteStartDate.Name = "dteStartDate";
            this.dteStartDate.Size = new System.Drawing.Size(154, 20);
            this.dteStartDate.TabIndex = 34;
            this.dteStartDate.TabStop = false;
            this.dteStartDate.Text = "Samstag, 31. Mai 2014";
            this.dteStartDate.Value = new System.DateTime(2014, 5, 31, 12, 4, 52, 559);
            // 
            // numAmount
            // 
            this.numAmount.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.bailBindingSource, "Amount", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.numAmount.DecimalPlaces = 2;
            this.numAmount.Enabled = false;
            this.numAmount.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numAmount.Location = new System.Drawing.Point(42, 40);
            this.numAmount.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numAmount.Name = "numAmount";
            this.numAmount.Size = new System.Drawing.Size(154, 20);
            this.numAmount.TabIndex = 33;
            this.numAmount.TabStop = false;
            this.numAmount.ThousandsSeparator = true;
            // 
            // txtNote
            // 
            this.txtNote.AutoSize = false;
            this.txtNote.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bailBindingSource, "Note", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtNote.Enabled = false;
            this.txtNote.Location = new System.Drawing.Point(42, 234);
            this.txtNote.Multiline = true;
            this.txtNote.Name = "txtNote";
            this.txtNote.Size = new System.Drawing.Size(154, 68);
            this.txtNote.TabIndex = 32;
            // 
            // txtGuarantorName
            // 
            this.txtGuarantorName.Location = new System.Drawing.Point(42, 12);
            this.txtGuarantorName.Name = "txtGuarantorName";
            this.txtGuarantorName.ReadOnly = true;
            this.txtGuarantorName.Size = new System.Drawing.Size(154, 20);
            this.txtGuarantorName.TabIndex = 31;
            this.txtGuarantorName.TabStop = false;
            // 
            // lblGuarantorName
            // 
            this.lblGuarantorName.AutoSize = false;
            this.lblGuarantorName.Location = new System.Drawing.Point(202, 11);
            this.lblGuarantorName.Name = "lblGuarantorName";
            this.lblGuarantorName.Size = new System.Drawing.Size(82, 18);
            this.lblGuarantorName.TabIndex = 29;
            this.lblGuarantorName.Text = "cv";
            this.lblGuarantorName.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblNote
            // 
            this.lblNote.AutoSize = false;
            this.lblNote.Location = new System.Drawing.Point(202, 234);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(82, 18);
            this.lblNote.TabIndex = 28;
            this.lblNote.Text = "cv";
            this.lblNote.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblIsExpired
            // 
            this.lblIsExpired.AutoSize = false;
            this.lblIsExpired.Location = new System.Drawing.Point(202, 206);
            this.lblIsExpired.Name = "lblIsExpired";
            this.lblIsExpired.Size = new System.Drawing.Size(82, 18);
            this.lblIsExpired.TabIndex = 27;
            this.lblIsExpired.Text = "cv";
            this.lblIsExpired.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblIsFamily
            // 
            this.lblIsFamily.AutoSize = false;
            this.lblIsFamily.Location = new System.Drawing.Point(202, 178);
            this.lblIsFamily.Name = "lblIsFamily";
            this.lblIsFamily.Size = new System.Drawing.Size(82, 18);
            this.lblIsFamily.TabIndex = 26;
            this.lblIsFamily.Text = "cv";
            this.lblIsFamily.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblCurrency
            // 
            this.lblCurrency.AutoSize = false;
            this.lblCurrency.Location = new System.Drawing.Point(12, 42);
            this.lblCurrency.Name = "lblCurrency";
            this.lblCurrency.Size = new System.Drawing.Size(24, 18);
            this.lblCurrency.TabIndex = 25;
            this.lblCurrency.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblNoLimit
            // 
            this.lblNoLimit.AutoSize = false;
            this.lblNoLimit.Location = new System.Drawing.Point(202, 67);
            this.lblNoLimit.Name = "lblNoLimit";
            this.lblNoLimit.Size = new System.Drawing.Size(82, 18);
            this.lblNoLimit.TabIndex = 24;
            this.lblNoLimit.Text = "cv";
            this.lblNoLimit.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblIsMonthly
            // 
            this.lblIsMonthly.AutoSize = false;
            this.lblIsMonthly.Location = new System.Drawing.Point(202, 151);
            this.lblIsMonthly.Name = "lblIsMonthly";
            this.lblIsMonthly.Size = new System.Drawing.Size(82, 18);
            this.lblIsMonthly.TabIndex = 23;
            this.lblIsMonthly.Text = "cv";
            this.lblIsMonthly.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblTo
            // 
            this.lblTo.AutoSize = false;
            this.lblTo.Location = new System.Drawing.Point(202, 123);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(82, 18);
            this.lblTo.TabIndex = 22;
            this.lblTo.Text = "cv";
            this.lblTo.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            // 
            // chkIsExpired
            // 
            this.chkIsExpired.Enabled = false;
            this.chkIsExpired.Location = new System.Drawing.Point(181, 206);
            this.chkIsExpired.Name = "chkIsExpired";
            this.chkIsExpired.Size = new System.Drawing.Size(15, 15);
            this.chkIsExpired.TabIndex = 20;
            this.chkIsExpired.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.chkIsExpired_ToggleStateChanged);
            // 
            // lblFrom
            // 
            this.lblFrom.AutoSize = false;
            this.lblFrom.Location = new System.Drawing.Point(202, 95);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(82, 18);
            this.lblFrom.TabIndex = 21;
            this.lblFrom.Text = "cv";
            this.lblFrom.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            // 
            // chkIsFamilyBail
            // 
            this.chkIsFamilyBail.DataBindings.Add(new System.Windows.Forms.Binding("IsChecked", this.bailBindingSource, "IsFamilyBail", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkIsFamilyBail.Enabled = false;
            this.chkIsFamilyBail.Location = new System.Drawing.Point(181, 179);
            this.chkIsFamilyBail.Name = "chkIsFamilyBail";
            this.chkIsFamilyBail.Size = new System.Drawing.Size(15, 15);
            this.chkIsFamilyBail.TabIndex = 19;
            // 
            // chkNoTime
            // 
            this.chkNoTime.Enabled = false;
            this.chkNoTime.Location = new System.Drawing.Point(181, 68);
            this.chkNoTime.Name = "chkNoTime";
            this.chkNoTime.Size = new System.Drawing.Size(15, 15);
            this.chkNoTime.TabIndex = 18;
            this.chkNoTime.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.chkNoTime_ToggleStateChanged);
            // 
            // lblAmount
            // 
            this.lblAmount.AutoSize = false;
            this.lblAmount.Location = new System.Drawing.Point(202, 39);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(82, 18);
            this.lblAmount.TabIndex = 30;
            this.lblAmount.Text = "cv";
            this.lblAmount.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            // 
            // chkIsMonthlyBail
            // 
            this.chkIsMonthlyBail.DataBindings.Add(new System.Windows.Forms.Binding("IsChecked", this.bailBindingSource, "IsMonthlyBail", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkIsMonthlyBail.Enabled = false;
            this.chkIsMonthlyBail.Location = new System.Drawing.Point(181, 152);
            this.chkIsMonthlyBail.Name = "chkIsMonthlyBail";
            this.chkIsMonthlyBail.Size = new System.Drawing.Size(15, 15);
            this.chkIsMonthlyBail.TabIndex = 17;
            // 
            // errorProvider1
            // 
            this.errorProvider1.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider1.ContainerControl = this;
            this.errorProvider1.RightToLeft = true;
            // 
            // BailEditView
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(284, 353);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnChooseGuarantor);
            this.Controls.Add(this.dteEndDate);
            this.Controls.Add(this.dteStartDate);
            this.Controls.Add(this.numAmount);
            this.Controls.Add(this.txtNote);
            this.Controls.Add(this.txtGuarantorName);
            this.Controls.Add(this.lblGuarantorName);
            this.Controls.Add(this.lblNote);
            this.Controls.Add(this.lblIsExpired);
            this.Controls.Add(this.lblIsFamily);
            this.Controls.Add(this.lblCurrency);
            this.Controls.Add(this.lblNoLimit);
            this.Controls.Add(this.lblIsMonthly);
            this.Controls.Add(this.lblTo);
            this.Controls.Add(this.chkIsExpired);
            this.Controls.Add(this.lblFrom);
            this.Controls.Add(this.chkIsFamilyBail);
            this.Controls.Add(this.chkNoTime);
            this.Controls.Add(this.lblAmount);
            this.Controls.Add(this.chkIsMonthlyBail);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "BailEditView";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "BailEditView";
            this.Load += new System.EventHandler(this.BailEditView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnChooseGuarantor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteEndDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bailBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteStartDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNote)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGuarantorName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblGuarantorName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNote)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblIsExpired)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblIsFamily)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCurrency)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNoLimit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblIsMonthly)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsExpired)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsFamilyBail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkNoTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsMonthlyBail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal Telerik.WinControls.UI.RadButton btnCancel;
        internal Telerik.WinControls.UI.RadButton btnSave;
        internal Telerik.WinControls.UI.RadButton btnChooseGuarantor;
        internal Telerik.WinControls.UI.RadDateTimePicker dteEndDate;
        private System.Windows.Forms.BindingSource bailBindingSource;
        internal Telerik.WinControls.UI.RadDateTimePicker dteStartDate;
        internal Telerik.WinControls.UI.RadSpinEditor numAmount;
        internal Telerik.WinControls.UI.RadTextBox txtNote;
        internal Telerik.WinControls.UI.RadTextBox txtGuarantorName;
        internal Telerik.WinControls.UI.RadLabel lblGuarantorName;
        internal Telerik.WinControls.UI.RadLabel lblNote;
        internal Telerik.WinControls.UI.RadLabel lblIsExpired;
        internal Telerik.WinControls.UI.RadLabel lblIsFamily;
        internal Telerik.WinControls.UI.RadLabel lblCurrency;
        internal Telerik.WinControls.UI.RadLabel lblNoLimit;
        internal Telerik.WinControls.UI.RadLabel lblIsMonthly;
        internal Telerik.WinControls.UI.RadLabel lblTo;
        internal Telerik.WinControls.UI.RadCheckBox chkIsExpired;
        internal Telerik.WinControls.UI.RadLabel lblFrom;
        internal Telerik.WinControls.UI.RadCheckBox chkIsFamilyBail;
        internal Telerik.WinControls.UI.RadCheckBox chkNoTime;
        internal Telerik.WinControls.UI.RadLabel lblAmount;
        internal Telerik.WinControls.UI.RadCheckBox chkIsMonthlyBail;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}
