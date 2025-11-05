// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Facades.Se;

namespace Enigma.Core.Slices.VenusStarPoint;


/// <summary>
/// Define the range of valid JD's for the Venus Star Point
/// </summary>
public class DefineJdRange(IJulDayFacade julDayFacade, ExactConjunctionDate exactConjunctionDate)
{
    private const double MAX_VENUS_PERIOD = 584.0;
    private const double MIN_VENUS_PERIOD = 583.0;
    private const Calendars CAL = Calendars.Gregorian;
    private const double JD_TOLERANCE = 0.1; // Tolerance for considering Julian Days as approximately the same (about 2.4 hours)
    private const int REPEAT_CALCULATION = 14;
    
    public List<Tuple<double, VenusPhenomena>> JdRange(double birthJd)
    {
        var jdsFound = new List<Tuple<double, VenusPhenomena>>();
        
        
        // calculate all occurrences from starting with 584 days before birthJd, and remember the value for VenusPhenomena
        var lastJdInferior= birthJd - 2 * MAX_VENUS_PERIOD;
        var lastJdSuperior = lastJdInferior;        // start JD for inferior and superior conjunctions is the same
        // calculate inferior conjunctiopns
        for (var i = 0; i < REPEAT_CALCULATION; i++)
        {
            var year = julDayFacade.DateTimeFromJd(lastJdInferior + MIN_VENUS_PERIOD, CAL).Year;
            var jdNewYear = julDayFacade.JdFromSe(new SimpleDateTime(year, 1, 1, 0, CAL));
            var yearFraction = YearFraction.CalcYearAndFraction(year, lastJdInferior + MIN_VENUS_PERIOD, jdNewYear);
            var jdPhenonomenon = DefineJdPhenomenon(yearFraction, VenusPhenomena.InferiorConjunction);
            lastJdInferior = jdPhenonomenon.Item1;        
            jdsFound.Add(jdPhenonomenon);
        }
        // calculate superior conjunctions
        for (var i = 0; i < REPEAT_CALCULATION; i++)
        {
            var year = julDayFacade.DateTimeFromJd(lastJdSuperior + MIN_VENUS_PERIOD, CAL).Year;
            var jdNewYear = julDayFacade.JdFromSe(new SimpleDateTime(year, 1, 1, 0, CAL));
            var yearFraction = YearFraction.CalcYearAndFraction(year, lastJdSuperior + MIN_VENUS_PERIOD, jdNewYear);
            var jdPhenonomenon = DefineJdPhenomenon(yearFraction, VenusPhenomena.SuperiorConjunction);
            lastJdSuperior = jdPhenonomenon.Item1;        
            jdsFound.Add(jdPhenonomenon);
        }
        
        jdsFound.Sort();
        jdsFound = RemoveDuplicateJds(jdsFound);
        
        // Return appropriate subset 
        return GetSubsetBasedOnPrenatal(jdsFound, birthJd);
    }
    
    /// <summary>
    /// Removes duplicate entries where Julian Days are approximately the same (within tolerance).
    /// If multiple items have approximately the same JD, only the first one is kept.
    /// </summary>
    /// <param name="jdsList">Sorted list of Julian Day and Venus phenomena tuples</param>
    /// <returns>List with duplicates removed</returns>
    private static List<Tuple<double, VenusPhenomena>> RemoveDuplicateJds(List<Tuple<double, VenusPhenomena>> jdsList)
    {
        if (jdsList.Count <= 1)
            return jdsList;

        var result = new List<Tuple<double, VenusPhenomena>> { jdsList[0] };  // Add the first item

        for (var i = 1; i < jdsList.Count; i++)
        {
            var currentJd = jdsList[i].Item1;
            var previousJd = jdsList[i - 1].Item1;
            
            // If the current JD is not approximately the same as the previous one, add it
            if (Math.Abs(currentJd - previousJd) > JD_TOLERANCE)
            {
                result.Add(jdsList[i]);
            }
        }
        return result;
    }
    
    /// <summary>
    /// Returns a subset of the list. .
    /// </summary>
    /// <param name="jdsList">Sorted list of Julian Day and Venus phenomena tuples</param>
    /// <param name="birthJd">Birth Julian Day</param>
    /// <returns>Subset of the list</returns>
    private static List<Tuple<double, VenusPhenomena>> GetSubsetBasedOnPrenatal(List<Tuple<double, VenusPhenomena>> jdsList, double birthJd)
    {
        if (jdsList.Count == 0)
            return jdsList;
            // Find the last JD before birth
            var lastBeforeBirthIndex = -1;
            for (var i = 0; i < jdsList.Count; i++)
            {
                if (jdsList[i].Item1 < birthJd)
                {
                    lastBeforeBirthIndex = i;
                }
                else
                {
                    break; // Found first JD after birth, stop searching
                }
            }
            
            if (lastBeforeBirthIndex == -1)
            {
                // No JDs before birth, return first 5 items
                return jdsList.Take(5).ToList();
            }
            
            // Start from the last JD before birth and include up to 5 items total
            var startIndex = Math.Max(0, lastBeforeBirthIndex - 2);
            var endIndex = Math.Min(jdsList.Count, startIndex + 5);
            return jdsList.GetRange(startIndex, endIndex - startIndex);
 
    }
    
    private Tuple<double, VenusPhenomena> DefineJdPhenomenon(double yearFraction, VenusPhenomena phenomenon)
    {
        var estimatedJd = EstimatedConjunctionDate.CalcEstimatedConjunctionDate(yearFraction, phenomenon);
        var exactJd = exactConjunctionDate.CalculateConjunctiondate(estimatedJd, phenomenon);
        return new Tuple<double, VenusPhenomena>(exactJd, phenomenon); 
    }
    
}