// Jan Kampherbeek, (c) 2022, 2023.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers.Interfaces;
using Enigma.Domain.Analysis.Aspects;
using Enigma.Domain.Interfaces;
using Enigma.Domain.Points;
using Serilog;

namespace Enigma.Core.Handlers.Analysis.Helpers;


public sealed class AspectOrbConstructor : IAspectOrbConstructor
{

    private readonly double _baseOrb = 10.0;   // todo make baseorb configurable

    private readonly IOrbDefinitions _orbDefinitions;
    private readonly IPointMappings _pointMappings;

    public AspectOrbConstructor(IOrbDefinitions orbDefinitions, IPointMappings pointMappings)
    {
        _orbDefinitions = orbDefinitions;
        _pointMappings = pointMappings;
    }

    /// <inheritdoc/>
    public double DefineOrb(CelPoints point1, CelPoints point2, AspectDetails aspectDetails)
    {
        double factor1 = _orbDefinitions.DefineCelPointOrb(point1).OrbFactor;
        double factor2 = _orbDefinitions.DefineCelPointOrb(point2).OrbFactor;
        double aspectFactor = aspectDetails.OrbFactor;
        return Math.Max(factor1, factor2) * aspectFactor * _baseOrb;
    }

    /// <inheritdoc/>
    public double DefineOrb(string mundanePoint, CelPoints celPoint, AspectDetails aspectDetails)
    {
        double mundaneFactor = _orbDefinitions.DefineMundanePointOrb(mundanePoint).OrbFactor;
        double celPointFactor = _orbDefinitions.DefineCelPointOrb(celPoint).OrbFactor;
        double aspectFactor = aspectDetails.OrbFactor;
        return Math.Max(mundaneFactor, celPointFactor) * aspectFactor * _baseOrb;
    }

    /// <inheritdoc/>
    public double DefineOrb(GeneralPoint point1, GeneralPoint point2, AspectDetails aspectDetails)
    {
        PointTypes point1Type = point1.PointType;
        PointTypes point2Type = point2.PointType;
        if (point1Type == PointTypes.CelestialPoint && point2Type == PointTypes.CelestialPoint)
        {
            CelPoints cp1 = _pointMappings.CelPointForIndex(point1.Index);
            CelPoints cp2 = _pointMappings.CelPointForIndex(point2.Index);
            return DefineOrb(cp1, cp2, aspectDetails);
        }
        if (point1Type == PointTypes.CelestialPoint && point2Type == PointTypes.MundaneSpecialPoint)
        {
            CelPoints cp = _pointMappings.CelPointForIndex(point1.Index);
            MundanePoints mp = _pointMappings.MundanePointForIndex(point2.Index);
            return DefineOrb(DefineMundanePointString(mp), cp, aspectDetails);
        }
        if (point1Type == PointTypes.MundaneSpecialPoint && point2Type == PointTypes.CelestialPoint)
        {
            CelPoints cp = _pointMappings.CelPointForIndex(point2.Index);
            MundanePoints mp = _pointMappings.MundanePointForIndex(point1.Index);
            return DefineOrb(DefineMundanePointString(mp), cp, aspectDetails);
        }
        string errorText = "AspectOrbConstructor.DefineOrb encountered unknown combination of points types:  " + point1Type.ToString() + " and " + point2Type.ToString(); 
        Log.Error(errorText);
        throw new ArgumentException(errorText);
    }


    private static string DefineMundanePointString(MundanePoints mundanePoint)
    {
        switch (mundanePoint)
        {
            case MundanePoints.Mc: 
                return "Mc";
            case MundanePoints.Ascendant:
                return "Ascendant";
            case MundanePoints.EastPoint:
                return "EastPoint";
            case MundanePoints.Vertex:
                return "Vertex";
            default:
                string errorText = "AspectOrbConstructor.DefineMundanePointString encountered unrecognized MundanePoint:  " + mundanePoint.ToString();
                Log.Error(errorText);
                throw new ArgumentException(errorText);
        }
    }
}