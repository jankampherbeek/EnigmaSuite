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
        if (pointId < 1000)                         // celestial points
        {
            if (_elementsCandidates.Contains(point)) return CalculationCats.CommonElements;
            else if (_formulaCandidates.Contains(point)) return CalculationCats.CommonFormula;
            else return CalculationCats.CommonSE;
        }
        else if (pointId < 3000)                    // mundane points or cusps
        {
            return CalculationCats.Mundane;
        }
        else return CalculationCats.Lots;      // specific points
    }

    /// <inheritdoc/>
    public int SeIdForCelestialPoint(ChartPoints point)
    {
        int seId = IdForPoint(point);
        if (seId < 0)
        {
            string errorText = "ChartPointsMapping.SeIdForCelestialPoint() was called with with an unrecognized ChartPoint: " + point.ToString();
            Log.Error(errorText);
            throw new EnigmaException(errorText);
        }
        return seId;
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
            _ => -1,
        };
    }
}