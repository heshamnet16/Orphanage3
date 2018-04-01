using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using Unity;

namespace OrphanageV3.Views.Main
{
    public partial class MainView : Telerik.WinControls.UI.RadForm
    {
        private ViewModel.Main.MainViewModel _mainViewModel = Program.Factory.Resolve<ViewModel.Main.MainViewModel>();

        public MainView()
        {
            InitializeComponent();
            TranslateControls();
            _mainViewModel.MainView = this;
        }

        private void TranslateControls()
        {
            mnuNew.Text = Properties.Resources.New;
            mnuNewOrphan.Text = Properties.Resources.OrphanEditViewTitle;
            mnuNewFamily.Text = Properties.Resources.Family;
            mnuShow.Text = Properties.Resources.Show;
            mnuShowOrphans.Text = Properties.Resources.Orphans;
            mnuShowUnBailedOrphans.Text = Properties.Resources.UnBaild;
            mnuShowBailedOrphans.Text = Properties.Resources.Baild;
            mnuShowALLOrphans.Text = Properties.Resources.All;
            mnuShowExcludedOrphans.Text = Properties.Resources.Excluded;
            mnuShowUnExcludedOrphans.Text = Properties.Resources.UnExcluded;
            mnuShowFamilies.Text = Properties.Resources.Families;
        }

        private void mnuNewOrphan_Click(object sender, EventArgs e)
        {
            _mainViewModel.ShowNewOrphanView();
        }
    }
}