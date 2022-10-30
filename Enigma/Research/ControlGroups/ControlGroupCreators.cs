// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Engima.Domain.Research;
using Enigma.Domain.Persistency;
using Enigma.Research.Interfaces;

namespace Enigma.Research.ControlGroups;



public class StandardShiftControlGroupCreator : IControlGroupCreator
{
    private readonly IControlGroupRng _controlGroupRng;
    private readonly IControlDataCalendar _controlDataCalendar;
    private readonly List<StandardInputItem> _controlGroupItems = new();
    private readonly List<int> years = new();
    private readonly List<int> months = new();
    private readonly List<int> days = new();
    private readonly List<int> hours = new();
    private readonly List<int> minutes = new();
    private readonly List<int> seconds = new();
    private readonly List<double> dsts = new();
    private readonly List<double> zoneOffsets = new();
    private readonly List<double> latitudes = new();
    private readonly List<double> longitudes = new();

    public StandardShiftControlGroupCreator(IControlGroupRng controlGroupRng, IControlDataCalendar controlDataCalendar)
    {
        _controlGroupRng = controlGroupRng;
        _controlDataCalendar = controlDataCalendar;
    }


    public List<StandardInputItem> CreateMultipleControlData(List<StandardInputItem> inputItems, 
        ControlGroupTypes controlGroupType, 
        int multiplicity)
    {
        var allControlData = new List<StandardInputItem>();
        for (int i = 0; i < multiplicity; i++)
        {
            var controlDataForOneSet = CreateControlData(inputItems, controlGroupType);
            allControlData.AddRange(controlDataForOneSet);
        }
        return allControlData;
    }


    private List<StandardInputItem> CreateControlData(List<StandardInputItem> inputItems, ControlGroupTypes controlGroupType)
    {
        _controlGroupItems.Clear();
        ProcessInputData(inputItems);
        SortDaysAndShuffleOtherItems();
        ProcessData();
        return _controlGroupItems;

    }

    private void ProcessInputData(List<StandardInputItem> inputItems)
    {
        years.Clear();
        months.Clear();
        days.Clear();
        hours.Clear();
        minutes.Clear();
        seconds.Clear();
        dsts.Clear();
        zoneOffsets.Clear();
        latitudes.Clear();
        longitudes.Clear();

        foreach (var inputItem in inputItems)
        {
            years.Add(inputItem.Date.Year);
            months.Add(inputItem.Date.Month);
            days.Add(inputItem.Date.Day);
            hours.Add(inputItem.Time.Hour);
            minutes.Add(inputItem.Time.Minute);
            seconds.Add(inputItem.Time.Second);
            dsts.Add(inputItem.Time.Dst);
            zoneOffsets.Add(inputItem.Time.ZoneOffset);
            latitudes.Add(inputItem.GeoLatitude);
            longitudes.Add(inputItem.GeoLongitude);
        }
    }

    private void SortDaysAndShuffleOtherItems()
    {
        days.Sort();
        days.Reverse();
        _controlGroupRng.ShuffleList(years);
        _controlGroupRng.ShuffleList(months);
        _controlGroupRng.ShuffleList(days);
        _controlGroupRng.ShuffleList(hours);
        _controlGroupRng.ShuffleList(minutes);
        _controlGroupRng.ShuffleList(seconds);
        _controlGroupRng.ShuffleList(dsts);
        _controlGroupRng.ShuffleList(zoneOffsets);
        _controlGroupRng.ShuffleList(latitudes);
        _controlGroupRng.ShuffleList(longitudes);
    }

    private void ProcessData()
    {
        int counter = 0;
        while (years.Count > 0) 
        {
            int year = GetFromList(years);
            int day = GetFromList(days);
            int month = FindMonth(day, year);
            int hour = GetFromList(hours);
            int minute = GetFromList(minutes);
            int second = GetFromList(seconds);
            double dst = GetFromList(dsts);
            double zoneOffset = GetFromList(zoneOffsets);
            double latitude = GetFromList(latitudes);
            double longitude = GetFromList(longitudes);

            PersistableDate date = new(year, month, day, "G");      // TODO add support for Julian? Or use always Gregorian?
            PersistableTime time = new(hour, minute, second, zoneOffset, dst);
            int id = counter++;
            string name = "Controldata " + id;
            _controlGroupItems.Add(new StandardInputItem(id.ToString(), name, longitude, latitude, date, time));
        }
    }

    private int FindMonth(int day, int year)
    {
        bool found = false;
        int month = 0;
        int counter = 0;
        while (!found && counter < months.Count)
        {
            month = months[counter];
            if (_controlDataCalendar.DayFitsInMonth(day, month, year)) {
                found = true;
                months.Remove(counter);
            }
            counter++;
        }
        return month;
    }

    private static int GetFromList(List<int> theList)
    {
        int result = theList[0];
        theList.Remove(0);
        return result;
    }

    private static double GetFromList(List<double> theList)
    {
        double result = theList[0];
        theList.Remove(0);
        return result;
    }

}

public class ControlDataCalendar : IControlDataCalendar
{
    private readonly int[] months31Array = { 2, 3, 5, 7, 8, 10, 12};
    private readonly int[] months30Array = { 1, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12};

    public bool DayFitsInMonth(int day, int month, int year)
    {
        List<int> months31 = new();
        List<int> months30 = new();
        months31.AddRange(months31Array);
        months30.AddRange(months30Array);
        return ( day < 29 
            || day == 29 && 2 != month
            || day == 30 && months30.Contains(month)
            || day == 31 && months31.Contains(month)
            || IsLeapYear(year) && day < 30);
    }

    private static bool IsLeapYear(int year)
    {
        return year % 400 == 0 || year % 100 != 0 && year % 4 == 0;
    }
}