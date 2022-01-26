// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace E4C.Models.Domain
{
    /// <summary>
    /// Global constants.
    /// </summary>
    /// <remarks>
    /// Constants for the Swiss Ephemeris are prefixed by 'SE'.
    /// </remarks>
    public class Constants
    {


        // SE solar system points.
        public const int SE_SUN = 0;
        public const int SE_MOON = 1;
        public const int SE_MERCURY = 2;
        public const int SE_VENUS = 3;
        public const int SE_MARS = 4;
        public const int SE_JUPITER = 5;
        public const int SE_SATURN = 6;
        public const int SE_URANUS = 7;
        public const int SE_NEPTUNE = 8;
        public const int SE_PLUTO = 9;
        public const int SE_MEAN_NODE = 10;
        public const int SE_TRUE_NODE = 11;
        public const int SE_EARTH = 14;
        public const int SE_CHIRON = 15;
        // SE flags
        public const int SEFLG_SWIEPH = 2;
        public const int SEFLG_HELCTR = 8;
        public const int SEFLG_SPEED = 256;
        public const int SEFLG_EQUATORIAL = 2048;
        public const int SEFLG_TOPOCTR = 32 * 1024;
        public const int SEFLG_SIDEREAL = 64 * 1024;

    }

    public class ErrorCodes
    {
        public const int ERR_INVALID_DATE = 1000;
        public const int ERR_INVALID_TIME = 1001;
    }
}
