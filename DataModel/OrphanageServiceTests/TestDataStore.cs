namespace OrphanageServiceTests
{
    public static class TestDataStore
    {
        public static OrphanageDataModel.RegularData.Name GetName()
        {
            return new OrphanageDataModel.RegularData.Name()
            {
                First = "TestFirstName",
                Father = "TestFatherName",
                Last = "TestLastName",
                EnglishFirst = "TestEFirstName",
                EnglishFather = "TestEFatherName",
                EnglishLast = "TestELastName"
            };
        }

        public static OrphanageDataModel.RegularData.Address GetAddress()
        {
            return new OrphanageDataModel.RegularData.Address()
            {
                Country = "TestSyria",
                City = "TestHoms",
                Town = "TestTalbissa",
                Street = "Street"
            };
        }
    }
}