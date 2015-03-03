using System.Windows;
using PublicationAssistantSystem.Core;

namespace PublicationAssistantSystem.UI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var test = new Test();
            test.Run();
        }
    }
}
