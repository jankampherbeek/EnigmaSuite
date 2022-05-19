// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.Constants;

/// <summary>Global constants.</summary>
/// <remarks>Constants for the Swiss Ephemeris are prefixed by 'SE'.</remarks>
public static class EnigmaConstants
{
    // SE solar system points. _RAM = School of Ram, _URA = Uranian.
    /// <summary>SE id to identify Admetos, Uranian astrology. The id is part of seorbel.txt.</summary>
    public const int SE_ADMETOS_URA = 45;
    /// <summary>SE id to identify Apollon, hypothetical planet, Uranian astrology. The id is part of seorbel.txt.</summary>
    public const int SE_APOLLON_URA = 44;
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
    /// <summary>Hermes, hypothetical planet, School of Ram.</summary>
    public const int SE_HERMES_RAM = 50;
    /// <summary>SE id to identify Jupiter.</summary>
    public const int SE_JUPITER = 5;
    /// <summary>SE id to identify Kronos, hypothetical planet, Uranian astrology. The id is part of seorbel.txt.</summary>
    public const int SE_KRONOS_URA = 43;
    /// <summary>SE id to identify Mars.</summary>
    public const int SE_MARS = 4;
    /// <summary>SE id to identify the Mean node.</summary>
    public const int SE_MEAN_NODE = 10;
    /// <summary>SE id to identify Mercury.</summary>
    public const int SE_MERCURY = 2;
    /// <summary>SE id to identify the Moon.</summary>
    public const int SE_MOON = 1;
    /// <summary>SE id to identify Neptune.</summary>
    public const int SE_NEPTUNE = 8;
    /// <summary>Persophona, hypothetical planet, School of Ram.</summary>
    public const int SE_PERSEPHONE_RAM = 49;
    /// <summary>SE id to identify Pluto.</summary>
    public const int SE_PLUTO = 9;
    /// <summary>SE id to identify Poseidon, hypothetical planet, Uranian astrology. The id is part of seorbel.txt.</summary>
    public const int SE_POSEIDON_URA = 47;
    /// <summary>SE id to identify Saturn.</summary>
    public const int SE_SATURN = 6;
    /// <summary>SE id to identify the Sun.</summary>
    public const int SE_SUN = 0;
    /// <summary>SE id to identify the oscilating (true) lunar node.</summary>
    public const int SE_TRUE_NODE = 11;
    /// <summary>SE id to identify Uranus.</summary>
    public const int SE_URANUS = 7;
    /// <summary>SE id to identify Vulcanus, hypothetical planet, Uranian astrology. The id is part of seorbel.txt.</summary>
    public const int SE_VULCANUS_URA = 46;
    /// <summary>SE id to identify Venus.</summary>
    public const int SE_VENUS = 3;
    /// <summary>SE id to identify Zeus, hypothetical planet, Uranian astrology. The id is part of seorbel.txt.</summary>
    public const int SE_ZEUS_URA = 42;

    // SE flags
    /// <summary>Flag: indication to use the Swiss Ephemeris.</summary>
    public const int SEFLG_SWIEPH = 2;
    /// <summary>Flag: indication to calculate heliocentric positions.</summary>
    public const int SEFLG_HELCTR = 8;
    /// <summary>Flag: indication to also calculate the speed.</summary>
    public const int SEFLG_SPEED = 256;
    /// <summary>Flag: indication to calculate equatorial positions.</summary>
    public const int SEFLG_EQUATORIAL = 2048;
    /// <summary>Flag: indication to calculate topocentric positions.</summary>
    public const int SEFLG_TOPOCTR = 32 * 1024;
    /// <summary>Flag: indication to use sidereal positions.</summary>
    public const int SEFLG_SIDEREAL = 64 * 1024;
    // Limits
    /// <summary>Maximum amount of degrees for geographic latitude.</summary>
    public const int GEOLAT_DEGREE_MAX = 89;
    /// <summary>Minimum amount of degrees for geographic latitude.</summary>
    public const int GEOLAT_DEGREE_MIN = 0;
    /// <summary>Maximum amount of degrees for geographic longitude.</summary>
    public const int GEOLON_DEGREE_MAX = 180;
    /// <summary>Minimum amount of degrees for geographic longitude.</summary>
    public const int GEOLON_DEGREE_MIN = 0;
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
    /// <summary>Numer of seconds in a day.</summary>
    public const int SECONDS_PER_DAY = 86400;
    // Conversion factors
    /// <summary>Number of hours in one day.</summary>
    public const int HOURS_PER_DAY = 24;
    /// <summary>Number of minutes in one day.</summary>
    public const int MINUTES_PER_DAY = 1440;
    /// <summary>Number of minutes in one hour.</summary>
    public const int MINUTES_PER_HOUR_DEGREE = 60;
    /// <summary>Number of seconds in one hour.</summary>
    public const int SECONDS_PER_HOUR_DEGREE = 3600;
    /// <summary>Number of seconds in one degree.</summary>
    public const int SECONDS_PER_MINUTE_DEGREE = 60;
    // Characters
    /// <summary>SMall elevated circle to identify degrees.</summary>
    public const char DEGREE_SIGN = '\u00B0';
    /// <summary>Apostroph-like identification for positional minutes.</summary>
    public const char MINUTE_SIGN = '\u2032';
    /// <summary>Quote-like identification for postional seconds.</summary>
    public const char SECOND_SIGN = '\u2033';

}
