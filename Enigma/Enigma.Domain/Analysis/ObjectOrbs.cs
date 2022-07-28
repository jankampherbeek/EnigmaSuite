// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.CalcVars;

namespace Enigma.Domain.Analysis;


/// <summary>
/// Default orb factor for a Solaar System Point.
/// </summary>
public record SolSysPointOrb
{
    public readonly SolarSystemPoints SolSysPoint;
    public readonly double OrbFactor;

    public SolSysPointOrb(SolarSystemPoints solSysPoint, double orbFactor)
    {
        SolSysPoint = solSysPoint;
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

public interface IOrbDefinitions
{
    public SolSysPointOrb DefineSolSysPointOrb(SolarSystemPoints solSysPoint);
    public MundanePointOrb DefineMundanePointOrb(string mundanePoint);
}


public class OrbDefinitions : IOrbDefinitions
{

    public SolSysPointOrb DefineSolSysPointOrb(SolarSystemPoints solSysPoint)
    {
        return solSysPoint switch
        {
            SolarSystemPoints.Sun => new SolSysPointOrb(solSysPoint, 1.0),
            SolarSystemPoints.Moon => new SolSysPointOrb(solSysPoint, 1.0),
            SolarSystemPoints.Mercury => new SolSysPointOrb(solSysPoint, 0.9),
            SolarSystemPoints.Venus => new SolSysPointOrb(solSysPoint, 0.9),
            SolarSystemPoints.Earth => new SolSysPointOrb(solSysPoint, 1.0),
            SolarSystemPoints.Mars => new SolSysPointOrb(solSysPoint, 0.9),
            SolarSystemPoints.Jupiter => new SolSysPointOrb(solSysPoint, 0.7),
            SolarSystemPoints.Saturn => new SolSysPointOrb(solSysPoint, 0.7),
            SolarSystemPoints.Uranus => new SolSysPointOrb(solSysPoint, 0.6),
            SolarSystemPoints.Neptune => new SolSysPointOrb(solSysPoint, 0.6),
            SolarSystemPoints.Pluto => new SolSysPointOrb(solSysPoint, 0.6),
            SolarSystemPoints.MeanNode => new SolSysPointOrb(solSysPoint, 0.3),
            SolarSystemPoints.TrueNode => new SolSysPointOrb(solSysPoint, 0.3),
            SolarSystemPoints.Chiron => new SolSysPointOrb(solSysPoint, 0.3),
            SolarSystemPoints.PersephoneRam => new SolSysPointOrb(solSysPoint, 0.0),
            SolarSystemPoints.HermesRam => new SolSysPointOrb(solSysPoint, 0.0),
            SolarSystemPoints.DemeterRam => new SolSysPointOrb(solSysPoint, 0.0),
            SolarSystemPoints.CupidoUra => new SolSysPointOrb(solSysPoint, 0.0),
            SolarSystemPoints.HadesUra => new SolSysPointOrb(solSysPoint, 0.0),
            SolarSystemPoints.ZeusUra => new SolSysPointOrb(solSysPoint, 0.0),
            SolarSystemPoints.KronosUra => new SolSysPointOrb(solSysPoint, 0.0),
            SolarSystemPoints.ApollonUra => new SolSysPointOrb(solSysPoint, 0.0),
            SolarSystemPoints.AdmetosUra => new SolSysPointOrb(solSysPoint, 0.0),
            SolarSystemPoints.VulcanusUra => new SolSysPointOrb(solSysPoint, 0.0),
            SolarSystemPoints.PoseidonUra => new SolSysPointOrb(solSysPoint, 0.0),
            SolarSystemPoints.Eris => new SolSysPointOrb(solSysPoint, 0.3),
            _ => throw new ArgumentException("Orb definition for solar system point unknown : " + solSysPoint.ToString())
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

