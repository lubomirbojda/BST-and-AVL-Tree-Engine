namespace AUS2_S_1.PCRTestsSystem.Data.Person
{
    public class Person
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public DateOnly BirthDate { get; private set; }
        public string PersonID {  get; private set; }

        public Person(string firstName, string lastName, DateOnly birthDate, string personID)
        {
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
            PersonID = personID;
        }
    }
}
