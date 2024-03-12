// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Dtos;
using Enigma.Domain.References;

namespace Enigma.Domain.Requests;

/// <summary>Request to calculate longitude equivalents.</summary>
/// <param name="Jd">Julian day number.</param>
/// <param name="PointsPosLongDecl">List with points, longitudes and declinations.</param>
public record LongitudeEquivalentRequest(double Jd, List<Tuple<ChartPoints, double, double>> PointsPosLongDecl);