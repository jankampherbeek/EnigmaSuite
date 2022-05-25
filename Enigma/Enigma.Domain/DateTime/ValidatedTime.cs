// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.DateTime;

/// <summary>
/// Wrapper for time with error information. Time should always be in UT.
/// </summary>
public record ValidatedUniversalTime
{
    public readonly int Hour;
    public readonly int Minute;
    public readonly int Second;
    public readonly List<int> ErrorCodes;

    public ValidatedUniversalTime(int hour, int minute, int second, List<int> errorCodes)
    {
        Hour = hour;
        Minute = minute;
        Second = second;
        ErrorCodes = errorCodes;
    }

}

