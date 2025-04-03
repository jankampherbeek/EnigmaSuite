// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.LocationAndTimeZones;
using Enigma.Domain.LocationsZones;
using Serilog;

namespace Enigma.Api.LocationAndTimezones;

/// <summary>Api to retrieve timezone information.</summary>
public interface ITimezoneApi
{
    /// <summary>Find timezone for a give date/time and a tzIndication that is retrieved from the location.</summary>
    /// <param name="dateTime">Date and time</param>
    /// <param name="tzIndication">String eith the indication of the timzeone. Should contain at least 5 characters.</param>
    /// <returns>If no errors occur: the timezone and a default ApiResult.
    /// Otherwise an empty timezone and an ApiResult with an error.</returns>
    public (ZoneInfo, ApiResult) ActualTimezone(DateTimeHms dateTime, string tzIndication);
}

/// <inheritdoc/>
public class TimezoneApi(ITzHandler handler):ITimezoneApi
{
    /// <inheritdoc/>
    public (ZoneInfo, ApiResult) ActualTimezone(DateTimeHms dateTime, string tzIndication)
    {
        var emptyZoneInfo = new ZoneInfo(0.0, "", true);
        if (tzIndication.Length < 5)
        {
            Log.Error($"Value for tzIndications is too short {tzIndication}");
        }

        var dt = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day);
        var helper = new PreciseTimeZoneHelper();
        var tzInfo = helper.GetPreciseTimeZoneInfo(tzIndication, dt);
        var offset = tzInfo.UtcOffset.Hours;
        var tzName = tzInfo.StandardName;
        var dst = tzInfo.IsDaylightSavingTime;

        var zoneInfo = new ZoneInfo(offset, tzName, dst);

        // try
        // {
        //     var zoneInfo = handler.CurrentTime(dateTime, tzIndication);
        //     var apiResult = new ApiResult();
        //     return (zoneInfo, apiResult);
        // }
        // catch (Exception e)
        // {
        //     var errorTxt = $"Exception {e.Message} when retrieving zone info";
        //     Log.Error(errorTxt);
        //     var apiResult = new ApiResult(true, errorTxt);
        //     return (emptyZoneInfo, apiResult);
        // }
        return (zoneInfo, new ApiResult());
    }
}