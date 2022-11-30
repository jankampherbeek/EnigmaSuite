// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

// Interfaces for API's that support astronomical calculations.


using Enigma.Domain.CalcChartsRange;
using Enigma.Domain.RequestResponse;

namespace Enigma.Api.Interfaces;

/// <summary>API for managing the Swiss Ephemeris.</summary>
public interface ISeApi
{
    /// <summary>Initialize the SE.</summary>
    /// <param name="pathToSeFiles">Full path to datafiles for the SE.</param>
    public void SetupSe(string pathToSeFiles);
}


/// <summary>API for calculation of a fully defined chart.</summary>
public interface IChartAllPositionsApi
{
    /// <summary>Api call to calculate a full chart.</summary>
    /// <param name="request"/>
    /// <remarks>Throws ArgumentNullException if the request is null.</remarks>
    /// <returns>Response with instance of FullChart with all positionscoordinates, or an indication of errors that occurred.</returns>
    public ChartAllPositionsResponse GetChart(ChartAllPositionsRequest request);
}

/// <summary>API for conversion between coordinates.</summary>
public interface ICoordinateConversionApi
{
    /// <summary>Api call to convert ecliptical coordinates into equatorial coordinates.</summary>
    /// <param name="request"/>
    /// <remarks>Throws ArgumentNullException if the request is null.</remarks>
    /// <returns>Equatorial coordinates that correspond to the ecliptic coordinates from the request, using the obliquity from the request.</returns>
    public CoordinateConversionResponse GetEquatorialFromEcliptic(CoordinateConversionRequest request);
}

/// <summary>API for the calculation of horizontal coordinates.</summary>
public interface IHorizontalApi
{

    /// <summary>Api call to retrieve horizontal coordinates (azimuth and altitude).</summary>
    /// <param name="request"/>
    /// <remarks>Throws ArgumentNullException if either the request is null or the request contains a location that is null or eclipticalcoordinates that are null.</remarks>
    /// <returns>Values for azimuth and altitude.</returns>
    public HorizontalResponse GetHorizontal(HorizontalRequest request);
}


/// <summary>API for calculation of house cusps and other mundane points.</summary>
public interface IHousesApi
{
    /// <summary>Api call to calculate house cusps and other mundane points.</summary>
    /// <param name="request"/>
    /// <remarks>Throws ArgumentNullException if the request is null.</remarks>
    /// <returns>Instance of FullHousesPosResponse with all coordinates for cusps, MC, Ascendant, EastPoint and Vertex.</returns>
    public FullHousesPosResponse GetHouses(FullHousesPosRequest request);
}

/// <summary>API for calculation of pblique longitude (True place for the WvA)</summary>
public interface IObliqueLongitudeApi
{
    /// <summary>Calculate oblique longitude.</summary>
    public ObliqueLongitudeResponse GetObliqueLongitude(ObliqueLongitudeRequest request);

}

/// <summary>API for calculation of the obliquity of the earth's axis.</summary>
public interface IObliquityApi
{
    /// <summary>Api call to retrieve obliquity.</summary>
    /// <param name="request"/>
    /// <remarks>Throws ArgumentNullException if the request is null.</remarks>
    /// <returns>Value for the obliquity of the earth's axis.</returns>
    public ObliquityResponse GetObliquity(ObliquityRequest request);
}

/// <summary>API for calculating date and time.</summary>
public interface ICalcDateTimeApi
{
    /// <summary>Api call to calculate date and time.</summary>
    /// <param name="request">DateTimeRequest with the value of a Julian Day number and the calendar that is used.</param>
    /// <remarks>Throws ArgumentNullException if the request is null.</remarks> 
    /// <returns>Response with validation and an instance of SimpleDateTime.</returns>
    public DateTimeResponse GetDateTime(DateTimeRequest request);

    /// <summary>Checks if a given date and time is possible.</summary>
    /// <param name="request">Instance of DateTimeRequest.</param>
    /// <remarks>Throws ArgumentNullException if the request is null.</remarks> 
    /// <returns>Instance of DateTimeResponse.</returns>
    public CheckDateTimeResponse CheckDateTime(CheckDateTimeRequest request);
}

/// <summary>API for date and time.</summary>
public interface IDateTimeApi
{
    /// <summary>Api call to check if a given date time is valid.</summary>
    /// <param name="request"/>
    /// <remarks>Throws ArgumentNullException if the request is null or if SimpleDateTime in the request is null.</remarks> 
    /// <returns>Response with an indication if the date and time are valid and an indication if errors did occur.</returns>
    public CheckDateTimeResponse CheckDateTime(CheckDateTimeRequest request);
}

/// <summary>API for the calculation of the Julian Day Number.</summary>
public interface IJulianDayApi
{
    /// <summary>Api call to calculate a Julian Day Number based on UT.</summary>
    /// <param name="request"/>
    /// <remarks>Throws ArgumentNullException if the request is null or if SimpleDateTime in the request is null.</remarks> 
    /// <returns>Response with validation and a value for a Julian Day number.</returns>
    public JulianDayResponse GetJulianDay(JulianDayRequest request);
}

/// <summary>API for the calculation of a range of charts.</summary>
public interface ICalcChartsRangeApi
{
    /// <summary>Calculate range of charts.</summary>
    /// <param name="request">Request with data and settings.</param>
    /// <returns>Calculatred charts.</returns>
    public List<FullChartForResearchItem> CalculateRange(ChartsRangeRequest request);
}