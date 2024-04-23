// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using System.Linq;
using Enigma.Api;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Domain.Requests;
using Enigma.Domain.Research;
using Enigma.Domain.Responses;
using Enigma.Frontend.Ui.State;

namespace Enigma.Frontend.Ui.Models;

/// <summary>Model for project usage</summary>
public class ProjectUsageModel
{
    private readonly DataVaultResearch _dataVaultResearch;
    private ResearchProject? _currentProject; 
    private readonly AstroConfig _currentAstroConfig;
    private readonly IResearchPerformApi _researchPerformApi;
    public HarmonicDetailsSelection HarmonicDetailsSelection { get; set; }
    public MidpointDetailsSelection MidpointDetailsSelection { get; set; }
    public ResearchPointSelection? CurrenPointSelection { get; set; }

    public ProjectUsageModel(IResearchPerformApi researchPerformApi)
    {
        _dataVaultResearch = DataVaultResearch.Instance;
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
        _currentProject = _dataVaultResearch.CurrentProject;
        MethodResponse? responseCg = null;
        MethodResponse? responseTest = null;
        if (CurrenPointSelection == null || CurrenPointSelection.SelectedPoints.Count <= 0) return;    // prevent processing if user closed window without entering data
        if (_currentProject == null) return;
        switch (researchMethod)
        {
            case ResearchMethods.CountPosInSigns:
            case ResearchMethods.CountPosInHouses:
            case ResearchMethods.CountAspects:
            case ResearchMethods.CountDeclinationParallels:    
            case ResearchMethods.CountUnaspected:
            case ResearchMethods.CountOob:
            {
                bool useControlGroup = false;
                GeneralResearchRequest request = new(_currentProject.Name, researchMethod, useControlGroup, CurrenPointSelection, _currentAstroConfig);
                responseTest = _researchPerformApi.PerformResearch(request);
                useControlGroup = true;
                request = new GeneralResearchRequest(_currentProject.Name, researchMethod, useControlGroup, CurrenPointSelection, _currentAstroConfig);
                responseCg = _researchPerformApi.PerformResearch(request);
                break;
            }
            case ResearchMethods.CountOccupiedMidpoints:
            {
                MidpointDetailsSelection? selection = DataVaultResearch.Instance.CurrenMidpointDetailsSelection;
                if (selection is null) return;
                (int divisionForDial, double orb) = selection;
                bool useControlGroup = false;
                CountOccupiedMidpointsRequest request = new(_currentProject.Name, researchMethod, useControlGroup, CurrenPointSelection, _currentAstroConfig, divisionForDial, orb);
                responseTest = _researchPerformApi.PerformResearch(request);
                useControlGroup = true;
                request = new CountOccupiedMidpointsRequest(_currentProject.Name, researchMethod, useControlGroup, CurrenPointSelection, _currentAstroConfig, divisionForDial, orb);
                responseCg = _researchPerformApi.PerformResearch(request);
                break;
            }
            case ResearchMethods.CountDeclinationMidpoints:
            {
                double orb = 1.0;
                bool useControlGroup = false;
                CountOccupiedMidpointsDeclinationRequest request = new(_currentProject.Name, researchMethod, useControlGroup, CurrenPointSelection, _currentAstroConfig, orb);
                responseTest = _researchPerformApi.PerformResearch(request);
                useControlGroup = true;
                request = new CountOccupiedMidpointsDeclinationRequest(_currentProject.Name, researchMethod, useControlGroup, CurrenPointSelection, _currentAstroConfig, orb);
                responseCg = _researchPerformApi.PerformResearch(request);
                break;
            }
            case ResearchMethods.CountHarmonicConjunctions:
            {
                HarmonicDetailsSelection? selection = DataVaultResearch.Instance.CurrentHarmonicDetailsSelection;
                if (selection is null) return;
                (double harmonicNumber, double orb) = selection;
                bool useControlGroup = false;
                CountHarmonicConjunctionsRequest request = new(_currentProject.Name, researchMethod, useControlGroup, CurrenPointSelection, _currentAstroConfig, harmonicNumber, orb);
                responseTest = _researchPerformApi.PerformResearch(request);
                useControlGroup = true;
                request = new CountHarmonicConjunctionsRequest(_currentProject.Name, researchMethod, useControlGroup, CurrenPointSelection, _currentAstroConfig, harmonicNumber, orb);
                responseCg = _researchPerformApi.PerformResearch(request);                    
                break;
            }
        }

        if (responseTest == null || responseCg == null) return;
        (MethodResponse, MethodResponse) results = (responseTest, responseCg);
        DataVaultResearch.Instance.ResponseTest = results.Item1;
        DataVaultResearch.Instance.ResponseCg = results.Item2;
    }
    


}

public class PresentableMethodDetails
{
    public string MethodName { get; init; } = string.Empty;
}
