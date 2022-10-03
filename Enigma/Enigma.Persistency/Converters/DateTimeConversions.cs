// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Calc.Api.DateTime;
using Enigma.Core.Calc.ReqResp;
using Enigma.Domain.Constants;
using Enigma.Domain.DateTime;
using Enigma.Persistency.Domain;
using System;

namespace Enigma.Persistency.Converters;

/// <summary>Conversions to date for csv-data.</summary>
public interface IDateCheckedConversion
{
    public Tuple<PersistableDate, bool> StandardCsvToDate(string csvDate, string csvCalendar);
}

/// <summary>Conversions to time for csv-data.</summary>
public interface ITimeCheckedConversion
{
    public Tuple<PersistableTime, bool> StandardCsvToTime(string csvTime, string zoneOffset, string dst);
}

public class DateCheckedConversion : IDateCheckedConversion
{
    private ICheckDateTimeApi _checkDateTimeApi;

    public DateCheckedConversion(ICheckDateTimeApi checkDateTimeApi)
    {
        _checkDateTimeApi = checkDateTimeApi;
    }

    public Tuple<PersistableDate, bool> StandardCsvToDate(string csvDate, string csvCalendar)     
    {
        bool noErrors = true;
        PersistableDate? date = null;
        string[] items = csvDate.Trim().Split('/');
        if (items.Length == 3)
        {
            bool result;
            result = int.TryParse(items[0], out int year);
            if (!result) noErrors = false;
            result = int.TryParse(items[1], out int month);
            if (!result) noErrors = false;
            result = int.TryParse(items[2], out int day);
            if (!result) noErrors = false;
            string cal = csvCalendar.Trim().ToUpper();
            if (noErrors)
            {
                noErrors = ValidateDate(year, month, day, cal);
            }
            if (noErrors) date = new PersistableDate(year, month, day, cal);
        } else noErrors = false;

        return new Tuple<PersistableDate, bool>(date, noErrors);
    }

    private bool ValidateDate(int year, int month, int day, string cal)
    {
        double ut = 0.0;
        Calendars calendar = (cal == "J") ? Calendars.Julian : Calendars.Gregorian;
        SimpleDateTime dateTime = new(year, month, day, ut, calendar);
        CheckDateTimeRequest checkDateTimeRequest = new(dateTime);
        CheckDateTimeResponse responseValidated = _checkDateTimeApi.CheckDateTime(checkDateTimeRequest);
        return (responseValidated.Validated && responseValidated.Success);
    }

}


public class TimeCheckedConversion : ITimeCheckedConversion
{
    public Tuple<PersistableTime, bool> StandardCsvToTime(string csvTime, string zoneOffset, string dst)
    {
        bool noErrors = true;
        PersistableTime? time = null;
        string[] items = csvTime.Trim().Split(":");
        if (items.Length == 3)
        {
            bool result;
            result = int.TryParse(items[0], out int hour);
            if (!result || hour < EnigmaConstants.HOUR_MIN || hour > EnigmaConstants.HOUR_MAX) noErrors = false;
            result = int.TryParse(items[1], out int minute);
            if (!result || minute < EnigmaConstants.MINUTE_MIN || minute > EnigmaConstants.MINUTE_MAX) noErrors = false;
            result = int.TryParse(items[2], out int second);
            if (!result || second < EnigmaConstants.SECOND_MIN || second > EnigmaConstants.SECOND_MAX) noErrors = false;
            result = double.TryParse(zoneOffset, out double offsetValue);
            if (!result || offsetValue < EnigmaConstants.TIMEZONE_MIN || offsetValue > EnigmaConstants.TIMEZONE_MAX) noErrors = false;
            result = double.TryParse(dst, out double dstValue);
            if (!result || dstValue < EnigmaConstants.DST_MIN || dstValue > EnigmaConstants.DST_MAX) noErrors = false;
            if (noErrors) time = new PersistableTime(hour, minute, second, offsetValue, dstValue);
        }
        return new Tuple<PersistableTime, bool>(time, noErrors);
    }
}


