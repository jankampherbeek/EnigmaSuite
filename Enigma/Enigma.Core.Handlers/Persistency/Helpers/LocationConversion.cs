// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers.Interfaces;
using Enigma.Domain.Constants;

namespace Enigma.Core.Handlers.Persistency.Helpers;


/// <inheritdoc/>
public sealed class LocationCheckedConversion : ILocationCheckedConversion
{

    /// <inheritdoc/>
    public Tuple<double, bool> StandardCsvToLongitude(string csvLocation)
    {
        return StandardCsvToValue(csvLocation, "E", "W", 180);
    }

    /// <inheritdoc/>
    public Tuple<double, bool> StandardCsvToLatitude(string csvLocation)
    {
        return StandardCsvToValue(csvLocation, "N", "S", 90);
    }


    private static Tuple<double, bool> StandardCsvToValue(string csvLocation, string dirPlus, string dirMin, int degreeLimit)
    {
        double calculatedValue = 0.0;
        bool noErrors = true;
        string[] items = csvLocation.Split(':');
        if (items.Length == 4)
        {
            double direction = 0.0;
            if (dirPlus.Equals(items[3].Trim().ToUpper())) direction = 1.0;
            else if (dirMin.Equals(items[3].Trim().ToUpper())) direction = -1;
            else noErrors = false;

            if (!noErrors) return new Tuple<double, bool>(calculatedValue, noErrors);
            Tuple<double, bool> validatedValue = SexagTextsToDouble(items[0], items[1], items[2]);
            if (validatedValue.Item2)
            {
                calculatedValue = validatedValue.Item1;
                if (calculatedValue >= degreeLimit)
                {
                    calculatedValue = 0.0;
                    noErrors = false;
                }
                else calculatedValue *= direction;
            }
            else noErrors = false;
        }
        else noErrors = false;
        return new Tuple<double, bool>(calculatedValue, noErrors);
    }

    private static Tuple<double, bool> SexagTextsToDouble(string degreeText, string minuteText, string secondText)
    {
        bool noErrors = true;
        bool result = int.TryParse(degreeText, out int degrees);
        if (!result || (degrees < 0)) noErrors = false;
        result = int.TryParse(minuteText, out int minutes);
        if (!result || (minutes < 0) || (minutes > 59)) noErrors = false;
        result = int.TryParse(secondText, out int seconds);
        if (!result || (seconds < 0) || (seconds > 59)) noErrors = false;
        return new Tuple<double, bool>(degrees
            + ((double)minutes / EnigmaConstants.MINUTES_PER_HOUR_DEGREE)
            + ((double)seconds / EnigmaConstants.SECONDS_PER_HOUR_DEGREE),
            noErrors);
    }

}




