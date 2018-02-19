using System;

namespace OrphanageService.Services.DataModel
{
    public enum DataTypeEnum
    {
        Unknown = 0,
        Orphan = 1,
        Father = 2,
        Mother = 3,
        Caregiver = 4,
        Family = 5,
        Guarantor = 6,
        User = 7
    }

    public class CheckerResultData<T>
    {
        public int Id { get; set; }

        public T DataValue { set; get; }

        public DataTypeEnum DataType { get; set; }
    }

    public class CheckerResultData
    {
        public int Id { get; set; }

        public object DataValue { set; get; }

        public DataTypeEnum DataType { get; set; }

        public Type ObjectType { get; set; }
    }
}