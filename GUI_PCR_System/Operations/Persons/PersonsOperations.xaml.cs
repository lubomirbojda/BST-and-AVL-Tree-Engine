using System.Windows;
using System.Windows.Controls;
using AUS2_S_1.GUI;
using AUS2_S_1.PCRTestsSystem.Data.Person;
using GUI_PCR_System.Operations.Persons.InputWindows;

namespace GUI_PCR_System.Operations.Persons
{
    /// <summary>
    /// Interaction logic for PersonsOperations.xaml
    /// </summary>
    public partial class PersonsOperations : Window
    {
        public GUIController GuiController { get; private set; }

        public PersonsOperations(GUIController controller)
        {
            InitializeComponent();
            GuiController = controller;
        }

        public void SetNewResult(IEnumerable<Person>? newItemsSource)
        {
            PersonsListView.ItemsSource = newItemsSource;
            CountTextBox.Text = PersonsListView.Items.Count.ToString();
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

        private void Operation_FindSickPersonsInDistrictAtDate(object sender, RoutedEventArgs e)
        {
            var findWindow = new PersonsFindAllSickinDistrictAtDate(this);
            findWindow.ShowDialog();
        }

        private void Operation_FindSickPersonsInRegionAtDate(object sender, RoutedEventArgs e)
        {
            var findWindow = new PersonsFindAllSickInRegionAtDate(this);
            findWindow.ShowDialog();
        }

        private void Operation_FindSickPersonsAtDate(object sender, RoutedEventArgs e)
        {
            var findWindow = new PersonsFindAllSickAtDate(this);
            findWindow.ShowDialog();
        }

        private void Operation_FindOneSickPersonWithHighestValueFromEveryDistrictAtDate(object sender, RoutedEventArgs e)
        {
            var findWindow = new PersonsFindOneSickAtDateInEveryDistrict(this);
            findWindow.ShowDialog();
        }

        private void Operation_InsertPerson(object sender, RoutedEventArgs e)
        {
            var insertWindow = new PersonInsertNew(this);
            insertWindow.ShowDialog();
        }

        private void Operation_DeletePerson(object sender, RoutedEventArgs e)
        {
            var deleteWindow = new PersonDeleteByID(this);
            deleteWindow.ShowDialog();
        }
    }
}
