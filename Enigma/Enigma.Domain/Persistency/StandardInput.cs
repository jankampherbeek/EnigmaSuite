// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.Persistency;

/// <summary>Representation of Standard Input as used in JSON.</summary>
public class StandardInput
{
    public string DataName { get; }
    public string Creation { get; }
    public List<StandardInputItem> ChartData { get; }

    public StandardInput(string dataName, string creation, List<StandardInputItem> chartData)
    {
        DataName = dataName;
        Creation = creation;
        ChartData = chartData;
    }
}



/// <summary>Representation of a single item using Standard Input.</summary>
public class StandardInputItem
{

    public string Id { get; }
    public string Name { get; }
    public double GeoLongitude { get; }
    public double GeoLatitude { get; }
    public PersistableDate? Date { get; }
    public PersistableTime? Time { get; }


    public StandardInputItem(string id, string name,
        double geoLongitude, double geoLatitude,
        PersistableDate? date, PersistableTime? time)
    {
        Id = id;
        Name = name;
        GeoLongitude = geoLongitude;
        GeoLatitude = geoLatitude;
        Date = date;
        Time = time;
    }

}
