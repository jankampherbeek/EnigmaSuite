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
            PopulateData(project);
        }



        private void PopulateTexts()
        {
            Title = _rosetta.TextForId("projectusagewindow.title");
            tbFormTitle.Text = _rosetta.TextForId("projectusagewindow.formtitle");
            tbProjDetails.Text = _rosetta.TextForId("projectusagewindow.projdetails");
            tbExistingMethods.Text = _rosetta.TextForId("projectusagewindow.existingmethods");

            tbProjName.Text = _rosetta.TextForId("projectusagewindow.details.name");
            tbProjDescr.Text = _rosetta.TextForId("projectusagewindow.details.description");
            tbProjDate.Text = _rosetta.TextForId("projectusagewindow.details.date");
            tbProjDataSet.Text = _rosetta.TextForId("projectusagewindow.details.dataname");
            tbProjControlGroupType.Text = _rosetta.TextForId("projectusagewindow.details.controlgrouptype");
            tbProjControlGroupMult.Text = _rosetta.TextForId("projectusagewindow.details.multiplication");

            btnConfig.Content = _rosetta.TextForId("projectusagewindow.btnconfig");
            btnPerformTest.Content = _rosetta.TextForId("projectusagewindow.btnperformtest");
            btnHelp.Content = _rosetta.TextForId("common.btnhelp");
            btnClose.Content = _rosetta.TextForId("common.btnclose");
        }

        private void PopulateData(ResearchProject project)
        {
            tbProjNameValue.Text = project.Name;
            tbProjDescrValue.Text = project.Description;
            tbProjDateValue.Text = project.CreationDate.ToString();
            tbProjDataSetValue.Text = project.DataName;
            tbProjControlGroupTypeValue.Text = project.ControlGroupType.ToString();
            tbProjControlGroupMultValue.Text = project.ControlGroupMultiplication.ToString();

            List<PresentableMethodDetails> methodDetails = _controller.GetAllMethodDetails();
            lbExistingMethods.ItemsSource = methodDetails;
        }


        private void PerformTest(object sender, RoutedEventArgs e)
        {
            PresentableMethodDetails method = lbExistingMethods.SelectedItem as PresentableMethodDetails;
            string methodName = method.MethodName;   // TODO add enum for methods to PresentableMethodDetails and use that to define the method
            ResearchMethods researchMethod = ResearchMethods.CountPosInSigns;   // temporary
            _controller.PerformRequest(researchMethod);
        }

        private void ShowConfig(object sender, RoutedEventArgs e)
        {
            _controller.ShowConfig();
        }

    }
}
