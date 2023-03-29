// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.Calc;
using Serilog;

namespace Enigma.Domain.Points;

/// <summary>Supported points.</summary>
/// <remarks>Any point that can be shown in a chart.</remarks>
public enum ChartPoints
{
    None = -1, Sun = 0, Moon = 1, Mercury = 2, Venus = 3, Earth = 4, Mars = 5, Jupiter = 6, Saturn = 7,
    Uranus = 8, Neptune = 9, Pluto = 10,
    MeanNode = 11, TrueNode = 12, Chiron = 13,
    PersephoneRam = 14, HermesRam = 15, DemeterRam = 16,
    CupidoUra = 17, HadesUra = 18, ZeusUra = 19, KronosUra = 20, ApollonUra = 21, AdmetosUra = 22, VulcanusUra = 23, PoseidonUra = 24,
    Eris = 25, Pholus = 26, Ceres = 27, Pallas = 28, Juno = 29, Vesta = 30, Isis = 31, Nessus = 32,
    Huya = 33, Varuna = 34, Ixion = 35, Quaoar = 36, Haumea = 37, Orcus = 38, Makemake = 39, Sedna = 40, Hygieia = 41, Astraea = 42,
    ApogeeMean = 43, ApogeeCorrected = 44, ApogeeInterpolated = 45, ApogeeDuval = 46,
    PersephoneCarteret = 47, VulcanusCarteret = 48,
    Ascendant = 1001, Mc = 1002, EastPoint = 1003, Vertex = 1004,
    Cusp1 = 2001, Cusp2 = 2002, Cusp3 = 2003, Cusp4 = 2004, Cusp5 = 2005, Cusp6 = 2006, Cusp7 = 2007, Cusp8 = 2008, Cusp9 = 2009,
    Cusp10 = 2010, Cusp11 = 2011, Cusp12 = 2012, Cusp13 = 2013, Cusp14 = 2014, Cusp15 = 2015, Cusp16 = 2016, Cusp17 = 2017, Cusp18 = 2018,
    Cusp19 = 2019, Cusp20 = 2020, Cusp21 = 2021, Cusp22 = 2022, Cusp23 = 2023, Cusp24 = 2024, Cusp25 = 2025, Cusp26 = 2026, Cusp27 = 2027,
    Cusp28 = 2028, Cusp29 = 2029, Cusp30 = 2030, Cusp31 = 2031, Cusp32 = 2032, Cusp33 = 2033, Cusp34 = 2034, Cusp35 = 2035, Cusp36 = 2036,
    ZeroAries = 3001, 
    FortunaSect = 4001, FortunaNoSect = 4002
}

/// <summary>Details for a point.</summary>
/// <param name="Point">The point.</param>
/// <param name="PointCat">The category for the point.</param>
/// <param name="CalculationCat">The category of calculation that is used for this point.</param>
/// <param name="TextId">Id to find a descriptive text in a resource bundle.</param>
public record PointDetails(ChartPoints Point, PointCats PointCat, CalculationCats CalculationCat, string TextId);


