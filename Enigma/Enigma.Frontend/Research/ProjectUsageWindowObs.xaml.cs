// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Research;
using Enigma.Frontend.Helpers.Support;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Windows;
using static Enigma.Frontend.Ui.Research.ProjectUsageControllerObs;

namespace Enigma.Frontend.Ui.Research
{
    /// <summary>Form for inputwindow for researchmethods.</summary>
    public partial class ProjectUsageWindowObs : Window
    {
        private readonly ProjectUsageControllerObs _controllerObs;

        public ProjectUsageWindowObs()
        {
            InitializeComponent();
            _controllerObs = App.ServiceProvider.GetRequiredService<ProjectUsageControllerObs>();
            PopulateTexts();
        }

        public void SetProject(ResearchProject project)
        {
            _controllerObs.SetProject(project);
            PopulateData(project);
        }



        private void PopulateTexts()
        {
            Title = Rosetta.TextForId("projectusagewindow.title");
            tbFormTitle.Text = Rosetta.TextForId("projectusagewindow.formtitle");
            tbProjDetails.Text = Rosetta.TextForId("projectusagewindow.projdetails");
            tbExistingMethods.Text = Rosetta.TextForId("projectusagewindow.existingmethods");

            tbProjName.Text = Rosetta.TextForId("projectusagewindow.details.name");
            tbProjDescr.Text = Rosetta.TextForId("projectusagewindow.details.description");
            tbProjDate.Text = Rosetta.TextForId("projectusagewindow.details.date");
            tbProjDataSet.Text = Rosetta.TextForId("projectusagewindow.details.dataname");
            tbProjControlGroupType.Text = Rosetta.TextForId("projectusagewindow.details.controlgrouptype");
            tbProjControlGroupMult.Text = Rosetta.TextForId("projectusagewindow.details.multiplication");

            btnConfig.Content = Rosetta.TextForId("projectusagewindow.btnconfig");
            btnPerformTest.Content = Rosetta.TextForId("projectusagewindow.btnperformtest");
            btnHelp.Content = Rosetta.TextForId("common.btnhelp");
            btnClose.Content = Rosetta.TextForId("common.btnclose");
        }

        private void PopulateData(ResearchProject project)
        {
            tbProjNameValue.Text = project.Name;
            tbProjDescrValue.Text = project.Description;
            tbProjDateValue.Text = project.CreationDate.ToString();
            tbProjDataSetValue.Text = project.DataName;
            tbProjControlGroupTypeValue.Text = project.ControlGroupType.ToString();
            tbProjControlGroupMultValue.Text = project.ControlGroupMultiplication.ToString();

            List<PresentableMethodDetails> methodDetails = _controllerObs.GetAllMethodDetails();
            lbExistingMethods.ItemsSource = methodDetails;
        }


        private void PerformTest(object sender, RoutedEventArgs e)
        {
            if (lbExistingMethods.SelectedItems.Count == 1)
            {
                int index = lbExistingMethods.SelectedIndex;
                ResearchMethods researchMethod = ResearchMethods.None.ResearchMethodForIndex(index);
                _controllerObs.PerformRequest(researchMethod);
            }
        }

        private void ShowConfig(object sender, RoutedEventArgs e)
        {
            _controllerObs.ShowConfig();
        }

        private void CloseClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void HelpClick(object sender, RoutedEventArgs e)
        {
            ProjectUsageControllerObs.ShowHelp(); ;
        }
    }
}
