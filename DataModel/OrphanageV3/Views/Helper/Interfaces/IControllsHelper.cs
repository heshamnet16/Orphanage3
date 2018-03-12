using OrphanageV3.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrphanageV3.Views.Helper.Interfaces
{
    public interface IControllsHelper
    {
        void SetNameForm(ref NameForm.NameForm nameForm, Name name);
        Name GetNameFromForm(NameForm.NameForm nameFrom);
    }
}
