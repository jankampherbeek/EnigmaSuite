// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Constants;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;

namespace Enigma.Core.Slices.ProgCalendar;

public static class ProgCalOrchestrator
{
    public static ProgCalResponse DefineProgressiveCalendar(ProgCalRequest request)
    {
        var allMatches = new List<ProgCalMatch>();
        var allPeriodMatches = new List<ProgCalPeriodMatch>();
        // ToDO populate allPeriodMatches


        var sortedAspPoints =
            ProgCalSortAspectPoints.CreateSortedAspectPoints(request.CalcChart, request.TransitPoints, request.Aspects);
        var transitAspects = new List<ProgCalAspectMatch>();
        var transitPeriodAspects = new List<ProgCalAspectPeriodMatch>();
        if (request.ProgTypes.Contains(ProgressionTypes.Transit))
        {
            transitAspects = FindTransitAspects(ProgressionTypes.Transit, sortedAspPoints, request.CalcChart,
                request.TransitPoints,
                request.StartJd,
                request.StartJd + request.PeriodLength);
            transitPeriodAspects = FindTransitPeriodAspects(ProgressionTypes.Transit, sortedAspPoints,
                request.CalcChart,
                request.TransitPoints,
                request.StartJd,
                request.StartJd + request.PeriodLength,
                request.OrbAspects);
        }

        var secundaryAspects = new List<ProgCalAspectMatch>();
        var secundaryPeriodAspects = new List<ProgCalAspectPeriodMatch>();
        if (request.ProgTypes.Contains(ProgressionTypes.Secundary))
        {
            secundaryAspects = FindSecundaryAspects(ProgressionTypes.Secundary, sortedAspPoints, request.CalcChart,
                request.SecundaryPoints,
                request.StartJd,
                request.StartJd + request.PeriodLength);
            secundaryPeriodAspects = FindSecundaryPeriodAspects(ProgressionTypes.Secundary, sortedAspPoints,
                request.CalcChart,
                request.SecundaryPoints,
                request.StartJd,
                request.StartJd + request.PeriodLength,
                request.OrbAspects);
        }


        var aspects = transitAspects.Concat(secundaryAspects).ToList();
        aspects = aspects.OrderBy(asp => asp.Jd).ToList();
        allMatches.AddRange(aspects);
        

        if (request.DeclParallels.Contains(DeclinationParallels.Parallel))
        {
            var declinationParallels =
                FindDeclionationParallel(request.TransitPoints, request.CalcChart, request.StartJd, request.StartJd + request.PeriodLength);
            allMatches.AddRange(declinationParallels);
            if (request.ProgTypes.Contains(ProgressionTypes.Secundary))
            {
                var secundaryDeclinationParallels =
                    FindSecundaryDeclionationParallel(request.SecundaryPoints, request.CalcChart, request.StartJd,
                        request.StartJd + request.PeriodLength);
                allMatches.AddRange(secundaryDeclinationParallels);                
            }
        }

        allMatches = allMatches.OrderBy(x => x.Jd).ToList();

        allPeriodMatches.AddRange(transitPeriodAspects);
        allPeriodMatches.AddRange(secundaryPeriodAspects);
        allPeriodMatches = allPeriodMatches.OrderBy(x => x.JdStart).ToList();
        return new ProgCalResponse(allMatches, allPeriodMatches);
    }

    private static List<ProgCalAspectMatch> FindTransitAspects(ProgressionTypes progType,
        List<ProgCalAspectPoint> sortedAspPoints, CalculatedChart calcChart, List<ChartPoints> progPoints,
        double jdStart, double jdEnd)
    {
        var aspectsFound =
            AspectMoments.FindAspectMoments(progType, calcChart, sortedAspPoints, progPoints, jdStart, jdEnd);
        return aspectsFound;
    }

    private static List<ProgCalAspectPeriodMatch> FindTransitPeriodAspects(ProgressionTypes progType,
        List<ProgCalAspectPoint> sortedAspPoints, CalculatedChart calcChart, List<ChartPoints> progPoints,
        double jdStart, double jdEnd, double orb)
    {
        var periodsFound =
            AspectMoments.FindAspectPeriods(progType, calcChart, sortedAspPoints, progPoints, jdStart, jdEnd, orb);
        return periodsFound;
    }


