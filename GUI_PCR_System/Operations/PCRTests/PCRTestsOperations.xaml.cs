using System.Windows;
using System.Windows.Controls;
namespace GUI_PCR_System.Operations.PCRTests
{
    /// <summary>
    /// Interaction logic for PCRTestsOperations.xaml
    /// </summary>
    public partial class PCRTestsOperations : Window
    {
        public PCRTestsOperations()
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

        private void Operation_InsertNewTest(object sender, RoutedEventArgs e)
        {
        }

        private void Operation_FindTestForPerson(object sender, RoutedEventArgs e)
        {

        }

        private void Operation_FindAllTestsForPerson(object sender, RoutedEventArgs e)
        {
        }

        private void Operation_FindAllTestsInDistrictInTimeInterval(object sender, RoutedEventArgs e)
        {

        }

        private void Operation_FindAllTestsInRegionInTimeInterval(object sender, RoutedEventArgs e)
        {

        }

        private void Operation_FindAllTestsInTimeInterval(object sender, RoutedEventArgs e)
        {

        }

        private void Operation_FindAllTestsInTimeIntervalAtStation(object sender, RoutedEventArgs e)
        {

        }

        private void Operation_FindTestByID(object sender, RoutedEventArgs e)
        {

        }

        private void Operation_DeleteTest(object sender, RoutedEventArgs e)
        {

        }
    }
}
