﻿// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Text.Json;
using Enigma.Core.Handlers;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Domain.Requests;
using Enigma.Domain.Responses;
using Serilog;
using Serilog.Core;

namespace Enigma.Core.Research;

/// <summary>Counting for points in parts of the zodiac (e.g. signs, decanates etc.</summary>
public interface IPointsInPartsCounting
{
    /// <summary>Perform a count for points in parts of the zodiac or in the housesystem.</summary>
    /// <param name="charts">The calculated charts to check.</param>
    /// <param name="request">The original request.</param>
    /// <returns>The calculated counts.</returns>
    public CountOfPartsResponse CountPointsInParts(List<CalculatedResearchChart> charts, GeneralResearchRequest request);
}

/// <inheritdoc/>
public sealed class PointsInPartsCounting : IPointsInPartsCounting
{
    private readonly IResearchPaths _researchPaths;
    private readonly IFilePersistencyHandler _filePersistencyHandler;

    public PointsInPartsCounting(IResearchPaths researchPaths,
        IFilePersistencyHandler filePersistencyHandler)
    {
        _researchPaths = researchPaths;
        _filePersistencyHandler = filePersistencyHandler;
    }

    /// <inheritdoc/>
    public CountOfPartsResponse CountPointsInParts(List<CalculatedResearchChart> charts, GeneralResearchRequest request)
    {
        return PerformCounts(charts, request);
    }

    private CountOfPartsResponse PerformCounts(List<CalculatedResearchChart> charts, GeneralResearchRequest request)
    {
        Log.Information("CGFIX: startint count");
        ResearchMethods researchMethod = request.Method;
        int nrOfParts = DefineNumberOfParts(request);
        List<CountOfParts> allCounts = InitializeCounts(request, nrOfParts);
        Log.Information("CGFIX: initialization of allCounts completed");
        ResearchPointSelection pointSelection = request.PointSelection;

        Log.Information("CGFIX: starting loop for all charts ");
        foreach (CalculatedResearchChart chart in charts)
        {
            HandleChart(researchMethod, chart, pointSelection, nrOfParts, ref allCounts);
        }
        Log.Information(("CGFIX: Loop for all charts completed"));
        List<int> totals = CountTotals(allCounts);
        CountOfPartsResponse response = new(request, allCounts, totals);

        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonText = JsonSerializer.Serialize(response, options);
        string pathForResults = _researchPaths.CountResultsPath(request.ProjectName, researchMethod.ToString(), request.UseControlGroup);
        _filePersistencyHandler.WriteFile(pathForResults, jsonText);
        Log.Information("Json with countings written to {Path}", pathForResults);
        return response;
    }

    private static int DefineNumberOfParts(GeneralResearchRequest request)
    {
        // ReSharper disable once ConvertIfStatementToSwitchStatement
        if (request.Method == ResearchMethods.CountPosInSigns) return 12;
        if (request.Method == ResearchMethods.CountPosInHouses)
            return request.Config.HouseSystem.GetDetails().NrOfCusps;

        Log.Error("PointsInPartsCounting: unsupported method in request: {Method}", request.Method);
        throw new ArgumentException("Unsupported method in GeneralResearchRequest");

    }


    private static List<CountOfParts> InitializeCounts(GeneralResearchRequest request, int nrOfParts)
    {
        int[] tempCounts = new int[nrOfParts];
        return request.PointSelection.SelectedPoints.Select(selectedCelPoint 
            => new CountOfParts(selectedCelPoint, tempCounts.ToList())).ToList();
    }


    private static void HandleChart(ResearchMethods researchMethod, CalculatedResearchChart chart, ResearchPointSelection pointSelection, int nrOfParts, ref List<CountOfParts> allCounts)
    {
        int pointIndex = 0;
        Dictionary<ChartPoints, FullPointPos> pointPositions = new Dictionary<ChartPoints, FullPointPos>();

        foreach (var posPoint in chart.Positions)
        {
            var details = posPoint.Key.GetDetails();
            if (details.PointCat == PointCats.Common || details.PointCat == PointCats.Angle)
            {
                pointPositions.Add(posPoint.Key, posPoint.Value);
            }
        }

        foreach (ChartPoints selectedCelPoint in pointSelection.SelectedPoints)
        {
          
            foreach (var commonPointPos in pointPositions)
            {
                if (commonPointPos.Key == selectedCelPoint)
                {
                    double longitude = commonPointPos.Value.Ecliptical.MainPosSpeed.Position;
                    int partIndex;

                    // Handle the switch expression
                    switch (researchMethod)
                    {
                        case ResearchMethods.CountPosInSigns:
                            partIndex = SignIndex(longitude);
                            break;
                        case ResearchMethods.CountPosInHouses:
                            partIndex = DefineHouseNr(longitude, nrOfParts, chart.Positions);
                            break;
                        default:
                            partIndex = -1;
                            break;
                    }
                    // Only process if partIndex is valid
                    if (partIndex >= 0)
                    {
                        allCounts[pointIndex].Counts[partIndex]++;
                    }
                }
            }
            pointIndex++;
        }
        Log.Information("CGFIX: at end of loop in HandleChart. Value of pointIndex : " + pointIndex);
        if (!pointSelection.IncludeCusps) return;
        foreach (KeyValuePair<ChartPoints, FullPointPos> cusp in chart.Positions.Where(cusp => cusp.Key.GetDetails().PointCat == PointCats.Cusp))
        {
            allCounts[pointIndex++].Counts[SignIndex(cusp.Value.Ecliptical.MainPosSpeed.Position)]++;
        }
    }


    private static int SignIndex(double longitude)
    {
        return (int)longitude / 30;
    }

    private static List<int> CountTotals(IReadOnlyList<CountOfParts> allCounts)
    {
        List<int> totals = new();
        int nrOfParts = allCounts.Count > 0 ? allCounts[0].Counts.Count : 0;
        for (int i = 0; i < nrOfParts; i++)
        {
            int subTotal = allCounts.Sum(cop => cop.Counts[i]);
            totals.Add(subTotal);
        }
        return totals;
    }

    private static int DefineHouseNr(double longitude, int nrOfCusps, Dictionary<ChartPoints, FullPointPos> positions)  // returns housenr 0..11 of 0..nrOfHouses-1
    {
        Dictionary<ChartPoints, FullPointPos> cusps = (from posPoint in positions 
            where (posPoint.Key.GetDetails().PointCat == PointCats.Cusp) 
            select posPoint).ToDictionary(x => x.Key, x => x.Value);
        List<double> cuspLongitudes = cusps.Select(cusp => cusp.Value.Ecliptical.MainPosSpeed.Position).ToList();
        for (int i = 0; i < nrOfCusps; i++)
        {
            double firstCusp = cuspLongitudes[i];
            double secondCusp = (i == 11 ? cuspLongitudes[0] : cuspLongitudes[i + 1]);
            if (firstCusp > secondCusp)
            {
                if ((longitude > firstCusp && longitude <= 360.0) || (longitude >= 0.0 && longitude < secondCusp)) return i;

            }
            else if (longitude > firstCusp && longitude < secondCusp) return i;
        }
        return -1;
    }

}
