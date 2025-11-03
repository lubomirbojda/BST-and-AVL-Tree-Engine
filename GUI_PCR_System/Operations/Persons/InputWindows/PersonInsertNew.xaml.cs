using System.Windows;

namespace GUI_PCR_System.Operations.Persons.InputWindows
{
    /// <summary>
    /// Interaction logic for PersonInsertNew.xaml
    /// </summary>
    public partial class PersonInsertNew : Window
    {
        private readonly PersonsOperations operationsWindow;
        public PersonInsertNew(PersonsOperations operationsWindow)
        {
            InitializeComponent();
            this.operationsWindow = operationsWindow;
        }

        private void InsertTestButton_Click(object sender, RoutedEventArgs e)
        {
            var insertResult = operationsWindow.GuiController.InsertPerson(
                FirstNameTextBox.Text,
                LastNameTextBox.Text,
                DateOnly.FromDateTime(DatePicker.SelectedDate ?? default),
                PersonIDTextBox.Text
            );

            if (insertResult)
                MessageBox.Show("Person inserted successfully.", "Success", MessageBoxButton.OK);
            else
                MessageBox.Show("Person not inserted.", "Failed", MessageBoxButton.OK);

            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
