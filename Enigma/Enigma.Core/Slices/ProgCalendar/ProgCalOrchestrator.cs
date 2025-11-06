// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Dtos;
using Enigma.Domain.References;

namespace Enigma.Core.Slices.ProgCalendar;

public static class ProgCalOrchestrator
{
    public static ProgCalResponse DefineProgressiveCalendar(ProgCalRequest request)
    {
        var aspects = FindAspects(request.CalcChart, request.ProgPoints, request.RadixPoints, request.Aspects,
            request.StartJd,
            request.EndJd);
        var declinationEvents =
            FindDeclionationEvents(request.ProgPoints, request.DeclEvents, request.StartJd, request.EndJd);
        var declinationParallels = FindDeclionationParallel(request.ProgPoints, request.RadixPoints,
            request.DeclParallels, request.StartJd, request.EndJd);

        return new ProgCalResponse(aspects, declinationEvents, declinationParallels);
    }

    private static List<ProgCalAspectMatch> FindAspects(CalculatedChart calcChart, List<ChartPoints> progPoints,
        List<ChartPoints> radixPoints,
        List<AspectTypes> aspects, double jdStart, double jdEnd)
    {
        var aspectsFound = new List<ProgCalAspectMatch>();
        var sortedaspPoints = ProgCalSortAspectPoints.CreateSortedAspectPoints(calcChart, progPoints, aspects);
        aspectsFound = AspectMoments.FindAspectMoments(calcChart, sortedaspPoints, progPoints, jdStart, jdEnd);
        return aspectsFound;
    }

    private static List<ProgCalDeclinationEventMatch> FindDeclionationEvents(List<ChartPoints> progPoints,
        List<DeclinationEvents> events, double jdStart, double jdEnd)
    {
        var declEventsFound = new List<ProgCalDeclinationEventMatch>();

        // todo find declination events

        return declEventsFound;
    }

    private static List<ProgCalDeclinationParallelMatch> FindDeclionationParallel(List<ChartPoints> progPoints,
        List<ChartPoints> radixPoints, List<DeclinationParallels> parallel, double jdStart, double jdEnd)
    {
        var declParallelsFound = new List<ProgCalDeclinationParallelMatch>();

        // todo find parallels

        return declParallelsFound;
    }
}