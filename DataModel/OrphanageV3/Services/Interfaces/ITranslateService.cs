using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrphanageV3.Services.Interfaces
{
    public interface ITranslateService
    {
        string Translate(string source);
        bool IsBoy(string arabicGender);
        string BooleanToString(bool value);
        string DateToYearsString(DateTime date);
        string DateToMonthString(DateTime date);
        string DateToDayString(DateTime date);
        string DateToString(DateTime date);
    }
}
