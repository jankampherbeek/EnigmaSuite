// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.
namespace Enigma.Core.Charts.Prog.PrimDir;


/// <summary>Parent for different types of speculums.</summary>
public abstract record SpeculumOld(SpeculumBaseOld SpecBaseOld);


/// <summary>Base elements for all speculums.</summary>
/// <param name="InMundo">True for directions in mundo, false for directions in zodiaco.</param>
/// <param name="GeoLat">Geographic latitude.</param>
/// <param name="JdRadix">Julian day number for the radix.</param>
/// <param name="RaMc">Right Ascension of the MC.</param>
/// <param name="RaIc">Right Ascension of the IC.</param>
/// <param name="Obl">Obliquity.</param>
/// <param name="LonMc">Longitude MC.</param>
/// <param name="LonAsc">Longitude Ascendant.</param>
/// <param name="OaAsc">Oblique Ascension Ascendant.</param>
public record SpeculumBaseOld(
    bool InMundo,           
    double GeoLat,         
    double JdRadix,
    double RaMc,            
    double RaIc,            
    double Obl,       
    double LonMc,              
    double LonAsc,
    double OaAsc);


