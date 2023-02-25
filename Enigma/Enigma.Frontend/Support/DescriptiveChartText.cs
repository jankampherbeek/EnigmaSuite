// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Calc.ChartItems;
using Enigma.Domain.Charts;
using Enigma.Domain.Configuration;
using Enigma.Frontend.Helpers.Support;
using Enigma.Frontend.Ui.Interfaces;
using System.Text;

namespace Enigma.Frontend.Ui.Support;

public class DescriptiveChartText: IDescriptiveChartText
{

    public string ShortDescriptiveText(AstroConfig config, MetaData meta)
    {
        StringBuilder constructedText = new();
        constructedText.Append(NameDescription(meta));
        constructedText.Append(ConfigText(config));
        return constructedText.ToString();

    }

    public string FullDescriptiveText(AstroConfig config, ChartData chartData)
    {
        StringBuilder constructedText = new();
        constructedText.Append(NameDescription(chartData.MetaData));
        constructedText.Append(chartData.FullDateTime.DateText);
        constructedText.Append(' ');
        constructedText.Append(chartData.FullDateTime.TimeText);
        constructedText.Append(' ');
        constructedText.Append(chartData.Location.LocationFullName.Replace("[N]", Rosetta.TextForId("")).Replace("[S]", Rosetta.TextForId("")).Replace("[E]", Rosetta.TextForId("")).Replace("[W]", Rosetta.TextForId("")));
        constructedText.Append('\n');
        constructedText.Append(ConfigText(config));
        return constructedText.ToString();
    }

    private static string NameDescription(MetaData meta)
    {
        StringBuilder constructedText = new();
        constructedText.Append(meta.Name);
        constructedText.Append(", ");
        constructedText.Append(meta.Description);
        constructedText.Append(' ');
        return constructedText.ToString() + "\n";
    }

    private static string ConfigText(AstroConfig config)
    {
        StringBuilder constructedText = new();
        constructedText.Append(Rosetta.TextForId(config.HouseSystem.GetDetails().TextId));
        constructedText.Append(' ');
        constructedText.Append(Rosetta.TextForId(config.ZodiacType.GetDetails().TextId));
        constructedText.Append(' ');
        if (config.ZodiacType == ZodiacTypes.Sidereal)
        {
            constructedText.Append(Rosetta.TextForId(config.Ayanamsha.GetDetails().TextId));
            constructedText.Append(' ');
        }
        constructedText.Append(Rosetta.TextForId(config.ObserverPosition.GetDetails().TextId));
        constructedText.Append(' ');
        constructedText.Append(Rosetta.TextForId(config.ProjectionType.GetDetails().TextId));
        return constructedText.ToString() + "\n";
    }
}