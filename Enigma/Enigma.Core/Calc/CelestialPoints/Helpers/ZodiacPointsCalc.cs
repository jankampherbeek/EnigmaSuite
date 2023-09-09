// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Interfaces;
using Enigma.Domain.Calc;
using Enigma.Domain.Calc.ChartItems;
using Enigma.Domain.Points;
using Enigma.Facades.Interfaces;

namespace Enigma.Core.Calc.CelestialPoints.Helpers;

/// <inheritdoc/>
public sealed class ZodiacPointsCalc : IZodiacPointsCalc
{
    private readonly ICoTransFacade _coordinateConversionFacade;
    private readonly IHorizontalHandler _horizontalHandler;
    private readonly IFullPointPosFactory _fullPointPosFactory;

    public ZodiacPointsCalc(ICoTransFacade coordinateConversionFacade, IHorizontalHandler horizontalHandler, IFullPointPosFactory fullPointPosFactory)
    {
        _coordinateConversionFacade = coordinateConversionFacade;
        _horizontalHandler = horizontalHandler;
        _fullPointPosFactory = fullPointPosFactory;
    }


    public Dictionary<ChartPoints, FullPointPos> CalculateAllZodiacalPoints(CalculationPreferences prefs, double jdUt, double obliquity, Location location)
    {
        Dictionary<ChartPoints, FullPointPos> allZodiacalPointsLots = new();
        List<ChartPoints> allPoints = prefs.ActualChartPoints;
        foreach (var point in allPoints)
        {
            if (point.GetDetails().CalculationCat != CalculationCats.ZodiacFixed) continue;
            double longitude = 0.0;
            if (point == ChartPoints.ZeroAries) longitude = 0.0;                // TODO backlog add Zero Cancer
            allZodiacalPointsLots.Add(point, CalculateZodiacFixedPoint(longitude, jdUt, obliquity, location));
        }
        return allZodiacalPointsLots;

    }


    private FullPointPos CalculateZodiacFixedPoint(double longitude, double jdUt, double obliquity, Location location)
    {


        PosSpeed[] eclipticPosSpeed = { new(longitude, 0.0), new(0.0, 0.0), new(0.0, 0.0) };

        const double latitude = 0.0;
        double[] equatorialPos = _coordinateConversionFacade.EclipticToEquatorial(new[] { longitude, latitude }, obliquity);
        double ra = equatorialPos[0];
        double decl = equatorialPos[1];
        PosSpeed[] equatorialPosSpeed = { new(ra, 0.0), new(decl, 0.0), new(0.0, 0.0) };

        EquatorialCoordinates equCoordinates = new(ra, decl);
        HorizontalRequest horizontalRequest = new(jdUt, location, equCoordinates);
        HorizontalCoordinates horCoord = _horizontalHandler.CalcHorizontal(horizontalRequest);
        return _fullPointPosFactory.CreateFullPointPos(eclipticPosSpeed, equatorialPosSpeed, horCoord);

    }


}