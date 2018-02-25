using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using OrphanageV3.Services.Interfaces;
using Unity;
using Telerik.WinControls;
using OrphanageV3.Services;

namespace OrphanageV3.Controlls
{
    public partial class OrphanageGridView : UserControl
    {
        private readonly ITranslateService _translateService;
        private string _ColorColumnName = "Color";
        public Telerik.WinControls.UI.RadGridView GridView
        {
            get
            {
                return this.radGridView;
            }
        }
        public string ColorColumnName => _ColorColumnName;

        public OrphanageGridView()
        {
            InitializeComponent();
            _translateService = new TranslateService();
            radGridView.MasterTemplate.AllowAddNewRow = false;
            radGridView.MasterTemplate.AutoGenerateColumns = true;
        }

        private void radGridView_CreateCell(object sender, Telerik.WinControls.UI.GridViewCreateCellEventArgs e)
        {
            if (e.CellType.FullName.Contains("FilterCellElement"))
            {
                var cell = new GridFilterCellElement((GridViewDataColumn)e.Column, e.Row);
                cell.FilterButton.Enabled = false;
                e.CellElement = cell;
                e.CellType = cell.GetType();
            }
            if (e.CellType.FullName.Contains("FilterCheckBoxCellElement"))
            {
                var cell = new GridFilterCheckBoxCellElement((GridViewDataColumn)e.Column, e.Row);
                cell.FilterButton.Enabled = false;
                cell.FilterButton.AutoSize = false;
                cell.FilterButton.Visibility = Telerik.WinControls.ElementVisibility.Hidden;
                cell.FilterButton.Size = new Size(1, 1);
                e.CellElement = cell;
                e.CellType = cell.GetType();
            }
        }

        private void TranslateGroupTools()
        {
            radGridView.TableElement.GridViewElement.GroupPanelElement.Text = Properties.Resources.GirdViewGroupPanelElementText;
            foreach (var elm in radGridView.TableElement.GridViewElement.GroupPanelElement.Children)
            {
                foreach (var elm1 in elm.Children)
                {
                    foreach (var elm2 in elm1.Children)
                    {
                        if (elm2 is LightVisualElement)
                        {
                            LightVisualElement Xobj = (LightVisualElement)elm2;
                            if (Xobj.Text == "Group by:")
                            {
                                Xobj.Text = Properties.Resources.GridViewGroupByText;
                                break;
                            }
                        }
                    }
                }
            }
        }
        private void TranslateColumnsNames()
        {
            foreach (var col in this.radGridView.Columns)
            {
                if (col.Name != col.HeaderText) continue;
                var translatedText = _translateService.Translate(col.HeaderText);
                if (translatedText != null)
                {
                    col.HeaderText = translatedText;
                }
                else
                {
                    col.IsVisible = false;
                }
            }
        }
        private void changeColumnsDateTimeFormat()
        {
            foreach (var col in radGridView.Columns)
            {
                if (col is GridViewDateTimeColumn)
                {
                    ((GridViewDateTimeColumn)col).Format = DateTimePickerFormat.Custom;
                    ((GridViewDateTimeColumn)col).FormatString = "{0:" + Properties.Settings.Default.GeneralDateFormat + "}";
                }
            }
        }
        private void radGridView_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {

            TranslateColumnsNames();
            TranslateGroupTools();
            changeColumnsDateTimeFormat();
        }

        private void radGridView_RowFormatting(object sender, RowFormattingEventArgs e)
        {
            if (radGridView.Columns.Contains(_ColorColumnName))
            {
                var row = e.RowElement.RowInfo;
                if (row.Cells[_ColorColumnName].Value != null)
                {
                    var colorDecimal = (int)row.Cells[_ColorColumnName].Value;
                    var ColorMark = Color.FromArgb(colorDecimal);
                    if (ColorMark != Color.Black)
                    {
                        e.RowElement.DrawFill = true;
                        if (Properties.Settings.Default.UseBackgroundColor)
                        {
                            e.RowElement.GradientStyle = GradientStyles.Solid;
                            e.RowElement.BackColor = ColorMark;
                            e.RowElement.BackColor2 = ColorMark;
                            e.RowElement.BackColor3 = ColorMark;
                            e.RowElement.BackColor4 = ColorMark;
                            e.RowElement.ForeColor = Color.Black;
                        }
                        else
                        {
                            e.RowElement.ForeColor = ColorMark;
                            e.RowElement.BackColor = Color.White;
                        }
                    }
                }
            }
        }
    }
}
