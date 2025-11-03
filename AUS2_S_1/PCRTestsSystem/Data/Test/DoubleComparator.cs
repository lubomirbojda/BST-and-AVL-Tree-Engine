namespace AUS2_S_1.PCRTestsSystem.Data.Test
{
    public static class DoubleComparator
    {
        private const double OFFSET = 0.000000001;
        public static int CompareDoubleWithOffset(double testValue, double otherTestValue)
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
