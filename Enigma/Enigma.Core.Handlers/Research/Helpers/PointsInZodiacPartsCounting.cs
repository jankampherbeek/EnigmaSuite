// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers.Interfaces;
using Enigma.Core.Handlers.Research.Interfaces;
using Enigma.Core.Work.Research.Interfaces;
using Enigma.Domain.AstronCalculations;
using Enigma.Domain.Enums;
using Enigma.Domain.Persistency;
using Enigma.Domain.Research;
using Enigma.Research.Domain;
using Newtonsoft.Json;
using Serilog;

namespace Enigma.Core.Handlers.Research.Helpers;

/// <inheritdoc/>
public class PointsInZodiacPartsCounting : IPointsInZodiacPartsCounting
{
    private readonly IResearchPaths _researchPaths;
    private readonly IFilePersistencyHandler _filePersistencyHandler;
    private readonly IResearchDataHandler _researchDataHandler;


    public PointsInZodiacPartsCounting(IResearchPaths researchPaths,
        IFilePersistencyHandler filePersistencyHandler,
        IResearchDataHandler researchDataHandler)
    {
        _researchPaths = researchPaths;
        _filePersistencyHandler = filePersistencyHandler;
        _researchDataHandler = researchDataHandler;
    }

    /// <inheritdoc/>
    public CountOfPartsResponse CountPointsInParts(List<CalculatedResearchChart> charts, GeneralCountRequest request)
    {
        return CountPointsInSign(charts, request);
    }

    private CountOfPartsResponse CountPointsInSign(List<CalculatedResearchChart> charts, GeneralCountRequest request)
    {
        int NrOfCelPoints = request.PointsSelection.SelectedCelPoints.Count;

        List<CountOfParts> allCounts = InitializeCounts(request);
        StandardInput standardInput = ReadInputForCharts(request);
        ResearchPointsSelection pointsSelection = request.PointsSelection;

        foreach (CalculatedResearchChart chart in charts)
        {
            HandleChart(chart, pointsSelection, ref allCounts);
        }

        List<int> totals = CountTotals(allCounts);
        CountOfPartsResponse response = new(request, allCounts, totals);

        string jsonText = JsonConvert.SerializeObject(response, Formatting.Indented);
        string pathForResults = _researchPaths.CountResultsPath(request.ProjectName, ResearchMethods.CountPosInSigns.ToString(), request.UseControlGroup);
        _filePersistencyHandler.WriteFile(pathForResults, jsonText);
        Log.Information("Json with countings written to {path}.", pathForResults);
        return response;
    }


    private int DefineNumberOfParts(GeneralCountRequest request)
    {
        if (request.Method == ResearchMethods.CountPosInSigns)
        {
            return 12;
        }
        else
        {
            string error = "PointsInZodiacPartsCounting: unsupported method in request: " + request.Method.ToString();
            Log.Error(error);
            throw new ArgumentException(error);
        }
    }


    private List<CountOfParts> InitializeCounts(GeneralCountRequest request)
    {
        List<CountOfParts> allCounts = new();
        int nrOfParts = DefineNumberOfParts(request);
        int[] tempCounts = new int[nrOfParts];
        foreach (CelPoints selectedCelPoint in request.PointsSelection.SelectedCelPoints)
        {
            int Id = selectedCelPoint.GetDetails().SeId;
            ResearchPoint researchPoint = new ResearchCelPoint(Id, selectedCelPoint);
            allCounts.Add(new(researchPoint, tempCounts.ToList()));
        }
        foreach (MundanePoints selectedMundanePoint in request.PointsSelection.SelectedMundanePoints)
        {
            int Id = (int)selectedMundanePoint;
            ResearchPoint researchPoint = new ResearchMundanePoint(Id, selectedMundanePoint);
            CountOfParts countOfParts = new(researchPoint, tempCounts.ToList());
            allCounts.Add(new(researchPoint, tempCounts.ToList()));
        }
        if (request.PointsSelection.IncludeCusps)
        {
            int nrOfCusps = request.Config.HouseSystem.GetDetails().NrOfCusps;
            for (int i = 0; i < nrOfCusps; i++)
            {
                int index = i + 1;
                string name = "Cusp " + index;
                ResearchPoint researchPoint = new ResearchCuspPoint(index, name);
                allCounts.Add(new(researchPoint, tempCounts.ToList()));
            }
        }
        return allCounts;
    }

    private StandardInput ReadInputForCharts(GeneralCountRequest request)
    {
        string fullPath = _researchPaths.DataPath(request.ProjectName, request.UseControlGroup);
        string json = _filePersistencyHandler.ReadFile(fullPath);
        return _researchDataHandler.GetStandardInputFromJson(json);
    }

    private void HandleChart(CalculatedResearchChart chart, ResearchPointsSelection pointsSelection, ref List<CountOfParts> allCounts)
    {
        int pointIndex = 0;
        foreach (CelPoints selectedCelPoint in pointsSelection.SelectedCelPoints)
        {
            foreach (FullCelPointPos chartCelPointPos in chart.CelPointPositions)
            {
                if (chartCelPointPos.CelPoint == selectedCelPoint)
                {
                    double longitude = chartCelPointPos.Longitude.Position;
                    int signIndex = (int)longitude / 30;
                    allCounts[pointIndex].Counts[signIndex]++;
                }
            }
            pointIndex++;
        }
        foreach (MundanePoints selectedMundanePoint in pointsSelection.SelectedMundanePoints)
        {
            if (selectedMundanePoint == MundanePoints.Mc)
            {
                allCounts[pointIndex].Counts[SignIndex(chart.FullHousePositions.Mc.Longitude)]++;
            }
            if (selectedMundanePoint == MundanePoints.Ascendant)
            {
                allCounts[pointIndex].Counts[SignIndex(chart.FullHousePositions.Ascendant.Longitude)]++;
            }
            pointIndex++;
        }

        if (pointsSelection.IncludeCusps)
        {
            foreach (var cusp in chart.FullHousePositions.Cusps)
            {
                allCounts[pointIndex++].Counts[SignIndex(cusp.Longitude)]++;
            }
        }
    }

    private int SignIndex(double longitude)
    {
        return (int)longitude / 30;
    }

    private List<int> CountTotals(List<CountOfParts> allCounts)
    {
        List<int> totals = new();
        int nrOfParts = allCounts.Count > 0 ? allCounts[0].Counts.Count : 0;
        for (int i = 0; i < nrOfParts; i++)
        {
            int subTotal = 0;
            foreach (CountOfParts cop in allCounts)
            {
                subTotal += cop.Counts[i];
            }
            totals.Add(subTotal);
        }
        return totals;
    }

}