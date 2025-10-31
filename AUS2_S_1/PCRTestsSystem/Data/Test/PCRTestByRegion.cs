using AUS2_S_1.Trees.AVLTree;

namespace AUS2_S_1.PCRTestsSystem.Data.Test
{
    public class PCRTestByRegion : BasePCRTestComparable<PCRTestByRegion>
    {
        public AVLT<PCRTestByDateAndTime> PCRTestsByDateAndTime { get; private set; } = new AVLT<PCRTestByDateAndTime>();
        public PCRTestByRegion(PCRTest pcrTest) : base(pcrTest)
        {
        }

        public PCRTestByRegion(int regionID) : base(new PCRTest(default, string.Empty, default, default, default, regionID, default, default))
        { 
        }

        public override int CompareTo(PCRTestByRegion? other)
        {
            if (other == null)
                throw new ArgumentNullException();

            return Test.RegionID.CompareTo(other.Test.RegionID);
        }
    }
}
