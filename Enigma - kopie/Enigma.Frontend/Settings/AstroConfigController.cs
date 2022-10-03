// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Configuration;
using Enigma.Configuration.Handlers;
using Enigma.Domain.Analysis;
using Enigma.Domain.CalcVars;
using Enigma.Frontend.State;
using Enigma.Frontend.Support;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace Enigma.Frontend.Settings;

public class AstroConfigController
{
    private ISolarSystemPointSpecifications _solarSystemPointSpecifications;
    private IAspectSpecifications _aspectSpecifications;
    private IConfigWriter _configWriter;
    private AstroConfig _astroConfig;

    public AstroConfigController(ISolarSystemPointSpecifications solarSystemPointSpecifications,
        IAspectSpecifications aspectSpecifications,
        IConfigWriter configWriter)
    {
        _solarSystemPointSpecifications = solarSystemPointSpecifications;
        _aspectSpecifications = aspectSpecifications;
        _configWriter = configWriter;
        _astroConfig = CurrentConfig.Instance.GetConfig();

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

    public AstroConfig GetConfig()
    {
        return _astroConfig;
    }

    public void UpdateConfig(AstroConfig astroConfig)
    {
        _configWriter.WriteChangedConfig(astroConfig);
        CurrentConfig.Instance.ChangeConfig(astroConfig);
    }

    public void ShowHelp()
    {
        HelpWindow? helpWindow = App.ServiceProvider.GetService<HelpWindow>();
        if (helpWindow != null)
        {
            helpWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            helpWindow.SetHelpPage("Configurations");
            helpWindow.ShowDialog();
        }
    }
}