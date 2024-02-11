// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Dtos;
using Enigma.Domain.References;

namespace Enigma.Core.Analysis;

/// <summary>
/// Define actual orb for an aspect.
/// </summary>
public interface IAspectOrbConstructor
{
    /// <summary>Define orb between two celestial points for a given aspect.</summary>
    public double DefineOrb(ChartPoints point1, ChartPoints point2, double baseOrb, double aspectOrbFactor, Dictionary<ChartPoints, ChartPointConfigSpecs?> chartPointConfigSpecs);
}

public sealed class AspectOrbConstructor : IAspectOrbConstructor
{

    private readonly IOrbDefinitions _orbDefinitions;

    public AspectOrbConstructor(IOrbDefinitions orbDefinitions) { _orbDefinitions = orbDefinitions; }

    // <inheritdoc/>
    public double DefineOrb(ChartPoints point1, ChartPoints point2, double baseOrb, double aspectOrbFactor, 
        Dictionary<ChartPoints, ChartPointConfigSpecs?> chartPointConfigSpecs)
    {
        double factor1 = _orbDefinitions.DefineChartPointOrb(point1, chartPointConfigSpecs).Value;
        double factor2 = _orbDefinitions.DefineChartPointOrb(point2, chartPointConfigSpecs).Value;
        return Math.Max(factor1, factor2) * aspectOrbFactor * baseOrb;
    }

}