// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023, 2024.
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
    private readonly List<ChartPoints> _formulaCandidates = new() { ChartPoints.ApogeeDuval, ChartPoints.PersephoneCarteret, 
        ChartPoints.VulcanusCarteret, ChartPoints.PriapusMean, ChartPoints.PriapusTrue, ChartPoints.Dragon, ChartPoints.Beast };
    private readonly List<ChartPoints> _apsideCandidates = new() { ChartPoints.BlackSun, ChartPoints.Diamond };

    /// <inheritdoc/>
    public CalculationCats CalculationTypeForPoint(ChartPoints point)
    {
        int pointId = (int)point;
        return pointId switch
        {
            // celestial points, < 1000 to avoid cusps etc.
            < 1000 when _elementsCandidates.Contains(point) => CalculationCats.CommonElements,
            < 1000 when _formulaCandidates.Contains(point) => CalculationCats.CommonFormula,
            < 1000 when _apsideCandidates.Contains(point) => CalculationCats.Apsides,
            < 1000 => CalculationCats.CommonSe,
            // mundane points or cusps
            < 3000 => CalculationCats.Mundane,
            _ => CalculationCats.Lots
        };
    }

    /// <inheritdoc/>
    public int SeIdForCelestialPoint(ChartPoints point)
    {
        int seId = IdForPoint(point);
        if (seId >= 0) return seId;
        Log.Error("ChartPointsMapping.SeIdForCelestialPoint() was called with with an unrecognized ChartPoint: {Point}", point);
        throw new EnigmaException("Wrong ChartPoint");
    }

    private static int IdForPoint(ChartPoints point)
    {
        return point switch
        {
            ChartPoints.Sun => EnigmaConstants.SE_SUN,
            ChartPoints.Moon => EnigmaConstants.SE_MOON,
            ChartPoints.Mercury => EnigmaConstants.SE_MERCURY,
            ChartPoints.Venus => EnigmaConstants.SE_VENUS,
            ChartPoints.Earth => EnigmaConstants.SE_EARTH,
            ChartPoints.Mars => EnigmaConstants.SE_MARS,
            ChartPoints.Jupiter => EnigmaConstants.SE_JUPITER,
            ChartPoints.Saturn => EnigmaConstants.SE_SATURN,
            ChartPoints.Uranus => EnigmaConstants.SE_URANUS,
            ChartPoints.Neptune => EnigmaConstants.SE_NEPTUNE,
            ChartPoints.Pluto => EnigmaConstants.SE_PLUTO,
            ChartPoints.MeanNode => EnigmaConstants.SE_MEAN_NODE,
            ChartPoints.TrueNode => EnigmaConstants.SE_TRUE_NODE,
            ChartPoints.Chiron => EnigmaConstants.SE_CHIRON,
            ChartPoints.PersephoneRam => EnigmaConstants.SE_PERSEPHONE_RAM,
            ChartPoints.HermesRam => EnigmaConstants.SE_HERMES_RAM,
            ChartPoints.DemeterRam => EnigmaConstants.SE_DEMETER_RAM,
            ChartPoints.CupidoUra => EnigmaConstants.SE_CUPIDO_URA,
            ChartPoints.HadesUra => EnigmaConstants.SE_HADES_URA,
            ChartPoints.ZeusUra => EnigmaConstants.SE_ZEUS_URA,
            ChartPoints.KronosUra => EnigmaConstants.SE_KRONOS_URA,
            ChartPoints.ApollonUra => EnigmaConstants.SE_APOLLON_URA,
            ChartPoints.AdmetosUra => EnigmaConstants.SE_ADMETOS_URA,
            ChartPoints.VulcanusUra => EnigmaConstants.SE_VULCANUS_URA,
            ChartPoints.PoseidonUra => EnigmaConstants.SE_POSEIDON_URA,
            ChartPoints.Eris => EnigmaConstants.SE_ERIS,
            ChartPoints.Pholus => EnigmaConstants.SE_PHOLUS,
            ChartPoints.Ceres => EnigmaConstants.SE_CERES,
            ChartPoints.Pallas => EnigmaConstants.SE_PALLAS,
            ChartPoints.Juno => EnigmaConstants.SE_JUNO,
            ChartPoints.Vesta => EnigmaConstants.SE_VESTA,
            ChartPoints.Isis => EnigmaConstants.SE_ISIS,
            ChartPoints.Nessus => EnigmaConstants.SE_NESSUS,
            ChartPoints.Huya => EnigmaConstants.SE_HUYA,
            ChartPoints.Varuna => EnigmaConstants.SE_VARUNA,
            ChartPoints.Ixion => EnigmaConstants.SE_IXION,
            ChartPoints.Quaoar => EnigmaConstants.SE_QUAOAR,
            ChartPoints.Haumea => EnigmaConstants.SE_HAUMEA,
            ChartPoints.Orcus => EnigmaConstants.SE_ORCUS,
            ChartPoints.Makemake => EnigmaConstants.SE_MAKEMAKE,
            ChartPoints.Sedna => EnigmaConstants.SE_SEDNA,
            ChartPoints.Hygieia => EnigmaConstants.SE_HYGIEIA,
            ChartPoints.Astraea => EnigmaConstants.SE_ASTRAEA,
            ChartPoints.ApogeeMean => EnigmaConstants.SE_MEAN_APOGEE,
            ChartPoints.ApogeeCorrected => EnigmaConstants.SE_OSCU_APOG,
            ChartPoints.ApogeeInterpolated => EnigmaConstants.SE_INTP_APOG,
            _ => -1
        };
    }
}