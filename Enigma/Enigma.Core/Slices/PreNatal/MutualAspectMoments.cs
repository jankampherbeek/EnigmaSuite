// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.References;
using Enigma.Facades.Se;

namespace Enigma.Core.Slices.PreNatal;

/// <summary>
/// Handle mutaul aspects for pre natal
/// </summary>
public static class MutualAspectMoments
{
   
  //  private static MutualConjunctionsOppositions mConOpp = new MutualConjunctionsOppositions();

    /// <summary>
    /// Find all mutual aspects for a given period.
    /// </summary>
    /// <param name="factors">All factors to include</param>
    /// <param name="aspects">All aspects to include</param>
    /// <param name="startJd">Julian day to start with</param>
    /// <param name="endJd">Julian day for end of period</param>
    /// <returns>List of aspects found</returns>
    public static List<MutualAspect> FindMutualAspectMoments(List<ChartPoints> factors, List<AspectTypes> aspects,
        double startJd, double endJd)
    {
        var mutualAspectMoments = new List<MutualAspect>();
 
        for (var i = 0; i < factors.Count; i++)
        {
            for (var j = i+1; j < factors.Count; j++)
            {
                var factor1 = factors[i];
                var factor2 = factors[j];
                var aspectsFound = FindAspectsForFactors(factor1, factor2, aspects, startJd, endJd);
                mutualAspectMoments.AddRange(aspectsFound);
            }
        }
        return mutualAspectMoments;
    }


    private static List<MutualAspect> FindAspectsForFactors(ChartPoints factor1, ChartPoints factor2, List<AspectTypes> aspects,
        double startJd, double endJd)
    {
        var aspectsFound = new List<MutualAspect>();
        var initialStepSize = DefineInitialStepSize(factor1, factor2);
        foreach (var aspect in aspects)
        {
            var newAspectsFound = BinarySearchForAspect(factor1, factor2, aspect, startJd, endJd, initialStepSize);
            aspectsFound.AddRange(newAspectsFound);
        }
        return aspectsFound;
    }


    private static List<MutualAspect> BinarySearchForAspect(ChartPoints factor1, ChartPoints factor2, AspectTypes aspect,
        double jdStart, double jdEnd, double stepSize)
    {
        const double marginForJd = 0.000001;            // better than 0.1 second of time
        var newAspects = new List<MutualAspect>();
        // if (aspect == AspectTypes.Conjunction)
        // {
        //     // return Math.Abs(distanceCurrent - distanceNew) < margin;
        //     var factors = new[] {factor1, factor2};
        //     newAspects.AddRange(mConOpp.FindAspects(factors, jdStart, jdEnd, aspect));
        // }
        // else if (aspect == AspectTypes.Opposition)
        // {
        //     var factors = new[] {factor1, factor2};
        //     newAspects.AddRange(mConOpp.FindAspects(factors, jdStart, jdEnd, aspect));
        // }
        // else
        // {
            var angle = (aspect.GetDetails().Angle);
            var jdCurrent = jdStart - stepSize; // start early to be able to check the first step
            while (jdCurrent <= jdEnd)
            {
                if (jdCurrent >= jdStart)
                {
                    var jdNew = jdCurrent + stepSize;
                    var longFactor1Current = LongitudeCalculator.CalcLongitude(factor1, jdCurrent);
                    var longFactor2Current = LongitudeCalculator.CalcLongitude(factor2, jdCurrent);
                    // var distanceCurrent = DefineDistance(longFactor1Current, longFactor2Current);
                    var longFactor1New = LongitudeCalculator.CalcLongitude(factor1, jdNew);
                    var longFactor2New = LongitudeCalculator.CalcLongitude(factor2, jdNew);
                    // var distanceNew = DefineDistance(longFactor1New, longFactor2New);
                    var factors1 = new[] { longFactor1Current, longFactor1New };
                    var factors2 = new[] { longFactor2Current, longFactor2New };
                    var aspectDetected = false;
                    if (aspect == AspectTypes.Conjunction)
                    {
                        aspectDetected = CheckForConjunction(factors1, factors2);
                    }
                    else if (aspect == AspectTypes.Opposition)
                    {
                        aspectDetected = CheckForOpposition(factors1, factors2);
                    }
                    else
                    {
                        aspectDetected = CheckForAspect(aspect, factors1, factors2, marginForJd);
                    }

                    if (aspectDetected)
                    {
                        if (Math.Abs(jdNew - jdCurrent) < marginForJd)
                        {
                            newAspects.Add(new MutualAspect(jdCurrent, factor1, factor2, longFactor1Current,
                                longFactor2Current, aspect));
                            // no return yet, as more aspects can be found, especially when the Moon is involved or in the case of retrogradation
                        }
                        else
                        {
                            newAspects.AddRange(BinarySearchForAspect(factor1, factor2, aspect, jdCurrent, jdNew,
                                stepSize / 10.0));
                        }
                    }
                }

                jdCurrent += stepSize;
            }
     //   }
        return newAspects;
    }

