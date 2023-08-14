// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers.Interfaces;
using Enigma.Domain.Calc;
using Enigma.Domain.Constants;
using Enigma.Domain.Exceptions;
using Enigma.Domain.Points;
using Serilog;

namespace Enigma.Core.Handlers.Calc;

/// <inheritdoc/>
public sealed class ChartPointsMapping : IChartPointsMapping
{
    private readonly List<ChartPoints> _elementsCandidates = new() { ChartPoints.PersephoneRam, ChartPoints.HermesRam, ChartPoints.DemeterRam };
    private readonly List<ChartPoints> _formulaCandidates = new() { ChartPoints.ApogeeDuval, ChartPoints.PersephoneCarteret, ChartPoints.VulcanusCarteret };


    /// <inheritdoc/>
    public CalculationCats CalculationTypeForPoint(ChartPoints point)
    {
        int pointId = (int)point;
        return pointId switch
        {
            // celestial points
            < 1000 when _elementsCandidates.Contains(point) => CalculationCats.CommonElements,
            < 1000 when _formulaCandidates.Contains(point) => CalculationCats.CommonFormula,
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
            ChartPoints.Sun => EnigmaConstants.SeSun,
            ChartPoints.Moon => EnigmaConstants.SeMoon,
            ChartPoints.Mercury => EnigmaConstants.SeMercury,
            ChartPoints.Venus => EnigmaConstants.SeVenus,
            ChartPoints.Earth => EnigmaConstants.SeEarth,
            ChartPoints.Mars => EnigmaConstants.SeMars,
            ChartPoints.Jupiter => EnigmaConstants.SeJupiter,
            ChartPoints.Saturn => EnigmaConstants.SeSaturn,
            ChartPoints.Uranus => EnigmaConstants.SeUranus,
            ChartPoints.Neptune => EnigmaConstants.SeNeptune,
            ChartPoints.Pluto => EnigmaConstants.SePluto,
            ChartPoints.MeanNode => EnigmaConstants.SeMeanNode,
            ChartPoints.TrueNode => EnigmaConstants.SeTrueNode,
            ChartPoints.Chiron => EnigmaConstants.SeChiron,
            ChartPoints.PersephoneRam => EnigmaConstants.SePersephoneRam,
            ChartPoints.HermesRam => EnigmaConstants.SeHermesRam,
            ChartPoints.DemeterRam => EnigmaConstants.SeDemeterRam,
            ChartPoints.CupidoUra => EnigmaConstants.SeCupidoUra,
            ChartPoints.HadesUra => EnigmaConstants.SeHadesUra,
            ChartPoints.ZeusUra => EnigmaConstants.SeZeusUra,
            ChartPoints.KronosUra => EnigmaConstants.SeKronosUra,
            ChartPoints.ApollonUra => EnigmaConstants.SeApollonUra,
            ChartPoints.AdmetosUra => EnigmaConstants.SeAdmetosUra,
            ChartPoints.VulcanusUra => EnigmaConstants.SeVulcanusUra,
            ChartPoints.PoseidonUra => EnigmaConstants.SePoseidonUra,
            ChartPoints.Eris => EnigmaConstants.SeEris,
            ChartPoints.Pholus => EnigmaConstants.SePholus,
            ChartPoints.Ceres => EnigmaConstants.SeCeres,
            ChartPoints.Pallas => EnigmaConstants.SePallas,
            ChartPoints.Juno => EnigmaConstants.SeJuno,
            ChartPoints.Vesta => EnigmaConstants.SeVesta,
            ChartPoints.Isis => EnigmaConstants.SeIsis,
            ChartPoints.Nessus => EnigmaConstants.SeNessus,
            ChartPoints.Huya => EnigmaConstants.SeHuya,
            ChartPoints.Varuna => EnigmaConstants.SeVaruna,
            ChartPoints.Ixion => EnigmaConstants.SeIxion,
            ChartPoints.Quaoar => EnigmaConstants.SeQuaoar,
            ChartPoints.Haumea => EnigmaConstants.SeHaumea,
            ChartPoints.Orcus => EnigmaConstants.SeOrcus,
            ChartPoints.Makemake => EnigmaConstants.SeMakemake,
            ChartPoints.Sedna => EnigmaConstants.SeSedna,
            ChartPoints.Hygieia => EnigmaConstants.SeHygieia,
            ChartPoints.Astraea => EnigmaConstants.SeAstraea,
            ChartPoints.ApogeeMean => EnigmaConstants.SeMeanApogee,
            ChartPoints.ApogeeCorrected => EnigmaConstants.SeOscuApog,
            ChartPoints.ApogeeInterpolated => EnigmaConstants.SeIntpApog,
            _ => -1
        };
    }
}