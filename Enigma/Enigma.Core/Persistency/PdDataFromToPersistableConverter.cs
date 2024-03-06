// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Globalization;
using Enigma.Core.Handlers;
using Enigma.Domain.Constants;
using Enigma.Domain.Dtos;
using Enigma.Domain.Persistables;
using Enigma.Domain.References;

namespace Enigma.Core.Persistency;

/// <summary>Converts between PlanetDance data and the persistables, in both directions.</summary>
public interface IPdDataFromToPersistableConverter
{
    public bool ConvertPdDataToPersistables(IEnumerable<string> csvLines, 
        out List<PersistableChartData> allPersistableChartData);
}


public class PdDataFromToPersistableConverter: IPdDataFromToPersistableConverter
{
    private readonly IChartDataDao _chartDataDao;
    private readonly ITextFileWriter _textFileWriter;
    private readonly IJulDayHandler _julDayHandler;
    private readonly List<string> _errorLines = new();
    private readonly string _fullErrorPath = ApplicationSettings.LocationDataFiles + Path.DirectorySeparatorChar + "errors_od_import.txt";
    private List<PersistableChartData> _allPersistableChartData = new();
    
  
    
    public PdDataFromToPersistableConverter(IChartDataDao chartDataDao, ITextFileWriter textFileWriter, IJulDayHandler julDayHandler)
    {
        _chartDataDao = chartDataDao;
        _textFileWriter = textFileWriter;
        _julDayHandler = julDayHandler;
    }
    
    public bool ConvertPdDataToPersistables(IEnumerable<string> csvLines, out List<PersistableChartData> foundPersistableChartData)
    {
        bool success = csvLines.Aggregate(true, (current, csvLine) => current && ProcessLine(csvLine));
        foundPersistableChartData = _allPersistableChartData;
        if (!success) _textFileWriter.WriteFile(_fullErrorPath, _errorLines);
        return success;
    }

    private bool ProcessLine(string csvLine)
    {
        bool success = true;
        string dateText = "";
        string timeText = "";
        string locationName = "";
        double jdForEt = 0.0;
        double geoLong = 0.0;
        double geoLat = 0.0;

        string[] elements = csvLine.Split(';');
        try
        {
            success = success &&
               ConstructDateText(elements[1], elements[2], elements[3], out dateText); // year, month, day
            success = success && ConstructTimeText(elements[4], elements[5], elements[6], elements[11],
                          out timeText); // hour, minute, second, offset
            success = success && double.TryParse(elements[9].Replace(',', '.'), System.Globalization.NumberStyles.Any,
                CultureInfo.InvariantCulture, out geoLong);
            success = success && double.TryParse(elements[10].Replace(',', '.'), System.Globalization.NumberStyles.Any,
                CultureInfo.InvariantCulture, out geoLat);
            success = success &&
                      ConstructLocationName(elements[7], elements[8], geoLong, geoLat,
                          out locationName); // city, country, longitude, latitude
            success = success && CalculateJd(elements[1], elements[2], elements[3], elements[4], elements[5],
                elements[6], elements[11], out jdForEt); // y,m,d,h,m,s,offset
        }
        catch (Exception e)
        {
            success = false;
        }
        if (success)
        {
            const string source = "Not defined";
            const long chartCategoryId = 7; // Undefined
            const long ratingId = 1; // Unknown
            PersistableChartIdentification pcIdent = new();
            pcIdent.Id = -1;
            pcIdent.ChartCategoryId = chartCategoryId;
            pcIdent.Description = "";
            pcIdent.Name = elements[0];
            PersistableChartDateTimeLocation pcChartDTL = new();
            pcChartDTL.Id = -1;
            pcChartDTL.ChartId = -1;
            pcChartDTL.Source = source;
            pcChartDTL.DateText = dateText;
            pcChartDTL.TimeText = timeText;
            pcChartDTL.LocationName = locationName;
            pcChartDTL.RatingId = ratingId;
            pcChartDTL.GeoLong = geoLong;
            pcChartDTL.GeoLat = geoLat;
            pcChartDTL.JdForEt = jdForEt;
            List<PersistableChartDateTimeLocation> allDateTimes = new();
            allDateTimes.Add((pcChartDTL));
            PersistableChartData pcData = new(pcIdent, allDateTimes);
            _allPersistableChartData.Add(pcData);
        }
        else _errorLines.Add(csvLine);
        return success;
    }

