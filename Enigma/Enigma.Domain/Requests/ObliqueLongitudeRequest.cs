﻿// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.Dtos;

namespace Enigma.Domain.Requests;

/// <summary>Data for a request to calculate oblique longitudes.</summary>
/// <param name="Armc">Right ascension of the MC (in degrees).</param>
/// <param name="Obliquity">True Obliquity of the earths axis.</param>
/// <param name="GeoLat">Geographic latitude.</param>
/// <param name="CelPointCoordinates">Celestial point for which to calculate the oblique longitude, incoluding their
/// ecliptical coordinates.</param>
/// <param name="AyanamshaOffset">Offset for ayanamsha, zero for tropical calculations.</param>
public record ObliqueLongitudeRequest(double Armc, double Obliquity, double GeoLat, 
    List<NamedEclipticCoordinates> CelPointCoordinates, double AyanamshaOffset);
