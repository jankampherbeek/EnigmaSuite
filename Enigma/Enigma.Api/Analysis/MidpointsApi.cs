﻿// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Ardalis.GuardClauses;
using Enigma.Api.Interfaces;
using Enigma.Core.Work.Analysis.Midpoints.Interfaces;
using Enigma.Domain.Analysis;
using Enigma.Domain.Charts;

namespace Enigma.Api.Analysis;


/// <inheritdoc/>
public class MidpointsApi : IMidpointsApi
{

    private readonly IMidpointsHandler _midpointsHandler;

    public MidpointsApi(IMidpointsHandler midpointsHandler)
    {
        _midpointsHandler = midpointsHandler;
    }


    /// <inheritdoc/>
    public List<BaseMidpoint> AllMidpoints(CalculatedChart chart)
    {
        Guard.Against.Null(chart);
        return _midpointsHandler.RetrieveBaseMidpoints(chart);
    }


    /// <inheritdoc/>
    public List<OccupiedMidpoint> OccupiedMidpoints(CalculatedChart chart, double dialSize)
    {
        Guard.Against.Null(chart);
        return _midpointsHandler.RetrieveOccupiedMidpoints(chart, dialSize);
    }
        


}