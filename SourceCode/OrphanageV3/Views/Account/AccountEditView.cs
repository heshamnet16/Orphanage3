using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;

namespace OrphanageV3.Views.Account
{
    public partial class AccountEditView : Telerik.WinControls.UI.RadForm
    {
        public AccountEditView()
        {
            InitializeComponent();
        }

        public AccountEditView(int accountId)
        {
            InitializeComponent();
        }
    }
}