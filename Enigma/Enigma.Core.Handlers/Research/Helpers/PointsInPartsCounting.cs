// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers.Interfaces;
using Enigma.Core.Handlers.Research.Interfaces;
using Enigma.Domain.Calc.ChartItems;
using Enigma.Domain.Points;
using Enigma.Domain.Research;
using Enigma.Research.Domain;
using Newtonsoft.Json;
using Serilog;

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

        string jsonText = JsonConvert.SerializeObject(response, Formatting.Indented);
        string pathForResults = _researchPaths.CountResultsPath(request.ProjectName, researchMethod.ToString(), request.UseControlGroup);
        _filePersistencyHandler.WriteFile(pathForResults, jsonText);
        Log.Information("Json with countings written to {path}.", pathForResults);
        return response;
    }

    private static int DefineNumberOfParts(GeneralResearchRequest request)
    {
        switch (request.Method)
        {
            case ResearchMethods.CountPosInSigns:
                return 12;
            case ResearchMethods.CountPosInHouses:
                return request.Config.HouseSystem.GetDetails().NrOfCusps;
            default:
                string error = "PointsInPartsCounting: unsupported method in request: " + request.Method.ToString();
                Log.Error(error);
                throw new ArgumentException(error);
        }
    }


    private static List<CountOfParts> InitializeCounts(GeneralResearchRequest request, int nrOfParts)
    {
        List<CountOfParts> allCounts = new();
        int[] tempCounts = new int[nrOfParts];
        foreach (ChartPoints selectedCelPoint in request.PointsSelection.SelectedPoints)
        {
            allCounts.Add(new(selectedCelPoint, tempCounts.ToList()));
        }
/*        if (request.Method != ResearchMethods.CountPosInHouses)
        {
            foreach (ChartPoints selectedMundanePoint in request.PointsSelection.SelectedMundanePoints)
            {
                allCounts.Add(new(selectedMundanePoint, tempCounts.ToList()));
            }
            if (request.PointsSelection.IncludeCusps)
            {
                int nrOfCusps = request.Config.HouseSystem.GetDetails().NrOfCusps;
                for (int i = 0; i < nrOfCusps; i++)
                {
                    int index = i + 1;
                    ChartPoints cusp = ChartPoints.None.PointForIndex(index + 2000);
                    allCounts.Add(new(cusp, tempCounts.ToList()));
                }
            }
        } */
        return allCounts;
    }


    private static void HandleChart(ResearchMethods researchMethod, CalculatedResearchChart chart, ResearchPointsSelection pointsSelection, int nrOfParts, ref List<CountOfParts> allCounts)
    {
        int pointIndex = 0;
        foreach (ChartPoints selectedCelPoint in pointsSelection.SelectedPoints)
        {
            Dictionary<ChartPoints, FullPointPos> pointPositions = (
                from posPoint in chart.Positions 
                where (posPoint.Key.GetDetails().PointCat == PointCats.Common) 
                select posPoint).ToDictionary(x => x.Key, x => x.Value);

            foreach (KeyValuePair<ChartPoints, FullPointPos> commonPointPos in pointPositions)
            {
                int partIndex = -1;
                if (commonPointPos.Key == selectedCelPoint)
                {
                    double longitude = commonPointPos.Value.Ecliptical.MainPosSpeed.Position;
                    switch (researchMethod)
                    {
                        case ResearchMethods.CountPosInSigns:
                            partIndex = SignIndex(longitude);
                            break;
                        case ResearchMethods.CountPosInHouses:
                            partIndex = DefineHouseNr(longitude, nrOfParts, chart.Positions);
                            break;
                        default:
                            break;
                    }
                    if (partIndex >= 0) allCounts[pointIndex].Counts[partIndex]++;
                }
            }
            pointIndex++;
        }

 /*           if (pointsSelection.IncludeCusps)
            {
                foreach (var cusp in chart.Positions)
                {
                    if (cusp.Key.GetDetails().PointCat == PointCats.Cusp)
                    {
                        allCounts[pointIndex++].Counts[SignIndex(cusp.Value.Ecliptical.MainPosSpeed.Position)]++;
                    }
                }
            }  */
    }


    private static int SignIndex(double longitude)
    {
        return (int)longitude / 30;
    }

    private static List<int> CountTotals(List<CountOfParts> allCounts)
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

    private static int DefineHouseNr(double longitude, int nrOfCusps,  Dictionary<ChartPoints, FullPointPos> positions)  // returns housenr 0..11 of 0..nrOfHouses-1
    {
        Dictionary<ChartPoints, FullPointPos> cusps = (from posPoint in positions where (posPoint.Key.GetDetails().PointCat == PointCats.Cusp) select posPoint).ToDictionary(x => x.Key, x => x.Value);
        List<double> cuspLongitudes = new();
        foreach (var cusp in cusps)
        {
            cuspLongitudes.Add(cusp.Value.Ecliptical.MainPosSpeed.Position);
        }
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
