﻿// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Api.Interfaces;
using Enigma.Domain.Configuration;
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
    private readonly IResearchPerformApi _researchPerformApi;
    private ResearchProject? _currentProject;
    //private readonly PointSelectWindow pointSelectWindow = App.ServiceProvider.GetRequiredService<PointSelectWindow>();
    //private readonly ResearchResultWindow researchResultWindow = App.ServiceProvider.GetRequiredService<ResearchResultWindow>();
    //private readonly AstroConfigWindow configWindow = App.ServiceProvider.GetRequiredService<AstroConfigWindow>();
   // private readonly HelpWindow helpWindow = App.ServiceProvider.GetRequiredService<HelpWindow>();

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
        if (IsProjectSelected() && _currentProject != null)
        {
            details.Add(new PresentableProjectDetails() { Name = Rosetta.TextForId("projectusagewindow.details.name"), Value = _currentProject.Name });
            details.Add(new PresentableProjectDetails() { Name = Rosetta.TextForId("projectusagewindow.details.description"), Value = _currentProject.Description });
            details.Add(new PresentableProjectDetails() { Name = Rosetta.TextForId("projectusagewindow.details.date"), Value = _currentProject.CreationDate });
            details.Add(new PresentableProjectDetails() { Name = Rosetta.TextForId("projectusagewindow.details.dataname"), Value = _currentProject.DataName });
            details.Add(new PresentableProjectDetails() { Name = Rosetta.TextForId("projectusagewindow.details.controlgrouptype"), Value = Rosetta.TextForId(_currentProject.ControlGroupType.GetDetails().TextId) });
            details.Add(new PresentableProjectDetails() { Name = Rosetta.TextForId("projectusagewindow.details.multiplication"), Value = _currentProject.ControlGroupMultiplication.ToString() });
        }
        return details;
    }

    public List<PresentableMethodDetails> GetAllMethodDetails()
    {
        List<PresentableMethodDetails> details = new();
        List<ResearchMethodDetails> methodDetails = ResearchMethods.None.AllDetails();
        foreach (var currentMethodDetails in methodDetails)
        {
            details.Add(new PresentableMethodDetails() { MethodName = Rosetta.TextForId(currentMethodDetails.TextId) });
        }
        return details;
    }



    public class PresentableProjectDetails
    {
        public string? Name { get; set; }
        public string? Value { get; set; }
    }

    public class PresentableMethodDetails
    {
        public string? MethodName { get; set; }
    }


    public void PerformRequest(ResearchMethods researchMethod)
    {
        int minimalNrOfPoints = 0;
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
        PointSelectWindow pointSelectWindow = App.ServiceProvider.GetRequiredService<PointSelectWindow>();
        pointSelectWindow.SetMinimalNrOfPoints(minimalNrOfPoints);
        pointSelectWindow.SetResearchMethod(researchMethod);
        pointSelectWindow.ShowDialog();

        if (pointSelectWindow.IsCompleted() && _currentProject != null)
        {
            List<SelectableCelPointDetails> selectedCelPoints = pointSelectWindow.SelectedCelPoints;
            List<SelectableMundanePointDetails> selectedMundanePoints = pointSelectWindow.SelectedMundanePoints;
            bool selectedUseCusps = pointSelectWindow.SelectedUseCusps;
            List<ChartPoints> celPoints = new();
            List<ChartPoints> mundanePoints = new();
            foreach (var point in selectedCelPoints)
            {
                celPoints.Add(point.ChartPoint);
            }
            if (researchMethod != ResearchMethods.CountPosInHouses)
            {
                foreach (var point in selectedMundanePoints)
                {
                    mundanePoints.Add(point.MundanePoint);
                }
                selectedUseCusps = pointSelectWindow.SelectedUseCusps;
            }
            ResearchPointsSelection pointsSelection = new(celPoints, mundanePoints, selectedUseCusps);

            if (researchMethod == ResearchMethods.CountPosInHouses || researchMethod == ResearchMethods.CountPosInSigns)
            {
                bool useControlGroup = false;
                GeneralResearchRequest request = new(_currentProject.Name, researchMethod, useControlGroup, pointsSelection, _currentAstroConfig);

                // fire request
                CountOfPartsResponse response = _researchPerformApi.PerformPartsCountTest(request);

                useControlGroup = true;
                request = new(_currentProject.Name, researchMethod, useControlGroup, pointsSelection, _currentAstroConfig);
                CountOfPartsResponse responseCg = _researchPerformApi.PerformPartsCountTest(request);
                ResearchResultWindow researchResultWindow = App.ServiceProvider.GetRequiredService<ResearchResultWindow>();
                researchResultWindow.SetResults(response, responseCg);
                researchResultWindow.ShowDialog();
            }
            else if (researchMethod == ResearchMethods.CountAspects)
            {
                bool useControlGroup = false;
                GeneralResearchRequest request = new(_currentProject.Name, researchMethod, useControlGroup, pointsSelection, _currentAstroConfig);
                CountOfAspectsResponse responseTest = _researchPerformApi.PerformAspectCount(request);
                useControlGroup = true;
                request = new(_currentProject.Name, researchMethod, useControlGroup, pointsSelection, _currentAstroConfig);
                CountOfAspectsResponse responseCg = _researchPerformApi.PerformAspectCount(request);
                ResearchResultWindow researchResultWindow = App.ServiceProvider.GetRequiredService<ResearchResultWindow>();
                researchResultWindow.SetResults(responseTest, responseCg);
                researchResultWindow.ShowDialog();
            }

        }
    }

    public void ShowConfig()
    {
        AstroConfigWindow configWindow = App.ServiceProvider.GetRequiredService<AstroConfigWindow>();
        configWindow.ShowDialog();
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
