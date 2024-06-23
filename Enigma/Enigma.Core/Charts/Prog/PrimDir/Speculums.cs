// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.References;

namespace Enigma.Core.Charts.Prog.PrimDir;







     

/// <summary>Speculum for Placidus / semi arc directions and for Placidus under the pole.</summary>
/// <param name="BaseOld"> Base data.</param>
/// <param name="DetailLines">Data per point definition.</param>
public record PlacidusSpeculum(
    SpeculumBaseOld BaseOld,                                                  
    Dictionary<ChartPoints, PlacidusPointDetails> DetailLines);    

/// <summary>Details per point. The significator can be a point inclusive aspect.
/// The promissor is always a point without aspect.</summary>
/// <param name="Significator">True: can be used as significator.</param>
/// <param name="Promissor">True: can be used as a promissor.</param>
/// <param name="ChartL">True for eastern half of chart, otherwise false.</param>
/// <param name="AboveHorizon">True if above horizon, false if under horizon.</param>
/// <param name="Lon">Longitude.</param>
/// <param name="Lat">Latitude.</param>
/// <param name="Ra">Right Ascension.</param>
/// <param name="Decl">Declination.</param>
/// <param name="Ad">Ascensional Difference.</param>
/// <param name="Oa">Oblique Ascension.</param>
/// <param name="HorDist">Horizontal Distance.</param>
/// <param name="Md">Meridian distance. Upper MD if above horizon, else Lower MD.</param>
/// <param name="SemiArc">Semi Arc, daily (diurnalis) if above horizon, else nocturnal.</param>
/// <param name="AdPolePc">Ascension Difference under the pole, used for Placidus under the pole. </param>
/// <param name="ElevPolePc">Elevation of the pool, only used for Placidus under the pole. </param>
/// <param name="PropDist">Proportional distance, only relevant if point is used as significator.</param>
public record PlacidusPointDetails(
    bool Significator,              
    bool Promissor,                 
    bool ChartL,         
    bool AboveHorizon,               
    double Lon,                     
    double Lat,     
    double Ra,
    double Decl,      
    double Ad,                 
    double Oa,                  
    double HorDist,      
    double Md,      
    double SemiArc,
    double AdPolePc,
    double ElevPolePc,
    double PropDist);

                             