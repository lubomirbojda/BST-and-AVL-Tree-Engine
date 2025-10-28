using AUS2_S_1.Trees.AVLTree;

namespace AUS2_S_1.PCRTestsSystem.Data.Test
{
    public class PCRTestByRegion : BasePCRTest, IComparable<PCRTestByRegion>
    {
        public AVLT<PCRTestByDateAndTime> PCRTestsByDateAndTime { get; private set; } = new AVLT<PCRTestByDateAndTime>();
        public PCRTestByRegion(DateTime dateAndTime, string personID, int testID, int testStationID, int districtID, int regionID, bool result, double value, string? note = null) : base(dateAndTime, personID, testID, testStationID, districtID, regionID, result, value, note)
        {
        }

        public PCRTestByRegion(int regionID) : base(default, string.Empty, default, default, default, regionID, default, default)
        {
        }

        public int CompareTo(PCRTestByRegion? other)
        {
            if (other == null)
                throw new ArgumentNullException();

            return RegionID.CompareTo(other.RegionID);
        }
    }
}
