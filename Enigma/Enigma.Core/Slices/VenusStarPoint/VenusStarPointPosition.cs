// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Core.Slices.VenusStarPoint;

/// <summary>
/// A single position for a Venus Star Point
/// </summary>
/// <param name="SequenceId">Number 1..5 that indicates the chronoligical sequence of the point</param>
/// <param name="Jd">Julian day</param>
/// <param name="Phenomenon">Indication of inferior/superior conjunction</param>
/// <param name="Longitude">Ecliptical longitude</param>
public record VenusStarPointPosition(int SequenceId, double Jd, VenusPhenomena Phenomenon, double Longitude);