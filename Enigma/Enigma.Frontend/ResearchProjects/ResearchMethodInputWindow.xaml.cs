// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Microsoft.Extensions.DependencyInjection;
using System.Windows;


namespace Enigma.Frontend.Ui.ResearchProjects
{
    /// <summary>Form for inputwindow for researchmethods.</summary>
    public partial class ResearchMethodInputWindow : Window
    {
        private ProjectItem _projectItem;
        private ResearchMethodInputController _controller;

        public ResearchMethodInputWindow()
        {
            InitializeComponent();
            _controller = App.ServiceProvider.GetRequiredService<ResearchMethodInputController>();
        }

        public void PopulateAll(ProjectItem projectItem)
        {
            _projectItem = projectItem;
            lbConfigGeneral.ItemsSource = _controller.GetAllConfigItems();




        }

        private void TestMethodChanged(object sender, RoutedEventArgs e)
        {

        }
    }
}
