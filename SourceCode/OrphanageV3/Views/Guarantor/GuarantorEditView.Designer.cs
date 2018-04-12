namespace OrphanageV3.Views.Guarantor
{
    partial class GuarantorEditView
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
            this.btnChooseAccount = new Telerik.WinControls.UI.RadButton();
            this.txtNote = new Telerik.WinControls.UI.RadTextBox();
            this.guarantorBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.txtAccountName = new Telerik.WinControls.UI.RadTextBox();
            this.txtAddress = new Telerik.WinControls.UI.RadTextBox();
            this.txtName = new Telerik.WinControls.UI.RadTextBox();
            this.lblAccountName = new Telerik.WinControls.UI.RadLabel();
            this.lblAddress = new Telerik.WinControls.UI.RadLabel();
            this.lblName = new Telerik.WinControls.UI.RadLabel();
            this.lblNotes = new Telerik.WinControls.UI.RadLabel();
            this.lblIsStillPaying = new Telerik.WinControls.UI.RadLabel();
            this.lblOnlyFamilies = new Telerik.WinControls.UI.RadLabel();
            this.lblIsPayingMonthly = new Telerik.WinControls.UI.RadLabel();
            this.chkIsStillPaying = new Telerik.WinControls.UI.RadCheckBox();
            this.chkIsSupportingOnlyFamilies = new Telerik.WinControls.UI.RadCheckBox();
            this.chkIsPayingMonthly = new Telerik.WinControls.UI.RadCheckBox();
            this.clrColor = new Telerik.WinControls.UI.RadColorBox();
            this.lblColor = new Telerik.WinControls.UI.RadLabel();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.nameForm1 = new OrphanageV3.Controlls.NameForm();
            this.addressForm1 = new OrphanageV3.Controlls.AddressForm();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnChooseAccount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNote)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.guarantorBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAccountName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblAccountName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblAddress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNotes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblIsStillPaying)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblOnlyFamilies)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblIsPayingMonthly)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsStillPaying)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsSupportingOnlyFamilies)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsPayingMonthly)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clrColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(208, 333);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(87, 24);
            this.btnCancel.TabIndex = 10151;
            this.btnCancel.Text = "الغاء الأمر";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(115, 333);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(87, 24);
            this.btnSave.TabIndex = 10150;
            this.btnSave.Text = "حفظ";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnChooseAccount
            // 
            this.btnChooseAccount.Enabled = false;
            this.btnChooseAccount.Location = new System.Drawing.Point(10, 104);
            this.btnChooseAccount.Name = "btnChooseAccount";
            this.btnChooseAccount.Size = new System.Drawing.Size(24, 20);
            this.btnChooseAccount.TabIndex = 10149;
            this.btnChooseAccount.Text = "...";
            this.btnChooseAccount.Click += new System.EventHandler(this.btnChooseAccount_Click);
            // 
            // txtNote
            // 
            this.txtNote.AutoSize = false;
            this.txtNote.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.guarantorBindingSource, "Note", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtNote.Location = new System.Drawing.Point(40, 248);
            this.txtNote.Multiline = true;
            this.txtNote.Name = "txtNote";
            this.txtNote.Size = new System.Drawing.Size(294, 70);
            this.txtNote.TabIndex = 10145;
            this.txtNote.Click += new System.EventHandler(this.HideNameAddressForms);
            // 
            // guarantorBindingSource
            // 
            this.guarantorBindingSource.DataSource = typeof(OrphanageDataModel.Persons.Guarantor);
            // 
            // txtAccountName
            // 
            this.txtAccountName.Location = new System.Drawing.Point(40, 104);
            this.txtAccountName.Name = "txtAccountName";
            this.txtAccountName.ReadOnly = true;
            this.txtAccountName.Size = new System.Drawing.Size(294, 20);
            this.txtAccountName.TabIndex = 10148;
            this.txtAccountName.TabStop = false;
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(40, 57);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.ReadOnly = true;
            this.txtAddress.Size = new System.Drawing.Size(294, 20);
            this.txtAddress.TabIndex = 10147;
            this.txtAddress.TabStop = false;
            this.txtAddress.Click += new System.EventHandler(this.txtAddress_Enter);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(40, 12);
            this.txtName.Name = "txtName";
            this.txtName.ReadOnly = true;
            this.txtName.Size = new System.Drawing.Size(294, 20);
            this.txtName.TabIndex = 10146;
            this.txtName.TabStop = false;
            this.txtName.Click += new System.EventHandler(this.txtName_Enter);
            // 
            // lblAccountName
            // 
            this.lblAccountName.AutoSize = false;
            this.lblAccountName.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAccountName.Location = new System.Drawing.Point(340, 106);
            this.lblAccountName.Name = "lblAccountName";
            this.lblAccountName.Size = new System.Drawing.Size(69, 17);
            this.lblAccountName.TabIndex = 10144;
            this.lblAccountName.Text = "الحساب :";
            this.lblAccountName.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            this.lblAccountName.Click += new System.EventHandler(this.HideNameAddressForms);
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = false;
            this.lblAddress.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAddress.Location = new System.Drawing.Point(340, 59);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(69, 17);
            this.lblAddress.TabIndex = 10143;
            this.lblAddress.Text = "العنوان :";
            this.lblAddress.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            this.lblAddress.Click += new System.EventHandler(this.HideNameAddressForms);
            // 
            // lblName
            // 
            this.lblName.AutoSize = false;
            this.lblName.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(340, 12);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(69, 17);
            this.lblName.TabIndex = 10142;
            this.lblName.Text = "الاسم :";
            this.lblName.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            this.lblName.Click += new System.EventHandler(this.HideNameAddressForms);
            // 
            // lblNotes
            // 
            this.lblNotes.AutoSize = false;
            this.lblNotes.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNotes.Location = new System.Drawing.Point(340, 248);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Size = new System.Drawing.Size(69, 17);
            this.lblNotes.TabIndex = 10138;
            this.lblNotes.Text = "ملاحظات :";
            this.lblNotes.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            this.lblNotes.Click += new System.EventHandler(this.HideNameAddressForms);
            // 
            // lblIsStillPaying
            // 
            this.lblIsStillPaying.AutoSize = false;
            this.lblIsStillPaying.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIsStillPaying.Location = new System.Drawing.Point(61, 155);
            this.lblIsStillPaying.Name = "lblIsStillPaying";
            this.lblIsStillPaying.Size = new System.Drawing.Size(60, 17);
            this.lblIsStillPaying.TabIndex = 10141;
            this.lblIsStillPaying.Text = "ملتزم :";
            this.lblIsStillPaying.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblOnlyFamilies
            // 
            this.lblOnlyFamilies.AutoSize = false;
            this.lblOnlyFamilies.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOnlyFamilies.Location = new System.Drawing.Point(195, 155);
            this.lblOnlyFamilies.Name = "lblOnlyFamilies";
            this.lblOnlyFamilies.Size = new System.Drawing.Size(64, 17);
            this.lblOnlyFamilies.TabIndex = 10140;
            this.lblOnlyFamilies.Text = "عائلات فقط :";
            this.lblOnlyFamilies.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblIsPayingMonthly
            // 
            this.lblIsPayingMonthly.AutoSize = false;
            this.lblIsPayingMonthly.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIsPayingMonthly.Location = new System.Drawing.Point(340, 155);
            this.lblIsPayingMonthly.Name = "lblIsPayingMonthly";
            this.lblIsPayingMonthly.Size = new System.Drawing.Size(69, 17);
            this.lblIsPayingMonthly.TabIndex = 10139;
            this.lblIsPayingMonthly.Text = "شهري :";
            this.lblIsPayingMonthly.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            this.lblIsPayingMonthly.Click += new System.EventHandler(this.HideNameAddressForms);
            // 
            // chkIsStillPaying
            // 
            this.chkIsStillPaying.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.guarantorBindingSource, "IsStillPaying", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkIsStillPaying.Location = new System.Drawing.Point(40, 155);
            this.chkIsStillPaying.Name = "chkIsStillPaying";
            this.chkIsStillPaying.Size = new System.Drawing.Size(15, 15);
            this.chkIsStillPaying.TabIndex = 10136;
            // 
            // chkIsSupportingOnlyFamilies
            // 
            this.chkIsSupportingOnlyFamilies.DataBindings.Add(new System.Windows.Forms.Binding("IsChecked", this.guarantorBindingSource, "IsSupportingOnlyFamilies", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkIsSupportingOnlyFamilies.Location = new System.Drawing.Point(174, 155);
            this.chkIsSupportingOnlyFamilies.Name = "chkIsSupportingOnlyFamilies";
            this.chkIsSupportingOnlyFamilies.Size = new System.Drawing.Size(15, 15);
            this.chkIsSupportingOnlyFamilies.TabIndex = 10137;
            // 
            // chkIsPayingMonthly
            // 
            this.chkIsPayingMonthly.DataBindings.Add(new System.Windows.Forms.Binding("IsChecked", this.guarantorBindingSource, "IsPayingMonthly", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkIsPayingMonthly.Location = new System.Drawing.Point(318, 155);
            this.chkIsPayingMonthly.Name = "chkIsPayingMonthly";
            this.chkIsPayingMonthly.Size = new System.Drawing.Size(15, 15);
            this.chkIsPayingMonthly.TabIndex = 10135;
            // 
            // clrColor
            // 
            this.clrColor.Location = new System.Drawing.Point(286, 200);
            this.clrColor.Name = "clrColor";
            this.clrColor.Size = new System.Drawing.Size(47, 20);
            this.clrColor.TabIndex = 10155;
            this.clrColor.Value = System.Drawing.Color.Black;
            // 
            // lblColor
            // 
            this.lblColor.Location = new System.Drawing.Point(339, 200);
            this.lblColor.Name = "lblColor";
            this.lblColor.Size = new System.Drawing.Size(58, 18);
            this.lblColor.TabIndex = 10154;
            this.lblColor.Text = "تمييز بلون :";
            this.lblColor.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            this.lblColor.Click += new System.EventHandler(this.HideNameAddressForms);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // nameForm1
            // 
            this.nameForm1.FocusWhenShow = true;
            this.nameForm1.HideOnEnter = false;
            this.nameForm1.Id = -1;
            this.nameForm1.Location = new System.Drawing.Point(3, 4);
            this.nameForm1.MoveFactor = 10;
            this.nameForm1.MoveType = OrphanageV3.Controlls.NameForm._MoveType.UpToDown;
            this.nameForm1.Name = "nameForm1";
            this.nameForm1.NameDataSource = typeof(OrphanageDataModel.RegularData.Name);
            this.nameForm1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.nameForm1.ShowMovement = false;
            this.nameForm1.Size = new System.Drawing.Size(355, 178);
            this.nameForm1.Style = Telerik.WinControls.UI.RadGroupBoxStyle.Office;
            this.nameForm1.TabIndex = 10153;
            this.nameForm1.Visible = false;
            // 
            // addressForm1
            // 
            this.addressForm1.AddressDataSource = typeof(OrphanageDataModel.RegularData.Address);
            this.addressForm1.HideOnEnter = false;
            this.addressForm1.Id = -1;
            this.addressForm1.Location = new System.Drawing.Point(7, 48);
            this.addressForm1.MoveFactor = 10;
            this.addressForm1.MoveType = OrphanageV3.Controlls.AddressForm._MoveType.UpToDown;
            this.addressForm1.Name = "addressForm1";
            this.addressForm1.ShowMovement = false;
            this.addressForm1.Size = new System.Drawing.Size(350, 268);
            this.addressForm1.Style = Telerik.WinControls.UI.RadGroupBoxStyle.Standard;
            this.addressForm1.TabIndex = 10152;
            this.addressForm1.Visible = false;
            // 
            // GuarantorEditView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(403, 376);
            this.Controls.Add(this.addressForm1);
            this.Controls.Add(this.nameForm1);
            this.Controls.Add(this.clrColor);
            this.Controls.Add(this.lblColor);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnChooseAccount);
            this.Controls.Add(this.txtNote);
            this.Controls.Add(this.txtAccountName);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblAccountName);
            this.Controls.Add(this.lblAddress);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.lblNotes);
            this.Controls.Add(this.lblIsStillPaying);
            this.Controls.Add(this.lblOnlyFamilies);
            this.Controls.Add(this.lblIsPayingMonthly);
            this.Controls.Add(this.chkIsStillPaying);
            this.Controls.Add(this.chkIsSupportingOnlyFamilies);
            this.Controls.Add(this.chkIsPayingMonthly);
            this.MaximizeBox = false;
            this.Name = "GuarantorEditView";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "GuarantorEditViewModel";
            this.Click += new System.EventHandler(this.HideNameAddressForms);
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnChooseAccount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNote)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.guarantorBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAccountName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblAccountName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblAddress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNotes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblIsStillPaying)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblOnlyFamilies)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblIsPayingMonthly)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsStillPaying)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsSupportingOnlyFamilies)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsPayingMonthly)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.clrColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal Telerik.WinControls.UI.RadButton btnCancel;
        internal Telerik.WinControls.UI.RadButton btnSave;
        internal Telerik.WinControls.UI.RadButton btnChooseAccount;
        internal Telerik.WinControls.UI.RadTextBox txtNote;
        internal Telerik.WinControls.UI.RadTextBox txtAccountName;
        internal Telerik.WinControls.UI.RadTextBox txtAddress;
        internal Telerik.WinControls.UI.RadTextBox txtName;
        internal Telerik.WinControls.UI.RadLabel lblAccountName;
        internal Telerik.WinControls.UI.RadLabel lblAddress;
        internal Telerik.WinControls.UI.RadLabel lblName;
        internal Telerik.WinControls.UI.RadLabel lblNotes;
        internal Telerik.WinControls.UI.RadLabel lblIsStillPaying;
        internal Telerik.WinControls.UI.RadLabel lblOnlyFamilies;
        internal Telerik.WinControls.UI.RadLabel lblIsPayingMonthly;
        internal Telerik.WinControls.UI.RadCheckBox chkIsStillPaying;
        internal Telerik.WinControls.UI.RadCheckBox chkIsSupportingOnlyFamilies;
        internal Telerik.WinControls.UI.RadCheckBox chkIsPayingMonthly;
        private Controlls.AddressForm addressForm1;
        private Controlls.NameForm nameForm1;
        private System.Windows.Forms.BindingSource guarantorBindingSource;
        internal Telerik.WinControls.UI.RadColorBox clrColor;
        internal Telerik.WinControls.UI.RadLabel lblColor;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}
