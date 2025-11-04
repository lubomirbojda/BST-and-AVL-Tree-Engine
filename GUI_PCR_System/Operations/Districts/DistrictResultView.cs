namespace GUI_PCR_System.Operations.Districts
{
    public class DistrictResultView
    {
        public int DistrictID { get; private set; }
        public int SickPeopleCount { get; private set; }

        public DistrictResultView(int districtID, int sickPeopleCount   )
        {
            DistrictID = districtID;
            SickPeopleCount = sickPeopleCount;
        }
    }
}
