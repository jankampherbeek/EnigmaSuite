// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;
using System.Linq;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Facades.Se;

namespace Enigma.Core.Slices.ProgCalendar;

/// <summary>
/// Find moments that a progressive point forms a parallel or contra-parallel with a radix point
/// </summary>
public static class ParallelMoments
{

    // TODO handle secundary
    
    private static readonly ICalcUtFacade CalcUtFacade = new CalcUtFacade();
    
    public static List<ProgCalDeclinationParallelMatch> FindParallelMoments(ProgressionTypes progType,
        CalculatedChart calcChart, List<ChartPoints> progPoints, double jdStart, double jdEnd)
    {
        var parallelMoments = new List<ProgCalDeclinationParallelMatch>();
        
        foreach (var progPoint in progPoints)
        {
            foreach (var radixPoint in calcChart.Positions)
            {
                if (!SupportsDeclination(progPoint) || !SupportsDeclination(radixPoint.Key)) continue;
                var initialStepSize = DefineInitialStepSize(progPoint);
                var radixDeclination = radixPoint.Value.Equatorial.DeviationPosSpeed.Position;
                var newParallelsFound = BinarySearchForParallelMoment(progType, progPoint, radixPoint.Key, radixDeclination,
                    jdStart, jdEnd, initialStepSize);
                parallelMoments.AddRange(newParallelsFound);
            }
        }        
        return parallelMoments;
    }

    private static bool SupportsDeclination(ChartPoints point)
    {
        var support = !(point.GetDetails().PointCat != PointCats.Angle && point.GetDetails().PointCat != PointCats.Common);
        if (point is ChartPoints.VulcanusCarteret or ChartPoints.PersephoneCarteret or ChartPoints.ApogeeCorrected) support = false;
        return support;
    }
    
    
    
    private static List<ProgCalDeclinationParallelMatch> BinarySearchForParallelMoment(ProgressionTypes progType, ChartPoints progPoint, 
        ChartPoints radixPoint,double radixDeclination, double jdStart, double jdEnd, double stepSize)
    {
        const double marginForJd = 0.000001;  // better than 0.1 second of time
        var newParallels = new List<ProgCalDeclinationParallelMatch>();
        var jdCurrent = jdStart - stepSize;  // start early to be able to check the first step
        
        while (jdCurrent <= jdEnd)
        {
            if (jdCurrent >= jdStart)
            {
                var jdNew = jdCurrent + stepSize;
                var declProgCurrent = CalcDeclination(progPoint, jdCurrent);
                var declProgNew = CalcDeclination(progPoint, jdNew);

                var parallelDetected = CheckForDeclinationCrossing(declProgCurrent, declProgNew, radixDeclination);
                
                if (parallelDetected)
                {
                    var parallelType = DeclinationParallels.ContraParallel;
                    if ((declProgCurrent > 0.0 && radixDeclination > 0.0) || (declProgCurrent < 0.0 && radixDeclination < 0.0) ) parallelType = DeclinationParallels.Parallel;

                    
                    
                    if (Math.Abs(jdNew - jdCurrent) < marginForJd)
                    {
                        newParallels.Add(new ProgCalDeclinationParallelMatch(
                            progPoint,
                            radixPoint,
                            progType,
                            declProgCurrent,
                            radixDeclination,
                            parallelType,
                            jdCurrent));
                        // no return yet, as more parallels can be found
                    }
                    else
                    {
                        newParallels.AddRange(BinarySearchForParallelMoment(progType, progPoint, radixPoint, radixDeclination,
                            jdCurrent, jdNew, stepSize / 10.0));
                                             
                    }
                }
            }
            jdCurrent += stepSize;
        }
        return newParallels;
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
    private static bool CheckForDeclinationCrossing(double declCurrent, double declNew, double targetDecl)
    {
        var targetAbs = Math.Abs(targetDecl);
        var declCurrentAbs = Math.Abs(declCurrent);
        var declNewAbs = Math.Abs(declNew);

        var distanceCurrent = DefineDistance(declCurrentAbs, targetAbs);
        var distanceNew = DefineDistance(declNewAbs, targetAbs);
        
        // Check if we crossed the target (distance changed from one side to the other)
        // Need a small margin to detect the crossing
        const double detectionMargin = 0.5;  // degrees
        
        if (distanceCurrent < detectionMargin || distanceNew < detectionMargin)
        {
            // Very close, check for actual crossing by sign change
            var diff1 = declCurrentAbs - targetAbs;
            var diff2 = declNewAbs - targetAbs;
            
            // If signs differ, we crossed
            if ((diff1 < 0 && diff2 > 0) || (diff1 > 0 && diff2 < 0))
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Define the shortest distance between two declinations
    /// </summary>
    private static double DefineDistance(double decl1, double decl2)
    {
        var distance = Math.Abs(decl1) - Math.Abs(decl2);
        return distance;
    }




    /// <summary>
    /// Calculate the declination for a given chart point at a specific Julian Day
    /// </summary>
    public static double CalcDeclination(ChartPoints factor, double jd)
    {
        var seId = factor.GetDetails().CalcId;
        const int flags = 2 + 2048;              // use SE, no speed, equatorial coordinates
        var positions = CalcUtFacade.PositionFromSe(jd, seId, flags);
        return positions[1];
    }



}