using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;

namespace OrphanageV3.Views.Orphan
{
    public partial class OrphansView : Telerik.WinControls.UI.RadForm
    {
        public OrphansView()
        {
            InitializeComponent();
            this.Text = Properties.Resources.OrphanViewTitle;
        }
    }
}
