using System.Windows.Forms;

namespace OrphanageV3.Views.Family
{
    partial class FimiliesView
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
            this.radCmdBar = new Telerik.WinControls.UI.RadCommandBar();
            this.CommandBarRowElement3 = new Telerik.WinControls.UI.CommandBarRowElement();
            this.CommandBarStripElement1 = new Telerik.WinControls.UI.CommandBarStripElement();
            this.btnDelete = new Telerik.WinControls.UI.CommandBarButton();
            this.btnEdit = new Telerik.WinControls.UI.CommandBarButton();
            this.mnuSep1 = new Telerik.WinControls.UI.CommandBarSeparator();
            this.btnExclude = new Telerik.WinControls.UI.CommandBarButton();
            this.btnSetColor = new Telerik.WinControls.UI.CommandBarButton();
            this.btnBail = new Telerik.WinControls.UI.CommandBarButton();
            this.mnuSep2 = new Telerik.WinControls.UI.CommandBarSeparator();
            this.btnShowOrphans = new Telerik.WinControls.UI.CommandBarButton();
            this.btnShowFathers = new Telerik.WinControls.UI.CommandBarButton();
            this.btnShowMothers = new Telerik.WinControls.UI.CommandBarButton();
            this.btnSep3 = new Telerik.WinControls.UI.CommandBarSeparator();
            this.btnColumn = new Telerik.WinControls.UI.CommandBarButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.orphanageGridView1 = new OrphanageV3.Controlls.OrphanageGridView();
            this.radColorDialog = new System.Windows.Forms.ColorDialog();
            ((System.ComponentModel.ISupportInitialize)(this.radCmdBar)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // radCmdBar
            // 
            this.radCmdBar.AutoSize = false;
            this.radCmdBar.Location = new System.Drawing.Point(168, 3);
            this.radCmdBar.Name = "radCmdBar";
            this.radCmdBar.Rows.AddRange(new Telerik.WinControls.UI.CommandBarRowElement[] {
            this.CommandBarRowElement3});
            this.radCmdBar.Size = new System.Drawing.Size(779, 38);
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
            this.mnuSep1,
            this.btnExclude,
            this.btnSetColor,
            this.btnBail,
            this.mnuSep2,
            this.btnShowOrphans,
            this.btnShowFathers,
            this.btnShowMothers,
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
            this.btnDelete.DisplayName = "commandBarButton1";
            this.btnDelete.Image = global::OrphanageV3.Properties.Resources.DeletePic;
            this.btnDelete.ImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Text = "";
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
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // mnuSep1
            // 
            this.mnuSep1.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.mnuSep1.DisplayName = "CommandBarSeparator1";
            this.mnuSep1.Name = "mnuSep1";
            this.mnuSep1.StretchHorizontally = false;
            this.mnuSep1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.mnuSep1.VisibleInOverflowMenu = false;
            // 
            // btnExclude
            // 
            this.btnExclude.AutoSize = false;
            this.btnExclude.Bounds = new System.Drawing.Rectangle(0, 0, 40, 40);
            this.btnExclude.Image = global::OrphanageV3.Properties.Resources.HidePic;
            this.btnExclude.ImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExclude.Name = "btnExclude";
            this.btnExclude.Text = "";
            this.btnExclude.Click += new System.EventHandler(this.btnExclude_Click);
            // 
            // btnSetColor
            // 
            this.btnSetColor.AutoSize = false;
            this.btnSetColor.Bounds = new System.Drawing.Rectangle(0, 0, 40, 40);
            this.btnSetColor.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnSetColor.Image = global::OrphanageV3.Properties.Resources.ColorPickerPic;
            this.btnSetColor.ImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSetColor.Name = "btnSetColor";
            this.btnSetColor.StretchHorizontally = false;
            this.btnSetColor.Text = "";
            this.btnSetColor.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnSetColor.Click += new System.EventHandler(this.btnSetColor_Click);
            // 
            // btnBail
            // 
            this.btnBail.AutoSize = false;
            this.btnBail.Bounds = new System.Drawing.Rectangle(0, 0, 40, 40);
            this.btnBail.DisplayName = "commandBarButton1";
            this.btnBail.Image = global::OrphanageV3.Properties.Resources.BailPic;
            this.btnBail.ImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBail.Name = "btnBail";
            this.btnBail.Text = "";
            this.btnBail.Click += new System.EventHandler(this.btnBail_Click);
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
            // btnShowOrphans
            // 
            this.btnShowOrphans.AutoSize = false;
            this.btnShowOrphans.Bounds = new System.Drawing.Rectangle(0, 0, 40, 40);
            this.btnShowOrphans.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnShowOrphans.DisplayName = "commandBarButton1";
            this.btnShowOrphans.Image = global::OrphanageV3.Properties.Resources.ChildrenPic;
            this.btnShowOrphans.ImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnShowOrphans.Name = "btnShowOrphans";
            this.btnShowOrphans.Text = "";
            this.btnShowOrphans.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnShowOrphans.Click += new System.EventHandler(this.btnShowOrphans_Click);
            // 
            // btnShowFathers
            // 
            this.btnShowFathers.AutoSize = false;
            this.btnShowFathers.Bounds = new System.Drawing.Rectangle(0, 0, 40, 40);
            this.btnShowFathers.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnShowFathers.DisplayName = "CommandBarButton2";
            this.btnShowFathers.Image = global::OrphanageV3.Properties.Resources.FatherPic;
            this.btnShowFathers.ImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnShowFathers.Name = "btnShowFathers";
            this.btnShowFathers.Text = "";
            this.btnShowFathers.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnShowFathers.Click += new System.EventHandler(this.btnShowFathers_Click);
            // 
            // btnShowMothers
            // 
            this.btnShowMothers.AutoSize = false;
            this.btnShowMothers.Bounds = new System.Drawing.Rectangle(0, 0, 40, 40);
            this.btnShowMothers.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnShowMothers.DisplayName = "CommandBarButton1";
            this.btnShowMothers.Image = global::OrphanageV3.Properties.Resources.MotherPic;
            this.btnShowMothers.ImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnShowMothers.Name = "btnShowMothers";
            this.btnShowMothers.Text = "";
            this.btnShowMothers.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnShowMothers.Click += new System.EventHandler(this.btnShowMothers_Click);
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
            this.btnColumn.Click += new System.EventHandler(this.btnColumn_Click);
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
            this.tableLayoutPanel1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(950, 538);
            this.tableLayoutPanel1.TabIndex = 5;
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
            this.orphanageGridView1.Size = new System.Drawing.Size(944, 454);
            this.orphanageGridView1.TabIndex = 0;
            // 
            // radColorDialog
            // 
            this.radColorDialog.AnyColor = true;
            this.radColorDialog.FullOpen = true;
            // 
            // FimiliesView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(950, 538);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FimiliesView";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "RadForm1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FimiliesView_FormClosing);
            this.Load += new System.EventHandler(this.FimiliesView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radCmdBar)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        internal Telerik.WinControls.UI.RadCommandBar radCmdBar;
        internal Telerik.WinControls.UI.CommandBarRowElement CommandBarRowElement3;
        internal Telerik.WinControls.UI.CommandBarStripElement CommandBarStripElement1;
        internal Telerik.WinControls.UI.CommandBarButton btnEdit;
        internal Telerik.WinControls.UI.CommandBarSeparator mnuSep1;
        internal Telerik.WinControls.UI.CommandBarButton btnSetColor;
        internal Telerik.WinControls.UI.CommandBarSeparator mnuSep2;
        private Telerik.WinControls.UI.CommandBarButton btnShowOrphans;
        internal Telerik.WinControls.UI.CommandBarButton btnShowFathers;
        internal Telerik.WinControls.UI.CommandBarButton btnShowMothers;
        internal Telerik.WinControls.UI.CommandBarSeparator btnSep3;
        internal Telerik.WinControls.UI.CommandBarButton btnColumn;
        private Controlls.OrphanageGridView orphanageGridView1;
        private TableLayoutPanel tableLayoutPanel1;
        private Telerik.WinControls.UI.CommandBarButton btnExclude;
        private Telerik.WinControls.UI.CommandBarButton btnDelete;
        private Telerik.WinControls.UI.CommandBarButton btnBail;
        private ColorDialog radColorDialog;
    }
}
