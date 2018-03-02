using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrphanageV3.Services.Interfaces
{
    public interface ITranslateService
    {
        string Translate(string source);
        bool IsBoy(string arabicGender);
    }
}
