// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers.Interfaces;
using Enigma.Domain.Analysis.Aspects;
using Enigma.Domain.Interfaces;
using Enigma.Domain.Points;
using Serilog;

namespace Enigma.Core.Handlers.Analysis.Helpers;


// TODO 0.1 Analysis 
public sealed class AspectOrbConstructor : IAspectOrbConstructor
{

    private readonly double _baseOrb = 10.0;   // todo make baseorb configurable

    private readonly IOrbDefinitions _orbDefinitions;

    public AspectOrbConstructor(IOrbDefinitions orbDefinitions)
    {
        _orbDefinitions = orbDefinitions;
    }

    /// <inheritdoc/>
    public double DefineOrb(ChartPoints point1, ChartPoints point2, AspectDetails aspectDetails)
    {
        double factor1 = _orbDefinitions.DefineChartPointOrb(point1).OrbFactor;
        double factor2 = _orbDefinitions.DefineChartPointOrb(point2).OrbFactor;
        double aspectFactor = aspectDetails.OrbFactor;
        return Math.Max(factor1, factor2) * aspectFactor * _baseOrb;
    }


    /// <inheritdoc/>
    /*
        public double DefineOrb(ChartPoints point1, ChartPoints point2, AspectDetails aspectDetails)

        {
             PointTypes point1Type = point1.PointType;
                   PointTypes point2Type = point2.PointType;
                 if (point1Type == PointTypes.CelestialPoint && point2Type == PointTypes.CelestialPoint)
                   {
                       ChartPoints cp1 = _pointMappings.CelPointForIndex(point1.Index);
                       ChartPoints cp2 = _pointMappings.CelPointForIndex(point2.Index);
                       return DefineOrb(cp1, cp2, aspectDetails);
                   }
                   if (point1Type == PointTypes.CelestialPoint && point2Type == PointTypes.MundaneSpecialPoint)
                   {
                       ChartPoints cp = _pointMappings.CelPointForIndex(point1.Index);
                       MundanePoints mp = _pointMappings.MundanePointForIndex(point2.Index);
                       return DefineOrb(DefineMundanePointString(mp), cp, aspectDetails);
                   }
                   if (point1Type == PointTypes.MundaneSpecialPoint && point2Type == PointTypes.CelestialPoint)
                   {
                       ChartPoints cp = _pointMappings.CelPointForIndex(point2.Index);
                       MundanePoints mp = _pointMappings.MundanePointForIndex(point1.Index);
                       return DefineOrb(DefineMundanePointString(mp), cp, aspectDetails);
                   }

            string errorText = "AspectOrbConstructor.DefineOrb encountered unknown combination of points types:  " + point1Type.ToString() + " and " + point2Type.ToString(); 

            string errorText = string.Empty;
            Log.Error(errorText);
            throw new ArgumentException(errorText);

}
                        */


    private static string DefineMundanePointString(ChartPoints mundanePoint)
    {
        switch (mundanePoint)
        {
            case ChartPoints.Mc:
                return "Mc";
            case ChartPoints.Ascendant:
                return "Ascendant";
            case ChartPoints.EastPoint:
                return "EastPoint";
            case ChartPoints.Vertex:
                return "Vertex";
            default:
                string errorText = "AspectOrbConstructor.DefineMundanePointString encountered unrecognized MundanePoint:  " + mundanePoint.ToString();
                Log.Error(errorText);
                throw new ArgumentException(errorText);
        }
    }
}