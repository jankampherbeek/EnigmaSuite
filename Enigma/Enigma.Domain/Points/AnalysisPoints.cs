// Jan Kampherbeek, (c) 2022, 2023.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.AstronCalculations;

namespace Enigma.Domain.Points;

/// <summary>
/// Generic DTO for points that will be part of an analysis.
/// </summary>
public record AnalysisPoint
{
    public readonly PointGroups PointGroup;
    public readonly int ItemId;
    public readonly double Position;
    public readonly string Glyph;

    /// <summary>
    /// Construct DTO.
    /// </summary>
    /// <param name="pointGroup">The group of points that this point belongs to.</param>
    /// <param name="itemId">Id for the item, corresponds with id in the enum for this type of items.</param>
    /// <param name="position">Position in decimal degrees. The type of position depends on the pointGroup.</param>
    /// <param name="glyph">Default glyph for this point.</param>
    public AnalysisPoint(PointGroups pointGroup, int itemId, double position, string glyph)
    {
        PointGroup = pointGroup;
        ItemId = itemId;
        Position = position;
        Glyph = glyph;
    }

}