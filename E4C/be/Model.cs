namespace E4C.be.model
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
}
