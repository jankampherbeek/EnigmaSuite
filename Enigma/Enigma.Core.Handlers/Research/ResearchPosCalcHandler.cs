// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Persistency;
using Enigma.Domain.Points;
using Serilog;

namespace Enigma.Core.Handlers.Research;

// TODO 0.1 Analysis

public sealed class ResearchPosCalcHandler
{

    public List<Tuple<ChartPoints, double>> CalculateLongitudes(StandardInput standardInput, List<ChartPoints> points)
    {
        List<Tuple<ChartPoints, double>> calculatedPositions = new();
        List<ChartPoints> mundanePoints = new();
        List<ChartPoints> celPoints = new();
        List<ChartPoints> cusps = new();
        List<ChartPoints> zodiacalPoints = new();
        foreach (ChartPoints point in points)
        {
            PointCats cat = point.GetDetails().PointCat;
            switch (cat)
            {
                case PointCats.Classic:
                case PointCats.Modern:
                case PointCats.Minor:
                case PointCats.MathPoint:
                case PointCats.Hypothetical:
                    celPoints.Add(point);
                    break;
                case PointCats.Mundane:
                    mundanePoints.Add(point);
                    break;
                case PointCats.Cusp:
                    cusps.Add(point);
                    break;
                case PointCats.Arabic:
                    break;
                case PointCats.Zodiac:
                    zodiacalPoints.Add(point);
                    break;
                default:
                    string errorText = "ResearchPosCalcHandler.CalculateLOngitudes(): Unreconized PointCats : " + cat.ToString();
                    Log.Error(errorText);
                    throw new ArgumentException(errorText);
            }
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
        foreach (ChartPoints point in celPoints)
        {
            // perform calculation for celPOints
        }
        foreach (ChartPoints point in zodiacalPoints)
        {
            if (point == ChartPoints.ZeroAries)
            {
                calculatedPositions.Add(new Tuple<ChartPoints, double>(point, 0.0));
            }
            if (point == ChartPoints.ZeroCancer)
            {
                calculatedPositions.Add(new Tuple<ChartPoints, double>(point, 90.0));
            }
        }
        return calculatedPositions;
    }

}