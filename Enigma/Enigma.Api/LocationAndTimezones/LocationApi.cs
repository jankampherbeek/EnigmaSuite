// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2025.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Ardalis.GuardClauses;
using Enigma.Core.LocationAndTimeZones;
using Enigma.Domain.LocationsZones;

namespace Enigma.Api.LocationAndTimeZones;

/// <summary>Api for retrieving information about locations</summary>
public interface ILocationApi
{
    /// <summary>Retrieve all countries</summary>
    /// <returns>All countries</returns>
    public List<Country> GetAllCountries();
    /// <summary>Retrieve all cities for a given country</summary>
    /// <param name="countryCode">Abbreviation for the country</param>
    /// <returns>All cities for this country</returns>
    public List<City> GetAllCitiesForCountry(string countryCode);
}

/// <inheritdoc/>
public class LocationApi(ILocationHandler locationHandler) : ILocationApi
{
    /// <inheritdoc/>
    public List<Country> GetAllCountries()
    {
        return locationHandler.AllCountries();
    }

    /// <inheritdoc/>
    public List<City> GetAllCitiesForCountry(string countryCode)
    {
        Guard.Against.NullOrEmpty(countryCode);
        return locationHandler.CitiesForCountry(countryCode);
    }
}