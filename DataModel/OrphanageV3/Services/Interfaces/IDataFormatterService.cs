using OrphanageDataModel.RegularData;
using System;

namespace OrphanageV3.Services.Interfaces
{
    public interface IDataFormatterService
    {
        string GetFullNameString(Name name);

        string GetFullNameEString(Name name);

        string GetAddressString(Address address);

        string GetFormattedDate(DateTime date);
    }
}