// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Serilog;

namespace Enigma.Domain.References;

/// <summary>Supported points.</summary>
/// <remarks>Any point that can be shown in a chart.</remarks>
public enum ChartPoints
{
    Sun = 0, Moon = 1, Mercury = 2, Venus = 3, Earth=4, Mars = 5, Jupiter = 6, Saturn = 7,
    Uranus = 8, Neptune = 9, Pluto = 10,
    MeanNode = 11, TrueNode = 12, Chiron = 13,
    PersephoneRam = 14, HermesRam = 15, DemeterRam = 16,
    CupidoUra = 17, HadesUra = 18, ZeusUra = 19, KronosUra = 20, ApollonUra = 21, AdmetosUra = 22, VulcanusUra = 23, PoseidonUra = 24,
    Eris = 25, Pholus = 26, Ceres = 27, Pallas = 28, Juno = 29, Vesta = 30, Isis = 31, Nessus = 32,
    Huya = 33, Varuna = 34, Ixion = 35, Quaoar = 36, Haumea = 37, Orcus = 38, Makemake = 39, Sedna = 40, Hygieia = 41, Astraea = 42,
    ApogeeMean = 43, ApogeeCorrected = 44, ApogeeInterpolated = 45, ApogeeDuval = 46,
    PersephoneCarteret = 47, VulcanusCarteret = 48, BlackSun = 49, Diamond = 50, PriapusMean = 51, PriapusTrue = 52, Dragon = 53, Beast = 54, 
    MeanSouthNode = 55, TrueSouthNode = 56,
    Ascendant = 1001, Mc = 1002, EastPoint = 1003, Vertex = 1004,
    Cusp1 = 2001, Cusp2 = 2002, Cusp3 = 2003, Cusp4 = 2004, Cusp5 = 2005, Cusp6 = 2006, Cusp7 = 2007, Cusp8 = 2008, Cusp9 = 2009,
    Cusp10 = 2010, Cusp11 = 2011, Cusp12 = 2012, Cusp13 = 2013, Cusp14 = 2014, Cusp15 = 2015, Cusp16 = 2016, Cusp17 = 2017, Cusp18 = 2018,
    Cusp19 = 2019, Cusp20 = 2020, Cusp21 = 2021, Cusp22 = 2022, Cusp23 = 2023, Cusp24 = 2024, Cusp25 = 2025, Cusp26 = 2026, Cusp27 = 2027,
    Cusp28 = 2028, Cusp29 = 2029, Cusp30 = 2030, Cusp31 = 2031, Cusp32 = 2032, Cusp33 = 2033, Cusp34 = 2034, Cusp35 = 2035, Cusp36 = 2036,
    ZeroAries = 3001,
    FortunaSect = 4001, FortunaNoSect = 4002
}

