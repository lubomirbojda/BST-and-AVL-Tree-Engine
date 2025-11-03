using System.Runtime.CompilerServices;
using System.Windows;
using AUS2_S_1.GUI;
using GUI_PCR_System.Operations.Districts;
using GUI_PCR_System.Operations.PCRTests;
using GUI_PCR_System.Operations.Persons;
using GUI_PCR_System.Operations.Regions;

namespace GUI_PCR_System
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly GUIController guiController = new();
        private readonly PCRTestsOperations PCRTestsOperationsWindow = new(guiController);
        private readonly PersonsOperations PersonsOperationsWindow = new(guiController);
        private readonly DistrictsOperations DistrictsOperationsWindow = new(guiController);
        private readonly RegionsOperations RegionsOperationsWindow = new(guiController);

        public MainWindow()
        {
            InitializeComponent();
            PCRTestsInComboBox.IsSelected = true;
        }

        private void ComboBoxPCRTests_Selected(object sender, RoutedEventArgs e)
        {
            ChangableContent.Content = PCRTestsOperationsWindow.Content;
        }

        private void ComboBoxPersons_Selected(object sender, RoutedEventArgs e)
        {
            ChangableContent.Content = PersonsOperationsWindow.Content;
        }

        private void ComboBoxDistricts_Selected(object sender, RoutedEventArgs e)
        {
            ChangableContent.Content = DistrictsOperationsWindow.Content;
        }

        private void ComboBoxRegions_Selected(object sender, RoutedEventArgs e)
        {
            ChangableContent.Content = RegionsOperationsWindow.Content;
        }
    }
}