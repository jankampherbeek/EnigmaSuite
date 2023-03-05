// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Calc.ChartItems;
using Enigma.Domain.Exceptions;
using Serilog;
using System.Runtime.InteropServices;

namespace Enigma.Facades.Se;

/// <summary>
/// Initializer for the Swiss Ephemeris. Check the comments for the methods as they are partly required to run before using the CommonSE.
/// </summary>
public static class SeInitializer
{
    /// <summary>Set location for Swiss Ephemeris files.</summary>
    /// <param name="path">Location, relative to the program.</param>
    /// <remarks>This method must run before the CommonSE can be used.</remarks>
    public static void SetEphePath(String path)
    {
        if (path != null)
        {
            try
            {
                ext_swe_set_ephe_path(path);
            }
            catch (DllNotFoundException)
            {
                Log.Error("SeInitializer could not find swedll64.dll. Throwing SwissEphException which should terminate the program.");
                throw new SwissEphException("The swedll64.dll, which is an essential part of the Swiss Ephemeris, could not be found.");
            }
        }

    }
    [DllImport("swedll64.dll", CharSet = CharSet.Unicode, EntryPoint = "swe_set_ephe_path")]
    private extern static void ext_swe_set_ephe_path(String path);

    public static void SetTopocentric(double geoLong, double geoLat, double altitudeMeters)
    {
        ext_swe_set_topo(geoLong, geoLat, altitudeMeters);
    }
    [DllImport("swedll64.dll", CharSet = CharSet.Unicode, EntryPoint = "swe_set_topo")]
    private extern static void ext_swe_set_topo(double geoLong, double geoLatr, double altitudeMeters);


    /// <summary>Close Swiss Ephemeris and release all allocated memory and resources.</summary>
    /// <remarks>Use ony at end of application or test. To reuse, call SetEphePath().</remarks>
    public static void CloseEphemeris()
    {
        ext_swe_close();
    }
    [DllImport("swedll64.dll", CharSet = CharSet.Unicode, EntryPoint = "swe_close")]
    private extern static void ext_swe_close();



    /// <summary>Define Ayanamsha for calculation of sidereal positions.</summary>
    /// <param name="idAyanamsha">The id for the Ayanamsha as used by the CommonSE.</param>
    /// <remarks>Run this method if sidereal calculations will be used. If this method has not run during the current session, Fagan/Bradley is used as default ayanamsha.
    /// The method from the CommonSE dll is called using parameters t0 and t1 with the value 0, these will be ignored for all prdefined ayanamsha's.</remarks>
    public static void SetAyanamsha(int idAyanamsha)
    {
        if (idAyanamsha >= -1 && idAyanamsha <= 39)
        {
            ext_swe_set_sid_mode(idAyanamsha, 0, 0);
        }

    }
    [DllImport("swedll64.dll", CharSet = CharSet.Unicode, EntryPoint = "swe_set_sid_mode")]
    private extern static void ext_swe_set_sid_mode(int idAyanamsha, int t0, int t1);

    public static void SetAyanamsha(Ayanamshas fagan)
    {
        throw new NotImplementedException();
    }
}
