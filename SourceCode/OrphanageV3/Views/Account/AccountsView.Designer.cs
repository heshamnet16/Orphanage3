namespace OrphanageV3.Views.Account
{
    partial class AccountsView
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.radCmdBar = new Telerik.WinControls.UI.RadCommandBar();
            this.CommandBarRowElement3 = new Telerik.WinControls.UI.CommandBarRowElement();
            this.CommandBarStripElement1 = new Telerik.WinControls.UI.CommandBarStripElement();
            this.btnDelete = new Telerik.WinControls.UI.CommandBarButton();
            this.btnEdit = new Telerik.WinControls.UI.CommandBarButton();
            this.mnuSep2 = new Telerik.WinControls.UI.CommandBarSeparator();
            this.btnShowGuarantors = new Telerik.WinControls.UI.CommandBarButton();
            this.btnShowBails = new Telerik.WinControls.UI.CommandBarButton();
            this.btnSep3 = new Telerik.WinControls.UI.CommandBarSeparator();
            this.btnColumn = new Telerik.WinControls.UI.CommandBarButton();
            this.orphanageGridView1 = new OrphanageV3.Controlls.OrphanageGridView();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radCmdBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.radCmdBar, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.orphanageGridView1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(710, 458);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // radCmdBar
            // 
            this.radCmdBar.AutoSize = false;
            this.radCmdBar.Location = new System.Drawing.Point(3, 3);
            this.radCmdBar.Name = "radCmdBar";
            this.radCmdBar.Rows.AddRange(new Telerik.WinControls.UI.CommandBarRowElement[] {
            this.CommandBarRowElement3});
            this.radCmdBar.Size = new System.Drawing.Size(704, 38);
            this.radCmdBar.TabIndex = 3;
            // 
            // CommandBarRowElement3
            // 
            this.CommandBarRowElement3.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.CommandBarRowElement3.MinSize = new System.Drawing.Size(25, 25);
            this.CommandBarRowElement3.Strips.AddRange(new Telerik.WinControls.UI.CommandBarStripElement[] {
            this.CommandBarStripElement1});
            this.CommandBarRowElement3.Text = "";
            this.CommandBarRowElement3.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // CommandBarStripElement1
            // 
            this.CommandBarStripElement1.AutoSize = true;
            this.CommandBarStripElement1.BorderGradientStyle = Telerik.WinControls.GradientStyles.Gel;
            this.CommandBarStripElement1.ClipDrawing = false;
            this.CommandBarStripElement1.ClipText = false;
            this.CommandBarStripElement1.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.CommandBarStripElement1.DisplayName = "CommandBarStripElement1";
            this.CommandBarStripElement1.DrawBorder = false;
            this.CommandBarStripElement1.EnableImageTransparency = true;
            this.CommandBarStripElement1.GradientAngle = 90F;
            this.CommandBarStripElement1.GradientPercentage = 0.54F;
            this.CommandBarStripElement1.GradientPercentage2 = 0.5F;
            this.CommandBarStripElement1.GradientStyle = Telerik.WinControls.GradientStyles.Linear;
            // 
            // 
            // 
            this.CommandBarStripElement1.Grip.Visibility = Telerik.WinControls.ElementVisibility.Hidden;
            this.CommandBarStripElement1.Items.AddRange(new Telerik.WinControls.UI.RadCommandBarBaseItem[] {
            this.btnDelete,
            this.btnEdit,
            this.mnuSep2,
            this.btnShowGuarantors,
            this.btnShowBails,
            this.btnSep3,
            this.btnColumn});
            this.CommandBarStripElement1.Name = "CommandBarStripElement1";
            this.CommandBarStripElement1.NumberOfColors = 16;
            this.CommandBarStripElement1.Opacity = 1D;
            // 
            // 
            // 
            this.CommandBarStripElement1.OverflowButton.Visibility = Telerik.WinControls.ElementVisibility.Hidden;
            this.CommandBarStripElement1.RightToLeft = true;
            this.CommandBarStripElement1.ShouldPaint = true;
            this.CommandBarStripElement1.ShowHorizontalLine = false;
            this.CommandBarStripElement1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            ((Telerik.WinControls.UI.RadCommandBarGrip)(this.CommandBarStripElement1.GetChildAt(0))).Visibility = Telerik.WinControls.ElementVisibility.Hidden;
            ((Telerik.WinControls.UI.RadCommandBarOverflowButton)(this.CommandBarStripElement1.GetChildAt(2))).Visibility = Telerik.WinControls.ElementVisibility.Hidden;
            // 
            // btnDelete
            // 
            this.btnDelete.AutoSize = false;
            this.btnDelete.Bounds = new System.Drawing.Rectangle(0, 0, 40, 40);
            this.btnDelete.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnDelete.DisplayName = "CommandBarButton1";
            this.btnDelete.Image = global::OrphanageV3.Properties.Resources.DeletePic;
            this.btnDelete.ImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Padding = new System.Windows.Forms.Padding(0);
            this.btnDelete.RightToLeft = true;
            this.btnDelete.StretchHorizontally = false;
            this.btnDelete.Text = "";
            this.btnDelete.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnDelete.ToolTipText = "حذف";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.AccessibleDescription = "CommandBarButton2";
            this.btnEdit.AccessibleName = "CommandBarButton2";
            this.btnEdit.AutoSize = false;
            this.btnEdit.Bounds = new System.Drawing.Rectangle(0, 0, 40, 40);
            this.btnEdit.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnEdit.DisplayName = "CommandBarButton2";
            this.btnEdit.Image = global::OrphanageV3.Properties.Resources.EditPic;
            this.btnEdit.ImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Padding = new System.Windows.Forms.Padding(0);
            this.btnEdit.RightToLeft = true;
            this.btnEdit.StretchHorizontally = false;
            this.btnEdit.Text = "";
            this.btnEdit.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnEdit.ToolTipText = "تعديل";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // mnuSep2
            // 
            this.mnuSep2.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.mnuSep2.DisplayName = "CommandBarSeparator2";
            this.mnuSep2.Name = "mnuSep2";
            this.mnuSep2.StretchHorizontally = false;
            this.mnuSep2.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.mnuSep2.VisibleInOverflowMenu = false;
            // 
            // btnShowGuarantors
            // 
            this.btnShowGuarantors.AutoSize = false;
            this.btnShowGuarantors.Bounds = new System.Drawing.Rectangle(0, 0, 40, 40);
            this.btnShowGuarantors.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnShowGuarantors.DisplayName = "commandBarButton1";
            this.btnShowGuarantors.Image = global::OrphanageV3.Properties.Resources.GuarantorsPic;
            this.btnShowGuarantors.ImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnShowGuarantors.Name = "btnShowGuarantors";
            this.btnShowGuarantors.Text = "";
            this.btnShowGuarantors.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnShowGuarantors.Click += new System.EventHandler(this.btnShowGuarantos_Click);
            // 
            // btnShowBails
            // 
            this.btnShowBails.AutoSize = false;
            this.btnShowBails.Bounds = new System.Drawing.Rectangle(0, 0, 40, 40);
            this.btnShowBails.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnShowBails.DisplayName = "CommandBarButton2";
            this.btnShowBails.Image = global::OrphanageV3.Properties.Resources.BailsPic;
            this.btnShowBails.ImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnShowBails.Name = "btnShowBails";
            this.btnShowBails.Text = "";
            this.btnShowBails.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnShowBails.ToolTipText = "عرض العائلات";
            this.btnShowBails.Click += new System.EventHandler(this.btnShowBails_Click);
            // 
            // btnSep3
            // 
            this.btnSep3.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnSep3.Name = "btnSep3";
            this.btnSep3.StretchHorizontally = false;
            this.btnSep3.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnSep3.VisibleInOverflowMenu = false;
            // 
            // btnColumn
            // 
            this.btnColumn.AccessibleDescription = "CommandBarButton1";
            this.btnColumn.AccessibleName = "CommandBarButton1";
            this.btnColumn.AutoSize = false;
            this.btnColumn.Bounds = new System.Drawing.Rectangle(0, 0, 40, 40);
            this.btnColumn.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnColumn.Image = global::OrphanageV3.Properties.Resources.ColumnsPic;
            this.btnColumn.ImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnColumn.Name = "btnColumn";
            this.btnColumn.StretchHorizontally = false;
            this.btnColumn.Text = "";
            this.btnColumn.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnColumn.TextWrap = false;
            this.btnColumn.ToolTipText = "الأعمدة";
            this.btnColumn.Click += new System.EventHandler(this.btnColumn_Click);
            // 
            // orphanageGridView1
            // 
            this.orphanageGridView1.AddSelectColumn = true;
            this.orphanageGridView1.ColorColumnName = "ColorMark";
            this.orphanageGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.orphanageGridView1.HideShowColumnName = "IsExcluded";
            this.orphanageGridView1.IdColumnName = "Id";
            this.orphanageGridView1.Location = new System.Drawing.Point(3, 48);
            this.orphanageGridView1.Name = "orphanageGridView1";
            this.orphanageGridView1.ShowHiddenRows = false;
            this.orphanageGridView1.Size = new System.Drawing.Size(704, 374);
            this.orphanageGridView1.TabIndex = 0;
            // 
            // AccountsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(710, 458);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "AccountsView";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "AccountsView";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AccountsView_FormClosing);
            this.Load += new System.EventHandler(this.AccountsView_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radCmdBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        internal Telerik.WinControls.UI.RadCommandBar radCmdBar;
        internal Telerik.WinControls.UI.CommandBarRowElement CommandBarRowElement3;
        internal Telerik.WinControls.UI.CommandBarStripElement CommandBarStripElement1;
        internal Telerik.WinControls.UI.CommandBarButton btnDelete;
        internal Telerik.WinControls.UI.CommandBarButton btnEdit;
        internal Telerik.WinControls.UI.CommandBarSeparator mnuSep2;
        private Telerik.WinControls.UI.CommandBarButton btnShowGuarantors;
        internal Telerik.WinControls.UI.CommandBarButton btnShowBails;
        internal Telerik.WinControls.UI.CommandBarSeparator btnSep3;
        internal Telerik.WinControls.UI.CommandBarButton btnColumn;
        private Controlls.OrphanageGridView orphanageGridView1;
    }
}
