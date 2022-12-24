// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Persistency;
using Enigma.Research.Domain;

namespace Enigma.Core.Handlers.Research;


public sealed class ResearchPosCalcHandler
{

    public List<Tuple<ResearchPoint, double>> CalculateLongitudes(StandardInput standardInput, List<ResearchPoint> points)
    {
        List<Tuple<ResearchPoint, double>> calculatedPositions = new();
        List<ResearchPoint> mundanePoints = new();
        List<ResearchPoint> celPoints = new();
        List<ResearchPoint> cusps = new();
        List<ResearchPoint> zodiacalPoints = new();
        foreach (ResearchPoint point in points)
        {
            if (point.Id < 1000) celPoints.Add(point);
            if (1000 <= point.Id && point.Id < 2000) mundanePoints.Add(point);
            if (2000 <= point.Id && point.Id < 3000) cusps.Add(point);
            if (3000 <= point.Id) zodiacalPoints.Add(point);
        }

        foreach (StandardInputItem item in standardInput.ChartData)
        {
            string itemId = item.Id;
            double geoLat = item.GeoLatitude;
            double geoLon = item.GeoLongitude;
            double year = item.Date.Year;
            double month = item.Date.Month;
            double day = item.Date.Day;
            var cal = item.Date.Calendar;
            double hour = item.Time.Hour;
            double minute = item.Time.Minute;
            double second = item.Time.Second;
            double dst = item.Time.Dst;
            double offSet = item.Time.ZoneOffset;

        }



        // calc mundane points and cusps
        foreach (ResearchPoint point in celPoints)
        {
            // perform calculation for celPOints
        }
        foreach (ResearchPoint point in zodiacalPoints)
        {
            if (point.Id == 3000)    // Zero Aries
            {
                calculatedPositions.Add(new Tuple<ResearchPoint, double>(point, 0.0));
            }
            if (point.Id == 3001)    // Zero Cancer
            {
                calculatedPositions.Add(new Tuple<ResearchPoint, double>(point, 90.0));
            }
        }




        return calculatedPositions;
    }

}