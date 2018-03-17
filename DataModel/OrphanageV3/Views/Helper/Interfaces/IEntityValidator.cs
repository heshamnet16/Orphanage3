using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using static System.Windows.Forms.Form;

namespace OrphanageV3.Views.Helper.Interfaces
{
    public interface IEntityValidator
    {
        Control.ControlCollection controlCollection { get; set; }
        object DataEntity { get; set; }
        bool IsValid();
        IEnumerable<KeyValuePair<Control, string>> ErrorsControls();
        void SetErrorProvider(ErrorProvider errorProvider);
    }
}
