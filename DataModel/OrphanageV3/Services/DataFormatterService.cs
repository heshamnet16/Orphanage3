using OrphanageDataModel.RegularData;
using OrphanageV3.Services.Interfaces;
using System;
using System.Collections.Generic;

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
    }
}