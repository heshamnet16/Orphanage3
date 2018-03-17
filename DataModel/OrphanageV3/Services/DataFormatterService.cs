using OrphanageDataModel.RegularData;
using OrphanageV3.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrphanageV3.Services
{
    public class DataFormatterService : IDataFormatterService
    {
        public IList<string> EnglishNameStrings { get; set; }

        private readonly IApiClient _apiClient;

        public DataFormatterService(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public string GetAddressString(Address address)
        {
            string ret = string.Empty;
            char sep = Properties.Settings.Default.AddressSeparator;
            if (address == null) return string.Empty;
            if (address.Country != null && address.Country.Length > 0)
                ret = address.Country;
            if (address.City != null && address.City.Length > 0)
                if (ret.Length > 0)
                    ret = ret + sep + address.City;
                else
                    ret = address.City;
            if (address.Town != null && address.Town.Length > 0)
                if (ret.Length > 0)
                    ret = ret + sep + address.Town;
                else
                    ret = address.Town;
            if (address.Street != null && address.Street.Length > 0)
                if (ret.Length > 0)
                    ret = ret + sep + address.Street;
                else
                    ret = address.Street;
            return ret;
        }

        public string GetFormattedDate(DateTime date)
        {
            if (date != null)
            {
                return date.ToString(Properties.Settings.Default.GeneralDateFormat);
            }
            else
                return string.Empty;
        }

        public string GetFullNameEString(Name name)
        {
            if (name == null || name.EnglishFirst == null) return string.Empty;
            if (name.EnglishFather == null || name.EnglishFather.Length == 0)
            {
                if (name.EnglishLast == null || name.EnglishLast.Length == 0)
                    return name.EnglishFirst;
                else
                    return name.EnglishFirst + " " + name.EnglishLast;
            }
            else
            {
                if (name.EnglishLast == null || name.EnglishLast.Length == 0)
                {
                    if (Properties.Settings.Default.UseFatherName)
                        return name.EnglishFirst + " " + name.EnglishFather;
                    else
                        return name.EnglishFirst;
                }
                else
                {
                    if (Properties.Settings.Default.UseFatherName)
                    {
                        return name.EnglishFirst + " " + name.EnglishFather + " " + name.EnglishLast;
                    }
                    else
                    {
                        return name.EnglishFirst + " " + name.EnglishLast;
                    }
                }
            }
        }

        public string GetFullNameString(Name name)
        {
            if (name == null || name.First == null) return string.Empty;
            if (name.Father == null || name.Father.Length == 0)
            {
                if (name.Last == null || name.Last.Length == 0)
                    return name.First;
                else
                    return name.First + " " + name.Last;
            }
            else
            {
                if (name.Last == null || name.Last.Length == 0)
                {
                    if (Properties.Settings.Default.UseFatherName)
                        return name.First + " " + name.Father;
                    else
                        return name.First;
                }
                else
                {
                    if (Properties.Settings.Default.UseFatherName)
                    {
                        return name.First + " " + name.Father + " " + name.Last;
                    }
                    else
                    {
                        return name.First + " " + name.Last;
                    }
                }
            }
        }
    }
}
