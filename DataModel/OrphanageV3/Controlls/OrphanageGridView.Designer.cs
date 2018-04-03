namespace OrphanageV3.Controlls
{
    partial class OrphanageGridView
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
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            this.radGridView = new Telerik.WinControls.UI.RadGridView();
            this.LayoutControl = new Telerik.WinControls.UI.RadLayoutControl();
            this.orphansViewModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.orphansViewModelBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.radGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LayoutControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.orphansViewModelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.orphansViewModelBindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // radGridView
            // 
            this.radGridView.BackColor = System.Drawing.SystemColors.Control;
            this.radGridView.CausesValidation = false;
            this.radGridView.ColumnChooserSortOrder = Telerik.WinControls.UI.RadSortOrder.Ascending;
            this.radGridView.Cursor = System.Windows.Forms.Cursors.Default;
            this.radGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radGridView.EnableCustomDrawing = true;
            this.radGridView.EnableGestures = false;
            this.radGridView.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.radGridView.ForeColor = System.Drawing.SystemColors.ControlText;
            this.radGridView.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.radGridView.Location = new System.Drawing.Point(0, 0);
            // 
            // 
            // 
            this.radGridView.MasterTemplate.AllowAddNewRow = false;
            this.radGridView.MasterTemplate.AllowCellContextMenu = false;
            this.radGridView.MasterTemplate.AllowColumnHeaderContextMenu = false;
            this.radGridView.MasterTemplate.AllowDeleteRow = false;
            this.radGridView.MasterTemplate.AllowEditRow = false;
            this.radGridView.MasterTemplate.AllowRowHeaderContextMenu = false;
            this.radGridView.MasterTemplate.AllowRowReorder = true;
            this.radGridView.MasterTemplate.EnableFiltering = true;
            this.radGridView.MasterTemplate.EnablePaging = true;
            this.radGridView.MasterTemplate.MultiSelect = true;
            this.radGridView.MasterTemplate.ShowFilterCellOperatorText = false;
            this.radGridView.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.radGridView.Name = "radGridView";
            this.radGridView.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.radGridView.ShowCellErrors = false;
            this.radGridView.ShowNoDataText = false;
            this.radGridView.ShowRowErrors = false;
            this.radGridView.Size = new System.Drawing.Size(1034, 546);
            this.radGridView.TabIndex = 0;
            this.radGridView.CreateCell += new Telerik.WinControls.UI.GridViewCreateCellEventHandler(this.radGridView_CreateCell);
            this.radGridView.RowFormatting += new Telerik.WinControls.UI.RowFormattingEventHandler(this.radGridView_RowFormatting);
            this.radGridView.PageChanged += new System.EventHandler<System.EventArgs>(this.radGridView_PageChanged);
            this.radGridView.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.radGridView_CellClick);
            this.radGridView.DataBindingComplete += new Telerik.WinControls.UI.GridViewBindingCompleteEventHandler(this.radGridView_DataBindingComplete);
            this.radGridView.GroupByChanged += new Telerik.WinControls.UI.GridViewCollectionChangedEventHandler(this.radGridView_GroupByChanged);
            this.radGridView.LayoutLoaded += new Telerik.WinControls.UI.LayoutLoadedEventHandler(this.radGridView_LayoutLoaded);
            this.radGridView.BindingContextChanged += new System.EventHandler(this.radGridView_BindingContextChanged);
            // 
            // LayoutControl
            // 
            this.LayoutControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LayoutControl.DrawBorder = false;
            this.LayoutControl.Location = new System.Drawing.Point(0, 0);
            this.LayoutControl.Name = "LayoutControl";
            this.LayoutControl.Size = new System.Drawing.Size(298, 148);
            this.LayoutControl.TabIndex = 0;
            // 
            // orphansViewModelBindingSource
            // 
            this.orphansViewModelBindingSource.DataSource = typeof(OrphanageV3.ViewModel.Orphan.OrphanModel);
            // 
            // orphansViewModelBindingSource1
            // 
            this.orphansViewModelBindingSource1.DataSource = typeof(OrphanageV3.ViewModel.Orphan.OrphanModel);
            // 
            // OrphanageGridView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.radGridView);
            this.Name = "OrphanageGridView";
            this.Size = new System.Drawing.Size(1034, 546);
            ((System.ComponentModel.ISupportInitialize)(this.radGridView.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LayoutControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.orphansViewModelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.orphansViewModelBindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadGridView radGridView;
        private Telerik.WinControls.UI.RadLayoutControl LayoutControl;
        private System.Windows.Forms.BindingSource orphansViewModelBindingSource;
        private System.Windows.Forms.BindingSource orphansViewModelBindingSource1;
    }
}
