namespace OrphanageV3.Controlls
{
    partial class NameForm
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
            this.grpName = new Telerik.WinControls.UI.RadGroupBox();
            this.txtEnglishLast = new Telerik.WinControls.UI.RadTextBox();
            this.nameBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.txtEnglishFather = new Telerik.WinControls.UI.RadTextBox();
            this.txtEnglishFirst = new Telerik.WinControls.UI.RadTextBox();
            this.txtLast = new Telerik.WinControls.UI.RadTextBox();
            this.txtFather = new Telerik.WinControls.UI.RadTextBox();
            this.txtFirst = new Telerik.WinControls.UI.RadTextBox();
            this.lblLastNameE = new Telerik.WinControls.UI.RadLabel();
            this.lblFatherNameE = new Telerik.WinControls.UI.RadLabel();
            this.lblFirstNameE = new Telerik.WinControls.UI.RadLabel();
            this.lblLastName = new Telerik.WinControls.UI.RadLabel();
            this.lblFatherName = new Telerik.WinControls.UI.RadLabel();
            this.lblFirstName = new Telerik.WinControls.UI.RadLabel();
            this.NameerrorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.grpName)).BeginInit();
            this.grpName.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtEnglishLast)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nameBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEnglishFather)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEnglishFirst)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLast)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFather)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFirst)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblLastNameE)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFatherNameE)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFirstNameE)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblLastName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFatherName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFirstName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NameerrorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // grpName
            // 
            this.grpName.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this.grpName.Controls.Add(this.txtEnglishLast);
            this.grpName.Controls.Add(this.txtEnglishFather);
            this.grpName.Controls.Add(this.txtEnglishFirst);
            this.grpName.Controls.Add(this.txtLast);
            this.grpName.Controls.Add(this.txtFather);
            this.grpName.Controls.Add(this.txtFirst);
            this.grpName.Controls.Add(this.lblLastNameE);
            this.grpName.Controls.Add(this.lblFatherNameE);
            this.grpName.Controls.Add(this.lblFirstNameE);
            this.grpName.Controls.Add(this.lblLastName);
            this.grpName.Controls.Add(this.lblFatherName);
            this.grpName.Controls.Add(this.lblFirstName);
            this.grpName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpName.GroupBoxStyle = Telerik.WinControls.UI.RadGroupBoxStyle.Office;
            this.grpName.HeaderAlignment = Telerik.WinControls.UI.HeaderAlignment.Center;
            this.grpName.HeaderText = "الاسم";
            this.grpName.Location = new System.Drawing.Point(0, 0);
            this.grpName.Name = "grpName";
            this.grpName.Size = new System.Drawing.Size(376, 178);
            this.grpName.TabIndex = 10103;
            this.grpName.TabStop = false;
            this.grpName.Text = "الاسم";
            this.grpName.ThemeName = "ControlDefault";
            // 
            // txtEnglishLast
            // 
            this.txtEnglishLast.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEnglishLast.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtEnglishLast.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtEnglishLast.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.nameBindingSource, "EnglishLast", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtEnglishLast.Location = new System.Drawing.Point(106, 153);
            this.txtEnglishLast.Name = "txtEnglishLast";
            this.txtEnglishLast.NullText = "Last Name";
            this.txtEnglishLast.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtEnglishLast.Size = new System.Drawing.Size(261, 20);
            this.txtEnglishLast.TabIndex = 6;
            this.txtEnglishLast.Enter += new System.EventHandler(this.txtFAtherNameEf_Enter);
            this.txtEnglishLast.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtFatherNameF_KeyUp);
            this.txtEnglishLast.Leave += new System.EventHandler(this.txtFAtherNameEf_Leave);
            // 
            // nameBindingSource
            // 
            this.nameBindingSource.DataSource = typeof(OrphanageDataModel.RegularData.Name);
            this.nameBindingSource.DataSourceChanged += new System.EventHandler(this.nameBindingSource_DataSourceChanged);
            // 
            // txtEnglishFather
            // 
            this.txtEnglishFather.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEnglishFather.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtEnglishFather.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtEnglishFather.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.nameBindingSource, "EnglishFather", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtEnglishFather.Location = new System.Drawing.Point(106, 127);
            this.txtEnglishFather.Name = "txtEnglishFather";
            this.txtEnglishFather.NullText = "Father Name";
            this.txtEnglishFather.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtEnglishFather.Size = new System.Drawing.Size(261, 20);
            this.txtEnglishFather.TabIndex = 5;
            this.txtEnglishFather.Enter += new System.EventHandler(this.txtFAtherNameEf_Enter);
            this.txtEnglishFather.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtFatherNameF_KeyUp);
            this.txtEnglishFather.Leave += new System.EventHandler(this.txtFAtherNameEf_Leave);
            // 
            // txtEnglishFirst
            // 
            this.txtEnglishFirst.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEnglishFirst.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtEnglishFirst.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtEnglishFirst.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.nameBindingSource, "EnglishFirst", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtEnglishFirst.Location = new System.Drawing.Point(106, 101);
            this.txtEnglishFirst.Name = "txtEnglishFirst";
            this.txtEnglishFirst.NullText = "First Name";
            this.txtEnglishFirst.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtEnglishFirst.Size = new System.Drawing.Size(261, 20);
            this.txtEnglishFirst.TabIndex = 4;
            this.txtEnglishFirst.Enter += new System.EventHandler(this.txtFAtherNameEf_Enter);
            this.txtEnglishFirst.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtFatherNameF_KeyUp);
            this.txtEnglishFirst.Leave += new System.EventHandler(this.txtFAtherNameEf_Leave);
            // 
            // txtLast
            // 
            this.txtLast.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLast.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.nameBindingSource, "Last", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtLast.Location = new System.Drawing.Point(5, 75);
            this.txtLast.Name = "txtLast";
            this.txtLast.NullText = "النسبة";
            this.txtLast.Size = new System.Drawing.Size(304, 20);
            this.txtLast.TabIndex = 3;
            this.txtLast.Enter += new System.EventHandler(this.txtFatherNameF_Enter);
            this.txtLast.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtFatherNameF_KeyUp);
            this.txtLast.Leave += new System.EventHandler(this.txtFAtherNameEf_Leave);
            // 
            // txtFather
            // 
            this.txtFather.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFather.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtFather.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtFather.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.nameBindingSource, "Father", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtFather.Location = new System.Drawing.Point(5, 49);
            this.txtFather.Name = "txtFather";
            this.txtFather.NullText = "اسم الاب";
            this.txtFather.Size = new System.Drawing.Size(304, 20);
            this.txtFather.TabIndex = 2;
            this.txtFather.Enter += new System.EventHandler(this.txtFatherNameF_Enter);
            this.txtFather.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtFatherNameF_KeyUp);
            this.txtFather.Leave += new System.EventHandler(this.txtFAtherNameEf_Leave);
            // 
            // txtFirst
            // 
            this.txtFirst.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFirst.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtFirst.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtFirst.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.nameBindingSource, "First", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtFirst.Location = new System.Drawing.Point(5, 23);
            this.txtFirst.Name = "txtFirst";
            this.txtFirst.NullText = "الاسم";
            this.txtFirst.Size = new System.Drawing.Size(304, 20);
            this.txtFirst.TabIndex = 1;
            this.txtFirst.Enter += new System.EventHandler(this.txtFatherNameF_Enter);
            this.txtFirst.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtFatherNameF_KeyUp);
            this.txtFirst.Leave += new System.EventHandler(this.txtFAtherNameEf_Leave);
            // 
            // lblLastNameE
            // 
            this.lblLastNameE.Location = new System.Drawing.Point(19, 153);
            this.lblLastNameE.Name = "lblLastNameE";
            this.lblLastNameE.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblLastNameE.Size = new System.Drawing.Size(64, 18);
            this.lblLastNameE.TabIndex = 1;
            this.lblLastNameE.Text = "Last Name :";
            // 
            // lblFatherNameE
            // 
            this.lblFatherNameE.Location = new System.Drawing.Point(5, 127);
            this.lblFatherNameE.Name = "lblFatherNameE";
            this.lblFatherNameE.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblFatherNameE.Size = new System.Drawing.Size(80, 18);
            this.lblFatherNameE.TabIndex = 1;
            this.lblFatherNameE.Text = "Fatther Name :";
            // 
            // lblFirstNameE
            // 
            this.lblFirstNameE.Location = new System.Drawing.Point(19, 103);
            this.lblFirstNameE.Name = "lblFirstNameE";
            this.lblFirstNameE.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblFirstNameE.Size = new System.Drawing.Size(66, 18);
            this.lblFirstNameE.TabIndex = 1;
            this.lblFirstNameE.Text = "First Name :";
            // 
            // lblLastName
            // 
            this.lblLastName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLastName.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLastName.Location = new System.Drawing.Point(317, 75);
            this.lblLastName.Name = "lblLastName";
            this.lblLastName.Size = new System.Drawing.Size(35, 17);
            this.lblLastName.TabIndex = 1;
            this.lblLastName.Text = "النسبة :";
            this.lblLastName.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblFatherName
            // 
            this.lblFatherName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFatherName.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFatherName.Location = new System.Drawing.Point(315, 49);
            this.lblFatherName.Name = "lblFatherName";
            this.lblFatherName.Size = new System.Drawing.Size(48, 17);
            this.lblFatherName.TabIndex = 1;
            this.lblFatherName.Text = "اسم الأب :";
            this.lblFatherName.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblFirstName
            // 
            this.lblFirstName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFirstName.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFirstName.Location = new System.Drawing.Point(315, 23);
            this.lblFirstName.Name = "lblFirstName";
            this.lblFirstName.Size = new System.Drawing.Size(57, 17);
            this.lblFirstName.TabIndex = 1;
            this.lblFirstName.Text = "الاسم الأول :";
            this.lblFirstName.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            // 
            // NameerrorProvider1
            // 
            this.NameerrorProvider1.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.NameerrorProvider1.ContainerControl = this;
            this.NameerrorProvider1.RightToLeft = true;
            // 
            // NameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpName);
            this.Name = "NameForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Size = new System.Drawing.Size(376, 178);
            this.Load += new System.EventHandler(this.NameForm_Load);
            this.VisibleChanged += new System.EventHandler(this.NameForm_VisibleChanged);
            ((System.ComponentModel.ISupportInitialize)(this.grpName)).EndInit();
            this.grpName.ResumeLayout(false);
            this.grpName.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtEnglishLast)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nameBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEnglishFather)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEnglishFirst)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLast)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFather)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFirst)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblLastNameE)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFatherNameE)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFirstNameE)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblLastName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFatherName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFirstName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NameerrorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal Telerik.WinControls.UI.RadGroupBox grpName;
        internal Telerik.WinControls.UI.RadTextBox txtEnglishLast;
        internal Telerik.WinControls.UI.RadTextBox txtEnglishFather;
        internal Telerik.WinControls.UI.RadTextBox txtEnglishFirst;
        internal Telerik.WinControls.UI.RadTextBox txtLast;
        internal Telerik.WinControls.UI.RadTextBox txtFather;
        internal Telerik.WinControls.UI.RadTextBox txtFirst;
        internal Telerik.WinControls.UI.RadLabel lblLastNameE;
        internal Telerik.WinControls.UI.RadLabel lblFatherNameE;
        internal Telerik.WinControls.UI.RadLabel lblFirstNameE;
        internal Telerik.WinControls.UI.RadLabel lblLastName;
        internal Telerik.WinControls.UI.RadLabel lblFatherName;
        internal Telerik.WinControls.UI.RadLabel lblFirstName;
        private System.Windows.Forms.BindingSource nameBindingSource;
        private System.Windows.Forms.ErrorProvider NameerrorProvider1;
    }
}
