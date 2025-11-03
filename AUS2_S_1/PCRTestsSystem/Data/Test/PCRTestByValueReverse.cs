namespace AUS2_S_1.PCRTestsSystem.Data.Test
{
    using AUS2_S_1.PCRTestsSystem.Data.Person;
    public class PCRTestByValueReverse : BasePCRTestComparable<PCRTestByValueReverse>
    {
        public PCRTestByValueReverse(PCRTest pcrTest, Person person) : base(pcrTest, person)
        {
        }

        public PCRTestByValueReverse(double value) : base(new PCRTest(default, string.Empty, default, default, default, default, value))
        {
        }

        public override int CompareTo(PCRTestByValueReverse? other)
        {
            if (other == null)
                throw new ArgumentNullException();

            var compareOffset = DoubleComparator.CompareDoubleWithOffset(other.Test.Value, Test.Value);

            if (compareOffset == 0)
            {
                return Test.TestID.CompareTo(other.Test.TestID);
            }

            return compareOffset;
        }
    }
}
