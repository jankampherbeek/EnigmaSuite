// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.Presentables;


/// <summary>
/// Presentable data for elements and crosses in a BLA Schema
/// </summary>
/// <param name="Name">Name of element or cross</param>
/// <param name="Sign">Count in signs</param>
/// <param name="House">Count in houses</param>
/// <param name="Sum">Som of count for houses and signs</param>
/// <param name="Hcusp">HCusp</param>
/// <param name="Total">Total</param>
public record PresentableCrossElementsCount(String Name, int Sign, int House, int Sum, int Hcusp, int Total);
