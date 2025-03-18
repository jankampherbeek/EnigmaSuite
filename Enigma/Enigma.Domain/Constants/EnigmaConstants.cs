// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024, 2025.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.Constants;

/// <summary>Global constants.</summary>
/// <remarks>Constants for the Swiss Ephemeris are prefixed by 'SE'.</remarks>
public static class EnigmaConstants
{
    // Version info
    public const string ENIGMA_VERSION = "0.6.0";

    /// <summary>SE id to identify obliquity and nutation.</summary>
    public const int SE_ECL_NUT = -1;
   
    
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
    
