// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.


using E4C.Core.Astron.Horizontal;
using E4C.Core.Astron.Obliquity;
using E4C.Core.Facades;
using E4C.Core.Shared.Domain;
using E4C.Shared.Domain;
using E4C.Shared.References;
using E4C.Shared.ReqResp;
using System.Collections.Generic;

namespace E4C.Core.Astron.SolSysPoints;

/// <summary>
/// Handler for the calculation of one or more solar system points.
/// </summary>
public interface ISolSysPointsHandler
{

    public SolSysPointsResponse CalcSolSysPoints(SolSysPointsRequest request);
}


/// <inheritdoc/>
public class SolSysPointsHandler : ISolSysPointsHandler
{
    private readonly IAyanamshaSpecifications _ayanamshaSpecifications;
    private readonly IFlagDefinitions _flagDefinitions;
    private readonly ISolSysPointSECalc _solSysPointSECalc;
    private readonly ISolSysPointsElementsCalc _solSysPointElementsCalc;
    private readonly ISolarSystemPointSpecifications _solSysPointSpecs;
    private readonly ICoTransFacade _coordinateConversionFacade;
    private readonly IObliquityHandler _obliquityHandler;
    private readonly IHorizontalHandler _horizontalHandler;


    public SolSysPointsHandler(IAyanamshaSpecifications ayanamshaSpecifications,
                               IFlagDefinitions flagDefinitions,
                               ISolSysPointSECalc positionSolSysPointSECalc,
                               ISolSysPointsElementsCalc posSolSysPointsElementsCalc,
                               ISolarSystemPointSpecifications solSysPointSpecs,
                               ICoTransFacade coordinateConversionFacade,
                               IObliquityHandler obliquityHandler,
                               IHorizontalHandler horizontalHandler)
    {
        _ayanamshaSpecifications = ayanamshaSpecifications;
        _flagDefinitions = flagDefinitions;
        _solSysPointSECalc = positionSolSysPointSECalc;
        _solSysPointSpecs = solSysPointSpecs;
        _solSysPointElementsCalc = posSolSysPointsElementsCalc;
        _coordinateConversionFacade = coordinateConversionFacade;
        _obliquityHandler = obliquityHandler;
        _horizontalHandler = horizontalHandler;

    }


    public SolSysPointsResponse CalcSolSysPoints(SolSysPointsRequest request)
    {
        double julDay = request.JulianDayUt;
        double previousJd = julDay - 0.5;
        double futureJd = julDay + 0.5;
        Location location = request.ChartLocation;
        var obliquityRequest = new ObliquityRequest(julDay, true);
        ObliquityResponse obliquityResponse = _obliquityHandler.CalcObliquity(obliquityRequest);
        double obliquity = obliquityResponse.Obliquity;
        bool success = obliquityResponse.Success;
        string errorText = obliquityResponse.ErrorText;
        if (request.ZodiacType == ZodiacTypes.Sidereal)
        {
            int idAyanamsa = _ayanamshaSpecifications.DetailsForAyanamsha(request.Ayanamsha).SeId;
            SeInitializer.SetAyanamsha(idAyanamsa);
        }
        int _flagsEcliptical = _flagDefinitions.DefineFlags(CoordinateSystems.Ecliptical, request.ObserverPosition, request.ZodiacType);
        int _flagsEquatorial = _flagDefinitions.DefineFlags(CoordinateSystems.Equatorial, request.ObserverPosition, request.ZodiacType);
        var _fullSolSysPoints = new List<FullSolSysPointPos>();
        foreach (SolarSystemPoints solSysPoint in request.SolarSystemPoints)
        {
            SolarSystemPointDetails details = _solSysPointSpecs.DetailsForPoint(solSysPoint);
            if (details.CalculationType == CalculationTypes.SE)
            {
                _fullSolSysPoints.Add(createPosForSePoint(solSysPoint, julDay, location, _flagsEcliptical, _flagsEquatorial));
            }
            else if (details.CalculationType == CalculationTypes.Elements)
            {
                double[][] positions = createPosForElementBasedPoint(solSysPoint, julDay, location, obliquity);
                double[][] previousPositions = createPosForElementBasedPoint(solSysPoint, previousJd, location, obliquity);
                double[][] futurePositions = createPosForElementBasedPoint(solSysPoint, futureJd, location, obliquity);
                PosSpeed longPosSpeed = new(positions[0][0], futurePositions[0][0] - previousPositions[0][0]);
                PosSpeed latPosSpeed = new(positions[0][1], futurePositions[0][1] - previousPositions[0][1]);
                PosSpeed distPosSpeed = new(positions[0][2], futurePositions[0][2] - previousPositions[0][2]);
                PosSpeed raPosSpeed = new(positions[1][0], futurePositions[1][0] - previousPositions[1][0]);
                PosSpeed declPosSpeed = new(positions[1][1], futurePositions[1][1] - previousPositions[1][1]);
                EclipticCoordinates eclCoordinates = new(positions[0][0], positions[0][1]);
                HorizontalRequest horizontalRequest = new(julDay, request.ChartLocation, eclCoordinates);
                HorizontalCoordinates horCoord = _horizontalHandler.CalcHorizontal(horizontalRequest).HorizontalAzimuthAltitude;
                _fullSolSysPoints.Add(new FullSolSysPointPos(solSysPoint, longPosSpeed, latPosSpeed, raPosSpeed, declPosSpeed, distPosSpeed, horCoord));
            }
        }
        return new SolSysPointsResponse(_fullSolSysPoints, success, errorText);

    }

    private FullSolSysPointPos createPosForSePoint(SolarSystemPoints solSysPoint, double julDay, Location location, int flagsEcl, int flagsEq)
    {
        PosSpeed[] eclipticPosSpeed = _solSysPointSECalc.CalculateSolSysPoint(solSysPoint, julDay, location, flagsEcl);
        PosSpeed[] equatorialPosSpeed = _solSysPointSECalc.CalculateSolSysPoint(solSysPoint, julDay, location, flagsEq);
        var eclCoordinates = new EclipticCoordinates(eclipticPosSpeed[0].Position, eclipticPosSpeed[1].Position);
        HorizontalRequest horizontalRequest = new(julDay, location, eclCoordinates);
        HorizontalCoordinates horCoord = _horizontalHandler.CalcHorizontal(horizontalRequest).HorizontalAzimuthAltitude;
        return new FullSolSysPointPos(solSysPoint, eclipticPosSpeed[0], eclipticPosSpeed[1], equatorialPosSpeed[0], equatorialPosSpeed[1], eclipticPosSpeed[2], horCoord);
    }

    private double[][] createPosForElementBasedPoint(SolarSystemPoints solSysPoint, double julDay, Location location, double obliquity)
    {
        double[] eclipticPos = _solSysPointElementsCalc.Calculate(solSysPoint, julDay);
        double[] equatorialPos = _coordinateConversionFacade.EclipticToEquatorial(new double[] { eclipticPos[0], eclipticPos[1] }, obliquity);
        return new double[][] { eclipticPos, equatorialPos };

    }
}


