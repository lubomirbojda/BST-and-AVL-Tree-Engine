using System.Windows;
using AUS2_S_1.PCRTestsSystem.Data.Test;

namespace GUI_PCR_System.Operations.PCRTests.InputWindows
{
    /// <summary>
    /// Interaction logic for PCRTestsFindAllForPerson.xaml
    /// </summary>
    public partial class PCRTestsFindAllForPerson : Window
    {
        private readonly PCRTestsOperations operationsWindow;
        public PCRTestsFindAllForPerson(PCRTestsOperations operationsWindow)
        {
            InitializeComponent();
            this.operationsWindow = operationsWindow;
        }

        private void FindButton_Click(object sender, RoutedEventArgs e)
        {
            var tests = operationsWindow.GuiController.FindAllTestsForPerson(PersonIDBox.Text);
            operationsWindow.SetNewResult(tests);
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
