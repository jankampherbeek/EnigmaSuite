// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Dtos;

namespace Enigma.Core.Analysis;


/// <summary>Helper class for the calculation of distances.</summary>
public interface ICalculatedDistance
{
    /// <summary>Calculates the shortest distance between two positions.</summary>
    /// <remarks>PRE: pos1 and pos2 &gt;= 0.0 and pos1 and pos2 &lt; 360.0</remarks>
    /// <param name="pos1">First position.</param>
    /// <param name="pos2">Second position.</param>
    /// <returns>Shortest distance between pos1 and pos2.</returns>
    public double ShortestDistance(double pos1, double pos2);
    
    /// <summary>Calculates the shortest distance between several combinations of two points.</summary>
    /// <param name="allPoints">List of points.</param>
    /// <returns>List of shortest distances between each pair of points.</returns>
    public List<DistanceBetween2Points> ShortestDistances(List<PositionedPoint> allPoints);

    /// <summary>Calculates the shortest distance in declination between several combinations of two points.</summary>
    /// <param name="allPoints">List of points.</param>
    /// <returns>List of shortest distances in declination between each pair of points.</returns>
    public List<DistanceBetween2Points> ShortestDistancesInDeclination(List<PositionedPoint> allPoints);
    
    /// <summary>Calculates the shortest distances between several combinations of two points,
    /// the first point is not a cusp (but could be Mc or Asc), the second point is a cusp.</summary>
    /// <param name="allPoints">All points except the cusps.</param>
    /// <param name="allCusps">The cusps.</param>
    /// <returns>List of shortest distances between a non-cusp (first position in distance) and a cusp (second position).</returns>
    public List<DistanceBetween2Points> ShortestDistanceBetweenPointsAndCusps(IEnumerable<PositionedPoint> allPoints, List<PositionedPoint> allCusps);
}

/// <inheritdoc/>
public class CalculatedDistance : ICalculatedDistance
{
    private const double Zero = 0.0;
    private const double SemiCircle = 180.0;
    private const double FullCircle = 360.0;
    
    /// <inheritdoc/>
    public double ShortestDistance(double pos1, double pos2)
    {
        if (pos1 < Zero || pos2 < Zero || pos1 >= FullCircle || pos2 >= FullCircle) 
            throw new ArgumentException("Wrong input for shortestdistance");
        return NormalizeShortestDistance(pos1 - pos2);
    }

    /// <inheritdoc/>
    public List<DistanceBetween2Points> ShortestDistances(List<PositionedPoint> allPoints)
    {
        List<DistanceBetween2Points> allDistances = new();
        int count = allPoints.Count;
        for (int i = 0; i < count; i++)
        {
            PositionedPoint pointPos1 = allPoints[i];
            for (int j = i + 1; j < count; j++)
            {
                var pointPos2 = allPoints[j];
                double distance = NormalizeShortestDistance(pointPos1.Position - pointPos2.Position);
                allDistances.Add(new DistanceBetween2Points(pointPos1, pointPos2, distance));
            }
        }
        return allDistances;
    }

    /// <inheritdoc/>
    public List<DistanceBetween2Points> ShortestDistancesInDeclination(List<PositionedPoint> allPoints)
    {
        List<DistanceBetween2Points> allDistances = new();
        int count = allPoints.Count;
        for (int i = 0; i < count; i++)
        {
            PositionedPoint pointPos1 = allPoints[i];
            for (int j = i + 1; j < count; j++)
            {
                var pointPos2 = allPoints[j];
                double distance = Math.Abs(pointPos1.Position - pointPos2.Position);
                allDistances.Add(new DistanceBetween2Points(pointPos1, pointPos2, distance));
            }
        }
        return allDistances;
    }

    /// <inheritdoc/>
    public List<DistanceBetween2Points> ShortestDistanceBetweenPointsAndCusps(IEnumerable<PositionedPoint> allPoints, List<PositionedPoint> allCusps)
    {
        return (from point in allPoints 
            from cusp in allCusps 
            let distance = NormalizeShortestDistance(point.Position - cusp.Position) 
            select new DistanceBetween2Points(point, cusp, distance)).ToList();
    }


    private static double NormalizeShortestDistance(double initialValue)
    {
        double distance = initialValue;
        if (distance < Zero) distance += FullCircle;
        if (distance > SemiCircle) distance = FullCircle - distance;
        return distance;
    }

}