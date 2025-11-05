using System.Windows;
using System.Windows.Controls;
using AUS2_S_1.GUI;
using AUS2_S_1.PCRTestsSystem.Data.Test;
using GUI_PCR_System.Operations.Districts.InputWindows;

namespace GUI_PCR_System.Operations.Districts
{
    /// <summary>
    /// Interaction logic for DistrictsOperations.xaml
    /// </summary>
    public partial class DistrictsOperations : Window
    {
        public GUIController GuiController { get; private set; }
        public DistrictsOperations(GUIController controller)
        {
            InitializeComponent();
            GuiController = controller;
        }

        public void SetNewResult(IEnumerable<DistrictResultView>? newItemsSource)
        {
            DistrictsListView.ItemsSource = newItemsSource;
            CountTextBox.Text = DistrictsListView.Items.Count.ToString();
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

        private void Operation_FindAllDistrictsOrderedBySickPeopleCountAtDate(object sender, RoutedEventArgs e)
        {
            var findWindow = new DistrictsFindAllOrderedBySickPeopleCountAtDate(this);
            findWindow.ShowDialog();
        }
    }
}
