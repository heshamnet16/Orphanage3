using NameForm;
using OrphanageV3.Services;
using OrphanageV3.Views.Helper.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrphanageV3.Views.Helper
{
    public class ControllsHelper : IControllsHelper
    {
        private readonly IApiClient _apiClient;

        public ControllsHelper(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }
        public void SetNameForm(ref NameForm.NameForm nameForm, Name name)
        {
            if (name != null && name.Id > 0 && nameForm != null)
            {
                nameForm.First = name.First;
                nameForm.Middle = name.Father;
                nameForm.Last = name.Last;
                nameForm.English_First = name.EnglishFirst;
                nameForm.English_Last = name.EnglishLast;
                nameForm.English_Middle = name.EnglishFather;
                nameForm.Id = name.Id.Value;
            }
        }
        public Name GetNameFromForm(NameForm.NameForm nameForm)
        {
            if (nameForm != null)
            {
                Name name = new Name();
                name.First = nameForm.First;
                name.Father = nameForm.Middle;
                name.Last = nameForm.Last;
                name.EnglishFirst = nameForm.English_First;
                name.EnglishLast = nameForm.English_Last;
                name.EnglishFather = nameForm.English_Middle;
                name.Id = nameForm.Id;
                return name;
            }
            else
                return null;
        }
    }
}
