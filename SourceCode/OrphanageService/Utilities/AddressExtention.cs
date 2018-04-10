using OrphanageDataModel.RegularData;

namespace OrphanageService.Utilities
{
    public static class AddressExtention
    {
        public static Address Clean(this Address address)
        {
            if (address.HomePhone == "(000)0000-000" || address.HomePhone == "(031)0000-000" ||
                address.HomePhone == "(____)___-___" || address.HomePhone == "(0000)000-000")
                address.HomePhone = null;

            if (address.CellPhone == "(000)0000-000" || address.CellPhone == "(031)0000-000" ||
                address.CellPhone == "(____)___-___" || address.CellPhone == "(0000)000-000")
                address.CellPhone = null;

            if (address.WorkPhone == "(000)0000-000" || address.WorkPhone == "(031)0000-000" ||
                address.WorkPhone == "(____)___-___" || address.WorkPhone == "(0000)000-000")
                address.WorkPhone = null;

            if (address.Fax == "(000)0000-000" || address.Fax == "(031)0000-000" ||
                address.Fax == "(____)___-___" || address.Fax == "(0000)000-000")
                address.Fax = null;
            if (address.Email != null && address.Email.Length == 0) address.Email = null;
            if (address.Facebook != null && address.Facebook.Length == 0) address.Facebook = null;
            if (address.Twitter != null && address.Twitter.Length == 0) address.Twitter = null;
            if (address.Country != null && address.Country.Length == 0) address.Country = null;
            if (address.Town != null && address.Town.Length == 0) address.Town = null;

            return address;
        }
    }
}