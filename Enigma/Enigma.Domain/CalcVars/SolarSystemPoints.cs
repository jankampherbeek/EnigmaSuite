// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.Constants;

namespace Enigma.Domain.CalcVars;

/// <summary>Supported points in the Solar System (Planets, lights, Plutoids etc.).</summary>
public enum SolarSystemPoints
{
    Sun = 0, Moon = 1, Mercury = 2, Venus = 3, Earth = 4, Mars = 5, Jupiter = 6, Saturn = 7, Uranus = 8, Neptune = 9, 
    Pluto = 10, MeanNode = 11, TrueNode = 12, Chiron = 13, PersephoneRam = 14, HermesRam = 15, DemeterRam = 16, CupidoUra = 17, HadesUra = 18, ZeusUra = 19, 
    KronosUra = 20, ApollonUra = 21, AdmetosUra = 22, VulcanusUra = 23, PoseidonUra = 24, Eris = 25, Pholus = 26, Ceres = 27, Pallas = 28, Juno = 29, 
    Vesta = 30, Isis = 31, Nessus = 32, Huya = 33, Varuna = 34, Ixion = 35, Quaoar = 36, Haumea = 37, Orcus = 38, Makemake = 39, 
    Sedna = 40, Hygieia = 41, Astraea = 42, ApogeeMean = 43, ApogeeCorrected = 44, ApogeeInterpolated = 45, ApogeeDuval = 46, ZeroAries = 47, ParsFortunaNoSect = 48, ParsFortunaSect = 49, 
    PersephoneCarteret = 50, VulcanusCarteret = 51
}

/// <summary>Details for a Solar System Point.</summary>
public record SolarSystemPointDetails
{
    readonly public SolarSystemPoints SolarSystemPoint;
    readonly public SolSysPointCats SolSysPointCat;
    readonly public CalculationTypes CalculationType;
    readonly public int SeId;
    readonly public bool UseForHeliocentric;
    readonly public bool UseForGeocentric;
    readonly public string TextId;
    readonly public string DefaultGlyph;

    /// <param name="solarSystemPoint">The Solar System Point.</param>
    /// <param name="solSysPointCat">The category for the Solar System Point.</param>
    /// <param name="calculationType">The type of calculation to be performed.</param>
    /// <param name="seId">The id as used by the Swiss Ephemeris.</param>
    /// <param name="useForHeliocentric">True if a heliocentric position can be calculated.</param>
    /// <param name="useForGeocentric">True if a geocentric position can be calculated.</param>
    /// <param name="textId">Id to find a descriptive text in a resource bundle.</param>
    /// <param name="defaultGlyph">Character to show default glyph.</param>
    public SolarSystemPointDetails(SolarSystemPoints solarSystemPoint, SolSysPointCats solSysPointCat, CalculationTypes calculationType, int seId, bool useForHeliocentric, bool useForGeocentric, string textId, string defaultGlyph)
    {
        SolarSystemPoint = solarSystemPoint;
        SolSysPointCat = solSysPointCat;
        CalculationType = calculationType;
        SeId = seId;
        UseForHeliocentric = useForHeliocentric;
        UseForGeocentric = useForGeocentric;
        TextId = textId;
        DefaultGlyph = defaultGlyph;
    }
}

/// <summary>Specifications for a Solar System Point.</summary>
public interface ISolarSystemPointSpecifications
{
    /// <summary>Returns the specifications for a Solar System Point.</summary>
    /// <param name="point">The solar system point for which to find the details.</param>
    /// <returns>A record SolarSystemPointDetails with the specifications.</returns>
    public SolarSystemPointDetails DetailsForPoint(SolarSystemPoints point);
}

