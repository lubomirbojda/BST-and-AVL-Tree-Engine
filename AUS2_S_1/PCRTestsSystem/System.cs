using AUS2_S_1.PCRTestsSystem.Data.Person;
using AUS2_S_1.PCRTestsSystem.Data.Test;
using AUS2_S_1.Trees.AVLTree;

namespace AUS2_S_1.PCRTestsSystem
{
    public class System
    {
        public AVLT<PersonByID> PersonsByID { get; private set; } = new AVLT<PersonByID>();
        public AVLT<PCRTestByDistrict> PCRTestsByDistrict { get; private set; } = new AVLT<PCRTestByDistrict>();
        public AVLT<PCRTestByRegion> PCRTestsByRegion { get; private set; } = new AVLT<PCRTestByRegion>();
        public AVLT<PCRTestByDateAndTime> PCRTestsByDateAndTime { get; private set; } = new AVLT<PCRTestByDateAndTime>();

        //01 - Insert new test result
        public void InsertNewPCRTest(PCRTest testToInsert)
        {
            //PersonsByID -> PCRTestsByID
            var person = PersonsByID.Find(new PersonByID(testToInsert.PersonID));
            person?.PCRTestsByID.Insert(new PCRTestByID(testToInsert));

            //PCRTestsByDistrict
            PCRTestsByDistrict.Insert(new PCRTestByDistrict(testToInsert));

            //PCRTestsByDistrict -> PCRTestsByDateAndTime
            var testWithDistrictId = PCRTestsByDistrict.Find(new PCRTestByDistrict(testToInsert.DistrictID));
            testWithDistrictId?.PCRTestsByDateAndTime.Insert(new PCRTestByDateAndTime(testToInsert));

            //PCRTestsByDistrict -> PCRTestsByValue
            testWithDistrictId?.PCRTestsByValueReverse.Insert(new PCRTestByValueReverse(testToInsert));

            //PCRTestsByRegion
            PCRTestsByRegion.Insert(new PCRTestByRegion(testToInsert));

            //PCRTestsByRegion -> PCRTestsByDateAndTime
            var testWithRegionId = PCRTestsByRegion.Find(new PCRTestByRegion(testToInsert.DistrictID));
            testWithRegionId?.PCRTestsByDateAndTime.Insert(new PCRTestByDateAndTime(testToInsert));

            //PCRTestsByDateAndTime
            PCRTestsByDateAndTime.Insert(new PCRTestByDateAndTime(testToInsert));
        }

        //02 - Find result of test (defined by testID) for person (defined by personID)
        public PCRTest? FindTestByTestIdAndPersonId(int testId, string personID)
        {
            var person = PersonsByID.Find(new PersonByID(personID));
            if (person == null) return null;

            var test = person.PCRTestsByID.Find(new PCRTestByID(testId));
            return test?.Test;
        }

        //03 - Find all PCRTests for person (defined by personID) sorted by date and time
        public IEnumerable<PCRTest> FindAllTestsForPersonSortedByDateAndTime(string personID)
        {
            var person = PersonsByID.Find(new PersonByID(personID));
            if (person == null) return [];

            return (IEnumerable<PCRTest>)person.PCRTestsByID.InOrder().OrderBy(x => x.Test.DateAndTime);
        }

        //04 - Find all positive tests in date interval for certain district
        public IEnumerable<PCRTest> FindAllPositiveTestsInDateIntervalForDistrict(DateTime dateIntervalMin, DateTime dateIntervalMax, int districtID)
        {
            return FindAllTestsInDateIntervalForDistrict(dateIntervalMin, dateIntervalMax, districtID).Where(x => x.Result == true);
        }

        //05 - Find all tests in date interval for certain district
        public IEnumerable<PCRTest> FindAllTestsInDateIntervalForDistrict(DateTime dateIntervalMin, DateTime dateIntervalMax, int districtID)
        {
            var district = PCRTestsByDistrict.Find(new PCRTestByDistrict(districtID));
            if (district == null) return [];
            
            return (IEnumerable<PCRTest>)district.PCRTestsByDateAndTime.FindInterval
            (
                new PCRTestByDateAndTime(dateIntervalMin, int.MinValue), new PCRTestByDateAndTime(dateIntervalMax, int.MaxValue)
            );
        }

