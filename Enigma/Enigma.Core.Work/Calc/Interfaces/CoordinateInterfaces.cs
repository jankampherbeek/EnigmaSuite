// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.AstronCalculations;

namespace Enigma.Core.Work.Calc.Interfaces;

/// <summary>Convert ecliptical longitude and latitude to equatorial right ascension and declination.</summary>
public interface ICoordinateConversionCalc
{
    public EquatorialCoordinates PerformConversion(EclipticCoordinates eclCoord, double obliquity);

}

public interface IHorizontalCalc
{
    public double[] CalculateHorizontal(double jdUt, Location location, EclipticCoordinates eclipticCoordinates, int flags);
}