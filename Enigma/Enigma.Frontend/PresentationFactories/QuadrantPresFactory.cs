// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;

namespace Enigma.Frontend.Ui.PresentationFactories;


/// <summary>
/// Presentabe version of quadrant counts.
/// </summary>
/// <param name="Quadrant">Name of the quadrant</param>
/// <param name="Count">Number of items in the quadrant</param>
public record PresentableQuadrantCount(string Quadrant, int Count);

/// <summary>
/// Factory for presentable version of quadrant counts.
/// </summary>
public class QuadrantPresFactory
{

    /// <summary>
    /// Create a presentable version of quadrant counts.
    /// </summary>
    /// <param name="qCount">The calculated quadrant counts</param>
    /// <returns>The presentable quadrant counts</returns>
    public List<PresentableQuadrantCount> CreateQuadrantCounts(Dictionary<int, int> qCount)
    {
        return
        [
            new PresentableQuadrantCount("Quadrant 1", qCount[1]),
            new PresentableQuadrantCount("Quadrant 2", qCount[2]),
            new PresentableQuadrantCount("Quadrant 3", qCount[3]),
            new PresentableQuadrantCount("Quadrant 4", qCount[4])
        ];
    }
}