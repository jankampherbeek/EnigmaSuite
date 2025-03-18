// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023, 2024, 2025.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Constants;
using Enigma.Domain.Exceptions;
using Enigma.Domain.References;
using Serilog;

namespace Enigma.Core.Calc;

/// <summary>Mapping of additional information to ChartPoints.</summary>
public interface IChartPointsMapping
{
    /// <summary>Find calculation type for a specific ChartPoint.</summary>
    /// <param name="point">The ChartPoint.</param>
    /// <returns>The calculationtype for the given ChartPoint.</returns>
    public CalculationCats CalculationTypeForPoint(ChartPoints point);

    /// <summary>Find the SeId (Siwss Ephemeris id) for a specific ChartPoint.</summary>
    /// <remarks>PRE: the chartpoint.
    /// POST: if the pre-condition is fullfilled the correct SeId is returned, otherwise an exception is thrown.</remarks>
    /// <param name="point">The ChartPoint, it should be a celestial points that has a CalculationType CommonSE.</param>
    /// <exception cref="EnigmaException">Is thrown if the ChartPoint is not supported.</exception>
    /// <returns></returns>
    public int SeIdForCelestialPoint(ChartPoints point);

}

/// <inheritdoc/>
public sealed class ChartPointsMapping : IChartPointsMapping
{
    private readonly List<ChartPoints> _elementsCandidates = new() { ChartPoints.PersephoneRam, ChartPoints.HermesRam, ChartPoints.DemeterRam };
    private readonly List<ChartPoints> _formulaCandidates = new() { ChartPoints.ApogeeCorrected, ChartPoints.PersephoneCarteret, 
        ChartPoints.VulcanusCarteret, ChartPoints.Priapus, ChartPoints.Dragon, ChartPoints.Beast };
    private readonly List<ChartPoints> _apsideCandidates = new() { ChartPoints.BlackSun, ChartPoints.Diamond };

    /// <inheritdoc/>
    public CalculationCats CalculationTypeForPoint(ChartPoints point)
    {
        return point.GetDetails().CalculationCat;
    }

    /// <inheritdoc/>
    public int SeIdForCelestialPoint(ChartPoints point)
    {
        int seId = point.GetDetails().CalcId;
        if (seId >= -1) return seId;
        Log.Error("ChartPointsMapping.SeIdForCelestialPoint() was called with with an unrecognized ChartPoint: {Point}", point);
        throw new EnigmaException("Wrong ChartPoint");
    }

}