/// <summary>Extension class for ChartPoints.</summary>
public static class PointsExtensions
{
    /// <summary>Retrieve details for a point.</summary>
    /// <param name="point">The point.</param>
    /// <returns>Details for the point.</returns>
    public static PointDetails GetDetails(this ChartPoints point)
    {
        return point switch
        {

            ChartPoints.Sun => new PointDetails(point, PointCats.Common, CalculationCats.CommonSE, "ref.enum.celpoint.sun"),
            ChartPoints.Moon => new PointDetails(point, PointCats.Common, CalculationCats.CommonSE, "ref.enum.celpoint.moon"),
            ChartPoints.Mercury => new PointDetails(point, PointCats.Common, CalculationCats.CommonSE, "ref.enum.celpoint.mercury"),
            ChartPoints.Venus => new PointDetails(point, PointCats.Common, CalculationCats.CommonSE, "ref.enum.celpoint.venus"),
            ChartPoints.Earth => new PointDetails(point, PointCats.Common, CalculationCats.CommonSE, "ref.enum.celpoint.earth"),
            ChartPoints.Mars => new PointDetails(point, PointCats.Common, CalculationCats.CommonSE, "ref.enum.celpoint.mars"),
            ChartPoints.Jupiter => new PointDetails(point, PointCats.Common, CalculationCats.CommonSE, "ref.enum.celpoint.jupiter"),
            ChartPoints.Saturn => new PointDetails(point, PointCats.Common, CalculationCats.CommonSE, "ref.enum.celpoint.saturn"),
            ChartPoints.Uranus => new PointDetails(point, PointCats.Common, CalculationCats.CommonSE, "ref.enum.celpoint.uranus"),
            ChartPoints.Neptune => new PointDetails(point, PointCats.Common, CalculationCats.CommonSE, "ref.enum.celpoint.neptune"),
            ChartPoints.Pluto => new PointDetails(point, PointCats.Common, CalculationCats.CommonSE, "ref.enum.celpoint.pluto"),
            ChartPoints.MeanNode => new PointDetails(point, PointCats.Common, CalculationCats.CommonSE, "ref.enum.celpoint.meannode"),
            ChartPoints.TrueNode => new PointDetails(point, PointCats.Common, CalculationCats.CommonSE, "ref.enum.celpoint.truenode"),
            ChartPoints.Chiron => new PointDetails(point, PointCats.Common, CalculationCats.CommonSE, "ref.enum.celpoint.chiron"),
            ChartPoints.PersephoneRam => new PointDetails(point, PointCats.Common, CalculationCats.CommonElements, "ref.enum.celpoint.persephone_ram"),
            ChartPoints.HermesRam => new PointDetails(point, PointCats.Common, CalculationCats.CommonElements, "ref.enum.celpoint.hermes_ram"),
            ChartPoints.DemeterRam => new PointDetails(point, PointCats.Common, CalculationCats.CommonElements, "ref.enum.celpoint.demeter_ram"),
            ChartPoints.CupidoUra => new PointDetails(point, PointCats.Common, CalculationCats.CommonSE, "ref.enum.celpoint.cupido_ura"),
            ChartPoints.HadesUra => new PointDetails(point, PointCats.Common, CalculationCats.CommonSE, "ref.enum.celpoint.hades_ura"),
            ChartPoints.ZeusUra => new PointDetails(point, PointCats.Common, CalculationCats.CommonSE, "ref.enum.celpoint.zeus_ura"),
            ChartPoints.KronosUra => new PointDetails(point, PointCats.Common, CalculationCats.CommonSE, "ref.enum.celpoint.kronos_ura"),
            ChartPoints.ApollonUra => new PointDetails(point, PointCats.Common, CalculationCats.CommonSE, "ref.enum.celpoint.apollon_ura"),
            ChartPoints.AdmetosUra => new PointDetails(point, PointCats.Common, CalculationCats.CommonSE, "ref.enum.celpoint.admetos_ura"),
            ChartPoints.VulcanusUra => new PointDetails(point, PointCats.Common, CalculationCats.CommonSE, "ref.enum.celpoint.vulcanus_ura"),
            ChartPoints.PoseidonUra => new PointDetails(point, PointCats.Common, CalculationCats.CommonSE, "ref.enum.celpoint.poseidon_ura"),
            ChartPoints.Eris => new PointDetails(point, PointCats.Common, CalculationCats.CommonSE, "ref.enum.celpoint.eris"),
            ChartPoints.Pholus => new PointDetails(point, PointCats.Common, CalculationCats.CommonSE, "ref.enum.celpoint.pholus"),
            ChartPoints.Ceres => new PointDetails(point, PointCats.Common, CalculationCats.CommonSE, "ref.enum.celpoint.ceres"),
            ChartPoints.Pallas => new PointDetails(point, PointCats.Common, CalculationCats.CommonSE, "ref.enum.celpoint.pallas"),
            ChartPoints.Juno => new PointDetails(point, PointCats.Common, CalculationCats.CommonSE, "ref.enum.celpoint.juno"),
            ChartPoints.Vesta => new PointDetails(point, PointCats.Common, CalculationCats.CommonSE, "ref.enum.celpoint.vesta"),
            ChartPoints.Isis => new PointDetails(point, PointCats.Common, CalculationCats.CommonSE, "ref.enum.celpoint.isis"),
            ChartPoints.Nessus => new PointDetails(point, PointCats.Common, CalculationCats.CommonSE, "ref.enum.celpoint.nessus"),
            ChartPoints.Huya => new PointDetails(point, PointCats.Common, CalculationCats.CommonSE, "ref.enum.celpoint.huya"),
            ChartPoints.Varuna => new PointDetails(point, PointCats.Common, CalculationCats.CommonSE, "ref.enum.celpoint.varuna"),
            ChartPoints.Ixion => new PointDetails(point, PointCats.Common, CalculationCats.CommonSE, "ref.enum.celpoint.ixion"),
            ChartPoints.Quaoar => new PointDetails(point, PointCats.Common, CalculationCats.CommonSE, "ref.enum.celpoint.quaoar"),
            ChartPoints.Haumea => new PointDetails(point, PointCats.Common, CalculationCats.CommonSE, "ref.enum.celpoint.haumea"),
            ChartPoints.Orcus => new PointDetails(point, PointCats.Common, CalculationCats.CommonSE, "ref.enum.celpoint.orcus"),
            ChartPoints.Makemake => new PointDetails(point, PointCats.Common, CalculationCats.CommonSE, "ref.enum.celpoint.makemake"),
            ChartPoints.Sedna => new PointDetails(point, PointCats.Common, CalculationCats.CommonSE, "ref.enum.celpoint.sedna"),
            ChartPoints.Hygieia => new PointDetails(point, PointCats.Common, CalculationCats.CommonSE, "ref.enum.celpoint.hygieia"),
            ChartPoints.Astraea => new PointDetails(point, PointCats.Common, CalculationCats.CommonSE, "ref.enum.celpoint.astraea"),
            ChartPoints.ApogeeMean => new PointDetails(point, PointCats.Common, CalculationCats.CommonSE, "ref.enum.celpoint.apogee_mean"),
            ChartPoints.ApogeeCorrected => new PointDetails(point, PointCats.Common, CalculationCats.CommonSE, "ref.enum.celpoint.apogee_corrected"),
            ChartPoints.ApogeeInterpolated => new PointDetails(point, PointCats.Common, CalculationCats.CommonSE, "ref.enum.celpoint.apogee_interpolated"),
            ChartPoints.ApogeeDuval => new PointDetails(point, PointCats.Common, CalculationCats.CommonFormula, "ref.enum.celpoint.apogee_duval"),
            ChartPoints.PersephoneCarteret => new PointDetails(point, PointCats.Common, CalculationCats.CommonFormula, "ref.enum.celpoint.persephone_carteret"),
            ChartPoints.VulcanusCarteret => new PointDetails(point, PointCats.Common, CalculationCats.CommonFormula, "ref.enum.celpoint.vulcanus_carteret"),

            ChartPoints.Ascendant => new PointDetails(point, PointCats.Angle, CalculationCats.Mundane, "ref.enum.mundanepoint.id.asc"),
            ChartPoints.Mc => new PointDetails(point, PointCats.Angle, CalculationCats.Mundane, "ref.enum.mundanepoint.id.mc"),
            ChartPoints.EastPoint => new PointDetails(point, PointCats.Angle, CalculationCats.Mundane, "ref.enum.mundanepoint.id.eastpoint"),
            ChartPoints.Vertex => new PointDetails(point, PointCats.Angle, CalculationCats.Mundane, "ref.enum.mundanepoint.id.vertex"),

            ChartPoints.Cusp1 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, "ref.enum.cusps.1"),
            ChartPoints.Cusp2 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, "ref.enum.cusps.2"),
            ChartPoints.Cusp3 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, "ref.enum.cusps.3"),
            ChartPoints.Cusp4 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, "ref.enum.cusps.4"),
            ChartPoints.Cusp5 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, "ref.enum.cusps.5"),
            ChartPoints.Cusp6 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, "ref.enum.cusps.6"),
            ChartPoints.Cusp7 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, "ref.enum.cusps.7"),
            ChartPoints.Cusp8 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, "ref.enum.cusps.8"),
            ChartPoints.Cusp9 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, "ref.enum.cusps.9"),
            ChartPoints.Cusp10 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, "ref.enum.cusps.10"),
            ChartPoints.Cusp11 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, "ref.enum.cusps.11"),
            ChartPoints.Cusp12 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, "ref.enum.cusps.12"),
            ChartPoints.Cusp13 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, "ref.enum.cusps.13"),
            ChartPoints.Cusp14 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, "ref.enum.cusps.14"),
            ChartPoints.Cusp15 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, "ref.enum.cusps.15"),
            ChartPoints.Cusp16 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, "ref.enum.cusps.16"),
            ChartPoints.Cusp17 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, "ref.enum.cusps.17"),
            ChartPoints.Cusp18 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, "ref.enum.cusps.18"),
            ChartPoints.Cusp19 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, "ref.enum.cusps.19"),
            ChartPoints.Cusp20 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, "ref.enum.cusps.20"),
            ChartPoints.Cusp21 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, "ref.enum.cusps.21"),
            ChartPoints.Cusp22 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, "ref.enum.cusps.22"),
            ChartPoints.Cusp23 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, "ref.enum.cusps.23"),
            ChartPoints.Cusp24 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, "ref.enum.cusps.24"),
            ChartPoints.Cusp25 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, "ref.enum.cusps.25"),
            ChartPoints.Cusp26 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, "ref.enum.cusps.26"),
            ChartPoints.Cusp27 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, "ref.enum.cusps.27"),
            ChartPoints.Cusp28 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, "ref.enum.cusps.28"),
            ChartPoints.Cusp29 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, "ref.enum.cusps.29"),
            ChartPoints.Cusp30 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, "ref.enum.cusps.30"),
            ChartPoints.Cusp31 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, "ref.enum.cusps.31"),
            ChartPoints.Cusp32 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, "ref.enum.cusps.32"),
            ChartPoints.Cusp33 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, "ref.enum.cusps.33"),
            ChartPoints.Cusp34 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, "ref.enum.cusps.34"),
            ChartPoints.Cusp35 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, "ref.enum.cusps.35"),
            ChartPoints.Cusp36 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, "ref.enum.cusps.36"),

            ChartPoints.ZeroAries => new PointDetails(point, PointCats.Zodiac, CalculationCats.ZodiacFixed, "ref.enum.zodiacpoints.id.zeroar"),

            ChartPoints.FortunaSect => new PointDetails(point, PointCats.Lots, CalculationCats.Lots, "ref.enum.arabicpoint.fortunasect"),
            ChartPoints.FortunaNoSect => new PointDetails(point, PointCats.Lots, CalculationCats.Lots, "ref.enum.arabicpoint.fortunanosect"),

            _ => throw new ArgumentException("Point unknown : " + point.ToString())
        };
    }

    /// <summary>Retrieve details for items in the enum ChartPoints.</summary>
    /// <returns>All details.</returns>
    public static List<PointDetails> AllDetails(this ChartPoints _)
    {
        var allDetails = new List<PointDetails>();
        foreach (ChartPoints currentPoint in Enum.GetValues(typeof(ChartPoints)))
        {
            if (currentPoint != ChartPoints.None)
            {
                allDetails.Add(currentPoint.GetDetails());
            }
        }
        return allDetails;
    }


    /// <summary>Find point for an index.</summary>
    /// <param name="index">Index to look for.</param>
    /// <returns>The point for the index.</returns>
    /// <exception cref="ArgumentException">Is thrown if a non existing index is given.</exception>
    public static ChartPoints PointForIndex(this ChartPoints _, int index)
    {
        foreach (ChartPoints currentPoint in Enum.GetValues(typeof(ChartPoints)))
        {
            if ((int)currentPoint == index) return currentPoint;
        }
        string errorText = "ChartPoints.PointForIndex(): Could not find point for index : " + index;
        Log.Error(errorText);
        throw new ArgumentException(errorText);
    }

}









