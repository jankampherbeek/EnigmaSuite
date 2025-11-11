// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Reflection.Metadata;
using Enigma.Domain.Constants;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;

namespace Enigma.Core.Slices.ProgCalendar;

public static class ProgCalOrchestrator
{
    public static List<ProgCalMatch> DefineProgressiveCalendar(ProgCalRequest request)
    {
        var allMatches = new List<ProgCalMatch>();
        var sortedAspPoints =
            ProgCalSortAspectPoints.CreateSortedAspectPoints(request.CalcChart, request.ProgPoints, request.Aspects);
        var transitAspects = new List<ProgCalAspectMatch>();
        if (request.ProgTypes.Contains(ProgressionTypes.Transit))
        {
            transitAspects = FindTransitAspects(ProgressionTypes.Transit, sortedAspPoints, request.CalcChart,
                request.ProgPoints,
                request.StartJd,
                request.EndJd);
        }

        var secundaryAspects = FindSecundaryAspects(ProgressionTypes.Secundary, sortedAspPoints, request.CalcChart,
            request.ProgPoints,
            request.StartJd,
            request.EndJd);

        var aspects = transitAspects.Concat(secundaryAspects).ToList();
        aspects = aspects.OrderBy(asp => asp.Jd).ToList();
        var declinationEvents =
            FindDeclionationEvents(request.ProgPoints, request.StartJd, request.EndJd);
        var declinationParallels =
            FindDeclionationParallel(request.ProgPoints, request.CalcChart, request.StartJd, request.EndJd);
        allMatches.AddRange(aspects);
        allMatches.AddRange(declinationEvents);
        allMatches.AddRange(declinationParallels);
        allMatches = allMatches.OrderBy(x => x.Jd).ToList();
        return allMatches;
    }

    private static List<ProgCalAspectMatch> FindTransitAspects(ProgressionTypes progType,
        List<ProgCalAspectPoint> sortedAspPoints, CalculatedChart calcChart, List<ChartPoints> progPoints,
        double jdStart, double jdEnd)
    {
        var aspectsFound =
            AspectMoments.FindAspectMoments(progType, calcChart, sortedAspPoints, progPoints, jdStart, jdEnd);
        return aspectsFound;
    }

    private static List<ProgCalAspectMatch> FindSecundaryAspects(ProgressionTypes progType,
        List<ProgCalAspectPoint> sortedAspPoints, CalculatedChart calcChart, List<ChartPoints> progPoints,
        double jdStart, double jdEnd)
    {
        var jdRadix = calcChart.InputtedChartData.FullDateTime.JulianDayForEt;
        // convert jd's to secundary direction scale
        var jdStartSec = DefineSecundaryJd(jdRadix, jdStart);
        var jdEndSec = DefineSecundaryJd(jdRadix, jdEnd);
        // call AspectMoments.FindAspectMoments
        var secAspectsFound =
            AspectMoments.FindAspectMoments(progType, calcChart, sortedAspPoints, progPoints, jdStartSec, jdEndSec);
        // convert jd's for matches to jd in lifetime
        var realAspectsFound = new List<ProgCalAspectMatch>();
        foreach (var match in secAspectsFound)
        {
            var realJd = DefineRealJdFromSecundary(jdRadix, match.Jd);
            realAspectsFound.Add(new ProgCalAspectMatch(match.ProgPoint, match.RadixPoint, match.ProgPosition,
                match.RadixLongitude, match.Aspect, progType, realJd));
        }

        return realAspectsFound;
    }


    private static List<ProgCalDeclinationEventMatch> FindDeclionationEvents(List<ChartPoints> progPoints,
        double jdStart, double jdEnd)
    {
        var declEventsFound = DeclinationEventMoments.FindDeclinationEvents(progPoints,  jdStart, jdEnd);

        

        return declEventsFound;
    }

    private static List<ProgCalDeclinationParallelMatch> FindDeclionationParallel(List<ChartPoints> progPoints,
        CalculatedChart calcChart, double jdStart, double jdEnd)
    {
        var declParallelsFound =
            ParallelMoments.FindParallelMoments(ProgressionTypes.Transit, calcChart, progPoints, jdStart, jdEnd);
        return declParallelsFound;
    }

    private static double DefineSecundaryJd(double jdRadix, double jdProg)
    {
        var lengthInDays = jdProg - jdRadix;
        return jdRadix + lengthInDays / EnigmaConstants.TROPICAL_YEAR_IN_DAYS;
    }

    private static double DefineRealJdFromSecundary(double jdRadix, double jdProg)
    {
        var lengthInDays = jdProg - jdRadix;
        return jdRadix + lengthInDays * EnigmaConstants.TROPICAL_YEAR_IN_DAYS;
    }
}