namespace OrphanageV3.Views.Caregiver
{
    partial class CaregiverEditView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CaregiverEditView));
            this.btnCancel = new Telerik.WinControls.UI.RadButton();
            this.btnSave = new Telerik.WinControls.UI.RadButton();
            this.grpIdentityCard = new Telerik.WinControls.UI.RadGroupBox();
            this.caregiverBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lblIdPhotoBack = new Telerik.WinControls.UI.RadLabel();
            this.lblIdPhotoFront = new Telerik.WinControls.UI.RadLabel();
            this.PicIDBack = new PictureSelector.PictureSelector();
            this.lblIdentityNumber = new Telerik.WinControls.UI.RadLabel();
            this.picIDFront = new PictureSelector.PictureSelector();
            this.grpData = new Telerik.WinControls.UI.RadGroupBox();
            this.clrColor = new Telerik.WinControls.UI.RadColorBox();
            this.lblColor = new Telerik.WinControls.UI.RadLabel();
            this.txtNote = new Telerik.WinControls.UI.RadTextBox();
            this.txtAddress = new Telerik.WinControls.UI.RadTextBox();
            this.txtName = new Telerik.WinControls.UI.RadTextBox();
            this.lblAddress = new Telerik.WinControls.UI.RadLabel();
            this.txtjop = new Telerik.WinControls.UI.RadTextBox();
            this.lblName = new Telerik.WinControls.UI.RadLabel();
            this.lblJob = new Telerik.WinControls.UI.RadLabel();
            this.lblNotes = new Telerik.WinControls.UI.RadLabel();
            this.lblIncome = new Telerik.WinControls.UI.RadLabel();
            this.numMonthlyIncome = new Telerik.WinControls.UI.RadSpinEditor();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.addressForm1 = new OrphanageV3.Controlls.AddressForm();
            this.nameForm1 = new OrphanageV3.Controlls.NameForm();
            this.txtIdentityCardId = new Telerik.WinControls.UI.RadTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpIdentityCard)).BeginInit();
            this.grpIdentityCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.caregiverBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblIdPhotoBack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblIdPhotoFront)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblIdentityNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpData)).BeginInit();
            this.grpData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.clrColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNote)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblAddress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtjop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblJob)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNotes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblIncome)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMonthlyIncome)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIdentityCardId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(239, 374);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 24);
            this.btnCancel.TabIndex = 57;
            this.btnCancel.Text = "إالغاء";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(150, 374);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(80, 24);
            this.btnSave.TabIndex = 58;
            this.btnSave.Text = "حفظ";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // grpIdentityCard
            // 
            this.grpIdentityCard.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this.grpIdentityCard.Controls.Add(this.lblIdPhotoBack);
            this.grpIdentityCard.Controls.Add(this.lblIdPhotoFront);
            this.grpIdentityCard.Controls.Add(this.PicIDBack);
            this.grpIdentityCard.Controls.Add(this.txtIdentityCardId);
            this.grpIdentityCard.Controls.Add(this.lblIdentityNumber);
            this.grpIdentityCard.Controls.Add(this.picIDFront);
            this.grpIdentityCard.HeaderText = "بطاقة شخصية";
            this.grpIdentityCard.Location = new System.Drawing.Point(6, 205);
            this.grpIdentityCard.Name = "grpIdentityCard";
            this.grpIdentityCard.Size = new System.Drawing.Size(457, 153);
            this.grpIdentityCard.TabIndex = 55;
            this.grpIdentityCard.Text = "بطاقة شخصية";
            this.grpIdentityCard.Click += new System.EventHandler(this.grpIdentityCard_Click);
            // 
            // caregiverBindingSource
            // 
            this.caregiverBindingSource.DataSource = typeof(OrphanageDataModel.Persons.Caregiver);
            // 
            // lblIdPhotoBack
            // 
            this.lblIdPhotoBack.Location = new System.Drawing.Point(196, 58);
            this.lblIdPhotoBack.Name = "lblIdPhotoBack";
            this.lblIdPhotoBack.Size = new System.Drawing.Size(67, 18);
            this.lblIdPhotoBack.TabIndex = 5;
            this.lblIdPhotoBack.Text = "صورة خلفية :";
            this.lblIdPhotoBack.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblIdPhotoFront
            // 
            this.lblIdPhotoFront.Location = new System.Drawing.Point(385, 58);
            this.lblIdPhotoFront.Name = "lblIdPhotoFront";
            this.lblIdPhotoFront.Size = new System.Drawing.Size(70, 18);
            this.lblIdPhotoFront.TabIndex = 5;
            this.lblIdPhotoFront.Text = "صورة امامية :";
            this.lblIdPhotoFront.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            // 
            // PicIDBack
            // 
            this.PicIDBack.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("PicIDBack.BackgroundImage")));
            this.PicIDBack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.PicIDBack.Location = new System.Drawing.Point(68, 45);
            this.PicIDBack.Name = "PicIDBack";
            this.PicIDBack.Photo = null;
            this.PicIDBack.Size = new System.Drawing.Size(122, 103);
            this.PicIDBack.TabIndex = 0;
            this.PicIDBack.PhotoChanged += new System.EventHandler(this.PhotoChanged);
            // 
            // lblIdentityNumber
            // 
            this.lblIdentityNumber.Location = new System.Drawing.Point(387, 21);
            this.lblIdentityNumber.Name = "lblIdentityNumber";
            this.lblIdentityNumber.Size = new System.Drawing.Size(28, 18);
            this.lblIdentityNumber.TabIndex = 5;
            this.lblIdentityNumber.Text = "رقم :";
            this.lblIdentityNumber.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            // 
            // picIDFront
            // 
            this.picIDFront.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picIDFront.BackgroundImage")));
            this.picIDFront.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.picIDFront.Location = new System.Drawing.Point(259, 45);
            this.picIDFront.Name = "picIDFront";
            this.picIDFront.Photo = null;
            this.picIDFront.Size = new System.Drawing.Size(122, 103);
            this.picIDFront.TabIndex = 0;
            this.picIDFront.PhotoChanged += new System.EventHandler(this.PhotoChanged);
            // 
            // grpData
            // 
            this.grpData.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this.grpData.Controls.Add(this.clrColor);
            this.grpData.Controls.Add(this.lblColor);
            this.grpData.Controls.Add(this.txtNote);
            this.grpData.Controls.Add(this.txtAddress);
            this.grpData.Controls.Add(this.txtName);
            this.grpData.Controls.Add(this.lblAddress);
            this.grpData.Controls.Add(this.txtjop);
            this.grpData.Controls.Add(this.lblName);
            this.grpData.Controls.Add(this.lblJob);
            this.grpData.Controls.Add(this.lblNotes);
            this.grpData.Controls.Add(this.lblIncome);
            this.grpData.Controls.Add(this.numMonthlyIncome);
            this.grpData.HeaderText = "بيانات";
            this.grpData.Location = new System.Drawing.Point(6, 12);
            this.grpData.Name = "grpData";
            this.grpData.Size = new System.Drawing.Size(457, 187);
            this.grpData.TabIndex = 56;
            this.grpData.Text = "بيانات";
            this.grpData.Click += new System.EventHandler(this.grpIdentityCard_Click);
            // 
            // clrColor
            // 
            this.clrColor.Location = new System.Drawing.Point(334, 112);
            this.clrColor.Name = "clrColor";
            this.clrColor.Size = new System.Drawing.Size(47, 20);
            this.clrColor.TabIndex = 10;
            this.clrColor.Value = System.Drawing.Color.Black;
            this.clrColor.ValueChanged += new System.EventHandler(this.clrColor_ValueChanged);
            // 
            // lblColor
            // 
            this.lblColor.Location = new System.Drawing.Point(381, 114);
            this.lblColor.Name = "lblColor";
            this.lblColor.Size = new System.Drawing.Size(58, 18);
            this.lblColor.TabIndex = 9;
            this.lblColor.Text = "تمييز بلون :";
            this.lblColor.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtNote
            // 
            this.txtNote.AutoSize = false;
            this.txtNote.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.caregiverBindingSource, "Note", true));
            this.txtNote.Location = new System.Drawing.Point(16, 41);
            this.txtNote.Multiline = true;
            this.txtNote.Name = "txtNote";
            this.txtNote.Size = new System.Drawing.Size(188, 91);
            this.txtNote.TabIndex = 4;
            this.txtNote.Click += new System.EventHandler(this.grpIdentityCard_Click);
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(16, 142);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.ReadOnly = true;
            this.txtAddress.Size = new System.Drawing.Size(365, 20);
            this.txtAddress.TabIndex = 4;
            this.txtAddress.Enter += new System.EventHandler(this.txtAddress_Enter);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(223, 16);
            this.txtName.Name = "txtName";
            this.txtName.ReadOnly = true;
            this.txtName.Size = new System.Drawing.Size(158, 20);
            this.txtName.TabIndex = 4;
            this.txtName.Enter += new System.EventHandler(this.txtName_Enter);
            // 
            // lblAddress
            // 
            this.lblAddress.Location = new System.Drawing.Point(381, 144);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(45, 18);
            this.lblAddress.TabIndex = 8;
            this.lblAddress.Text = "العنوان :";
            this.lblAddress.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtjop
            // 
            this.txtjop.AutoCompleteCustomSource.AddRange(new string[] {
            "نجار",
            "حداد",
            "جزار",
            "مزارع",
            "عامل",
            "سائق تاكسي",
            "سائق شاحنة",
            "سائق آلية ثقيلة",
            "موظف",
            "ممرض",
            "طبيب",
            "مهندس",
            "مدرس",
            "مترجم",
            "متعهد",
            "تاجر",
            "مساعد مهندس",
            "مراقب فني",
            "صيدلي",
            "فنان",
            "دهان"});
            this.txtjop.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtjop.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtjop.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.caregiverBindingSource, "Jop", true));
            this.txtjop.Location = new System.Drawing.Point(223, 48);
            this.txtjop.Name = "txtjop";
            this.txtjop.Size = new System.Drawing.Size(158, 20);
            this.txtjop.TabIndex = 4;
            // 
            // lblName
            // 
            this.lblName.Location = new System.Drawing.Point(381, 17);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(37, 18);
            this.lblName.TabIndex = 8;
            this.lblName.Text = "الاسم :";
            this.lblName.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            this.lblName.Click += new System.EventHandler(this.grpIdentityCard_Click);
            // 
            // lblJob
            // 
            this.lblJob.Location = new System.Drawing.Point(381, 49);
            this.lblJob.Name = "lblJob";
            this.lblJob.Size = new System.Drawing.Size(39, 18);
            this.lblJob.TabIndex = 8;
            this.lblJob.Text = "العمل :";
            this.lblJob.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            this.lblJob.Click += new System.EventHandler(this.grpIdentityCard_Click);
            // 
            // lblNotes
            // 
            this.lblNotes.Location = new System.Drawing.Point(158, 17);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Size = new System.Drawing.Size(54, 18);
            this.lblNotes.TabIndex = 6;
            this.lblNotes.Text = "ملاحظات :";
            this.lblNotes.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblIncome
            // 
            this.lblIncome.Location = new System.Drawing.Point(381, 82);
            this.lblIncome.Name = "lblIncome";
            this.lblIncome.Size = new System.Drawing.Size(75, 18);
            this.lblIncome.TabIndex = 6;
            this.lblIncome.Text = "الدخل الشهري :";
            this.lblIncome.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            // 
            // numMonthlyIncome
            // 
            this.numMonthlyIncome.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.caregiverBindingSource, "Income", true));
            this.numMonthlyIncome.Location = new System.Drawing.Point(223, 80);
            this.numMonthlyIncome.Maximum = new decimal(new int[] {
            20000,
            0,
            0,
            0});
            this.numMonthlyIncome.Name = "numMonthlyIncome";
            this.numMonthlyIncome.Size = new System.Drawing.Size(158, 20);
            this.numMonthlyIncome.TabIndex = 3;
            this.numMonthlyIncome.TabStop = false;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            this.errorProvider1.RightToLeft = true;
            // 
            // addressForm1
            // 
            this.addressForm1.AddressDataSource = typeof(OrphanageDataModel.RegularData.Address);
            this.addressForm1.HideOnEnter = false;
            this.addressForm1.Id = -1;
            this.addressForm1.Location = new System.Drawing.Point(3, 90);
            this.addressForm1.MoveFactor = 10;
            this.addressForm1.MoveType = OrphanageV3.Controlls.AddressForm._MoveType.UpToDown;
            this.addressForm1.Name = "addressForm1";
            this.addressForm1.ShowMovement = false;
            this.addressForm1.Size = new System.Drawing.Size(462, 268);
            this.addressForm1.TabIndex = 59;
            this.addressForm1.Visible = false;
            // 
            // nameForm1
            // 
            this.nameForm1.FocusWhenShow = true;
            this.nameForm1.HideOnEnter = false;
            this.nameForm1.Id = -1;
            this.nameForm1.Location = new System.Drawing.Point(71, 28);
            this.nameForm1.MoveFactor = 10;
            this.nameForm1.MoveType = OrphanageV3.Controlls.NameForm._MoveType.UpToDown;
            this.nameForm1.Name = "nameForm1";
            this.nameForm1.NameDataSource = typeof(OrphanageDataModel.RegularData.Name);
            this.nameForm1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.nameForm1.ShowMovement = false;
            this.nameForm1.Size = new System.Drawing.Size(376, 178);
            this.nameForm1.TabIndex = 60;
            this.nameForm1.Visible = false;
            // 
            // txtIdentityCardId
            // 
            this.txtIdentityCardId.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.caregiverBindingSource, "IdentityCardId", true));
            this.txtIdentityCardId.Location = new System.Drawing.Point(16, 21);
            this.txtIdentityCardId.Name = "txtIdentityCardId";
            this.txtIdentityCardId.Size = new System.Drawing.Size(365, 20);
            this.txtIdentityCardId.TabIndex = 4;
            // 
            // CaregiverEditView
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(470, 410);
            this.Controls.Add(this.addressForm1);
            this.Controls.Add(this.nameForm1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.grpIdentityCard);
            this.Controls.Add(this.grpData);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CaregiverEditView";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "CaregiverEditView";
            this.Load += new System.EventHandler(this.CaregiverEditView_Load);
            this.Click += new System.EventHandler(this.grpIdentityCard_Click);
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpIdentityCard)).EndInit();
            this.grpIdentityCard.ResumeLayout(false);
            this.grpIdentityCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.caregiverBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblIdPhotoBack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblIdPhotoFront)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblIdentityNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpData)).EndInit();
            this.grpData.ResumeLayout(false);
            this.grpData.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.clrColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNote)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblAddress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtjop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblJob)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNotes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblIncome)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMonthlyIncome)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIdentityCardId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal Telerik.WinControls.UI.RadButton btnCancel;
        internal Telerik.WinControls.UI.RadButton btnSave;
        internal Telerik.WinControls.UI.RadGroupBox grpIdentityCard;
        internal Telerik.WinControls.UI.RadLabel lblIdPhotoBack;
        internal Telerik.WinControls.UI.RadLabel lblIdPhotoFront;
        internal PictureSelector.PictureSelector PicIDBack;
        internal Telerik.WinControls.UI.RadLabel lblIdentityNumber;
        internal PictureSelector.PictureSelector picIDFront;
        internal Telerik.WinControls.UI.RadGroupBox grpData;
        internal Telerik.WinControls.UI.RadColorBox clrColor;
        internal Telerik.WinControls.UI.RadLabel lblColor;
        internal Telerik.WinControls.UI.RadTextBox txtNote;
        internal Telerik.WinControls.UI.RadTextBox txtAddress;
        internal Telerik.WinControls.UI.RadTextBox txtName;
        internal Telerik.WinControls.UI.RadLabel lblAddress;
        internal Telerik.WinControls.UI.RadTextBox txtjop;
        internal Telerik.WinControls.UI.RadLabel lblName;
        internal Telerik.WinControls.UI.RadLabel lblJob;
        internal Telerik.WinControls.UI.RadLabel lblNotes;
        internal Telerik.WinControls.UI.RadLabel lblIncome;
        internal Telerik.WinControls.UI.RadSpinEditor numMonthlyIncome;
        private System.Windows.Forms.BindingSource caregiverBindingSource;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private Controlls.AddressForm addressForm1;
        private Controlls.NameForm nameForm1;
        internal Telerik.WinControls.UI.RadTextBox txtIdentityCardId;
    }
}