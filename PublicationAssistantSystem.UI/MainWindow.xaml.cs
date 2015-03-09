using System.Windows;
using PublicationAssistantSystem.Core;
using PublicationAssistantSystem.Core.Mappers.WOS;
using PublicationAssistantSystem.Core.Infrastructure;
using PublicationAssistantSystem.Core.WebOfKnowledgeApi.Search;

namespace PublicationAssistantSystem.UI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            //var test = new Test();
            //test.Run();MN
            var converter = new WOSRecordToIRecordConverter();
            var res = Extensions.Deserialize<searchResults>();
            var result = converter.ToIRecord(res);
            System.Console.WriteLine("chuj");
        }
    }
}
