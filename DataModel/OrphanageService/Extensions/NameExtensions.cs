using OrphanageDataModel.RegularData;

namespace OrphanageV3.Extensions
{
    public static class NameExtensions
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
                    return name.First + " " + name.Father;
                }
                else
                {
                    return name.First + " " + name.Father + " " + name.Last;
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
                    return name.EnglishFirst + " " + name.EnglishFather;
                }
                else
                {
                    return name.EnglishFirst + " " + name.EnglishFather + " " + name.EnglishLast;
                }
            }
        }
    }
}