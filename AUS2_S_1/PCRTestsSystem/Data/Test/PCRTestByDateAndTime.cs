
namespace AUS2_S_1.PCRTestsSystem.Data.Test
{
    using AUS2_S_1.PCRTestsSystem.Data.Person;
    public class PCRTestByDateAndTime : BasePCRTestComparable<PCRTestByDateAndTime>
    {
        public PCRTestByDateAndTime(PCRTest pcrTest, Person person) : base(pcrTest, person)
        {
        }

        public PCRTestByDateAndTime(DateTime dateTime, int testID) : base(new PCRTest(dateTime, string.Empty, testID, default, default, default, default))
        {
        }

        public override int CompareTo(PCRTestByDateAndTime? other)
        {
            if (other == null)
                throw new ArgumentNullException();

            var compareByDateAndTime = Test.DateAndTime.CompareTo(other.Test.DateAndTime);
            if (compareByDateAndTime == 0)
            {
                return Test.TestID.CompareTo(other.Test.TestID);
            }

            return compareByDateAndTime;
        }
    }
}
