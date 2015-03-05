using System.Windows;
using PublicationAssistantSystem.Core;
using PublicationAssistantSystem.DAL.Context;
using PublicationAssistantSystem.DAL.Models.Publications;

namespace PublicationAssistantSystem.UI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            using (var context = new PublicationAssistantContext())
            {
                //context.Database.Delete();
                context.Publications.Add(new Article());
                context.SaveChanges();
            }
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var test = new Test();
            test.Run();
        }
    }
}
