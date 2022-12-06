// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Research;
using Enigma.Frontend.Helpers.Support;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Windows;
using static Enigma.Frontend.Ui.Research.ProjectUsageController;

namespace Enigma.Frontend.Ui.Research
{
    /// <summary>Form for inputwindow for researchmethods.</summary>
    public partial class ProjectUsageWindow : Window
    {
        private Rosetta _rosetta = Rosetta.Instance;
        private ProjectItem _projectItem;
        private ProjectUsageController _controller;

        public ProjectUsageWindow()
        {
            InitializeComponent();
            _controller = App.ServiceProvider.GetRequiredService<ProjectUsageController>();
            PopulateTexts();
        }

        public void SetProject(ResearchProject project)
        {
            _controller.SetProject(project);
            PopulateData();
        }



        private void PopulateTexts()
        {
            Title = _rosetta.TextForId("projectusagewindow.title");
            tbFormTitle.Text = _rosetta.TextForId("projectusagewindow.formtitle");
            tbProjDetails.Text = _rosetta.TextForId("projectusagewindow.projdetails");
            tbExistingMethods.Text = _rosetta.TextForId("projectusagewindow.existingmethods");
            btnConfig.Content = _rosetta.TextForId("projectusagewindow.btnconfig");
            btnTestMethod.Content = _rosetta.TextForId("projectusagewindow.btntestmethod");
            btnMethodDetails.Content = _rosetta.TextForId("projectusagewindow.btnmethoddetails");
            btnPerformTest.Content = _rosetta.TextForId("projectusagewindow.btnperformtest");
            btnHelp.Content = _rosetta.TextForId("common.btnhelp");
            btnClose.Content = _rosetta.TextForId("common.btnclose");
        }

        private void PopulateData()
        {
            List<PresentableProjectDetails> projectDetails = _controller.GetAllProjectDetails();
            lbProjDetails.ItemsSource = projectDetails;
        }

    }
}
