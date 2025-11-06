// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Dtos;
using Enigma.Domain.References;

namespace Enigma.Core.Slices.ProgCalendar;

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
/// <param name="ChartPoint">The ChartPoint that is throwing the aspect</param>
/// <param name="Aspect">The type of aspect</param>
/// <param name="Longitude">The position in ecliptical longitude</param>
public record ProgCalAspectPoint(ChartPoints ChartPoint, AspectTypes Aspect, double Longitude);


/// <summary>
/// An actually formed aspect
/// </summary>
/// <param name="ProgPoint">Progressive chartpoint</param>
/// <param name="RadixPoint">Radix chartpoint</param>
/// <param name="ProgLongitude">Longitude of ProgPoint</param>
/// <param name="RadixLongitude">Longitude of RadixPoint</param>
/// <param name="Aspect">Type of aspect</param>
/// <param name="ProgType">Progression type</param>
/// <param name="Jd">Julian day when the aspect is exact</param>
public record ProgCalAspectMatch(
    ChartPoints ProgPoint,
    ChartPoints RadixPoint,
    double ProgLongitude,
    double RadixLongitude,
    AspectTypes Aspect,
    ProgressionTypes ProgType,
    double Jd);

/// <summary>
/// An actually formed declination event
/// </summary>
/// <param name="ChartPoint">Progresive chartpoint</param>
/// <param name="Declination">Position in declination</param>
/// <param name="DeclEvent">Type of declination event</param>
/// <param name="Jd">Julian day when the declination event is exact</param>
public record ProgCalDeclinationEventMatch(
    ChartPoints ChartPoint,
    double Declination,
    DeclinationEvents DeclEvent,
    double Jd);

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
    double Jd);

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
/// Response for progressive calendar
/// </summary>
/// <param name="Aspects">Aspects found</param>
/// <param name="DeclEvents">DeclEvents found</param>
/// <param name="DeclParallels">DeclParallels found</param>
public record ProgCalResponse(
    List<ProgCalAspectMatch> Aspects,
    List<ProgCalDeclinationEventMatch> DeclEvents,
    List<ProgCalDeclinationParallelMatch> DeclParallels);    