        //06 - Find all positive tests in date interval for certain region
        public IEnumerable<PCRTest> FindAllPositiveTestsInDateIntervalForRegion(DateTime dateIntervalMin, DateTime dateIntervalMax, int regionID)
        {
            return FindAllTestsInDateIntervalForRegion(dateIntervalMin, dateIntervalMax, regionID).Where(x => x.Result == true);
        }

        //07 - Find all tests in date interval for certain region
        public IEnumerable<PCRTest> FindAllTestsInDateIntervalForRegion(DateTime dateIntervalMin, DateTime dateIntervalMax, int regionID)
        {
            var region = PCRTestsByRegion.Find(new PCRTestByRegion(regionID));
            if (region == null) return [];
            
            return (IEnumerable<PCRTest>)region.PCRTestsByDateAndTime.FindInterval
            (
                new PCRTestByDateAndTime(dateIntervalMin, int.MinValue), new PCRTestByDateAndTime(dateIntervalMax, int.MaxValue)
            );
        }

        //08 - Find all positive tests in date interval
        public IEnumerable<PCRTest> FindAllPositiveTestsInDateInterval(DateTime dateIntervalMin, DateTime dateIntervalMax)
        {
            return FindAllTestsInDateInterval(dateIntervalMin, dateIntervalMax).Where(x => x.Result == true);
        }

        //09 - Find all tests in date interval
        public IEnumerable<PCRTest> FindAllTestsInDateInterval(DateTime dateIntervalMin, DateTime dateIntervalMax)
        {
            return (IEnumerable<PCRTest>)PCRTestsByDateAndTime.FindInterval(
                new PCRTestByDateAndTime(dateIntervalMin, int.MinValue), new PCRTestByDateAndTime(dateIntervalMax, int.MaxValue)
            );
        }

        //10 - Find all sick persons for district at some date (person is sick for x days from positive test)
        public IEnumerable<Person> FindAllSickPersonsForDistrictAtDate(int districtID, DateOnly atDate, int daysOfPositiveness)
        {
            var district = PCRTestsByDistrict.Find(new PCRTestByDistrict(districtID));
            if (district == null) 
                yield break;

            var minValueToFind = atDate.AddDays(-daysOfPositiveness).ToDateTime(TimeOnly.MinValue);
            var maxValueToFind = atDate.ToDateTime(TimeOnly.MinValue);

            var testsAtDate = (IEnumerable<PCRTest>)district.PCRTestsByDateAndTime.FindInterval(
                new PCRTestByDateAndTime(minValueToFind, int.MinValue), new PCRTestByDateAndTime (maxValueToFind, int.MaxValue)
            );

            var persons = new AVLT<PersonByID>();

            foreach (var test in testsAtDate)
            {
                if (!test.Result) continue;

                var personById = new PersonByID(test.PersonID);
                //if found, person was already returned
                if (persons.Find(personById) != null) continue;

                persons.Insert(personById);

                var returningPerson = PersonsByID.Find(personById);
                if (returningPerson != null)
                    yield return returningPerson.Person;
            }
        }

        //11 - Find all sick persons for district at some date (person is sick for x days from positive test) ordered by test value
        public IEnumerable<Person> FindAllSickPersonsForDistrictAtDateOrderedByTestValue(int districtID, DateOnly atDate, int daysOfPositiveness)
        {
            var district = PCRTestsByDistrict.Find(new PCRTestByDistrict(districtID));
            if (district == null)
                yield break;

            var minValueDate = atDate.AddDays(-daysOfPositiveness).ToDateTime(TimeOnly.MinValue);
            var maxValueDate = atDate.ToDateTime(TimeOnly.MinValue);

            var persons = new AVLT<PersonByID>();

            foreach (var test in district.PCRTestsByValueReverse.InOrder())
            {
                if (test.Test.DateAndTime < minValueDate || test.Test.DateAndTime > maxValueDate)
                    continue;

                if (!test.Test.Result)
                    continue;

                var personById = new PersonByID(test.Test.PersonID);
                //if found, person was already returned
                if (persons.Find(personById) != null) continue;

                persons.Insert(personById);

                var returningPerson = PersonsByID.Find(personById);
                if (returningPerson != null)
                    yield return returningPerson.Person;
            }

        }

