// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;
using System.Collections.Generic;
using Enigma.Domain.Dtos;
using Enigma.Domain.Graphics;
using Enigma.Domain.Presentables;
using Enigma.Domain.References;

namespace Enigma.Frontend.Ui.PresentationFactories;

/// <summary>Sorts positions of celestial points for usage in graphics.</summary>
public interface ISortedGraphicCelPointsFactory
{
    /// <summary>Sort points to be used in a wheel and define the positions where to plot them.</summary>
    /// <param name="celPointPositions">The positions of celestial points to handle.</param>
    /// <param name="longitudeAsc">Longitude of the ascendant.</param>
    /// <param name="minDistance">Indication for the minimal distance to be maintained.</param>
    /// <returns>The sorted positions.</returns>
    public List<GraphicCelPointForWheelPositions> CreateSortedListForWheel(
        Dictionary<ChartPoints, FullPointPos> celPointPositions, double longitudeAsc, double minDistance);
}

/// <inheritdoc/>
public class SortedGraphicCelPointsFactory : ISortedGraphicCelPointsFactory
{
    private readonly ICelPointForDataGridFactory _celPointFactory;

    public SortedGraphicCelPointsFactory(ICelPointForDataGridFactory celPointFactory)
    {
        _celPointFactory = celPointFactory;
    }

    /// <inheritdoc/>
    public List<GraphicCelPointForWheelPositions> CreateSortedListForWheel(
        Dictionary<ChartPoints, FullPointPos> celPointPositions, double longitudeAsc, double minDistance)
    {
        List<GraphicCelPointForWheelPositions> graphPositions =
            CreateGraphicPositionsInWheel(celPointPositions, longitudeAsc);
        graphPositions.Sort((pos1, pos2) =>
            pos1.MundanePos.CompareTo(pos2.MundanePos));

        // Forward pass: ensure minimum distance between consecutive items
        GraphicCelPointForWheelPositions? lastPos = null;
        foreach (var pos in graphPositions)
        {
            if (lastPos != null)
            {
                double actDistance = pos.MundanePos - lastPos.PlotPos;
                if (actDistance < minDistance)
                {
                    pos.PlotPos += minDistance - actDistance;
                }
            }

            lastPos = pos;
        }

        //  Check for circular overlap between last and first item
        if (graphPositions.Count > 1)
        {
            var firstPos = graphPositions[0];
            var lastPosItem = graphPositions[^1];

            // Calculate circular distance from last to first (wrapping around 360)
            double circularDistance = (firstPos.PlotPos + 360.0 - lastPosItem.PlotPos);
            if (circularDistance >= 360.0) circularDistance -= 360.0;

            if (circularDistance < minDistance)
            {
                // There's overlap - need to adjust. Do a backward pass to redistribute spacing.
                double deficit = minDistance - circularDistance;

                // Adjust positions backward, starting from the last item
                for (int i = graphPositions.Count - 1; i >= 0; i--)
                {
                    graphPositions[i].PlotPos -= deficit;

                    // Ensure we maintain minDistance with the previous item (or wrap to last)
                    if (i > 0)
                    {
                        double distToPrevious = graphPositions[i].PlotPos - graphPositions[i - 1].PlotPos;
                        if (distToPrevious < minDistance && deficit < 20.0)
                        {
                            // Need to push previous items back further

                          deficit += (minDistance - distToPrevious);
                        }
                        else
                        {
                            // We have enough space, can stop adjusting
                            break;
                        }
                    }
                }
            }
        }

        // Normalize all positions to 0-360 range
        foreach (var pos in graphPositions)
        {
            while (pos.PlotPos >= 360.0) pos.PlotPos -= 360.0;
            while (pos.PlotPos < 0.0) pos.PlotPos += 360.0;
        }

        return graphPositions;
    }

    public List<GraphicCelPointForDeclDiagram> CreateSortedListForDeclinationDiagram(
        Dictionary<ChartPoints, FullPointPos> celPointPositions)
    {
        throw new System.NotImplementedException();
    }


    private List<GraphicCelPointForWheelPositions> CreateGraphicPositionsInWheel(
        Dictionary<ChartPoints, FullPointPos> fullPositions, double longitudeAsc)
    {
        List<GraphicCelPointForWheelPositions> graphPositions = new();
        List<PresentableCommonPositions> presentablePositions =
            _celPointFactory.CreateCelPointPosForDataGrid(fullPositions);
        int count = 0;
        foreach ((ChartPoints celPoint, FullPointPos? value) in fullPositions)
        {
            double longitude = value.Ecliptical.MainPosSpeed.Position;
            double mundanePos = longitude - longitudeAsc + 90.0;
            if (mundanePos < 0.0) mundanePos += 360.0;
            if (mundanePos >= 360.0) mundanePos -= 360.0;
            string longitudeText = presentablePositions[count].LongText;

            GraphicCelPointForWheelPositions graphicPos = new(celPoint, longitude, mundanePos, longitudeText);
            graphPositions.Add(graphicPos);
            count++;
        }

        return graphPositions;
    }
}