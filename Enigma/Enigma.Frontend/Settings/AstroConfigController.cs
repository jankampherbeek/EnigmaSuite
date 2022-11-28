// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

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
    private readonly ICelPointSpecifications _celPointSpecifications;
    private readonly IAspectSpecifications _aspectSpecifications;
    private readonly IConfigurationApi _configApi;
    private readonly AstroConfig _astroConfig;

    public ICelPointSpecifications CelPointSpecifications => _celPointSpecifications;

    public AstroConfigController(ICelPointSpecifications celPointSpecifications,
        IAspectSpecifications aspectSpecifications,
        IConfigurationApi configApi)
    {
        _celPointSpecifications = celPointSpecifications;
        _aspectSpecifications = aspectSpecifications;
        _configApi = configApi;
        _astroConfig = CurrentConfig.Instance.GetConfig();

    }

    public string DefineGlyph(CelPoints point)
    {
        return CelPointSpecifications.DetailsForPoint(point).DefaultGlyph;
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
        _configApi.WriteConfig(astroConfig);
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