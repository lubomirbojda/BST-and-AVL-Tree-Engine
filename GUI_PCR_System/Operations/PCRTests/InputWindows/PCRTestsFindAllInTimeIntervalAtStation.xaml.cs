using System.Windows;

namespace GUI_PCR_System.Operations.PCRTests.InputWindows
{
    /// <summary>
    /// Interaction logic for PCRTestsFindAllInTimeIntervalAtStation.xaml
    /// </summary>
    public partial class PCRTestsFindAllInTimeIntervalAtStation : Window
    {
        private readonly PCRTestsOperations operationsWindow;
        public PCRTestsFindAllInTimeIntervalAtStation(PCRTestsOperations operationsWindow)
        {
            InitializeComponent();
            this.operationsWindow = operationsWindow;
        }

        private void FindButton_Click(object sender, RoutedEventArgs e)
        {
            var timeFrom = DateTimePickerFrom.Value ?? DateTime.MinValue;
            var timeTo = DateTimePickerTo.Value ?? DateTime.MaxValue;
            var operationResult = operationsWindow.GuiController.FindAllTestsInTimeIntervalAtStation(
                timeFrom,
                timeTo,
                StationIDBox.Value ?? default
            );
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
