// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Exceptions;
using Enigma.Domain.Interfaces;
using Enigma.Domain.Points;
using Enigma.Domain.Research;
using Serilog;

namespace Enigma.Frontend.Helpers.Support;

/// <inheritdoc/>
public class PointsExclusionManager : IPointsExclusionManager
{

    /// <inheritdoc/>
    public PointsToExclude DefineExclusions(ResearchMethods researchMethod)
    {
        switch (researchMethod)
        {
            case ResearchMethods.CountPosInSigns: return ExclusionForEclipticCounting();
            case ResearchMethods.CountPosInHouses: return ExclusionForHousesOnly();
            case ResearchMethods.CountAspects: return ExclusionForAspectsCounting();
            case ResearchMethods.CountUnaspected: return ExclusionForUnAspectedCounting();
            case ResearchMethods.CountOccupiedMidpoints: return ExclusionForMidpoints();
            case ResearchMethods.CountHarmonicConjunctions: return ExclusionForHarmonics();
            default:
                {
                    string errorTxt = "PointsExclusionManager.DefineExclusions(). Did not recognize researchMethod: " + researchMethod;
                    Log.Error(errorTxt);
                    throw new EnigmaException(errorTxt);
                }
        }
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

    private static PointsToExclude ExclusionForHousesOnly()
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
}