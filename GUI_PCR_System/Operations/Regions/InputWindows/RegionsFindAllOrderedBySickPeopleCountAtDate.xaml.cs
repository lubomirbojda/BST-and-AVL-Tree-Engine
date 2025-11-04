using System.Windows;
using System.Windows.Controls;
using GUI_PCR_System.Operations.Districts;

namespace GUI_PCR_System.Operations.Regions.InputWindows
{
    /// <summary>
    /// Interaction logic for RegionsFindAllOrderedBySickPeopleCountAtDate.xaml
    /// </summary>
    public partial class RegionsFindAllOrderedBySickPeopleCountAtDate : Window
    {
        private readonly RegionsOperations operationsWindow;
        public RegionsFindAllOrderedBySickPeopleCountAtDate(RegionsOperations operationsWindow)
        {
            InitializeComponent();
            this.operationsWindow = operationsWindow;
        }

        private void FindButton_Click(object sender, RoutedEventArgs e)
        {
            var regions = operationsWindow.GuiController.FindAllRegionsOrderedBySickPeopleCountAtDate(
                DateOnly.FromDateTime(DatePicker.SelectedDate ?? default),
                PositivenessDaysPicker.Value ?? default
            );
            IEnumerable<RegionResultView> regs = regions.Select(x => new RegionResultView(x.regionID, x.sickPeopleCount));
            operationsWindow.SetNewResult(regs);
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
