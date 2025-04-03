// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Core.LocationAndTimeZones;

using NodaTime;
using NodaTime.TimeZones;
using System;
using System.Collections.Generic;
using System.Linq;

public class TimeZoneHelper
{
    private readonly IDateTimeZoneProvider _timeZoneProvider;

    public TimeZoneHelper()
    {
        // Ise the tz database
        _timeZoneProvider = DateTimeZoneProviders.Tzdb;
    }

    /// <summary>Retrieve time zone information for a specific location and time</summary>
    /// <param name="timeZoneId">IANA time zone ID (e.g. "Europe/Amsterdam")</param>
    /// <param name="dateTime">The time to check</param>
    /// <returns>Timezone information</returns>
    public TimeZoneInfo GetTimeZoneInfo(string timeZoneId, DateTime dateTime)
    {
        if (!_timeZoneProvider.Ids.Contains(timeZoneId))
        {
            throw new ArgumentException($"Unknown time zone: {timeZoneId}");
        }

        var zone = _timeZoneProvider[timeZoneId];
        var instant = LocalDateTime.FromDateTime(dateTime).InZoneStrictly(zone).ToInstant();
        var zoneInterval = zone.GetZoneInterval(instant);
        
        return new TimeZoneInfo
        {
            TimeZoneId = timeZoneId,
            StandardName = zoneInterval.Name,
            IsDaylightSavingTime = zoneInterval.Savings != Offset.Zero,
            UtcOffset = zoneInterval.WallOffset.ToTimeSpan(),
            DaylightSavingsOffset = zoneInterval.Savings.ToTimeSpan(),
            Abbreviation = GetTimeZoneAbbreviation(zoneInterval.Name)
        };
    }

    /// <summary>Search time zones that are used for a specific location</summary>
    /// <param name="countryCode">ISO 3166-1 countrycode (e.g. "NL")</param>
    /// <returns>List of time zone IDs</returns>
    public List<string> GetTimeZonesForCountry(string countryCode)
    {
        var mappings = TzdbDateTimeZoneSource.Default.WindowsMapping.MapZones;
        var countryZones = TzdbDateTimeZoneSource.Default.ZoneLocations
            .Where(loc => loc.CountryCode == countryCode)
            .Select(loc => loc.ZoneId)
            .Distinct()
            .ToList();

        return countryZones;
    }

    /// <summary>Convert a time to UTC based on historical time zone rules</summary>
    public DateTime ConvertToUtc(DateTime localDateTime, string timeZoneId)
    {
        var zone = _timeZoneProvider[timeZoneId];
        var local = LocalDateTime.FromDateTime(localDateTime);
        var zoned = local.InZoneLeniently(zone);
        return zoned.ToDateTimeUtc();
    }

    /// <summary>Checks for DST</summary>
    public bool IsDaylightSavingTime(DateTime dateTime, string timeZoneId)
    {
        var info = GetTimeZoneInfo(timeZoneId, dateTime);
        return info.IsDaylightSavingTime;
    }

    private string GetTimeZoneAbbreviation(string name)
    {
        // Vereenvoudigde afkorting - in een echte implementatie zou je een uitgebreidere mapping willen
        return name.Split('/').Last();
    }
}

public class TimeZoneInfoOld
{
    public string TimeZoneId { get; set; }
    public string StandardName { get; set; }
    public bool IsDaylightSavingTime { get; set; }
    public TimeSpan UtcOffset { get; set; }
    public TimeSpan DaylightSavingsOffset { get; set; }
    public string Abbreviation { get; set; }

    public override string ToString()
    {
        return $"{Abbreviation} (UTC{(UtcOffset >= TimeSpan.Zero ? "+" : "")}{UtcOffset}) {(IsDaylightSavingTime ? "DST" : "STD")}";
    }
}