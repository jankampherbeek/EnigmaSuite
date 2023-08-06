// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Core.Handlers.Calc.Coordinates.Helpers;
using Enigma.Domain.Calc.ChartItems;
using Enigma.Domain.Calc.DateTime;
using Enigma.Domain.Calc.Specials;
using Enigma.Domain.Points;
using Enigma.Domain.RequestResponse;

namespace Enigma.Api.Interfaces;

/// <summary>API for managing the Swiss Ephemeris.</summary>
public interface ISeApi
{
    /// <summary>Initialize the CommonSE.</summary>
    /// <param name="pathToSeFiles">Full path to datafiles for the CommonSE.</param>
    public void SetupSe(string? pathToSeFiles);

    public void CloseSe();
}


/// <summary>API for calculation of a fully defined chart.</summary>
public interface IChartAllPositionsApi
{
    /// <summary>Api call to calculate a full chart.</summary>
    /// <param name="request"/>
    /// <remarks>Throws ArgumentNullException if the request is null.</remarks>
    /// <returns>Response with all positions.</returns>
    public Dictionary<ChartPoints, FullPointPos> GetChart(CelPointsRequest request);
}

/// <summary>API for conversion between coordinates.</summary>
public interface ICoordinateConversionApi
{
    /// <summary>Api call to convert ecliptical coordinates into equatorial coordinates.</summary>
    /// <param name="request"/>
    /// <remarks>Throws ArgumentNullException if the request is null.</remarks>
    /// <returns>Equatorial coordinates that correspond to the ecliptic coordinates from the request, using the obliquity from the request.</returns>
    public EquatorialCoordinates GetEquatorialFromEcliptic(CoordinateConversionRequest request);
}

/// <summary>API for the calculation of horizontal coordinates.</summary>
public interface IHorizontalApi
{

    /// <summary>Api call to retrieve horizontal coordinates (azimuth and altitude).</summary>
    /// <param name="request"/>
    /// <remarks>Throws ArgumentNullException if either the request is null or the request contains a location that is null or eclipticalcoordinates that are null.</remarks>
    /// <returns>Values for azimuth and altitude.</returns>
    public HorizontalCoordinates GetHorizontal(HorizontalRequest request);
}


/// <summary>API for calculation of house cusps and other mundane points.</summary>
public interface IHousesApi
{
    /// <summary>Api call to calculate house cusps and other mundane points.</summary>
    /// <param name="request"/>
    /// <remarks>Throws ArgumentNullException if the request is null.</remarks>
    /// <returns>Instance of FullHousesPosResponse with all coordinates for cusps, MC, Ascendant, EastPoint and Vertex.</returns>
    public Dictionary<ChartPoints, FullPointPos> GetHouses(FullHousesPosRequest request);
}

/// <summary>API for calculation of pblique longitude (True place for the WvA)</summary>
public interface IObliqueLongitudeApi
{
    /// <summary>Calculate oblique longitude.</summary>
    public List<NamedEclipticLongitude> GetObliqueLongitude(ObliqueLongitudeRequest request);

}

/// <summary>API for calculation of the obliquity of the earth's axis.</summary>
public interface IObliquityApi
{
    /// <summary>Api call to retrieve obliquity.</summary>
    /// <param name="request"/>
    /// <remarks>Throws ArgumentNullException if the request is null.</remarks>
    /// <returns>Value for the obliquity of the earth's axis.</returns>
    public double GetObliquity(ObliquityRequest request);
}

/// <summary>API for handling date and time.</summary>
public interface IDateTimeApi
{
    /// <summary>Api call to calculate date and time.</summary>
    /// <param name="request">DateTimeRequest with the value of a Julian Day number and the calendar that is used.</param>
    /// <remarks>Throws ArgumentNullException if the request is null.</remarks> 
    /// <returns>Response with validation and an instance of SimpleDateTime.</returns>
    public DateTimeResponse GetDateTime(DateTimeRequest request);

    /// <summary>Checks if a given date and time is possible.</summary>
    /// <param name="dateTime">Date and time.</param>
    /// <remarks>Throws ArgumentNullException if the request is null.</remarks> 
    /// <returns>True if date and tme are valid.</returns>
    public bool CheckDateTime(SimpleDateTime dateTime);
}



/// <summary>API for the calculation of the Julian Day Number.</summary>
public interface IJulianDayApi
{
    /// <summary>Api call to calculate a Julian Day Number based on UT.</summary>
    /// <param name="request"/>
    /// <remarks>Throws ArgumentNullException if the request is null or if SimpleDateTime in the request is null.</remarks> 
    /// <returns>Response with validation and a value for a Julian Day number.</returns>
    public JulianDayResponse GetJulianDay(SimpleDateTime request);
}

/// <summary>API for the calculation of a range of charts.</summary>
public interface ICalcChartsRangeApi
{
    /// <summary>Calculate range of charts.</summary>
    /// <param name="request">Request with data and settings.</param>
    /// <returns>Calculatred charts.</returns>
    public List<FullChartForResearchItem> CalculateRange(ChartsRangeRequest request);
}