// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Constants;

namespace Enigma.Domain.CalcVars;


/// <summary>Coordinate systems, used to define a position.</summary>
public enum CoordinateSystems
{
    Ecliptical, Equatorial, Horizontal
}

/// <summary>Details for a coordinate system.</summary>
public record CoordinateSystemDetails
{
    readonly public CoordinateSystems CoordinateSystem;
    readonly public int ValueForFlag;
    readonly public string TextId;

    /// <summary>Constructor for the details of a coordinate system.</summary>
    /// <param name="system">The coordinate system.</param>
    /// <param name="valueForFlag">The value to construct the flags, as defined by the Swiss Ephemeris.</param>
    /// <param name="textId">Id to find a descriptive text in a resource bundle.</param>
    public CoordinateSystemDetails(CoordinateSystems system, int valueForFlag, string textId)
    {
        CoordinateSystem = system;
        ValueForFlag = valueForFlag;
        TextId = textId;
    }
}

/// <summary>Specifications for a coordinate system.</summary>
public interface ICoordinateSystemSpecifications
{
    /// <summary>Returns the specifications for a Coordinate System.</summary>
    /// <param name="coordinateSystem">The coordinate system, from the enum CoordinateSystems.</param>
    /// <returns>A record CoordinateSystemDetails with the specifications of the coordinate system.</returns>
    public CoordinateSystemDetails DetailsForCoordinateSystem(CoordinateSystems coordinateSystem);
}

/// <inheritdoc/>
public class CoordinateSystemSpecifications : ICoordinateSystemSpecifications
{
    /// <inheritdoc/>
    /// <exception cref="ArgumentException">Is thrown if the Coordinate System was not recognized.</exception>
    CoordinateSystemDetails ICoordinateSystemSpecifications.DetailsForCoordinateSystem(CoordinateSystems coordinateSystem)
    {
        return coordinateSystem switch
        {
            // No specific flags for ecliptical and horizontal.
            CoordinateSystems.Ecliptical => new CoordinateSystemDetails(coordinateSystem, 0, "coordinateSysEcliptic"),
            CoordinateSystems.Equatorial => new CoordinateSystemDetails(coordinateSystem, EnigmaConstants.SEFLG_EQUATORIAL, "coordinateSysEquatorial"),
            CoordinateSystems.Horizontal => new CoordinateSystemDetails(coordinateSystem, 0, "coordinateSysHorizontal"),
            _ => throw new ArgumentException("Coordinate system unknown : " + coordinateSystem.ToString())
        };
    }
}

