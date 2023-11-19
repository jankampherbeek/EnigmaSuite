// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Constants;
using Serilog;

namespace Enigma.Domain.References;

/// <summary>Observer positions, the center points for the calculation of positions of celestial bodies</summary>
public enum ObserverPositions
{
    GeoCentric = 0, TopoCentric = 1, HelioCentric = 2
}

/// <summary>Details for an observer Position</summary>
/// <param name="Position">The observer Position</param>
/// <param name="ValueForFlag">The value to construct the flags, as defined by the Swiss Ephemeris</param>
/// <param name="Text">Descriptive text</param>
public record ObserverPositionDetails(ObserverPositions Position, int ValueForFlag, string Text);


/// <summary>Extension class for the enum ObserverPositions</summary>
public static class ObserverPositionsExtensions
{
    /// <summary>Retrieve details for observer Position</summary>
    /// <param name="obsPos">The observer Position, is automatically filled</param>
    /// <returns>Details for the observer Position</returns>
    public static ObserverPositionDetails GetDetails(this ObserverPositions obsPos)
    {
        return obsPos switch
        {
            // No specific flags for geocentric.
            ObserverPositions.GeoCentric => new ObserverPositionDetails(obsPos, 0, "Geocentric"),
          //  ObserverPositions.HelioCentric => new ObserverPositionDetails(obsPos, EnigmaConstants.SeflgHelctr, "Heliocentric"),
            ObserverPositions.TopoCentric => new ObserverPositionDetails(obsPos, EnigmaConstants.SEFLG_TOPOCTR, "Topocentric (with parallax)"),
            ObserverPositions.HelioCentric => new ObserverPositionDetails(obsPos, EnigmaConstants.SEFLG_HELCTR, "Heliocentric"),
            _ => throw new ArgumentException("Observer Position unknown : " + obsPos)
        };
    }


    /// <summary>Retrieve details for items in the enum ObserverPositions</summary>
    /// <returns>All details</returns>
    public static IEnumerable<ObserverPositionDetails> AllDetails()
    {
        return (from ObserverPositions currentPosition in Enum.GetValues(typeof(ObserverPositions)) 
            select currentPosition.GetDetails()).ToList();
    }

    /// <summary>Find observer position for a given index</summary>
    /// <param name="index">The index</param>
    /// <returns>The observer position</returns>
    /// <exception cref="ArgumentException">Thrown if the observer position was not found.</exception>
    public static ObserverPositions ObserverPositionForIndex(int index)
    {
        foreach (ObserverPositions currentObsPos in Enum.GetValues(typeof(ObserverPositions)))
        {
            if ((int)currentObsPos == index ) return currentObsPos;
        }
        Log.Error("ObserverPositions.ObserverPositionForIndex(): Could not find Observer Position for index : {Index}", index );
        throw new ArgumentException("No observer position for given index");
    }

}
