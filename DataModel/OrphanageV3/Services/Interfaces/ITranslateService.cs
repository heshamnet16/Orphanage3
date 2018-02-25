using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrphanageV3.Services.Interfaces
{
    public interface ITranslateService
    {
        void Translate(ref Type source);
        string Translate(string source);
    }
}
