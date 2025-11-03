namespace AUS2_S_1.PCRTestsSystem.Data.Test
{
    using AUS2_S_1.PCRTestsSystem.Data.Person;
    public abstract class BasePCRTestComparable<T> : IComparable<T>
    {
        public PCRTest Test { get; private set; }
        public Person Person { get; private set; }

        public BasePCRTestComparable(PCRTest pcrTest)
        {
            Test = pcrTest;
            Person = new Person(string.Empty, string.Empty, default, Test.PersonID);
        }

        public BasePCRTestComparable(PCRTest pcrTest, Person person)
        {
            Test = pcrTest;
            Person = person;
        }

        public abstract int CompareTo(T? other);
    }
}
