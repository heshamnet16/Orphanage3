using Itenso.TimePeriod;
using OrphanageV3.Services.Interfaces;
using System;

namespace OrphanageV3.Services
{
    public class TranslateService : ITranslateService
    {
        public TranslateService()
        {
        }

        public string BooleanToString(bool value)
        {
            if (value)
                return Properties.Resources.BooleanTrue;
            else
                return Properties.Resources.BooleanFalse;
        }

        public string DateToDayString(DateTime date)
        {
            DateDiff dateDiff = new DateDiff(date, DateTime.Now);
            int _IntD = dateDiff.ElapsedMonths;
            switch (_IntD)
            {
                case 0:
                    {
                        return Properties.Resources.LessThanDayString;
                    }
                case 1:
                    {
                        return Properties.Resources.DayString;
                    }
                case 2:
                    {
                        return Properties.Resources.TowDaysString;
                    }
            }
            if ((_IntD >= 3) && (_IntD <= 10))
            {
                return _IntD + " " + Properties.Resources.DaysString;
            }
            else
                return _IntD + " " + Properties.Resources.DaysAccusativeString;
        }

        public string DateToMonthString(DateTime date)
        {
            DateDiff dateDiff = new DateDiff(date, DateTime.Now);
            int _IntD = dateDiff.ElapsedMonths;
            switch (_IntD)
            {
                case 0:
                    {
                        return Properties.Resources.LessThanMonthString;
                    }
                case 1:
                    {
                        return Properties.Resources.MonthString;
                    }
                case 2:
                    {
                        return Properties.Resources.TowMonthesString;
                    }
            }
            if ((_IntD >= 3) && (_IntD <= 10))
            {
                return _IntD + " " + Properties.Resources.MonthesString;
            }
            else
                return _IntD + " " + Properties.Resources.MonthesAccusativeString;
        }

        public string DateToString(DateTime date)
        {
            string ret = "";
            DateDiff dateDiff = new DateDiff(date, DateTime.Now);
            if (dateDiff.ElapsedYears > 0)
            {
                ret += DateToYearsString(date);
            }
            if (dateDiff.ElapsedMonths > 0)
            {
                if (ret.Length > 0)
                {
                    ret += " " + Properties.Resources.AndString + " ";
                }
                ret += DateToMonthString(date);
            }
            if (dateDiff.ElapsedDays > 0)
            {
                if (ret.Length > 0)
                {
                    ret += " " + Properties.Resources.AndString + " ";
                }
                ret += DateToDayString(date);
            }
            return ret;
        }

        public string DateToYearsString(DateTime date)
        {
            DateDiff dateDiff = new DateDiff(date, DateTime.Now);
            switch (dateDiff.ElapsedYears)
            {
                case 0:
                    {
                        return Properties.Resources.LessThanYearString;
                    }
                case 1:
                    {
                        return Properties.Resources.YearString;
                    }
                case 2:
                    {
                        return Properties.Resources.TowYearsString;
                    }
            }
            if ((dateDiff.ElapsedYears >= 3) && (dateDiff.ElapsedYears <= 10))
            {
                return dateDiff.ElapsedYears + " " + Properties.Resources.YearsString;
            }
            else
                return dateDiff.ElapsedYears + " " + Properties.Resources.YearString;
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