/// <inheritdoc/>
public class SolarSystemPointSpecifications : ISolarSystemPointSpecifications
{
    /// <inheritdoc/>
    /// <exception cref="ArgumentException">Is thrown if the Solar System Point was not recognized.</exception>
    SolarSystemPointDetails ISolarSystemPointSpecifications.DetailsForPoint(SolarSystemPoints point)
    {
        return point switch
        {
            /*
             * To add:
             * Black Moon Mean       12 Mean apogee
             * Black Moon corrected  13 Oscu apogee
             * ---> Interpolated Apogee   21
             * ---> Interpolated Perigee 22
             * Black Moon Duval                   --> 105
             * Zero Aries                         --> 106
             * ParsFortuna Sect                   --> 107
             * ParsFortuna NoSect                 --> 108

             * 
             * Vulcanus Carteret   (103)
             * Persephone Carteret (104)
             * Persephone/hermes/Demeter - Ram nieuwe nrs: 100, 101, 102
             * 0 Ram : 105

             */


            SolarSystemPoints.Sun => new SolarSystemPointDetails(point, SolSysPointCats.Classic, CalculationTypes.SE, EnigmaConstants.SE_SUN, false, true, "sun", "a"),
            SolarSystemPoints.Moon => new SolarSystemPointDetails(point, SolSysPointCats.Classic, CalculationTypes.SE, EnigmaConstants.SE_MOON, false, true, "moon", "b"),
            SolarSystemPoints.Mercury => new SolarSystemPointDetails(point, SolSysPointCats.Classic, CalculationTypes.SE, EnigmaConstants.SE_MERCURY, true, true, "mercury", "c"),
            SolarSystemPoints.Venus => new SolarSystemPointDetails(point, SolSysPointCats.Classic, CalculationTypes.SE, EnigmaConstants.SE_VENUS, true, true, "venus", "d"),
            SolarSystemPoints.Earth => new SolarSystemPointDetails(point, SolSysPointCats.Classic, CalculationTypes.SE, EnigmaConstants.SE_EARTH, true, false, "earth", "e"),
            SolarSystemPoints.Mars => new SolarSystemPointDetails(point, SolSysPointCats.Classic, CalculationTypes.SE, EnigmaConstants.SE_MARS, true, true, "mars", "f"),
            SolarSystemPoints.Jupiter => new SolarSystemPointDetails(point, SolSysPointCats.Classic, CalculationTypes.SE, EnigmaConstants.SE_JUPITER, true, true, "jupiter", "g"),
            SolarSystemPoints.Saturn => new SolarSystemPointDetails(point, SolSysPointCats.Classic, CalculationTypes.SE, EnigmaConstants.SE_SATURN, true, true, "saturn", "h"),
            SolarSystemPoints.Uranus => new SolarSystemPointDetails(point, SolSysPointCats.Modern, CalculationTypes.SE, EnigmaConstants.SE_URANUS, true, true, "uranus", "i"),
            SolarSystemPoints.Neptune => new SolarSystemPointDetails(point, SolSysPointCats.Modern, CalculationTypes.SE, EnigmaConstants.SE_NEPTUNE, true, true, "neptune", "j"),
            SolarSystemPoints.Pluto => new SolarSystemPointDetails(point, SolSysPointCats.Modern, CalculationTypes.SE, EnigmaConstants.SE_PLUTO, true, true, "pluto", "k"),
            SolarSystemPoints.MeanNode => new SolarSystemPointDetails(point, SolSysPointCats.MathPoint, CalculationTypes.SE, EnigmaConstants.SE_MEAN_NODE, false, true, "meanNode", "{"),
            SolarSystemPoints.TrueNode => new SolarSystemPointDetails(point, SolSysPointCats.MathPoint, CalculationTypes.SE, EnigmaConstants.SE_TRUE_NODE, false, true, "trueNode", "{"),
            SolarSystemPoints.Chiron => new SolarSystemPointDetails(point, SolSysPointCats.Minor, CalculationTypes.SE, EnigmaConstants.SE_CHIRON, true, true, "chiron", "w"),
            SolarSystemPoints.PersephoneRam => new SolarSystemPointDetails(point, SolSysPointCats.Hypothetical, CalculationTypes.Elements, EnigmaConstants.SE_PERSEPHONE_RAM, true, true, "persephone_ram", "/"),
            SolarSystemPoints.HermesRam => new SolarSystemPointDetails(point, SolSysPointCats.Hypothetical, CalculationTypes.Elements, EnigmaConstants.SE_HERMES_RAM, true, true, "hermes_ram", "<"),
            SolarSystemPoints.DemeterRam => new SolarSystemPointDetails(point, SolSysPointCats.Hypothetical, CalculationTypes.Elements, EnigmaConstants.SE_DEMETER_RAM, true, true, "demeter_ram", ">"),
            SolarSystemPoints.CupidoUra => new SolarSystemPointDetails(point, SolSysPointCats.Hypothetical, CalculationTypes.SE, EnigmaConstants.SE_CUPIDO_URA, true, true, "cupido_ura", "y"),
            SolarSystemPoints.HadesUra => new SolarSystemPointDetails(point, SolSysPointCats.Hypothetical, CalculationTypes.SE, EnigmaConstants.SE_HADES_URA, true, true, "hades_ura", "z"),
            SolarSystemPoints.ZeusUra => new SolarSystemPointDetails(point, SolSysPointCats.Hypothetical, CalculationTypes.SE, EnigmaConstants.SE_ZEUS_URA, true, true, "zeus_ura", "!"),
            SolarSystemPoints.KronosUra => new SolarSystemPointDetails(point, SolSysPointCats.Hypothetical, CalculationTypes.SE, EnigmaConstants.SE_KRONOS_URA, true, true, "kronos_ura", "@"),
            SolarSystemPoints.ApollonUra => new SolarSystemPointDetails(point, SolSysPointCats.Hypothetical, CalculationTypes.SE, EnigmaConstants.SE_APOLLON_URA, true, true, "apollon_ura", "#"),
            SolarSystemPoints.AdmetosUra => new SolarSystemPointDetails(point, SolSysPointCats.Hypothetical, CalculationTypes.SE, EnigmaConstants.SE_ADMETOS_URA, true, true, "admetos_ura", "$"),
            SolarSystemPoints.VulcanusUra => new SolarSystemPointDetails(point, SolSysPointCats.Hypothetical, CalculationTypes.SE, EnigmaConstants.SE_VULCANUS_URA, true, true, "vulcanus_ura", "%"),
            SolarSystemPoints.PoseidonUra => new SolarSystemPointDetails(point, SolSysPointCats.Hypothetical, CalculationTypes.SE, EnigmaConstants.SE_POSEIDON_URA, true, true, "poseidon_ura", "&"),
            SolarSystemPoints.Eris => new SolarSystemPointDetails(point, SolSysPointCats.Minor, CalculationTypes.SE, EnigmaConstants.SE_ERIS, true, true, "eris", "*"),
            SolarSystemPoints.Pholus => new SolarSystemPointDetails(point, SolSysPointCats.Minor, CalculationTypes.SE, EnigmaConstants.SE_PHOLUS, true, true, "pholus", ")"),
            SolarSystemPoints.Ceres => new SolarSystemPointDetails(point, SolSysPointCats.Minor, CalculationTypes.SE, EnigmaConstants.SE_CERES, true, true, "ceres", "_"),
            SolarSystemPoints.Pallas => new SolarSystemPointDetails(point, SolSysPointCats.Minor, CalculationTypes.SE, EnigmaConstants.SE_PALLAS, true, true, "pallas", "û"),
            SolarSystemPoints.Juno => new SolarSystemPointDetails(point, SolSysPointCats.Minor, CalculationTypes.SE, EnigmaConstants.SE_JUNO, true, true, "juno", "ü"),
            SolarSystemPoints.Vesta => new SolarSystemPointDetails(point, SolSysPointCats.Minor, CalculationTypes.SE, EnigmaConstants.SE_VESTA, true, true, "vesta", "À"),
            SolarSystemPoints.Isis => new SolarSystemPointDetails(point, SolSysPointCats.Hypothetical, CalculationTypes.SE, EnigmaConstants.SE_ISIS, true, true, "isis", "â"),
            SolarSystemPoints.Nessus => new SolarSystemPointDetails(point, SolSysPointCats.Minor, CalculationTypes.SE, EnigmaConstants.SE_NESSUS, true, true, "nessus", "("),
            SolarSystemPoints.Huya => new SolarSystemPointDetails(point, SolSysPointCats.Minor, CalculationTypes.SE, EnigmaConstants.SE_HUYA, true, true, "huya", "ï"),
            SolarSystemPoints.Varuna => new SolarSystemPointDetails(point, SolSysPointCats.Minor, CalculationTypes.SE, EnigmaConstants.SE_VARUNA, true, true, "varuna", "ò"),
            SolarSystemPoints.Ixion => new SolarSystemPointDetails(point, SolSysPointCats.Minor, CalculationTypes.SE, EnigmaConstants.SE_IXION, true, true, "ixion", "ó"),
            SolarSystemPoints.Quaoar => new SolarSystemPointDetails(point, SolSysPointCats.Minor, CalculationTypes.SE, EnigmaConstants.SE_QUAOAR, true, true, "quaoar", "ô"),
            SolarSystemPoints.Haumea => new SolarSystemPointDetails(point, SolSysPointCats.Minor, CalculationTypes.SE, EnigmaConstants.SE_HAUMEA, true, true, "haumea", "í"),
            SolarSystemPoints.Orcus => new SolarSystemPointDetails(point, SolSysPointCats.Minor, CalculationTypes.SE, EnigmaConstants.SE_ORCUS, true, true, "orcus", "ù"),
            SolarSystemPoints.Makemake => new SolarSystemPointDetails(point, SolSysPointCats.Minor, CalculationTypes.SE, EnigmaConstants.SE_MAKEMAKE, true, true, "makemake", "î"),
            SolarSystemPoints.Sedna => new SolarSystemPointDetails(point, SolSysPointCats.Minor, CalculationTypes.SE, EnigmaConstants.SE_SEDNA, true, true, "sedna", "ö"),
            SolarSystemPoints.Hygieia => new SolarSystemPointDetails(point, SolSysPointCats.Minor, CalculationTypes.SE, EnigmaConstants.SE_HYGIEIA, true, true, "hygieia", "Á"),
            SolarSystemPoints.Astraea => new SolarSystemPointDetails(point, SolSysPointCats.Minor, CalculationTypes.SE, EnigmaConstants.SE_ASTRAEA, true, true, "astraea", "Ã"),
            SolarSystemPoints.ApogeeMean => new SolarSystemPointDetails(point, SolSysPointCats.MathPoint, CalculationTypes.SE, EnigmaConstants.SE_MEAN_APOGEE, false, true, "apogee_mean", ","),
            SolarSystemPoints.ApogeeCorrected => new SolarSystemPointDetails(point, SolSysPointCats.MathPoint, CalculationTypes.SE, EnigmaConstants.SE_OSCU_APOG, false, true, "apogee_corrected", "."),
            SolarSystemPoints.ApogeeInterpolated => new SolarSystemPointDetails(point, SolSysPointCats.MathPoint, CalculationTypes.SE, EnigmaConstants.SE_INTP_APOG, false, true, "apogee_interpolated", "."),
            SolarSystemPoints.ApogeeDuval => new SolarSystemPointDetails(point, SolSysPointCats.MathPoint, CalculationTypes.Numeric, EnigmaConstants.NON_SE_DUVAL_APOGEE, false, true, "apogee_duval", "."),
            SolarSystemPoints.ZeroAries => new SolarSystemPointDetails(point, SolSysPointCats.MathPoint, CalculationTypes.Numeric, EnigmaConstants.NON_SE_ZEROARIES, false, true, "zero_aries", "1"),
            SolarSystemPoints.ParsFortunaSect => new SolarSystemPointDetails(point, SolSysPointCats.MathPoint, CalculationTypes.Numeric, EnigmaConstants.NON_SE_PARS_FORTUNA_SECT, false, true, "pars_fortuna_sect", "e"),
            SolarSystemPoints.ParsFortunaNoSect => new SolarSystemPointDetails(point, SolSysPointCats.MathPoint, CalculationTypes.Numeric, EnigmaConstants.NON_SE_PARS_FORTUNA_NOSECT, false, true, "pars_fortuna_nosect", "e"),
            SolarSystemPoints.PersephoneCarteret => new SolarSystemPointDetails(point, SolSysPointCats.Hypothetical, CalculationTypes.Numeric, EnigmaConstants.NON_SE_PERSEPHONE_CARTERET, false, true, "pesephone_carteret", "à"),
            SolarSystemPoints.VulcanusCarteret => new SolarSystemPointDetails(point, SolSysPointCats.Hypothetical, CalculationTypes.Numeric, EnigmaConstants.NON_SE_VULCANUS_CARTERET, false, true, "pesephone_carteret", "Ï"),

            _ => throw new ArgumentException("SolarSystemPoint unknown : " + point.ToString())
        };
    }
}