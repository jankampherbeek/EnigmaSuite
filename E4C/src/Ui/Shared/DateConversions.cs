// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.Core.Api.Datetime;
using E4C.Core.Shared.Domain;
using E4C.Shared.References;
using E4C.Shared.ReqResp;
using System;

namespace E4C.Ui.Shared;


public interface IDateConversions
{
    /// <summary>
    /// Convert inputvalues for a date to a Julian Day number. 
    /// </summary>
    /// <param name="inputDate">String array with year, month and day, in that sequence.</param>
    /// <param name="calendar">Value from enum Calendars.</param>
    /// <param name="yearCount">Value from enum YearCounts.</param>
    /// <returns>The Julian day umber for UT 0:00:00 of the given date.</returns>
    public double InputDateToJdNr(string[] inputDate, Calendars calendar, YearCounts yearCount);

    /// <summary>
    /// Convert string array for year, month and day to an int array. 
    /// </summary>
    /// <param name="inputDate">String array with year, month and day, in that sequence.</param>
    /// <returns>Int array with year, month and day, in that sequence.</returns>
    public int[] InputDateToDecimals(string[] inputDate);

}


public class DateConversions : IDateConversions
{
    private readonly ICalcDateTimeApi _calcDateTimeApi;
    private readonly IJulianDayApi _julianDayApi;

    public DateConversions(ICalcDateTimeApi dateTimeApi, IJulianDayApi julianDayApi)
    {
        _calcDateTimeApi = dateTimeApi;
        _julianDayApi = julianDayApi;
    }

    public int[] InputDateToDecimals(string[] inputDate)
    {
        try
        {
            int value1 = int.Parse(inputDate[0]);
            int value2 = int.Parse(inputDate[1]);
            int value3 = int.Parse(inputDate[2]);
            return new int[] { value1, value2, value3 };
        }
        catch (Exception e)
        {
            throw new ArgumentException("Error converting strings for a date to decimal, using values : " + inputDate.ToString() + ". Original exception message : " + e.Message);
        }
    }

    public double InputDateToJdNr(string[] inputDate, Calendars calendar, YearCounts yearCount)
    {
        int[] _dateValues = InputDateToDecimals(inputDate);
        double _ut = 0.0;
        if (yearCount == YearCounts.BCE)
        {
            _dateValues[0]++;
        }
        SimpleDateTime _simpleDateTime = new(_dateValues[0], _dateValues[1], _dateValues[2], _ut, calendar);
        JulianDayResponse julDayResponse = _julianDayApi.getJulianDay(new JulianDayRequest(_simpleDateTime));
        if (julDayResponse.Success)
        {
            return julDayResponse.JulDay;
        }
        throw new ArgumentException("Error calculating JD while converting inputdate to Jdnr. Using values : " + _simpleDateTime.ToString() + " and Calendar : " + calendar);
    }
}
