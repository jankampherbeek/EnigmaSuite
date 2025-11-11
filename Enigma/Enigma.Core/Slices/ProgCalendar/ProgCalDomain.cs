// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Dtos;
using Enigma.Domain.References;

namespace Enigma.Core.Slices.ProgCalendar;

public record ProgCalMatch(ChartPoints ProgPoint, double Jd, double ProgPosition);

public enum DeclinationEvents
{
    ZeroDeclination = 0,
    MaxDeclination = 1,
    MinDeclination = 2,
    Oob = 3,
    InBounds = 4,
}

public enum DeclinationParallels
{
    Parallel = 1,
    ContraParallel = 2
}

public enum ProgressionTypes
{
    Secundary = 1,
    Transit = 2
}

/// <summary>
/// Chartpoijnt, combined with longitude
/// </summary>
/// <param name="Point">The chart point</param>
/// <param name="Longitude">Ecliptic longitude</param>
public record PointPosition(ChartPoints Point, double Longitude);

/// <summary>
/// Point that forms an aspect  
/// </summary>
/// <param name="ChartPoint">The ProgPoint that is throwing the aspect</param>
/// <param name="Aspect">The type of aspect</param>
/// <param name="Longitude">The position in ecliptical longitude</param>
public record ProgCalAspectPoint(ChartPoints ChartPoint, AspectTypes Aspect, double Longitude);


/// <summary>
/// An actually formed aspect
/// </summary>
/// <param name="ProgPoint">Progressive chartpoint</param>
/// <param name="RadixPoint">Radix chartpoint</param>
/// <param name="ProgPosition">Longitude of ProgPoint</param>
/// <param name="RadixLongitude">Longitude of RadixPoint</param>
/// <param name="Aspect">Type of aspect</param>
/// <param name="ProgType">Progression type</param>
/// <param name="Jd">Julian day when the aspect is exact</param>
public record ProgCalAspectMatch (
    ChartPoints ProgPoint,
    ChartPoints RadixPoint,
    double ProgPosition,
    double RadixLongitude,
    AspectTypes Aspect,
    ProgressionTypes ProgType,
    double Jd): ProgCalMatch(ProgPoint, Jd, ProgPosition);

/// <summary>
/// An actually formed declination event
/// </summary>
/// <param name="ProgPoint">Progresive chartpoint</param>
/// <param name="Declination">Position in declination</param>
/// <param name="DeclEvent">Type of declination event</param>
/// <param name="Jd">Julian day when the declination event is exact</param>
public record ProgCalDeclinationEventMatch(
    ChartPoints ProgPoint,
    double Declination,
    DeclinationEvents DeclEvent,
    double Jd): ProgCalMatch(ProgPoint, Jd, Declination);

/// <summary>
/// An acually formed declination parallel or contra parallel
/// </summary>
/// <param name="ProgPoint">Progressive chartpoint</param>
/// <param name="RadixPoint">Radix chartpoint</param>
/// <param name="ProgDeclination">Declination of ProgPoint</param>
/// <param name="RadixDeclination">Declination of RadixPoint</param>
/// <param name="DeclParallel">Type of declination parallel</param>
/// <param name="Jd">Julian day when the declination parallel is exact</param>
public record ProgCalDeclinationParallelMatch(
    ChartPoints ProgPoint,
    ChartPoints RadixPoint,
    double ProgDeclination,
    double RadixDeclination,
    DeclinationParallels DeclParallel,
    double Jd): ProgCalMatch(ProgPoint, Jd, ProgDeclination);

/// <summary>
/// Request for progressive calendar
/// </summary>
/// <param name="StartJd">First julian day number</param>
/// <param name="EndJd">Last julian day number</param>
/// <param name="CalcChart">Chart with calculated positions</param>
/// <param name="ProgTypes">Progression type</param>
/// <param name="ProgPoints">Progressive chartpoints to include</param>
/// <param name="RadixPoints">Radix chartpoints to include</param>
/// <param name="Aspects">Aspects to include</param>
/// <param name="DeclEvents">DeclEvents to include</param>
/// <param name="DeclParallels">DeclParallels to include</param>
public record ProgCalRequest(
    double StartJd,
    double EndJd,
    CalculatedChart CalcChart,
    List<ProgressionTypes> ProgTypes,
    List<ChartPoints> ProgPoints,
    List<ChartPoints> RadixPoints,
    List<AspectTypes> Aspects,
    List<DeclinationEvents> DeclEvents,
    List<DeclinationParallels> DeclParallels);

/// <summary>
/// Presetable version of a progressive calendar item
/// </summary>
/// <param name="DateTime">Date and time</param>
/// <param name="EventType">Type of event</param>
/// <param name="ProgPointGlyph">Glyph for the progressive point</param>
/// <param name="AspectGlyph">Optional glyph for the aspect</param>
/// <param name="RadixGlyph">Optional glyph for the radix point</param>
/// <param name="ProgPointPosition">Position, either longitude or declination</param>
/// <param name="SignGlyph">Optional glyph for the sign</param>
  public record PresentableProgCalItem(
      string DateTime,
      string EventType,
      char ProgPointGlyph,
      char AspectGlyph,
      char RadixGlyph,
      string ProgPointPosition,
      char SignGlyph
      );