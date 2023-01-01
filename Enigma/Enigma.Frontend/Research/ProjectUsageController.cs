// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Api.Interfaces;
using Enigma.Domain.AstronCalculations;
using Enigma.Domain.Configuration;
using Enigma.Domain.Enums;
using Enigma.Domain.Points;
using Enigma.Domain.Research;
using Enigma.Frontend.Helpers.Support;
using Enigma.Frontend.Ui.Configuration;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Support;
using Enigma.Research.Domain;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Windows;
using static Enigma.Frontend.Ui.Research.PointSelectController;

namespace Enigma.Frontend.Ui.Research;


public class ProjectUsageController
{
    private AstroConfig _currentAstroConfig;
    private IResearchPerformApi _researchPerformApi;
    private readonly Rosetta _rosetta = Rosetta.Instance;
    private ResearchProject _currentProject;
    private PointSelectWindow _pointSelectWindow;
    private ResearchResultWindow _researchResultWindow;
    private AstroConfigWindow _configWindow;

    public ProjectUsageController(IResearchPerformApi researchPerformApi)
    {
        _currentAstroConfig = CurrentConfig.Instance.GetConfig();
        _researchPerformApi = researchPerformApi;
    }

    public void SetProject(ResearchProject project)
    {
        _currentProject = project;
    }

    public bool IsProjectSelected()
    {
        return _currentProject != null;
    }


    public List<PresentableProjectDetails> GetAllProjectDetails()
    {
        List<PresentableProjectDetails> details = new();
        if (IsProjectSelected())
        {
            details.Add(new PresentableProjectDetails() { Name = _rosetta.TextForId("projectusagewindow.details.name"), Value = _currentProject.Name });
            details.Add(new PresentableProjectDetails() { Name = _rosetta.TextForId("projectusagewindow.details.description"), Value = _currentProject.Description });
            details.Add(new PresentableProjectDetails() { Name = _rosetta.TextForId("projectusagewindow.details.date"), Value = _currentProject.CreationDate });
            details.Add(new PresentableProjectDetails() { Name = _rosetta.TextForId("projectusagewindow.details.dataname"), Value = _currentProject.DataName });
            details.Add(new PresentableProjectDetails() { Name = _rosetta.TextForId("projectusagewindow.details.controlgrouptype"), Value = _rosetta.TextForId(_currentProject.ControlGroupType.GetDetails().TextId) });
            details.Add(new PresentableProjectDetails() { Name = _rosetta.TextForId("projectusagewindow.details.multiplication"), Value = _currentProject.ControlGroupMultiplication.ToString() });
        }
        return details;
    }

    public List<PresentableMethodDetails> GetAllMethodDetails()
    {
        List<PresentableMethodDetails> details = new();
        List<ResearchMethodDetails> methodDetails = ResearchMethods.None.AllDetails();
        foreach (var currentMethodDetails in methodDetails)
        {
            details.Add(new PresentableMethodDetails() { MethodName = _rosetta.TextForId(currentMethodDetails.TextId) });
        }
        return details;
    }



    public class PresentableProjectDetails
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class PresentableMethodDetails
    {
        public string MethodName { get; set; }
    }


    public void PerformRequest(ResearchMethods researchMethod)
    {
        int minimalNrOfPoints = 0;
        _pointSelectWindow = App.ServiceProvider.GetRequiredService<PointSelectWindow>();

        switch (researchMethod)
        {
            case ResearchMethods.None:
                break;
            case ResearchMethods.CountPosInSigns:
                minimalNrOfPoints = 1;
                break;
            case ResearchMethods.CountPosInHouses:
                minimalNrOfPoints = 1;
                break;
            case ResearchMethods.CountAspects:
                minimalNrOfPoints = 2;
                break;
            case ResearchMethods.CountUnaspected:
                minimalNrOfPoints = 1;
                break;
            case ResearchMethods.CountOccupiedMidpoints:
                minimalNrOfPoints = 3;
                break;
            case ResearchMethods.CountHarmonicConjunctions:
                minimalNrOfPoints = 2;
                break;
            default:
                minimalNrOfPoints = 1;
                break;
        }
        _pointSelectWindow.SetMinimalNrOfPoints(minimalNrOfPoints);
        _pointSelectWindow.SetResearchMethod(researchMethod);
        _pointSelectWindow.ShowDialog();

        if (_pointSelectWindow.IsCompleted())
        {
            List<SelectableCelPointDetails> selectedCelPoints = _pointSelectWindow.SelectedCelPoints;
            List<SelectableMundanePointDetails> selectedMundanePoints = _pointSelectWindow.SelectedMundanePoints;
            bool selectedUseCusps = false;
            List<CelPoints> celPoints = new();
            List<MundanePoints> mundanePoints = new();
            foreach (var point in selectedCelPoints)
            {
                celPoints.Add(point.CelPoint);
            }
            if (researchMethod != ResearchMethods.CountPosInHouses)
            {
                foreach (var point in selectedMundanePoints)
                {
                    mundanePoints.Add(point.MundanePoint);
                }
                selectedUseCusps = _pointSelectWindow.SelectedUseCusps;
            }
            ResearchPointsSelection pointsSelection = new(celPoints, mundanePoints, selectedUseCusps);

            bool useControlGroup = false;
            GeneralCountRequest request = new(_currentProject.Name, researchMethod, useControlGroup, pointsSelection, _currentAstroConfig);

            // fire request
            CountOfPartsResponse responseTest = _researchPerformApi.PerformTest(request);

            useControlGroup = true;
            request = new(_currentProject.Name, researchMethod, useControlGroup, pointsSelection, _currentAstroConfig);
            CountOfPartsResponse responseCg = _researchPerformApi.PerformTest(request);

            _researchResultWindow = App.ServiceProvider.GetRequiredService<ResearchResultWindow>();
            _researchResultWindow.SetResults(responseTest, responseCg);
            _researchResultWindow.ShowDialog();
        }    
    }

    public void ShowConfig()
    {
        _configWindow = App.ServiceProvider.GetRequiredService<AstroConfigWindow>();
        _configWindow.ShowDialog();
        _currentAstroConfig = CurrentConfig.Instance.GetConfig();
    }

    public void ShowHelp()
    {
        HelpWindow helpWindow = App.ServiceProvider.GetRequiredService<HelpWindow>();
        helpWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        helpWindow.SetHelpPage("ProjectOverview");
        helpWindow.ShowDialog();
    }
}

