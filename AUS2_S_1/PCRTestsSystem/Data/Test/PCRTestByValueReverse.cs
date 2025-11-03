namespace AUS2_S_1.PCRTestsSystem.Data.Test
{
    public class PCRTestByValueReverse : BasePCRTestComparable<PCRTestByValueReverse>
    {
        private const double OFFSET  = 0.000000001;
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

            var compareOffset = CompareWithOffset(other.Test.Value, Test.Value);

            if (compareOffset == 0)
            {
                return Test.TestID.CompareTo(other.Test.TestID);
            }

            return compareOffset;
        }

        private static int CompareWithOffset(double testValue, double otherTestValue)
        {
            if (Math.Abs(testValue - otherTestValue) < OFFSET)
            {
                return 0;
            }
            if (testValue < otherTestValue)
            {
                return -1;
            }
            return 1;
        }
    }
}
