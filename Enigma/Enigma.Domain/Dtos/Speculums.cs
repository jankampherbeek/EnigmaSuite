// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.References;

namespace Enigma.Domain.Dtos;

/// <summary>Base elements for all speculums.</summary>
/// <param name="InMundo">True for directions in mundo, false for directions in zodiaco.</param>
/// <param name="GeoLat">Geographic latitude.</param>
/// <param name="RaMc">Right Ascension of the MC.</param>
/// <param name="RaIc">Right Ascension of the IC.</param>
/// <param name="Obliquity">Obliquity.</param>
/// <param name="Mc">Longitude MC.</param>
/// <param name="Ascendant">Longitude Ascendant.</param>
public record SpeculumBase(
    bool InMundo,           
    double GeoLat,          
    double RaMc,            
    double RaIc,            
    double Obliquity,       
    double Mc,              
    double Ascendant);      

/// <summary>Speculum for Placidus / semi arc directions.</summary>
/// <param name="Base"> Base data.</param>
/// <param name="DetailLines">Data per point definition.</param>
public record PlacidusSpeculum(
    SpeculumBase Base,                                                  
    Dictionary<PlacidusPointDef, PlacidusPointDetails> DetailLines);    

/// <summary>Details per point. The significator can be a point inclusive aspect.
/// The promissor is always a point without aspect.</summary>
/// <param name="Significator">True: can be used as significator.</param>
/// <param name="Promissor">True: can be used as a promissor.</param>
/// <param name="EasternHemisphere">True for eastern hemisphere, false for western hemisphere.</param>
/// <param name="AboveHorizon">True if above horizon, false if under horizon.</param>
/// <param name="Lon">Longitude.</param>
/// <param name="Lat">Latitude.</param>
/// <param name="Decl">Declination.</param>
/// <param name="AscDiff">Ascensional Difference.</param>
/// <param name="OblAsc">Oblique Ascension.</param>
/// <param name="HorDist">Horizontal Distance.</param>
/// <param name="MerDist">Meridian distance. Upper MD if above horizon, else Lower MD.</param>
/// <param name="SemiArc">Semi Arc, daily (diurnalis) if above horizon, else nocturnal.</param>
/// <param name="PropDist">Proportional distance, only relevant if point is used as significator.</param>
public record PlacidusPointDetails(
    bool Significator,              
    bool Promissor,                 
    bool EasternHemisphere,         
    bool AboveHorizon,               
    double Lon,                     
    double Lat,                     
    double Decl,                    
    double AscDiff,                 
    double OblAsc,                  
    double HorDist,      
    double MerDist,      
    double SemiArc,       
    double PropDist);     

/// <summary>Point definition for Placidus directions.</summary>
/// <remarks>For the significator, or if the chartpoint should be used without aspects, use only conjunction.</remarks>
/// <param name="point">Any instance of a chart point, planet, cusp, etc.</param>
/// <param name="aspect">Aspect from chartpoint.</param>
public record PlacidusPointDef(
    ChartPoints point,       
    AspectTypes aspect);    
                             