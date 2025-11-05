using System.Windows;

namespace GUI_PCR_System.Operations.PCRTests.InputWindows
{
    /// <summary>
    /// Interaction logic for PCRTestDeleteByID.xaml
    /// </summary>
    public partial class PCRTestDeleteByID : Window
    {
        private readonly PCRTestsOperations operationsWindow;
        public PCRTestDeleteByID(PCRTestsOperations operationsWindow)
        {
            InitializeComponent();
            this.operationsWindow = operationsWindow;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var deleteResult = operationsWindow.GuiController.DeleteTestByTestID(TestIDBox.Value ?? default);

            if (deleteResult)
                System.Windows.MessageBox.Show("Test deleted successfully.", "Success", MessageBoxButton.OK);
            else
                System.Windows.MessageBox.Show("Test not deleted.", "Failed", MessageBoxButton.OK);

            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
