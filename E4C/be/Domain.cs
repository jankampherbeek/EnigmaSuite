// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace E4C.be.domain
{

    /// <summary>
    /// Presentation for a date and time, including calendar.
    /// </summary>
    /// <remarks>
    /// For ut (Universal Time) add the time in 0..23 hours and a decimal fraction for the total of minutes and seconds.
    /// </remarks>
    public record SimpleDateTime
    {
        public readonly int year, month, day;
        public readonly double ut;
        public readonly bool gregorian;

        public SimpleDateTime(int year, int month, int day, double ut, bool gregorian)
        {
            this.year = year;
            this.month = month;
            this.day = day;
            this.ut = ut;
            this.gregorian = gregorian;
        }
    }

    /// <summary>
    /// Wrapper for a calculated double with error information.
    /// </summary>
    public record ResultForDouble
    {
        public readonly double returnValue;
        public readonly bool noErrors;
        public readonly string errorText = "";

        public ResultForDouble(double returnValue, bool noErrors, string errorText = "")
        {
            this.returnValue = returnValue;
            this.noErrors = noErrors;
            this.errorText = errorText;
        }
    }

    /// <summary>
    /// Wrapper for a data with error information.
    /// </summary>
    public record ValidatedDate
    {
        public readonly int year;
        public readonly int month;
        public readonly int day;
        public readonly bool isGregorian;
        public readonly bool noErrors;
        public readonly string errorText = "";

        public ValidatedDate(int year, int month, int day, bool isGregorian, bool noErrors, string errorText = "")
        {
            this.year = year;
            this.month = month;
            this.day = day;
            this.isGregorian = isGregorian;
            this.noErrors = noErrors;
            this.errorText = errorText;
        }
    }

    /// <summary>
    /// Wrapper for time with error information. Time should always be in UT.
    /// </summary>
    public record ValidatedUniversalTime
    {
        public readonly int hour;
        public readonly int minute;
        public readonly int second;
        public readonly bool noErrors;
        public readonly string errorText = "";

        public ValidatedUniversalTime(int hour, int minute, int second, bool noErrors, string errorText = "")
        {
            this.hour = hour;
            this.minute = minute;
            this.second = second;
            this.noErrors = noErrors;
            this.errorText = errorText;
        }

    }

}
