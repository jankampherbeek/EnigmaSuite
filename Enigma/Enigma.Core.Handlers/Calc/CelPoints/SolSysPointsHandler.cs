// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Core.Work.Calc.Interfaces;
using Enigma.Domain.AstronCalculations;
using Enigma.Domain.Enums;
using Enigma.Domain.Interfaces;
using Enigma.Domain.RequestResponse;
using Enigma.Facades.Interfaces;
using Enigma.Facades.Se;

namespace Enigma.Core.Work.Handlers.Calc.CelPoints;




/// <inheritdoc/>
public class SolSysPointsHandler : ISolSysPointsHandler
{
    private readonly IAyanamshaSpecifications _ayanamshaSpecifications;
    private readonly ISeFlags _seFlags;
    private readonly ISolSysPointSECalc _solSysPointSECalc;
    private readonly ISolSysPointsElementsCalc _solSysPointElementsCalc;
    private readonly ISolarSystemPointSpecifications _solSysPointSpecs;
    private readonly ICoTransFacade _coordinateConversionFacade;
    private readonly IObliquityHandler _obliquityHandler;
    private readonly IHorizontalHandler _horizontalHandler;


    public SolSysPointsHandler(IAyanamshaSpecifications ayanamshaSpecifications,
                               ISeFlags seFlags,
                               ISolSysPointSECalc positionSolSysPointSECalc,
                               ISolSysPointsElementsCalc posSolSysPointsElementsCalc,
                               ISolarSystemPointSpecifications solSysPointSpecs,
                               ICoTransFacade coordinateConversionFacade,
                               IObliquityHandler obliquityHandler,
                               IHorizontalHandler horizontalHandler)
    {
        _ayanamshaSpecifications = ayanamshaSpecifications;
        _seFlags = seFlags;
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
        var obliquityRequest = new ObliquityRequest(julDay);
        ObliquityResponse obliquityResponse = _obliquityHandler.CalcObliquity(obliquityRequest);
        double obliquity = obliquityResponse.ObliquityTrue;
        bool success = obliquityResponse.Success;
        string errorText = obliquityResponse.ErrorText;
        if (request.ActualCalculationPreferences.ActualZodiacType == ZodiacTypes.Sidereal)
        {
            int idAyanamsa = _ayanamshaSpecifications.DetailsForAyanamsha(request.ActualCalculationPreferences.ActualAyanamsha).SeId;
            SeInitializer.SetAyanamsha(idAyanamsa);
        }
        int _flagsEcliptical = _seFlags.DefineFlags(CoordinateSystems.Ecliptical, request.ActualCalculationPreferences.ActualObserverPosition, request.ActualCalculationPreferences.ActualZodiacType);
        int _flagsEquatorial = _seFlags.DefineFlags(CoordinateSystems.Equatorial, request.ActualCalculationPreferences.ActualObserverPosition, request.ActualCalculationPreferences.ActualZodiacType);
        var _fullSolSysPoints = new List<FullSolSysPointPos>();
        foreach (SolarSystemPoints solSysPoint in request.ActualCalculationPreferences.ActualSolarSystemPoints)
        {
            SolarSystemPointDetails details = _solSysPointSpecs.DetailsForPoint(solSysPoint);
            if (details.CalculationType == CalculationTypes.SE)
            {
                _fullSolSysPoints.Add(CreatePosForSePoint(solSysPoint, julDay, location, _flagsEcliptical, _flagsEquatorial));
            }
            else if (details.CalculationType == CalculationTypes.Elements)
            {
                double[][] positions = CreatePosForElementBasedPoint(solSysPoint, julDay, location, obliquity);
                double[][] previousPositions = CreatePosForElementBasedPoint(solSysPoint, previousJd, location, obliquity);
                double[][] futurePositions = CreatePosForElementBasedPoint(solSysPoint, futureJd, location, obliquity);
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

    private FullSolSysPointPos CreatePosForSePoint(SolarSystemPoints solSysPoint, double julDay, Location location, int flagsEcl, int flagsEq)
    {
        PosSpeed[] eclipticPosSpeed = _solSysPointSECalc.CalculateSolSysPoint(solSysPoint, julDay, location, flagsEcl);
        PosSpeed[] equatorialPosSpeed = _solSysPointSECalc.CalculateSolSysPoint(solSysPoint, julDay, location, flagsEq);
        var eclCoordinates = new EclipticCoordinates(eclipticPosSpeed[0].Position, eclipticPosSpeed[1].Position);
        HorizontalRequest horizontalRequest = new(julDay, location, eclCoordinates);
        HorizontalCoordinates horCoord = _horizontalHandler.CalcHorizontal(horizontalRequest).HorizontalAzimuthAltitude;
        return new FullSolSysPointPos(solSysPoint, eclipticPosSpeed[0], eclipticPosSpeed[1], equatorialPosSpeed[0], equatorialPosSpeed[1], eclipticPosSpeed[2], horCoord);
    }

    private double[][] CreatePosForElementBasedPoint(SolarSystemPoints solSysPoint, double julDay, Location location, double obliquity)
    {
        double[] eclipticPos = _solSysPointElementsCalc.Calculate(solSysPoint, julDay);
        double[] equatorialPos = _coordinateConversionFacade.EclipticToEquatorial(new double[] { eclipticPos[0], eclipticPos[1] }, obliquity);
        return new double[][] { eclipticPos, equatorialPos };

    }
}


