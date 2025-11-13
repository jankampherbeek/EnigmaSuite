// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Calc;
using Enigma.Domain.Constants;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Facades.Se;

namespace Enigma.Core.Slices.ProgCalendar;

/// <summary>
/// Find moments that a declination reaches its maximum, becomes zero, becomes OOB or becomes in bounds.
/// </summary>
public static class DeclinationEventMoments
{
    
    private static readonly ICalcUtFacade CalcUtFacade = new CalcUtFacade();

    public static List<ProgCalDeclinationEventMatch> FindDeclinationEvents(List<ChartPoints> progPoints, double jdStart, double jdEnd)
    {
        var declEventMoments = new List<ProgCalDeclinationEventMatch>();

        foreach (var progPoint in progPoints)
        {
                var initialStepSize = DefineInitialStepSize(progPoint);
                var eventsZeroDeclFound = BinarySearchForDeclEventMoment(DeclinationEvents.ZeroDeclination, progPoint,
                    jdStart, jdEnd, initialStepSize);
                declEventMoments.AddRange(eventsZeroDeclFound);
                var eventsMaxDeclFound = BinarySearchForDeclEventMoment(DeclinationEvents.MaxDeclination, progPoint,
                    jdStart, jdEnd, initialStepSize);
                declEventMoments.AddRange(eventsMaxDeclFound);
                var eventsMinDeclFound = BinarySearchForDeclEventMoment(DeclinationEvents.MinDeclination, progPoint,
                    jdStart, jdEnd, initialStepSize);
                declEventMoments.AddRange(eventsMinDeclFound);
                var eventsOobFound = BinarySearchForDeclEventMoment(DeclinationEvents.Oob, progPoint,
                    jdStart, jdEnd, initialStepSize);
                declEventMoments.AddRange(eventsOobFound);
                var eventsInBoundsFound = BinarySearchForDeclEventMoment(DeclinationEvents.InBounds, progPoint,
                    jdStart, jdEnd, initialStepSize);
                declEventMoments.AddRange(eventsInBoundsFound);
                
        }
        return declEventMoments;
    }


    private static List<ProgCalDeclinationEventMatch> BinarySearchForDeclEventMoment(DeclinationEvents eventType, ChartPoints progPoint,
        double jdStart, double jdEnd, double stepSize)
    {
        
        const double marginForJd = 0.000001; 
        const double marginForDeclination = 0.0000001;

        var newEvents = new List<ProgCalDeclinationEventMatch>();
        if (!SupportsDeclination(progPoint)) return newEvents;
        
        var jdCurrent = jdStart - stepSize; // start early to be able to check the first step
        var jdPrevious = jdCurrent - stepSize * 2;
        var obliquity = CalcObliquity(jdCurrent);        
        while (jdCurrent <= jdEnd)
        {
            if (jdCurrent >= jdStart)
            {
                var jdNew = jdCurrent + stepSize;
                var declProgCurrent = CalcDeclination(progPoint, jdCurrent);
                var declProgNew = CalcDeclination(progPoint, jdNew);
                var declProgPrevious = CalcDeclination(progPoint, jdPrevious);
                var eventDetected = CheckForDeclinationEvent(eventType,  declProgPrevious, declProgCurrent, declProgNew, obliquity);

                if (eventDetected)
                {
                    if (Math.Abs(jdNew - jdCurrent) < marginForJd)
                    {
                        var newEvent = new ProgCalDeclinationEventMatch(
                            progPoint,
                            declProgCurrent,
                            eventType,
                            jdCurrent);
                        AddEventIfUnique(newEvents, newEvent, marginForJd, marginForDeclination);
                        break;
                    }
                    else
                    {
                        var refinedEvents = BinarySearchForDeclEventMoment(eventType, progPoint, jdCurrent, jdNew, stepSize / 10.0);
                        foreach (var refinedEvent in refinedEvents)
                        {
                            AddEventIfUnique(newEvents, refinedEvent, marginForJd, marginForDeclination);
                        }
                        jdPrevious = jdNew - stepSize;
                        jdCurrent = jdNew;
                        continue;
                    }
                }
            }
            jdCurrent += stepSize;
            jdPrevious += stepSize;           
        }
        return newEvents;
    }

    /// <summary>
    /// Define initial stepsize based on the ProgPoint involved
    /// </summary>
    /// <param name="factor">The progressive chart point</param>
    /// <returns>Step size: 0.2 for Moon, 2.0 for fast planets, 4.0 for slow planets</returns>
    private static double DefineInitialStepSize(ChartPoints factor)
    {
        var fastFactors = new List<ChartPoints>
        {
            ChartPoints.Sun,
            ChartPoints.Mercury,
            ChartPoints.Venus,
            ChartPoints.Mars
        };

        if (factor == ChartPoints.Moon) return 0.2;
        if (fastFactors.Contains(factor)) return 2.0;
        return 4.0;
    }

    /// <summary>
    /// Check if a progressive point crosses a target declination between two positions
    /// </summary>
    private static bool CheckForDeclinationEvent(DeclinationEvents eventType, double declPrevious, double declCurrent, double declNew, double obliquity)
    {
        return eventType switch
        {
            DeclinationEvents.ZeroDeclination => Math.Abs(declCurrent - 0.0) < 0.000001,
            DeclinationEvents.MaxDeclination => declPrevious < declCurrent && declNew < declCurrent,
            DeclinationEvents.MinDeclination => declPrevious > declCurrent && declNew > declCurrent,
            DeclinationEvents.Oob => Math.Abs(declPrevious) < obliquity && Math.Abs(declCurrent) > obliquity,
            DeclinationEvents.InBounds => Math.Abs(declPrevious) > obliquity && Math.Abs(declCurrent) < obliquity,
            _ => false
        };
    }

    /// <summary>
    /// Define the shortest distance between two declinations
    /// </summary>
    private static double DefineDistance(double decl1, double decl2)
    {
        var distance = Math.Abs(decl1 - decl2);
        return distance;
    }




    /// <summary>
    /// Calculate the declination for a given chart point at a specific Julian Day
    /// </summary>
    private static double CalcDeclination(ChartPoints factor, double jd)
    {
        var seId = factor.GetDetails().CalcId;
        const int flags = 2 + 2048; // use SE, no speed, equatorial coordinates
        var positions = CalcUtFacade.PositionFromSe(jd, seId, flags);
        return positions[1];
    }


    private static double CalcObliquity(double jd)
    {
        const int flags = 0;
        double[] positions = CalcUtFacade.PositionFromSe(jd, EnigmaConstants.SE_ECL_NUT, flags);
        var useTrueObliquity = true;
        return useTrueObliquity ? positions[0] : positions[1];
    }
    
    private static bool SupportsDeclination(ChartPoints point)
    {
        var support = !(point.GetDetails().PointCat != PointCats.Angle && point.GetDetails().PointCat != PointCats.Common);
        if (point is ChartPoints.VulcanusCarteret or ChartPoints.PersephoneCarteret or ChartPoints.ApogeeCorrected) support = false;
        return support;
    }

    private static void AddEventIfUnique(List<ProgCalDeclinationEventMatch> events, ProgCalDeclinationEventMatch newEvent, double marginForJd, double marginForDeclination)
    {
        var duplicate = events.Any(existing =>
            existing.ProgPoint == newEvent.ProgPoint &&
            existing.DeclEvent == newEvent.DeclEvent &&
            Math.Abs(existing.Jd - newEvent.Jd) < marginForJd &&
            Math.Abs(existing.Declination - newEvent.Declination) < marginForDeclination);

        if (!duplicate)
        {
            events.Add(newEvent);
        }
    }
}