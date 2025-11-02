using System.Windows;
using System.Windows.Controls;

namespace GUI_PCR_System.Operations.Persons
{
    /// <summary>
    /// Interaction logic for PersonsOperations.xaml
    /// </summary>
    public partial class PersonsOperations : Window
    {
        public PersonsOperations()
        {
            InitializeComponent();
        }

        private void ChooseOperation_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button?.ContextMenu != null)
            {
                button.ContextMenu.PlacementTarget = button;
                button.ContextMenu.IsOpen = true;
            }
        }

        private void Operation_FindSickPersonsInDistrictAtDate(object sender, RoutedEventArgs e)
        {

        }

        private void Operation_FindSickPersonsInRegionAtDate(object sender, RoutedEventArgs e)
        {

        }

        private void Operation_FindSickPersonsAtDate(object sender, RoutedEventArgs e)
        {

        }

        private void Operation_FindOneSickPersonWithHighestValueFromEveryDistrictAtDate(object sender, RoutedEventArgs e)
        {

        }

        private void Operation_InsertPerson(object sender, RoutedEventArgs e)
        {

        }

        private void Operation_DeletePerson(object sender, RoutedEventArgs e)
        {

        }
    }
}
