﻿// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Enigma.Facades.Se;

/// <summary>Facade for the conversion between ecliptic and equatorial coordinates.</summary>
/// <remarks>Enables accessing the CommonSE dll. Passes any result without checking, exceptions are automatically propagated.</remarks>
public interface ICoTransFacade
{
    /// <summary>Convert ecliptic to equatorial coordinates.</summary>
    /// <remarks>Calls the function ext_swe_cotrans from the CommonSE.</remarks>/// 
    /// <param name="eclipticCoordinates">Array with subsequently longitude and latitude.</param>
    /// <param name="obliquity"/>
    /// <returns>Array with subsequently right ascension and declination.</returns>
    public double[] EclipticToEquatorial(double[] eclipticCoordinates, double obliquity);
}

/// <inheritdoc/>
[SuppressMessage("Interoperability", "SYSLIB1054:Use \'LibraryImportAttribute\' instead of \'DllImportAttribute\' to generate P/Invoke marshalling code at compile time")]
public class CoTransFacade : ICoTransFacade
{
    /// <inheritdoc/>
    /// <remarks>Throws SwissEphException if the CommonSE returns an error.</remarks>
    public double[] EclipticToEquatorial(double[] eclipticCoordinates, double obliquity)
    {
        double negativeObliquity = -Math.Abs(obliquity);
        const double placeHolderForDistance = 1.0;
        double[] allEclipticCoordinates = { eclipticCoordinates[0], eclipticCoordinates[1], placeHolderForDistance };
        double[] equatorialResults = new double[3];
        ext_swe_cotrans(allEclipticCoordinates, equatorialResults, negativeObliquity);
        return equatorialResults;
    }

    [DllImport("swedll64.dll", CharSet = CharSet.Ansi, EntryPoint = "swe_cotrans")]
    private static extern void ext_swe_cotrans(double[] allEclipticCoordinates, double[] equatorialResults, double negativeObliquity);
}