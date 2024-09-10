// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.Constants;

/// <summary>Global constants.</summary>
/// <remarks>Constants for the Swiss Ephemeris are prefixed by 'SE'.</remarks>
public static class EnigmaConstants
{
    // Version info
    public const string ENIGMA_VERSION = "0.5.0";
    // CommonSE celestial points. _RAM = School of Ram, _URA = Uranian.
    /// <summary>SE id to identify Admetos, Uranian astrology. The id is part of seorbel.txt.</summary>
    public const int SE_ADMETOS_URA = 45;
    /// <summary>SE id to identify Apollon, hypothetical planet, Uranian astrology. The id is part of seorbel.txt.</summary>
    public const int SE_APOLLON_URA = 44;
    /// <summary>SE id for Astraea.</summary>
    public const int SE_ASTRAEA = 10005;
    /// <summary>SE id to identify the planetoid Ceres.</summary>
    public const int SE_CERES = 17;
    /// <summary>SE id to identify Chiron.</summary>
    public const int SE_CHIRON = 15;
    /// <summary>SE id to identify Cupido, hypothetical planet, Uranian astrology. The id is part of seorbel.txt.</summary>
    public const int SE_CUPIDO_URA = 40;
    /// <summary>Demeter, hypothetical planet, School of Ram.</summary>
    public const int SE_DEMETER_RAM = 51;
    /// <summary>SE id to identify Earth.</summary>
    public const int SE_EARTH = 14;
    /// <summary>SE id to identify obliquity and nutation.</summary>
    public const int SE_ECL_NUT = -1;
    /// <summary>SE id to identify Eris.</summary>
    public const int SE_ERIS = 1009001;
    /// <summary>SE id to identify Hades, hypothetical planet, Uranian astrology. The id is part of seorbel.txt.</summary>
    public const int SE_HADES_URA = 41;
    /// <summary>SE id for Haumea.</summary>
    public const int SE_HAUMEA = 146108;
    /// <summary>Hermes, hypothetical planet, School of Ram.</summary>
    public const int SE_HERMES_RAM = 50;
    /// <summary>SE id for Huya.</summary>
    public const int SE_HUYA = 48628;
    /// <summary>SE id for Hygieia.</summary>
    public const int SE_HYGIEIA = 10010;
    /// <summary>SE id to identify the interpolated Apogee.</summary>
    public const int SE_INTP_APOG = 21;
    /// <summary>SE id to identify the hypothetical planet Isis/Transpluto.</summary>
    public const int SE_ISIS = 48;
    /// <summary>SE id for Ixion.</summary>
    public const int SE_IXION = 38978;
    /// <summary>SE id to identify the planetoid Juno.</summary>
    public const int SE_JUNO = 19;
    /// <summary>SE id to identify Jupiter.</summary>
    public const int SE_JUPITER = 5;
    /// <summary>SE id to identify Kronos, hypothetical planet, Uranian astrology. The id is part of seorbel.txt.</summary>
    public const int SE_KRONOS_URA = 43;
    /// <summary>Se id for Makemake.</summary>
    public const int SE_MAKEMAKE = 146472;
    /// <summary>SE id to identify Mars.</summary>
    public const int SE_MARS = 4;
    /// <summary>SE id to identify the mean Apogee.</summary>
    public const int SE_MEAN_APOGEE = 12;
    /// <summary>SE id to identify the Mean node.</summary>
    public const int SE_MEAN_NODE = 10;
    /// <summary>SE id to identify Mercury.</summary>
    public const int SE_MERCURY = 2;
    /// <summary>SE id to identify the Moon.</summary>
    public const int SE_MOON = 1;
    /// <summary>SE id to identify Neptune.</summary>
    public const int SE_NEPTUNE = 8;
    /// <summary>SE id for Nessus.</summary>
    public const int SE_NESSUS = 17066;
    /// <summary>SE id to identify Orcus.</summary>
    public const int SE_ORCUS = 100482;
    /// <summary>SE id to identify the osculating Apogee.</summary>
    public const int SE_OSCU_APOG = 13;
    /// <summary>SE id to identify the centaur Pallas.</summary>
    public const int SE_PALLAS = 18;
    /// <summary>Persephone, hypothetical planet, School of Ram.</summary>
    public const int SE_PERSEPHONE_RAM = 49;
    /// <summary>SE id to identify the centaur Pholus.</summary>
    public const int SE_PHOLUS = 16;
    /// <summary>SE id to identify Pluto.</summary>
    public const int SE_PLUTO = 9;
    /// <summary>SE id to identify Poseidon, hypothetical planet, Uranian astrology. The id is part of seorbel.txt.</summary>
    public const int SE_POSEIDON_URA = 47;
    /// <summary>SE id to identify Quaoar.</summary>
    public const int SE_QUAOAR = 60000;
    /// <summary>SE id to identify Saturn.</summary>
    public const int SE_SATURN = 6;
    /// <summary>SE id to identify Sedna.</summary>
    public const int SE_SEDNA = 100377;
    /// <summary>SE id to identify the Sun.</summary>
    public const int SE_SUN = 0;
    /// <summary>SE id to identify the oscilating (true) lunar node.</summary>
    public const int SE_TRUE_NODE = 11;
    /// <summary>SE id to identify Uranus.</summary>
    public const int SE_URANUS = 7;
    /// <summary>SE id to identify Varuna.</summary>
    public const int SE_VARUNA = 30000;
    /// <summary>SE id to identify Venus.</summary>
    public const int SE_VENUS = 3;
    /// <summary>SE id to identify the planetoid Vesta.</summary>
    public const int SE_VESTA = 20;
    /// <summary>SE id to identify Vulcanus, hypothetical planet, Uranian astrology. The id is part of seorbel.txt.</summary>
    public const int SE_VULCANUS_URA = 46;
    /// <summary>SE id to identify Zeus, hypothetical planet, Uranian astrology. The id is part of seorbel.txt.</summary>
    public const int SE_ZEUS_URA = 42;

