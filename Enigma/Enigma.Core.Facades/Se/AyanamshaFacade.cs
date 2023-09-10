// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Diagnostics.CodeAnalysis;
using Enigma.Domain.Constants;
using Enigma.Domain.Exceptions;
using Enigma.Facades.Interfaces;
using Serilog;
using System.Runtime.InteropServices;
using System.Text;

namespace Enigma.Facades.Se;



[SuppressMessage("Globalization", "CA2101:Specify marshaling for P/Invoke string arguments")]
public class AyanamshaFacade : IAyanamshaFacade
{
    public double GetAyanamshaOffset(double jdUt)
    {
        const int epheFlag = EnigmaConstants.SEFLG_SWIEPH; // Value not parameterized as Enigma always uses this approach for calculations.
        double ayanamshaValue = 0.0;
        StringBuilder serr = new(256);
        long result = ext_swe_get_ayanamsa_ex_ut(jdUt, epheFlag, ref ayanamshaValue, serr);
        if (result >= 0) return ayanamshaValue;
        Log.Error("AyanamshaFacade.GetAyanamsha(). Error {Result} when calculating ayanamsha for jdUt {JdUt}. Errormessage from SE: {Serr}",
            result, jdUt, serr);
        throw new EnigmaException("Error in AyanamshaFacade.GetAyanamsha()");
    }

    [DllImport("swedll64.dll", CharSet = CharSet.Ansi, EntryPoint = "swe_get_ayanamsa_ex_ut")]
    private static extern int ext_swe_get_ayanamsa_ex_ut(double jdUt, int epheFlag, ref double ayanamshaValue, StringBuilder serr);

}
