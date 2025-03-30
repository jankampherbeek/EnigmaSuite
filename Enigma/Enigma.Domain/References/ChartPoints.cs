// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023, 2025.
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
    NorthNode = 11, TrueNode = 12, Chiron = 13,
    PersephoneRam = 14, HermesRam = 15, DemeterRam = 16,
    CupidoUra = 17, HadesUra = 18, ZeusUra = 19, KronosUra = 20, ApollonUra = 21, AdmetosUra = 22, VulcanusUra = 23, PoseidonUra = 24,
    Eris = 25, Pholus = 26, Ceres = 27, Pallas = 28, Juno = 29, Vesta = 30, Isis = 31, Nessus = 32,
    Huya = 33, Varuna = 34, Ixion = 35, Quaoar = 36, Haumea = 37, Orcus = 38, Makemake = 39, Sedna = 40, Hygieia = 41, Astraea = 42,
    ApogeeMean = 43, ApogeeCorrected = 44, ApogeeInterpolated = 45, 
    PersephoneCarteret = 47, VulcanusCarteret = 48, PerigeeInterpolated = 49,
    Priapus = 50, PriapusCorrected = 51, Dragon = 52, Beast = 53, SouthNode = 54, BlackSun = 55, Diamond = 56,
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
/// <param name="CalcId">Calculation ID, same as SEId for Se calculated points.</param>
/// <param name="Text">Name of point</param>
public record PointDetails(ChartPoints Point, PointCats PointCat, CalculationCats CalculationCat, int CalcId, string Text);


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
            // CommonSe
            ChartPoints.Sun => new PointDetails(point, PointCats.Common, CalculationCats.CommonSe, 0, "Sun"),
            ChartPoints.Moon => new PointDetails(point, PointCats.Common, CalculationCats.CommonSe, 1,"Moon"),
            ChartPoints.Mercury => new PointDetails(point, PointCats.Common, CalculationCats.CommonSe, 2,"Mercury"),
            ChartPoints.Venus => new PointDetails(point, PointCats.Common, CalculationCats.CommonSe, 3,"Venus"),
            ChartPoints.Mars => new PointDetails(point, PointCats.Common, CalculationCats.CommonSe, 4,"Mars"),
            ChartPoints.Jupiter => new PointDetails(point, PointCats.Common, CalculationCats.CommonSe, 5,"Jupiter"),
            ChartPoints.Saturn => new PointDetails(point, PointCats.Common, CalculationCats.CommonSe, 6,"Saturn"),
            ChartPoints.Uranus => new PointDetails(point, PointCats.Common, CalculationCats.CommonSe, 7,"Uranus"),
            ChartPoints.Neptune => new PointDetails(point, PointCats.Common, CalculationCats.CommonSe, 8,"Neptune"),
            ChartPoints.Pluto => new PointDetails(point, PointCats.Common, CalculationCats.CommonSe, 9, "Pluto"),
            ChartPoints.NorthNode => new PointDetails(point, PointCats.Common, CalculationCats.CommonSe, 10, "North node"),
            ChartPoints.TrueNode => new PointDetails(point, PointCats.Common, CalculationCats.CommonSe, 11, "North node"),
            ChartPoints.ApogeeMean => new PointDetails(point, PointCats.Common, CalculationCats.CommonSe, 12,"Apogee-mean"),
            // TODO add variable type for CalculationCats to Apogeecorrected
            ChartPoints.ApogeeCorrected => new PointDetails(point, PointCats.Common, CalculationCats.CommonSe, 13,"Apogee-corrected"),
            ChartPoints.Earth => new PointDetails(point, PointCats.Common, CalculationCats.CommonSe, 14,"Earth"),            
            ChartPoints.Chiron => new PointDetails(point, PointCats.Common, CalculationCats.CommonSe, 15,"Chiron"),            
            ChartPoints.Pholus => new PointDetails(point, PointCats.Common, CalculationCats.CommonSe, 16,"Pholus"),
            ChartPoints.Ceres => new PointDetails(point, PointCats.Common, CalculationCats.CommonSe, 17,"Ceres"),
            ChartPoints.Pallas => new PointDetails(point, PointCats.Common, CalculationCats.CommonSe, 18,"Pallas"),
            ChartPoints.Juno => new PointDetails(point, PointCats.Common, CalculationCats.CommonSe, 19,"Juno"),
            ChartPoints.Vesta => new PointDetails(point, PointCats.Common, CalculationCats.CommonSe, 20,"Vesta"),
            ChartPoints.ApogeeInterpolated => new PointDetails(point, PointCats.Common, CalculationCats.CommonSe, 21, "Apogee-interpolated"),
            ChartPoints.PerigeeInterpolated => new PointDetails(point, PointCats.Common, CalculationCats.CommonSe, 22, "Perigee-interpolated"),
            ChartPoints.CupidoUra => new PointDetails(point, PointCats.Common, CalculationCats.CommonSe, 40, "Cupido-Uranian"),
            ChartPoints.HadesUra => new PointDetails(point, PointCats.Common, CalculationCats.CommonSe, 41, "Hades-Uranian"),
            ChartPoints.ZeusUra => new PointDetails(point, PointCats.Common, CalculationCats.CommonSe, 42, "Zeus-Uranian"),
            ChartPoints.KronosUra => new PointDetails(point, PointCats.Common, CalculationCats.CommonSe, 43, "Kronos-Uranian"),
            ChartPoints.ApollonUra => new PointDetails(point, PointCats.Common, CalculationCats.CommonSe, 44, "Apollon-Uranian"),
            ChartPoints.AdmetosUra => new PointDetails(point, PointCats.Common, CalculationCats.CommonSe, 45, "Admetos-Uranian"),
            ChartPoints.VulcanusUra => new PointDetails(point, PointCats.Common, CalculationCats.CommonSe, 46,"Vulcanus-Uranian"),
            ChartPoints.PoseidonUra => new PointDetails(point, PointCats.Common, CalculationCats.CommonSe, 47,"Poseidon-Uranian"),
            ChartPoints.Isis => new PointDetails(point, PointCats.Common, CalculationCats.CommonSe, 48,"Isis-Transpluto"),
            ChartPoints.Eris => new PointDetails(point, PointCats.Common, CalculationCats.CommonSe, 1009001,"Eris"),
            ChartPoints.Nessus => new PointDetails(point, PointCats.Common, CalculationCats.CommonSe, 17066,"Nessus"),
            ChartPoints.Huya => new PointDetails(point, PointCats.Common, CalculationCats.CommonSe, 48628,"Huya"),
            ChartPoints.Varuna => new PointDetails(point, PointCats.Common, CalculationCats.CommonSe, 30000,"Varuna"),
            ChartPoints.Ixion => new PointDetails(point, PointCats.Common, CalculationCats.CommonSe, 38978, "Ixion"),
            ChartPoints.Quaoar => new PointDetails(point, PointCats.Common, CalculationCats.CommonSe, 60000,"Quaoar"),
            ChartPoints.Haumea => new PointDetails(point, PointCats.Common, CalculationCats.CommonSe, 146108,"Haumea"),
            ChartPoints.Orcus => new PointDetails(point, PointCats.Common, CalculationCats.CommonSe, 100482,"Orcus"),
            ChartPoints.Makemake => new PointDetails(point, PointCats.Common, CalculationCats.CommonSe, 146472,"Makemake"),
            ChartPoints.Sedna => new PointDetails(point, PointCats.Common, CalculationCats.CommonSe, 100377, "Sedna"),
            ChartPoints.Hygieia => new PointDetails(point, PointCats.Common, CalculationCats.CommonSe,10010, "Hygieia"),
            ChartPoints.Astraea => new PointDetails(point, PointCats.Common, CalculationCats.CommonSe, 10005,"Astraea"),
            // CommonElements
            ChartPoints.PersephoneRam => new PointDetails(point, PointCats.Common, CalculationCats.CommonElements, 300,"Persephone-Ram"),
            ChartPoints.HermesRam => new PointDetails(point, PointCats.Common, CalculationCats.CommonElements, 301,"Hermes-Ram"),
            ChartPoints.DemeterRam => new PointDetails(point, PointCats.Common, CalculationCats.CommonElements, 302,"Demeter-Ram"),
            // CommonFormulaLongitude
            ChartPoints.PersephoneCarteret => new PointDetails(point, PointCats.Common, CalculationCats.CommonFormulaLongitude, 400, "Persephone-Carteret"),
            ChartPoints.VulcanusCarteret => new PointDetails(point, PointCats.Common, CalculationCats.CommonFormulaLongitude, 401,"Vulcanus-Carteret"),
   //         ChartPoints.ApogeeDuval => new PointDetails(point, PointCats.Common, CalculationCats.CommonFormulaLongitude, 402, "Apogee Duval"),
            // CommonFormulaFull
            ChartPoints.Priapus => new PointDetails(point, PointCats.Common, CalculationCats.CommonFormulaFull, 501,"Priapus (perigee)"),
            ChartPoints.PriapusCorrected => new PointDetails(point, PointCats.Common, CalculationCats.CommonFormulaFull, 502, "Priapus (corrected perigee)"),
            ChartPoints.Dragon => new PointDetails(point, PointCats.Common, CalculationCats.CommonFormulaFull, 503,"Dragon"),
            ChartPoints.Beast => new PointDetails(point, PointCats.Common, CalculationCats.CommonFormulaFull, 504,"Beast"),  
            ChartPoints.SouthNode => new PointDetails(point, PointCats.Common, CalculationCats.CommonFormulaFull, 505, "South node"),
            // CommonApsides
            ChartPoints.BlackSun => new PointDetails(point, PointCats.Common, CalculationCats.Apsides, 601,"Black Sun (aphelion)"),
            ChartPoints.Diamond => new PointDetails(point, PointCats.Common, CalculationCats.Apsides, 602,"Diamond (perihelion)"),
            // Mundane
            ChartPoints.Mc => new PointDetails(point, PointCats.Angle, CalculationCats.Mundane, 700,"MC"),
            ChartPoints.Ascendant => new PointDetails(point, PointCats.Angle, CalculationCats.Mundane, 701, "Ascendant"),
            ChartPoints.EastPoint => new PointDetails(point, PointCats.Angle, CalculationCats.Mundane, 702,"Eastpoint"),
            ChartPoints.Vertex => new PointDetails(point, PointCats.Angle, CalculationCats.Mundane, 703,"Vertex"),
            ChartPoints.Cusp1 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, 1,"Cusp-1"),
            ChartPoints.Cusp2 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, 2,"Cusp-2"),
            ChartPoints.Cusp3 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, 3,"Cusp-3"),
            ChartPoints.Cusp4 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, 4,"Cusp-4"),
            ChartPoints.Cusp5 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, 5,"Cusp-5"),
            ChartPoints.Cusp6 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, 6,"Cusp-6"),
            ChartPoints.Cusp7 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, 7,"Cusp-7"),
            ChartPoints.Cusp8 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, 8,"Cusp-8"),
            ChartPoints.Cusp9 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, 9,"Cusp-9"),
            ChartPoints.Cusp10 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, 10,"Cusp-10"),
            ChartPoints.Cusp11 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, 11,"Cusp-11"),
            ChartPoints.Cusp12 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, 12,"Cusp-12"),
            ChartPoints.Cusp13 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, 13,"Cusp-13"),
            ChartPoints.Cusp14 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, 14,"Cusp-14"),
            ChartPoints.Cusp15 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, 15,"Cusp-15"),
            ChartPoints.Cusp16 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, 16,"Cusp-16"),
            ChartPoints.Cusp17 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, 17,"Cusp-17"),
            ChartPoints.Cusp18 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, 18,"Cusp-18"),
            ChartPoints.Cusp19 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, 19,"Cusp-19"),
            ChartPoints.Cusp20 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, 20,"Cusp-20"),
            ChartPoints.Cusp21 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, 21, "Cusp-21"),
            ChartPoints.Cusp22 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, 22,"Cusp-22"),
            ChartPoints.Cusp23 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, 23,"Cusp-23"),
            ChartPoints.Cusp24 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, 24,"Cusp-24"),
            ChartPoints.Cusp25 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, 25,"Cusp-25"),
            ChartPoints.Cusp26 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, 26,"Cusp-26"),
            ChartPoints.Cusp27 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, 27,"Cusp-27"),
            ChartPoints.Cusp28 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, 28, "Cusp-28"),
            ChartPoints.Cusp29 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, 29,"Cusp-29"),
            ChartPoints.Cusp30 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, 30,"Cusp-30"),
            ChartPoints.Cusp31 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, 31,"Cusp-31"),
            ChartPoints.Cusp32 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, 32,"Cusp-32"),
            ChartPoints.Cusp33 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, 33,"Cusp-33"),
            ChartPoints.Cusp34 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, 34,"Cusp-34"),
            ChartPoints.Cusp35 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, 35,"Cusp-35"),
            ChartPoints.Cusp36 => new PointDetails(point, PointCats.Cusp, CalculationCats.Mundane, 36,"Cusp-36"),
            // ZodiacFixed
            ChartPoints.ZeroAries => new PointDetails(point, PointCats.Zodiac, CalculationCats.ZodiacFixed, 800,"Zero-Aries"),
            // Lots
            ChartPoints.FortunaSect => new PointDetails(point, PointCats.Lots, CalculationCats.Lots, 900,"Pars-sect"),
            ChartPoints.FortunaNoSect => new PointDetails(point, PointCats.Lots, CalculationCats.Lots, 901,"Pars-no-sect)"),

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


    /// <summary>Find point for a caclulation index.</summary>
    /// <param name="calcCat">Calcualtion category</param>
    /// <param name="calcId">Calculation index to look for.</param>
    /// <returns>The point for the index.</returns>
    /// <exception cref="ArgumentException">Is thrown if a non existing index is given.</exception>
    public static ChartPoints PointForIndex(CalculationCats calcCat, int calcId)
    {
        foreach (ChartPoints currentPoint in Enum.GetValues(typeof(ChartPoints)))
        {
            if (currentPoint.GetDetails().CalcId == calcId && currentPoint.GetDetails().CalculationCat == calcCat) return currentPoint;
        }
        Log.Error("ChartPoints.PointForIndex(): Could not find point for calculation category {Cat} and index : {Index}", calcCat, calcId);
        throw new ArgumentException("Wrong index for ChartPoints");
    }


    
}









