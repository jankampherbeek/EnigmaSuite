// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.Constants;
using Enigma.Domain.RequestResponse;

namespace Enigma.Domain.Charts;
public class JulDayResult
{
    public string JulDayUtText { get; }
    public string JulDayEtText { get; }
    public string DeltaTTextInDays { get; }
    public string DeltaTTextInSeconds { get; }
    public bool NoErrors { get; }
    public readonly string ErrorText = "- Error -";

    public JulDayResult(JulianDayResponse response)
    {
        if (response.Success)
        {
            NoErrors = true;
            JulDayUtText = response.JulDayUt.ToString().Replace(',', '.');
            JulDayEtText = response.JulDayEt.ToString().Replace(',', '.');
            DeltaTTextInSeconds = (response.DeltaT * EnigmaConstants.SECONDS_PER_DAY).ToString().Replace(',', '.');
            DeltaTTextInDays = response.DeltaT.ToString().Replace(',', '.');
        }
        else
        {
            NoErrors = false;
            JulDayUtText = ErrorText;
            JulDayEtText = ErrorText;
            DeltaTTextInSeconds = ErrorText;
            DeltaTTextInDays = ErrorText;
        }

    }
}