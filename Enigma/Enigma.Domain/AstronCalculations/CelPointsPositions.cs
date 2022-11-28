// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.Enums;

namespace Enigma.Domain.AstronCalculations;

/// <summary>Results of calculation for a single celestial point.</summary>
public record FullCelPointPos
{
    public readonly CelPoints CelPoint;
    public readonly PosSpeed Longitude;
    public readonly PosSpeed Latitude;
    public readonly PosSpeed RightAscension;
    public readonly PosSpeed Declination;
    public readonly PosSpeed Distance;
    public readonly HorizontalCoordinates AzimuthAltitude;

    /// <param name="celPoint">Instance of the enum CelPoints.</param>
    /// <param name="longitude">Longitude in degrees.</param>
    /// <param name="latitude">Latitude in degrees.</param>
    /// <param name="rightAscension">Right ascension in degrees.</param>
    /// <param name="declination">Declination in degrees.</param>
    /// <param name="distance">distance in AU.</param>
    /// <param name="azimuthAltitude">Azimuth and altitude in degrees.</param>
    public FullCelPointPos(CelPoints celPoint, PosSpeed longitude, PosSpeed latitude, PosSpeed rightAscension,
        PosSpeed declination, PosSpeed distance, HorizontalCoordinates azimuthAltitude)
    {
        CelPoint = celPoint;
        Longitude = longitude;
        Latitude = latitude;
        RightAscension = rightAscension;
        Declination = declination;
        Distance = distance;
        AzimuthAltitude = azimuthAltitude;
    }
}


/// <summary>Combination of position and speed (for a celestial point).</summary>
public record PosSpeed
{
    public readonly double Position;
    public readonly double Speed;

    public PosSpeed(double position, double speed)
    {
        Position = position;
        Speed = speed;
    }
}


/// <summary>Position, speed and distance in a coordinatesystem for a celestial point.</summary>
public record CelPointPosSpeeds
{
    public readonly PosSpeed MainPosSpeed;
    public readonly PosSpeed DeviationPosSpeed;
    public readonly PosSpeed DistancePosSpeed;

    public CelPointPosSpeeds(double[] values)
    {
        if (values.Length != 6) throw new ArgumentException("Wrong numer of values for CelPointSpeeds.");
        MainPosSpeed = new PosSpeed(values[0], values[1]);
        DeviationPosSpeed = new PosSpeed(values[2], values[3]);
        DistancePosSpeed = new PosSpeed(values[4], values[5]);
    }

    public CelPointPosSpeeds(PosSpeed mainPosSpeed, PosSpeed deviationPosSpeed, PosSpeed distancePosSpeed)
    {
        MainPosSpeed = mainPosSpeed;
        DeviationPosSpeed = deviationPosSpeed;
        DistancePosSpeed = distancePosSpeed;
    }
}