using OrphanageDataModel.RegularData;
using System;

namespace OrphanageV3.Services.Interfaces
{
    public interface IDataFormatterService
    {
        string GetFormattedDate(DateTime date);
    }
}