﻿// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Core.Handlers.Interfaces;
using Enigma.Domain.Analysis;
using Enigma.Domain.Charts;
using Enigma.Domain.Points;

namespace Enigma.Core.Handlers.Analysis;

/// <inheritdoc/>
public sealed class MidpointsHandler : IMidpointsHandler
{
    private readonly IPointsForMidpoints _analysisPointsForMidpoints;
    private readonly IBaseMidpointsCreator _baseMidpointsCreator;
    private readonly IOccupiedMidpoints _occupiedMidpoints;


    public MidpointsHandler(IPointsForMidpoints analysisPointsForMidpoints,
        IBaseMidpointsCreator baseMidpointsCreator,
        IOccupiedMidpoints occupiedMidpoints)
    {
        _analysisPointsForMidpoints = analysisPointsForMidpoints;
        _baseMidpointsCreator = baseMidpointsCreator;
        _occupiedMidpoints = occupiedMidpoints;
    }

    /// <inheritdoc/>
    public List<BaseMidpoint> RetrieveBaseMidpoints(CalculatedChart chart)
    {
        double dialSize = 360.0;
        List<PositionedPoint> analysisPoints = _analysisPointsForMidpoints.CreateAnalysisPoints(chart, dialSize);
        return _baseMidpointsCreator.CreateBaseMidpoints(analysisPoints);
    }

    /// <inheritdoc/>
    public List<OccupiedMidpoint> RetrieveOccupiedMidpoints(CalculatedChart chart, double dialSize)
    {
        return _occupiedMidpoints.CalculateOccupiedMidpoints(chart, dialSize);
    }
}