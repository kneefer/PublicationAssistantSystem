using System.Windows;
using PublicationAssistantSystem.Core;
using PublicationAssistantSystem.DAL.Context;
using PublicationAssistantSystem.DAL.Models.Publications;
using PublicationAssistantSystem.Core.Mappers.Common;
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
            //test.Run();
            WOSRecordToIRecordConverter converter = new WOSRecordToIRecordConverter();
            var res = Extensions.Deserialize<searchResults>();
            var result = converter.ToIRecord(res);
            System.Console.WriteLine("chuj");
        }
    }
}
