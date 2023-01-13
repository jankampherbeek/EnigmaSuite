// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.Calc.ChartItems.Coordinates;

namespace Enigma.Domain.Points;


/// <summary>General data for a fully defined Position</summary>
/// <remarks>This record should be included in specific records, e.g. FullChartPointPos etc.</remarks>
/// <param name="Longitude">Longitude in degrees.</param>
/// <param name="Latitude">Latitude in degrees.</param>
/// <param name="RightAscension">Right ascension in degrees.</param>
/// <param name="Declination">Declination in degrees.</param>
/// <param name="AzimuthAltitude">Azimuth and altitude in degrees.</param>
public record FullPointPos(PosSpeed Longitude, PosSpeed Latitude, PosSpeed RightAscension, PosSpeed Declination, HorizontalCoordinates AzimuthAltitude);


/// <summary>Results of calculation for a single chart point.</summary>
public record FullChartPointPos
{
    public readonly ChartPoints ChartPoint;
    public readonly string ChartPointName;
    public readonly PosSpeed Distance;
    public readonly FullPointPos PointPos;

    /// <param name="chartPoint">Instance of the enum ChartPoints.</param>
    /// <param name="distance">distance in AU.</param>
    /// <param name="pointPos">Postion in all coordinates.</param>
    public FullChartPointPos(ChartPoints chartPoint, PosSpeed distance, FullPointPos pointPos)
    {
        ChartPoint = chartPoint;
        ChartPointName = ChartPoint.ToString();
        Distance = distance;
        PointPos = pointPos;
    }
}


/// <summary>Combination of Position and Speed for a point.</summary>
/// <param name="Position">Position of point.</param>
/// <param name="Speed">Speed of point.</param>
public record PosSpeed(double Position, double Speed);



/// <summary>Position, Speed and distance in a coordinatesystem for a point.</summary>
public record PointPosSpeeds
{
    public readonly PosSpeed MainPosSpeed;
    public readonly PosSpeed DeviationPosSpeed;
    public readonly PosSpeed DistancePosSpeed;

    public PointPosSpeeds(double[] values)
    {
        if (values.Length != 6) throw new ArgumentException("Wrong number of values for PointSpeeds.");
        MainPosSpeed = new PosSpeed(values[0], values[1]);
        DeviationPosSpeed = new PosSpeed(values[2], values[3]);
        DistancePosSpeed = new PosSpeed(values[4], values[5]);
    }

    public PointPosSpeeds(PosSpeed mainPosSpeed, PosSpeed deviationPosSpeed, PosSpeed distancePosSpeed)
    {
        MainPosSpeed = mainPosSpeed;
        DeviationPosSpeed = deviationPosSpeed;
        DistancePosSpeed = distancePosSpeed;
    }
}