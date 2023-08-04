// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using System.Linq;
using Enigma.Api.Interfaces;
using Enigma.Domain.Configuration;
using Enigma.Domain.Research;
using Enigma.Frontend.Ui.Research;
using Enigma.Frontend.Ui.State;
using Microsoft.Extensions.DependencyInjection;

namespace Enigma.Frontend.Ui.Models;

/// <summary>Model for project usage</summary>
public class ProjectUsageModel
{
    private readonly DataVault _dataVault;
    private ResearchProject? _currentProject;
    private readonly AstroConfig _currentAstroConfig;
    private readonly IResearchPerformApi _researchPerformApi;
    
    public ProjectUsageModel(IResearchPerformApi researchPerformApi)
    {
        _dataVault = DataVault.Instance;
        _researchPerformApi = researchPerformApi;
        _currentAstroConfig = CurrentConfig.Instance.GetConfig();
    }
    
    public List<PresentableMethodDetails> GetAllMethodDetails()
    {
        List<ResearchMethodDetails> methodDetails = ResearchMethods.None.AllDetails();
        return methodDetails.Select(methodDetail => 
            new PresentableMethodDetails() { MethodName = methodDetail.Text }).ToList();
    }
    
    public void PerformRequest(ResearchMethods researchMethod)
    {
        _currentProject = _dataVault.CurrentProject;
        MethodResponse? responseCg = null;
        MethodResponse? responseTest = null;
        ResearchPointsSelection? pointsSelection = _dataVault.CurrentPointsSelection;
        if (pointsSelection == null || pointsSelection.SelectedPoints.Count <= 0) return;    // prevent processing if user closed window without entering data
        if (_currentProject == null) return;
        switch (researchMethod)
        {
            case ResearchMethods.CountPosInSigns:
            case ResearchMethods.CountPosInHouses:
            case ResearchMethods.CountAspects:
            case ResearchMethods.CountUnaspected:
            {
                bool useControlGroup = false;
                GeneralResearchRequest request = new(_currentProject.Name, researchMethod, useControlGroup, pointsSelection, _currentAstroConfig);
                responseTest = _researchPerformApi.PerformResearch(request);
                useControlGroup = true;
                request = new(_currentProject.Name, researchMethod, useControlGroup, pointsSelection, _currentAstroConfig);
                responseCg = _researchPerformApi.PerformResearch(request);
                break;
            }
            case ResearchMethods.CountOccupiedMidpoints:
            {
                MidpointDetailsWindow detailsWindow = App.ServiceProvider.GetRequiredService<MidpointDetailsWindow>();
                detailsWindow.ShowDialog();
                if (detailsWindow.IsCompleted())
                {
                    int divisionForDial = detailsWindow.DialDivision;
                    double orb = detailsWindow.Orb;
                    bool useControlGroup = false;
                    CountOccupiedMidpointsRequest request = new(_currentProject.Name, researchMethod, useControlGroup, pointsSelection, _currentAstroConfig, divisionForDial, orb);
                    responseTest = _researchPerformApi.PerformResearch(request);
                    useControlGroup = true;
                    request = new(_currentProject.Name, researchMethod, useControlGroup, pointsSelection, _currentAstroConfig, divisionForDial, orb);
                    responseCg = _researchPerformApi.PerformResearch(request);
                }

                break;
            }
            case ResearchMethods.CountHarmonicConjunctions:
            {
                HarmonicDetailsWindow detailsWindow = App.ServiceProvider.GetRequiredService<HarmonicDetailsWindow>();
                detailsWindow.ShowDialog();
                if (detailsWindow.IsCompleted())
                {
                    double harmonicNumber = detailsWindow.HarmonicNumber;
                    double orb = detailsWindow.Orb;
                    bool useControlGroup = false;
                    CountHarmonicConjunctionsRequest request = new(_currentProject.Name, researchMethod, useControlGroup, pointsSelection, _currentAstroConfig, harmonicNumber, orb);
                    responseTest = _researchPerformApi.PerformResearch(request);
                    useControlGroup = true;
                    request = new(_currentProject.Name, researchMethod, useControlGroup, pointsSelection, _currentAstroConfig, harmonicNumber, orb);
                    responseCg = _researchPerformApi.PerformResearch(request);
                }

                break;
            }
        }
        if (responseTest != null && responseCg != null)
        {
            (MethodResponse, MethodResponse) results = (responseTest, responseCg);
            DataVault.Instance.ResponseTest = results.Item1;
            DataVault.Instance.ResponseCg = results.Item2;
        }
    }

   
    
}

public class PresentableMethodDetails
{
    public string MethodName { get; set; }
}
