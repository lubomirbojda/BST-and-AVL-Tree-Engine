namespace AUS2_S_1.PCRTestsSystem.Data.Test
{
    public abstract class BasePCRTestComparable<T> : IComparable<T>
    {
        public PCRTest Test { get; private set; }

        public BasePCRTestComparable(PCRTest pcrTest)
        {
            Test = pcrTest;
        }

        public abstract int CompareTo(T? other);
    }
}
