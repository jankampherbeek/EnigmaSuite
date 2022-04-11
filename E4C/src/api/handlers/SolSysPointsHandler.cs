// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using api.handlers;
using E4C.calc.elements;
using E4C.calc.seph;
using E4C.calc.seph.secalculations;
using E4C.calc.seph.sefacade;
using E4C.domain.shared.positions;
using E4C.domain.shared.references;
using E4C.domain.shared.reqresp;
using E4C.exceptions;
using E4C.Models.Domain;
using System.Collections.Generic;

namespace E4C.api.handlers;

/// <summary>
/// Handler for cthe calculation of one or more solar system points.
/// </summary>
public interface ISolSysPointsHandler
{
    /// <summary>
    /// Perform the calculation.
    /// </summary>
    public SolSysPointsResponse CalcSolSysPoints(SolSysPointsRequest request);
}


/// <inheritdoc/>
public class SolSysPointsHandler : ISolSysPointsHandler
{

    private readonly IAyanamshaSpecifications _ayanamshaSpecifications;
    private readonly IFlagDefinitions _flagDefinitions;
    private readonly IPositionSolSysPointSECalc _posSolSysPointSECalc;
    private readonly IPosSolSysPointsElementsCalc _posSolSysPointElementsCalc;
    private readonly ISolarSystemPointSpecifications _solSysPointSpecs;
    private readonly ICoordinateConversionFacade _coordinateConversionFacade;
    private readonly IObliquityHandler _obliquityHandler;


    public SolSysPointsHandler(IAyanamshaSpecifications ayanamshaSpecifications, 
                               IFlagDefinitions flagDefinitions, 
                               IPositionSolSysPointSECalc positionSolSysPointSECalc,
                               IPosSolSysPointsElementsCalc posSolSysPointsElementsCalc,
                               ISolarSystemPointSpecifications solSysPointSpecs,
                               ICoordinateConversionFacade coordinateConversionFacade,
                               IObliquityHandler obliquityHandler)
    {
        _ayanamshaSpecifications = ayanamshaSpecifications;
        _flagDefinitions = flagDefinitions;
        _posSolSysPointSECalc = positionSolSysPointSECalc;
        _solSysPointSpecs = solSysPointSpecs;
        _posSolSysPointElementsCalc = posSolSysPointsElementsCalc;
        _coordinateConversionFacade = coordinateConversionFacade;
        _obliquityHandler = obliquityHandler;

    }


    public SolSysPointsResponse CalcSolSysPoints(SolSysPointsRequest request)
    {
        var obliquityRequest = new ObliquityRequest(request.JulianDayUt, true);
        ObliquityResponse obliquityResponse = _obliquityHandler.CalcObliquity(obliquityRequest);
        double obliquity = obliquityResponse.Obliquity;
        bool success = obliquityResponse.Success;
        string errorText = obliquityResponse.ErrorText;
        if (request.ZodiacType == ZodiacTypes.Sidereal)
        {
            int idAyanamsa = _ayanamshaSpecifications.DetailsForAyanamsha(request.Ayanamsha).SeId;
            SeInitializer.SetAyanamsha(idAyanamsa);
        }
        int _flagsEcliptical = _flagDefinitions.DefineFlags(request);
        int _flagsEquatorial = _flagDefinitions.AddEquatorial(_flagsEcliptical);
        var _fullSolSysPoints = new List<FullSolSysPointPos>();
        foreach (SolarSystemPoints solSysPoint in request.SolarSystemPoints)
        {
            SolarSystemPointDetails details = _solSysPointSpecs.DetailsForPoint(solSysPoint);
            if (details.CalculationType == CalculationTypes.SE)
            {
                _fullSolSysPoints.Add(_posSolSysPointSECalc.CalculateSolSysPoint(solSysPoint, request.JulianDayUt, request.ChartLocation, _flagsEcliptical, _flagsEquatorial));
                // TODO split calculations, separate for equatorial and horizontal
            }
            else if (details.CalculationType == CalculationTypes.Elements)
            {
                double[] eclipticPos = _posSolSysPointElementsCalc.Calculate(solSysPoint, request.JulianDayUt);
                double[] equatorialPos = _coordinateConversionFacade.EclipticToEquatorial(new double[] { eclipticPos[0], eclipticPos[1] }, obliquity);

                // temporary solution for speed and horizontal coordinates
                // TODO add calculation of equatorial coordinates
                // TODO add calculation of horizontal coordinates
                // TODO define speed
                PosSpeed longitude = new(eclipticPos[0], 0.0);
                PosSpeed latitude = new(eclipticPos[1], 0.0);
                PosSpeed distance = new(eclipticPos[2], 0.0);
                PosSpeed rightAscension = new(equatorialPos[0], 0.0);
                PosSpeed declination = new(equatorialPos[1], 0.0);
                HorizontalPos azimuthAltitude = new(0.0, 0.0);
                _fullSolSysPoints.Add(new FullSolSysPointPos(solSysPoint, longitude, latitude, rightAscension, declination, distance, azimuthAltitude));
            }
        }
        return new SolSysPointsResponse(_fullSolSysPoints, success, errorText);

    }
}


