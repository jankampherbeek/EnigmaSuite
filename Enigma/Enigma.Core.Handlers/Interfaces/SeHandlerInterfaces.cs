// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.



/// <summary>Interfaces for handlers that manage the Swiss Ephemeris.</summary>
public interface ISeHandler
{
    /// <summary>Initialize the Se.</summary>
    /// <param name="pathToSeFiles">Full path to the SE data files.</param>
    public void SetupSe(string pathToSeFiles);
}

