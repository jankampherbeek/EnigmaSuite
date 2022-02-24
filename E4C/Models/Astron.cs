// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.Models.Domain;
using E4C.Models.SeFacade;
using System;
using System.Collections.Generic;

namespace E4C.Models.Astron
{
    /// <summary>
    /// Calculations for date and time functionality.
    /// </summary>
    public interface ICalendarCalc
    {
        /// <summary>
        /// Calculate Julian Day number.
        /// </summary>
        /// <param name="dateTime">Date, time and calendar.</param>
        /// <returns>The calculated and validated Julian Day number.</returns>
        public ResultForDouble CalculateJd(SimpleDateTime dateTime);

        /// <summary>
        /// Calculate date and time (ut) from a given Julian Day Number.
        /// </summary>
        /// <param name="julianDayNumber">Vale of the Julian Day Number.</param>
        /// <param name="calendar">Gregorian or Julian calendar.</param>
        /// <returns>Calculated date and timne.</returns>
        public SimpleDateTime CalculateDateTimeFromJd(double julianDayNumber, Calendars calendar);

        /// <summary>
        /// Checks date and time for correctness. Date should fit in the calendar used, taking leapyears into account. Time should be >= 0.0 and < 24.0.
        /// </summary>
        /// <param name="dateTime">Instance of SimpleDateTime to check</param>
        /// <returns>true if date and time are both correct, otherwise false.</returns>
        public bool ValidDateAndtime(SimpleDateTime dateTime);

    }

    /// <summary>
    /// Calculations for obliquity and/or nutation.
    /// </summary>
    public interface IObliquityNutationCalc
    {
        /// <summary>
        /// Calculate obliquity.
        /// </summary>
        /// <param name="JulianDayUt">Julian Day for UT.</param>
        /// <param name="useTrueObliquity">True for true obliquity, false for mean obliquity.</param>
        /// <returns>The calculated and validated obliquity.</returns>
        public ResultForDouble CalculateObliquity(double julianDayUt, bool useTrueObliquity);
    }

    /// <summary>
    /// Definitons for flags.
    /// </summary>
    public interface IFlagDefinitions
    {
        /// <summary>
        /// Define flags for a given FullChartRequest.
        /// </summary>
        /// <param name="request">Request for which the flags need to be defined.</param>
        /// <returns>Combined vlaue for flags.</returns>
        public int DefineFlags(FullChartRequest request);

        /// <summary>
        /// Changes existing flags to suppport equatorial calculations. 
        /// </summary>
        /// <param name="eclipticFlags"></param>
        /// <returns>Value for equatorial flags.</returns>
        public int AddEquatorial(int eclipticFlags);
    }


    /// <summary>
    /// Calculations for Solar System points.
    /// </summary>
    public interface IPositionSolSysPointCalc
    {
        /// <summary>
        /// Calculate a single Solar System point.
        /// </summary>
        /// <param name="solarSystemPoint">The Solar System point that will be calcualted.</param>
        /// <param name="jdnr">The Julian day number.</param>
        /// <param name="location">Location with coordinates.</param>
        /// <param name="flagsEcliptical">Flags that contain the settings for ecliptic based calculations.</param>
        /// <param name="flagsEquatorial">Flags that contain the settings for equatorial based calculations.</param>
        /// <returns>Instance of CalculatedFullSolSysPointPosition.</returns>
        public FullSolSysPointPos CalculateSolSysPoint(SolarSystemPoints solarSystemPoint, double jdnr, Location location, int flagsEcliptical, int flagsEquatorial);
    }

    /// <summary>
    /// Calculations for mundane positions (houses etc.).
    /// </summary>
    public interface IPositionsMundane
    {
        /// <summary>
        /// Calculate house cusps, MC, Ascendant, Vertex and Eastpoint.
        /// </summary>
        /// <param name="julianDayUt">Julian Day for UT.</param>
        /// <param name="obliquity"">Obliquity of the earths axis.</param>
        /// <param name="flags">Flags swith the required settings.</param>
        /// <param name="location">Location with coordinates.</param>
        /// <param name="houseSystem">The Housesystem to use, from the enum HouseSystems.</param>
        /// <returns>Instance of MundanePositions with the calculated values.</returns>
        public MundanePositions CalculateAllMundanePositions(double julianDayUt, double obliquity, int flags, Location location, HouseSystems houseSystem);
    }

