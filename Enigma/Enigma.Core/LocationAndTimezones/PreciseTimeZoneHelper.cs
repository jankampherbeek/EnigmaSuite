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

public class PreciseTimeZoneHelper
{
    private readonly IDateTimeZoneProvider _timeZoneProvider;

    public PreciseTimeZoneHelper()
    {
        _timeZoneProvider = DateTimeZoneProviders.Tzdb;
    }

    /// <summary>
    /// Haalt gedetailleerde tijdzone-informatie op voor een exact tijdstip
    /// </summary>
    public TimeZoneInfo GetPreciseTimeZoneInfo(string timeZoneId, DateTime dateTime)
    {
        if (!_timeZoneProvider.Ids.Contains(timeZoneId))
        {
            throw new ArgumentException($"Onbekende tijdzone: {timeZoneId}");
        }

        var zone = _timeZoneProvider[timeZoneId];
        var localDateTime = LocalDateTime.FromDateTime(dateTime);
        
        // Controleer of het tijdstip ambigu is
        var mapping = zone.MapLocal(localDateTime);
        var ambiguous = mapping.Count > 1;
        
        // Controleer of het tijdstip ongeldig is
        var invalid = mapping.Count == 0;

        var zonedDateTime = localDateTime.InZoneLeniently(zone);
        var zoneInterval = zone.GetZoneInterval(zonedDateTime.ToInstant());

        return new TimeZoneInfo
        {
            TimeZoneId = timeZoneId,
            StandardName = zoneInterval.Name,
            IsDaylightSavingTime = zoneInterval.Savings != Offset.Zero,
            UtcOffset = zoneInterval.WallOffset.ToTimeSpan(),
            DaylightSavingsOffset = zoneInterval.Savings.ToTimeSpan(),
            Abbreviation = GetTimeZoneAbbreviation(zoneInterval.Name),
            LocalDateTime = dateTime,
            UtcDateTime = zonedDateTime.ToDateTimeUtc(),
            IsAmbiguousTime = ambiguous,
            IsInvalidTime = invalid
        };
    }

    /// <summary>
    /// Controleert of een specifiek tijdstip tijdens een zomertijdovergang valt
    /// </summary>
    public TransitionInfo CheckForTransition(string timeZoneId, DateTime dateTime)
    {
        var zone = _timeZoneProvider[timeZoneId];
        var localDateTime = LocalDateTime.FromDateTime(dateTime);
        var zonedDateTime = localDateTime.InZoneLeniently(zone);
        var instant = zonedDateTime.ToInstant();
        
        // Zoek overgangen in een 48-uurs venster rond het tijdstip
        var start = instant - Duration.FromHours(24);
        var end = instant + Duration.FromHours(24);
        var intervals = zone.GetZoneIntervals(start, end).ToList();
        
        ZoneInterval previous = null;
        ZoneInterval next = null;
        
        for (int i = 0; i < intervals.Count; i++)
        {
            if (intervals[i].Start <= instant && intervals[i].End > instant)
            {
                if (i > 0) previous = intervals[i-1];
                if (i < intervals.Count - 1) next = intervals[i+1];
                break;
            }
        }
        
        return new TransitionInfo
        {
            IsNearTransition = (previous != null && (instant - previous.End) < Duration.FromHours(24)) ||
                             (next != null && (next.Start - instant) < Duration.FromHours(24)),
            PreviousTransition = previous,
            NextTransition = next
        };
    }

    /// <summary>
    /// Geeft alle tijdzone-overgangen voor een bepaald jaar
    /// </summary>
    public List<ZoneTransition> GetTransitionsForYear(string timeZoneId, int year)
    {
        var zone = _timeZoneProvider[timeZoneId];
        var start = Instant.FromUtc(year, 1, 1, 0, 0);
        var end = Instant.FromUtc(year + 1, 1, 1, 0, 0);
        
        var intervals = zone.GetZoneIntervals(start, end).ToList();
        var transitions = new List<ZoneTransition>();
        
        for (int i = 1; i < intervals.Count; i++)
        {
            transitions.Add(new ZoneTransition
            {
                TransitionTime = intervals[i].Start.InZone(zone).ToDateTimeOffset(),
                NewOffset = intervals[i].WallOffset.ToTimeSpan(),
                DaylightSavingsChange = (intervals[i].Savings - intervals[i-1].Savings).ToTimeSpan()
            });
        }
        
        return transitions;
    }

    private string GetTimeZoneAbbreviation(string name)
    {
        var commonAbbreviations = new Dictionary<string, string>
        {
            {"Central European Time", "CET"},
            {"Central European Summer Time", "CEST"},
            {"Eastern European Time", "EET"},
            {"Eastern European Summer Time", "EEST"},
        };
        
        return commonAbbreviations.TryGetValue(name, out var abbreviation) 
            ? abbreviation 
            : name.Split('/').Last();
    }
}

public class TimeZoneInfo
{
    public string TimeZoneId { get; set; }
    public string StandardName { get; set; }
    public bool IsDaylightSavingTime { get; set; }
    public TimeSpan UtcOffset { get; set; }
    public TimeSpan DaylightSavingsOffset { get; set; }
    public string Abbreviation { get; set; }
    public DateTime LocalDateTime { get; set; }
    public DateTime UtcDateTime { get; set; }
    public bool IsAmbiguousTime { get; set; }
    public bool IsInvalidTime { get; set; }

    public override string ToString()
    {
        return $"{LocalDateTime:yyyy-MM-dd HH:mm} {Abbreviation} (UTC{(UtcOffset >= TimeSpan.Zero ? "+" : "")}{UtcOffset:hh\\:mm}) " +
               $"{(IsDaylightSavingTime ? "DST" : "STD")} " +
               $"{(IsAmbiguousTime ? "[AMBIGUOUS]" : "")} " +
               $"{(IsInvalidTime ? "[INVALID]" : "")}";
    }
}

public class TransitionInfo
{
    public bool IsNearTransition { get; set; }
    public ZoneInterval PreviousTransition { get; set; }
    public ZoneInterval NextTransition { get; set; }
}

public class ZoneTransition
{
    public DateTimeOffset TransitionTime { get; set; }
    public TimeSpan NewOffset { get; set; }
    public TimeSpan DaylightSavingsChange { get; set; }

    public override string ToString()
    {
        return $"{TransitionTime:yyyy-MM-dd HH:mm} (UTC{(NewOffset >= TimeSpan.Zero ? "+" : "")}{NewOffset:hh\\:mm}), " +
               $"DST change: {(DaylightSavingsChange >= TimeSpan.Zero ? "+" : "")}{DaylightSavingsChange:hh\\:mm}";
    }
}