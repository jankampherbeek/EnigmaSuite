// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Core.Handlers.Interfaces;
using Enigma.Domain.Calc;
using Enigma.Domain.Calc.ChartItems;
using Enigma.Domain.Calc.ChartItems.Coordinates;
using Enigma.Domain.Calc.Specials;
using Enigma.Domain.Charts;
using Enigma.Domain.Points;
using Enigma.Facades.Interfaces;
using Enigma.Facades.Se;

namespace Enigma.Core.Handlers.Calc.CelestialPoints;


/// <inheritdoc/>
public sealed class CelPointsHandler : ICelPointsHandler
{
    private readonly ISeFlags _seFlags;
    private readonly ICelPointSECalc _celPointSECalc;
    private readonly ICelPointsElementsCalc _celPointElementsCalc;
    private readonly ICoTransFacade _coordinateConversionFacade;
    private readonly IObliquityHandler _obliquityHandler;
    private readonly IHorizontalHandler _horizontalHandler;
    private readonly IChartPointsMapping _chartPointsMapping;


    public CelPointsHandler(ISeFlags seFlags,
                               ICelPointSECalc positionCelPointSECalc,
                               ICelPointsElementsCalc posCelPointsElementsCalc,
                               ICoTransFacade coordinateConversionFacade,
                               IObliquityHandler obliquityHandler,
                               IHorizontalHandler horizontalHandler,
                               IChartPointsMapping chartPointsMapping)
    {
        _seFlags = seFlags;
        _celPointSECalc = positionCelPointSECalc;
        _celPointElementsCalc = posCelPointsElementsCalc;
        _coordinateConversionFacade = coordinateConversionFacade;
        _obliquityHandler = obliquityHandler;
        _horizontalHandler = horizontalHandler;
        _chartPointsMapping = chartPointsMapping;
    }


    public CelPointsResponse CalcCelPoints(CelPointsRequest request)
    {
        double julDay = request.JulianDayUt;
        double previousJd = julDay - 0.5;
        double futureJd = julDay + 0.5;
        Location location = request.Location;
        var obliquityRequest = new ObliquityRequest(julDay, true);

        double obliquity = _obliquityHandler.CalcObliquity(obliquityRequest);
        if (request.CalculationPreferences.ActualZodiacType == ZodiacTypes.Sidereal)
        {
            int idAyanamsa = request.CalculationPreferences.ActualAyanamsha.GetDetails().SeId;
            SeInitializer.SetAyanamsha(idAyanamsa);
        }
        int _flagsEcliptical = _seFlags.DefineFlags(CoordinateSystems.Ecliptical, request.CalculationPreferences.ActualObserverPosition, request.CalculationPreferences.ActualZodiacType);
        int _flagsEquatorial = _seFlags.DefineFlags(CoordinateSystems.Equatorial, request.CalculationPreferences.ActualObserverPosition, request.CalculationPreferences.ActualZodiacType);
        var _fullCelPoints = new List<FullChartPointPos>();
        foreach (ChartPoints celPoint in request.CalculationPreferences.ActualChartPoints)
        {
            CalculationTypes calculationType = _chartPointsMapping.CalculationTypeForPoint(celPoint);
            if (calculationType == CalculationTypes.CelPointSE)
            {
                _fullCelPoints.Add(CreatePosForSePoint(celPoint, julDay, location, _flagsEcliptical, _flagsEquatorial));
            }
            else if (calculationType == CalculationTypes.CelPointElements)
            {
                double[][] positions = CreatePosForElementBasedPoint(celPoint, julDay, obliquity);
                double[][] previousPositions = CreatePosForElementBasedPoint(celPoint, previousJd, obliquity);
                double[][] futurePositions = CreatePosForElementBasedPoint(celPoint, futureJd, obliquity);
                PosSpeed longPosSpeed = new(positions[0][0], futurePositions[0][0] - previousPositions[0][0]);
                PosSpeed latPosSpeed = new(positions[0][1], futurePositions[0][1] - previousPositions[0][1]);
                PosSpeed distPosSpeed = new(positions[0][2], futurePositions[0][2] - previousPositions[0][2]);
                PosSpeed raPosSpeed = new(positions[1][0], futurePositions[1][0] - previousPositions[1][0]);
                PosSpeed declPosSpeed = new(positions[1][1], futurePositions[1][1] - previousPositions[1][1]);
                EclipticCoordinates eclCoordinates = new(positions[0][0], positions[0][1]);
                HorizontalRequest horizontalRequest = new(julDay, request.Location, eclCoordinates);
                HorizontalCoordinates horCoord = _horizontalHandler.CalcHorizontal(horizontalRequest);
                FullPointPos fullPointPos = new(longPosSpeed, latPosSpeed, raPosSpeed, declPosSpeed, horCoord);
                _fullCelPoints.Add(new FullChartPointPos(celPoint, distPosSpeed, fullPointPos));
            }
        }
        bool success = true;
        string errorText = "";
        return new CelPointsResponse(_fullCelPoints, success, errorText);
    }


    private FullChartPointPos CreatePosForSePoint(ChartPoints celPoint, double julDay, Location location, int flagsEcl, int flagsEq)
    {
        PosSpeed[] eclipticPosSpeed = _celPointSECalc.CalculateCelPoint(celPoint, julDay, location, flagsEcl);
        PosSpeed longPosSpeed = eclipticPosSpeed[0];
        PosSpeed latPosSpeed = eclipticPosSpeed[1];
        PosSpeed distPosSpeed = eclipticPosSpeed[2];
        PosSpeed[] equatorialPosSpeed = _celPointSECalc.CalculateCelPoint(celPoint, julDay, location, flagsEq);
        PosSpeed raPosSpeed = equatorialPosSpeed[0];
        PosSpeed declPosSpeed = equatorialPosSpeed[1];
        var eclCoordinates = new EclipticCoordinates(eclipticPosSpeed[0].Position, eclipticPosSpeed[1].Position);
        HorizontalRequest horizontalRequest = new(julDay, location, eclCoordinates);
        HorizontalCoordinates horCoord = _horizontalHandler.CalcHorizontal(horizontalRequest);
        FullPointPos fullPointPos = new(longPosSpeed, latPosSpeed, raPosSpeed, declPosSpeed, horCoord);
        return new FullChartPointPos(celPoint, distPosSpeed, fullPointPos);
    }


    private double[][] CreatePosForElementBasedPoint(ChartPoints celPoint, double julDay, double obliquity)
    {
        double[] eclipticPos = _celPointElementsCalc.Calculate(celPoint, julDay);
        double[] equatorialPos = _coordinateConversionFacade.EclipticToEquatorial(new double[] { eclipticPos[0], eclipticPos[1] }, obliquity);
        return new double[][] { eclipticPos, equatorialPos };
    }
}
