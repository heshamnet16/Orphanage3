namespace OrphanageService.Utilities.Interfaces
{
    public interface IStringsFixer
    {
        string FixIdentityCardNumber(string idNumber);

        string FixPhoneNumber(string phoneNumber);

        string FixArabicNames(string name);
    }
}