using AUS2_S_1.Trees.AVLTree;

namespace AUS2_S_1.PCRTestsSystem.Data.Test
{
    public class PCRTestByTestStation : BasePCRTestComparable<PCRTestByTestStation>
    {
        public AVLT<PCRTestByDateAndTime> PCRTestsByDateAndTime { get; private set; } = new AVLT<PCRTestByDateAndTime>();

        public PCRTestByTestStation(PCRTest pcrTest) : base(pcrTest)
        {
        }

        public PCRTestByTestStation(int testStationID) : base(new PCRTest(default, string.Empty, default, testStationID, default, default, default, default))
        {
        }

        public override int CompareTo(PCRTestByTestStation? other)
        {
            if (other == null)
                throw new ArgumentNullException();

            return Test.TestStationID.CompareTo(other.Test.TestStationID);
        }
    }
}
