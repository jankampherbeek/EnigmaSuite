// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers.Interfaces;
using Enigma.Domain.Calc.ChartItems;
using Enigma.Domain.Calc.ChartItems.Coordinates;
using Enigma.Domain.Calc.Specials;
using Enigma.Domain.Points;
using Enigma.Facades.Interfaces;

namespace Enigma.Core.Handlers.Calc.CelestialPoints.Helpers;

/// <inheritdoc/>
public sealed class ZodiacPointsCalc: IZodiacPointsCalc
{
    private readonly IObliquityHandler _obliquityHandler;
    private readonly ICoTransFacade _coordinateConversionFacade;
    private readonly IHorizontalHandler _horizontalHandler;

    public ZodiacPointsCalc(IObliquityHandler obliquityHandler, ICoTransFacade coordinateConversionFacade, IHorizontalHandler horizontalHandler)
    {
        _obliquityHandler = obliquityHandler;
        _coordinateConversionFacade = coordinateConversionFacade;
        _horizontalHandler = horizontalHandler;
    }

    /// <inheritdoc/>
    public FullPointPos DefineZeroAries(CelPointsRequest request)
    {
        double longitude = 0.0;
        return PerformCalculation(request, longitude);
    }

    /// <inheritdoc/>
    public FullPointPos DefineZeroCancer(CelPointsRequest request)
    {
        double longitude = 90.0;
        return PerformCalculation(request, longitude);
    }

    private FullPointPos PerformCalculation(CelPointsRequest request, double longitude) {
        double julDay = request.JulianDayUt;
        var obliquityRequest = new ObliquityRequest(julDay, true);
        double obliquity = _obliquityHandler.CalcObliquity(obliquityRequest);
        double latitude = 0.0;
        double[] equatorialPos = _coordinateConversionFacade.EclipticToEquatorial(new double[] { longitude, latitude }, obliquity);
        double ra = equatorialPos[0];
        double decl = equatorialPos[1];

        EclipticCoordinates eclCoordinates = new(longitude, latitude);
        HorizontalRequest horizontalRequest = new(julDay, request.Location, eclCoordinates);
        HorizontalCoordinates horCoord = _horizontalHandler.CalcHorizontal(horizontalRequest);
        PosSpeed aziPosSpeed = new(horCoord.Azimuth, 0.0);
        PosSpeed altPosSpeed = new(horCoord.Altitude, 0.0);

        PosSpeed longPosSpeed = new(longitude, 0.0);
        PosSpeed latPosSpeed = new(latitude, 0.0);
        PosSpeed raPosSpeed = new(ra, 0.0);
        PosSpeed declPosSpeed = new(decl, 0.0);
        PosSpeed distPosSpeed = new(0.0, 0.0);

        PointPosSpeeds ppsEcliptical = new(longPosSpeed, latPosSpeed, distPosSpeed);
        PointPosSpeeds ppsEquatorial = new(raPosSpeed, declPosSpeed, distPosSpeed);
        PointPosSpeeds ppsHorizontal = new(aziPosSpeed, altPosSpeed, distPosSpeed);
        return new FullPointPos(ppsEcliptical, ppsEquatorial, ppsHorizontal);
    }


}