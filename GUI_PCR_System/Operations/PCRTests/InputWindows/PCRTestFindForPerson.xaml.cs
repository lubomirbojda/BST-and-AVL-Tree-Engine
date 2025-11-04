using System.Windows;
using AUS2_S_1.PCRTestsSystem.Data.Person;
using AUS2_S_1.PCRTestsSystem.Data.Test;

namespace GUI_PCR_System.Operations.PCRTests.InputWindows
{
    /// <summary>
    /// Interaction logic for PCRTestFindForPerson.xaml
    /// </summary>
    public partial class PCRTestFindForPerson : Window
    {
        private readonly PCRTestsOperations operationsWindow;
        public PCRTestFindForPerson(PCRTestsOperations operationsWindow)
        {
            InitializeComponent();
            this.operationsWindow = operationsWindow;
        }

        private void FindButton_Click(object sender, RoutedEventArgs e)
        {
            var test = operationsWindow.GuiController.FindTestForPerson(PersonIDBox.Text, TestIDBox.Value ?? default);
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