    // CommonSE flags
    /// <summary>Flag: indication to use the Swiss Ephemeris.</summary>
    public const int SEFLG_SWIEPH = 2;
    /// <summary>Flag: indication to calculate equatorial positions.</summary>
    public const int SEFLG_EQUATORIAL = 2048;
    /// <summary>Flag: indication to calculate topocentric positions.</summary>
    public const int SEFLG_TOPOCTR = 32 * 1024;
    /// <summary>Flag: indication to use sidereal positions.</summary>
    public const int SEFLG_SIDEREAL = 64 * 1024;

   
    // Limits
    /// <summary>Maximum value for daylight saving time.</summary>
    public const double DST_MAX = 3.0;
    /// <summary>Minimum value for daylight saving time.</summary>
    public const double DST_MIN = 0.0;
    /// <summary>Maximum amount of hours.</summary>
    public const int HOUR_MAX = 23;
    /// <summary>Minimum amount of hours.</summary>
    public const int HOUR_MIN = 0;
    /// <summary>Maximum amount of minutes.</summary>
    public const int MINUTE_MAX = 59;
    /// <summary>Minimum amount of minutes.</summary>
    public const int MINUTE_MIN = 0;
    /// <summary>Maximum amount of seconds.</summary>
    public const int SECOND_MAX = 59;
    /// <summary>Minimum amount of seconds.</summary>
    public const int SECOND_MIN = 0;
    /// <summary>Maximum value for timezone. </summary>
    public const double TIMEZONE_MAX = 12.0;
    /// <summary>Minimum value for timezone. </summary>
    public const double TIMEZONE_MIN = -12.0;