/// <summary>Details for a point</summary>
/// <param name="Point">The point</param>
/// <param name="PointCat">The category for the point</param>
/// <param name="CalculationCat">The category of calculation that is used for this point</param>
/// <param name="Text">Name of point</param>
public record PointDetails(ChartPoints Point, PointCats PointCat, CalculationCats CalculationCat, string Text);


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

            ChartPoints.Sun => new PointDetails(point, PointCats.Common, CalculationCats.CommonSe, "Sun"),
            ChartPoints.Moon => new PointDetails(point, PointCats.Common, CalculationCats.CommonSe, "Moon"),
            ChartPoints.Mercury => new PointDetails(point, PointCats.Common, CalculationCats.CommonSe, "Mercury"),
            ChartPoints.Venus => new PointDetails(point, PointCats.Common, CalculationCats.CommonSe, "Venus"),
            ChartPoints.Earth => new PointDetails(point, PointCats.Common, CalculationCats.CommonSe, "Earth"),
            ChartPoints.Mars => new PointDetails(point, PointCats.Common, CalculationCats.CommonSe, "Mars"),
            ChartPoints.Jupiter => new PointDetails(point, PointCats.Common, CalculationCats.CommonSe, "Jupiter"),
            ChartPoints.Saturn => new PointDetails(point, PointCats.Common, CalculationCats.CommonSe, "Saturn"),
            ChartPoints.Uranus => new PointDetails(point, PointCats.Common, CalculationCats.CommonSe, "Uranus"),
            ChartPoints.Neptune => new PointDetails(point, PointCats.Common, CalculationCats.CommonSe, "Neptune"),
            ChartPoints.Pluto => new PointDetails(point, PointCats.Common, CalculationCats.CommonSe, "Pluto"),
            ChartPoints.MeanNode => new PointDetails(point, PointCats.Common, CalculationCats.CommonSe, "Node-mean"),
            ChartPoints.TrueNode => new PointDetails(point, PointCats.Common, CalculationCats.CommonSe, "Node-true"),
            ChartPoints.Chiron => new PointDetails(point, PointCats.Common, CalculationCats.CommonSe, "Chiron"),
            ChartPoints.PersephoneRam => new PointDetails(point, PointCats.Common, CalculationCats.CommonElements, "Persephone-Ram"),
            ChartPoints.HermesRam => new PointDetails(point, PointCats.Common, CalculationCats.CommonElements, "Hermes-Ram"),
            ChartPoints.DemeterRam => new PointDetails(point, PointCats.Common, CalculationCats.CommonElements, "Demeter-Ram"),
            ChartPoints.CupidoUra => new PointDetails(point, PointCats.Common, CalculationCats.CommonSe, "Cupido-Uranian"),
            ChartPoints.HadesUra => new PointDetails(point, PointCats.Common, CalculationCats.CommonSe, "Hades-Uranian"),
            ChartPoints.ZeusUra => new PointDetails(point, PointCats.Common, CalculationCats.CommonSe, "Zeus-Uranian"),
            ChartPoints.KronosUra => new PointDetails(point, PointCats.Common, CalculationCats.CommonSe, "Kronos-Uranian"),
            ChartPoints.ApollonUra => new PointDetails(point, PointCats.Common, CalculationCats.CommonSe, "Apollon-Uranian"),
            ChartPoints.AdmetosUra => new PointDetails(point, PointCats.Common, CalculationCats.CommonSe, "Admetos-Uranian"),
            ChartPoints.VulcanusUra => new PointDetails(point, PointCats.Common, CalculationCats.CommonSe, "Vulcanus-Uranian"),
            ChartPoints.PoseidonUra => new PointDetails(point, PointCats.Common, CalculationCats.CommonSe, "Poseidon-Uranian"),
            ChartPoints.Eris => new PointDetails(point, PointCats.Common, CalculationCats.CommonSe, "Eris"),
            ChartPoints.Pholus => new PointDetails(point, PointCats.Common, CalculationCats.CommonSe, "Pholus"),
            ChartPoints.Ceres => new PointDetails(point, PointCats.Common, CalculationCats.CommonSe, "Ceres"),
            ChartPoints.Pallas => new PointDetails(point, PointCats.Common, CalculationCats.CommonSe, "Pallas"),
            ChartPoints.Juno => new PointDetails(point, PointCats.Common, CalculationCats.CommonSe, "Juno"),
            ChartPoints.Vesta => new PointDetails(point, PointCats.Common, CalculationCats.CommonSe, "Vesta"),
            ChartPoints.Isis => new PointDetails(point, PointCats.Common, CalculationCats.CommonSe, "Isis-Transpluto"),
            ChartPoints.Nessus => new PointDetails(point, PointCats.Common, CalculationCats.CommonSe, "Nessus"),
            ChartPoints.Huya => new PointDetails(point, PointCats.Common, CalculationCats.CommonSe, "Huya"),
            ChartPoints.Varuna => new PointDetails(point, PointCats.Common, CalculationCats.CommonSe, "Varuna"),
            ChartPoints.Ixion => new PointDetails(point, PointCats.Common, CalculationCats.CommonSe, "Ixion"),
            ChartPoints.Quaoar => new PointDetails(point, PointCats.Common, CalculationCats.CommonSe, "Quaoar"),
            ChartPoints.Haumea => new PointDetails(point, PointCats.Common, CalculationCats.CommonSe, "Haumea"),
            ChartPoints.Orcus => new PointDetails(point, PointCats.Common, CalculationCats.CommonSe, "Orcus"),
            ChartPoints.Makemake => new PointDetails(point, PointCats.Common, CalculationCats.CommonSe, "Makemake"),
            ChartPoints.Sedna => new PointDetails(point, PointCats.Common, CalculationCats.CommonSe, "Sedna"),
            ChartPoints.Hygieia => new PointDetails(point, PointCats.Common, CalculationCats.CommonSe, "Hygieia"),
            ChartPoints.Astraea => new PointDetails(point, PointCats.Common, CalculationCats.CommonSe, "Astraea"),
            ChartPoints.ApogeeMean => new PointDetails(point, PointCats.Common, CalculationCats.CommonSe, "Apogee-mean"),
            ChartPoints.ApogeeCorrected => new PointDetails(point, PointCats.Common, CalculationCats.CommonSe, "Apogee-corrected"),
            ChartPoints.ApogeeInterpolated => new PointDetails(point, PointCats.Common, CalculationCats.CommonSe, "Apogee-interpolated"),
            ChartPoints.ApogeeDuval => new PointDetails(point, PointCats.Common, CalculationCats.CommonFormula, "Apogee-Duval"),
            ChartPoints.PersephoneCarteret => new PointDetails(point, PointCats.Common, CalculationCats.CommonFormula, "Persephone-Carteret"),
            ChartPoints.VulcanusCarteret => new PointDetails(point, PointCats.Common, CalculationCats.CommonFormula, "Vulcanus-Carteret"),
            ChartPoints.BlackSun => new PointDetails(point, PointCats.Common, CalculationCats.Apsides, "Black Sun (aphelion)"),
            ChartPoints.Diamond => new PointDetails(point, PointCats.Common, CalculationCats.Apsides, "Diamond (perihelion)"),
            ChartPoints.PriapusMean => new PointDetails(point, PointCats.Common, CalculationCats.CommonFormula, "Priapus (perigee)"),
            ChartPoints.PriapusTrue => new PointDetails(point, PointCats.Common, CalculationCats.CommonFormula, "Priapus (perigee, oscillating)"),
            ChartPoints.Dragon => new PointDetails(point, PointCats.Common, CalculationCats.CommonFormula, "Dragon"),
            ChartPoints.Beast => new PointDetails(point, PointCats.Common, CalculationCats.CommonFormula, "Beast"),            
            ChartPoints.MeanSouthNode => new PointDetails(point, PointCats.Common, CalculationCats.CommonFormula, "South node - mean"),
            ChartPoints.TrueSouthNode => new PointDetails(point, PointCats.Common, CalculationCats.CommonFormula, "South node - true"),
            
            ChartPoints.Ascendant => new PointDetails(point, PointCats.Angle, CalculationCats.Mundane, "Ascendant"),
            ChartPoints.Mc => new PointDetails(point, PointCats.Angle, CalculationCats.Mundane, "MC"),
            ChartPoints.EastPoint => new PointDetails(point, PointCats.Angle, CalculationCats.Mundane, "Eastpoint"),
            ChartPoints.Vertex => new PointDetails(point, PointCats.Angle, CalculationCats.Mundane, "Vertex"),

            ChartPoints.Cusp1 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, "Cusp-1"),
            ChartPoints.Cusp2 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, "Cusp-2"),
            ChartPoints.Cusp3 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, "Cusp-3"),
            ChartPoints.Cusp4 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, "Cusp-4"),
            ChartPoints.Cusp5 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, "Cusp-5"),
            ChartPoints.Cusp6 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, "Cusp-6"),
            ChartPoints.Cusp7 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, "Cusp-7"),
            ChartPoints.Cusp8 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, "Cusp-8"),
            ChartPoints.Cusp9 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, "Cusp-9"),
            ChartPoints.Cusp10 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, "Cusp-10"),
            ChartPoints.Cusp11 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, "Cusp-11"),
            ChartPoints.Cusp12 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, "Cusp-12"),
            ChartPoints.Cusp13 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, "Cusp-13"),
            ChartPoints.Cusp14 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, "Cusp-14"),
            ChartPoints.Cusp15 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, "Cusp-15"),
            ChartPoints.Cusp16 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, "Cusp-16"),
            ChartPoints.Cusp17 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, "Cusp-17"),
            ChartPoints.Cusp18 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, "Cusp-18"),
            ChartPoints.Cusp19 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, "Cusp-19"),
            ChartPoints.Cusp20 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, "Cusp-20"),
            ChartPoints.Cusp21 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, "Cusp-21"),
            ChartPoints.Cusp22 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, "Cusp-22"),
            ChartPoints.Cusp23 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, "Cusp-23"),
            ChartPoints.Cusp24 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, "Cusp-24"),
            ChartPoints.Cusp25 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, "Cusp-25"),
            ChartPoints.Cusp26 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, "Cusp-26"),
            ChartPoints.Cusp27 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, "Cusp-27"),
            ChartPoints.Cusp28 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, "Cusp-28"),
            ChartPoints.Cusp29 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, "Cusp-29"),
            ChartPoints.Cusp30 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, "Cusp-30"),
            ChartPoints.Cusp31 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, "Cusp-31"),
            ChartPoints.Cusp32 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, "Cusp-32"),
            ChartPoints.Cusp33 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, "Cusp-33"),
            ChartPoints.Cusp34 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, "Cusp-34"),
            ChartPoints.Cusp35 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, "Cusp-35"),
            ChartPoints.Cusp36 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, "Cusp-36"),

            ChartPoints.ZeroAries => new PointDetails(point, PointCats.Zodiac, CalculationCats.ZodiacFixed, "Zero-Aries"),

            ChartPoints.FortunaSect => new PointDetails(point, PointCats.Lots, CalculationCats.Lots, "Pars-sect"),
            ChartPoints.FortunaNoSect => new PointDetails(point, PointCats.Lots, CalculationCats.Lots, "Pars-no-sect)"),

            _ => throw new ArgumentException("Point unknown : " + point)
        };
    }

    /// <summary>Retrieve details for items in the enum ChartPoints.</summary>
    /// <returns>All details.</returns>
    public static List<PointDetails> AllDetails()
    {
        return (from ChartPoints currentPoint in Enum.GetValues(typeof(ChartPoints))
            select currentPoint.GetDetails()).ToList();
    }


    /// <summary>Find point for an index.</summary>
    /// <param name="index">Index to look for.</param>
    /// <returns>The point for the index.</returns>
    /// <exception cref="ArgumentException">Is thrown if a non existing index is given.</exception>
    public static ChartPoints PointForIndex(int index)
    {
        foreach (ChartPoints currentPoint in Enum.GetValues(typeof(ChartPoints)))
        {
            if ((int)currentPoint == index) return currentPoint;
        }
        Log.Error("ChartPoints.PointForIndex(): Could not find point for index : {Index}", index);
        throw new ArgumentException("Wrong index for ChartPoints");
    }

}









