namespace AUS2_S_1.PCRTestsSystem.Data.Test
{
    public class PCRTestByDateAndTime : BasePCRTest, IComparable<PCRTestByDateAndTime>
    {
        public PCRTestByDateAndTime(DateTime dateAndTime, string personID, int testID, int testStationID, int districtID, int regionID, bool result, double value, string? note = null) : base(dateAndTime, personID, testID, testStationID, districtID, regionID, result, value, note)
        {
        }

        public PCRTestByDateAndTime(DateTime dateAndTime) : base(dateAndTime, string.Empty, default, default, default, default, default, default)
        {
        }

        public int CompareTo(PCRTestByDateAndTime? other)
        {
            if (other == null)
                throw new ArgumentNullException();

            return DateAndTime.CompareTo(other.DateAndTime);
        }
    }
}
