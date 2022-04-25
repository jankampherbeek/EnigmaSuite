using domain.shared;
using System;
using System.Collections.Generic;


namespace E4C.Models.Creators
{

    /// <summary>
    /// Validations for the inputs for geographic longitude and geographic latitude.
    /// </summary>
    public interface ILocationValidations
    {
        /// <summary>
        /// Validate the input for geographic longitude. Checks the values for degrees, minutes and seconds.
        /// </summary>
        /// <param name="geoLong">String array with degrees, minutes and seconds for geographic longitude, in that sequence.</param>
        /// <returns>List with error codes.</returns>
        public List<int> ValidateGeoLongitude(string[] geoLong);

        /// <summary>
        /// Validate the input for geographic longitude. Checks the values for degrees, minutes and seconds.
        /// </summary>
        /// <param name="geoLong">String array with degrees, minutes and seconds for geographic latitude, in that sequence.</param>
        /// <returns>List with error codes.</returns>
        public List<int> ValidateGeoLatitude(string[] geoLong);
    }


    public class LocationValidations : ILocationValidations
    {

        public List<int> ValidateGeoLongitude(string[] geoLong)
        {
            List<int> _errorCodes = new();
            try
            {
                int[] _parsedValues = ParseDegreesMinutesSeconds(geoLong);
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
            catch (Exception)
            {
                _errorCodes.Add(ErrorCodes.ERR_INVALID_GEOLON);
            }
            return _errorCodes;
        }

        public List<int> ValidateGeoLatitude(string[] geoLong)
        {
            List<int> _errorCodes = new();
            try
            {
                int[] _parsedValues = ParseDegreesMinutesSeconds(geoLong);
                if (!CheckDegreesMinutesSeconds(_parsedValues, Constants.GEOLAT_DEGREE_MIN, Constants.GEOLAT_DEGREE_MAX))
                {
                    _errorCodes.Add(ErrorCodes.ERR_INVALID_GEOLAT);
                }
            }
            catch (Exception)
            {
                _errorCodes.Add(ErrorCodes.ERR_INVALID_GEOLAT);
            }
            return _errorCodes;
        }

        private static int[] ParseDegreesMinutesSeconds(string[] geoLong)
        {
            int[] _parsedValues = new int[3];
            _parsedValues[0] = int.Parse(geoLong[0]);
            _parsedValues[1] = int.Parse(geoLong[1]);
            _parsedValues[2] = int.Parse(geoLong[2]);
            return _parsedValues;
        }

        private static bool CheckDegreesMinutesSeconds(int[] values, int minDegree, int maxDegree)
        {
            return values[0] >= minDegree && values[0] <= maxDegree
                && values[1] >= Constants.MINUTE_MIN && values[1] <= Constants.MINUTE_MAX
                && values[2] >= Constants.SECOND_MIN && values[2] <= Constants.SECOND_MAX;
        }

    }


}
