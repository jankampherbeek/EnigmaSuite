// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Research.Interfaces;
using Enigma.Domain.Persistables;
using Enigma.Domain.References;

namespace Enigma.Core.Research.Helpers;



public sealed class StandardShiftControlGroupCreator : IControlGroupCreator
{
    private readonly IControlGroupRng _controlGroupRng;
    private readonly IControlDataCalendar _controlDataCalendar;
    private readonly List<StandardInputItem> _controlGroupItems = new();
    private readonly List<int> _years = new();
    private readonly List<int> _months = new();
    private readonly List<int> _days = new();
    private readonly List<int> _hours = new();
    private readonly List<int> _minutes = new();
    private readonly List<int> _seconds = new();
    private readonly List<double> _dsts = new();
    private readonly List<double> _zoneOffsets = new();
    private readonly List<double> _latitudes = new();
    private readonly List<double> _longitudes = new();

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
            var controlDataForOneSet = CreateControlData(inputItems, i);
            allControlData.AddRange(controlDataForOneSet);
        }
        return allControlData;
    }


    private IEnumerable<StandardInputItem> CreateControlData(List<StandardInputItem> inputItems, int sequence)
    {
        _controlGroupItems.Clear();
        ProcessInputData(inputItems);
        SortDaysAndShuffleOtherItems();
        ProcessData(sequence);
        return _controlGroupItems;

    }

    private void ProcessInputData(List<StandardInputItem> inputItems)
    {
        _years.Clear();
        _months.Clear();
        _days.Clear();
        _hours.Clear();
        _minutes.Clear();
        _seconds.Clear();
        _dsts.Clear();
        _zoneOffsets.Clear();
        _latitudes.Clear();
        _longitudes.Clear();


        foreach (var inputItem in inputItems)
        {
            PersistableDate date = inputItem.Date!;
            PersistableTime time = inputItem.Time!;
            _years.Add(date.Year);
            _months.Add(date.Month);
            _days.Add(date.Day);
            _hours.Add(time.Hour);
            _minutes.Add(time.Minute);
            _seconds.Add(time.Second);
            _dsts.Add(time.Dst);
            _zoneOffsets.Add(time.ZoneOffset);
            _latitudes.Add(inputItem.GeoLatitude);
            _longitudes.Add(inputItem.GeoLongitude);
        }
    }

    private void SortDaysAndShuffleOtherItems()
    {
        _days.Sort();
        _days.Reverse();
        _controlGroupRng.ShuffleList(_years);
        _controlGroupRng.ShuffleList(_months);
        _controlGroupRng.ShuffleList(_days);
        _controlGroupRng.ShuffleList(_hours);
        _controlGroupRng.ShuffleList(_minutes);
        _controlGroupRng.ShuffleList(_seconds);
        _controlGroupRng.ShuffleList(_dsts);
        _controlGroupRng.ShuffleList(_zoneOffsets);
        _controlGroupRng.ShuffleList(_latitudes);
        _controlGroupRng.ShuffleList(_longitudes);
    }

    private void ProcessData(int sequence)
    {
        int counter = 0;
        while (_years.Count > 0)
        {
            int year = GetFromList(_years);
            int day = GetFromList(_days);
            int month = FindMonth(day, year);
            int hour = GetFromList(_hours);
            int minute = GetFromList(_minutes);
            int second = GetFromList(_seconds);
            double dst = GetFromList(_dsts);
            double zoneOffset = GetFromList(_zoneOffsets);
            double latitude = GetFromList(_latitudes);
            double longitude = GetFromList(_longitudes);

            PersistableDate date = new(year, month, day, "G");      // TODO 0.2 add support for Julian? Or use always Gregorian?
            PersistableTime time = new(hour, minute, second, zoneOffset, dst);
            int id = counter++;
            string name = "Controldata " + sequence + "-" + id;
            _controlGroupItems.Add(new StandardInputItem(sequence + "-" + id, name, longitude, latitude, date, time));
        }
    }

    private int FindMonth(int day, int year)
    {
        bool found = false;
        int month = 0;
        int counter = 0;
        while (!found && counter < _months.Count)
        {
            month = _months[counter];
            if (_controlDataCalendar.DayFitsInMonth(day, month, year))
            {
                found = true;
                _months.RemoveAt(counter);
            }
            counter++;
        }
        return month;
    }

    private static int GetFromList(IList<int> theList)
    {
        int result = theList[0];
        theList.RemoveAt(0);
        return result;
    }

    private static double GetFromList(IList<double> theList)
    {
        double result = theList[0];
        theList.RemoveAt(0);
        return result;
    }

}

public class ControlDataCalendar : IControlDataCalendar
{
    private readonly int[] _months31Array = { 2, 3, 5, 7, 8, 10, 12 };
    private readonly int[] _months30Array = { 1, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };

    public bool DayFitsInMonth(int day, int month, int year)
    {
        List<int> months31 = new();
        List<int> months30 = new();
        months31.AddRange(_months31Array);
        months30.AddRange(_months30Array);
        return day < 29
            || (day == 29 && 2 != month)
            || (day == 30 && months30.Contains(month))
            || (day == 31 && months31.Contains(month))
            || (IsLeapYear(year) && day < 30);
    }

    private static bool IsLeapYear(int year)
    {
        return year % 400 == 0 || (year % 100 != 0 && year % 4 == 0);
    }
}