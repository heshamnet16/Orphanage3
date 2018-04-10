using OrphanageService.Utilities.Interfaces;

namespace OrphanageService.Utilities
{
    public class StringsFixer : IStringsFixer
    {
        public string FixArabicNames(string name)
        {
            return name.Replace("أ", "ا").Replace("إ", "ا").Replace("آ", "ا").Replace("لآ", "لا").Replace("لإ", "لا");
        }

        public string FixIdentityCardNumber(string idNumber)
        {
            if (idNumber == null || idNumber.Length < 4) return null;
            string temp = idNumber;
            while (temp.Length < 10)
            {
                temp = "0" + temp;
            }
            return temp;
        }

        public string FixPhoneNumber(string phoneNumber)
        {
            if (phoneNumber == null || phoneNumber.Length == 0) return null;
            string ret = phoneNumber.Replace(" ", "");
            ret = ret.Replace("-", "");
            ret = ret.Replace(",", "");
            ret = ret.Replace("(", "");
            ret = ret.Replace(")", "");
            ret = ret.Replace("[", "");
            ret = ret.Replace("]", "");
            ret = ret.Replace("_", "");
            ret = ret.Replace("\\", "");
            ret = ret.Replace("/", "");
            return ret;
        }
    }
}