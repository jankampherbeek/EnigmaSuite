// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Analysis;
using Enigma.Domain.Analysis.Aspects;
using Enigma.Domain.Configuration;
using Enigma.Domain.Enums;
using Enigma.Domain.Points;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Support;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace Enigma.Frontend.Ui.Configuration;

public class AstroConfigController
{
    private readonly IConfigurationApi _configApi;
    private readonly AstroConfig _astroConfig;


    public AstroConfigController(IConfigurationApi configApi)
    {
        _configApi = configApi;
        _astroConfig = CurrentConfig.Instance.GetConfig();

    }

    public string DefineGlyph(CelPoints point)
    {
        return point.GetDetails().DefaultGlyph;
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

    public string DefineGlyph(ArabicPoints point)
    {
        return point.GetDetails().DefaultGlyph;
    }

    public string DefineGlyph(AspectTypes aspect)
    {
        return aspect.GetDetails().Glyph;
    }

    public string DefineGlyph(ZodiacPoints point)
    {
        return point.GetDetails().DefaultGlyph;
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