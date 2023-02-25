// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.Analysis;
using Enigma.Domain.Analysis.Aspects;
using Enigma.Domain.Charts;
using Enigma.Domain.RequestResponse;

namespace Enigma.Api.Interfaces;


/// <summary>Api for the analysis of aspects.</summary>
public interface IAspectsApi
{
    /// <summary>Aspects for celestial points.</summary>
    public List<DefinedAspect> AspectsForCelPoints(AspectRequest request);

}


/// <summary>Api for the analysis of midpoints.</summary>
public interface IMidpointsApi
{
    /// <summary>Return all base midpoints.</summary>
    /// <param name="chart">Calculated chart.</param>
    /// <returns>Midpoints in 360 degree dial, regardless of being occupied.</returns>
    public List<BaseMidpoint> AllMidpoints(CalculatedChart chart);

    /// <summary>Return all occupied midpoints for a specific dial.</summary>
    /// <param name="chart">Chart with positions.</param>
    /// <param name="dialSize">Size of dial in degrees.</param>
    /// <param name="orb">Base orb from configuration.</param>
    /// <returns>All occupied midpoints.</returns>
    public List<OccupiedMidpoint> OccupiedMidpoints(CalculatedChart chart, double dialSize, double orb);
}

/// <summary>Api for the calculation of harmonics.</summary>
public interface IHarmonicsApi
{
    /// <summary>Calculate harmonic positions using the given harmonic number.</summary>
    /// <param name="chart">Calculated chart.</param>
    /// <param name="harmonicNumber">Number for the harmonic, a fractional number is possible.</param>
    /// <returns>List with harmnc positions, the celestial points in the same sequence as in the calculated chart, followed by Mc, Asc, Vertex and Eastpoint (in that sequence).</returns>
    public List<double> Harmonics(CalculatedChart chart, double harmonicNumber);
}