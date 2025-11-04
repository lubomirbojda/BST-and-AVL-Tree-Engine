namespace AUS2_S_1.PCRTestsSystem.Data.Test
{
    using System.Globalization;
    using AUS2_S_1.PCRTestsSystem.Data.Person;
    using AUS2_S_1.PCRTestsSystem.Interfaces;

    public class PCRTestByID : BasePCRTestComparable<PCRTestByID>, ISaveble<PCRTestByID>
    {

        public PCRTestByID() : base(new PCRTest(default, string.Empty, default, default, default, default, default))
        { 
        }
        public PCRTestByID(PCRTest pcrTest, Person person) : base(pcrTest, person)
        {
        }

        public PCRTestByID(int testID) : base(new PCRTest(default, string.Empty, testID, default, default, default, default)) 
        {
        }

        public override int CompareTo(PCRTestByID? other)
        {
            if (other == null)
                throw new ArgumentNullException();

            return Test.TestID.CompareTo(other.Test.TestID);
        }

        public string CreateCSVLine(PCRTestByID data)
        {
            return $"{data.Test.DateAndTime.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)};" +
                   $"{data.Test.PersonID};" +
                   $"{data.Test.TestID};" +
                   $"{data.Test.TestStationID};" +
                   $"{data.Test.DistrictID};" +
                   $"{data.Test.RegionID};" +
                   $"{data.Test.Value.ToString(CultureInfo.InvariantCulture)};" +
                   $"{data.Test.Note?.Replace(';', ',') ?? string.Empty}";
        }

        public PCRTestByID LoadFromCSVLine(string line)
        {
            string[] parts = line.Split(';');
            if (parts.Length != 8)
            {
                throw new FormatException("CSV line needs to have 7 parts!");
            }

            var dateAndTime = DateTime.ParseExact(parts[0], "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None);
            string personId = parts[1];
            int testId = int.Parse(parts[2]);
            int testStationId = int.Parse(parts[3]);
            int districtId = int.Parse(parts[4]);
            int regionId = int.Parse(parts[5]);
            double value = double.Parse(parts[6], CultureInfo.InvariantCulture);
            string? note = parts.Length > 7 ? parts[7] : null;

            if (string.IsNullOrWhiteSpace(note))
            {
                note = null;
            }

            return new PCRTestByID(new PCRTest(dateAndTime, personId, testId, testStationId, districtId, regionId, value, note), Person);
        }
    }
}
