﻿using OrphanageDataModel.RegularData;

namespace OrphanageV3.Extensions
{
    public static class NameAddressExtensions
    {
        public static string FullName(this Name name)
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

        public static string FullEnglishName(this Name name)
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

        public static string FullAddress(this Address address)
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
    }
}