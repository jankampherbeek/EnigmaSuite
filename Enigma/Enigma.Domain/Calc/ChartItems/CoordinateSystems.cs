﻿// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Constants;
using Serilog;

namespace Enigma.Domain.Calc.ChartItems;


/// <summary>Coordinate systems, used to define a position.</summary>
public enum CoordinateSystems
{
    Ecliptical = 0, Equatorial = 1, Horizontal = 3
}


/// <summary>Details for a coordinate system.</summary>
/// <param name="CoordSystem">The coordinate system.</param>
/// <param name="ValueForFlag">The value to construct the flags, as defined by the Swiss Ephemeris.</param>
/// <param name="TextId">Id to find a descriptive text in a resource bundle.</param>
public record CoordinateSystemDetails(CoordinateSystems CoordSystem, int ValueForFlag, string TextId);


/// <summary>Extension class for enum CoordinateSystems.</summary>
public static class CoordinateSystemsExtensions
{
    /// <summary>Retrieve details for a coordinate system.</summary>
    /// <param name="coordSys">The coordinate system, is automatically filled.</param>
    /// <returns>Details for the coordinate system.</returns>
    public static CoordinateSystemDetails GetDetails(this CoordinateSystems coordSys)
    {
        return coordSys switch
        {
            // No specific flags for ecliptical and horizontal.
            CoordinateSystems.Ecliptical => new CoordinateSystemDetails(coordSys, 0, "coordinateSysEcliptic"),
            CoordinateSystems.Equatorial => new CoordinateSystemDetails(coordSys, EnigmaConstants.SEFLG_EQUATORIAL, "coordinateSysEquatorial"),
            CoordinateSystems.Horizontal => new CoordinateSystemDetails(coordSys, 0, "coordinateSysHorizontal"),
            _ => throw new ArgumentException("Coordinate system unknown : " + coordSys.ToString())
        };
    }


    /// <summary>Retrieve details for all items in the enum CoordinateSystems.</summary>
    /// <returns>All details.</returns>
    public static List<CoordinateSystemDetails> AllDetails(this CoordinateSystems _)
    {
        var allDetails = new List<CoordinateSystemDetails>();
        foreach (CoordinateSystems currentSys in Enum.GetValues(typeof(CoordinateSystems)))
        {
            allDetails.Add(currentSys.GetDetails());
        }
        return allDetails;
    }


    /// <summary>Find coordinate system for an index.</summary>
    /// <param name="index">Index to look for.</param>
    /// <returns>The coordinate systrem for the given index.</returns>
    /// <exception cref="ArgumentException">Is thrown if a non existing index is given.</exception>
    public static CoordinateSystems CoordinateSystemForIndex(this CoordinateSystems _, int index)
    {
        foreach (CoordinateSystems currentSys in Enum.GetValues(typeof(CoordinateSystems)))
        {
            if ((int)currentSys == index) return currentSys;
        }
        string errorText = "CoordinateSystems.CoordinateSystemForIndex(): Could not find coordinate system for index : " + index;
        Log.Error(errorText);
        throw new ArgumentException(errorText);
    }

}