    // Supported periods
    /// <summary>Julian day of start supported period for Ceres and Vesta.</summary>
    public const double PERIOD_CERES_VESTA_START = -3026613.5;
    /// <summary>Julian day of end supported period for Ceres and Cesta.</summary>
    public const double PERIOD_CERES_VESTA_END = 5224242.5;
    /// <summary>Julian day of start supported period for Chiron.</summary>
    public const double PERIOD_CHIRON_START = 1967601.5;
    /// <summary>Julian day of end supported period for Chiron.</summary>
    public const double PERIOD_CHIRON_END = 3419437.5;
    /// <summary>Julian day of start supported period for Nessus, Huya, Ixion, Orcus, Varuna, Makemake, Haumea, Quaoar, Eris and Sedna.</summary>
    public const double PERIOD_NESSUS_HUYA_ETC_START = 625384.5;
    /// <summary>Julian day of end supported period for Nessus, Huya, Ixion, Orcus, Varuna, Makemake, Haumea, Quaoar, Eris and Sedna.</summary>
    public const double PERIOD_NESSUS_HUYA_ETC_END = 2816291.5;
    /// <summary>Julian day of start supported period for Pholus.</summary>
    public const double PERIOD_PHOLUS_START = 640648.5;
    /// <summary>Julian day of end supported period for Pholus.</summary>
    public const double PERIOD_PHOLUS_END = 4390615.5;
    /// <summary>Julian day of start supported period for calculations in general.</summary>
    public const double PERIOD_TOTAL_START = -3026613.5;
    /// <summary>Julian day of end supported period for calculations in general.</summary>
    public const double PERIOD_TOTAL_END = 7857131.5;



    // Conversion factors
    /// <summary>Number of minutes in one hour.</summary>
    public const int MINUTES_PER_HOUR_DEGREE = 60;
    /// <summary>Number of seconds in one hour.</summary>
    public const int SECONDS_PER_HOUR_DEGREE = 3600;

    // Characters
    /// <summary>Small elevated circle to identify degrees.</summary>
    public const char DEGREE_SIGN = '\u00B0';
    /// <summary>Apostroph-like identification for positional minutes.</summary>
    public const char MINUTE_SIGN = '\u2032';
    /// <summary>Quote-like identification for postional seconds.</summary>
    public const char SECOND_SIGN = '\u2033';
    /// <summary>Separator for date input.</summary>
    public const char SEPARATOR_DATE = '/';
    /// <summary>Separator for time input.</summary>
    public const char SEPARATOR_TIME = ':';
    /// <summary>Separator for geographic longitude input.</summary>
    public const char SEPARATOR_GEOLONG = ':';
    /// <summary>Separator for geographic latitude input.</summary>
    public const char SEPARATOR_GEOLAT = ':';
    /// <summary>Character for line-end (new line).</summary>
    public const char NEW_LINE = '\n';

    // Locations
    public const string USER_MANUAL = "https://radixpro.com/usermanual/0_4/UserManual.html";
    /// <summary>Location of deltas for configuration file, contains path and filename.</summary>
    public const string CONFIG_DELTA_LOCATION = "c:/enigma_ar/enigmacfgdelta.json";
    /// <summary>Location of deltas for configuration file for rogressions, contains path and filename.</summary>
    public const string CONFIG_PROG_DELTA_LOCATION = "c:/enigma_ar/enigmaprogcfgdelta.json";
    /// <summary>Name of relational database.</summary>   
    public const string RDBMS_NAME = "/EnigmaRDBMS.sqlite";
    /// <summary>Url to find latest release information.</summary>
    public const string RELEASE_CHECK_URL = "http://radixpro.com/rel/enigma-ar-latest.json";

    /// <summary>Length of tropical year measured in tropical days.</summary>
    /// <remarks>According to: NASA 365 days, 5 hours, 48 minutes, and 46 seconds,
    /// https://www.grc.nasa.gov/www/k-12/Numbers/Math/Mathematical_Thinking/calendar_calculations.htm</remarks>
    public const double TROPICAL_YEAR_IN_DAYS = 365.242199074;  
    
}  
    
