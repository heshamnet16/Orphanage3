using OrphanageV3.ViewModel.Father;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using Unity;
namespace OrphanageV3.Views.ChooserView
{
    public partial class ChooserView : Telerik.WinControls.UI.RadForm
    {
        private List<object> _itemsList = new List<object>();

        public object SelectedObject => entityChooser1.SelectedItem;

        public IList<object> SelectedObjects => entityChooser1.SelectedItems;

        public ChooserView(List<object> list, string title)
        {
            InitializeComponent();
            _itemsList = list;
            btnOk.Text = Properties.Resources.OkString;
            btnClose.Text = Properties.Resources.CancelText;
            entityChooser1.DataSource = list;
            this.Text = title;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
