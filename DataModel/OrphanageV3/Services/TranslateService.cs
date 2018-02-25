using OrphanageV3.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrphanageV3.Services
{
    public class TranslateService : ITranslateService
    {
        public void Translate(ref Type source)
        {
            throw new NotImplementedException();
        }

        public string Translate(string source)
        {
            Properties.Resources resources = new Properties.Resources();
            Type resourcesType = resources.GetType();
            var propInfo = resourcesType.GetProperty(source);
            return propInfo.GetValue(resources).ToString();
        }
    }
}
