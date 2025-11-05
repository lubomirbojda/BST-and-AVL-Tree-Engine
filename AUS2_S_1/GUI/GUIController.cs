namespace AUS2_S_1.GUI
{
    using System;
    using AUS2_S_1.PCRTestsSystem.Data.Person;
    using AUS2_S_1.PCRTestsSystem.Data.Test;
    using PCRTestsSystem;

    public class GUIController
    {
        private readonly PCRTestsSystem systemPCR = new();
        private const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        private string GetRandomString(int stringLenght, Random rand)
        {
            return new string(Enumerable.Repeat(chars, stringLenght).Select(s => s[rand.Next(s.Length)]).ToArray());
        }

        public void GenerateDataForSystem()
        {
            systemPCR.ClearSystem();
            var rand = new Random();
            var personIDS = new List<string>();
            int numberOfPersons = 100000;
            int numberOfTests = 500000;
            int numberOfRegions = 200;
            int numberOfDistricts = 1000 / numberOfRegions;
            int numberOfTestStations = 5000 / numberOfRegions;

            for (int i = 0; i < numberOfPersons; i++)
            {
                var firstName = GetRandomString(5, rand);
                var lastName = GetRandomString(5, rand);
                var personID = GetRandomString(7, rand);
                var birthdate = DateOnly.FromDateTime(DateTime.Now).AddDays(-rand.Next(1, 365)).AddMonths(-rand.Next(1, 12)).AddYears(-rand.Next(18, 50));
                var inserted = InsertPerson(firstName, lastName, birthdate, personID);
                if (inserted)
                    personIDS.Add(personID);
            }

            for (int i = 0; i < numberOfTests; i++)
            {
                double totalSeconds = (DateTime.Now.AddYears(-2) - DateTime.Now).TotalSeconds;
                double randomSeconds = rand.NextDouble() * totalSeconds;
                var testTime = DateTime.Now.AddSeconds(randomSeconds);
                var personID = personIDS[rand.Next(0, personIDS.Count)];
                int regionID = rand.Next(1, numberOfRegions+1);
                int districtID = ((regionID - 1) * numberOfDistricts) + rand.Next(0, numberOfDistricts);
                int stationID = ((regionID - 1) * numberOfTestStations) + rand.Next(0, numberOfTestStations);
                double value = rand.NextDouble() * 10;
                InsertNewTest(testTime, personID, stationID, districtID, regionID, value);
            }
        }

        public void SaveSystem(string directoryPath)
        {
            systemPCR.SaveSystem(directoryPath);
        }

        public void LoadSystem(string directoryPath)
        {
            systemPCR.LoadSystem(directoryPath);
        }

        //01
        public bool InsertNewTest(DateTime testTime, string personID, int stationID, int districtID, int regionID, double testValue, string? note = null)
        {
            var rand = new Random();
            int testID = rand.Next();
            while (systemPCR.IsTestIdInSystem(testID))
            {
                testID = rand.Next();
            }

            return systemPCR.InsertNewPCRTest(new PCRTest(testTime, personID, testID, stationID, districtID, regionID, testValue, note));
        }

        //02
        public (PCRTest?, Person?) FindTestForPerson(string personID, int testID)
        {
            return systemPCR.FindTestByTestIdAndPersonId(testID, personID);
        }

        //03
        public IEnumerable<(PCRTest, Person)> FindAllTestsForPerson(string personID)
        {
            return systemPCR.FindAllTestsForPersonOrderedByDateAndTime(personID);
        }

        //04 - 05
        public IEnumerable<(PCRTest, Person)> FindAllTestsInDistrictInTimeInterval(DateTime timeFrom, DateTime timeTo, int disctrictID, bool onlyPositive)
        {
            if (onlyPositive)
                return systemPCR.FindAllPositiveTestsInDateIntervalForDistrict(timeFrom, timeTo, disctrictID);
            else
                return systemPCR.FindAllTestsInDateIntervalForDistrict(timeFrom, timeTo, disctrictID);
        }

        //06 - 07
        public IEnumerable<(PCRTest, Person)> FindAllTestsInDateIntervalForRegion(DateTime timeFrom, DateTime timeTo, int regionID, bool onlyPositive)
        {
            if (onlyPositive)
                return systemPCR.FindAllPositiveTestsInDateIntervalForRegion(timeFrom, timeTo, regionID);
            else
                return systemPCR.FindAllTestsInDateIntervalForRegion(timeFrom, timeTo, regionID);
        }

        //08 - 09
        public IEnumerable<(PCRTest, Person)> FindAllTestsInDateInterval(DateTime timeFrom, DateTime timeTo, bool onlyPositive)
        {
            if (onlyPositive)
                return systemPCR.FindAllPositiveTestsInDateInterval(timeFrom, timeTo);
            else
                return systemPCR.FindAllTestsInDateInterval(timeFrom, timeTo);
        }


        //10 - 11
        public IEnumerable<Person> FindAllSickPersonsInDisctrictAtDate(int districtID, DateOnly atDate, int daysOfPositiveness, bool sortByValue)
        {
            if (sortByValue)
                return systemPCR.FindAllSickPersonsForDistrictAtDateOrderedByTestValue(districtID, atDate, daysOfPositiveness);
            else
                return systemPCR.FindAllSickPersonsForDistrictAtDate(districtID, atDate, daysOfPositiveness);
        }

        //12
        public IEnumerable<Person> FindAllSickPersonsInRegionAtDate(int regionID, DateOnly atDate, int daysOfPositiveness)
        {
            return systemPCR.FindAllSickPersonsForRegionAtDate(regionID, atDate, daysOfPositiveness);
        }

        //13
        public IEnumerable<Person> FindAllSickPersonsAtDate(DateOnly atDate, int daysOfPositiveness)
        {
            return systemPCR.FindAllSickPersonsAtDate(atDate, daysOfPositiveness);
        }

        //14
        public IEnumerable<Person> FindOneSickPersonFromEveryDistrictWithHighestValue(DateOnly atDate, int daysOfPositiveness)
        {
            return systemPCR.FindOneSickPersonAtDateFromEveryDistrictWithHighestValue(atDate, daysOfPositiveness);
        }

        //15
        public IEnumerable<(int districtID, int sickPeopleCount)> FindAllDistrictsOrderedBySickPeopleCountAtDate(DateOnly atDate, int daysOfPositiveness)
        {
            return systemPCR.FindAllDistrictsOrderedBySickPeopleCount(atDate, daysOfPositiveness);
        }

        //16
        public IEnumerable<(int regionID, int sickPeopleCount)> FindAllRegionsOrderedBySickPeopleCountAtDate(DateOnly atDate, int daysOfPositiveness)
        {
            return systemPCR.FindAllRegionsOrderedBySickPeopleCount(atDate, daysOfPositiveness);
        }

        //17
        public IEnumerable<(PCRTest, Person)> FindAllTestsInTimeIntervalAtStation(DateTime timeFrom, DateTime timeTo, int stationID)
        {
            return systemPCR.FindAllTestsInDateIntervalForTestStation(timeFrom, timeTo.Date, stationID);
        }

        //18
        public (PCRTest?, Person?) FindPCRTestByTestID(int testID)
        {
            return systemPCR.FindTestByTestId(testID);
        }

        //19
        public bool InsertPerson(string firstName, string lastName, DateOnly birthDate, string personID)
        {
            return systemPCR.InsertNewPerson(new Person(firstName, lastName, birthDate, personID));
        }

        //20
        public bool DeleteTestByTestID(int testID)
        {
            return systemPCR.DeletePCRTestById(testID);
        }

        //21
        public bool DeletePersonByPersonID(string personID)
        {
            return systemPCR.DeletePersonById(personID);
        }
    }
}
