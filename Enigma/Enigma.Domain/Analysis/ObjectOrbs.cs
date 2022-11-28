// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Enums;
using Enigma.Domain.Interfaces;

namespace Enigma.Domain.Analysis;


/// <summary>
/// Default orb factor for a celestial point.
/// </summary>
public record CelPointOrb
{
    public readonly CelPoints CelPoint;
    public readonly double OrbFactor;

    public CelPointOrb(CelPoints celPoint, double orbFactor)
    {
        CelPoint = celPoint;
        OrbFactor = orbFactor;
    }
}

/// <summary>
/// Default orb factor for a mundane point.
/// </summary>
public record MundanePointOrb
{
    public readonly string MundanePoint;
    public readonly double OrbFactor;

    public MundanePointOrb(string mundanePoint, double orbFactor)
    {
        MundanePoint = mundanePoint;
        OrbFactor = orbFactor;
    }
}


public class OrbDefinitions : IOrbDefinitions
{

    public CelPointOrb DefineCelPointOrb(CelPoints celPoint)
    {
        return celPoint switch
        {
            CelPoints.Sun => new CelPointOrb(celPoint, 1.0),
            CelPoints.Moon => new CelPointOrb(celPoint, 1.0),
            CelPoints.Mercury => new CelPointOrb(celPoint, 0.9),
            CelPoints.Venus => new CelPointOrb(celPoint, 0.9),
            CelPoints.Earth => new CelPointOrb(celPoint, 1.0),
            CelPoints.Mars => new CelPointOrb(celPoint, 0.9),
            CelPoints.Jupiter => new CelPointOrb(celPoint, 0.7),
            CelPoints.Saturn => new CelPointOrb(celPoint, 0.7),
            CelPoints.Uranus => new CelPointOrb(celPoint, 0.6),
            CelPoints.Neptune => new CelPointOrb(celPoint, 0.6),
            CelPoints.Pluto => new CelPointOrb(celPoint, 0.6),
            CelPoints.MeanNode => new CelPointOrb(celPoint, 0.3),
            CelPoints.TrueNode => new CelPointOrb(celPoint, 0.3),
            CelPoints.Chiron => new CelPointOrb(celPoint, 0.3),
            CelPoints.PersephoneRam => new CelPointOrb(celPoint, 0.0),
            CelPoints.HermesRam => new CelPointOrb(celPoint, 0.0),
            CelPoints.DemeterRam => new CelPointOrb(celPoint, 0.0),
            CelPoints.CupidoUra => new CelPointOrb(celPoint, 0.0),
            CelPoints.HadesUra => new CelPointOrb(celPoint, 0.0),
            CelPoints.ZeusUra => new CelPointOrb(celPoint, 0.0),
            CelPoints.KronosUra => new CelPointOrb(celPoint, 0.0),
            CelPoints.ApollonUra => new CelPointOrb(celPoint, 0.0),
            CelPoints.AdmetosUra => new CelPointOrb(celPoint, 0.0),
            CelPoints.VulcanusUra => new CelPointOrb(celPoint, 0.0),
            CelPoints.PoseidonUra => new CelPointOrb(celPoint, 0.0),
            CelPoints.Eris => new CelPointOrb(celPoint, 0.3),
            _ => throw new ArgumentException("Orb definition for celestial point unknown : " + celPoint.ToString())
        };
    }

    public MundanePointOrb DefineMundanePointOrb(string mundanePoint)
    {
        return mundanePoint.ToUpper() switch
        {
            "MC" => new MundanePointOrb(mundanePoint, 1.0),
            "ASC" => new MundanePointOrb(mundanePoint, 1.0),
            _ => throw new ArgumentException("Orb definition for mundane point unknown : " + mundanePoint)
        };
    }
}

