using System.Windows;

namespace GUI_PCR_System.Operations.Districts.InputWindows
{
    /// <summary>
    /// Interaction logic for DistrictsFindAllOrderedBySickPeopleCountAtDate.xaml
    /// </summary>
    public partial class DistrictsFindAllOrderedBySickPeopleCountAtDate : Window
    {
        private readonly DistrictsOperations operationsWindow;
        public DistrictsFindAllOrderedBySickPeopleCountAtDate(DistrictsOperations operationsWindow)
        {
            InitializeComponent();
            this.operationsWindow = operationsWindow;
        }

        private void FindButton_Click(object sender, RoutedEventArgs e)
        {
            var districts = operationsWindow.GuiController.FindAllDistrictsOrderedBySickPeopleCountAtDate(
                DateOnly.FromDateTime(DatePicker.SelectedDate ?? default),
                PositivenessDaysPicker.Value ?? default
            );
            operationsWindow.SetNewResult(districts);
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
