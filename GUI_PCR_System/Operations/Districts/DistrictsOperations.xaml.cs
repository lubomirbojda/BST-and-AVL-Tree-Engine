using System.Windows;
using System.Windows.Controls;

namespace GUI_PCR_System.Operations.Districts
{
    /// <summary>
    /// Interaction logic for DistrictsOperations.xaml
    /// </summary>
    public partial class DistrictsOperations : Window
    {
        public DistrictsOperations()
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

        private void Operation_FindAllDistrictsOrderedBySickPeopleCountAtDate(object sender, RoutedEventArgs e)
        {

        }
    }
}
