using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OrphanageV3.Attributes;
using OrphanageV3.Services.Interfaces;
using OrphanageV3.Services;
using Telerik.WinControls.UI;

namespace OrphanageV3.Controlls
{
    public partial class EntityChooser : UserControl
    {
        private readonly ITranslateService _translateService;
        private object[] _Items;
        private List<string> _ShowColumns;
        private List<string> _greenStrings;
        private string _IdColumnName = "Id";
        public string IdColumnName { get => _IdColumnName; set { _IdColumnName = value; } }


        public object[] Items
        {
            get => _Items;
            set
            {
                _Items = value;
                lstDataList.DataSource = _Items;
            }
        }

        public object SelectedItem
        {
            get { return lstDataList.SelectedItem?.Value; }
        }

        public IList<object> SelectedItems
        {
            get
            {
                if (lstDataList.SelectedItems != null && lstDataList.SelectedItems.Count > 0)
                {
                    List<object> retList = lstDataList.SelectedItems.Select(itm => itm.Value).ToList();
                    return retList;
                }
                else
                    return null;
            }
        }

        public bool MultiSelect
        {
            get { return lstDataList.MultiSelect; }
            set { lstDataList.MultiSelect = value; }
        }

        public object DataSource
        {
            get => lstDataList.DataSource;
            set => lstDataList.DataSource = value;
        }

        public EntityChooser()
        {
            InitializeComponent();
            _ShowColumns = new List<string>();
            _greenStrings = new List<string>();
            _translateService = new TranslateService();
        }

        private void SetAttributedColumns()
        {
            foreach (object item in _Items)
            {
                var itemType = item.GetType();
                var props = itemType.GetProperties();
                foreach (var prop in props)
                {
                    var attributs = prop.GetCustomAttributes(false);
                    foreach (var attribute in attributs)
                    {
                        if (attribute is ShowInChooser)
                        {
                            if (!_ShowColumns.Contains(prop.Name))
                            {
                                var showInChooser = (ShowInChooser)attribute;
                                _ShowColumns.Add(prop.Name);
                                var col = lstDataList.Columns[prop.Name];
                                lstDataList.Columns.Move(lstDataList.Columns.IndexOf(col), showInChooser.Order);
                            }
                        }
                    }
                }
            }
        }

        private void TranslateColumnsNames()
        {
            foreach (var col in this.lstDataList.Columns)
            {
                if (col.Name != col.HeaderText) continue;
                var translatedText = _translateService.Translate(col.HeaderText);
                if (translatedText != null && _ShowColumns.Contains(col.Name))
                {
                    col.HeaderText = translatedText;
                }
                else
                {
                    col.Visible = false;
                }
                col.BestFit();
            }
        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearch_Click(null, null);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            _greenStrings.Clear();
            if (lstDataList.Items != null && lstDataList.Items.Count > 0)
            {
                foreach (var itm in lstDataList.Items)
                {
                    Application.DoEvents();
                    if (txtSearch.Text != null && txtSearch.Text.Length > 0)
                    {
                        for (int i = 0; i < lstDataList.Columns.Count; i++)
                        {
                            if (!lstDataList.Columns[i].Visible) continue;


                            if (itm[i] != null && itm[i].ToString().Contains(txtSearch.Text))
                            {
                                itm.Visible = true;
                                _greenStrings.Add(itm[i].ToString());
                                break;
                            }
                            else
                            {
                                itm.Visible = false;
                            }
                        }
                    }
                    else
                    {
                        itm.Visible = true;
                    }
                }
            }
        }

        private void lstDataList_BindingCompleted(object sender, EventArgs e)
        {
            SetAttributedColumns();
            TranslateColumnsNames();           
        }

        private void lstDataList_CellFormatting(object sender, ListViewCellFormattingEventArgs e)
        {
            if (_greenStrings.Contains(e.CellElement.Text))
            {
                e.CellElement.ForeColor = Color.Green;
            }
            else
                e.CellElement.ForeColor = Color.Black;
        }
    }
}
