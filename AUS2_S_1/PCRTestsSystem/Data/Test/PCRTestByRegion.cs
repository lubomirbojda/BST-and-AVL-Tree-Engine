using AUS2_S_1.Trees.AVLTree;

namespace AUS2_S_1.PCRTestsSystem.Data.Test
{
    using AUS2_S_1.PCRTestsSystem.Data.Person;
    public class PCRTestByRegion : BasePCRTestComparable<PCRTestByRegion>
    {
        public AVLT<PCRTestByDateAndTime> PCRTestsByDateAndTime { get; private set; } = new AVLT<PCRTestByDateAndTime>();
        public PCRTestByRegion(PCRTest pcrTest, Person person) : base(pcrTest, person)
        {
        }

        public PCRTestByRegion(int regionID) : base(new PCRTest(default, string.Empty, default, default, default, regionID, default))
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
