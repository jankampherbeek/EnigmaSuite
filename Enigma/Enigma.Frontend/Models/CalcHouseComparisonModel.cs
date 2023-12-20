// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using System.Linq;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Frontend.Ui.Interfaces;

namespace Enigma.Frontend.Ui.Models;

public class CalcHouseComparisonModel
{

    private readonly IGeoLatInputParser _geoLatInputParser;
    private readonly IDateInputParser _dateInputParser;
    protected FullDate? FullDate;
    protected FullGeoLatitude? FullGeoLatitude;
    public List<string> AllQuadrantHouses { get; private set; }


    public CalcHouseComparisonModel(IGeoLatInputParser geoLatInputParser, IDateInputParser dateInputParser)
    {
        _geoLatInputParser = geoLatInputParser;
        _dateInputParser = dateInputParser;
        AllQuadrantHouses = new List<string>();
        PopulateHouses();
    }

    public bool IsGeoLatValid(string latitude)
    {
        Directions4GeoLat dir = Directions4GeoLat.North;
        bool isValid = _geoLatInputParser.HandleGeoLat(latitude, dir, out FullGeoLatitude? fullGeoLatitude);
        if (isValid) FullGeoLatitude=fullGeoLatitude;
        return isValid;
    }

    public bool IsDateValid(string inputDate)
    {
        Calendars cal = Calendars.Gregorian;
        YearCounts yearCount = YearCounts.Astronomical;
        bool isValid = _dateInputParser.HandleDate(inputDate, cal, yearCount, out FullDate? fullDate);
        if (isValid) FullDate = fullDate;
        return isValid;
    }

    public void CompareSystems(HouseSystems system1, HouseSystems system2)
    {
        // todo perform comparison
    
    }
    
    private void PopulateHouses()
    {
        List<HouseSystemDetails> houseSystems = HouseSystemsExtensions.AllDetails();
        AllQuadrantHouses = (from houseSystem in houseSystems 
            where houseSystem.QuadrantSystem select houseSystem.Text).ToList();
    }
    
}