// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Dtos;
using Enigma.Domain.References;

namespace Enigma.Domain.Requests;

/// <summary>Request for the calculation of an OOB Calendar.</summary>
/// <param name="JdStart">Julian day for the radix.</param>
/// <param name="TimeOffset">Offset of timezone in fractional hours.</param>
/// <param name="Cal">Calendar</param>
/// <param name="Location">Actual location. Only relevant if parallax is used.</param>
/// <param name="Config">Current configuration.</param>
public record OobCalRequest(double JdStart, double TimeOffset, Calendars Cal, Location? Location, AstroConfig? Config);