namespace OrphanageV3.Views.Father
{
    partial class FatherEditView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FatherEditView));
            this.FlowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnCancel = new Telerik.WinControls.UI.RadButton();
            this.btnSave = new Telerik.WinControls.UI.RadButton();
            this.grpOtherData = new Telerik.WinControls.UI.RadGroupBox();
            this.clrColor = new Telerik.WinControls.UI.RadColorBox();
            this.picDeathCertificate = new PictureSelector.PictureSelector();
            this.picPhoto = new PictureSelector.PictureSelector();
            this.lblNote = new Telerik.WinControls.UI.RadLabel();
            this.txtNote = new Telerik.WinControls.UI.RadTextBox();
            this.fatherBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lblDeathCertificatePhoto = new Telerik.WinControls.UI.RadLabel();
            this.lblPersonalPhoto = new Telerik.WinControls.UI.RadLabel();
            this.lblColor = new Telerik.WinControls.UI.RadLabel();
            this.lblStory = new Telerik.WinControls.UI.RadLabel();
            this.txtStory = new Telerik.WinControls.UI.RadTextBox();
            this.grpBasicData = new Telerik.WinControls.UI.RadGroupBox();
            this.txtName = new Telerik.WinControls.UI.RadTextBox();
            this.dteBirthday = new Telerik.WinControls.UI.RadDateTimePicker();
            this.lblIdentityNumber = new Telerik.WinControls.UI.RadLabel();
            this.lblName = new Telerik.WinControls.UI.RadLabel();
            this.lblJob = new Telerik.WinControls.UI.RadLabel();
            this.dteDateOfDeath = new Telerik.WinControls.UI.RadDateTimePicker();
            this.lblDeathReason = new Telerik.WinControls.UI.RadLabel();
            this.lblDateOfDeath = new Telerik.WinControls.UI.RadLabel();
            this.txtJop = new Telerik.WinControls.UI.RadTextBox();
            this.lblBirthday = new Telerik.WinControls.UI.RadLabel();
            this.txtIdentityCardNumber = new Telerik.WinControls.UI.RadTextBox();
            this.txtDeathReason = new Telerik.WinControls.UI.RadTextBox();
            this.fatherErrorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.nameForm1 = new OrphanageV3.Controlls.NameForm();
            this.FlowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpOtherData)).BeginInit();
            this.grpOtherData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.clrColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNote)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNote)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fatherBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDeathCertificatePhoto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblPersonalPhoto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblStory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpBasicData)).BeginInit();
            this.grpBasicData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteBirthday)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblIdentityNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblJob)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteDateOfDeath)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDeathReason)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDateOfDeath)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtJop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblBirthday)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIdentityCardNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDeathReason)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fatherErrorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // FlowLayoutPanel1
            // 
            this.FlowLayoutPanel1.Controls.Add(this.btnCancel);
            this.FlowLayoutPanel1.Controls.Add(this.btnSave);
            this.FlowLayoutPanel1.Location = new System.Drawing.Point(138, 294);
            this.FlowLayoutPanel1.Name = "FlowLayoutPanel1";
            this.FlowLayoutPanel1.Size = new System.Drawing.Size(233, 28);
            this.FlowLayoutPanel1.TabIndex = 10;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(120, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(110, 24);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "إلغاء الأمر";
            this.btnCancel.ThemeName = "ControlDefault";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(4, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(110, 24);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "موافق";
            this.btnSave.ThemeName = "ControlDefault";
            this.btnSave.Click += new System.EventHandler(this.BtnOK_Click);
            // 
            // grpOtherData
            // 
            this.grpOtherData.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this.grpOtherData.Controls.Add(this.clrColor);
            this.grpOtherData.Controls.Add(this.picDeathCertificate);
            this.grpOtherData.Controls.Add(this.picPhoto);
            this.grpOtherData.Controls.Add(this.lblNote);
            this.grpOtherData.Controls.Add(this.txtNote);
            this.grpOtherData.Controls.Add(this.lblDeathCertificatePhoto);
            this.grpOtherData.Controls.Add(this.lblPersonalPhoto);
            this.grpOtherData.Controls.Add(this.lblColor);
            this.grpOtherData.Controls.Add(this.lblStory);
            this.grpOtherData.Controls.Add(this.txtStory);
            this.grpOtherData.HeaderText = "بيانات أخرى";
            this.grpOtherData.Location = new System.Drawing.Point(12, 126);
            this.grpOtherData.Name = "grpOtherData";
            this.grpOtherData.Size = new System.Drawing.Size(485, 159);
            this.grpOtherData.TabIndex = 9;
            this.grpOtherData.Text = "بيانات أخرى";
            this.grpOtherData.Click += new System.EventHandler(this.HideNameAddressForms);
            this.grpOtherData.Enter += new System.EventHandler(this.HideNameAddressForms);
            // 
            // clrColor
            // 
            this.clrColor.Location = new System.Drawing.Point(368, 125);
            this.clrColor.Name = "clrColor";
            this.clrColor.Size = new System.Drawing.Size(47, 20);
            this.clrColor.TabIndex = 7;
            this.clrColor.Value = System.Drawing.Color.Black;
            this.clrColor.ValueChanged += new System.EventHandler(this.clrColor_ValueChanged);
            // 
            // picDeathCertificate
            // 
            this.picDeathCertificate.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picDeathCertificate.BackgroundImage")));
            this.picDeathCertificate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.picDeathCertificate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picDeathCertificate.Location = new System.Drawing.Point(5, 39);
            this.picDeathCertificate.Name = "picDeathCertificate";
            this.picDeathCertificate.Photo = null;
            this.picDeathCertificate.Size = new System.Drawing.Size(110, 80);
            this.picDeathCertificate.TabIndex = 3;
            this.picDeathCertificate.PhotoChanged += new System.EventHandler(this.PhotoChanged);
            // 
            // picPhoto
            // 
            this.picPhoto.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picPhoto.BackgroundImage")));
            this.picPhoto.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.picPhoto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picPhoto.Location = new System.Drawing.Point(121, 39);
            this.picPhoto.Name = "picPhoto";
            this.picPhoto.Photo = null;
            this.picPhoto.Size = new System.Drawing.Size(110, 80);
            this.picPhoto.TabIndex = 3;
            this.picPhoto.PhotoChanged += new System.EventHandler(this.PhotoChanged);
            // 
            // lblNote
            // 
            this.lblNote.AutoSize = false;
            this.lblNote.Location = new System.Drawing.Point(310, 21);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(54, 18);
            this.lblNote.TabIndex = 2;
            this.lblNote.Text = "ملاحظات :";
            this.lblNote.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtNote
            // 
            this.txtNote.AutoSize = false;
            this.txtNote.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.fatherBindingSource, "Note", true));
            this.txtNote.Location = new System.Drawing.Point(252, 39);
            this.txtNote.Multiline = true;
            this.txtNote.Name = "txtNote";
            this.txtNote.Size = new System.Drawing.Size(110, 80);
            this.txtNote.TabIndex = 0;
            this.txtNote.Click += new System.EventHandler(this.HideNameAddressForms);
            this.txtNote.Enter += new System.EventHandler(this.HideNameAddressForms);
            // 
            // fatherBindingSource
            // 
            this.fatherBindingSource.DataSource = typeof(OrphanageDataModel.Persons.Father);
            // 
            // lblDeathCertificatePhoto
            // 
            this.lblDeathCertificatePhoto.AutoSize = false;
            this.lblDeathCertificatePhoto.Location = new System.Drawing.Point(15, 21);
            this.lblDeathCertificatePhoto.Name = "lblDeathCertificatePhoto";
            this.lblDeathCertificatePhoto.Size = new System.Drawing.Size(100, 18);
            this.lblDeathCertificatePhoto.TabIndex = 2;
            this.lblDeathCertificatePhoto.Text = "صورة شهادة الوفاة :";
            this.lblDeathCertificatePhoto.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            this.lblDeathCertificatePhoto.Click += new System.EventHandler(this.HideNameAddressForms);
            this.lblDeathCertificatePhoto.Enter += new System.EventHandler(this.HideNameAddressForms);
            // 
            // lblPersonalPhoto
            // 
            this.lblPersonalPhoto.AutoSize = false;
            this.lblPersonalPhoto.Location = new System.Drawing.Point(155, 21);
            this.lblPersonalPhoto.Name = "lblPersonalPhoto";
            this.lblPersonalPhoto.Size = new System.Drawing.Size(78, 18);
            this.lblPersonalPhoto.TabIndex = 2;
            this.lblPersonalPhoto.Text = "صورة شخصية :";
            this.lblPersonalPhoto.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            this.lblPersonalPhoto.Click += new System.EventHandler(this.HideNameAddressForms);
            this.lblPersonalPhoto.Enter += new System.EventHandler(this.HideNameAddressForms);
            // 
            // lblColor
            // 
            this.lblColor.AutoSize = false;
            this.lblColor.Location = new System.Drawing.Point(412, 127);
            this.lblColor.Name = "lblColor";
            this.lblColor.Size = new System.Drawing.Size(66, 18);
            this.lblColor.TabIndex = 2;
            this.lblColor.Text = "تمييز بلون :";
            this.lblColor.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            this.lblColor.Click += new System.EventHandler(this.HideNameAddressForms);
            this.lblColor.Enter += new System.EventHandler(this.HideNameAddressForms);
            // 
            // lblStory
            // 
            this.lblStory.AutoSize = false;
            this.lblStory.Location = new System.Drawing.Point(412, 21);
            this.lblStory.Name = "lblStory";
            this.lblStory.Size = new System.Drawing.Size(67, 18);
            this.lblStory.TabIndex = 2;
            this.lblStory.Text = "قصة :";
            this.lblStory.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtStory
            // 
            this.txtStory.AutoSize = false;
            this.txtStory.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.fatherBindingSource, "Story", true));
            this.txtStory.Location = new System.Drawing.Point(368, 39);
            this.txtStory.Multiline = true;
            this.txtStory.Name = "txtStory";
            this.txtStory.Size = new System.Drawing.Size(110, 80);
            this.txtStory.TabIndex = 0;
            this.txtStory.Click += new System.EventHandler(this.HideNameAddressForms);
            this.txtStory.Enter += new System.EventHandler(this.HideNameAddressForms);
            // 
            // grpBasicData
            // 
            this.grpBasicData.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this.grpBasicData.Controls.Add(this.txtName);
            this.grpBasicData.Controls.Add(this.dteBirthday);
            this.grpBasicData.Controls.Add(this.lblIdentityNumber);
            this.grpBasicData.Controls.Add(this.lblName);
            this.grpBasicData.Controls.Add(this.lblJob);
            this.grpBasicData.Controls.Add(this.dteDateOfDeath);
            this.grpBasicData.Controls.Add(this.lblDeathReason);
            this.grpBasicData.Controls.Add(this.lblDateOfDeath);
            this.grpBasicData.Controls.Add(this.txtJop);
            this.grpBasicData.Controls.Add(this.lblBirthday);
            this.grpBasicData.Controls.Add(this.txtIdentityCardNumber);
            this.grpBasicData.Controls.Add(this.txtDeathReason);
            this.grpBasicData.HeaderText = "بيانات اساسية";
            this.grpBasicData.Location = new System.Drawing.Point(12, 12);
            this.grpBasicData.Name = "grpBasicData";
            this.grpBasicData.Size = new System.Drawing.Size(485, 108);
            this.grpBasicData.TabIndex = 8;
            this.grpBasicData.Text = "بيانات اساسية";
            this.grpBasicData.Click += new System.EventHandler(this.HideNameAddressForms);
            this.grpBasicData.Enter += new System.EventHandler(this.HideNameAddressForms);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(270, 21);
            this.txtName.Name = "txtName";
            this.txtName.ReadOnly = true;
            this.txtName.Size = new System.Drawing.Size(144, 20);
            this.txtName.TabIndex = 0;
            this.txtName.Click += new System.EventHandler(this.txtName_Enter);
            this.txtName.Enter += new System.EventHandler(this.txtName_Enter);
            // 
            // dteBirthday
            // 
            this.dteBirthday.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.fatherBindingSource, "Birthday", true));
            this.dteBirthday.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dteBirthday.Location = new System.Drawing.Point(270, 46);
            this.dteBirthday.Name = "dteBirthday";
            this.dteBirthday.Size = new System.Drawing.Size(144, 20);
            this.dteBirthday.TabIndex = 1;
            this.dteBirthday.TabStop = false;
            this.dteBirthday.Text = "27.01.2014";
            this.dteBirthday.Value = new System.DateTime(2014, 1, 27, 14, 35, 14, 685);
            // 
            // lblIdentityNumber
            // 
            this.lblIdentityNumber.AutoSize = false;
            this.lblIdentityNumber.Location = new System.Drawing.Point(170, 46);
            this.lblIdentityNumber.Name = "lblIdentityNumber";
            this.lblIdentityNumber.Size = new System.Drawing.Size(63, 18);
            this.lblIdentityNumber.TabIndex = 2;
            this.lblIdentityNumber.Text = "رقم الهوية :";
            this.lblIdentityNumber.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblName
            // 
            this.lblName.AutoSize = false;
            this.lblName.Location = new System.Drawing.Point(412, 21);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(66, 18);
            this.lblName.TabIndex = 2;
            this.lblName.Text = "الاسم :";
            this.lblName.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblJob
            // 
            this.lblJob.AutoSize = false;
            this.lblJob.Location = new System.Drawing.Point(170, 69);
            this.lblJob.Name = "lblJob";
            this.lblJob.Size = new System.Drawing.Size(48, 18);
            this.lblJob.TabIndex = 2;
            this.lblJob.Text = "العمل :";
            this.lblJob.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            // 
            // dteDateOfDeath
            // 
            this.dteDateOfDeath.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.fatherBindingSource, "DateOfDeath", true));
            this.dteDateOfDeath.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dteDateOfDeath.Location = new System.Drawing.Point(270, 71);
            this.dteDateOfDeath.Name = "dteDateOfDeath";
            this.dteDateOfDeath.Size = new System.Drawing.Size(144, 20);
            this.dteDateOfDeath.TabIndex = 1;
            this.dteDateOfDeath.TabStop = false;
            this.dteDateOfDeath.Text = "27.01.2014";
            this.dteDateOfDeath.Value = new System.DateTime(2014, 1, 27, 14, 35, 14, 685);
            // 
            // lblDeathReason
            // 
            this.lblDeathReason.AutoSize = false;
            this.lblDeathReason.Location = new System.Drawing.Point(170, 21);
            this.lblDeathReason.Name = "lblDeathReason";
            this.lblDeathReason.Size = new System.Drawing.Size(69, 18);
            this.lblDeathReason.TabIndex = 2;
            this.lblDeathReason.Text = "سبب الوفاة :";
            this.lblDeathReason.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblDateOfDeath
            // 
            this.lblDateOfDeath.AutoSize = false;
            this.lblDateOfDeath.Location = new System.Drawing.Point(412, 71);
            this.lblDateOfDeath.Name = "lblDateOfDeath";
            this.lblDateOfDeath.Size = new System.Drawing.Size(72, 18);
            this.lblDateOfDeath.TabIndex = 2;
            this.lblDateOfDeath.Text = "تاريخ الوفاة :";
            this.lblDateOfDeath.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtJop
            // 
            this.txtJop.AutoCompleteCustomSource.AddRange(new string[] {
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
            this.txtJop.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtJop.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtJop.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.fatherBindingSource, "Jop", true));
            this.txtJop.Location = new System.Drawing.Point(20, 71);
            this.txtJop.Name = "txtJop";
            this.txtJop.Size = new System.Drawing.Size(144, 20);
            this.txtJop.TabIndex = 0;
            this.txtJop.Click += new System.EventHandler(this.HideNameAddressForms);
            this.txtJop.Enter += new System.EventHandler(this.HideNameAddressForms);
            // 
            // lblBirthday
            // 
            this.lblBirthday.AutoSize = false;
            this.lblBirthday.Location = new System.Drawing.Point(412, 46);
            this.lblBirthday.Name = "lblBirthday";
            this.lblBirthday.Size = new System.Drawing.Size(76, 18);
            this.lblBirthday.TabIndex = 2;
            this.lblBirthday.Text = "تاريخ الولادة :";
            this.lblBirthday.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtIdentityCardNumber
            // 
            this.txtIdentityCardNumber.AutoCompleteCustomSource.AddRange(new string[] {
            "سكتة قلبية",
            "سكتة دماغية",
            "طلق ناري",
            "شظية",
            "مرض مزمن",
            "مرض خبيث",
            "غير معروف"});
            this.txtIdentityCardNumber.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtIdentityCardNumber.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtIdentityCardNumber.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.fatherBindingSource, "IdentityCardNumber", true));
            this.txtIdentityCardNumber.Location = new System.Drawing.Point(20, 46);
            this.txtIdentityCardNumber.Name = "txtIdentityCardNumber";
            this.txtIdentityCardNumber.Size = new System.Drawing.Size(144, 20);
            this.txtIdentityCardNumber.TabIndex = 0;
            this.txtIdentityCardNumber.Click += new System.EventHandler(this.HideNameAddressForms);
            this.txtIdentityCardNumber.Enter += new System.EventHandler(this.HideNameAddressForms);
            // 
            // txtDeathReason
            // 
            this.txtDeathReason.AutoCompleteCustomSource.AddRange(new string[] {
            "سكتة قلبية",
            "سكتة دماغية",
            "طلق ناري",
            "شظية",
            "مرض مزمن",
            "مرض خبيث",
            "غير معروف"});
            this.txtDeathReason.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtDeathReason.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtDeathReason.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.fatherBindingSource, "DeathReason", true));
            this.txtDeathReason.Location = new System.Drawing.Point(20, 21);
            this.txtDeathReason.Name = "txtDeathReason";
            this.txtDeathReason.Size = new System.Drawing.Size(144, 20);
            this.txtDeathReason.TabIndex = 0;
            this.txtDeathReason.Click += new System.EventHandler(this.HideNameAddressForms);
            this.txtDeathReason.Enter += new System.EventHandler(this.HideNameAddressForms);
            // 
            // fatherErrorProvider1
            // 
            this.fatherErrorProvider1.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.fatherErrorProvider1.ContainerControl = this;
            // 
            // nameForm1
            // 
            this.nameForm1.FocusWhenShow = true;
            this.nameForm1.HideOnEnter = false;
            this.nameForm1.Id = -1;
            this.nameForm1.Location = new System.Drawing.Point(273, 33);
            this.nameForm1.MoveFactor = 10;
            this.nameForm1.MoveType = OrphanageV3.Controlls.NameForm._MoveType.UpToDown;
            this.nameForm1.Name = "nameForm1";
            this.nameForm1.NameDataSource = typeof(OrphanageDataModel.RegularData.Name);
            this.nameForm1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.nameForm1.ShowMovement = false;
            this.nameForm1.Size = new System.Drawing.Size(207, 178);
            this.nameForm1.Style = Telerik.WinControls.UI.RadGroupBoxStyle.Office;
            this.nameForm1.TabIndex = 11;
            this.nameForm1.Visible = false;
            // 
            // FatherEditView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(504, 329);
            this.Controls.Add(this.nameForm1);
            this.Controls.Add(this.FlowLayoutPanel1);
            this.Controls.Add(this.grpOtherData);
            this.Controls.Add(this.grpBasicData);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FatherEditView";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "RadForm1";
            this.Click += new System.EventHandler(this.HideNameAddressForms);
            this.Enter += new System.EventHandler(this.HideNameAddressForms);
            this.FlowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpOtherData)).EndInit();
            this.grpOtherData.ResumeLayout(false);
            this.grpOtherData.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.clrColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNote)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNote)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fatherBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDeathCertificatePhoto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblPersonalPhoto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblStory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpBasicData)).EndInit();
            this.grpBasicData.ResumeLayout(false);
            this.grpBasicData.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteBirthday)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblIdentityNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblJob)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteDateOfDeath)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDeathReason)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDateOfDeath)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtJop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblBirthday)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIdentityCardNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDeathReason)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fatherErrorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.FlowLayoutPanel FlowLayoutPanel1;
        internal Telerik.WinControls.UI.RadButton btnCancel;
        internal Telerik.WinControls.UI.RadButton btnSave;
        internal Telerik.WinControls.UI.RadGroupBox grpOtherData;
        internal Telerik.WinControls.UI.RadColorBox clrColor;
        internal PictureSelector.PictureSelector picDeathCertificate;
        internal PictureSelector.PictureSelector picPhoto;
        internal Telerik.WinControls.UI.RadLabel lblNote;
        internal Telerik.WinControls.UI.RadTextBox txtNote;
        internal Telerik.WinControls.UI.RadLabel lblDeathCertificatePhoto;
        internal Telerik.WinControls.UI.RadLabel lblPersonalPhoto;
        internal Telerik.WinControls.UI.RadLabel lblColor;
        internal Telerik.WinControls.UI.RadLabel lblStory;
        internal Telerik.WinControls.UI.RadTextBox txtStory;
        internal Telerik.WinControls.UI.RadGroupBox grpBasicData;
        internal Telerik.WinControls.UI.RadTextBox txtName;
        internal Telerik.WinControls.UI.RadDateTimePicker dteBirthday;
        internal Telerik.WinControls.UI.RadLabel lblIdentityNumber;
        internal Telerik.WinControls.UI.RadLabel lblName;
        internal Telerik.WinControls.UI.RadLabel lblJob;
        internal Telerik.WinControls.UI.RadDateTimePicker dteDateOfDeath;
        internal Telerik.WinControls.UI.RadLabel lblDeathReason;
        internal Telerik.WinControls.UI.RadLabel lblDateOfDeath;
        internal Telerik.WinControls.UI.RadTextBox txtJop;
        internal Telerik.WinControls.UI.RadLabel lblBirthday;
        internal Telerik.WinControls.UI.RadTextBox txtDeathReason;
        private System.Windows.Forms.BindingSource fatherBindingSource;
        private System.Windows.Forms.ErrorProvider fatherErrorProvider1;
        private Controlls.NameForm nameForm1;
        internal Telerik.WinControls.UI.RadTextBox txtIdentityCardNumber;
    }
}
