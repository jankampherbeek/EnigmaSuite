// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.CalcVars;

namespace Enigma.Core.Analysis.Dto;

/// <summary>
/// Generic DTO for points that will be part of an analysis.
/// </summary>
public record AnalysisPoint
{
    public readonly PointGroups PointGroup;
    public readonly int ItemId;
    public readonly double Position;

    /// <summary>
    /// Construct DTO.
    /// </summary>
    /// <param name="pointGroup">The group of points that this point belongs to.</param>
    /// <param name="itemId">Id for the item, corresponds with id in the enum for this type of items.</param>
    /// <param name="position">Position in decimal degrees. The type of position depends on the pointGroup.</param>
    public AnalysisPoint(PointGroups pointGroup, int itemId, double position)
    {
        PointGroup = pointGroup;
        ItemId = itemId;
        Position = position;
    }

}