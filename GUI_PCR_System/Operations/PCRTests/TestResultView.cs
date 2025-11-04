using AUS2_S_1.PCRTestsSystem.Data.Person;
using AUS2_S_1.PCRTestsSystem.Data.Test;

namespace GUI_PCR_System.Operations.PCRTests
{
    public class TestResultView
    {
        //test
        public DateTime DateAndTime { get; private set; }
        public int TestID { get; private set; }
        public int TestStationID { get; private set; }
        public int DistrictID { get; private set; }
        public int RegionID { get; private set; }
        public bool Result { get; private set; }
        public double Value { get; private set; }
        public string? Note { get; private set; }

        //person
        public string PersonID { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public DateOnly Birthdate { get; private set; }

        public TestResultView(PCRTest test, Person person)
        {
            DateAndTime = test.DateAndTime;
            TestID = test.TestID;
            TestStationID = test.TestStationID;
            DistrictID = test.DistrictID;
            RegionID = test.RegionID;
            Result = test.Result;
            Value = test.Value;
            Note = test.Note;

            PersonID = person.PersonID;
            FirstName = person.FirstName;
            LastName = person.LastName;
            Birthdate = person.BirthDate;
        }
    }
}