    /// <summary>
    /// Calculator for fully defined charts.
    /// </summary>
    /// <remarks>No unit test as this would require too much mocked data. This class should be tested by an integration test.</remarks>
    public interface IFullChartCalc
    {
        /// <summary>
        /// Calculate a fully defined chart.
        /// </summary>
        /// <param name="request">Instance of FullChartRequest.</param>
        /// <returns>A FUllChartResponse with all the calculated data.</returns>
        public FullChartResponse CalculateFullChart(FullChartRequest request);
    }


    public class CalendarCalc : ICalendarCalc
    {

        private readonly ISeDateTimeFacade _dateTimeFacade;


        public CalendarCalc(ISeDateTimeFacade dateTimeFacade)
        {
            _dateTimeFacade = dateTimeFacade;
        }


        public ResultForDouble CalculateJd(SimpleDateTime dateTime)
        {
            ResultForDouble _result;
            try
            {
                double _jdNr = _dateTimeFacade.JdFromSe(dateTime);
                _result = new ResultForDouble(_jdNr, true);
            }
            catch (System.Exception e)
            {
                _result = new ResultForDouble(0.0, false, "Exception: " + e.Message);
                // TODO log exception
            }
            return _result;
        }

        public SimpleDateTime CalculateDateTimeFromJd(double julianDayNumber, Calendars calendar)
        {
            SimpleDateTime _result;
            try
            {
                _result = _dateTimeFacade.DateTimeFromJd(julianDayNumber, calendar);
            }
            catch (Exception e)
            {
                // todo handle exception, write to log-file
                Console.WriteLine("Error to log in CalendarCalc.CalculateDateTimeFromJd: " + e.Message);
                _result = new SimpleDateTime(0, 0, 0, 0.0, calendar);
            }
            return _result;
        }

        public bool ValidDateAndtime(SimpleDateTime dateTime)
        {
            return _dateTimeFacade.DateTimeIsValid(dateTime);
        }
    }

    public class ObliquityNutationCalc : IObliquityNutationCalc
    {
        const int SE_ECL_NUT = -1;   // TODO move to separate class that contains constants for the SE 

        private readonly ISePosCelPointFacade _posCelPointFacade;

        public ObliquityNutationCalc(ISePosCelPointFacade celPointFacade)
        {
            _posCelPointFacade = celPointFacade;
        }

        public ResultForDouble CalculateObliquity(double julianDayUt, bool useTrueObliquity)
        {
            ResultForDouble _result;
            try
            {
                int _celPointId = SE_ECL_NUT;
                int _flags = 0;   // todo define flags
                double[] _positions = _posCelPointFacade.PosCelPointFromSe(julianDayUt, _celPointId, _flags);
                double _resultingPosition = useTrueObliquity ? _positions[1] : _positions[0];
                _result = new ResultForDouble(_resultingPosition, true);
            }
            catch (System.Exception e)   // todo replace with specific exception for SE
            {
                _result = new ResultForDouble(0.0, false, "Exception: " + e.Message);
                // todo handle exception, write to log-file
                Console.WriteLine("Error to log in CalendarCalc.CalculateObliquity: " + e.Message);
            }

            return _result;
        }
    }

    public class FlagDefinitions : IFlagDefinitions
    {

        public int DefineFlags(FullChartRequest request)
        {
            int flags = Constants.SEFLG_SWIEPH | Constants.SEFLG_SPEED;
            if (request.ObserverPosition == ObserverPositions.HelioCentric)
            {
                flags = flags | Constants.SEFLG_HELCTR;
            }
            if (request.ObserverPosition == ObserverPositions.TopoCentric)
            {
                flags = flags | Constants.SEFLG_TOPOCTR;
            }
            if (request.ZodiacType == ZodiacTypes.Sidereal)
            {
                flags = flags | Constants.SEFLG_SIDEREAL;
            }
            return flags;
        }

        public int AddEquatorial(int eclipticFlags)
        {
            return eclipticFlags | Constants.SEFLG_EQUATORIAL;
        }



    }


