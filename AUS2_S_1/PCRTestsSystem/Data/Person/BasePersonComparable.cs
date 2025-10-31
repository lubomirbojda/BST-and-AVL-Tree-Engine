namespace AUS2_S_1.PCRTestsSystem.Data.Person
{
    public abstract class BasePersonComparable<T> : IComparable<T>
    {
        public Person Person { get; set; }

        public BasePersonComparable(Person person) 
        {
            Person = person;
        }

        public abstract int CompareTo(T? other);
    }
}
