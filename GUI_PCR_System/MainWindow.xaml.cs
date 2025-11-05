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
            Closed += MW_Closed;
            PCRTestsInComboBox.IsSelected = true;
        }

        private void MW_Closed(object? sender, EventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
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
            using var dialog = new FolderBrowserDialog();
            DialogResult result = dialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.SelectedPath))
            {
                try
                {
                    guiController.SaveSystem(dialog.SelectedPath);
                    System.Windows.MessageBox.Show($"Data saved to: {dialog.SelectedPath}",
                                    "Saved successfuly", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show($"Error at saving: {ex.Message}", "Error",
                                    MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            using var dialog = new FolderBrowserDialog();
            DialogResult result = dialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.SelectedPath))
            {
                try
                {
                    // Pošleme vybranú cestu do kontroléra
                    guiController.LoadSystem(dialog.SelectedPath);

                    // Zobrazenie úspechu vo WPF
                    System.Windows.MessageBox.Show($"Data loaded from: {dialog.SelectedPath}",
                                    "Load successful", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show($"Error at loading: {ex.Message}", "Error",
                                    MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}