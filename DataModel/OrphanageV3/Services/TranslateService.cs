using OrphanageV3.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OrphanageV3.Services
{
    public class TranslateService : ITranslateService
    {
        public TranslateService()
        {
        }

        public bool IsBoy(string arabicGender)
        {
            if (arabicGender.Contains("ذ"))
                return true;
            else
                return false;
        }

        public string Translate(string source)
        {
            if (source == null) return null;
            Properties.Resources resources = new Properties.Resources();
            Type resourcesType = resources.GetType();
            var props = resourcesType.GetProperties();
            foreach (var prop in props)
            {
                if (prop.Name.ToLower() == source.ToLower())
                    return prop.GetValue(resources).ToString();
            }
            return null;
        }
        
    }
}
