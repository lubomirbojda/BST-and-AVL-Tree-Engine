using AUS2_S_1.Trees.AVLTree;

namespace AUS2_S_1.PCRTestsSystem.Data.Test
{
    public class PCRTestByDistrict : BasePCRTestComparable<PCRTestByDistrict>
    {
        public AVLT<PCRTestByDateAndTime> PCRTestsByDateAndTime { get; private set; } = new AVLT<PCRTestByDateAndTime>();
        public AVLT<PCRTestByValueReverse> PCRTestsByValueReverse { get; private set; } = new AVLT<PCRTestByValueReverse>();
        
        public PCRTestByDistrict(PCRTest pcrTest) : base(pcrTest)
        {
        }

        public PCRTestByDistrict(int districtID) : base(new PCRTest(default, string.Empty, default, default, districtID, default, default, default))
        { 
        }

        public override int CompareTo(PCRTestByDistrict? other)
        {
            if (other == null)
                throw new ArgumentNullException();

            return Test.DistrictID.CompareTo(other.Test.DistrictID);
        }
    }
}
