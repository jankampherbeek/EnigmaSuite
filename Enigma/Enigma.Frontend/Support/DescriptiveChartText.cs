// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Calc.ChartItems;
using Enigma.Domain.Charts;
using Enigma.Domain.Configuration;
using Enigma.Frontend.Ui.Interfaces;
using System.Text;

namespace Enigma.Frontend.Ui.Support;

public class DescriptiveChartText : IDescriptiveChartText
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
        constructedText.Append(chartData.Location.LocationFullName.Replace("[N]", "Unknown").Replace("[S]", "Unknnown").Replace("[E]", "Unknown")).Replace("[W]", "Unknown");
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
        return constructedText + "\n";
    }

    private static string ConfigText(AstroConfig config)
    {
        StringBuilder constructedText = new();
        constructedText.Append(config.HouseSystem.GetDetails().Text);
        constructedText.Append(' ');
        constructedText.Append(config.ZodiacType.GetDetails().Text);
        constructedText.Append(' ');
        if (config.ZodiacType == ZodiacTypes.Sidereal)
        {
            constructedText.Append(config.Ayanamsha.GetDetails().Text);
            constructedText.Append(' ');
        }
        constructedText.Append(config.ObserverPosition.GetDetails().Text);
        constructedText.Append(' ');
        constructedText.Append(config.ProjectionType.GetDetails().Text);
        return constructedText + "\n";
    }
}