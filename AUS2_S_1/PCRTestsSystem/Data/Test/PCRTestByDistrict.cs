using AUS2_S_1.Trees.AVLTree;

namespace AUS2_S_1.PCRTestsSystem.Data.Test
{
    using AUS2_S_1.PCRTestsSystem.Data.Person;
    public class PCRTestByDistrict : BasePCRTestComparable<PCRTestByDistrict>
    {
        public AVLT<PCRTestByDateAndTime> PCRTestsByDateAndTime { get; private set; } = new AVLT<PCRTestByDateAndTime>();
        public AVLT<PCRTestByValueReverse> PCRTestsByValueReverse { get; private set; } = new AVLT<PCRTestByValueReverse>();
        
        public PCRTestByDistrict(PCRTest pcrTest, Person person) : base(pcrTest, person)
        {
        }

        public PCRTestByDistrict(int districtID) : base(new PCRTest(default, string.Empty, default, default, districtID, default, default))
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
