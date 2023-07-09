// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Core.Handlers.Interfaces;
using Enigma.Domain.Constants;
using Enigma.Domain.Points;

namespace Enigma.Frontend.Ui.Support;

public sealed class PeriodSupportChecker: IPeriodSupportChecker
{

    public bool IsSupported(ChartPoints chartPoint, double jdnr)
    {
        if (chartPoint == ChartPoints.Chiron) return jdnr >= EnigmaConstants.PeriodChironStart && jdnr < EnigmaConstants.PeriodChironEnd;
        if (chartPoint == ChartPoints.Pholus) return jdnr >= EnigmaConstants.PeriodPholusStart && jdnr < EnigmaConstants.PeriodPholusEnd;

        ChartPoints[] ceresVesta = { ChartPoints.Ceres, ChartPoints.Vesta };
        if (ceresVesta.Contains(chartPoint))
        {
            return jdnr >= EnigmaConstants.PeriodCeresVestaStart && jdnr < EnigmaConstants.PeriodCeresVestaEnd;
        }

        ChartPoints[] nessusAndOthers = {ChartPoints.Nessus, ChartPoints.Huya, ChartPoints.Ixion, ChartPoints.Orcus, ChartPoints.Varuna, ChartPoints.Makemake, ChartPoints.Haumea,
                ChartPoints.Quaoar, ChartPoints.Eris, ChartPoints.Sedna};
        if (nessusAndOthers.Contains(chartPoint)) {
            return jdnr >= EnigmaConstants.PeriodNessusHuyaEtcStart && jdnr < EnigmaConstants.PeriodNessusHuyaEtcEnd;
        }

        return true;
    }

}
