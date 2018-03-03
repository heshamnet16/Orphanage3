using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
