using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using AUS2_S_1.GUI;
using GUI_PCR_System.Operations.Districts;
using GUI_PCR_System.Operations.PCRTests;
using GUI_PCR_System.Operations.Persons;
using GUI_PCR_System.Operations.Regions;
using Microsoft.Win32;

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
            PCRTestsOperationsWindow.SetNewResult(null);
        }

        private void ComboBoxPersons_Selected(object sender, RoutedEventArgs e)
        {
            ChangableContent.Content = PersonsOperationsWindow.Content;
            PersonsOperationsWindow.SetNewResult(null);
        }

        private void ComboBoxDistricts_Selected(object sender, RoutedEventArgs e)
        {
            ChangableContent.Content = DistrictsOperationsWindow.Content;
            DistrictsOperationsWindow.SetNewResult(null);
        }

        private void ComboBoxRegions_Selected(object sender, RoutedEventArgs e)
        {
            ChangableContent.Content = RegionsOperationsWindow.Content;
            RegionsOperationsWindow.SetNewResult(null);
        }

        private void Generate_Click(object sender, RoutedEventArgs e)
        {
            guiController.GenerateDataForSystem();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            //var saveFileDialog = new SaveFileDialog
            //{
            //    Filter = "CSV File|*.csv"
            //};
            //var save = saveFileDialog.ShowDialog();

            //if (save == true && saveFileDialog.CheckPathExists)
            //{
            //    //employees.SaveToJson(new FileInfo(saveFileDialog.FileName));
            //}
            guiController.SaveSystem();
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            guiController.LoadSystem();
        }
    }
}