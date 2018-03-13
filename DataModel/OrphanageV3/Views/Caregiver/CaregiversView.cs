using OrphanageV3.ViewModel.Caregiver;
using OrphanageV3.ViewModel.Orphan;
using OrphanageV3.Views.Helper.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using Unity;
namespace OrphanageV3.Views.Caregiver
{
    public partial class CaregiversView : Telerik.WinControls.UI.RadForm
    {
        private CaregiversViewModel _caregiversViewModel = Program.Factory.Resolve<CaregiversViewModel>();
        private IRadGridHelper _radGridHelper = Program.Factory.Resolve<IRadGridHelper>();

        public CaregiversView()
        {
            InitializeComponent();
            _caregiversViewModel.DataLoaded += _caregiversViewModel_DataLoaded;
        }

        private void _caregiversViewModel_DataLoaded(object sender, EventArgs e)
        {
            orphanageGridView1.GridView.DataSource = _caregiversViewModel.Caregivers;
        }

        private void orphanageGridView1_Load(object sender, EventArgs e)
        {
            _caregiversViewModel.LoadCaregivers();
        }
    }
}
