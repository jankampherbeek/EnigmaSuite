// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Analysis;
using Enigma.Domain.Analysis.Aspects;
using Enigma.Domain.AstronCalculations;
using Enigma.Domain.Charts;
using Enigma.Domain.RequestResponse;

namespace Enigma.Core.Handlers.Interfaces;

/// <summary>Handler for aspects.</summary>
public interface IAspectsHandler
{
    /// <summary>Find aspects to mundane points.</summary>
    /// <param name="request">Request with positions.</param>
    /// <returns>Aspects found.</returns>
    public List<EffectiveAspect> AspectsForMundanePoints(AspectRequest request);

    /// <summary>Find aspects to mundane points.</summary>
    /// <param name="aspectDetails">Supported aspects.</param>
    /// <param name="calculatedChart">Calculated chart.</param>
    /// <returns>Aspects found.</returns>
    public List<EffectiveAspect> AspectsForMundanePoints(List<AspectDetails> aspectDetails, CalculatedChart calculatedChart);

    /// <summary>Find aspects between celestial points (excluding mundane points).</summary>
    /// <param name="request">Request with positions.</param>
    /// <returns>Aspects found.</returns>
    public List<EffectiveAspect> AspectsForCelPoints(AspectRequest request);

    /// <summary>Find aspects between celestial points (excluding mundane points).</summary>
    /// <param name="aspectDetails">Supported aspects.</param>
    /// <param name="fullCelPointPositions">Supported celestial points.</param>
    /// <returns>Aspects found.</returns>
    public List<EffectiveAspect> AspectsForCelPoints(List<AspectDetails> aspectDetails, List<FullCelPointPos> fullCelPointPositions);
}



/// <summary>Handler for harmonics.</summary>
public interface IHarmonicsHandler
{
    /// <summary>Define the harmonics for all positions in CalcualtedChart.</summary>
    /// <param name="chart">Chart with all positions.</param>
    /// <param name="harmonicNumber">The harmonic number, this can also be a fractional number.</param>
    /// <returns>The calculated harmonic positions, all celestial points followed by Mc, Asc, Vertex, Eastpoint in that sequence.</returns>
    public List<double> RetrieveHarmonicPositions(CalculatedChart chart, double harmonicNumber);
}

/// <summary>Handler for midpoints.</summary>
public interface IMidpointsHandler
{
    /// <summary>Retrieve list with all base midpoints between two items, regardless if the midpoint is occupied.</summary>
    /// <param name="chart">Calculated chart with positions.</param>
    /// <returns>All base midpoints.</returns>
    public List<BaseMidpoint> RetrieveBaseMidpoints(CalculatedChart chart);

    /// <summary>Retrieve list with all occupied midpoints for a specifed dial.</summary>
    /// <param name="chart">Calculated chart with positions.</param>
    /// <param name="dialSize">Degrees for specified dial.</param>
    /// <returns>All occupied midpoints.</returns>
    public List<OccupiedMidpoint> RetrieveOccupiedMidpoints(CalculatedChart chart, double dialSize);
}



