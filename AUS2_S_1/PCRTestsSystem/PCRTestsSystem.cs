using AUS2_S_1.PCRTestsSystem.Data;
using AUS2_S_1.PCRTestsSystem.Data.Person;
using AUS2_S_1.PCRTestsSystem.Data.Test;
using AUS2_S_1.Trees.AVLTree;

namespace AUS2_S_1.PCRTestsSystem
{
    public class PCRTestsSystem
    {
        private readonly AVLT<PersonByID> PersonsByID = new();
        private readonly AVLT<PCRTestByDistrict> PCRTestsByDistrict = new();
        private readonly AVLT<PCRTestByRegion> PCRTestsByRegion = new();
        private readonly AVLT<PCRTestByDateAndTime> PCRTestsByDateAndTime = new();
        private readonly AVLT<PCRTestByTestStation> PCRTestsByTestStation = new();
        private readonly AVLT<PCRTestByID> PCRTestsByID = new();

        //01 - Insert new test result
        public void InsertNewPCRTest(PCRTest testToInsert)
        {
            //PersonsByID -> PCRTestsByID
            var person = PersonsByID.Find(new PersonByID(testToInsert.PersonID));
            person?.PCRTestsByID.Insert(new PCRTestByID(testToInsert));

            //PersonsByID -> PCRTestsByDateAndTime
            person?.PCRTestsByDateAndTime.Insert(new PCRTestByDateAndTime(testToInsert));

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

            //PCRTestsByTestStation
            PCRTestsByTestStation.Insert(new PCRTestByTestStation(testToInsert));

            //PCRTestsByTestStation -> PCRTestsByDateAndTime
            var testWithTestStation = PCRTestsByTestStation.Find(new PCRTestByTestStation(testToInsert.TestStationID));
            testWithTestStation?.PCRTestsByDateAndTime.Insert(new PCRTestByDateAndTime(testToInsert));

            //PCRTestsByID
            PCRTestsByID.Insert(new PCRTestByID(testToInsert));
        }

        //02 - Find result of test (defined by testID) for person (defined by personID)
        public PCRTest? FindTestByTestIdAndPersonId(int testId, string personID)
        {
            var person = PersonsByID.Find(new PersonByID(personID));
            if (person == null) return null;

            var test = person.PCRTestsByID.Find(new PCRTestByID(testId));
            return test?.Test;
        }

        //03 - Find all PCRTests for person (defined by personID) ordered by date and time
        public IEnumerable<PCRTest> FindAllTestsForPersonOrderedByDateAndTime(string personID)
        {
            var person = PersonsByID.Find(new PersonByID(personID));
            if (person == null) return [];

            return person.PCRTestsByDateAndTime.InOrder().Select(x => x.Test);
        }

        //04 - Find all positive tests in date interval for certain district
        public IEnumerable<PCRTest> FindAllPositiveTestsInDateIntervalForDistrict(DateTime dateIntervalMin, DateTime dateIntervalMax, int districtID)
        {
            foreach (var test in FindAllTestsInDateIntervalForDistrict(dateIntervalMin, dateIntervalMax, districtID))
            {
                if (test.Result)
                    yield return test;
            }
            //return FindAllTestsInDateIntervalForDistrict(dateIntervalMin, dateIntervalMax, districtID).Where(x => x.Result == true);
        }

        //05 - Find all tests in date interval for certain district
        public IEnumerable<PCRTest> FindAllTestsInDateIntervalForDistrict(DateTime dateIntervalMin, DateTime dateIntervalMax, int districtID)
        {
            var district = PCRTestsByDistrict.Find(new PCRTestByDistrict(districtID));
            if (district == null) return [];
            
            return district.PCRTestsByDateAndTime.FindInterval
            (
                new PCRTestByDateAndTime(dateIntervalMin, int.MinValue), new PCRTestByDateAndTime(dateIntervalMax, int.MaxValue)
            ).Select(x => x.Test);
        }

        //06 - Find all positive tests in date interval for certain region
        public IEnumerable<PCRTest> FindAllPositiveTestsInDateIntervalForRegion(DateTime dateIntervalMin, DateTime dateIntervalMax, int regionID)
        {
            foreach (var test in FindAllTestsInDateIntervalForRegion(dateIntervalMin, dateIntervalMax, regionID))
            {
                if (test.Result)
                    yield return test;
            }

            //return FindAllTestsInDateIntervalForRegion(dateIntervalMin, dateIntervalMax, regionID).Where(x => x.Result == true);
        }

