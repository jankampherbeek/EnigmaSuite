// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using System.Globalization;
using Enigma.Core.Interfaces;
using Enigma.Domain.Constants;
using Enigma.Domain.Dtos;
using Enigma.Domain.Persistables;
using Enigma.Domain.References;

namespace Enigma.Core.Persistency.Helpers;


public sealed class DateCheckedConversion : IDateCheckedConversion
{
    private readonly IDateTimeValidator _dateTimeValidator;

    public DateCheckedConversion(IDateTimeValidator dateTimeValidator)
    {
        _dateTimeValidator = dateTimeValidator;
    }

    public Tuple<PersistableDate, bool> StandardCsvToDate(string csvDate, string csvCalendar)
    {
        bool noErrors = true;
        var date = new PersistableDate(0, 0, 0, "g");
        string[] items = csvDate.Trim().Split('/');
        if (items.Length == 3)
        {
            bool result = int.TryParse(items[0], out int year);
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
        }
        else noErrors = false;

        return new Tuple<PersistableDate, bool>(date, noErrors);
    }

    private bool ValidateDate(int year, int month, int day, string cal)
    {
        const double ut = 0.0;
        Calendars calendar = (cal == "J") ? Calendars.Julian : Calendars.Gregorian;
        SimpleDateTime dateTime = new(year, month, day, ut, calendar);
        return _dateTimeValidator.ValidateDateTime(dateTime);
    }

}


public class TimeCheckedConversion : ITimeCheckedConversion
{
    public Tuple<PersistableTime, bool> StandardCsvToTime(string csvTime, string zoneOffset, string dst)
    {
        bool noErrors = true;
        var time = new PersistableTime(0, 0, 0, 0.0, 0.0);

        if (csvTime.LastIndexOf(':') == csvTime.IndexOf(':'))   // check for seconds
        {
            csvTime += ":0";
        }

        string[] items = csvTime.Trim().Split(":");

        if (items.Length == 3)
        {
            bool result = int.TryParse(items[0], out int hour);
            if (!result || hour < EnigmaConstants.HOUR_MIN || hour > EnigmaConstants.HOUR_MAX) noErrors = false;
            result = int.TryParse(items[1], out int minute);
            if (!result || minute < EnigmaConstants.MINUTE_MIN || minute > EnigmaConstants.MINUTE_MAX) noErrors = false;
            result = int.TryParse(items[2], out int second);
            if (!result || second < EnigmaConstants.SECOND_MIN || second > EnigmaConstants.SECOND_MAX) noErrors = false;
            double offsetValue = double.Parse(zoneOffset, CultureInfo.InvariantCulture);            // CultureInfo is required to handle decimal point on Localities that use a comma instead.
            if (offsetValue is < EnigmaConstants.TIMEZONE_MIN or > EnigmaConstants.TIMEZONE_MAX) noErrors = false;
            result = double.TryParse(dst, out double dstValue);
            if (!result || dstValue < EnigmaConstants.DST_MIN || dstValue > EnigmaConstants.DST_MAX) noErrors = false;
            if (noErrors) time = new PersistableTime(hour, minute, second, offsetValue, dstValue);
        }
        else
        {
            noErrors = false;
        }
        return new Tuple<PersistableTime, bool>(time, noErrors);

    }
}


