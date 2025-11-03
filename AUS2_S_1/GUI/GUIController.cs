namespace AUS2_S_1.GUI
{
    using AUS2_S_1.PCRTestsSystem.Data.Person;
    using AUS2_S_1.PCRTestsSystem.Data.Test;
    using PCRTestsSystem;

    public class GUIController
    {
        private readonly PCRTestsSystem systemPCR = new();

        //01
        public bool InsertNewTest(DateTime testTime, string personID, int stationID, int districtID, int regionID, bool testResult, double testValue, string note)
        {
            var rand = new Random();
            int testID = rand.Next();
            while (systemPCR.IsTestIdInSystem(testID))
            {
                testID = rand.Next();
            }

            return systemPCR.InsertNewPCRTest(new PCRTest(testTime, personID, testID, stationID, districtID, regionID, testResult, testValue, note));
        }

        //02
        public PCRTest? FindTestForPerson(string personID, int testID)
        {
            return systemPCR.FindTestByTestIdAndPersonId(testID, personID);
        }

        //03
        public IEnumerable<PCRTest> FindAllTestsForPerson(string personID)
        {
            return systemPCR.FindAllTestsForPersonOrderedByDateAndTime(personID);
        }

        //04 - 05
        public IEnumerable<PCRTest> FindAllTestsInDistrictInTimeInterval(DateTime timeFrom, DateTime timeTo, int disctrictID, bool onlyPositive)
        {
            if (onlyPositive)
                return systemPCR.FindAllPositiveTestsInDateIntervalForDistrict(timeFrom, timeTo, disctrictID);
            else
                return systemPCR.FindAllTestsInDateIntervalForDistrict(timeFrom, timeTo, disctrictID);
        }

        //06 - 07
        public IEnumerable<PCRTest> FindAllTestsInDateIntervalForRegion(DateTime timeFrom, DateTime timeTo, int regionID, bool onlyPositive)
        {
            if (onlyPositive)
                return systemPCR.FindAllPositiveTestsInDateIntervalForRegion(timeFrom, timeTo, regionID);
            else
                return systemPCR.FindAllTestsInDateIntervalForRegion(timeFrom, timeTo, regionID);
        }

        //08 - 09
        public IEnumerable<PCRTest> FindAllTestsInDateInterval(DateTime timeFrom, DateTime timeTo, bool onlyPositive)
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
        public IEnumerable<PCRTest> FindAllTestsInTimeIntervalAtStation(DateTime timeFrom, DateTime timeTo, int stationID)
        {
            return systemPCR.FindAllTestsInDateIntervalForTestStation(timeFrom, timeTo.Date, stationID);
        }

        //18
        public PCRTest? FindPCRTestByTestID(int testID)
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
