// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Api.Interfaces;
using Enigma.Domain.AstronCalculations;
using Enigma.Domain.Configuration;
using Enigma.Domain.Enums;
using Enigma.Domain.Research;
using Enigma.Frontend.Helpers.Support;
using Enigma.Frontend.Ui.Settings;
using Enigma.Frontend.Ui.State;
using Enigma.Research.Domain;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
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
        _pointSelectWindow = App.ServiceProvider.GetRequiredService<PointSelectWindow>();
        _pointSelectWindow.ShowDialog();
        List<SelectableCelPointDetails> selectedCelPoints = _pointSelectWindow.SelectedCelPoints;
        List<SelectableMundanePointDetails> selectedMundanePoints = _pointSelectWindow.SelectedMundanePoints;
        bool selectedUseCusps = _pointSelectWindow.SelectedUseCusps;
        List<CelPoints> celPoints = new();
        foreach (var point in selectedCelPoints)
        {
            celPoints.Add(point.CelPoint);
        }
        List<MundanePoints> mundanePoints = new();
        foreach (var point in selectedMundanePoints)
        {
            mundanePoints.Add(point.MundanePoint);
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

    public void ShowConfig()
    {
        _configWindow = App.ServiceProvider.GetRequiredService<AstroConfigWindow>();
        _configWindow.ShowDialog();
        _currentAstroConfig = CurrentConfig.Instance.GetConfig();
    }

}

