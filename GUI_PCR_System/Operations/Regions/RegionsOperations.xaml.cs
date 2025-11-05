using System.Windows;
using System.Windows.Controls;
using AUS2_S_1.GUI;
using GUI_PCR_System.Operations.Regions.InputWindows;

namespace GUI_PCR_System.Operations.Regions
{
    /// <summary>
    /// Interaction logic for RegionsOperations.xaml
    /// </summary>
    public partial class RegionsOperations : Window
    {
        public GUIController GuiController { get; private set; }
        public RegionsOperations(GUIController controller)
        {
            InitializeComponent();
            GuiController = controller;
        }

        public void SetNewResult(IEnumerable<RegionResultView>? newItemsSource)
        {
            RegionsListView.ItemsSource = newItemsSource;
            CountTextBox.Text = RegionsListView.Items.Count.ToString();
        }

        private void ChooseOperation_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as System.Windows.Controls.Button;
            if (button?.ContextMenu != null)
            {
                button.ContextMenu.PlacementTarget = button;
                button.ContextMenu.IsOpen = true;
            }
        }

        private void Operation_FindAllRegionsOrderedBySickPeopleCountAtDate(object sender, RoutedEventArgs e)
        {
            var findWindow = new RegionsFindAllOrderedBySickPeopleCountAtDate(this);
            findWindow.ShowDialog();
        }
    }
}
