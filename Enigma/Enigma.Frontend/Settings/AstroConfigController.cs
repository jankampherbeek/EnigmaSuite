// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.CalcVars;

namespace Enigma.Frontend.Settings;

public class AstroConfigController
{
    private ISolarSystemPointSpecifications _solarSystemPointSpecifications;


    public AstroConfigController(ISolarSystemPointSpecifications solarSystemPointSpecifications)
    {
        _solarSystemPointSpecifications = solarSystemPointSpecifications;
    }

    public string DefineGlyph(SolarSystemPoints point)
    {
        return _solarSystemPointSpecifications.DetailsForPoint(point).DefaultGlyph;
    }

}