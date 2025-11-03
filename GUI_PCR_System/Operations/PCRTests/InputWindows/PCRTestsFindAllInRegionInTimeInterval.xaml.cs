using System.Windows;

namespace GUI_PCR_System.Operations.PCRTests.InputWindows
{
    /// <summary>
    /// Interaction logic for PCRTestsFindAllInRegionInTimeInterval.xaml
    /// </summary>
    public partial class PCRTestsFindAllInRegionInTimeInterval : Window
    {
        private readonly PCRTestsOperations operationsWindow;
        public PCRTestsFindAllInRegionInTimeInterval(PCRTestsOperations operationsWindow)
        {
            InitializeComponent();
            this.operationsWindow = operationsWindow;
        }

        private void FindButton_Click(object sender, RoutedEventArgs e)
        {
            var timeFrom = DateTimePickerFrom.Value ?? DateTime.MinValue;
            var timeTo = DateTimePickerTo.Value ?? DateTime.MaxValue;
            var result = operationsWindow.GuiController.FindAllTestsInDateIntervalForRegion(
                timeFrom,
                timeTo,
                RegionIDBox.Value ?? default,
                OnlyPositiveCheckBox.IsChecked ?? default);
            operationsWindow.SetNewResult(result);
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
