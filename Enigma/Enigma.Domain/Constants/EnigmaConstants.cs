// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.Constants;

/// <summary>Global constants.</summary>
/// <remarks>Constants for the Swiss Ephemeris are prefixed by 'SE'.</remarks>
public static class EnigmaConstants
{
    // Version info
    public const string EnigmaVersion = "0.1.0";
    // CommonSE celestial points. _RAM = School of Ram, _URA = Uranian.
    /// <summary>SE id to identify Admetos, Uranian astrology. The id is part of seorbel.txt.</summary>
    public const int SeAdmetosUra = 45;
    /// <summary>SE id to identify Apollon, hypothetical planet, Uranian astrology. The id is part of seorbel.txt.</summary>
    public const int SeApollonUra = 44;
    /// <summary>SE id for Astraea.</summary>
    public const int SeAstraea = 10005;
    /// <summary>SE id to identify the planetoid Ceres.</summary>
    public const int SeCeres = 17;
    /// <summary>SE id to identify Chiron.</summary>
    public const int SeChiron = 15;
    /// <summary>SE id to identify Cupido, hypothetical planet, Uranian astrology. The id is part of seorbel.txt.</summary>
    public const int SeCupidoUra = 40;
    /// <summary>Demeter, hypothetical planet, School of Ram.</summary>
    public const int SeDemeterRam = 51;
    /// <summary>SE id to identify Earth.</summary>
    public const int SeEarth = 14;
    /// <summary>SE id to identify obliquity and nutation.</summary>
    public const int SeEclNut = -1;
    /// <summary>SE id to identify Eris.</summary>
    public const int SeEris = 1009001;
    /// <summary>SE id to identify Hades, hypothetical planet, Uranian astrology. The id is part of seorbel.txt.</summary>
    public const int SeHadesUra = 41;
    /// <summary>SE id for Haumea.</summary>
    public const int SeHaumea = 146108;
    /// <summary>Hermes, hypothetical planet, School of Ram.</summary>
    public const int SeHermesRam = 50;
    /// <summary>SE id for Huya.</summary>
    public const int SeHuya = 48628;
    /// <summary>SE id for Hygieia.</summary>
    public const int SeHygieia = 10010;
    /// <summary>SE id to identify the interpolated Apogee.</summary>
    public const int SeIntpApog = 21;
    /// <summary>SE id to identify the hypothetical planet Isis/Transpluto.</summary>
    public const int SeIsis = 48;
    /// <summary>SE id for Ixion.</summary>
    public const int SeIxion = 38978;
    /// <summary>SE id to identify the planetoid Juno.</summary>
    public const int SeJuno = 19;
    /// <summary>SE id to identify Jupiter.</summary>
    public const int SeJupiter = 5;
    /// <summary>SE id to identify Kronos, hypothetical planet, Uranian astrology. The id is part of seorbel.txt.</summary>
    public const int SeKronosUra = 43;
    /// <summary>Se id for Makemake.</summary>
    public const int SeMakemake = 146472;
    /// <summary>SE id to identify Mars.</summary>
    public const int SeMars = 4;
    /// <summary>SE id to identify the mean Apogee.</summary>
    public const int SeMeanApogee = 12;
    /// <summary>SE id to identify the Mean node.</summary>
    public const int SeMeanNode = 10;
    /// <summary>SE id to identify Mercury.</summary>
    public const int SeMercury = 2;
    /// <summary>SE id to identify the Moon.</summary>
    public const int SeMoon = 1;
    /// <summary>SE id to identify Neptune.</summary>
    public const int SeNeptune = 8;
    /// <summary>SE id for Nessus.</summary>
    public const int SeNessus = 17066;
    /// <summary>SE id to identify Orcus.</summary>
    public const int SeOrcus = 100482;
    /// <summary>SE id to identify the osculating Apogee.</summary>
    public const int SeOscuApog = 13;
    /// <summary>SE id to identify the centaur Pallas.</summary>
    public const int SePallas = 18;
    /// <summary>Persephone, hypothetical planet, School of Ram.</summary>
    public const int SePersephoneRam = 49;
    /// <summary>SE id to identify the centaur Pholus.</summary>
    public const int SePholus = 16;
    /// <summary>SE id to identify Pluto.</summary>
    public const int SePluto = 9;
    /// <summary>SE id to identify Poseidon, hypothetical planet, Uranian astrology. The id is part of seorbel.txt.</summary>
    public const int SePoseidonUra = 47;
    /// <summary>SE id to identify Quaoar.</summary>
    public const int SeQuaoar = 60000;
    /// <summary>SE id to identify Saturn.</summary>
    public const int SeSaturn = 6;
    /// <summary>SE id to identify Sedna.</summary>
    public const int SeSedna = 100377;
    /// <summary>SE id to identify the Sun.</summary>
    public const int SeSun = 0;
    /// <summary>SE id to identify the oscilating (true) lunar node.</summary>
    public const int SeTrueNode = 11;
    /// <summary>SE id to identify Uranus.</summary>
    public const int SeUranus = 7;
    /// <summary>SE id to identify Varuna.</summary>
    public const int SeVaruna = 30000;
    /// <summary>SE id to identify Venus.</summary>
    public const int SeVenus = 3;
    /// <summary>SE id to identify the planetoid Vesta.</summary>
    public const int SeVesta = 20;
    /// <summary>SE id to identify Vulcanus, hypothetical planet, Uranian astrology. The id is part of seorbel.txt.</summary>
    public const int SeVulcanusUra = 46;
    /// <summary>SE id to identify Zeus, hypothetical planet, Uranian astrology. The id is part of seorbel.txt.</summary>
    public const int SeZeusUra = 42;


