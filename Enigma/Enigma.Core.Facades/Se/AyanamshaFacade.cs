// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Constants;
using Enigma.Domain.Exceptions;
using Enigma.Facades.Interfaces;
using Serilog;
using System.Runtime.InteropServices;
using System.Text;

namespace Enigma.Facades.Se;



public class AyanamshaFacade : IAyanamshaFacade
{
    public double GetAyanamshaOffset(double jdUt)
    {
        int epheFlag = EnigmaConstants.SeflgSwieph;    // Value not parameterized as Enigma always uses this approach for calculations.
        double ayanamshaValue = 0.0;
        StringBuilder serr = new(256);
        long result = ext_swe_get_ayanamsa_ex_ut(jdUt, epheFlag, ref ayanamshaValue, serr);
        if (result < 0)
        {
            string errorTxt = "AyanamshaFacade.GetAyanamsha(). Error " + result + " when calculating ayanamsha for jdUt " + jdUt + " . Errormessage from SE: " + serr.ToString();
            Log.Error(errorTxt);
            throw new EnigmaException(errorTxt);
        }
        return ayanamshaValue;
    }

    [DllImport("swedll64.dll", CharSet = CharSet.Ansi, EntryPoint = "swe_get_ayanamsa_ex_ut")]
    private extern static int ext_swe_get_ayanamsa_ex_ut(double jdUt, int epheFlag, ref double ayanamshaValue, StringBuilder serr);

}
