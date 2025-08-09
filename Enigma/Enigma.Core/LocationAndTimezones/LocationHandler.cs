// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2025.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.LocationsZones;
using Serilog;

namespace Enigma.Core.LocationAndTimeZones;

    public interface ILocationHandler
    {
        List<Country> AllCountries();
        List<City> CitiesForCountry(string countryCode);
    }

    public class LocationHandler : ILocationHandler
    {
        private static readonly string PathSep = Path.DirectorySeparatorChar.ToString();
        private static readonly string CountriesFile = $"tz-coord{PathSep}countries.csv";
        private static readonly string CitiesFile = $"tz-coord{PathSep}cities.csv";
        private static readonly string RegionsFile = $"tz-coord{PathSep}regions.csv";
        private const string ITEM_SEPARATOR = ";";
        
        public List<Country> AllCountries()
        {
            try
            {
                var countries = new List<Country>();
                var lines = File.ReadAllLines(CountriesFile);
                foreach (var line in lines)
                {
                    var fields = line.Split(ITEM_SEPARATOR);
                    if (fields.Length == 3) // there is a field for continent that is currently not used
                    {
                        countries.Add(new Country
                        {
                            Code = fields[0].Trim(),
                            Name = fields[1].Trim()
                        });
                    }
                }
                countries.Sort((x, y) =>
                    string.Compare(x.Name, y.Name, StringComparison.OrdinalIgnoreCase));
                return countries;
            }
            catch (Exception ex)
            {
                Log.Error($"Error reading countries file: {ex.Message}");
                throw;
            }
        }

        public List<City> CitiesForCountry(string countryCode)
        {
            try
            {
                var cities = new List<City>();
                var lines = File.ReadAllLines(CitiesFile);

                foreach (var line in lines)
                {
                    var fields = line.Split(ITEM_SEPARATOR);
                    if (fields.Length != 7 || fields[0] != countryCode) continue;
                    
                    // Extract region from city name (already in parentheses) - much faster than file lookup
                    string regionName = ExtractRegionFromCityName(fields[1].Trim());

                    cities.Add(new City
                    {
                        Country = countryCode,
                        Name = fields[1].Trim(),
                        GeoLat = fields[2].Trim(),
                        GeoLong = fields[3].Trim(),
                        Region = regionName,
                        Elevation = fields[5].Trim(),
                        IndicationTz = fields[6].Trim()
                    });
                }
                cities.Sort((x, y) =>
                    string.Compare(x.Name, y.Name, StringComparison.OrdinalIgnoreCase));
                return cities;
            }
            catch (Exception ex)
            {
                Log.Error($"Error reading cities file: {ex.Message}");
                throw;
            }
        }

        private static string ExtractRegionFromCityName(string cityName)
        {
            // Extract region from city name format: "CityName (RegionName)"
            // e.g., "Fillmore (California)" -> "California"
            if (string.IsNullOrEmpty(cityName)) return string.Empty;
            
            var openParen = cityName.LastIndexOf('(');
            var closeParen = cityName.LastIndexOf(')');
            
            if (openParen > 0 && closeParen > openParen)
            {
                return cityName.Substring(openParen + 1, closeParen - openParen - 1).Trim();
            }
            
            return string.Empty;
        }
    }
