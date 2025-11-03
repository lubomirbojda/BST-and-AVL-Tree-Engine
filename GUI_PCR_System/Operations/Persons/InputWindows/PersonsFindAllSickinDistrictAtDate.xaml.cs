using System.Windows;

namespace GUI_PCR_System.Operations.Persons.InputWindows
{
    /// <summary>
    /// Interaction logic for PersonsFindAllSickinDistrictAtDate.xaml
    /// </summary>
    public partial class PersonsFindAllSickinDistrictAtDate : Window
    {
        private readonly PersonsOperations operationsWindow;
        public PersonsFindAllSickinDistrictAtDate(PersonsOperations operationsWindow)
        {
            InitializeComponent();
            this.operationsWindow = operationsWindow;
        }

        private void FindButton_Click(object sender, RoutedEventArgs e)
        {
            var persons = operationsWindow.GuiController.FindAllSickPersonsInDisctrictAtDate(
                DisctrictIDBox.Value ?? default,
                DateOnly.FromDateTime(DatePicker.SelectedDate ?? default),
                PositivenessDaysPicker.Value ?? default,
                OrderByValueCheckBox.IsChecked ?? default
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
