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
                    var regionCode = $"{fields[0].Trim()}.{fields[4].Trim()}";
                    string regionName;
                    try
                    {
                        regionName = FindRegionName(regionCode);
                    }
                    catch
                    {
                        // TODO: handle error
                        regionName = string.Empty;
                    }

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

                return cities;
            }
            catch (Exception ex)
            {
                Log.Error($"Error reading cities file: {ex.Message}");
                throw;
            }
        }

        private string FindRegionName(string regionCode)
        {
            try
            {
                var lines = File.ReadAllLines(RegionsFile);
                foreach (var line in lines)
                {
                    var fields = line.Split(ITEM_SEPARATOR);
                    if (fields.Length == 2 && fields[0] == regionCode)
                    {
                        return fields[1].Trim();
                    }
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                Log.Error($"Error reading regions file: {ex.Message}");
                throw;
            }
        }
    }
