using System.Windows;
using System.Windows.Controls;

namespace GUI_PCR_System.Operations.Persons.InputWindows
{
    /// <summary>
    /// Interaction logic for PersonsFindAllSickAtDate.xaml
    /// </summary>
    public partial class PersonsFindAllSickAtDate : Window
    {
        private readonly PersonsOperations operationsWindow;
        public PersonsFindAllSickAtDate(PersonsOperations operationsWindow)
        {
            InitializeComponent();
            this.operationsWindow = operationsWindow;
        }

        private void FindButton_Click(object sender, RoutedEventArgs e)
        {
            var persons = operationsWindow.GuiController.FindAllSickPersonsAtDate(
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
