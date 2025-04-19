// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Persistables;
using Enigma.Domain.References;

namespace Enigma.Core.Research;

public interface IControlGroupCreator
{
    public List<StandardInputItem> CreateMultipleControlData(List<StandardInputItem> inputItems,
        ControlGroupTypes controlGroupType,
        int multiplicity);

}

public interface IControlDataCalendar
{
    public bool DayFitsInMonth(int day, int month, int year);
}





public sealed class StandardShiftControlGroupCreator(
    IControlGroupRng controlGroupRng,
    IControlDataCalendar controlDataCalendar)
    : IControlGroupCreator
{
    private readonly List<StandardInputItem> _controlGroupItems = [];
    private readonly List<int> _years = [];
    private readonly List<int> _months = [];
    private readonly List<int> _days = [];
    private readonly List<int> _hours = [];
    private readonly List<int> _minutes = [];
    private readonly List<int> _seconds = [];
    private readonly List<double> _dsts = [];
    private readonly List<double> _zoneOffsets = [];
    private readonly List<double> _latitudes = [];
    private readonly List<double> _longitudes = [];


    public List<StandardInputItem> CreateMultipleControlData(List<StandardInputItem> inputItems,
        ControlGroupTypes controlGroupType,
        int multiplicity)
    {
        var allControlData = new List<StandardInputItem>();
        for (var i = 0; i < multiplicity; i++)
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
            _years.Add(inputItem.Year);
            _months.Add(inputItem.Month);
            _days.Add(inputItem.Day);
            _hours.Add(inputItem.Hour);
            _minutes.Add(inputItem.Minute);
            _seconds.Add(inputItem.Second);
            _dsts.Add(inputItem.Dst);
            _zoneOffsets.Add(inputItem.ZoneOffset);
            _latitudes.Add(inputItem.GeoLatitude);
            _longitudes.Add(inputItem.GeoLongitude);
        }
    }

    private void SortDaysAndShuffleOtherItems()
    {
        _days.Sort();
        _days.Reverse();
        controlGroupRng.ShuffleList(_years);
        controlGroupRng.ShuffleList(_months);
        controlGroupRng.ShuffleList(_days);
        controlGroupRng.ShuffleList(_hours);
        controlGroupRng.ShuffleList(_minutes);
        controlGroupRng.ShuffleList(_seconds);
        controlGroupRng.ShuffleList(_dsts);
        controlGroupRng.ShuffleList(_zoneOffsets);
        controlGroupRng.ShuffleList(_latitudes);
        controlGroupRng.ShuffleList(_longitudes);
    }

    private void ProcessData(int sequence)
    {
        var counter = 0;
        while (_years.Count > 0)
        {
            var year = GetFromList(_years);
            var day = GetFromList(_days);
            var month = FindMonth(day, year);
            var hour = GetFromList(_hours);
            var minute = GetFromList(_minutes);
            var second = GetFromList(_seconds);
            var dst = GetFromList(_dsts);
            var zoneOffset = GetFromList(_zoneOffsets);
            var latitude = GetFromList(_latitudes);
            var longitude = GetFromList(_longitudes);

            // For now only support for Gregorian calendar
            const string cal = "G";
            var id = counter++;
            var name = "Controldata " + sequence + "-" + id;
            _controlGroupItems.Add(new StandardInputItem(sequence + "-" + id, name, longitude, latitude, year, month,
                day, cal, hour, minute, second, zoneOffset, dst));
            
        }
    }

    private int FindMonth(int day, int year)
    {
        var found = false;
        var month = 0;
        var counter = 0;
        while (!found && counter < _months.Count)
        {
            month = _months[counter];
            if (controlDataCalendar.DayFitsInMonth(day, month, year))
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
        var result = theList[0];
        theList.RemoveAt(0);
        return result;
    }

    private static double GetFromList(IList<double> theList)
    {
        var result = theList[0];
        theList.RemoveAt(0);
        return result;
    }

}

public class ControlDataCalendar : IControlDataCalendar
{
    private readonly int[] _months31Array = [2, 3, 5, 7, 8, 10, 12];
    private readonly int[] _months30Array = [1, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12];

    public bool DayFitsInMonth(int day, int month, int year)
    {
        List<int> months31 = [];
        List<int> months30 = [];
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