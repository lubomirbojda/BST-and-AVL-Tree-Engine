namespace GUI_PCR_System.Operations.Regions
{
    public class RegionResultView
    {
        public int RegionID { get; private set; }
        public int SickPeopleCount { get; private set; }

        public RegionResultView(int regionID, int sickPeopleCount) 
        { 
            RegionID = regionID;
            SickPeopleCount = sickPeopleCount;
        }
    }
}
