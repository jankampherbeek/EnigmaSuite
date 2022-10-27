// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.CalcVars;

namespace Enigma.Domain.Positional;

/// <summary>Results of calculation for a single Solar System Point.</summary>
public record FullSolSysPointPos
{
    public readonly SolarSystemPoints SolarSystemPoint;
    public readonly PosSpeed Longitude;
    public readonly PosSpeed Latitude;
    public readonly PosSpeed RightAscension;
    public readonly PosSpeed Declination;
    public readonly PosSpeed Distance;
    public readonly HorizontalCoordinates AzimuthAltitude;

    /// <param name="solarSystemPoint">Instance of the enum SolarSystemPoints.</param>
    /// <param name="longitude">Longitude in degrees.</param>
    /// <param name="latitude">Latitude in degrees.</param>
    /// <param name="rightAscension">Right ascension in degrees.</param>
    /// <param name="declination">Declination in degrees.</param>
    /// <param name="distance">distance in AU.</param>
    /// <param name="azimuthAltitude">Azimuth and altitude in degrees.</param>
    public FullSolSysPointPos(SolarSystemPoints solarSystemPoint, PosSpeed longitude, PosSpeed latitude, PosSpeed rightAscension,
        PosSpeed declination, PosSpeed distance, HorizontalCoordinates azimuthAltitude)
    {
        SolarSystemPoint = solarSystemPoint;
        Longitude = longitude;
        Latitude = latitude;
        RightAscension = rightAscension;
        Declination = declination;
        Distance = distance;
        AzimuthAltitude = azimuthAltitude;
    }
}


/// <summary>Combination of position and speed (for a solar system point).</summary>
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


/// <summary>Position, speed and distance in a coordinatesystem for point in the Solar system.</summary>
public record SolSysPointPosSpeeds
{
    public readonly PosSpeed MainPosSpeed;
    public readonly PosSpeed DeviationPosSpeed;
    public readonly PosSpeed DistancePosSpeed;

    public SolSysPointPosSpeeds(double[] values)
    {
        if (values.Length != 6) throw new ArgumentException("Wrong numer of values for SolSysPointSpeeds.");
        MainPosSpeed = new PosSpeed(values[0], values[1]);
        DeviationPosSpeed = new PosSpeed(values[2], values[3]);
        DistancePosSpeed = new PosSpeed(values[4], values[5]);
    }

    public SolSysPointPosSpeeds(PosSpeed mainPosSpeed, PosSpeed deviationPosSpeed, PosSpeed distancePosSpeed)
    {
        MainPosSpeed = mainPosSpeed;
        DeviationPosSpeed = deviationPosSpeed;
        DistancePosSpeed = distancePosSpeed;
    }
}