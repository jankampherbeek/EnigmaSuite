﻿// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.AstronCalculations;
using Enigma.Domain.Enums;

namespace Enigma.Frontend.Helpers.Interfaces;

// interfaces for parsers that handle inputted data.

/// <summary>Parse, validate and convert input for a date.</summary>
public interface IDateInputParser
{
    public bool HandleGeoLong(string inputDate, Calendars calendar, YearCounts yearCount, out FullDate? fullDate);
}

/// <summary>Parse, validate and convert input for geographic longitude.</summary>
public interface IGeoLatInputParser
{
    public bool HandleGeoLat(string inputGeoLat, Directions4GeoLat direction, out FullGeoLatitude fullGeoLatitude);
}


/// <summary>Parse, validate and convert input for geographic longitude.</summary>
public interface IGeoLongInputParser
{
    public bool HandleGeoLong(string inputGeoLong, Directions4GeoLong direction, out FullGeoLongitude fullGeoLongitude);
}

/// <summary>Parse, validate and convert input for a date.</summary>
public interface ITimeInputParser
{
    public bool HandleTime(string inputTime, TimeZones timeZone, double lmtOffset, out FullTime? fullTime);
}