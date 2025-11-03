using System.Windows;
using AUS2_S_1.PCRTestsSystem.Data.Test;

namespace GUI_PCR_System.Operations.PCRTests.InputWindows
{
    /// <summary>
    /// Interaction logic for PCRTestsFindAllInDistrictInTimeInterval.xaml
    /// </summary>
    public partial class PCRTestsFindAllInDistrictInTimeInterval : Window
    {
        private readonly PCRTestsOperations operationsWindow;
        public PCRTestsFindAllInDistrictInTimeInterval(PCRTestsOperations operationsWindow)
        {
            InitializeComponent();
            this.operationsWindow = operationsWindow;
        }

        private void FindButton_Click(object sender, RoutedEventArgs e)
        {
            var timeFrom = DateTimePickerFrom.Value ?? DateTime.MinValue;
            var timeTo = DateTimePickerTo.Value ?? DateTime.MaxValue;
            var result = operationsWindow.GuiController.FindAllTestsInDistrictInTimeInterval(
                timeFrom, 
                timeTo,
                DisctrictIDBox.Value ?? default,
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
