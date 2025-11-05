using System.Windows;
using System.Windows.Controls;
using AUS2_S_1.GUI;
using AUS2_S_1.PCRTestsSystem.Data.Person;
using AUS2_S_1.PCRTestsSystem.Data.Test;
using GUI_PCR_System.Operations.PCRTests.InputWindows;
namespace GUI_PCR_System.Operations.PCRTests
{
    /// <summary>
    /// Interaction logic for PCRTestsOperations.xaml
    /// </summary>
    public partial class PCRTestsOperations : Window
    {
        public GUIController GuiController { get; private set; }

        public PCRTestsOperations(GUIController controller)
        {
            InitializeComponent();
            GuiController = controller;
        }

        public void SetNewResult(IEnumerable<TestResultView>? newItemsSource)
        {
            PCRTestsListView.ItemsSource = newItemsSource;
            CountTextBox.Text = PCRTestsListView.Items.Count.ToString();
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

        private void Operation_InsertNewTest(object sender, RoutedEventArgs e)
        {
            var insertWindow = new PCRTestInsert(this);
            insertWindow.ShowDialog();
        }

        private void Operation_FindTestForPerson(object sender, RoutedEventArgs e)
        {
            var findWindow = new PCRTestFindForPerson(this);
            findWindow.ShowDialog();
        }

        private void Operation_FindAllTestsForPerson(object sender, RoutedEventArgs e)
        {
            var findWindow = new PCRTestsFindAllForPerson(this);
            findWindow.ShowDialog();
        }

        private void Operation_FindAllTestsInDistrictInTimeInterval(object sender, RoutedEventArgs e)
        {
            var findWindow = new PCRTestsFindAllInDistrictInTimeInterval(this);
            findWindow.ShowDialog();
        }

        private void Operation_FindAllTestsInRegionInTimeInterval(object sender, RoutedEventArgs e)
        {
            var findWindow = new PCRTestsFindAllInRegionInTimeInterval(this);
            findWindow.ShowDialog();
        }

        private void Operation_FindAllTestsInTimeInterval(object sender, RoutedEventArgs e)
        {
            var findWindow = new PCRTestsFindAllInDateInterval(this);
            findWindow.ShowDialog();
        }

        private void Operation_FindAllTestsInTimeIntervalAtStation(object sender, RoutedEventArgs e)
        {
            var findWindow = new PCRTestsFindAllInTimeIntervalAtStation(this);
            findWindow.ShowDialog();
        }

        private void Operation_FindTestByID(object sender, RoutedEventArgs e)
        {
            var findWindow = new PCRTestsFindTestByID(this);
            findWindow.ShowDialog();
        }

        private void Operation_DeleteTest(object sender, RoutedEventArgs e)
        {
            var deleteWindow = new PCRTestDeleteByID(this);
            deleteWindow.ShowDialog();
        }
    }
}
