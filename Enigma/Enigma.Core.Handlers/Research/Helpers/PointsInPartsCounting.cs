// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers.Interfaces;
using Enigma.Core.Handlers.Research.Interfaces;
using Enigma.Domain.Calc.ChartItems;
using Enigma.Domain.Points;
using Enigma.Domain.Research;
using Serilog;
using System.Text.Json;

namespace Enigma.Core.Handlers.Research.Helpers;

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
        ResearchMethods researchMethod = request.Method;
        int nrOfParts = DefineNumberOfParts(request);
        List<CountOfParts> allCounts = InitializeCounts(request, nrOfParts);
        ResearchPointsSelection pointsSelection = request.PointsSelection;

        foreach (CalculatedResearchChart chart in charts)
        {
            HandleChart(researchMethod, chart, pointsSelection, nrOfParts, ref allCounts);
        }

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
        return request.PointsSelection.SelectedPoints.Select(selectedCelPoint 
            => new CountOfParts(selectedCelPoint, tempCounts.ToList())).ToList();
    }


    private static void HandleChart(ResearchMethods researchMethod, CalculatedResearchChart chart, ResearchPointsSelection pointsSelection, int nrOfParts, ref List<CountOfParts> allCounts)
    {
        int pointIndex = 0;
        foreach (ChartPoints selectedCelPoint in pointsSelection.SelectedPoints)
        {
            Dictionary<ChartPoints, FullPointPos> pointPositions = (
                from posPoint in chart.Positions
                where (posPoint.Key.GetDetails().PointCat == PointCats.Common || posPoint.Key.GetDetails().PointCat == PointCats.Angle)
                select posPoint).ToDictionary(x => x.Key, x => x.Value);

            foreach (int partIndex in from commonPointPos in pointPositions 
                     let partIndex = -1 where commonPointPos.Key == selectedCelPoint 
                     let longitude = commonPointPos.Value.Ecliptical.MainPosSpeed.Position 
                     select researchMethod switch
                     {
                         ResearchMethods.CountPosInSigns => SignIndex(longitude),
                         ResearchMethods.CountPosInHouses => DefineHouseNr(longitude, nrOfParts, chart.Positions),
                         _ => partIndex
                     } into partIndex where partIndex >= 0 select partIndex)
            {
                allCounts[pointIndex].Counts[partIndex]++;
            }
            pointIndex++;
        }

        if (!pointsSelection.IncludeCusps) return;
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
