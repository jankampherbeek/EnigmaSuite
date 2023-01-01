// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.AstronCalculations;
using Enigma.Domain.Constants;

namespace Enigma.Domain.Points;

/// <summary>Supported celestial points (Planets, lights, Plutoids etc.).</summary>
public enum CelPoints
{
    Sun = 0, Moon = 1, Mercury = 2, Venus = 3, Earth = 4, Mars = 5, Jupiter = 6, Saturn = 7, Uranus = 8, Neptune = 9,
    Pluto = 10, MeanNode = 11, TrueNode = 12, Chiron = 13, PersephoneRam = 14, HermesRam = 15, DemeterRam = 16, CupidoUra = 17, HadesUra = 18, ZeusUra = 19,
    KronosUra = 20, ApollonUra = 21, AdmetosUra = 22, VulcanusUra = 23, PoseidonUra = 24, Eris = 25, Pholus = 26, Ceres = 27, Pallas = 28, Juno = 29,
    Vesta = 30, Isis = 31, Nessus = 32, Huya = 33, Varuna = 34, Ixion = 35, Quaoar = 36, Haumea = 37, Orcus = 38, Makemake = 39,
    Sedna = 40, Hygieia = 41, Astraea = 42, ApogeeMean = 43, ApogeeCorrected = 44, ApogeeInterpolated = 45, ApogeeDuval = 46,
    PersephoneCarteret = 47, VulcanusCarteret = 48
}

