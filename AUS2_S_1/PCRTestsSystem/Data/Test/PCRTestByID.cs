namespace AUS2_S_1.PCRTestsSystem.Data.Test
{
    public class PCRTestByID : BasePCRTestComparable<PCRTestByID>
    {
        public PCRTestByID(PCRTest pcrTest) : base(pcrTest)
        {
        }

        public PCRTestByID(int testID) : base(new PCRTest(default, string.Empty, testID, default, default, default, default, default)) 
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
