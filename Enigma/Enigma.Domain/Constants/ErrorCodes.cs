// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.Constants;

/// <summary>Numeric errcodes.</summary>
public static class ErrorCodes
{
    /// <summary>/// Error: date is invalid./// </summary>
    public const int ERR_INVALID_DATE = 1000;
    /// <summary>/// Error: geographic latitude is invalid./// </summary>
    public const int ERR_INVALID_GEOLAT = 1001;
    /// <summary>/// Error: geographic longitude is invalid./// </summary>
    public const int ERR_INVALID_GEOLON = 1002;
    /// <summary>/// Error: geographic longitude for lmt is invalid./// </summary>
    public const int ERR_INVALID_GEOLON_LMT = 1003;
    /// <summary>/// Error: time is invalid./// </summary>
    public const int ERR_INVALID_TIME = 1004;
    /// <summary>/// Error: offset for UT is invalid./// </summary>
    public const int ERR_INVALID_OFFSET = 1005;

}