public static class CelpointssExtensions
{
    /// <summary>Retrieve details for a celestial point.</summary>
    /// <param name="point">The celestial point, is automatically filled.</param>
    /// <returns>Details for the celestial point.</returns>
    public static CelPointDetails GetDetails(this CelPoints point)
    {
        return point switch
        {
            /*
             * TODO add celestial Points:
             * Black Moon Mean       12 Mean apogee
             * Black Moon corrected  13 Oscu apogee
             * ---> Interpolated Apogee   21
             * ---> Interpolated Perigee 22
             * Black Moon Duval                   --> 105
             * Zero Aries                         --> 106
             * ParsFortuna Sect                   --> 107
             * ParsFortuna NoSect                 --> 108
             * Vulcanus Carteret   (103)
             * Persephone Carteret (104)
             * Persephone/hermes/Demeter - Ram new nrs: 100, 101, 102
             * 0 Ram : 105
             * 
             * 
             * add to Arabic points and zodiacal points: ZeroAries = 47, ParsFortunaNoSect = 48, ParsFortunaSect = 49,
             *             CelPoints.ZeroAries => new CelPointDetails(point, CelPointCats.MathPoint, CalculationTypes.Numeric, EnigmaConstants.NON_SE_ZEROARIES, false, true, "zero_aries", "1"),
            CelPoints.ParsFortunaSect => new CelPointDetails(point, CelPointCats.MathPoint, CalculationTypes.Numeric, EnigmaConstants.NON_SE_PARS_FORTUNA_SECT, false, true, "pars_fortuna_sect", "e"),
            CelPoints.ParsFortunaNoSect => new CelPointDetails(point, CelPointCats.MathPoint, CalculationTypes.Numeric, EnigmaConstants.NON_SE_PARS_FORTUNA_NOSECT, false, true, "pars_fortuna_nosect", "e"),

             */
            CelPoints.Sun => new CelPointDetails(point, CelPointCats.Classic, CalculationTypes.SE, EnigmaConstants.SE_SUN, false, true, "sun", "a"),
            CelPoints.Moon => new CelPointDetails(point, CelPointCats.Classic, CalculationTypes.SE, EnigmaConstants.SE_MOON, false, true, "moon", "b"),
            CelPoints.Mercury => new CelPointDetails(point, CelPointCats.Classic, CalculationTypes.SE, EnigmaConstants.SE_MERCURY, true, true, "mercury", "c"),
            CelPoints.Venus => new CelPointDetails(point, CelPointCats.Classic, CalculationTypes.SE, EnigmaConstants.SE_VENUS, true, true, "venus", "d"),
            CelPoints.Earth => new CelPointDetails(point, CelPointCats.Classic, CalculationTypes.SE, EnigmaConstants.SE_EARTH, true, false, "earth", "e"),
            CelPoints.Mars => new CelPointDetails(point, CelPointCats.Classic, CalculationTypes.SE, EnigmaConstants.SE_MARS, true, true, "mars", "f"),
            CelPoints.Jupiter => new CelPointDetails(point, CelPointCats.Classic, CalculationTypes.SE, EnigmaConstants.SE_JUPITER, true, true, "jupiter", "g"),
            CelPoints.Saturn => new CelPointDetails(point, CelPointCats.Classic, CalculationTypes.SE, EnigmaConstants.SE_SATURN, true, true, "saturn", "h"),
            CelPoints.Uranus => new CelPointDetails(point, CelPointCats.Modern, CalculationTypes.SE, EnigmaConstants.SE_URANUS, true, true, "uranus", "i"),
            CelPoints.Neptune => new CelPointDetails(point, CelPointCats.Modern, CalculationTypes.SE, EnigmaConstants.SE_NEPTUNE, true, true, "neptune", "j"),
            CelPoints.Pluto => new CelPointDetails(point, CelPointCats.Modern, CalculationTypes.SE, EnigmaConstants.SE_PLUTO, true, true, "pluto", "k"),
            CelPoints.MeanNode => new CelPointDetails(point, CelPointCats.MathPoint, CalculationTypes.SE, EnigmaConstants.SE_MEAN_NODE, false, true, "meannode", "{"),
            CelPoints.TrueNode => new CelPointDetails(point, CelPointCats.MathPoint, CalculationTypes.SE, EnigmaConstants.SE_TRUE_NODE, false, true, "truenode", "{"),
            CelPoints.Chiron => new CelPointDetails(point, CelPointCats.Minor, CalculationTypes.SE, EnigmaConstants.SE_CHIRON, true, true, "chiron", "w"),
            CelPoints.PersephoneRam => new CelPointDetails(point, CelPointCats.Hypothetical, CalculationTypes.Elements, EnigmaConstants.SE_PERSEPHONE_RAM, true, true, "persephone_ram", "/"),
            CelPoints.HermesRam => new CelPointDetails(point, CelPointCats.Hypothetical, CalculationTypes.Elements, EnigmaConstants.SE_HERMES_RAM, true, true, "hermes_ram", "<"),
            CelPoints.DemeterRam => new CelPointDetails(point, CelPointCats.Hypothetical, CalculationTypes.Elements, EnigmaConstants.SE_DEMETER_RAM, true, true, "demeter_ram", ">"),
            CelPoints.CupidoUra => new CelPointDetails(point, CelPointCats.Hypothetical, CalculationTypes.SE, EnigmaConstants.SE_CUPIDO_URA, true, true, "cupido_ura", "y"),
            CelPoints.HadesUra => new CelPointDetails(point, CelPointCats.Hypothetical, CalculationTypes.SE, EnigmaConstants.SE_HADES_URA, true, true, "hades_ura", "z"),
            CelPoints.ZeusUra => new CelPointDetails(point, CelPointCats.Hypothetical, CalculationTypes.SE, EnigmaConstants.SE_ZEUS_URA, true, true, "zeus_ura", "!"),
            CelPoints.KronosUra => new CelPointDetails(point, CelPointCats.Hypothetical, CalculationTypes.SE, EnigmaConstants.SE_KRONOS_URA, true, true, "kronos_ura", "@"),
            CelPoints.ApollonUra => new CelPointDetails(point, CelPointCats.Hypothetical, CalculationTypes.SE, EnigmaConstants.SE_APOLLON_URA, true, true, "apollon_ura", "#"),
            CelPoints.AdmetosUra => new CelPointDetails(point, CelPointCats.Hypothetical, CalculationTypes.SE, EnigmaConstants.SE_ADMETOS_URA, true, true, "admetos_ura", "$"),
            CelPoints.VulcanusUra => new CelPointDetails(point, CelPointCats.Hypothetical, CalculationTypes.SE, EnigmaConstants.SE_VULCANUS_URA, true, true, "vulcanus_ura", "%"),
            CelPoints.PoseidonUra => new CelPointDetails(point, CelPointCats.Hypothetical, CalculationTypes.SE, EnigmaConstants.SE_POSEIDON_URA, true, true, "poseidon_ura", "&"),
            CelPoints.Eris => new CelPointDetails(point, CelPointCats.Minor, CalculationTypes.SE, EnigmaConstants.SE_ERIS, true, true, "eris", "*"),
            CelPoints.Pholus => new CelPointDetails(point, CelPointCats.Minor, CalculationTypes.SE, EnigmaConstants.SE_PHOLUS, true, true, "pholus", ")"),
            CelPoints.Ceres => new CelPointDetails(point, CelPointCats.Minor, CalculationTypes.SE, EnigmaConstants.SE_CERES, true, true, "ceres", "_"),
            CelPoints.Pallas => new CelPointDetails(point, CelPointCats.Minor, CalculationTypes.SE, EnigmaConstants.SE_PALLAS, true, true, "pallas", "û"),
            CelPoints.Juno => new CelPointDetails(point, CelPointCats.Minor, CalculationTypes.SE, EnigmaConstants.SE_JUNO, true, true, "juno", "ü"),
            CelPoints.Vesta => new CelPointDetails(point, CelPointCats.Minor, CalculationTypes.SE, EnigmaConstants.SE_VESTA, true, true, "vesta", "À"),
            CelPoints.Isis => new CelPointDetails(point, CelPointCats.Hypothetical, CalculationTypes.SE, EnigmaConstants.SE_ISIS, true, true, "isis", "â"),
            CelPoints.Nessus => new CelPointDetails(point, CelPointCats.Minor, CalculationTypes.SE, EnigmaConstants.SE_NESSUS, true, true, "nessus", "("),
            CelPoints.Huya => new CelPointDetails(point, CelPointCats.Minor, CalculationTypes.SE, EnigmaConstants.SE_HUYA, true, true, "huya", "ï"),
            CelPoints.Varuna => new CelPointDetails(point, CelPointCats.Minor, CalculationTypes.SE, EnigmaConstants.SE_VARUNA, true, true, "varuna", "ò"),
            CelPoints.Ixion => new CelPointDetails(point, CelPointCats.Minor, CalculationTypes.SE, EnigmaConstants.SE_IXION, true, true, "ixion", "ó"),
            CelPoints.Quaoar => new CelPointDetails(point, CelPointCats.Minor, CalculationTypes.SE, EnigmaConstants.SE_QUAOAR, true, true, "quaoar", "ô"),
            CelPoints.Haumea => new CelPointDetails(point, CelPointCats.Minor, CalculationTypes.SE, EnigmaConstants.SE_HAUMEA, true, true, "haumea", "í"),
            CelPoints.Orcus => new CelPointDetails(point, CelPointCats.Minor, CalculationTypes.SE, EnigmaConstants.SE_ORCUS, true, true, "orcus", "ù"),
            CelPoints.Makemake => new CelPointDetails(point, CelPointCats.Minor, CalculationTypes.SE, EnigmaConstants.SE_MAKEMAKE, true, true, "makemake", "î"),
            CelPoints.Sedna => new CelPointDetails(point, CelPointCats.Minor, CalculationTypes.SE, EnigmaConstants.SE_SEDNA, true, true, "sedna", "ö"),
            CelPoints.Hygieia => new CelPointDetails(point, CelPointCats.Minor, CalculationTypes.SE, EnigmaConstants.SE_HYGIEIA, true, true, "hygieia", "Á"),
            CelPoints.Astraea => new CelPointDetails(point, CelPointCats.Minor, CalculationTypes.SE, EnigmaConstants.SE_ASTRAEA, true, true, "astraea", "Ã"),
            CelPoints.ApogeeMean => new CelPointDetails(point, CelPointCats.MathPoint, CalculationTypes.SE, EnigmaConstants.SE_MEAN_APOGEE, false, true, "apogee_mean", ","),
            CelPoints.ApogeeCorrected => new CelPointDetails(point, CelPointCats.MathPoint, CalculationTypes.SE, EnigmaConstants.SE_OSCU_APOG, false, true, "apogee_corrected", "."),
            CelPoints.ApogeeInterpolated => new CelPointDetails(point, CelPointCats.MathPoint, CalculationTypes.SE, EnigmaConstants.SE_INTP_APOG, false, true, "apogee_interpolated", "."),
            CelPoints.ApogeeDuval => new CelPointDetails(point, CelPointCats.MathPoint, CalculationTypes.Numeric, EnigmaConstants.NON_SE_DUVAL_APOGEE, false, true, "apogee_duval", "."),
            CelPoints.PersephoneCarteret => new CelPointDetails(point, CelPointCats.Hypothetical, CalculationTypes.Numeric, EnigmaConstants.NON_SE_PERSEPHONE_CARTERET, false, true, "persephone_carteret", "à"),
            CelPoints.VulcanusCarteret => new CelPointDetails(point, CelPointCats.Hypothetical, CalculationTypes.Numeric, EnigmaConstants.NON_SE_VULCANUS_CARTERET, false, true, "vulcanus_carteret", "Ï"),

            _ => throw new ArgumentException("CelPoint unknown : " + point.ToString())
        };
    }