    private static List<ProgCalAspectMatch> FindSecundaryAspects(ProgressionTypes progType,
        List<ProgCalAspectPoint> sortedAspPoints, CalculatedChart calcChart, List<ChartPoints> progPoints,
        double jdStart, double jdEnd)
    {
        var jdRadix = calcChart.InputtedChartData.FullDateTime.JulianDayForEt;
        var jdStartSec = DefineSecundaryJd(jdRadix, jdStart);
        var jdEndSec = DefineSecundaryJd(jdRadix, jdEnd);
        
        var secAspectsFound =
            AspectMoments.FindAspectMoments(progType, calcChart, sortedAspPoints, progPoints, jdStartSec, jdEndSec);
        var realAspectsFound = new List<ProgCalAspectMatch>();
        foreach (var match in secAspectsFound)
        {
            var realJd = DefineRealJdFromSecundary(jdRadix, match.Jd);
            realAspectsFound.Add(new ProgCalAspectMatch(match.ProgPoint, match.RadixPoint, match.ProgPosition,
                match.RadixLongitude, match.Aspect, progType, realJd));
        }

        return realAspectsFound;
    }

    private static List<ProgCalAspectPeriodMatch> FindSecundaryPeriodAspects(ProgressionTypes progType,
        List<ProgCalAspectPoint> sortedAspPoints, CalculatedChart calcChart, List<ChartPoints> progPoints,
        double jdStart, double jdEnd, double orb)
    {
        var jdRadix = calcChart.InputtedChartData.FullDateTime.JulianDayForEt;
        // convert jd's to secundary direction scale
        var jdStartSec = DefineSecundaryJd(jdRadix, jdStart);
        var jdEndSec = DefineSecundaryJd(jdRadix, jdEnd);
        var secPeriodsFound = AspectMoments.FindAspectPeriods(progType, calcChart, sortedAspPoints, progPoints,
            jdStartSec, jdEndSec, orb);
        var realPeriodsFound = new List<ProgCalAspectPeriodMatch>();
        foreach (var match in secPeriodsFound)
        {
            var realJdStart = DefineRealJdFromSecundary(jdRadix, match.JdStart);
            var realJdEnd = DefineRealJdFromSecundary(jdRadix, match.JdEnd);
            realPeriodsFound.Add(new ProgCalAspectPeriodMatch(match.ProgPoint, match.RadixPoint, match.Aspect,
                match.ProgType, realJdStart, realJdEnd));
        }

        return realPeriodsFound;
    }


    private static List<ProgCalDeclinationParallelMatch> FindDeclionationParallel(List<ChartPoints> progPoints,
        CalculatedChart calcChart, double jdStart, double jdEnd)
    {
        var declParallelsFound =
            ParallelMoments.FindParallelMoments(ProgressionTypes.Transit, calcChart, progPoints, jdStart, jdEnd);
        return declParallelsFound;
    }

    private static List<ProgCalDeclinationParallelMatch> FindSecundaryDeclionationParallel(List<ChartPoints> progPoints,
        CalculatedChart calcChart, double jdStart, double jdEnd)
    {
        var jdRadix = calcChart.InputtedChartData.FullDateTime.JulianDayForEt;
        // convert jd's to secundary direction scale
        var jdStartSec = DefineSecundaryJd(jdRadix, jdStart);
        var jdEndSec = DefineSecundaryJd(jdRadix, jdEnd);
        var secParallelsFound =
            ParallelMoments.FindParallelMoments(ProgressionTypes.Secundary, calcChart, progPoints, jdStartSec,
                jdEndSec);
        // convert jd's for matches to jd in lifetime
        var realParallelsFound = new List<ProgCalDeclinationParallelMatch>();
        foreach (var match in secParallelsFound)
        {
            var realJd = DefineRealJdFromSecundary(jdRadix, match.Jd);
            realParallelsFound.Add(new ProgCalDeclinationParallelMatch(match.ProgPoint, match.RadixPoint,
                ProgressionTypes.Secundary, match.ProgPosition,
                match.RadixDeclination, match.DeclParallel, realJd));
        }

        return realParallelsFound;
    }

    private static double DefineSecundaryJd(double jdRadix, double jdProg)
    {
        var lengthInDays = jdProg - jdRadix;
        return jdRadix + lengthInDays / EnigmaConstants.TROPICAL_YEAR_IN_DAYS;
    }

    private static double DefineRealJdFromSecundary(double jdRadix, double jdProg)
    {
        var lengthInDays = jdProg - jdRadix;
        var realJd = jdRadix + lengthInDays * EnigmaConstants.TROPICAL_YEAR_IN_DAYS;
        return realJd;
    }
}