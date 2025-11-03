using System.Windows;
using System.Windows.Controls;

namespace GUI_PCR_System.Operations.Persons.InputWindows
{
    /// <summary>
    /// Interaction logic for PersonDeleteByID.xaml
    /// </summary>
    public partial class PersonDeleteByID : Window
    {
        private readonly PersonsOperations operationsWindow;
        public PersonDeleteByID(PersonsOperations operationsWindow)
        {
            InitializeComponent();
            this.operationsWindow = operationsWindow;
        }

        private void DeleteTestButton_Click(object sender, RoutedEventArgs e)
        {
            var deleteResult = operationsWindow.GuiController.DeletePersonByPersonID(
                PersonIDBox.Text
            );

            if (deleteResult)
                MessageBox.Show("Person deleted successfully.", "Success", MessageBoxButton.OK);
            else
                MessageBox.Show("Person not deleted.", "Failed", MessageBoxButton.OK);

            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
