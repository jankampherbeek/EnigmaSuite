// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.References;

namespace Enigma.Domain.Dtos;


/// <summary>
/// Simplified version of a chart. Contains longitudes for chartpoints and cusps.
/// </summary>
/// <param name="Points">The chartpoints and their longitudes</param>
/// <param name="Cusps">The cusps and their longitudes</param>
public record ChartLongitudes(
    Dictionary<ChartPoints, double> Points,
    Dictionary<int, double> Cusps);
