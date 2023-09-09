// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using System.Linq;
using Enigma.Core.Interfaces;
using Enigma.Domain.Constants;
using Enigma.Domain.Points;

namespace Enigma.Frontend.Ui.Support;

public sealed class PeriodSupportChecker: IPeriodSupportChecker
{

    public bool IsSupported(ChartPoints chartPoint, double jdnr)
    {
        // ReSharper disable once ConvertIfStatementToSwitchStatement
        if (chartPoint == ChartPoints.Chiron) return jdnr is >= EnigmaConstants.PeriodChironStart and < EnigmaConstants.PeriodChironEnd;
        if (chartPoint == ChartPoints.Pholus) return jdnr is >= EnigmaConstants.PeriodPholusStart and < EnigmaConstants.PeriodPholusEnd;

        ChartPoints[] ceresVesta = { ChartPoints.Ceres, ChartPoints.Vesta };
        if (ceresVesta.Contains(chartPoint))
        {
            return jdnr is >= EnigmaConstants.PeriodCeresVestaStart and < EnigmaConstants.PeriodCeresVestaEnd;
        }

        ChartPoints[] nessusAndOthers = {ChartPoints.Nessus, ChartPoints.Huya, ChartPoints.Ixion, ChartPoints.Orcus, ChartPoints.Varuna, ChartPoints.Makemake, ChartPoints.Haumea,
                ChartPoints.Quaoar, ChartPoints.Eris, ChartPoints.Sedna};
        if (nessusAndOthers.Contains(chartPoint)) {
            return jdnr is >= EnigmaConstants.PeriodNessusHuyaEtcStart and < EnigmaConstants.PeriodNessusHuyaEtcEnd;
        }

        return true;
    }

}
