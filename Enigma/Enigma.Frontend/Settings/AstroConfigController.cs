// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Configuration.Handlers;
using Enigma.Domain.Analysis;
using Enigma.Domain.Configuration;
using Enigma.Domain.Enums;
using Enigma.Domain.Interfaces;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Support;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace Enigma.Frontend.Ui.Settings;

public class AstroConfigController
{
    private readonly ISolarSystemPointSpecifications _solarSystemPointSpecifications;
    private readonly IAspectSpecifications _aspectSpecifications;
    private readonly IConfigWriter _configWriter;
    private readonly AstroConfig _astroConfig;

    public ISolarSystemPointSpecifications SolarSystemPointSpecifications => _solarSystemPointSpecifications;

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
        return SolarSystemPointSpecifications.DetailsForPoint(point).DefaultGlyph;
    }

    public string DefineGlyph(MundanePoints point)
    {
        return point switch
        {
            MundanePoints.Mc => "M",
            MundanePoints.Ascendant => "A",
            MundanePoints.Vertex => "",
            MundanePoints.EastPoint => "",
            _ => throw new ArgumentException("Wrong value for mundane points when defining glyph."),
        };
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
        HelpWindow helpWindow = App.ServiceProvider.GetRequiredService<HelpWindow>();
        helpWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        helpWindow.SetHelpPage("Configurations");
        helpWindow.ShowDialog();
    }
}