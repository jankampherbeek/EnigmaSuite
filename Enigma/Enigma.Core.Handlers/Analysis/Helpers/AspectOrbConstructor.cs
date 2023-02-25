// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers.Interfaces;
using Enigma.Domain.Configuration;
using Enigma.Domain.Interfaces;
using Enigma.Domain.Points;

namespace Enigma.Core.Handlers.Analysis.Helpers;


public sealed class AspectOrbConstructor : IAspectOrbConstructor
{

    private readonly IOrbDefinitions _orbDefinitions;

    public AspectOrbConstructor(IOrbDefinitions orbDefinitions) { _orbDefinitions = orbDefinitions; }

    // <inheritdoc/>
    public double DefineOrb(ChartPoints point1, ChartPoints point2, double baseOrb, double aspectOrbFactor, Dictionary<ChartPoints, ChartPointConfigSpecs> chartPointConfigSpecs)
    {
        double factor1 = _orbDefinitions.DefineChartPointOrb(point1, chartPointConfigSpecs).OrbFactor;
        double factor2 = _orbDefinitions.DefineChartPointOrb(point2, chartPointConfigSpecs).OrbFactor;
        return Math.Max(factor1, factor2) * aspectOrbFactor * baseOrb;
    }

}