using System.Collections.Generic;
using System.Windows.Forms;

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