// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Analysis;
using Enigma.Domain.CalcVars;
using System;
using System.Collections;

namespace Enigma.Frontend.Settings;

public class AstroConfigController
{
    private ISolarSystemPointSpecifications _solarSystemPointSpecifications;
    private IAspectSpecifications _aspectSpecifications;

    public AstroConfigController(ISolarSystemPointSpecifications solarSystemPointSpecifications,
        IAspectSpecifications aspectSpecifications)
    {
        _solarSystemPointSpecifications = solarSystemPointSpecifications;
        _aspectSpecifications = aspectSpecifications;
    }

    public string DefineGlyph(SolarSystemPoints point)
    {
        return _solarSystemPointSpecifications.DetailsForPoint(point).DefaultGlyph;
    }

    public string DefineGlyph(MundanePoints point)
    {
        switch (point)
        {
            case MundanePoints.Mc: 
                return "M"; 
            case MundanePoints.Ascendant:
                return "A";
            case MundanePoints.Vertex:
                return "";
            case MundanePoints.EastPoint:
                return "";
            default:
                throw new ArgumentException("Wrong value for mundane points when defining glyph.");
        }
    }

    public string DefineGlyph(AspectTypes aspect)
    {
        return _aspectSpecifications.DetailsForAspect(aspect).Glyph;
    }
}