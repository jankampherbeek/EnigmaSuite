// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.RequestResponse;



/// <summary>Handles the conversion from ecliptical to equatorial coordinates.</summary>
public interface ICoordinateConversionHandler
{
    public CoordinateConversionResponse HandleConversion(CoordinateConversionRequest request);
}

public interface IHorizontalHandler
{
    public HorizontalResponse CalcHorizontal(HorizontalRequest request);
}