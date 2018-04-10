namespace OrphanageV3.Views.Family
{
    partial class FamilyEditView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FamilyEditView));
            this.FlowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnCancel = new Telerik.WinControls.UI.RadButton();
            this.btnOK = new Telerik.WinControls.UI.RadButton();
            this.RadGroupBox3 = new Telerik.WinControls.UI.RadGroupBox();
            this.txtAddress2 = new Telerik.WinControls.UI.RadTextBox();
            this.txtAddress = new Telerik.WinControls.UI.RadTextBox();
            this.lblCurrentAddress = new Telerik.WinControls.UI.RadLabel();
            this.lblPrimaryAddress = new Telerik.WinControls.UI.RadLabel();
            this.grpFamilyCardPhotoP1 = new Telerik.WinControls.UI.RadGroupBox();
            this.picCardphoto1 = new PictureSelector.PictureSelector();
            this.grpFamilyCardPhotoP2 = new Telerik.WinControls.UI.RadGroupBox();
            this.picCardPhoto2 = new PictureSelector.PictureSelector();
            this.grpBasicData = new Telerik.WinControls.UI.RadGroupBox();
            this.txtNote = new Telerik.WinControls.UI.RadTextBox();
            this.familyBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.txtFamilyCardNumber = new Telerik.WinControls.UI.RadTextBox();
            this.txtFinncialStatus = new Telerik.WinControls.UI.RadDropDownList();
            this.txtResidenceStatus = new Telerik.WinControls.UI.RadDropDownList();
            this.txtResidenceType = new Telerik.WinControls.UI.RadDropDownList();
            this.chkIsTheyRefugees = new Telerik.WinControls.UI.RadCheckBox();
            this.lblResidenceStatus = new Telerik.WinControls.UI.RadLabel();
            this.lblNote = new Telerik.WinControls.UI.RadLabel();
            this.lblResidenceType = new Telerik.WinControls.UI.RadLabel();
            this.lblIsTheyRefugees = new Telerik.WinControls.UI.RadLabel();
            this.lblFamilyCardNumber = new Telerik.WinControls.UI.RadLabel();
            this.lblFinncialStatus = new Telerik.WinControls.UI.RadLabel();
            this.familyErrorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.addressForm1 = new OrphanageV3.Controlls.AddressForm();
            this.addressForm2 = new OrphanageV3.Controlls.AddressForm();
            this.FlowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RadGroupBox3)).BeginInit();
            this.RadGroupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddress2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCurrentAddress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblPrimaryAddress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpFamilyCardPhotoP1)).BeginInit();
            this.grpFamilyCardPhotoP1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpFamilyCardPhotoP2)).BeginInit();
            this.grpFamilyCardPhotoP2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpBasicData)).BeginInit();
            this.grpBasicData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNote)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.familyBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFamilyCardNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFinncialStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtResidenceStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtResidenceType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsTheyRefugees)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblResidenceStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNote)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblResidenceType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblIsTheyRefugees)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFamilyCardNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFinncialStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.familyErrorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // FlowLayoutPanel1
            // 
            this.FlowLayoutPanel1.Controls.Add(this.btnCancel);
            this.FlowLayoutPanel1.Controls.Add(this.btnOK);
            this.FlowLayoutPanel1.Location = new System.Drawing.Point(99, 375);
            this.FlowLayoutPanel1.Name = "FlowLayoutPanel1";
            this.FlowLayoutPanel1.Size = new System.Drawing.Size(233, 28);
            this.FlowLayoutPanel1.TabIndex = 36;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(120, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(110, 24);
            this.btnCancel.TabIndex = 18;
            this.btnCancel.Text = "إلغاء الأمر";
            this.btnCancel.ThemeName = "ControlDefault";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(4, 3);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(110, 24);
            this.btnOK.TabIndex = 17;
            this.btnOK.Text = "موافق";
            this.btnOK.ThemeName = "ControlDefault";
            this.btnOK.Click += new System.EventHandler(this.BtnOK_Click);
            // 
            // RadGroupBox3
            // 
            this.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this.RadGroupBox3.Controls.Add(this.txtAddress2);
            this.RadGroupBox3.Controls.Add(this.txtAddress);
            this.RadGroupBox3.Controls.Add(this.lblCurrentAddress);
            this.RadGroupBox3.Controls.Add(this.lblPrimaryAddress);
            this.RadGroupBox3.HeaderText = "";
            this.RadGroupBox3.Location = new System.Drawing.Point(6, 291);
            this.RadGroupBox3.Name = "RadGroupBox3";
            this.RadGroupBox3.Size = new System.Drawing.Size(426, 78);
            this.RadGroupBox3.TabIndex = 35;
            this.RadGroupBox3.Click += new System.EventHandler(this.HideNameAddressForms);
            // 
            // txtAddress2
            // 
            this.txtAddress2.Location = new System.Drawing.Point(23, 46);
            this.txtAddress2.Name = "txtAddress2";
            this.txtAddress2.ReadOnly = true;
            this.txtAddress2.Size = new System.Drawing.Size(317, 20);
            this.txtAddress2.TabIndex = 29;
            this.txtAddress2.Click += new System.EventHandler(this.txtAddress2_Click);
            this.txtAddress2.Enter += new System.EventHandler(this.txtAddress2_Click);
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(23, 21);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.ReadOnly = true;
            this.txtAddress.Size = new System.Drawing.Size(317, 20);
            this.txtAddress.TabIndex = 29;
            this.txtAddress.Click += new System.EventHandler(this.txtAddress_Enter);
            this.txtAddress.Enter += new System.EventHandler(this.txtAddress_Enter);
            // 
            // lblCurrentAddress
            // 
            this.lblCurrentAddress.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblCurrentAddress.AutoSize = false;
            this.lblCurrentAddress.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentAddress.Location = new System.Drawing.Point(348, 46);
            this.lblCurrentAddress.Name = "lblCurrentAddress";
            this.lblCurrentAddress.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCurrentAddress.Size = new System.Drawing.Size(68, 17);
            this.lblCurrentAddress.TabIndex = 1;
            this.lblCurrentAddress.Text = "العنوان الحالي :";
            this.lblCurrentAddress.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPrimaryAddress
            // 
            this.lblPrimaryAddress.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblPrimaryAddress.AutoSize = false;
            this.lblPrimaryAddress.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrimaryAddress.Location = new System.Drawing.Point(347, 21);
            this.lblPrimaryAddress.Name = "lblPrimaryAddress";
            this.lblPrimaryAddress.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblPrimaryAddress.Size = new System.Drawing.Size(73, 17);
            this.lblPrimaryAddress.TabIndex = 1;
            this.lblPrimaryAddress.Text = "العنوان الأصلي :";
            this.lblPrimaryAddress.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // grpFamilyCardPhotoP1
            // 
            this.grpFamilyCardPhotoP1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this.grpFamilyCardPhotoP1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.grpFamilyCardPhotoP1.Controls.Add(this.picCardphoto1);
            this.grpFamilyCardPhotoP1.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpFamilyCardPhotoP1.GroupBoxStyle = Telerik.WinControls.UI.RadGroupBoxStyle.Office;
            this.grpFamilyCardPhotoP1.HeaderText = "";
            this.grpFamilyCardPhotoP1.Location = new System.Drawing.Point(6, 7);
            this.grpFamilyCardPhotoP1.Name = "grpFamilyCardPhotoP1";
            this.grpFamilyCardPhotoP1.Size = new System.Drawing.Size(168, 140);
            this.grpFamilyCardPhotoP1.TabIndex = 34;
            this.grpFamilyCardPhotoP1.Click += new System.EventHandler(this.HideNameAddressForms);
            // 
            // picCardphoto1
            // 
            this.picCardphoto1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picCardphoto1.BackgroundImage")));
            this.picCardphoto1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.picCardphoto1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picCardphoto1.Location = new System.Drawing.Point(2, 18);
            this.picCardphoto1.Name = "picCardphoto1";
            this.picCardphoto1.Photo = null;
            this.picCardphoto1.Size = new System.Drawing.Size(164, 120);
            this.picCardphoto1.TabIndex = 26;
            this.picCardphoto1.TabStop = false;
            this.picCardphoto1.PhotoChanged += new System.EventHandler(this.PhotoChanged);
            // 
            // grpFamilyCardPhotoP2
            // 
            this.grpFamilyCardPhotoP2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this.grpFamilyCardPhotoP2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.grpFamilyCardPhotoP2.Controls.Add(this.picCardPhoto2);
            this.grpFamilyCardPhotoP2.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpFamilyCardPhotoP2.GroupBoxStyle = Telerik.WinControls.UI.RadGroupBoxStyle.Office;
            this.grpFamilyCardPhotoP2.HeaderText = "صورة البطاقة العائلية2";
            this.grpFamilyCardPhotoP2.Location = new System.Drawing.Point(6, 153);
            this.grpFamilyCardPhotoP2.Name = "grpFamilyCardPhotoP2";
            this.grpFamilyCardPhotoP2.Size = new System.Drawing.Size(168, 132);
            this.grpFamilyCardPhotoP2.TabIndex = 33;
            this.grpFamilyCardPhotoP2.Text = "صورة البطاقة العائلية2";
            this.grpFamilyCardPhotoP2.Click += new System.EventHandler(this.HideNameAddressForms);
            // 
            // picCardPhoto2
            // 
            this.picCardPhoto2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picCardPhoto2.BackgroundImage")));
            this.picCardPhoto2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.picCardPhoto2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picCardPhoto2.Location = new System.Drawing.Point(2, 18);
            this.picCardPhoto2.Name = "picCardPhoto2";
            this.picCardPhoto2.Photo = null;
            this.picCardPhoto2.Size = new System.Drawing.Size(164, 112);
            this.picCardPhoto2.TabIndex = 25;
            this.picCardPhoto2.TabStop = false;
            this.picCardPhoto2.PhotoChanged += new System.EventHandler(this.PhotoChanged);
            // 
            // grpBasicData
            // 
            this.grpBasicData.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this.grpBasicData.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.grpBasicData.Controls.Add(this.txtNote);
            this.grpBasicData.Controls.Add(this.txtFamilyCardNumber);
            this.grpBasicData.Controls.Add(this.txtFinncialStatus);
            this.grpBasicData.Controls.Add(this.txtResidenceStatus);
            this.grpBasicData.Controls.Add(this.txtResidenceType);
            this.grpBasicData.Controls.Add(this.chkIsTheyRefugees);
            this.grpBasicData.Controls.Add(this.lblResidenceStatus);
            this.grpBasicData.Controls.Add(this.lblNote);
            this.grpBasicData.Controls.Add(this.lblResidenceType);
            this.grpBasicData.Controls.Add(this.lblIsTheyRefugees);
            this.grpBasicData.Controls.Add(this.lblFamilyCardNumber);
            this.grpBasicData.Controls.Add(this.lblFinncialStatus);
            this.grpBasicData.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpBasicData.GroupBoxStyle = Telerik.WinControls.UI.RadGroupBoxStyle.Office;
            this.grpBasicData.HeaderAlignment = Telerik.WinControls.UI.HeaderAlignment.Center;
            this.grpBasicData.HeaderText = "";
            this.grpBasicData.Location = new System.Drawing.Point(182, 7);
            this.grpBasicData.Name = "grpBasicData";
            this.grpBasicData.Size = new System.Drawing.Size(250, 278);
            this.grpBasicData.TabIndex = 32;
            this.grpBasicData.TabStop = false;
            this.grpBasicData.ThemeName = "ControlDefault";
            this.grpBasicData.Click += new System.EventHandler(this.HideNameAddressForms);
            // 
            // txtNote
            // 
            this.txtNote.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.txtNote.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtNote.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtNote.AutoSize = false;
            this.txtNote.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.familyBindingSource, "Note", true));
            this.txtNote.Location = new System.Drawing.Point(20, 151);
            this.txtNote.Multiline = true;
            this.txtNote.Name = "txtNote";
            this.txtNote.Size = new System.Drawing.Size(144, 105);
            this.txtNote.TabIndex = 105;
            // 
            // familyBindingSource
            // 
            this.familyBindingSource.DataSource = typeof(OrphanageDataModel.RegularData.Family);
            // 
            // txtFamilyCardNumber
            // 
            this.txtFamilyCardNumber.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.familyBindingSource, "FamilyCardNumber", true));
            this.txtFamilyCardNumber.Location = new System.Drawing.Point(20, 125);
            this.txtFamilyCardNumber.Name = "txtFamilyCardNumber";
            this.txtFamilyCardNumber.Size = new System.Drawing.Size(144, 20);
            this.txtFamilyCardNumber.TabIndex = 29;
            // 
            // txtFinncialStatus
            // 
            this.txtFinncialStatus.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.txtFinncialStatus.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtFinncialStatus.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.familyBindingSource, "FinncialStatus", true));
            this.txtFinncialStatus.DefaultItemsCountInDropDown = 5;
            this.txtFinncialStatus.DropDownHeight = 300;
            this.txtFinncialStatus.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList;
            this.txtFinncialStatus.Location = new System.Drawing.Point(20, 85);
            this.txtFinncialStatus.Name = "txtFinncialStatus";
            this.txtFinncialStatus.SelectNextOnDoubleClick = true;
            this.txtFinncialStatus.Size = new System.Drawing.Size(144, 20);
            this.txtFinncialStatus.TabIndex = 102;
            // 
            // txtResidenceStatus
            // 
            this.txtResidenceStatus.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.txtResidenceStatus.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtResidenceStatus.DataBindings.Add(new System.Windows.Forms.Binding("SelectedItem", this.familyBindingSource, "ResidenceStatus", true));
            this.txtResidenceStatus.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.familyBindingSource, "ResidenceStatus", true));
            this.txtResidenceStatus.DefaultItemsCountInDropDown = 5;
            this.txtResidenceStatus.DropDownHeight = 220;
            this.txtResidenceStatus.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList;
            this.txtResidenceStatus.Location = new System.Drawing.Point(20, 62);
            this.txtResidenceStatus.Name = "txtResidenceStatus";
            this.txtResidenceStatus.SelectNextOnDoubleClick = true;
            this.txtResidenceStatus.Size = new System.Drawing.Size(144, 20);
            this.txtResidenceStatus.TabIndex = 101;
            // 
            // txtResidenceType
            // 
            this.txtResidenceType.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.txtResidenceType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtResidenceType.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.familyBindingSource, "ResidenceType", true));
            this.txtResidenceType.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.familyBindingSource, "ResidenceType", true));
            this.txtResidenceType.DefaultItemsCountInDropDown = 5;
            this.txtResidenceType.DropDownHeight = 300;
            this.txtResidenceType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList;
            this.txtResidenceType.Location = new System.Drawing.Point(20, 39);
            this.txtResidenceType.Name = "txtResidenceType";
            this.txtResidenceType.NullText = "نوع السكن";
            this.txtResidenceType.SelectNextOnDoubleClick = true;
            this.txtResidenceType.Size = new System.Drawing.Size(144, 20);
            this.txtResidenceType.TabIndex = 100;
            // 
            // chkIsTheyRefugees
            // 
            this.chkIsTheyRefugees.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.chkIsTheyRefugees.DataBindings.Add(new System.Windows.Forms.Binding("IsChecked", this.familyBindingSource, "IsTheyRefugees", true));
            this.chkIsTheyRefugees.Location = new System.Drawing.Point(149, 107);
            this.chkIsTheyRefugees.Name = "chkIsTheyRefugees";
            this.chkIsTheyRefugees.Size = new System.Drawing.Size(15, 15);
            this.chkIsTheyRefugees.TabIndex = 103;
            this.chkIsTheyRefugees.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.chkIsTheyRefugees_ToggleStateChanged);
            // 
            // lblResidenceStatus
            // 
            this.lblResidenceStatus.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblResidenceStatus.AutoSize = false;
            this.lblResidenceStatus.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResidenceStatus.Location = new System.Drawing.Point(168, 61);
            this.lblResidenceStatus.Name = "lblResidenceStatus";
            this.lblResidenceStatus.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblResidenceStatus.Size = new System.Drawing.Size(56, 17);
            this.lblResidenceStatus.TabIndex = 1;
            this.lblResidenceStatus.Text = "حالة السكن :";
            this.lblResidenceStatus.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblNote
            // 
            this.lblNote.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblNote.AutoSize = false;
            this.lblNote.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNote.Location = new System.Drawing.Point(169, 151);
            this.lblNote.Name = "lblNote";
            this.lblNote.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblNote.Size = new System.Drawing.Size(51, 17);
            this.lblNote.TabIndex = 1;
            this.lblNote.Text = "ملاحظات :";
            this.lblNote.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblResidenceType
            // 
            this.lblResidenceType.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblResidenceType.AutoSize = false;
            this.lblResidenceType.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResidenceType.Location = new System.Drawing.Point(168, 40);
            this.lblResidenceType.Name = "lblResidenceType";
            this.lblResidenceType.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblResidenceType.Size = new System.Drawing.Size(54, 17);
            this.lblResidenceType.TabIndex = 1;
            this.lblResidenceType.Text = "نوع السكن :";
            this.lblResidenceType.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblIsTheyRefugees
            // 
            this.lblIsTheyRefugees.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblIsTheyRefugees.AutoSize = false;
            this.lblIsTheyRefugees.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIsTheyRefugees.Location = new System.Drawing.Point(167, 106);
            this.lblIsTheyRefugees.Name = "lblIsTheyRefugees";
            this.lblIsTheyRefugees.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblIsTheyRefugees.Size = new System.Drawing.Size(61, 17);
            this.lblIsTheyRefugees.TabIndex = 1;
            this.lblIsTheyRefugees.Text = "عائلة مهجرة :";
            this.lblIsTheyRefugees.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblFamilyCardNumber
            // 
            this.lblFamilyCardNumber.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblFamilyCardNumber.AutoSize = false;
            this.lblFamilyCardNumber.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFamilyCardNumber.Location = new System.Drawing.Point(166, 127);
            this.lblFamilyCardNumber.Name = "lblFamilyCardNumber";
            this.lblFamilyCardNumber.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblFamilyCardNumber.Size = new System.Drawing.Size(82, 17);
            this.lblFamilyCardNumber.TabIndex = 1;
            this.lblFamilyCardNumber.Text = "رقم البطاقة العائلة :";
            this.lblFamilyCardNumber.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblFinncialStatus
            // 
            this.lblFinncialStatus.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblFinncialStatus.AutoSize = false;
            this.lblFinncialStatus.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFinncialStatus.Location = new System.Drawing.Point(168, 85);
            this.lblFinncialStatus.Name = "lblFinncialStatus";
            this.lblFinncialStatus.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblFinncialStatus.Size = new System.Drawing.Size(62, 17);
            this.lblFinncialStatus.TabIndex = 1;
            this.lblFinncialStatus.Text = "الحالة المادية :";
            this.lblFinncialStatus.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // familyErrorProvider1
            // 
            this.familyErrorProvider1.ContainerControl = this;
            // 
            // addressForm1
            // 
            this.addressForm1.AddressDataSource = typeof(OrphanageDataModel.RegularData.Address);
            this.addressForm1.HideOnEnter = false;
            this.addressForm1.Id = -1;
            this.addressForm1.Location = new System.Drawing.Point(11, 46);
            this.addressForm1.MoveFactor = 10;
            this.addressForm1.MoveType = OrphanageV3.Controlls.AddressForm._MoveType.DownToUp;
            this.addressForm1.Name = "addressForm1";
            this.addressForm1.ShowMovement = true;
            this.addressForm1.Size = new System.Drawing.Size(415, 284);
            this.addressForm1.TabIndex = 37;
            this.addressForm1.Visible = false;
            // 
            // addressForm2
            // 
            this.addressForm2.AddressDataSource = typeof(OrphanageDataModel.RegularData.Address);
            this.addressForm2.HideOnEnter = false;
            this.addressForm2.Id = -1;
            this.addressForm2.Location = new System.Drawing.Point(13, 61);
            this.addressForm2.MoveFactor = 10;
            this.addressForm2.MoveType = OrphanageV3.Controlls.AddressForm._MoveType.UpToDown;
            this.addressForm2.Name = "addressForm2";
            this.addressForm2.ShowMovement = true;
            this.addressForm2.Size = new System.Drawing.Size(415, 284);
            this.addressForm2.TabIndex = 38;
            this.addressForm2.Visible = false;
            // 
            // FamilyEditView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(441, 406);
            this.Controls.Add(this.addressForm2);
            this.Controls.Add(this.addressForm1);
            this.Controls.Add(this.FlowLayoutPanel1);
            this.Controls.Add(this.RadGroupBox3);
            this.Controls.Add(this.grpFamilyCardPhotoP1);
            this.Controls.Add(this.grpFamilyCardPhotoP2);
            this.Controls.Add(this.grpBasicData);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FamilyEditView";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "FamilyEditView";
            this.Click += new System.EventHandler(this.HideNameAddressForms);
            this.FlowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RadGroupBox3)).EndInit();
            this.RadGroupBox3.ResumeLayout(false);
            this.RadGroupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddress2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCurrentAddress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblPrimaryAddress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpFamilyCardPhotoP1)).EndInit();
            this.grpFamilyCardPhotoP1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpFamilyCardPhotoP2)).EndInit();
            this.grpFamilyCardPhotoP2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpBasicData)).EndInit();
            this.grpBasicData.ResumeLayout(false);
            this.grpBasicData.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNote)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.familyBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFamilyCardNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFinncialStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtResidenceStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtResidenceType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsTheyRefugees)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblResidenceStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNote)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblResidenceType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblIsTheyRefugees)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFamilyCardNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFinncialStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.familyErrorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.FlowLayoutPanel FlowLayoutPanel1;
        internal Telerik.WinControls.UI.RadButton btnCancel;
        internal Telerik.WinControls.UI.RadButton btnOK;
        internal Telerik.WinControls.UI.RadGroupBox RadGroupBox3;
        internal Telerik.WinControls.UI.RadTextBox txtAddress2;
        internal Telerik.WinControls.UI.RadTextBox txtAddress;
        internal Telerik.WinControls.UI.RadLabel lblCurrentAddress;
        internal Telerik.WinControls.UI.RadLabel lblPrimaryAddress;
        internal Telerik.WinControls.UI.RadGroupBox grpFamilyCardPhotoP1;
        internal PictureSelector.PictureSelector picCardphoto1;
        internal Telerik.WinControls.UI.RadGroupBox grpFamilyCardPhotoP2;
        internal PictureSelector.PictureSelector picCardPhoto2;
        internal Telerik.WinControls.UI.RadGroupBox grpBasicData;
        internal Telerik.WinControls.UI.RadTextBox txtNote;
        internal Telerik.WinControls.UI.RadDropDownList txtFinncialStatus;
        internal Telerik.WinControls.UI.RadDropDownList txtResidenceStatus;
        internal Telerik.WinControls.UI.RadDropDownList txtResidenceType;
        internal Telerik.WinControls.UI.RadCheckBox chkIsTheyRefugees;
        internal Telerik.WinControls.UI.RadLabel lblResidenceStatus;
        internal Telerik.WinControls.UI.RadLabel lblNote;
        internal Telerik.WinControls.UI.RadLabel lblResidenceType;
        internal Telerik.WinControls.UI.RadLabel lblIsTheyRefugees;
        internal Telerik.WinControls.UI.RadLabel lblFamilyCardNumber;
        internal Telerik.WinControls.UI.RadLabel lblFinncialStatus;
        private System.Windows.Forms.BindingSource familyBindingSource;
        internal Telerik.WinControls.UI.RadTextBox txtFamilyCardNumber;
        private Controlls.AddressForm addressForm1;
        private Controlls.AddressForm addressForm2;
        private System.Windows.Forms.ErrorProvider familyErrorProvider1;
    }
}
