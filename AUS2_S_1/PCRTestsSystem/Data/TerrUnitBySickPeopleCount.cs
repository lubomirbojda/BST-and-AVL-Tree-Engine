namespace AUS2_S_1.PCRTestsSystem.Data
{
    public class TerrUnitBySickPeopleCount : IComparable<TerrUnitBySickPeopleCount>
    {
        public int SickPeopleCount { get; private set; }
        public int TerrUnitID { get; private set; }

        public TerrUnitBySickPeopleCount(int districtCount, int districtID)
        {
            SickPeopleCount = districtCount;
            TerrUnitID = districtID;
        }

        public int CompareTo(TerrUnitBySickPeopleCount? other)
        {
            if (other == null)
                throw new ArgumentNullException();

            var compareByValue = SickPeopleCount.CompareTo(other.SickPeopleCount);
            if (compareByValue == 0)
            {
                return TerrUnitID.CompareTo(other.TerrUnitID);
            }

            return compareByValue;
        }
    }
}
