// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using Enigma.Domain.Dtos;
using Enigma.Domain.Exceptions;
using Enigma.Domain.References;
using Serilog;

namespace Enigma.Frontend.Ui.Support;


/// <summary>Manages points that need to be excluded from research.</summary>
public interface IPointsExclusionManager
{
    /// <summary>Define points to exclude for a specific research method.</summary>
    /// <param name="researchMethod"/>
    /// <returns>Specification of points that need to be excluded using the given research method.</returns>
    public PointsToExclude DefineExclusions(ResearchMethods researchMethod);

    /// <summary>Define points to exclude for heliocentric calculations</summary>
    /// <returns>Specification of points that need to be excluded</returns>
    public PointsToExclude DefineHelioExclusions();

    /// <summary>Define points to exclude for the calculation of cycles</summary>
    /// <returns>Specification of points that need to be excluded</returns>
    public PointsToExclude DefineCycleExclusions();
}



/// <inheritdoc/>
public class PointsExclusionManager : IPointsExclusionManager
{
 
    /// <inheritdoc/>
    public PointsToExclude DefineExclusions(ResearchMethods researchMethod)
    {
        switch (researchMethod)
        {
            case ResearchMethods.CountPosInSigns: return ExclusionForEclipticCounting();
            case ResearchMethods.CountPosInHouses: return ExclusionHouses();
            case ResearchMethods.CountAspects: return ExclusionForAspectsCounting();
            case ResearchMethods.CountUnaspected: return ExclusionForUnAspectedCounting();
            case ResearchMethods.CountOccupiedMidpoints: return ExclusionForMidpoints();
            case ResearchMethods.CountHarmonicConjunctions: return ExclusionForHarmonics();
            case ResearchMethods.CountOob: return ExclusionForOob();
            default:
            {
                Log.Error("PointsExclusionManager.DefineExclusions(). Did not recognize researchMethod: {Method}", researchMethod);
                throw new EnigmaException("Wrong ResearchMethod");
            }
        }
    }

    public PointsToExclude DefineHelioExclusions()
    {
        return ExclusionForHeliocentric();
    }

    public PointsToExclude DefineCycleExclusions()
    {
        return ExclusionHouses();
    }


    private static PointsToExclude ExclusionForEclipticCounting()
    {
        List<ChartPoints> exclusionPoints = new()
        {
            ChartPoints.Vertex,
            ChartPoints.EastPoint,
            ChartPoints.ZeroAries
        };
        const bool excludeCusps = true;
        return new PointsToExclude(exclusionPoints, excludeCusps);
    }

    private static PointsToExclude ExclusionHouses()
    {
        List<ChartPoints> exclusionPoints = new()
        {
            ChartPoints.Vertex,
            ChartPoints.EastPoint,
            ChartPoints.ZeroAries,
            ChartPoints.Mc,
            ChartPoints.Ascendant,
            ChartPoints.EastPoint,
            ChartPoints.Vertex
        };
        const bool excludeCusps = true;
        return new PointsToExclude(exclusionPoints, excludeCusps);
    }

    private static PointsToExclude ExclusionForMidpoints()
    {
        List<ChartPoints> exclusionPoints = new()
        {
            ChartPoints.Vertex,
            ChartPoints.EastPoint
        };
        const bool excludeCusps = true;
        return new PointsToExclude(exclusionPoints, excludeCusps);
    }


    private static PointsToExclude ExclusionForHarmonics()
    {
        List<ChartPoints> exclusionPoints = new()
        {
            ChartPoints.Vertex,
            ChartPoints.EastPoint,
            ChartPoints.ZeroAries,
            ChartPoints.FortunaNoSect,
            ChartPoints.FortunaSect
        };
        const bool excludeCusps = true;
        return new PointsToExclude(exclusionPoints, excludeCusps);
    }

    private static PointsToExclude ExclusionForAspectsCounting()
    {
        List<ChartPoints> exclusionPoints = new()
        {
            ChartPoints.Vertex,
            ChartPoints.EastPoint
        };
        const bool excludeCusps = false;
        return new PointsToExclude(exclusionPoints, excludeCusps);
    }

    private static PointsToExclude ExclusionForUnAspectedCounting()
    {
        List<ChartPoints> exclusionPoints = new()
        {
            ChartPoints.Vertex,
            ChartPoints.EastPoint
        };
        const bool excludeCusps = true;
        return new PointsToExclude(exclusionPoints, excludeCusps);
    }

    private static PointsToExclude ExclusionForOob()
    {
        List<ChartPoints> exclusionPoints = new()
        {
            ChartPoints.Vertex,
            ChartPoints.EastPoint,
            ChartPoints.Ascendant,
            ChartPoints.Mc,
            ChartPoints.Sun,
            ChartPoints.MeanNode,
            ChartPoints.TrueNode
        };
        const bool excludeCusps = true;
        return new PointsToExclude(exclusionPoints, excludeCusps);
    }
    
    private static PointsToExclude ExclusionForHeliocentric()
    {
        List<ChartPoints> exclusionPoints = new()
        {
            ChartPoints.Vertex,
            ChartPoints.EastPoint,
            ChartPoints.FortunaSect,
            ChartPoints.Ascendant,
            ChartPoints.Mc,
            ChartPoints.Sun,
            ChartPoints.Moon,
            ChartPoints.MeanNode,
            ChartPoints.TrueNode,
            ChartPoints.ApogeeMean,
            ChartPoints.ApogeeCorrected,
            ChartPoints.ApogeeDuval,
            ChartPoints.ApogeeInterpolated
        };
        const bool excludeCusps = true;
        return new PointsToExclude(exclusionPoints, excludeCusps);
    }
}
