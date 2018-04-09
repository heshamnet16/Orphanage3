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