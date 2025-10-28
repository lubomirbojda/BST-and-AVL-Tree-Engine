using AUS2_S_1.PCRTestsSystem.Data.Person;
using AUS2_S_1.PCRTestsSystem.Data.Test;
using AUS2_S_1.Trees.AVLTree;

namespace AUS2_S_1.PCRTestsSystem
{
    public class System
    {
        public AVLT<PersonByID> PersonsById { get; private set; } = new AVLT<PersonByID>();
        public AVLT<PCRTestByRegion> PCRTestsByRegion { get; private set; } = new AVLT<PCRTestByRegion>();

        //01 - Insert new test result
        public bool InsertNewPCRTest(BasePCRTest testToInsert)
        {
            var person = PersonsById.Find(new PersonByID(testToInsert.PersonID));
            if (person == null)
                return false;

            person.PCRTestsByID.Insert(new PCRTestByID(testToInsert.TestID));

            return true;
        }

        //02 - Find result of test (defined by testID) for person (defined by personID)
        public BasePCRTest? FindTestByTestIdAndPersonId(int testId, string personID)
        {
            var person = PersonsById.Find(new PersonByID(personID));
            if (person == null) return null;

            var test = person.PCRTestsByID.Find(new PCRTestByID(testId));
            return test;
        }

        //03 - Find all PCRTests for person (defined by personID) sorted by date and time
        public IEnumerable<BasePCRTest> FindAllTestsForPersonSortedByDateAndTime(string personID)
        {
            var person = PersonsById.Find(new PersonByID(personID));
            if (person == null) return [];

            return person.PCRTestsByID.InOrder().OrderBy(x => x.DateAndTime);
        }

        //04 - Find all positive tests in date interval for certain region
        public IEnumerable<BasePCRTest> FindAllPositiveTestsInDateIntervalForRegion(DateTime dateIntervalMin, DateTime dateIntervalMax, int regionID)
        {
            var region = PCRTestsByRegion.Find(new PCRTestByRegion(regionID));
            if (region == null) return [];

            return region.PCRTestsByDateAndTime.FindInterval
            (
                new PCRTestByDateAndTime(dateIntervalMin), new PCRTestByDateAndTime(dateIntervalMax)
            ).Where(x => x.Result == true);
        }

        //05 - Find all tests in date interval for certain region
        public IEnumerable<BasePCRTest> FindAllTestsInDateIntervalForRegion(DateTime dateIntervalMin, DateTime dateIntervalMax, int regionID)
        {
            var region = PCRTestsByRegion.Find(new PCRTestByRegion(regionID));
            if (region == null) return [];
            
            return region.PCRTestsByDateAndTime.FindInterval
            (
                new PCRTestByDateAndTime(dateIntervalMin), new PCRTestByDateAndTime(dateIntervalMax)
            );
        }
    }
}
