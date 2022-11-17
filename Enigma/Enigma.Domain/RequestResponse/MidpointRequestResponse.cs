// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.Analysis;
using Enigma.Domain.Charts;

namespace Enigma.Domain.RequestResponse;

/// <summary>Request for retrieving midpoints.</summary>
public record MidpointRequest
{
    public CalculatedChart CalcChart { get; }
    public MidpointTypes MidpointType { get; }

    public MidpointRequest(MidpointTypes midpointType ,CalculatedChart calculatedChart)
    {
        MidpointType= midpointType;
        CalcChart = calculatedChart;
    }
}


/// <summary>Response for midpoints, includes simple positions in 360 degree and actual dial, and occupied midpoints.</summary>
public record MidpointResponse
{
    /// <summary>All midpointpositions in a 360 degree dial.</summary>
    public List<EffectiveMidpoint> EffectiveMidpoints { get; }

    /// <summary>All midpointpositions in the selected dial.</summary>
    public List<EffectiveMidpoint> EffectiveMidpointsInDisk { get; }

    /// <summary>Occupiedmidpoints in the selected dial.</summary>
    public List<EffOccupiedMidpoint> EffOccupiedMidpoints { get; }

    public MidpointResponse(List<EffectiveMidpoint> effectiveMidpoints, List<EffectiveMidpoint> effectiveMidpointsinDisk, List<EffOccupiedMidpoint> effOccupiedMidpoints) {
        EffectiveMidpoints = effectiveMidpoints;
        EffectiveMidpointsInDisk = effectiveMidpointsinDisk;
        EffOccupiedMidpoints = effOccupiedMidpoints;
    }
}