    public class FullChartCalc : IFullChartCalc
    {
        private IObliquityNutationCalc _obliquityNutationCalc;
        private IPositionsMundane _positionsMundane;
        private IPositionSolSysPointCalc _positionSolSysPointCalc;
        private IFlagDefinitions _flagDefinitions;

        /// <summary>
        /// Constructor for FullChartCalc.
        /// </summary>
        /// <param name="obliquityNutationCalc">Calclator for boliquity and nutation.</param>
        /// <param name="positionsMundane">Calculator for mundane positions.</param>
        /// <param name="positionSolSysPointCalc">Calculator for solar system points.</param>
        public FullChartCalc(IObliquityNutationCalc obliquityNutationCalc, IPositionsMundane positionsMundane, IPositionSolSysPointCalc positionSolSysPointCalc, IFlagDefinitions flagDefinitions)
        {
            _obliquityNutationCalc = obliquityNutationCalc;
            _positionsMundane = positionsMundane;
            _positionSolSysPointCalc = positionSolSysPointCalc;
            _flagDefinitions = flagDefinitions;
        }


        public FullChartResponse CalculateFullChart(FullChartRequest request)
        {
            int _flagsEcliptical = _flagDefinitions.DefineFlags(request);
            int _flagsEquatorial = _flagDefinitions.AddEquatorial(_flagsEcliptical);
            double _obliquity = CalculateObliquity(request.JulianDayUt);
            MundanePositions _mundanePositions = _positionsMundane.CalculateAllMundanePositions(request.JulianDayUt, _obliquity, _flagsEcliptical, request.ChartLocation, request.HouseSystem);

            var _fullSolSysPoints = new List<FullSolSysPointPos>();
            foreach (SolarSystemPoints solSysPoint in request.SolarSystemPoints)
            {
                _fullSolSysPoints.Add(_positionSolSysPointCalc.CalculateSolSysPoint(solSysPoint, request.JulianDayUt, request.ChartLocation, _flagsEcliptical, _flagsEquatorial));
            }
            return new FullChartResponse(_fullSolSysPoints, _mundanePositions);
        }

        private double CalculateObliquity(double _jdUt)
        {
            bool _trueObliquity = false;
            ResultForDouble _obliquityResult = _obliquityNutationCalc.CalculateObliquity(_jdUt, _trueObliquity);
            if (!_obliquityResult.NoErrors)
            {
                // todo use specific exception
                throw new Exception("Error when calculating obliquity in FullChartCalc. Received message: " + _obliquityResult.ErrorText);
            }
            return _obliquityResult.ReturnValue;
        }
    }

    public class PositionSolSysPointCalc: IPositionSolSysPointCalc
    {
        private readonly ISePosCelPointFacade _posCelPointFacade;
        private readonly IHorizontalCoordinatesFacade _horizontalCoordinatesFacade;
        private readonly ISolarSystemPointSpecifications _solarSystemPointSpecifications;

        public PositionSolSysPointCalc(ISePosCelPointFacade posCelPointFacade, IHorizontalCoordinatesFacade horizontalCoordinatesFacade, 
            ISolarSystemPointSpecifications solarSystemPointSpecifications)
        {
            _posCelPointFacade = posCelPointFacade;
            _horizontalCoordinatesFacade = horizontalCoordinatesFacade;
            _solarSystemPointSpecifications = solarSystemPointSpecifications;
        }


        public FullSolSysPointPos CalculateSolSysPoint(SolarSystemPoints solarSystemPoint, double jdnr, Location location, int flagsEcliptical, int flagsEquatorial)
        {

            // todo handle actions for sidereal and/or topocentric
            // todo define flags
            double heightAboveSeaLevel = 0.0;
            int pointId = _solarSystemPointSpecifications.DetailsForPoint(solarSystemPoint).SeId;
            double[] _fullEclipticPositions = _posCelPointFacade.PosCelPointFromSe(jdnr, pointId, flagsEcliptical);
            double[] _fullEquatorialPositions = _posCelPointFacade.PosCelPointFromSe(jdnr, pointId, flagsEquatorial);
            var _eclCoordinatesForHorCalculation = new double[] { _fullEclipticPositions[0], _fullEclipticPositions[1], _fullEclipticPositions[2] };
            var _geoGraphicCoordinates = new double[] { location.GeoLong, location.GeoLat, heightAboveSeaLevel };
            HorizontalPos _horizontalPos = _horizontalCoordinatesFacade.CalculateHorizontalCoordinates(   jdnr, _geoGraphicCoordinates, _eclCoordinatesForHorCalculation, flagsEcliptical);
            var _longitude = new PosSpeed(_fullEclipticPositions[0], _fullEclipticPositions[3]);
            var _latitude = new PosSpeed(_fullEclipticPositions[1], _fullEclipticPositions[4]);
            var _distance = new PosSpeed(_fullEclipticPositions[2], _fullEclipticPositions[5]);
            var _rightAscension = new PosSpeed(_fullEquatorialPositions[0], _fullEquatorialPositions[3]);
            var _declination = new PosSpeed(_fullEquatorialPositions[1], _fullEquatorialPositions[4]);
            return new FullSolSysPointPos(solarSystemPoint, _longitude, _latitude, _rightAscension, _declination, _distance, _horizontalPos);
        }
    }

