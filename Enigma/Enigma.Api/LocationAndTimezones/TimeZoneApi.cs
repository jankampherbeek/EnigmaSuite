// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Ardalis.GuardClauses;
using Enigma.Core.LocationAndTimeZones;
using Enigma.Domain.Dtos;
using Serilog;

namespace Enigma.Api.LocationAndTimeZones;

/// <summary>Api to retrieve timzone and dst</summary>
public interface ITimeZoneApi
{
    /// <summary>Get the specifications for time zone and dst</summary>
    /// <param name="dateTime">Date and time</param>
    /// <param name="tzIndication">Indication of timezone according to IANA, e.g. Europe/Amsterdam</param>
    /// <returns>The timezone and dst info</returns>
    public ZoneInfo GetTimeZoneDst(DateTimeHms dateTime, string tzIndication);
}

/// <inheritdoc/>
public class TimeZoneApi(ITzHandler tzHandler): ITimeZoneApi
{
    /// <inheritdoc/>
    public ZoneInfo GetTimeZoneDst(DateTimeHms dateTime, string tzIndication)
    {
        if (tzIndication.Length >= 2) return tzHandler.CurrentTime(dateTime, tzIndication);
        var errorTxt = $"Indication for time zone should have at least 2 characters but is {tzIndication}";
        Log.Error(errorTxt);
        throw new ArgumentException(errorTxt);
    }
}

