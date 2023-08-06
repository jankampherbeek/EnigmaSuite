// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers.Calc.Coordinates.Helpers;
using Enigma.Domain.Calc.ChartItems;

namespace Enigma.Core.Handlers.Interfaces;

/// <summary>Handles the conversion from ecliptical to equatorial coordinates.</summary>
public interface ICoordinateConversionHandler
{
    /// <summary>Start conversion from ecliptical to equatorial coordinates..</summary>
    /// <param name="request">Request with astronomical details.</param>
    /// <returns>Equatorial coordinates.</returns>
    public EquatorialCoordinates HandleConversion(CoordinateConversionRequest request);
}

/// <summary>Handles the calculation of horizontal coordinates (azimuth and altitude).</summary>
public interface IHorizontalHandler
{
    /// <summary>Start the calculation of horizontal coordinates.</summary>
    /// <param name="request">Request with the astronomical information.</param>
    /// <returns>The horizontal coordinates.</returns>
    public HorizontalCoordinates CalcHorizontal(HorizontalRequest request);
}


/// <summary>Convert ecliptical longitude and latitude to equatorial right ascension and declination.</summary>
public interface ICoordinateConversionCalc
{
    /// <summary>Convert ecliptic coordinates to equatorial coordinates.</summary>
    /// <param name="eclCoord">The ecliptic coordinates.</param>
    /// <param name="obliquity">True obliquity of the earths axis.</param>
    /// <returns>The equatorial coordinates.</returns>
    public EquatorialCoordinates PerformConversion(EclipticCoordinates eclCoord, double obliquity);

}

/// <summary>Calculate the horizontal coordinates.</summary>
public interface IHorizontalCalc
{
    /// <summary>Perform the calculation of the horizontal coordinates.</summary>
    /// <param name="jdUt">Julian day for UT.</param>
    /// <param name="location"/>
    /// <param name="equCoordinates"/>
    /// <param name="flags">Flags for the SE.</param>
    /// <returns>Calculated horizontal coordinates (azimuth and altitude).</returns>
    public double[] CalculateHorizontal(double jdUt, Location location, EquatorialCoordinates equCoordinates, int flags);
}