    public class PositionsMundane : IPositionsMundane
    {
        private ISePosHousesFacade _sePosHousesFacade;
        private ICoordinateConversionFacade _coordinateConversionFacade;
        private IHorizontalCoordinatesFacade _horizontalCoordinatesFacade;
        private IHouseSystemSpecifications _houseSystemSpecifications;

        public PositionsMundane(ISePosHousesFacade sePosHousesFacade, ICoordinateConversionFacade coordinateConversionFacade, 
            IHorizontalCoordinatesFacade horizontalCoordinatesFacade, IHouseSystemSpecifications houseSystemSpecifications)
        {
            _sePosHousesFacade = sePosHousesFacade;
            _coordinateConversionFacade = coordinateConversionFacade;
            _horizontalCoordinatesFacade = horizontalCoordinatesFacade;
            _houseSystemSpecifications = houseSystemSpecifications;
        }

        public MundanePositions CalculateAllMundanePositions(double julianDayUt, double obliquity, int flags, Location location, HouseSystems houseSystem)
        {
            char _houseSystemId = _houseSystemSpecifications.DetailsForHouseSystem(houseSystem).SeId;
            int _nrOfCusps = _houseSystemSpecifications.DetailsForHouseSystem(houseSystem).NrOfCusps;
            double[][] _longitudeValues = _sePosHousesFacade.PosHousesFromSe(julianDayUt, flags, location.GeoLat, location.GeoLong, _houseSystemId);
            var _cusps = new List<CuspFullPos>(); 
            for (int i = 0; i < _nrOfCusps; i++)
            {
                double _longitude = _longitudeValues[0][i + 1];
                _cusps.Add(CreateFullMundanePos(julianDayUt, obliquity, _longitude, flags, location));
            }
            CuspFullPos _mc = CreateFullMundanePos(julianDayUt, obliquity, _longitudeValues[1][0], flags, location);
            CuspFullPos _asc = CreateFullMundanePos(julianDayUt, obliquity, _longitudeValues[1][1], flags, location);
            CuspFullPos _vertex = CreateFullMundanePos(julianDayUt, obliquity, _longitudeValues[1][3], flags, location);
            CuspFullPos _eastPoint = CreateFullMundanePos(julianDayUt, obliquity, _longitudeValues[1][4], flags, location);
            return new MundanePositions(_cusps, _mc, _asc, _vertex, _eastPoint);
        }

        private CuspFullPos CreateFullMundanePos(double jdnr, double obliquity, double eclLongitude, int flags, Location location )
        {
            double _latitude = 0.0;    // always zero for mundane positions.
            double _distance = 1.0;    // placeholder.
            var _geographicCoordinates = new double[] { location.GeoLong, location.GeoLat, _distance };
            var _eclipticCoordinates = new double[] { eclLongitude, _latitude, _distance };
            double[] _equatorialCoordinates = _coordinateConversionFacade.EclipticToEquatorial(new double[] { eclLongitude, 0.0 }, obliquity);
            HorizontalPos _horizontalPos = _horizontalCoordinatesFacade.CalculateHorizontalCoordinates(jdnr, _geographicCoordinates, _eclipticCoordinates, flags);
            return new CuspFullPos(eclLongitude, _equatorialCoordinates[0], _equatorialCoordinates[1], _horizontalPos);
        }

    }

}