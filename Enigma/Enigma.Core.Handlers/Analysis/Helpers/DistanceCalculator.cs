﻿// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers.Interfaces;
using Enigma.Domain.Analysis;
using Enigma.Domain.Points;

namespace Enigma.Core.Handlers.Analysis.Helpers;

/// <inheritdoc/>
public class DistanceCalculator: IDistanceCalculator
{

    /// <inheritdoc/>
    public List<DistanceBetween2Points> FindShortestDistances(List<PositionedPoint> allPoints)
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
    public List<DistanceBetween2Points> FindShortestDistanceBetweenPointsAndCusps(List<PositionedPoint> allPoints, List<PositionedPoint> allCusps) 
    {
        List<DistanceBetween2Points> allDistances = new();
        foreach (PositionedPoint point in allPoints)
        {
            foreach(PositionedPoint cusp in allCusps)
            {
                double distance = NormalizeShortestDistance(point.Position - cusp.Position);
                allDistances.Add(new DistanceBetween2Points(point, cusp, distance));
            }
        }
        return allDistances;
    }


    private static double NormalizeShortestDistance(double InitialValue)
    {
        double distance = InitialValue;
        if (distance < 0) distance += 360.0;
        if (distance > 180.0) distance = 360.0 - distance;
        return distance;
    }

}