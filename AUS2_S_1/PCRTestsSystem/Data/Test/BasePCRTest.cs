namespace AUS2_S_1.PCRTestsSystem.Data.Test
{
    public class BasePCRTest
    {
        public DateTime DateAndTime { get; private set; }
        public string PersonID { get; private set; }
        public int TestID { get; private set; }
        public int TestStationID { get; private set; }
        public int DistrictID { get; private set; }
        public int RegionID { get; private set; }
        public bool Result { get; private set; }
        public double Value { get; private set; }
        public string? Note { get; private set; }

        public BasePCRTest(DateTime dateAndTime, string personID, int testID, int testStationID, int districtID, int regionID, bool result, double value, string? note = null)
        {
            DateAndTime = dateAndTime;
            PersonID = personID;
            TestID = testID;
            TestStationID = testStationID;
            DistrictID = districtID;
            RegionID = regionID;
            Result = result;
            Value = value;
            Note = note;
        }
    }
}
