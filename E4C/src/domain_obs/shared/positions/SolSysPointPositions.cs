// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;

namespace E4C.domain.shared.positions;

/// <summary>
/// Combination of position and speed (for a solar system point).
/// </summary>
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





/// <summary>
/// Position, speed and distance in a coordinatesystem for point in the Solar system.
/// </summary>
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