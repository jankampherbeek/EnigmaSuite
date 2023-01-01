// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.AstronCalculations;

namespace Enigma.Domain.Points;


/// <summary>General data for a fully defined Position</summary>
/// <remarks>This recod should be included in specific records, e.g. FullCelPointPos etc.</remarks>
/// <param name="Longitude">Longitude in degrees.</param>
/// <param name="Latitude">Latitude in degrees.</param>
/// <param name="RightAscension">Right ascension in degrees.</param>
/// <param name="Declination">Declination in degrees.</param>
/// <param name="AzimuthAltitude">Azimuth and altitude in degrees.</param>
public record FullPointPos(PosSpeed Longitude, PosSpeed Latitude, PosSpeed RightAscension, PosSpeed Declination, HorizontalCoordinates AzimuthAltitude);


/// <summary>Results of calculation for a single celestial point.</summary>
public record FullCelPointPos
{
    public readonly CelPoints CelPoint;
    public readonly string CelPointName;
    public readonly PosSpeed Distance;
    public readonly FullPointPos GeneralPointPos;

    /// <param name="celPoint">Instance of the enum CelPoints.</param>
    /// <param name="longitude">Longitude in degrees.</param>
    /// <param name="latitude">Latitude in degrees.</param>
    /// <param name="rightAscension">Right ascension in degrees.</param>
    /// <param name="declination">Declination in degrees.</param>
    /// <param name="distance">distance in AU.</param>
    /// <param name="azimuthAltitude">Azimuth and altitude in degrees.</param>
    public FullCelPointPos(CelPoints celPoint, PosSpeed distance, FullPointPos generalPointPos)
    {
        CelPoint = celPoint;
        CelPointName = CelPoint.ToString();
        Distance = distance;
        GeneralPointPos = generalPointPos;
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
        if (values.Length != 6) throw new ArgumentException("Wrong numer of values for PointSpeeds.");
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