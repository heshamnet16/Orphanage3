using OrphanageDataModel.RegularData;
using System;

namespace OrphanageV3.Services.Interfaces
{
    public interface IDataFormatterService
    {
        string GetAddressString(Address address);

        string GetFormattedDate(DateTime date);
    }
}