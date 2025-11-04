using System.Globalization;
using AUS2_S_1.PCRTestsSystem.Interfaces;

namespace AUS2_S_1.PCRTestsSystem.Data.Test
{
    public class PCRTest
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

        public PCRTest(DateTime dateAndTime, string personID, int testID, int testStationID, int districtID, int regionID, double value, string? note = null)
        {
            DateAndTime = dateAndTime;
            PersonID = personID;
            TestID = testID;
            TestStationID = testStationID;
            DistrictID = districtID;
            RegionID = regionID;
            Value = value;
            if (DoubleComparator.CompareDoubleWithOffset(value, 3) > 0)
                Result = true;
            else
                Result = false;
            Note = note;
        }
    }
}
