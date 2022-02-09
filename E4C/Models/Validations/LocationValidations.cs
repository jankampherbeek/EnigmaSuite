using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E4C.Models.Domain;


namespace E4C.Models.Validations
{

    /// <summary>
    /// Validations for the inputs for geographic longitude and geographic latitude.
    /// </summary>
    public interface ILocationValidations
    {
        /// <summary>
        /// Validate the input for geographic longitude. Checks the values for degrees, minutes and seconds.
        /// </summary>
        /// <param name="degrees">The text with the degrees.</param>
        /// <param name="minutes">The text with the minutes.</param>
        /// <param name="seconds">The text with the seconds,</param>
        /// <returns>List with error codes.</returns>
        public List<int> ValidateGeoLongitude(string degrees, string minutes, string seconds);

        /// <summary>
        /// Validate the input for geographic longitude. Checks the values for degrees, minutes and seconds.
        /// </summary>
        /// <param name="degrees">The text with the degrees.</param>
        /// <param name="minutes">The text with the minutes.</param>
        /// <param name="seconds">The text with the seconds,</param>
        /// <returns>List with error codes.</returns>
        public List<int> ValidateGeoLatitude(string degrees, string minutes, string seconds);
    }


    public class LocationValidations : ILocationValidations
    {

        public List<int> ValidateGeoLongitude(string degrees, string minutes, string seconds)
        {
            List<int> _errorCodes = new();
            try
            {
                int[] _parsedValues = ParseDegreesMinutesSeconds(degrees, minutes, seconds);
                if (!CheckDegreesMinutesSeconds(_parsedValues, Constants.GEOLON_DEGREE_MIN, Constants.GEOLON_DEGREE_MAX))
                {
                    _errorCodes.Add(ErrorCodes.ERR_INVALID_GEOLON);
                }
                // check values for exactly 180 degrees
                if (_parsedValues[0] == Constants.GEOLON_DEGREE_MAX && (_parsedValues[1] > Constants.MINUTE_MIN || _parsedValues[2] > Constants.SECOND_MIN))
                {
                    _errorCodes.Add(ErrorCodes.ERR_INVALID_GEOLON);
                }
            }
            catch (Exception ex)
            {
                _errorCodes.Add(ErrorCodes.ERR_INVALID_GEOLON);
            }
            return _errorCodes;
        }

        public List<int> ValidateGeoLatitude(string degrees, string minutes, string seconds)
        {
            List<int> _errorCodes = new();
            try
            {
                int[] _parsedValues = ParseDegreesMinutesSeconds(degrees, minutes, seconds);
                if (!CheckDegreesMinutesSeconds(_parsedValues, Constants.GEOLAT_DEGREE_MIN, Constants.GEOLAT_DEGREE_MAX))
                {
                    _errorCodes.Add(ErrorCodes.ERR_INVALID_GEOLAT);
                }
            }
            catch (Exception ex)
            {
                _errorCodes.Add(ErrorCodes.ERR_INVALID_GEOLAT);
            }
            return _errorCodes;
        }

        private int[] ParseDegreesMinutesSeconds(string degrees, string minutes, string seconds)
        {
            int[] _parsedValues = new int[3];
            _parsedValues[0] = int.Parse(degrees);
            _parsedValues[1] = int.Parse(minutes);
            _parsedValues[2] = int.Parse(seconds);
            return _parsedValues; 
        }

        private bool CheckDegreesMinutesSeconds(int[] values, int minDegree, int maxDegree)
        {
            return values[0] >= minDegree && values[0] <= maxDegree 
                && values[1] >= Constants.MINUTE_MIN && values[1] <= Constants.MINUTE_MAX
                && values[2] >= Constants.SECOND_MIN && values[2] <= Constants.SECOND_MAX;
        }

    }


}
