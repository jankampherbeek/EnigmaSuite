// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Constants;
using Serilog;

namespace Enigma.Domain.Calc.ChartItems;

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
            ObserverPositions.HelioCentric => new ObserverPositionDetails(obsPos, EnigmaConstants.SEFLG_HELCTR, "Heliocentric"),
            ObserverPositions.TopoCentric => new ObserverPositionDetails(obsPos, EnigmaConstants.SEFLG_TOPOCTR, "Topocentric (with parallax)"),
            _ => throw new ArgumentException("Observer Position unknown : " + obsPos.ToString())
        };
    }


    /// <summary>Retrieve details for items in the enum ObserverPositions</summary>
    /// <returns>All details</returns>
    public static List<ObserverPositionDetails> AllDetails(this ObserverPositions _)
    {
        var allDetails = new List<ObserverPositionDetails>();
        foreach (ObserverPositions currentPosition in Enum.GetValues(typeof(ObserverPositions)))
        {
            allDetails.Add(currentPosition.GetDetails());
        }
        return allDetails;
    }

    /// <summary>Find observer position for a given index</summary>
    /// <param name="index">The index</param>
    /// <returns>The observer position</returns>
    /// <exception cref="ArgumentException">Thrown if the observer position was not found.</exception>
    public static ObserverPositions ObserverPositionForIndex(this ObserverPositions _, int index)
    {
        foreach (ObserverPositions currentObsPos in Enum.GetValues(typeof(ObserverPositions)))
        {
            if ((int)currentObsPos == index ) return currentObsPos;
        }
        string errorText = "ObserverPositions.ObserverPositionForIndex(): Could not find Observer Position for index : " + index;
        Log.Error(errorText);
        throw new ArgumentException(errorText);
    }

}
