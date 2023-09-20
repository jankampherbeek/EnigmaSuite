// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


namespace Enigma.Domain.Dtos;


/// <summary>Full data for a specific point.</summary>
/// <param name="Ecliptical">Longitude and latitude.</param>
/// <param name="Equatorial">Right ascension and declination.</param>
/// <param name="Horizontal">Azimuth and altitude.</param>
public record FullPointPos(PointPosSpeeds Ecliptical, PointPosSpeeds Equatorial, PointPosSpeeds Horizontal);



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

    public PointPosSpeeds(IReadOnlyList<double> values)
    {
        if (values.Count != 6) throw new ArgumentException("Wrong number of values for PointSpeeds.");
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