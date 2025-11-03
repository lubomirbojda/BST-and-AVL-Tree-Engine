using System.Windows;

namespace GUI_PCR_System.Operations.PCRTests.InputWindows
{
    /// <summary>
    /// Interaction logic for PCRTestInsert.xaml
    /// </summary>
    public partial class PCRTestInsert : Window
    {
        private readonly PCRTestsOperations operationsWindow;
        public PCRTestInsert(PCRTestsOperations operationsWindow)
        {
            InitializeComponent();
            this.operationsWindow = operationsWindow;
        }

        private void InsertTestButton_Click(object sender, RoutedEventArgs e)
        {
            var insertResult = operationsWindow.GuiController.InsertNewTest(
                DateTimePicker.Value ?? default,
                PersonIDTextBox.Text,
                TestStationIDBox.Value ?? default,
                DisctrictIDBox.Value ?? default,
                RegionIDBox.Value ?? default,
                ValueBox.Value ?? default,
                NoteTextBox.Text
                );

            if (insertResult)
                MessageBox.Show("Test inserted successfully.", "Success", MessageBoxButton.OK);
            else
                MessageBox.Show("Test not inserted.", "Failed", MessageBoxButton.OK);

            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
