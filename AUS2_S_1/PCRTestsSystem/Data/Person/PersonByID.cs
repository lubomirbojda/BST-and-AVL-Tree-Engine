using AUS2_S_1.PCRTestsSystem.Data.Test;
using AUS2_S_1.Trees.AVLTree;

namespace AUS2_S_1.PCRTestsSystem.Data.Person
{
    public class PersonByID : BasePersonComparable<PersonByID>
    {
        public AVLT<PCRTestByID> PCRTestsByID { get; private set; } = new AVLT<PCRTestByID>();
        public AVLT<PCRTestByDateAndTime> PCRTestsByDateAndTime { get; private set; } = new AVLT<PCRTestByDateAndTime>();

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
    }
}