    /// <summary>Retrieve details for items in the enum CelPoints.</summary>
    /// <param name="celPoint">The celestial point, is automatically filled.</param>
    /// <returns>All details.</returns>
    public static List<CelPointDetails> AllDetails(this CelPoints celPoint)
    {
        var allDetails = new List<CelPointDetails>();
        foreach (CelPoints currentPoint in Enum.GetValues(typeof(CelPoints)))
        {
            allDetails.Add(currentPoint.GetDetails());
        }
        return allDetails;
    }


    /// <summary>Find celestial point for an index.</summary>
    /// <param name="celPoint">Any celestial point, automatically filled.</param>
    /// <param name="index">Index to look for.</param>
    /// <returns>The celestial point for the index.</returns>
    /// <exception cref="ArgumentException">Is thrown if a non existing index is given.</exception>
    public static CelPoints CelestialPointForIndex(this CelPoints celPoint, int index)
    {
        foreach (CelPoints currentPoint in Enum.GetValues(typeof(CelPoints)))
        {
            if ((int)currentPoint == index) return currentPoint;
        }
        throw new ArgumentException("Could not find celestial point for index : " + index);
    }

}


/// <summary>Details for a celestial point.</summary>
/// <param name="celPoint">The celestial point.</param>
/// <param name="celPointCat">The category for the celestial point.</param>
/// <param name="calculationType">The type of calculation to be performed.</param>
/// <param name="seId">The id as used by the Swiss Ephemeris.</param>
/// <param name="useForHeliocentric">True if a heliocentric position can be calculated.</param>
/// <param name="useForGeocentric">True if a geocentric position can be calculated.</param>
/// <param name="textId">Id to find a descriptive text in a resource bundle.</param>
/// <param name="defaultGlyph">Character to show default glyph.</param> 
public record CelPointDetails(CelPoints CelPoint, CelPointCats CelPointCat, CalculationTypes CalculationType, int SeId, bool UseForHeliocentric, bool UseForGeocentric, string TextId, string DefaultGlyph);
