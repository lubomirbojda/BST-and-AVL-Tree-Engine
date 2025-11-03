namespace AUS2_S_1.PCRTestsSystem.Data.Test
{
    using AUS2_S_1.PCRTestsSystem.Data.Person;
    public class PCRTestByID : BasePCRTestComparable<PCRTestByID>
    {
        public PCRTestByID(PCRTest pcrTest, Person person) : base(pcrTest, person)
        {
        }

        public PCRTestByID(int testID) : base(new PCRTest(default, string.Empty, testID, default, default, default, default)) 
        {
        }

        public override int CompareTo(PCRTestByID? other)
        {
            if (other == null)
                throw new ArgumentNullException();

            return Test.TestID.CompareTo(other.Test.TestID);
        }
    }
}