        //07 - Find all tests in date interval for certain region
        public IEnumerable<PCRTest> FindAllTestsInDateIntervalForRegion(DateTime dateIntervalMin, DateTime dateIntervalMax, int regionID)
        {
            var region = PCRTestsByRegion.Find(new PCRTestByRegion(regionID));
            if (region == null) return [];
            
            return region.PCRTestsByDateAndTime.FindInterval
            (
                new PCRTestByDateAndTime(dateIntervalMin, int.MinValue), new PCRTestByDateAndTime(dateIntervalMax, int.MaxValue)
            ).Select(x => x.Test);
        }

        //08 - Find all positive tests in date interval
        public IEnumerable<PCRTest> FindAllPositiveTestsInDateInterval(DateTime dateIntervalMin, DateTime dateIntervalMax)
        {
            foreach (var test in FindAllTestsInDateInterval(dateIntervalMin, dateIntervalMax))
            {
                if (test.Result) 
                    yield return test;
            }
            //return FindAllTestsInDateInterval(dateIntervalMin, dateIntervalMax).Where(x => x.Result == true);
        }

        //09 - Find all tests in date interval
        public IEnumerable<PCRTest> FindAllTestsInDateInterval(DateTime dateIntervalMin, DateTime dateIntervalMax)
        {
            return PCRTestsByDateAndTime.FindInterval(
                new PCRTestByDateAndTime(dateIntervalMin, int.MinValue), new PCRTestByDateAndTime(dateIntervalMax, int.MaxValue)
            ).Select(x => x.Test);
        }

        //10 - Find all sick persons for district at some date (person is sick for x days from positive test)
        public IEnumerable<Person> FindAllSickPersonsForDistrictAtDate(int districtID, DateOnly atDate, int daysOfPositiveness)
        {
            var district = PCRTestsByDistrict.Find(new PCRTestByDistrict(districtID));
            if (district == null) 
                yield break;

            var (minValueDate, maxValueDate) = GetPositiveDaysFromDate(atDate, daysOfPositiveness);

            var testsAtDate = GetTestsInPositiveDaysRange(district.PCRTestsByDateAndTime, minValueDate, maxValueDate);

            foreach (var person in GetAllSickPersonsFromTests(testsAtDate))
            {
                yield return person;
            }
        }

        //11 - Find all sick persons for district at some date (person is sick for x days from positive test) ordered by test value
        public IEnumerable<Person> FindAllSickPersonsForDistrictAtDateOrderedByTestValue(int districtID, DateOnly atDate, int daysOfPositiveness)
        {
            var district = PCRTestsByDistrict.Find(new PCRTestByDistrict(districtID));
            if (district == null)
                yield break;

            var (minValueDate, maxValueDate) = GetPositiveDaysFromDate(atDate, daysOfPositiveness);

            foreach (var person in GetAllSickPersonsFromTests(district.PCRTestsByValueReverse.InOrder().Select(x => x.Test), (minValueDate, maxValueDate)))
            {
                yield return person;
            }
        }

        //12 - Find all sick persons for region at some date (person is sick for x days from positive test)
        public IEnumerable<Person> FindAllSickPersonsForRegionAtDate(int regionID, DateOnly atDate, int daysOfPositiveness)
        {
            var region = PCRTestsByRegion.Find(new PCRTestByRegion(regionID));
            if (region == null)
                yield break;

            var (minValueDate, maxValueDate) = GetPositiveDaysFromDate(atDate, daysOfPositiveness);

            var testsAtDate = GetTestsInPositiveDaysRange(region.PCRTestsByDateAndTime, minValueDate, maxValueDate);

            foreach (var person in GetAllSickPersonsFromTests(testsAtDate))
            {
                yield return person;
            }
        }

        //13 - Find all sick persons at some date (person is sick for x days from positive test)
        public IEnumerable<Person> FindAllSickPersonsAtDate(DateOnly atDate, int daysOfPositiveness)
        {
            var (minValueDate, maxValueDate) = GetPositiveDaysFromDate(atDate, daysOfPositiveness);

            var testsAtDate = GetTestsInPositiveDaysRange(PCRTestsByDateAndTime, minValueDate, maxValueDate);

            foreach (var person in GetAllSickPersonsFromTests(testsAtDate))
            {
                yield return person;
            }
        }

