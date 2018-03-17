namespace OrphanageV3.Controlls
{
    partial class AddressForm
    {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.txtWorkPhone = new Telerik.WinControls.UI.RadMaskedEditBox();
            this.addressBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lblWorkPhone = new Telerik.WinControls.UI.RadLabel();
            this.txtFax = new Telerik.WinControls.UI.RadMaskedEditBox();
            this.txtHomePhone = new Telerik.WinControls.UI.RadMaskedEditBox();
            this.txtCellPhone = new Telerik.WinControls.UI.RadMaskedEditBox();
            this.lblHomePhone = new Telerik.WinControls.UI.RadLabel();
            this.lblFaxNumber = new Telerik.WinControls.UI.RadLabel();
            this.grpPhoneNumbers = new Telerik.WinControls.UI.RadGroupBox();
            this.lblMobileNumber = new Telerik.WinControls.UI.RadLabel();
            this.txtTwitter = new Telerik.WinControls.UI.RadMaskedEditBox();
            this.txtFacebook = new Telerik.WinControls.UI.RadMaskedEditBox();
            this.txtNote = new Telerik.WinControls.UI.RadTextBoxControl();
            this.txtEmail = new Telerik.WinControls.UI.RadMaskedEditBox();
            this.lblState = new Telerik.WinControls.UI.RadLabel();
            this.lblNotes = new Telerik.WinControls.UI.RadLabel();
            this.lblSkype = new Telerik.WinControls.UI.RadLabel();
            this.lblFacebook = new Telerik.WinControls.UI.RadLabel();
            this.lblEmail = new Telerik.WinControls.UI.RadLabel();
            this.txtStreet = new Telerik.WinControls.UI.RadTextBox();
            this.txtTown = new Telerik.WinControls.UI.RadTextBox();
            this.txtCity = new Telerik.WinControls.UI.RadTextBox();
            this.lblStreet = new Telerik.WinControls.UI.RadLabel();
            this.lblTown = new Telerik.WinControls.UI.RadLabel();
            this.lblCity = new Telerik.WinControls.UI.RadLabel();
            this.grpInternet = new Telerik.WinControls.UI.RadGroupBox();
            this.grpAddress = new Telerik.WinControls.UI.RadGroupBox();
            this.txtCountry = new Telerik.WinControls.UI.RadTextBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.txtWorkPhone)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.addressBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblWorkPhone)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHomePhone)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCellPhone)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblHomePhone)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFaxNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpPhoneNumbers)).BeginInit();
            this.grpPhoneNumbers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lblMobileNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTwitter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFacebook)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNote)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblState)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNotes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblSkype)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFacebook)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblEmail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStreet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblStreet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblTown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpInternet)).BeginInit();
            this.grpInternet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpAddress)).BeginInit();
            this.grpAddress.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCountry)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtWorkPhone
            // 
            this.txtWorkPhone.AcceptsTab = true;
            this.txtWorkPhone.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtWorkPhone.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.addressBindingSource, "WorkPhone", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtWorkPhone.Location = new System.Drawing.Point(7, 97);
            this.txtWorkPhone.Mask = "(999)0000-000";
            this.txtWorkPhone.MaskType = Telerik.WinControls.UI.MaskType.Standard;
            this.txtWorkPhone.Name = "txtWorkPhone";
            this.txtWorkPhone.NullText = "(031)0000-000";
            this.txtWorkPhone.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtWorkPhone.Size = new System.Drawing.Size(83, 20);
            this.txtWorkPhone.TabIndex = 1008;
            this.txtWorkPhone.TabStop = false;
            this.txtWorkPhone.Text = "(031)0000-000";
            this.txtWorkPhone.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtWorkPhone.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.txtWorkPhone.Leave += new System.EventHandler(this.txtCountry_Leave);
            // 
            // addressBindingSource
            // 
            this.addressBindingSource.AllowNew = true;
            this.addressBindingSource.DataSource = typeof(OrphanageDataModel.RegularData.Address);
            this.addressBindingSource.DataSourceChanged += new System.EventHandler(this.addressBindingSource_DataSourceChanged);
            // 
            // lblWorkPhone
            // 
            this.lblWorkPhone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblWorkPhone.AutoSize = false;
            this.lblWorkPhone.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWorkPhone.Location = new System.Drawing.Point(92, 99);
            this.lblWorkPhone.Name = "lblWorkPhone";
            this.lblWorkPhone.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblWorkPhone.Size = new System.Drawing.Size(55, 17);
            this.lblWorkPhone.TabIndex = 74;
            this.lblWorkPhone.Text = "هاتف العمل:";
            this.lblWorkPhone.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtFax
            // 
            this.txtFax.AcceptsTab = true;
            this.txtFax.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFax.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.addressBindingSource, "Fax", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtFax.Location = new System.Drawing.Point(7, 47);
            this.txtFax.Mask = "(999)0000-000";
            this.txtFax.MaskType = Telerik.WinControls.UI.MaskType.Standard;
            this.txtFax.Name = "txtFax";
            this.txtFax.NullText = "(031)0000-000";
            this.txtFax.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtFax.Size = new System.Drawing.Size(83, 20);
            this.txtFax.TabIndex = 1006;
            this.txtFax.TabStop = false;
            this.txtFax.Text = "(031)0000-000";
            this.txtFax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtFax.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.txtFax.Leave += new System.EventHandler(this.txtCountry_Leave);
            // 
            // txtHomePhone
            // 
            this.txtHomePhone.AcceptsTab = true;
            this.txtHomePhone.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHomePhone.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.addressBindingSource, "HomePhone", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtHomePhone.Location = new System.Drawing.Point(7, 71);
            this.txtHomePhone.Mask = "(999)0000-000";
            this.txtHomePhone.MaskType = Telerik.WinControls.UI.MaskType.Standard;
            this.txtHomePhone.Name = "txtHomePhone";
            this.txtHomePhone.NullText = "(031)0000-000";
            this.txtHomePhone.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtHomePhone.Size = new System.Drawing.Size(83, 20);
            this.txtHomePhone.TabIndex = 1007;
            this.txtHomePhone.TabStop = false;
            this.txtHomePhone.Text = "(031)0000-000";
            this.txtHomePhone.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtHomePhone.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.txtHomePhone.Leave += new System.EventHandler(this.txtCountry_Leave);
            // 
            // txtCellPhone
            // 
            this.txtCellPhone.AcceptsTab = true;
            this.txtCellPhone.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCellPhone.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.addressBindingSource, "CellPhone", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtCellPhone.Location = new System.Drawing.Point(7, 23);
            this.txtCellPhone.Mask = "(9999)000-000";
            this.txtCellPhone.MaskType = Telerik.WinControls.UI.MaskType.Standard;
            this.txtCellPhone.Name = "txtCellPhone";
            this.txtCellPhone.NullText = "(0944)000-000";
            this.txtCellPhone.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtCellPhone.Size = new System.Drawing.Size(83, 20);
            this.txtCellPhone.TabIndex = 1005;
            this.txtCellPhone.TabStop = false;
            this.txtCellPhone.Text = "(0944)000-000";
            this.txtCellPhone.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCellPhone.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.txtCellPhone.Leave += new System.EventHandler(this.txtCountry_Leave);
            // 
            // lblHomePhone
            // 
            this.lblHomePhone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblHomePhone.AutoSize = false;
            this.lblHomePhone.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHomePhone.Location = new System.Drawing.Point(90, 71);
            this.lblHomePhone.Name = "lblHomePhone";
            this.lblHomePhone.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblHomePhone.Size = new System.Drawing.Size(59, 17);
            this.lblHomePhone.TabIndex = 63;
            this.lblHomePhone.Text = "هاتف المنزل:";
            this.lblHomePhone.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblFaxNumber
            // 
            this.lblFaxNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFaxNumber.AutoSize = false;
            this.lblFaxNumber.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFaxNumber.Location = new System.Drawing.Point(95, 47);
            this.lblFaxNumber.Name = "lblFaxNumber";
            this.lblFaxNumber.Size = new System.Drawing.Size(53, 17);
            this.lblFaxNumber.TabIndex = 65;
            this.lblFaxNumber.Text = "رقم الفاكس:";
            this.lblFaxNumber.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            // 
            // grpPhoneNumbers
            // 
            this.grpPhoneNumbers.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this.grpPhoneNumbers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpPhoneNumbers.Controls.Add(this.txtWorkPhone);
            this.grpPhoneNumbers.Controls.Add(this.lblWorkPhone);
            this.grpPhoneNumbers.Controls.Add(this.txtFax);
            this.grpPhoneNumbers.Controls.Add(this.txtHomePhone);
            this.grpPhoneNumbers.Controls.Add(this.txtCellPhone);
            this.grpPhoneNumbers.Controls.Add(this.lblHomePhone);
            this.grpPhoneNumbers.Controls.Add(this.lblMobileNumber);
            this.grpPhoneNumbers.Controls.Add(this.lblFaxNumber);
            this.grpPhoneNumbers.GroupBoxStyle = Telerik.WinControls.UI.RadGroupBoxStyle.Office;
            this.grpPhoneNumbers.HeaderText = " ";
            this.grpPhoneNumbers.Location = new System.Drawing.Point(346, 137);
            this.grpPhoneNumbers.Name = "grpPhoneNumbers";
            this.grpPhoneNumbers.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.grpPhoneNumbers.Size = new System.Drawing.Size(153, 144);
            this.grpPhoneNumbers.TabIndex = 1;
            this.grpPhoneNumbers.Text = " ";
            // 
            // lblMobileNumber
            // 
            this.lblMobileNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMobileNumber.AutoSize = false;
            this.lblMobileNumber.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMobileNumber.Location = new System.Drawing.Point(96, 23);
            this.lblMobileNumber.Name = "lblMobileNumber";
            this.lblMobileNumber.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblMobileNumber.Size = new System.Drawing.Size(52, 17);
            this.lblMobileNumber.TabIndex = 64;
            this.lblMobileNumber.Text = "رقم الجوال:";
            this.lblMobileNumber.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtTwitter
            // 
            this.txtTwitter.AcceptsTab = true;
            this.txtTwitter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTwitter.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.addressBindingSource, "Twitter", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtTwitter.Location = new System.Drawing.Point(5, 127);
            this.txtTwitter.Name = "txtTwitter";
            this.txtTwitter.NullText = "Https://www.twitter.com/";
            this.txtTwitter.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtTwitter.Size = new System.Drawing.Size(317, 20);
            this.txtTwitter.TabIndex = 1011;
            this.txtTwitter.TabStop = false;
            this.txtTwitter.Enter += new System.EventHandler(this.txtEmail_Enter);
            this.txtTwitter.Leave += new System.EventHandler(this.txtCountry_Leave);
            // 
            // txtFacebook
            // 
            this.txtFacebook.AcceptsTab = true;
            this.txtFacebook.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFacebook.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.addressBindingSource, "Facebook", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtFacebook.Location = new System.Drawing.Point(5, 81);
            this.txtFacebook.Name = "txtFacebook";
            this.txtFacebook.NullText = "Https://www.Facebook.com/";
            this.txtFacebook.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtFacebook.Size = new System.Drawing.Size(317, 20);
            this.txtFacebook.TabIndex = 1010;
            this.txtFacebook.TabStop = false;
            this.txtFacebook.Enter += new System.EventHandler(this.txtEmail_Enter);
            this.txtFacebook.Leave += new System.EventHandler(this.txtCountry_Leave);
            // 
            // txtNote
            // 
            this.txtNote.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNote.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.addressBindingSource, "Note", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtNote.Location = new System.Drawing.Point(5, 179);
            this.txtNote.Multiline = true;
            this.txtNote.Name = "txtNote";
            this.txtNote.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtNote.SelectionOpacity = 50;
            this.txtNote.Size = new System.Drawing.Size(317, 90);
            this.txtNote.TabIndex = 1012;
            this.txtNote.Enter += new System.EventHandler(this.txtCountry_Enter);
            this.txtNote.Leave += new System.EventHandler(this.txtCountry_Leave);
            // 
            // txtEmail
            // 
            this.txtEmail.AcceptsTab = true;
            this.txtEmail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEmail.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.addressBindingSource, "Email", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtEmail.Location = new System.Drawing.Point(5, 39);
            this.txtEmail.MaskType = Telerik.WinControls.UI.MaskType.EMail;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.NullText = "example@server.srv";
            this.txtEmail.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtEmail.Size = new System.Drawing.Size(317, 20);
            this.txtEmail.TabIndex = 1009;
            this.txtEmail.TabStop = false;
            this.txtEmail.Enter += new System.EventHandler(this.txtEmail_Enter);
            this.txtEmail.Leave += new System.EventHandler(this.txtCountry_Leave);
            // 
            // lblState
            // 
            this.lblState.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblState.AutoSize = false;
            this.lblState.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblState.Location = new System.Drawing.Point(109, 23);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(44, 17);
            this.lblState.TabIndex = 62;
            this.lblState.Text = "المحافظة:";
            this.lblState.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblNotes
            // 
            this.lblNotes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNotes.AutoSize = false;
            this.lblNotes.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNotes.Location = new System.Drawing.Point(274, 158);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblNotes.Size = new System.Drawing.Size(48, 17);
            this.lblNotes.TabIndex = 69;
            this.lblNotes.Text = "ملاحظات:";
            this.lblNotes.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblSkype
            // 
            this.lblSkype.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSkype.AutoSize = false;
            this.lblSkype.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSkype.Location = new System.Drawing.Point(286, 107);
            this.lblSkype.Name = "lblSkype";
            this.lblSkype.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblSkype.Size = new System.Drawing.Size(36, 17);
            this.lblSkype.TabIndex = 68;
            this.lblSkype.Text = "سكايب:";
            this.lblSkype.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblFacebook
            // 
            this.lblFacebook.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFacebook.AutoSize = false;
            this.lblFacebook.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFacebook.Location = new System.Drawing.Point(283, 65);
            this.lblFacebook.Name = "lblFacebook";
            this.lblFacebook.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblFacebook.Size = new System.Drawing.Size(39, 17);
            this.lblFacebook.TabIndex = 70;
            this.lblFacebook.Text = "فيسبوك:";
            this.lblFacebook.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblEmail
            // 
            this.lblEmail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblEmail.AutoSize = false;
            this.lblEmail.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmail.Location = new System.Drawing.Point(286, 21);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblEmail.Size = new System.Drawing.Size(36, 17);
            this.lblEmail.TabIndex = 72;
            this.lblEmail.Text = "الأيميل:";
            this.lblEmail.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtStreet
            // 
            this.txtStreet.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtStreet.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtStreet.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.addressBindingSource, "Street", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtStreet.Location = new System.Drawing.Point(7, 97);
            this.txtStreet.Name = "txtStreet";
            this.txtStreet.Size = new System.Drawing.Size(100, 20);
            this.txtStreet.TabIndex = 1004;
            this.txtStreet.Enter += new System.EventHandler(this.txtCountry_Enter);
            this.txtStreet.Leave += new System.EventHandler(this.txtCountry_Leave);
            // 
            // txtTown
            // 
            this.txtTown.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtTown.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtTown.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.addressBindingSource, "Town", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtTown.Location = new System.Drawing.Point(7, 72);
            this.txtTown.Name = "txtTown";
            this.txtTown.Size = new System.Drawing.Size(100, 20);
            this.txtTown.TabIndex = 1003;
            this.txtTown.Enter += new System.EventHandler(this.txtCountry_Enter);
            this.txtTown.Leave += new System.EventHandler(this.txtCountry_Leave);
            // 
            // txtCity
            // 
            this.txtCity.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtCity.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtCity.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.addressBindingSource, "City", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtCity.Location = new System.Drawing.Point(7, 48);
            this.txtCity.Name = "txtCity";
            this.txtCity.Size = new System.Drawing.Size(100, 20);
            this.txtCity.TabIndex = 1002;
            this.txtCity.Enter += new System.EventHandler(this.txtCountry_Enter);
            this.txtCity.Leave += new System.EventHandler(this.txtCountry_Leave);
            // 
            // lblStreet
            // 
            this.lblStreet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStreet.AutoSize = false;
            this.lblStreet.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStreet.Location = new System.Drawing.Point(110, 97);
            this.lblStreet.Name = "lblStreet";
            this.lblStreet.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblStreet.Size = new System.Drawing.Size(37, 17);
            this.lblStreet.TabIndex = 60;
            this.lblStreet.Text = "الشارع:";
            this.lblStreet.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblTown
            // 
            this.lblTown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTown.AutoSize = false;
            this.lblTown.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTown.Location = new System.Drawing.Point(110, 73);
            this.lblTown.Name = "lblTown";
            this.lblTown.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblTown.Size = new System.Drawing.Size(28, 17);
            this.lblTown.TabIndex = 61;
            this.lblTown.Text = "البلدة:";
            this.lblTown.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblCity
            // 
            this.lblCity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCity.AutoSize = false;
            this.lblCity.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCity.Location = new System.Drawing.Point(109, 49);
            this.lblCity.Name = "lblCity";
            this.lblCity.Size = new System.Drawing.Size(35, 17);
            this.lblCity.TabIndex = 63;
            this.lblCity.Text = "المدينة:";
            this.lblCity.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            // 
            // grpInternet
            // 
            this.grpInternet.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this.grpInternet.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpInternet.Controls.Add(this.txtTwitter);
            this.grpInternet.Controls.Add(this.txtFacebook);
            this.grpInternet.Controls.Add(this.txtNote);
            this.grpInternet.Controls.Add(this.txtEmail);
            this.grpInternet.Controls.Add(this.lblNotes);
            this.grpInternet.Controls.Add(this.lblSkype);
            this.grpInternet.Controls.Add(this.lblFacebook);
            this.grpInternet.Controls.Add(this.lblEmail);
            this.grpInternet.GroupBoxStyle = Telerik.WinControls.UI.RadGroupBoxStyle.Office;
            this.grpInternet.HeaderText = " ";
            this.grpInternet.Location = new System.Drawing.Point(3, 5);
            this.grpInternet.Name = "grpInternet";
            this.grpInternet.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.grpInternet.Size = new System.Drawing.Size(337, 276);
            this.grpInternet.TabIndex = 2;
            this.grpInternet.TabStop = false;
            this.grpInternet.Text = " ";
            // 
            // grpAddress
            // 
            this.grpAddress.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this.grpAddress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.grpAddress.Controls.Add(this.txtStreet);
            this.grpAddress.Controls.Add(this.txtTown);
            this.grpAddress.Controls.Add(this.txtCity);
            this.grpAddress.Controls.Add(this.txtCountry);
            this.grpAddress.Controls.Add(this.lblStreet);
            this.grpAddress.Controls.Add(this.lblTown);
            this.grpAddress.Controls.Add(this.lblCity);
            this.grpAddress.Controls.Add(this.lblState);
            this.grpAddress.HeaderText = " ";
            this.grpAddress.Location = new System.Drawing.Point(346, 5);
            this.grpAddress.Name = "grpAddress";
            this.grpAddress.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.grpAddress.Size = new System.Drawing.Size(153, 126);
            this.grpAddress.TabIndex = 3;
            this.grpAddress.Text = " ";
            // 
            // txtCountry
            // 
            this.txtCountry.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtCountry.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtCountry.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.addressBindingSource, "Country", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtCountry.Location = new System.Drawing.Point(7, 23);
            this.txtCountry.Name = "txtCountry";
            this.txtCountry.Size = new System.Drawing.Size(100, 20);
            this.txtCountry.TabIndex = 1000;
            this.txtCountry.Enter += new System.EventHandler(this.txtCountry_Enter);
            this.txtCountry.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtCountry_KeyUp);
            this.txtCountry.Leave += new System.EventHandler(this.txtCountry_Leave);
            // 
            // errorProvider1
            // 
            this.errorProvider1.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider1.ContainerControl = this;
            this.errorProvider1.DataSource = this.addressBindingSource;
            this.errorProvider1.RightToLeft = true;
            // 
            // AddressForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpPhoneNumbers);
            this.Controls.Add(this.grpInternet);
            this.Controls.Add(this.grpAddress);
            this.Name = "AddressForm";
            this.Size = new System.Drawing.Size(502, 284);
            this.Load += new System.EventHandler(this.AddressFrom_Load);
            this.VisibleChanged += new System.EventHandler(this.AddressFrom_VisibleChanged);
            ((System.ComponentModel.ISupportInitialize)(this.txtWorkPhone)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.addressBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblWorkPhone)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHomePhone)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCellPhone)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblHomePhone)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFaxNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpPhoneNumbers)).EndInit();
            this.grpPhoneNumbers.ResumeLayout(false);
            this.grpPhoneNumbers.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lblMobileNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTwitter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFacebook)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNote)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblState)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNotes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblSkype)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFacebook)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblEmail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStreet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblStreet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblTown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpInternet)).EndInit();
            this.grpInternet.ResumeLayout(false);
            this.grpInternet.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpAddress)).EndInit();
            this.grpAddress.ResumeLayout(false);
            this.grpAddress.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCountry)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal Telerik.WinControls.UI.RadMaskedEditBox txtWorkPhone;
        internal Telerik.WinControls.UI.RadLabel lblWorkPhone;
        internal Telerik.WinControls.UI.RadMaskedEditBox txtFax;
        internal Telerik.WinControls.UI.RadMaskedEditBox txtHomePhone;
        internal Telerik.WinControls.UI.RadMaskedEditBox txtCellPhone;
        internal Telerik.WinControls.UI.RadLabel lblHomePhone;
        internal Telerik.WinControls.UI.RadLabel lblFaxNumber;
        internal Telerik.WinControls.UI.RadGroupBox grpPhoneNumbers;
        internal Telerik.WinControls.UI.RadLabel lblMobileNumber;
        internal Telerik.WinControls.UI.RadMaskedEditBox txtTwitter;
        internal Telerik.WinControls.UI.RadMaskedEditBox txtFacebook;
        internal Telerik.WinControls.UI.RadTextBoxControl txtNote;
        internal Telerik.WinControls.UI.RadMaskedEditBox txtEmail;
        internal Telerik.WinControls.UI.RadLabel lblState;
        internal Telerik.WinControls.UI.RadLabel lblNotes;
        internal Telerik.WinControls.UI.RadLabel lblSkype;
        internal Telerik.WinControls.UI.RadLabel lblFacebook;
        internal Telerik.WinControls.UI.RadLabel lblEmail;
        internal Telerik.WinControls.UI.RadTextBox txtStreet;
        internal Telerik.WinControls.UI.RadTextBox txtTown;
        internal Telerik.WinControls.UI.RadTextBox txtCity;
        internal Telerik.WinControls.UI.RadLabel lblStreet;
        internal Telerik.WinControls.UI.RadLabel lblTown;
        internal Telerik.WinControls.UI.RadLabel lblCity;
        internal Telerik.WinControls.UI.RadGroupBox grpInternet;
        internal Telerik.WinControls.UI.RadGroupBox grpAddress;
        internal Telerik.WinControls.UI.RadTextBox txtCountry;
        private System.Windows.Forms.BindingSource addressBindingSource;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}
