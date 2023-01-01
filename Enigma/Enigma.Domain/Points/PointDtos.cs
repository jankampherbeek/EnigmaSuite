// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.Points;

/// <summary>Definition of a point that can be used in analysis, research etc.</summary>
/// <remarks>Used to ahdnle different types of points (celestial points, mundane points etc.) in one collection.</remarks>
/// <param name="Index">A unique number for this point. </param>
/// <param name="Name">Name for the point, main use is that the name shows up in Json files.</param>  
/// <param name="PointType">The type of point, from the enum PointTypes.</param>
/// <param name="RbId">Id to retrieve text from the resource bundle.</param>
public record GeneralPoint(int Index, string Name, PointTypes PointType, string RbId);


/// <summary>A GenericPoint combined with a single position.</summary>
/// <param name="Point">The point.</param>
/// <param name="Position">The position in decimal degrees.</param>
public record PositionedPoint(GeneralPoint Point, double Position);

