using System.Windows;
using System.Windows.Controls;

namespace GUI_PCR_System.Operations.Persons.InputWindows
{
    /// <summary>
    /// Interaction logic for PersonsFindAllSickInRegionAtDate.xaml
    /// </summary>
    public partial class PersonsFindAllSickInRegionAtDate : Window
    {
        private readonly PersonsOperations operationsWindow;
        public PersonsFindAllSickInRegionAtDate(PersonsOperations operationsWindow)
        {
            InitializeComponent();
            this.operationsWindow = operationsWindow;
        }

        private void FindButton_Click(object sender, RoutedEventArgs e)
        {
            var persons = operationsWindow.GuiController.FindAllSickPersonsInRegionAtDate(
                RegionIDBox.Value ?? default,
                DateOnly.FromDateTime(DatePicker.SelectedDate ?? default),
                PositivenessDaysPicker.Value ?? default
            );
            operationsWindow.SetNewResult(persons);
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
