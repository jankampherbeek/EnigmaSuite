// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Core.Handlers.Interfaces;
using Enigma.Domain.Constants;
using Enigma.Domain.Points;

namespace Enigma.Core.Handlers.Calc.CelestialPoints.Helpers;

public sealed class PeriodSupportChecker: IPeriodSupportChecker
{

    public bool IsSupported(ChartPoints chartPoint, double jdnr)
    {
        if (chartPoint == ChartPoints.Chiron) return jdnr is >= EnigmaConstants.PERIOD_CHIRON_START and < EnigmaConstants.PERIOD_CHIRON_END;
        if (chartPoint == ChartPoints.Pholus) return jdnr is >= EnigmaConstants.PERIOD_PHOLUS_START and < EnigmaConstants.PERIOD_PHOLUS_END;

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
