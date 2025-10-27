// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.References;

namespace Enigma.Core.Slices.PreNatal;

/// <summary>
/// Handle moments of ingresses in signs
/// </summary>
public static class IngressMoments
{
    
    /// <summary>
    /// Find the ingresses in a sign for a given period.
    /// </summary>
    /// <param name="factors">The celestial points to use</param>
    /// <param name="startJd">Julian Day Number to start with</param>
    /// <param name="endJd">Julian Day Number that ends the period</param>
    /// <returns>Sorted list of ingresses that were found</returns>
    public static List<Ingress> FindIngressMoments(List<ChartPoints> factors, double startJd, double endJd)
    {
        var ingresses = new List<Ingress>();
        foreach (var factor in factors)
        {
            var ingressesForFactor = SearchForIngress(factor, startJd, endJd, 1.0); 
            ingresses.AddRange(ingressesForFactor);
        }
        return ingresses;
    }

    
    private static List<Ingress> SearchForIngress(ChartPoints factor, double jdStart, double jdEnd, double stepSize)
    {
        const double margin = 0.000001;            // better than 0.1 second of time

        var newIngresses = new List<Ingress>();
            
            var jdCurrent = jdStart - stepSize; // start early to be able to check the first step
            while (jdCurrent <= jdEnd)
            {
                Console.WriteLine(jdCurrent);
                if (jdCurrent >= jdStart)
                {
                    var jdNew = jdCurrent + stepSize;
                    var longitude1 = LongitudeCalculator.CalcLongitude(factor, jdCurrent);
                    var longitude2 = LongitudeCalculator.CalcLongitude(factor, jdNew);
                    var sign1 = (int)(longitude1 / 30) + 1;
                    var sign2 = (int)(longitude2 / 30) + 1;
                    if (sign1 != sign2)
                    {
                     //   var newSign = longitude1 < longitude2 ? sign2 : sign1;  // check for retrogradation
                     var newSign = sign2; 
                     if (sign1 > sign2 && !(sign1 == 12 && sign2 ==1)) newSign = sign1;
                     
                     
                     if (stepSize < margin)
                        {
                            newIngresses.Add(new Ingress(jdCurrent, factor, newSign));
                        }
                        else if ((longitude1 % 30.0) < margin)
                        {
                            newIngresses.Add(new Ingress(jdCurrent, factor, newSign));
                        }
                        else
                        {
                            newIngresses.AddRange(SearchForIngress(factor, jdCurrent, jdNew, stepSize / 10.0));
                        }
                    }
                }
                jdCurrent += stepSize;
            }
        return newIngresses;
    }
    
    
}