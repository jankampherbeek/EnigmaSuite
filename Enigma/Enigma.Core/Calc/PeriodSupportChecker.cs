// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Constants;
using Enigma.Domain.References;

namespace Enigma.Core.Calc;

/// <summary>Support supported period for ChartPoints.</summary>
public interface IPeriodSupportChecker
{
    /// <summary>Checks if the calculation of a ChartPoint for a specific date is supported.</summary>
    /// <param name="chartPoint">The chart point to check.</param>
    /// <param name="jdnr">The julian day number for the given date.</param>
    /// <returns>True if the ChartPoint can be calculated for the given date, otherwise false.</returns>
    public bool IsSupported(ChartPoints chartPoint, double jdnr);
}

public sealed class PeriodSupportChecker: IPeriodSupportChecker
{

    public bool IsSupported(ChartPoints chartPoint, double jdnr)
    {
        switch (chartPoint)
        {
            case ChartPoints.Chiron:
                return jdnr is >= EnigmaConstants.PERIOD_CHIRON_START and < EnigmaConstants.PERIOD_CHIRON_END;
            case ChartPoints.Pholus:
                return jdnr is >= EnigmaConstants.PERIOD_PHOLUS_START and < EnigmaConstants.PERIOD_PHOLUS_END;
        }

        ChartPoints[] ceresVesta = { ChartPoints.Ceres, ChartPoints.Vesta };
        if (ceresVesta.Contains(chartPoint))
        {
            return jdnr is >= EnigmaConstants.PERIOD_CERES_VESTA_START and < EnigmaConstants.PERIOD_CERES_VESTA_END;
        }

        ChartPoints[] nessusAndOthers = {ChartPoints.Nessus, ChartPoints.Huya, ChartPoints.Ixion, ChartPoints.Orcus, ChartPoints.Varuna, ChartPoints.Makemake, ChartPoints.Haumea,
                ChartPoints.Quaoar, ChartPoints.Eris, ChartPoints.Sedna};
        if (nessusAndOthers.Contains(chartPoint)) {
            return jdnr is >= EnigmaConstants.PERIOD_NESSUS_HUYA_ETC_START and < EnigmaConstants.PERIOD_NESSUS_HUYA_ETC_END;
        }

        return true;
    }

}
