// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Interfaces;
using Enigma.Domain.Dtos;

namespace Enigma.Core.Analysis;

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