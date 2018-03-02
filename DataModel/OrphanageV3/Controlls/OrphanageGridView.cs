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
        private string _ColorColumnName = "ColorMark";
        private string _HideShowColumnName = "IsEcluded";
        private bool _ShowHiddenRows = Properties.Settings.Default.ShowHiddenRows;
        private bool IsButtonsRotated = false;
        public Telerik.WinControls.UI.RadGridView GridView
        {
            get
            {
                return this.radGridView;
            }
        }

        public string ColorColumnName { get => _ColorColumnName; set { _ColorColumnName = value; } }
        public string HideShowColumnName { get => _HideShowColumnName; set { _HideShowColumnName = value; } }
        public bool ShowHiddenRows { get => _ShowHiddenRows; set { _ShowHiddenRows = value; } }

        public OrphanageGridView()
        {
            InitializeComponent();
            _translateService = new TranslateService();
            TranslateGroupTools();
            TranslatePagingPanel(radGridView.TableElement.GridViewElement.PagingPanelElement.Children);
        }

        private void radGridView_CreateCell(object sender, Telerik.WinControls.UI.GridViewCreateCellEventArgs e)
        {
            if (e.CellType.FullName.Contains("FilterCellElement"))
            {
                var cell = new GridFilterCellElement((GridViewDataColumn)e.Column, e.Row);
                cell.FilterButton.Enabled = false;
                e.CellElement = cell;
            }
            if (e.CellType.FullName.Contains("FilterCheckBoxCellElement"))
            {
                var cell = new GridFilterCheckBoxCellElement((GridViewDataColumn)e.Column, e.Row);
                cell.FilterButton.Enabled = false;
                cell.FilterButton.AutoSize = false;
                cell.FilterButton.Visibility = Telerik.WinControls.ElementVisibility.Hidden;
                cell.FilterButton.Size = new Size(1, 1);
                e.CellElement = cell;
            }
        }

        private void TranslateGroupTools()
        {
            radGridView.TableElement.GridViewElement.GroupPanelElement.Text = Properties.Resources.GirdViewGroupPanelElementText;
            foreach (var elm in radGridView.TableElement.GridViewElement.GroupPanelElement.Children)
            {
                if (elm is LightVisualElement)
                    if (((LightVisualElement)elm).Text == "Group by:")
                    {
                        ((LightVisualElement)elm).Text = Properties.Resources.GridViewGroupByText;
                        break;
                    }
            }
        }
        private void TranslatePagingPanel(RadElementCollection elementCollection)
        {
            foreach (var ele in elementCollection)
            {
                try
                {
                    if (ele.Children != null && ele.Children.Count > 0)
                    {
                        TranslatePagingPanel(ele.Children);
                    }
                    else
                        ele.RightToLeft = false;
                    if (ele is CommandBarLabel)
                    {
                        if (((CommandBarLabel)ele).Text.ToLower() == "page")
                            ((CommandBarLabel)ele).Text = Properties.Resources.Page;
                        if (((CommandBarLabel)ele).Text.ToLower() == "of")
                            ((CommandBarLabel)ele).Text = Properties.Resources.of;
                    }
                    if (ele is CommandBarButton)
                    {
                        var btn = (CommandBarButton)ele;
                        var arrowImg = btn.Image;
                        if (arrowImg != null && !IsButtonsRotated)
                        {
                            arrowImg.RotateFlip(RotateFlipType.Rotate180FlipY);
                        }
                    }
                }
                catch
                {
                }
            }
            IsButtonsRotated = true;
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
                    col.VisibleInColumnChooser = true;
                }
                else
                {
                    col.IsVisible = false;
                    col.VisibleInColumnChooser = false;
                }
            }
            TranslateColumnsChooser();
        }
        private void TranslateColumnsChooser()
        {
            radGridView.ColumnChooser.Text = Properties.Resources.Columns;
            radGridView.ColumnChooser.ColumnChooserControl.ColumnChooserElement.Text = Properties.Resources.DragAndDropColumnHere;
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
            TranslatePagingPanel(radGridView.TableElement.GridViewElement.PagingPanelElement.Children);
        }

        private void radGridView_RowFormatting(object sender, RowFormattingEventArgs e)
        {
            var row = e.RowElement.RowInfo;

            if (radGridView.Columns.Contains(_ColorColumnName))
            {
                e.RowElement.DrawFill = true;
                if (row.Cells[_ColorColumnName].Value != null)
                {
                    var colorDecimal = row.Cells[_ColorColumnName].Value;
                    var ColorMark = Color.FromArgb(int.Parse(colorDecimal.ToString()));
                    e.RowElement.GradientStyle = GradientStyles.Gel;
                    //e.RowElement.GradientPercentage
                    e.RowElement.BackColor2 = Color.White;
                    e.RowElement.BackColor3 = Color.White;
                    e.RowElement.BackColor4 = Color.White;
                    if (ColorMark.ToArgb() != Color.White.ToArgb() && ColorMark.ToArgb() != Color.Black.ToArgb() && int.Parse(colorDecimal.ToString()) != 0)
                    {
                        if (Properties.Settings.Default.UseBackgroundColor)
                        {
                            e.RowElement.BackColor = ColorMark;
                            e.RowElement.ForeColor = Color.Black;
                        }
                        else
                        {
                            e.RowElement.ForeColor = ColorMark;
                            e.RowElement.BackColor = Color.White;
                        }
                    }
                    else
                    {
                        e.RowElement.ForeColor = Color.Black;
                        e.RowElement.BackColor = Color.White;
                    }
                }
            }
            if (radGridView.Columns.Contains(_HideShowColumnName))
            {
                if (!_ShowHiddenRows)
                {
                    var isHidden = (bool)row.Cells[_HideShowColumnName].Value;
                    if (isHidden) row.IsVisible = true;
                }
            }
        }

        private void radGridView_GroupByChanged(object sender, GridViewCollectionChangedEventArgs e)
        {
            TranslateGroupTools();
            TranslatePagingPanel(radGridView.TableElement.GridViewElement.PagingPanelElement.Children);
        }
        private void radGridView_PageChanged(object sender, EventArgs e)
        {
            TranslateGroupTools();
            TranslatePagingPanel(radGridView.TableElement.GridViewElement.PagingPanelElement.Children);
        }

        private void radGridView_BindingContextChanged(object sender, EventArgs e)
        {

        }

        public void ShowColumnsChooser()
        {
            TranslateColumnsChooser();
            radGridView.ColumnChooser.Show(ParentForm);
        }

    }
}
