// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Domain.Requests;
using Enigma.Domain.Responses;
using Serilog;

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
public sealed class PointsInPartsCounting: IPointsInPartsCounting
{
    /// <inheritdoc/>
    public CountOfPartsResponse CountPointsInParts(List<CalculatedResearchChart> charts, GeneralResearchRequest request)
    {
        return PerformCounts(charts, request);
    }

    private CountOfPartsResponse PerformCounts(List<CalculatedResearchChart> charts, GeneralResearchRequest request)
    {
        var researchMethod = request.Method;
        var nrOfParts = DefineNumberOfParts(request);
        var allCounts = InitializeCounts(request, nrOfParts);
        var pointSelection = request.PointSelection;

        foreach (var chart in charts)
        {
            HandleChart(researchMethod, chart, pointSelection, nrOfParts, ref allCounts);
        }
        var totals = CountTotals(allCounts);
        CountOfPartsResponse response = new(request, allCounts, totals);
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
        var tempCounts = new int[nrOfParts];
        return request.PointSelection.SelectedPoints.Select(selectedCelPoint 
            => new CountOfParts(selectedCelPoint, tempCounts.ToList())).ToList();
    }


    private static void HandleChart(ResearchMethods researchMethod, CalculatedResearchChart chart, ResearchPointSelection pointSelection, int nrOfParts, ref List<CountOfParts> allCounts)
    {
        var pointIndex = 0;
        Dictionary<ChartPoints, FullPointPos> pointPositions = new Dictionary<ChartPoints, FullPointPos>();

        foreach (var posPoint in chart.Positions)
        {
            var details = posPoint.Key.GetDetails();
            if (details.PointCat == PointCats.Common || details.PointCat == PointCats.Angle)
            {
                pointPositions.Add(posPoint.Key, posPoint.Value);
            }
        }

        foreach (var selectedCelPoint in pointSelection.SelectedPoints)
        {
            foreach (var commonPointPos in pointPositions)
            {
                if (commonPointPos.Key != selectedCelPoint) continue;
                var longitude = commonPointPos.Value.Ecliptical.MainPosSpeed.Position;
                
                var partIndex = researchMethod switch
                {
                    ResearchMethods.CountPosInSigns => SignIndex(longitude),
                    ResearchMethods.CountPosInHouses => DefineHouseNr(longitude, nrOfParts, chart.Positions),
                    _ => -1
                };
                if (partIndex >= 0)
                {
                    allCounts[pointIndex].Counts[partIndex]++;
                }
            }
            pointIndex++;
        }
        if (!pointSelection.IncludeCusps) return;
        foreach (var cusp in chart.Positions.Where(cusp => cusp.Key.GetDetails().PointCat == PointCats.Cusp))
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
        List<int> totals = [];
        var nrOfParts = allCounts.Count > 0 ? allCounts[0].Counts.Count : 0;
        for (var i = 0; i < nrOfParts; i++)
        {
            var subTotal = allCounts.Sum(cop => cop.Counts[i]);
            totals.Add(subTotal);
        }
        return totals;
    }

    private static int DefineHouseNr(double longitude, int nrOfCusps, Dictionary<ChartPoints, FullPointPos> positions)  // returns housenr 0..11 of 0..nrOfHouses-1
    {
        var cusps = (from posPoint in positions 
            where (posPoint.Key.GetDetails().PointCat == PointCats.Cusp) 
            select posPoint).ToDictionary(x => x.Key, x => x.Value);
        var cuspLongitudes = cusps.Select(cusp => cusp.Value.Ecliptical.MainPosSpeed.Position).ToList();
        for (var i = 0; i < nrOfCusps; i++)
        {
            var firstCusp = cuspLongitudes[i];
            var secondCusp = (i == 11 ? cuspLongitudes[0] : cuspLongitudes[i + 1]);
            if (firstCusp > secondCusp)
            {
                if ((longitude > firstCusp && longitude <= 360.0) || (longitude >= 0.0 && longitude < secondCusp)) return i;

            }
            else if (longitude > firstCusp && longitude < secondCusp) return i;
        }
        return -1;
    }

}
