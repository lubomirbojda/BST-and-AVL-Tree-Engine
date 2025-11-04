using System.Globalization;
using AUS2_S_1.PCRTestsSystem.Data.Test;
using AUS2_S_1.PCRTestsSystem.Interfaces;
using AUS2_S_1.Trees.AVLTree;

namespace AUS2_S_1.PCRTestsSystem.Data.Person
{
    public class PersonByID : BasePersonComparable<PersonByID>, ISaveble<PersonByID>
    {
        public AVLT<PCRTestByID> PCRTestsByID { get; private set; } = new AVLT<PCRTestByID>();
        public AVLT<PCRTestByDateAndTime> PCRTestsByDateAndTime { get; private set; } = new AVLT<PCRTestByDateAndTime>();

        public PersonByID() : base(new Person(string.Empty, string.Empty, default, string.Empty))
        {
        }

        public PersonByID(Person person) : base(person)
        {
        }

        public PersonByID(string personID) : base(new Person(string.Empty, string.Empty, default, personID))
        { 
        }

        public override int CompareTo(PersonByID? other)
        {
            if (other == null)
                throw new ArgumentNullException();

            return string.CompareOrdinal(Person.PersonID, other.Person.PersonID);
        }

        public string CreateCSVLine(PersonByID data)
        {
            return $"{data.Person.FirstName};" +
                   $"{data.Person.LastName};" +
                   $"{data.Person.BirthDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)};" +
                   $"{data.Person.PersonID}";
        }

        public PersonByID LoadFromCSVLine(string line)
        {
            string[] parts = line.Split(';');
            if (parts.Length != 4)
            {
                throw new FormatException("CSV line needs to have 4 parts!");
            }
            string firstName = parts[0];
            string lastName = parts[1];
            string personId = parts[3];
            var birthDate = DateOnly.ParseExact(parts[2], "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None);

            return new PersonByID(new Person(firstName, lastName, birthDate, personId));
        }
    }
}
