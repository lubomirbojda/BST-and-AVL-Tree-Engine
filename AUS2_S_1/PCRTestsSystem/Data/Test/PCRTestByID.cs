namespace AUS2_S_1.PCRTestsSystem.Data.Test
{
    public class PCRTestByID : BasePCRTest, IComparable<PCRTestByID>
    {
        public PCRTestByID(DateTime dateAndTime, string personID, int testID, int testStationID, int districtID, int regionID, bool result, double value, string? note = null) : base(dateAndTime, personID, testID, testStationID, districtID, regionID, result, value, note)
        {
        }

        public PCRTestByID(int testID) : base(default, string.Empty, testID, default, default, default, default, default)
        {
        }

        public int CompareTo(PCRTestByID? other)
        {
            if (other == null)
                throw new ArgumentNullException();

            return TestID.CompareTo(other.TestID);
        }
    }
}
