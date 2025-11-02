using System.Windows;
using System.Windows.Controls;

namespace GUI_PCR_System.Operations.Regions
{
    /// <summary>
    /// Interaction logic for RegionsOperations.xaml
    /// </summary>
    public partial class RegionsOperations : Window
    {
        public RegionsOperations()
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

        private void Operation_FindAllRegionsOrderedBySickPeopleCountAtDate(object sender, RoutedEventArgs e)
        {

        }
    }
}
