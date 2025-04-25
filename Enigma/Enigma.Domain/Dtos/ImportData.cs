// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.Dtos;
// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


/// <summary>Standard data format for Enigma</summary>
/// <param name="Id">Id of chart</param>
/// <param name="Name">Name or description</param>
/// <param name="Lon">Geographic longitude in format ddd:mm:ss:E (or W)</param>
/// <param name="Lat">Geographic latitude in format dd:mm:ss N (or S)</param>
/// <param name="Cal">Calendar, 'J' for Julian, 'G' for Gregorian</param>
/// <param name="Date">Date in format yyyy/mm/dd</param>
/// <param name="Time">Time in format hh:mm:ss</param>
/// <param name="Zone">Offset for zone</param>
/// <param name="Dst">Offset for DST</param>
public record EnigmaData(  
    string Id,
    string Name,
    string Lon,
    string Lat,
    string Date,
    string Cal,
    string Time,
    double Zone,
    double Dst);




/// <summary>Data as exported from PlanetDance</summary>
/// <remarks>Example line: Rumen Kolev;1960;11;29;3;0;50;Varna;Bulgaria;27.916667;43.216667;2.000000;</remarks>
/// <param name="Name">Name or id</param>
/// <param name="Year">Year of birth</param>
/// <param name="Month">Month of birth</param>
/// <param name="Day">Day in month</param>
/// <param name="Hour">Hour 0..23</param>
/// <param name="Min">Minute</param>
/// <param name="Sec">Second</param>
/// <param name="Place">Name of location</param>
/// <param name="Country">Name of country</param>
/// <param name="Lon">Geographic longitude</param>
/// <param name="Lat">Geographic latitude</param>
/// <param name="Zone">Total offset, including offset for DST</param>
public record PlanetDanceData(
    string Name,
    int Year,
    int Month,
    int Day,
    int Hour,
    int Min,
    int Sec,
    string Place,
    string Country,
    double Lon,
    double Lat,
    double Zone);
