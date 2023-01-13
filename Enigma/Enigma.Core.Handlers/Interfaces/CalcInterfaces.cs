// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Calc;
using Enigma.Domain.Points;

namespace Enigma.Core.Handlers.Interfaces;

/// <summary>Mapping of additional information to ChartPoints.</summary>
public interface IChartPointsMapping
{
    /// <summary>Find calculation type for a specific ChartPoint.</summary>
    /// <param name="point">The ChartPoint.</param>
    /// <returns>The calculationtype for the given ChartPoint.</returns>
    public CalculationTypes CalculationTypeForPoint(ChartPoints point);

    /// <summary>Find the SeId (Siwss Ephemeris id) for a specific ChartPoint.</summary>
    /// <remarks>PRE: the chartpoint.
    /// POST: if the pre-condition is fullfilled the correct SeId is returned, otherwise an exception is thrown.</remarks>
    /// <param name="point">The ChartPoint, it should be a celestial points that has a CalculationType CelPointSE.</param>
    /// <exception cref="EnigmaException">Is thrown if the ChartPoint is not supported.</exception>
    /// <returns></returns>
    public int SeIdForCelestialPoint(ChartPoints point);



}