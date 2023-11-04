// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.References;

namespace Enigma.Domain.Dtos;

/// <summary>Speculum for primary directions.</summary>
/// <param name="Method">Method for the calculation of primary directions that is used.</param>
/// <param name="GeoLat">Geographic latitude,</param>
/// <param name="RaMc">Right Ascension of the MC.</param>
/// <param name="RaIc">Right Ascension of the IC.</param>
/// <param name="OaAscendant">Oblique Ascension of the ascendant.</param>
/// <param name="OdDescendant">Oblique Descension of the descendant.</param>
/// <param name="SpeculumItems">Details for specific points (both significators and promissors.</param>
public record Speculum(PrimaryDirMethods Method, double GeoLat, double RaMc, double RaIc, 
    double OaAscendant, double OdDescendant, Dictionary<ChartPoints, SpeculumItem> SpeculumItems);

/// <summary>Speculumitem with astronomical details for a specific point as used in SA directions.</summary>
/// <param name="Aspect">Indication of aspect, 0 for conjunction or distance in zodiacal direction.</param>
/// <param name="Ra">Right ascension.</param>
/// <param name="Declination">Declination.</param>
/// <param name="MdMc">Meridian distance to Mc.</param>
/// <param name="MdIc">Meridian distance to Ic.</param>
/// <param name="Ad">Ascensional difference.</param>
/// <param name="Oa">Oblique ascension.</param>
/// <param name="Hd">Horizontal difference.</param>
/// <param name="Dsa">Diurnal semi-arc.</param>
/// <param name="Nsa">Nocturnal semi-arc.</param>
/// <param name="PropSa">Proportional semi-arc.</param>
public record SpeculumItem(double Aspect, double Ra, double Declination, double MdMc, double MdIc, double Ad, double Oa, 
    double Hd, double Dsa, double Nsa, double PropSa);