    private bool ConstructDateText(string yearTxt, string monthTxt, string dayTxt, out string dateTxt)
    {
        string sep = "/";
        dateTxt = yearTxt + sep + monthTxt + sep + dayTxt + " " + "G";
        return yearTxt.Length > 0 && monthTxt.Length > 0 && dayTxt.Length > 0;
    }
    
    private bool ConstructTimeText(string hourTxt, string minuteTxt, string secTxt, string offSetTxt, out string timeTxt)
    {
        string sep = ":";
        double offSetValue = 0.0;
        if (secTxt.Length == 0) secTxt = "0";
        if (offSetTxt.Length == 0) offSetTxt = "0";
        else
        {
            bool success = double.TryParse(offSetTxt.Replace(',', '.'), System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out offSetValue);
            int hour = (int)Math.Floor(offSetValue);
            double fullMinute = Math.Floor((offSetValue - hour) * 60.0); 
            int minute = (int)fullMinute;
            int second = (int)Math.Floor((fullMinute - minute) * 60.0);
            string plusMinus = offSetValue >= 0.0 ? "+" : "-";
            offSetTxt = plusMinus + " " + hour + sep + minute + sep + second;
        }
        timeTxt = hourTxt + sep + minuteTxt + sep + secTxt + " " + "Zone: " + offSetTxt;
        return hourTxt.Length > 0 && minuteTxt.Length > 0;
    }
    
    
    private bool ConstructLocationName(string city, string country, double geoLong, double geoLat, out string locationName)
    {
        // TODO a lot of repeated code, move conversions to backend.
        string latDir = geoLat >= 0.0 ? "N" : "S";
        string longDir = geoLong >= 0.0 ? "E" : "W";
        string latSexag = ConvertDoubleToPositionsDmsText(Math.Abs(geoLat));
        string longSexag = ConvertDoubleToPositionsDmsText(Math.Abs(geoLong));
        string sep = " ";
        string coordinateText = longSexag + longDir + sep + latSexag + latDir; 
        if (city.Length < 1) city = "Place undefined";
        locationName = city + sep + country + sep + coordinateText;
        return true;
    }

    private string ConvertDoubleToPositionsDmsText(double position)
    {
        string minusSign = position < 0.0 ? "-" : "";
        const double correctionForDouble = 0.00000001;    // correction to prevent double values like 0.99999999999
        double remaining = Math.Abs(position) + correctionForDouble;
        if (remaining >= 360.0) remaining-= 360.0;
        int degrees = (int)remaining;
        remaining -= degrees;
        int minutes = (int)(remaining * 60.0);
        remaining -= minutes / 60.0;
        int seconds = (int)(remaining * 3600.0);
        return minusSign + CreateDmsString(degrees, minutes, seconds);

    }
    private static string CreateDmsString(int degrees, int minutes, int seconds)
    {
        string degreeText = degrees.ToString();
        string minuteText = $"{minutes:00}";
        string secondText = $"{seconds:00}";
        return degreeText + EnigmaConstants.DEGREE_SIGN + minuteText + EnigmaConstants.MINUTE_SIGN + secondText + EnigmaConstants.SECOND_SIGN;
    }



    private bool CalculateJd(string yearTxt, string monthTxt, string dayTxt, string hourTxt, string minuteTxt, string secTxt, string offsetTxt, out double jdValue)
    {
        int yearValue = 0;
        int monthValue = 0;
        int dayValue = 0;
        int hourValue = 0;
        int minuteValue = 0;
        int secValue = 0;
        double offsetValue = 0.0;
        jdValue = 0.0;
        bool success = int.TryParse(yearTxt, out yearValue);
        success = success && int.TryParse(monthTxt, out monthValue);
        success = success && int.TryParse(dayTxt, out dayValue);
        success = success && int.TryParse(hourTxt, out hourValue);
        success = success && int.TryParse(minuteTxt, out minuteValue);
        success = success && int.TryParse(secTxt, out secValue);
        success = success && double.TryParse(offsetTxt.Replace(',', '.'), System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out offsetValue);
        if (!success) return success;
        double correctionForDayChange = 0.0;
        double ut = hourValue + minuteValue / 60.0 + secValue / 3600.0 - offsetValue;    
        if (ut >= 24.0)
        {
            ut -= 24.0;
            correctionForDayChange = 1.0;
        }
        if (ut < 0.0)
        {
            ut += 24.0;
            correctionForDayChange = -1.0;
        }
        Calendars cal = Calendars.Gregorian;
        SimpleDateTime dateTime = new SimpleDateTime(yearValue, monthValue, dayValue, ut, cal);
        jdValue = _julDayHandler.CalcJulDay(dateTime).JulDayEt + correctionForDayChange;
        return success;
    }


}


