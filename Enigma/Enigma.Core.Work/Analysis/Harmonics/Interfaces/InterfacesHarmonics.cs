// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Charts;

namespace Enigma.Core.Work.Analysis.Harmonics;

/// <summary>Calculator for harmonics.</summary>
public interface IHarmonicsCalculator
{
    /// <summary>Calculate harmonics for a list of positions using a specified harmonic number.</summary>
    /// <param name="originalPositions">List of original positions.</param>
    /// <param name="harmonicNumber">The number for the harmonic to calculate.</param>
    /// <returns>List with harmonic positions in the same sequence as the original positions.</returns>
    public List<double> CalculateHarmonics(List<double> originalPositions, double harmonicNumber);
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


