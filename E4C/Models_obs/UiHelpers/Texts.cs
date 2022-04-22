// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.be.persistency;
using E4C.Models.Domain;
using E4C.domain.shared.references;
using System.Collections.Generic;
using E4C.Shared.References;

namespace E4C.Models.UiHelpers
{



    /// <summary>
    /// Manage texts that are stored in an external dictionary file.
    /// </summary>
    public interface IRosetta
    {
        /// <summary>
        /// Retrieve text for a specific id.
        /// </summary>
        /// <param name="id">The id to search.</param>
        /// <returns>The text for the Id. Returns the string '-NOT FOUND-' if the text could not be found.</returns>
        public string TextForId(string id);
    }

    public class Rosetta : IRosetta
    {
        readonly private ITextFromFileReader _fileReader;
        readonly private List<KeyValuePair<string, string>> _texts;

        public Rosetta(ITextFromFileReader fileReader)
        {
            _fileReader = fileReader;
            _texts = new List<KeyValuePair<string, string>>();
            ReadTextsFromFile();
        }

        public string TextForId(string id)
        {
            foreach (KeyValuePair<string, string> _text in _texts)
            {
                if (_text.Key == id) return _text.Value;
            }
            return "-NOT FOUND-";
        }

        private void ReadTextsFromFile()
        {
            string[] _partsOfText;
            string _filename = @"..\..\..\res\texts.txt";
            IEnumerable<string> _allLines = _fileReader.ReadSeparatedLines(_filename);

            foreach (string _line in _allLines)
            {
                _partsOfText = _line.Split('=');
                // skip empty lines and remarks
                if (_partsOfText.Length == 2)
                {
                    _texts.Add(new KeyValuePair<string, string>(_partsOfText[0].Trim(), _partsOfText[1].Trim()));
                }
            }
        }

    }





    /// <summary>
    /// Assemble texts from different components.
    /// </summary>
    public interface ITextAssembler
    {


        /// <summary>
        /// Create a text for a full date.
        /// </summary>
        /// <param name="dateValues">Year, month and day, int htat sequence.</param>
        /// <param name="calendar">Calendar used.</param>
        /// <param name="yearCount">Yearcount used.</param>
        /// <returns>A string in a format as follows: January 29, 1953. Gregorian, CE.</returns>
        string CreateDayFullText(int[] dateValues, Calendars calendar, YearCounts yearCount);

        /// <summary>
        /// Create a text for a full time.
        /// </summary>
        /// <param name="timeValues">Hour, minute and second, in that sequence.</param>
        /// <param name="timeZone">Timezone used.</param>
        /// <param name="dst">True if dst is used.</param>
        /// <param name="lmtLongValues"></param>
        /// <returns>A string in a format as follows: 8:37:30, CET +1.0, no DST.
        /// If DST was used replace 'no DST' with 'DST applied.'
        /// If timezone is LMT, the text for timezone is as follows 'LMT for 6°54'00" East'.</returns>
        string CreateTimeFullText(int[] timeValues, TimeZones timeZone, bool dst, int[]? lmtLongValues = null, Directions4GeoLong? dirGeoLong = null);
    }

    public class TextAssembler : ITextAssembler
    {
        private readonly IRosetta _rosetta;
        private readonly ITimeZoneSpecifications _timeZoneSpecifications;

        public TextAssembler(IRosetta rosetta, ITimeZoneSpecifications timeZoneSpecifications)
        {
            _rosetta = rosetta;
            _timeZoneSpecifications = timeZoneSpecifications;
        }

        public string CreateDayFullText(int[] dateValues, Calendars calendar, YearCounts yearCount)
        {
            string _monthText = GetMonthTextFromNumber(dateValues[1]);
            string _resourceBundleId = calendar == Calendars.Gregorian ? "ref.enum.calendar.gregorian" : "ref.enum.calendar.julian";
            string _calendarText = _rosetta.TextForId(_resourceBundleId);
            if (yearCount == YearCounts.BCE)
            {
                _resourceBundleId = "ref.enum.yearcount.bce";
            }
            else if (yearCount == YearCounts.CE)
            {
                _resourceBundleId = "ref.enum.yearcount.ce";
            }
            else
            {
                _resourceBundleId = "ref.enum.yearcount.astronomical";
            }
            string _yearCountText = _rosetta.TextForId(_resourceBundleId);
            return $"{_monthText} {dateValues[2]}, {dateValues[0]}. {_calendarText}, {_yearCountText}.";
            //            string _expected = "8:37:30, LMT for 6°54'00\" East, no DST.";

        }

        public string CreateTimeFullText(int[] timeValues, TimeZones timeZone, bool dst, int[]? lmtLongValues = null, Directions4GeoLong? dirLmt = null)
        {
            string _resourceBundleId = dst ? "common.time.dst.used" : "common.time.dst.notused";
            string dstText = _rosetta.TextForId(_resourceBundleId);
            TimeZoneDetails _timeZoneDetails = _timeZoneSpecifications.DetailsForTimeZone(timeZone);
            _resourceBundleId = _timeZoneDetails.TextId;
            string _timeZoneText = _rosetta.TextForId(_resourceBundleId);
            string _lmtLongText = "";
            if (timeZone == TimeZones.LMT && lmtLongValues != null)
            {
                string _forText = _rosetta.TextForId("common.for");
                string _dirTextId = dirLmt == Directions4GeoLong.East ? "common.location.dirgeolong.east" : "common.location.dirgeolong.west";
                string _dirText = _rosetta.TextForId(_dirTextId);
                _lmtLongText = $" {_forText} {lmtLongValues[0]}°{lmtLongValues[1]:D2}'{lmtLongValues[2]:D2}\" {_dirText}";
            }
            return $"{timeValues[0]}:{timeValues[1]:D2}:{timeValues[2]:D2}, {_timeZoneText}{_lmtLongText}, {dstText}.";
        }

        private string GetMonthTextFromNumber(int monthId)
        {
            string[] postFixes = new string[] { "jan", "feb", "mar", "apr", "may", "jun", "jul", "aug", "sep", "oct", "nov", "dec" };
            string postFixIdForResourceBundle = postFixes[monthId - 1];
            return _rosetta.TextForId("common.date.month." + postFixIdForResourceBundle);
        }
    }

}