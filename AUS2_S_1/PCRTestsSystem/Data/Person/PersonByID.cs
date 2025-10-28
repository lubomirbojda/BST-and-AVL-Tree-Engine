using AUS2_S_1.PCRTestsSystem.Data.Test;
using AUS2_S_1.Trees.AVLTree;

namespace AUS2_S_1.PCRTestsSystem.Data.Person
{
    public class PersonByID : BasePerson, IComparable<PersonByID>
    {
        public AVLT<PCRTestByID> PCRTestsByID { get; private set; } = new AVLT<PCRTestByID>();
        public PersonByID(string firstName, string lastName, DateOnly birthDate, string personID) : base(firstName, lastName, birthDate, personID)
        {
        }

        public PersonByID(string personID) : base(string.Empty, string.Empty, default, personID)
        { 
        }

        public int CompareTo(PersonByID? other)
        {
            if (other == null)
                throw new ArgumentNullException();

            return string.CompareOrdinal(PersonID, other.PersonID);
        }
    }
}
