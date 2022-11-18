// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers.Interfaces;
using Enigma.Core.Helpers.Interfaces;
using Enigma.Domain.Analysis;
using Enigma.Domain.AstronCalculations;
using Enigma.Domain.Charts;
using Enigma.Domain.Enums;
using Enigma.Domain.Interfaces;
using Enigma.Domain.RequestResponse;

namespace Enigma.Core.Handlers.Analysis;

public class MidpointsHandler: IMidpointsHandler
{

    private readonly IMidpointsHelper _midpointsHelper;
    private readonly IMidpointSpecifications _midpointSpecifications;
    private readonly IAnalysisPointsMapping _analysisPointsMapping;

    public MidpointsHandler(IMidpointsHelper midpointsHelper, IMidpointSpecifications midpointSpecifications, IAnalysisPointsMapping analysisPointsMapping)
    {
        _midpointsHelper = midpointsHelper;
        _midpointSpecifications = midpointSpecifications;
        _analysisPointsMapping = analysisPointsMapping;
    }

    public MidpointResponse RetrieveMidpoints(MidpointRequest request)
    {
        double divisionForDial = 1.0;
        if (request.MidpointType == MidpointTypes.Dial90) divisionForDial = 0.25;
        if (request.MidpointType == MidpointTypes.Dial45) divisionForDial = 0.125;
        List<PointGroups> pointGroups = new List<PointGroups> {PointGroups.SolarSystemPoints, PointGroups.MundanePoints, PointGroups.ZodiacalPoints };
        CoordinateSystems coordSystem = CoordinateSystems.Ecliptical;
        bool mainCoord = true;
        CalculatedChart chart = request.CalcChart;
        List<AnalysisPoint> analysisPoints = _analysisPointsMapping.ChartToSingLeAnalysisPoints(pointGroups, coordSystem, mainCoord, chart);
        List<EffectiveMidpoint> effectiveMidpoints = FindMidpoints(analysisPoints, divisionForDial);
        List<EffectiveMidpoint> effectiveMidpointsForDial = CreateMidpoints4Dial(request.MidpointType, effectiveMidpoints);
        List<EffOccupiedMidpoint> effOccupiedMidpoints = FindOccupiedMidpoints(request.MidpointType, effectiveMidpoints, analysisPoints);        
        return new MidpointResponse(effectiveMidpoints, effectiveMidpointsForDial, effOccupiedMidpoints);
    }

    /// <inheritdoc/>
    private List<EffectiveMidpoint> FindMidpoints(List<AnalysisPoint> analysisPoints, double divisionForDial)
    {
        List<EffectiveMidpoint> midpoints = new();
        int nrOfPoints = analysisPoints.Count;
        for (int i = 0; i < nrOfPoints; i++)
        {
            for (int j = i + 1; j < nrOfPoints; j++)
            {
                midpoints.Add(_midpointsHelper.ConstructEffectiveMidpointInDial(analysisPoints[i], analysisPoints[j], divisionForDial));
            }
        }
        return midpoints;
    }

    private List<EffectiveMidpoint> CreateMidpoints4Dial(MidpointTypes midpointType, List<EffectiveMidpoint> midpoints)
    {
        double factor = _midpointSpecifications.DetailsForMidpoint(midpointType).Division;
        return _midpointsHelper.CreateMidpoints4Dial(factor, midpoints);
    }


    private List<EffOccupiedMidpoint> FindOccupiedMidpoints(MidpointTypes midpointType, List<EffectiveMidpoint> effMidpoints, List<AnalysisPoint> analysisPoints)
    {
        List<EffOccupiedMidpoint> effOccupiedMidpoints = new();
        MidpointDetails mpDetails = _midpointSpecifications.DetailsForMidpoint(midpointType);
        double orbFactor = mpDetails.OrbFactor;
        double maxOrb = 1.6 * orbFactor;         // TODO retrieve orb from configuration.
        double division = mpDetails.Division;

        foreach (var effMidpoint in effMidpoints)
        {
            double position = effMidpoint.Position;
            foreach (var analysisPoint in analysisPoints)
            {
                double orb = _midpointsHelper.MeasureMidpointDeviation(division, position, analysisPoint.Position);
                if (orb <= maxOrb)
                {
                    double exactness = 100.0 - ((orb / maxOrb) * 100.0);
                    effOccupiedMidpoints.Add(new EffOccupiedMidpoint(effMidpoint, analysisPoint, orb, exactness));
                }
            }

        }
        return effOccupiedMidpoints;
    }

}