using System.Windows;
using AUS2_S_1.PCRTestsSystem.Data.Person;
using AUS2_S_1.PCRTestsSystem.Data.Test;
using static System.Net.Mime.MediaTypeNames;

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

        private void FindButton_Click(object sender, RoutedEventArgs e)
        {
            var test = operationsWindow.GuiController.FindPCRTestByTestID(TestIDBox.Value ?? default);
            IEnumerable<(PCRTest, Person)> operationResult = (test.Item1 != null && test.Item2 != null) ? [(test.Item1, test.Item2)] : [];
            IEnumerable<TestResultView> result = operationResult.Select(x => new TestResultView(x.Item1, x.Item2));
            operationsWindow.SetNewResult(result);
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