        //12 - Find all sick persons for region at some date (person is sick for x days from positive test)
        public IEnumerable<Person> FindAllSickPersonsForRegionAtDate(int regionID, DateOnly atDate, int daysOfPositiveness)
        {
            var region = PCRTestsByRegion.Find(new PCRTestByRegion(regionID));
            if (region == null)
                yield break;

            var minValueToFind = atDate.AddDays(-daysOfPositiveness).ToDateTime(TimeOnly.MinValue);
            var maxValueToFind = atDate.ToDateTime(TimeOnly.MinValue);

            var testsAtDate = (IEnumerable<PCRTest>)region.PCRTestsByDateAndTime.FindInterval(
                new PCRTestByDateAndTime(minValueToFind, int.MinValue), new PCRTestByDateAndTime(maxValueToFind, int.MaxValue)
            );

            var persons = new AVLT<PersonByID>();

            foreach (var test in testsAtDate)
            {
                if (!test.Result) continue;

                var personById = new PersonByID(test.PersonID);
                //if found, person was already returned
                if (persons.Find(personById) != null) continue;

                persons.Insert(personById);

                var returningPerson = PersonsByID.Find(personById);
                if (returningPerson != null)
                    yield return returningPerson.Person;
            }
        }

        //13 - Find all sick persons at some date (person is sick for x days from positive test)
        public IEnumerable<Person> FindAllSickPersonsAtDate(DateOnly atDate, int daysOfPositiveness)
        {
            var minValueToFind = atDate.AddDays(-daysOfPositiveness).ToDateTime(TimeOnly.MinValue);
            var maxValueToFind = atDate.ToDateTime(TimeOnly.MinValue);

            var testsAtDate = (IEnumerable<PCRTest>)PCRTestsByDateAndTime.FindInterval(
                new PCRTestByDateAndTime(minValueToFind, int.MinValue), new PCRTestByDateAndTime(maxValueToFind, int.MaxValue)
            );

            var persons = new AVLT<PersonByID>();
            foreach (var test in testsAtDate)
            {
                if (!test.Result) continue;

                var personById = new PersonByID(test.PersonID);
                //if found, person was already returned
                if (persons.Find(personById) != null) continue;

                persons.Insert(personById);

                var returningPerson = PersonsByID.Find(personById);
                if (returningPerson != null)
                    yield return returningPerson.Person;
            }
        }

        //14 - Find one sick person at date from every district with highest test value
        public IEnumerable<Person> FindOneSickPersonAtDateFromEveryDistrictWithHighestValue(DateOnly atDate, int daysOfPositiveness)
        {
            var minValueDate = atDate.AddDays(-daysOfPositiveness).ToDateTime(TimeOnly.MinValue);
            var maxValueDate = atDate.ToDateTime(TimeOnly.MinValue);

            foreach (var district in PCRTestsByDistrict.InOrder())
            {
                foreach (var test in district.PCRTestsByValueReverse.InOrder())
                {
                    if (test.Test.DateAndTime < minValueDate || test.Test.DateAndTime > maxValueDate)
                        continue;

                    if (!test.Test.Result)
                        continue;

                    var personById = PersonsByID.Find(new PersonByID(test.Test.PersonID));
                    if (personById != null)
                    {
                        yield return personById.Person;
                        break;
                    }   
                }
            }
        }

        //15 - Find all districts ordered by count of sick people at date
        
    }
}