    /// <summary>
    /// Define intial stepsize based on ChartPoints involved
    /// </summary>
    /// <param name="factor1">The first factor</param>
    /// <param name="factor2">The second factor</param>
    /// <returns>A stepsize of 0.1 is the Moon is involved, else 1.0 if Sun, Mercury, Venus or Mars are involved, else 2.0</returns>
    private static double DefineInitialStepSize(ChartPoints factor1, ChartPoints factor2)
    {
        var fastFactors = new List<ChartPoints>
        {
            ChartPoints.Sun,
            ChartPoints.Mercury,
            ChartPoints.Venus,
            ChartPoints.Mars
        };
        
        if (factor1 == ChartPoints.Moon || factor2 == ChartPoints.Moon) return 0.2;
        if (fastFactors.Contains(factor1) || fastFactors.Contains(factor2)) return 2.0;
        return 4.0;
    }

    /// <summary>
    /// Define distance for aspects.
    /// </summary>
    /// <param name="longitude1">Longitudeof first factor</param>
    /// <param name="longitude2">Longitude of second factor</param>
    /// <returns>The absolute shortest distance between the given longitudes</returns>
    private static double DefineDistance(double longitude1, double longitude2)
    {
        var distance = Math.Abs(longitude1 - longitude2);
        if (distance > 360.0) distance -= 360.0;
        if (distance > 180.0) distance = 360.0 - distance;
        return distance;
    }

    private static bool CheckForAspect(AspectTypes aspect, double[] positionsFactor1, double[] positionsFactor2, double margin)
    {
        var angle = (aspect.GetDetails().Angle);
        var distanceCurrent = DefineDistance(positionsFactor1[0], positionsFactor2[0]);
        var distanceNew = DefineDistance(positionsFactor1[1], positionsFactor2[1]);
        return (Math.Abs(distanceCurrent) < angle && Math.Abs(distanceNew) > angle) ||
               (Math.Abs(distanceCurrent) > angle && Math.Abs(distanceNew) < angle);

    }

    private static bool CheckForConjunction(double[] positionsFactor1, double[] positionsFactor2)
    {
        if (Math.Abs(positionsFactor1[0] - positionsFactor2[0]) > 4.0) return false;
        var distanceCurrent = positionsFactor1[0] -  positionsFactor2[0];
        var distanceNew = positionsFactor1[1] - positionsFactor2[1];
        switch (distanceCurrent)
        {
            case < 0.0 when distanceNew > 0.0:
            case > 0.0 when distanceNew < 0.0:
                return true;
            default:
                return false;
        }
    }
 
    private static bool CheckForOpposition(double[] positionsFactor1, double[] positionsFactor2)
    {
        if (Math.Abs(positionsFactor1[0] - positionsFactor2[0]) > 184.0) return false;
        if (Math.Abs(positionsFactor1[0] - positionsFactor2[0]) < 176.0) return false;
        var distanceCurrent = positionsFactor1[0] -  positionsFactor2[0];
        var distanceNew = positionsFactor1[1] - positionsFactor2[1];
        switch (distanceCurrent)
        {
            case < 180.0 when distanceNew > 180.0:
            case > 1800.0 when distanceNew < 180.0:
                return true;
            default:
                return false;
        }
    }
    
}