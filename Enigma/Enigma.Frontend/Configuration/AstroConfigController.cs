// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Api.Interfaces;
using Enigma.Domain.Configuration;
using Enigma.Domain.Points;
using Enigma.Frontend.Helpers.Support;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Support;
using Microsoft.Extensions.DependencyInjection;

using System.Windows;

namespace Enigma.Frontend.Ui.Configuration;

public sealed class AstroConfigController
{
    private readonly IConfigurationApi _configApi;
    private readonly AstroConfig _astroConfig;
    private readonly GlyphsForChartPoints _glyphsForChartPoints;


    public AstroConfigController(IConfigurationApi configApi)
    {
        _configApi = configApi;
        _astroConfig = CurrentConfig.Instance.GetConfig();
        _glyphsForChartPoints = new();
    }

    public char DefineGlyph(ChartPoints point)
    {
        return _glyphsForChartPoints.FindGlyph(point);
    }

    public AstroConfig GetConfig()
    {
        return _astroConfig;
    }

    public void UpdateConfig(AstroConfig astroConfig)
    {
        _configApi.WriteConfig(astroConfig);
        CurrentConfig.Instance.ChangeConfig(astroConfig);
        DataVault.Instance.ClearExistingCharts();
    }

    public static void ShowHelp()
    {
        HelpWindow helpWindow = App.ServiceProvider.GetRequiredService<HelpWindow>();
        helpWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        helpWindow.SetHelpPage("Configurations");
        helpWindow.ShowDialog();
    }
}