        //14 - Find one sick person at date from every district with highest test value
        public IEnumerable<Person> FindOneSickPersonAtDateFromEveryDistrictWithHighestValue(DateOnly atDate, int daysOfPositiveness)
        {
            var (minValueDate, maxValueDate) = GetPositiveDaysFromDate(atDate, daysOfPositiveness);

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
        public IEnumerable<(int districtID, int sickPeopleCount)> FindAllDistrictsOrderedBySickPeopleCount(DateOnly atDate, int daysOfPositiveness)
        {
            foreach (var terrUnit in FindAllTerrUnitsOrderedBySickPeopleCount(PCRTestsByDistrict.InOrder().Select(x => x.Test), atDate, daysOfPositiveness))
            {
                yield return terrUnit;
            }
        }

        //16 - Find all regions ordered by count of sick people at date
        public IEnumerable<(int regionID, int sickPeopleCount)> FindAllRegionsOrderedBySickPeopleCount(DateOnly atDate, int daysOfPositiveness)
        {
            foreach (var terrUnit in FindAllTerrUnitsOrderedBySickPeopleCount(PCRTestsByRegion.InOrder().Select(x => x.Test), atDate, daysOfPositiveness))
            {
                yield return terrUnit;
            }
        }

        //17 - Find all tests in date interval for certain test station
        public IEnumerable<PCRTest> FindAllTestsInDateIntervalForTestStation(DateTime dateIntervalMin, DateTime dateIntervalMax, int testStationID)
        {
            var testStation = PCRTestsByTestStation.Find(new PCRTestByTestStation(testStationID));
            if (testStation == null) return [];

            return testStation.PCRTestsByDateAndTime.FindInterval
            (
                new PCRTestByDateAndTime(dateIntervalMin, int.MinValue), new PCRTestByDateAndTime(dateIntervalMax, int.MaxValue)
            ).Select(x => x.Test);
        }

        //18 - Find test by testID
        public PCRTest? FindTestByTestId(int testId)
        {
            return PCRTestsByID.Find(new PCRTestByID(testId))?.Test;
        }

        //19 - Insert new person to system
        public void InsertNewPerson(Person personToInsert)
        {
            PersonsByID.Insert(new PersonByID(personToInsert));
        }

        //20 - Delete PCR test from system
        public bool DeletePCRTestById(int testId)
        {
            var testToDeleteById = PCRTestsByID.Find(new PCRTestByID(testId));
            if (testToDeleteById == null) return false;

            var testToDelete = testToDeleteById.Test;

            //PCRTestsByID
            PCRTestsByID.Delete(new PCRTestByID(testToDelete));

            //PersonsByID -> PCRTestsByID
            var person = PersonsByID.Find(new PersonByID(testToDelete.PersonID));
            person?.PCRTestsByID.Delete(new PCRTestByID(testToDelete));

            //PersonsByID -> PCRTestsByDateAndTime
            person?.PCRTestsByDateAndTime.Delete(new PCRTestByDateAndTime(testToDelete.DateAndTime, testToDelete.TestID));

            //PCRTestsByDistrict
            PCRTestsByDistrict.Delete(new PCRTestByDistrict(testToDelete.DistrictID));

            //PCRTestsByDistrict -> PCRTestsByDateAndTime
            var testWithDistrictId = PCRTestsByDistrict.Find(new PCRTestByDistrict(testToDelete.DistrictID));
            testWithDistrictId?.PCRTestsByDateAndTime.Delete(new PCRTestByDateAndTime(testToDelete));

            //PCRTestsByDistrict -> PCRTestsByValue
            testWithDistrictId?.PCRTestsByValueReverse.Delete(new PCRTestByValueReverse(testToDelete));

            //PCRTestsByRegion
            PCRTestsByRegion.Delete(new PCRTestByRegion(testToDelete));

            //PCRTestsByRegion -> PCRTestsByDateAndTime
            var testWithRegionId = PCRTestsByRegion.Find(new PCRTestByRegion(testToDelete.DistrictID));
            testWithRegionId?.PCRTestsByDateAndTime.Delete(new PCRTestByDateAndTime(testToDelete));

            //PCRTestsByDateAndTime
            PCRTestsByDateAndTime.Delete(new PCRTestByDateAndTime(testToDelete));

            //PCRTestsByTestStation
            PCRTestsByTestStation.Delete(new PCRTestByTestStation(testToDelete));

            //PCRTestsByTestStation -> PCRTestsByDateAndTime
            var testWithTestStation = PCRTestsByTestStation.Find(new PCRTestByTestStation(testToDelete.TestStationID));
            testWithTestStation?.PCRTestsByDateAndTime.Delete(new PCRTestByDateAndTime(testToDelete));

            return true;
        }

        //21 - Delete person from system
        public bool DeletePersonById(string personID)
        {
            var person = PersonsByID.Find(new PersonByID(personID));
            if (person == null) return false;

            var personTests = person.PCRTestsByID.InOrder();

            foreach (var test in personTests)
            {
                var region = PCRTestsByRegion.Find(new PCRTestByRegion(test.Test));
                region?.PCRTestsByDateAndTime.Delete(new PCRTestByDateAndTime(test.Test));

                var district = PCRTestsByDistrict.Find(new PCRTestByDistrict(test.Test));
                district?.PCRTestsByDateAndTime.Delete(new PCRTestByDateAndTime(test.Test));
                district?.PCRTestsByValueReverse.Delete(new PCRTestByValueReverse(test.Test));

                PCRTestsByDateAndTime.Delete(new PCRTestByDateAndTime(test.Test));

                var testStation = PCRTestsByTestStation.Find(new PCRTestByTestStation(test.Test));
                testStation?.PCRTestsByDateAndTime.Delete(new PCRTestByDateAndTime(test.Test));

                PCRTestsByID.Delete(new PCRTestByID(test.Test));
            }

            PersonsByID.Delete(new PersonByID(person.Person));

            return true;
        }


        //Private methods ------------------------------------
        private static (DateTime, DateTime) GetPositiveDaysFromDate(DateOnly atDate, int daysOfPositiveness)
        {
            var minValueDate = atDate.AddDays(-daysOfPositiveness).ToDateTime(TimeOnly.MinValue);
            var maxValueDate = atDate.ToDateTime(TimeOnly.MinValue);
            return (minValueDate, maxValueDate);
        }

        private static IEnumerable<PCRTest> GetTestsInPositiveDaysRange(AVLT<PCRTestByDateAndTime> avltOfTestsByDateAndTime, DateTime minValueDate, DateTime maxValueDate)
        {
            return avltOfTestsByDateAndTime.FindInterval(
                new PCRTestByDateAndTime(minValueDate, int.MinValue), new PCRTestByDateAndTime(maxValueDate, int.MaxValue)
            ).Select(x => x.Test);
        }

        //Return all sick persons (distinct) from given tests + if given datesValues, it return sick persons from tests only in given date range
        private IEnumerable<Person> GetAllSickPersonsFromTests(IEnumerable<PCRTest> tests, (DateTime minValueDate, DateTime maxValueDate)? datesValues = null)
        {
            var persons = new AVLT<PersonByID>();

            foreach (var test in tests)
            {
                if (datesValues != null)
                {
                    if (test.DateAndTime < datesValues.Value.minValueDate || test.DateAndTime > datesValues.Value.maxValueDate)
                        continue;
                }

                if (!test.Result)
                    continue;

                var personById = new PersonByID(test.PersonID);
                //if found, person was already returned
                if (persons.Find(personById) != null) continue;

                persons.Insert(personById);

                var returningPerson = PersonsByID.Find(personById);
                if (returningPerson != null)
                    yield return returningPerson.Person;
            }
        }

        private IEnumerable<(int terrUnitID, int sickPeopleCount)> FindAllTerrUnitsOrderedBySickPeopleCount(IEnumerable<PCRTest> pcrTestsByTerrUnit, 
            DateOnly atDate, int daysOfPositiveness)
        {
            var avltTerrUnitsByCount = new AVLT<TerrUnitBySickPeopleCount>();
            foreach (var terrUnit in pcrTestsByTerrUnit)
            {
                var allSickPersonsInDistrictCount = FindAllSickPersonsForDistrictAtDate(terrUnit.DistrictID, atDate, daysOfPositiveness).Count();
                avltTerrUnitsByCount.Insert(new TerrUnitBySickPeopleCount(allSickPersonsInDistrictCount, terrUnit.DistrictID));
            }

            foreach (var district in avltTerrUnitsByCount.InOrder())
            {
                yield return (district.TerrUnitID, district.SickPeopleCount);
            }
        }
    }
}
