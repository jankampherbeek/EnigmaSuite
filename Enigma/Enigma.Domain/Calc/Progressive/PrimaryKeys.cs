﻿// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Serilog;

namespace Enigma.Domain.Calc.Progressive;

/// <summary>Time keys for primary directions.</summary>
public enum PrimaryKeys
{
    None = 0,
    PtolemyRa = 1,
    NaibodRa = 2,
    BraheRa = 3,
    PlacidusRa = 4,
    PtolemyLongitude = 5,
    NaibodLongitude = 6,
    BraheLongitude = 7,
    PlacidusLongitude = 8
}

/// <summary>Details for a primary time key.</summary>
/// <param name="PrimaryKey">Time key.</param>
/// <param name="TextId">Id to find a descriptive text in a resource bundle.</param>
public record PrimaryKeyDetails(PrimaryKeys PrimaryKeys, string TextId);


/// <summary>Extension class for enum PrimaryKeys.</summary>
public static class PrimaryKeyExtensions
{
    /// <summary>Retrieve details for primary time key.</summary>
    /// <param name="primaryKey">The primary time key.</param>
    /// <returns>Details for the primary time key.</returns>
    public static PrimaryKeyDetails GetDetails(this PrimaryKeys primaryKey)
    {
        return primaryKey switch
        {
            PrimaryKeys.PtolemyRa => new PrimaryKeyDetails(primaryKey, "ref.enum.primarykey.ptolemyra"),
            PrimaryKeys.NaibodRa => new PrimaryKeyDetails(primaryKey, "ref.enum.primarykey.naibodra"),
            PrimaryKeys.BraheRa => new PrimaryKeyDetails(primaryKey, "ref.enum.primarykey.brahera"),
            PrimaryKeys.PlacidusRa => new PrimaryKeyDetails(primaryKey, "ref.enum.primarykey.placidusra"),
            PrimaryKeys.PtolemyLongitude => new PrimaryKeyDetails(primaryKey, "ref.enum.primarykey.ptolemylong"),
            PrimaryKeys.NaibodLongitude => new PrimaryKeyDetails(primaryKey, "ref.enum.primarykey.naibodlong"),
            PrimaryKeys.BraheLongitude => new PrimaryKeyDetails(primaryKey, "ref.enum.primarykey.brahelong"),
            PrimaryKeys.PlacidusLongitude => new PrimaryKeyDetails(primaryKey, "ref.enum.primarykey.placiduslong"),
            _ => throw new ArgumentException("Primary time key unknown : " + primaryKey.ToString())
        };
    }



    /// <summary>Retrieve details for items in the enum PrimaryKeys.</summary>
    /// <returns>All details.</returns>
    public static List<PrimaryKeyDetails> AllDetails(this PrimaryKeys _)
    {
        var allDetails = new List<PrimaryKeyDetails>();
        foreach (PrimaryKeys primKeyCurrent in Enum.GetValues(typeof(PrimaryKeys)))
        {
            if (primKeyCurrent != PrimaryKeys.None)
            {
                allDetails.Add(primKeyCurrent.GetDetails());
            }
        }
        return allDetails;
    }

    /// <summary>Find primary time key for a given index.</summary>
    /// <param name="index">The index.</param>
    /// <returns>The primary time key.</returns>
    /// <exception cref="ArgumentException">Thrown if the time key could not be found.</exception>
    public static PrimaryKeys PrimaryKeysForIndex(this PrimaryKeys _, int index)
    {
        foreach (PrimaryKeys primKeyCurrent in Enum.GetValues(typeof(PrimaryKeys)))
        {
            if ((int)primKeyCurrent == index) return primKeyCurrent;
        }
        string errorText = "PrimaryKeys.PrimaryKeyForIndex(): Could not find Primary Key for index : " + index;
        Log.Error(errorText);
        throw new ArgumentException(errorText);
    }

}