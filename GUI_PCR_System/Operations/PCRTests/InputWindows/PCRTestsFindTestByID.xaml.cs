using System.Windows;
using AUS2_S_1.PCRTestsSystem.Data.Test;

namespace GUI_PCR_System.Operations.PCRTests.InputWindows
{
    /// <summary>
    /// Interaction logic for PCRTestsFindTestByID.xaml
    /// </summary>
    public partial class PCRTestsFindTestByID : Window
    {
        private readonly PCRTestsOperations operationsWindow;
        public PCRTestsFindTestByID(PCRTestsOperations operationsWindow)
        {
            InitializeComponent();
            this.operationsWindow = operationsWindow;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var test = operationsWindow.GuiController.FindPCRTestByTestID(TestIDBox.Value ?? default);
            IEnumerable<PCRTest>? result = test != null ? [test] : null;
            operationsWindow.SetNewResult(result);
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
