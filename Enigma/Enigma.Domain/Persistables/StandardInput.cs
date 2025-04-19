// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Requests;

namespace Enigma.Domain.Persistables;

/// <summary>Representation of Standard Input as used for persistency.</summary>
public record StandardInput(string DataName, string Creation, List<StandardInputItem> ChartData);



/// <summary>Representation of a single item using Standard Input.</summary>
public record StandardInputItem(
    string Id,
    string Name,
    double GeoLongitude,
    double GeoLatitude,
    int Year,
    int Month,
    int Day,
    string Calendar,
    int Hour,
    int Minute,
    int Second,
    double ZoneOffset,
    double Dst);