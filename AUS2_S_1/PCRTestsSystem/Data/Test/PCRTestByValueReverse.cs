namespace AUS2_S_1.PCRTestsSystem.Data.Test
{
    public class PCRTestByValueReverse : BasePCRTestComparable<PCRTestByValueReverse>
    {
        public PCRTestByValueReverse(PCRTest pcrTest) : base(pcrTest)
        {
        }

        public PCRTestByValueReverse(double value) : base(new PCRTest(default, string.Empty, default, default, default, default, default, value))
        {
        }

        public override int CompareTo(PCRTestByValueReverse? other)
        {
            if (other == null)
                throw new ArgumentNullException();

            var compareByValue = other.Test.Value.CompareTo(Test.Value);
            if (compareByValue == 0)
            {
                return Test.TestID.CompareTo(other.Test.TestID);
            }

            return compareByValue;
        }
    }
}
