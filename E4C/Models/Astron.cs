// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.calc.seph.secalculations;
using E4C.calc.seph.sefacade;
using E4C.Models.Domain;
using E4C.domain.shared.positions;
using E4C.domain.shared.specifications;
using System;
using System.Collections.Generic;
using E4C.calc.seph;
using E4C.calc.util;
using E4C.domain.shared.references;
using E4C.domain.shared.reqresp;

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
        /// Checks date and time for correctness. Date should fit in the calendar used, taking leapyears into account. Time should be between 0.0 (inclusive) and 24.0 (exclusive).
        /// </summary>
        /// <param name="dateTime">Instance of SimpleDateTime to check</param>
        /// <returns>true if date and time are both correct, otherwise false.</returns>
        public bool ValidDateAndtime(SimpleDateTime dateTime);

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




    public class FullChartCalc : IFullChartCalc
    {
        private readonly IObliquityNutationCalc _obliquityNutationCalc;
        private readonly IMundanePositionsCalculator _positionsMundane;
        private readonly IPositionSolSysPointSECalc _positionSolSysPointCalc;
        private readonly IFlagDefinitions _flagDefinitions;
        private readonly IAyanamshaSpecifications _ayanamshaSpecifications;

        /// <summary>
        /// Constructor for FullChartCalc.
        /// </summary>
        /// <param name="obliquityNutationCalc">Calclator for boliquity and nutation.</param>
        /// <param name="positionsMundane">Calculator for mundane positions.</param>
        /// <param name="positionSolSysPointCalc">Calculator for solar system points.</param>
        public FullChartCalc(IObliquityNutationCalc obliquityNutationCalc, IMundanePositionsCalculator positionsMundane, IPositionSolSysPointSECalc positionSolSysPointCalc,
            IFlagDefinitions flagDefinitions, IAyanamshaSpecifications ayanamshaSpecifications)
        {
            _obliquityNutationCalc = obliquityNutationCalc;
            _positionsMundane = positionsMundane;
            _positionSolSysPointCalc = positionSolSysPointCalc;
            _flagDefinitions = flagDefinitions;
            _ayanamshaSpecifications = ayanamshaSpecifications;
        }


        public FullChartResponse CalculateFullChart(FullChartRequest request)
        {
            Boolean success = true;
            string errorText = "";
            if (request.SolSysPointRequest.ZodiacType == ZodiacTypes.Sidereal)
            {
                int idAyanamsa = _ayanamshaSpecifications.DetailsForAyanamsha(request.SolSysPointRequest.Ayanamsha).SeId;
                SeInitializer.SetAyanamsha(idAyanamsa);
            }
            int _flagsEcliptical = _flagDefinitions.DefineFlags(request);
            int _flagsEquatorial = _flagDefinitions.AddEquatorial(_flagsEcliptical);
            double _obliquity = CalculateObliquity(request.SolSysPointRequest.JulianDayUt);
            FullMundanePositions _mundanePositions = _positionsMundane.CalculateAllMundanePositions(request.SolSysPointRequest.JulianDayUt, _obliquity, _flagsEcliptical, request.SolSysPointRequest.ChartLocation, request.HouseSystem);

            var _fullSolSysPoints = new List<FullSolSysPointPos>();
            foreach (SolarSystemPoints solSysPoint in request.SolSysPointRequest.SolarSystemPoints)
            {
                _fullSolSysPoints.Add(_positionSolSysPointCalc.CalculateSolSysPoint(solSysPoint, request.SolSysPointRequest.JulianDayUt, request.SolSysPointRequest.ChartLocation, _flagsEcliptical, _flagsEquatorial));
            }
            // TODO add error checking
            return new FullChartResponse(_fullSolSysPoints, _mundanePositions, success, errorText);
        }

        private double CalculateObliquity(double _jdUt)
        {
            bool _trueObliquity = false;
            return _obliquityNutationCalc.CalculateObliquity(_jdUt, _trueObliquity);
        }
    }




}