// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using System.Linq;
using Enigma.Api.Interfaces;
using Enigma.Domain.Configuration;
using Enigma.Domain.References;
using Enigma.Domain.Research;
using Enigma.Frontend.Ui.State;

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
    
    public static List<PresentableMethodDetails> GetAllMethodDetails()
    {
        List<ResearchMethodDetails> methodDetails = ResearchMethodsExtensions.AllDetails();
        return methodDetails.Select(methodDetail => 
            new PresentableMethodDetails { MethodName = methodDetail.Text }).ToList();
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
                request = new GeneralResearchRequest(_currentProject.Name, researchMethod, useControlGroup, pointsSelection, _currentAstroConfig);
                responseCg = _researchPerformApi.PerformResearch(request);
                break;
            }
            case ResearchMethods.CountOccupiedMidpoints:
            {
                int divisionForDial = _dataVault.ResearchMidpointDialDivision;
                double orb = _dataVault.ResearchMidpointOrb;
                bool useControlGroup = false;
                CountOccupiedMidpointsRequest request = new(_currentProject.Name, researchMethod, useControlGroup, pointsSelection, _currentAstroConfig, divisionForDial, orb);
                responseTest = _researchPerformApi.PerformResearch(request);
                useControlGroup = true;
                request = new CountOccupiedMidpointsRequest(_currentProject.Name, researchMethod, useControlGroup, pointsSelection, _currentAstroConfig, divisionForDial, orb);
                responseCg = _researchPerformApi.PerformResearch(request);
                break;
            }
            case ResearchMethods.CountHarmonicConjunctions:
            {
                double harmonicNumber = _dataVault.ResearchHarmonicValue;
                double orb = _dataVault.ResearchHarmonicOrb;
                bool useControlGroup = false;
                CountHarmonicConjunctionsRequest request = new(_currentProject.Name, researchMethod, useControlGroup, pointsSelection, _currentAstroConfig, harmonicNumber, orb);
                responseTest = _researchPerformApi.PerformResearch(request);
                useControlGroup = true;
                request = new CountHarmonicConjunctionsRequest(_currentProject.Name, researchMethod, useControlGroup, pointsSelection, _currentAstroConfig, harmonicNumber, orb);
                responseCg = _researchPerformApi.PerformResearch(request);
                break;
            }
        }

        if (responseTest == null || responseCg == null) return;
        (MethodResponse, MethodResponse) results = (responseTest, responseCg);
        DataVault.Instance.ResponseTest = results.Item1;
        DataVault.Instance.ResponseCg = results.Item2;
    }

   
    
}

public class PresentableMethodDetails
{
    public string MethodName { get; init; } = string.Empty;
}
