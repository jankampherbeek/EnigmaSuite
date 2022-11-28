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

namespace Enigma.Core.Work.Handlers.Calc.CelestialPoints;




/// <inheritdoc/>
public class CelsPointsHandler : ICelPointsHandler
{
    private readonly IAyanamshaSpecifications _ayanamshaSpecifications;
    private readonly ISeFlags _seFlags;
    private readonly ICelPointSECalc _celPointSECalc;
    private readonly ICelPointsElementsCalc _celPointElementsCalc;
    private readonly ICelPointSpecifications _celPointSpecs;
    private readonly ICoTransFacade _coordinateConversionFacade;
    private readonly IObliquityHandler _obliquityHandler;
    private readonly IHorizontalHandler _horizontalHandler;


    public CelsPointsHandler(IAyanamshaSpecifications ayanamshaSpecifications,
                               ISeFlags seFlags,
                               ICelPointSECalc positionCelPointSECalc,
                               ICelPointsElementsCalc posCelPointsElementsCalc,
                               ICelPointSpecifications celPointspecs,
                               ICoTransFacade coordinateConversionFacade,
                               IObliquityHandler obliquityHandler,
                               IHorizontalHandler horizontalHandler)
    {
        _ayanamshaSpecifications = ayanamshaSpecifications;
        _seFlags = seFlags;
        _celPointSECalc = positionCelPointSECalc;
        _celPointSpecs = celPointspecs;
        _celPointElementsCalc = posCelPointsElementsCalc;
        _coordinateConversionFacade = coordinateConversionFacade;
        _obliquityHandler = obliquityHandler;
        _horizontalHandler = horizontalHandler;

    }


    public CelPointsResponse CalcCelPoints(CelPointsRequest request)
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
        var _fullCelPoints = new List<FullCelPointPos>();
        foreach (Domain.Enums.CelPoints celPoint in request.ActualCalculationPreferences.ActualCelPoints)
        {
            CelPointDetails details = _celPointSpecs.DetailsForPoint(celPoint);
            if (details.CalculationType == CalculationTypes.SE)
            {
                _fullCelPoints.Add(CreatePosForSePoint(celPoint, julDay, location, _flagsEcliptical, _flagsEquatorial));
            }
            else if (details.CalculationType == CalculationTypes.Elements)
            {
                double[][] positions = CreatePosForElementBasedPoint(celPoint, julDay, location, obliquity);
                double[][] previousPositions = CreatePosForElementBasedPoint(celPoint, previousJd, location, obliquity);
                double[][] futurePositions = CreatePosForElementBasedPoint(celPoint, futureJd, location, obliquity);
                PosSpeed longPosSpeed = new(positions[0][0], futurePositions[0][0] - previousPositions[0][0]);
                PosSpeed latPosSpeed = new(positions[0][1], futurePositions[0][1] - previousPositions[0][1]);
                PosSpeed distPosSpeed = new(positions[0][2], futurePositions[0][2] - previousPositions[0][2]);
                PosSpeed raPosSpeed = new(positions[1][0], futurePositions[1][0] - previousPositions[1][0]);
                PosSpeed declPosSpeed = new(positions[1][1], futurePositions[1][1] - previousPositions[1][1]);
                EclipticCoordinates eclCoordinates = new(positions[0][0], positions[0][1]);
                HorizontalRequest horizontalRequest = new(julDay, request.ChartLocation, eclCoordinates);
                HorizontalCoordinates horCoord = _horizontalHandler.CalcHorizontal(horizontalRequest).HorizontalAzimuthAltitude;
                _fullCelPoints.Add(new FullCelPointPos(celPoint, longPosSpeed, latPosSpeed, raPosSpeed, declPosSpeed, distPosSpeed, horCoord));
            }
        }
        return new CelPointsResponse(_fullCelPoints, success, errorText);

    }

    private FullCelPointPos CreatePosForSePoint(Domain.Enums.CelPoints celPoint, double julDay, Location location, int flagsEcl, int flagsEq)
    {
        PosSpeed[] eclipticPosSpeed = _celPointSECalc.CalculateCelPoint(celPoint, julDay, location, flagsEcl);
        PosSpeed[] equatorialPosSpeed = _celPointSECalc.CalculateCelPoint(celPoint, julDay, location, flagsEq);
        var eclCoordinates = new EclipticCoordinates(eclipticPosSpeed[0].Position, eclipticPosSpeed[1].Position);
        HorizontalRequest horizontalRequest = new(julDay, location, eclCoordinates);
        HorizontalCoordinates horCoord = _horizontalHandler.CalcHorizontal(horizontalRequest).HorizontalAzimuthAltitude;
        return new FullCelPointPos(celPoint, eclipticPosSpeed[0], eclipticPosSpeed[1], equatorialPosSpeed[0], equatorialPosSpeed[1], eclipticPosSpeed[2], horCoord);
    }

    private double[][] CreatePosForElementBasedPoint(Domain.Enums.CelPoints celPoint, double julDay, Location location, double obliquity)
    {
        double[] eclipticPos = _celPointElementsCalc.Calculate(celPoint, julDay);
        double[] equatorialPos = _coordinateConversionFacade.EclipticToEquatorial(new double[] { eclipticPos[0], eclipticPos[1] }, obliquity);
        return new double[][] { eclipticPos, equatorialPos };

    }
}