    // Celestial points that are not calculated with the Swiss Ephemeris
    /// <summary>Apogee, approximately according to Max Duval, formula's by Cees Jansen.</summary>
    public const int NonSeDuvalApogee = 105;
    /// <summary>Persephone according to Carteret.</summary>
    public const int NonSePersephoneCarteret = 104;
    /// <summary>Vulcanus according to Carteret.</summary>
    public const int NonSeVulcanusCarteret = 103;
    /// <summary>Spring equinox/zero degrees Aries.</summary>
    public const int NonSeZeroaries = 106;

    // CommonSE flags
    /// <summary>Flag: indication to use the Swiss Ephemeris.</summary>
    public const int SeflgSwieph = 2;
    /// <summary>Flag: indication to calculate heliocentric positions.</summary>
    public const int SeflgHelctr = 8;
    /// <summary>Flag: indication to also calculate the speed.</summary>
    public const int SeflgSpeed = 256;
    /// <summary>Flag: indication to calculate equatorial positions.</summary>
    public const int SeflgEquatorial = 2048;
    /// <summary>Flag: indication to calculate topocentric positions.</summary>
    public const int SeflgTopoctr = 32 * 1024;
    /// <summary>Flag: indication to use sidereal positions.</summary>
    public const int SeflgSidereal = 64 * 1024;

    // Limits
    /// <summary>Maximum value for daylight saving time.</summary>
    public const double DstMax = 3.0;
    /// <summary>Minimum value for daylight saving time.</summary>
    public const double DstMin = 0.0;
    /// <summary>Maximum amount of hours.</summary>
    public const int HourMax = 23;
    /// <summary>Minimum amount of hours.</summary>
    public const int HourMin = 0;
    /// <summary>Maximum amount of minutes.</summary>
    public const int MinuteMax = 59;
    /// <summary>Minimum amount of minutes.</summary>
    public const int MinuteMin = 0;
    /// <summary>Maximum amount of seconds.</summary>
    public const int SecondMax = 59;
    /// <summary>Minimum amount of seconds.</summary>
    public const int SecondMin = 0;
    /// <summary>Maximum value for timezone. </summary>
    public const double TimezoneMax = 12.0;
    /// <summary>Minimum value for timezone. </summary>
    public const double TimezoneMin = -12.0;


    // Supported periods
    /// <summary>Julian day of start supported period for Ceres and Vesta.</summary>
    public const double PeriodCeresVestaStart = -3026613.5;
    /// <summary>Julian day of end supported period for Ceres and Cesta.</summary>
    public const double PeriodCeresVestaEnd = 5224242.5;
    /// <summary>Julian day of start supported period for Chiron.</summary>
    public const double PeriodChironStart = 1967601.5;
    /// <summary>Julian day of end supported period for Chiron.</summary>
    public const double PeriodChironEnd = 3419437.5;
    /// <summary>Julian day of start supported period for Nessus, Huya, Ixion, Orcus, Varuna, Makemake, Haumea, Quaoar, Eris and Sedna.</summary>
    public const double PeriodNessusHuyaEtcStart = 625384.5;
    /// <summary>Julian day of end supported period for Nessus, Huya, Ixion, Orcus, Varuna, Makemake, Haumea, Quaoar, Eris and Sedna.</summary>
    public const double PeriodNessusHuyaEtcEnd = 2816291.5;
    /// <summary>Julian day of start supported period for Pholus.</summary>
    public const double PeriodPholusStart = 640648.5;
    /// <summary>Julian day of end supported period for Pholus.</summary>
    public const double PeriodPholusEnd = 4390615.5;
    /// <summary>Julian day of start supported period for calculations in general.</summary>
    public const double PeriodTotalStart = -3026613.5;
    /// <summary>Julian day of end supported period for calculations in general.</summary>
    public const double PeriodTotalEnd = 7857131.5;



    // Conversion factors
    /// <summary>Number of minutes in one hour.</summary>
    public const int MinutesPerHourDegree = 60;
    /// <summary>Number of seconds in one hour.</summary>
    public const int SecondsPerHourDegree = 3600;

    // Characters
    /// <summary>Small elevated circle to identify degrees.</summary>
    public const char DegreeSign = '\u00B0';
    /// <summary>Apostroph-like identification for positional minutes.</summary>
    public const char MinuteSign = '\u2032';
    /// <summary>Quote-like identification for postional seconds.</summary>
    public const char SecondSign = '\u2033';
    /// <summary>Separator for date input.</summary>
    public const char SeparatorDate = '/';
    /// <summary>Separator for time input.</summary>
    public const char SeparatorTime = ':';
    /// <summary>Separator for geographic longitude input.</summary>
    public const char SeparatorGeolong = ':';
    /// <summary>Separator for geographic latitude input.</summary>
    public const char SeparatorGeolat = ':';
    /// <summary>Character for line-end (new line).</summary>
    public const char NewLine = '\n';

    // Locations
    /// <summary>Location of configuration file, contains path and filename.</summary>
    public const string ConfigLocation = "c:/enigma_ar/enigmaconfig.json";
    /// <summary>Name of database for charts.</summary>
    public const string DatabaseName = "/Enigma.db";
    /// <summary>Url to find latest release information.</summary>
    public const string ReleaseCheckUrl = "http://radixpro.com/rel/enigma-ar-latest.json";

    /// <summary>Timekey according to Naibod.</summary>
    public const double Naibod = 0.985647222;
}
