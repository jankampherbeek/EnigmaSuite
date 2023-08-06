// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.Calc.ChartItems;
using Enigma.Domain.RequestResponse;

namespace Enigma.Core.Handlers.Interfaces;

/// <summary>Calculator for oblique longitudes (School of Ram).</summary>
public interface IObliqueLongitudeCalculator
{
    /// <summary>Perform calculations to obtain oblique longitudes.</summary>
    /// <param name="request">Specifications for the calculation.</param>
    /// <returns>Celestial points with the oblique longitude.</returns>
    public List<NamedEclipticLongitude> CalcObliqueLongitudes(ObliqueLongitudeRequest request);
}



/// <summary>Calculations for the south-point.</summary>
public interface ISouthPointCalculator
{
    /// <summary>Calculate longitude and latitude for the south-point.</summary>
    /// <param name="armc">Right ascension for the MC.</param>
    /// <param name="obliquity">Obliquity of the earths axis.</param>
    /// <param name="geoLat">Geographic latitude.</param>
    /// <returns>An instance of EclipticCoodinates with values for longitude and latitude.</returns>
    public EclipticCoordinates CalculateSouthPoint(double armc, double obliquity, double geoLat);
}