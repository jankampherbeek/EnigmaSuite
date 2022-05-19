// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Runtime.InteropServices;

namespace Enigma.Core.Calc.SeFacades;

/// <summary>
/// Initializer for the Swiss Ephemeris. Check the comments for the methods as they are partly required to run before using the SE.
/// </summary>
public class SeInitializer
{
    /// <summary>Set location for Swiss Ephemeris files.</summary>
    /// <param name="path">Location, relative to the program.</param>
    /// <remarks>This method must run before the SE can be used.</remarks>
    public static void SetEphePath(String path)
    {
        ext_swe_set_ephe_path(path);
    }
    [DllImport("swedll64.dll", CharSet = CharSet.Unicode, EntryPoint = "swe_set_ephe_path")]
    private extern static void ext_swe_set_ephe_path(String path);

    /// <summary>Close Swiss Ephemeris and release all allocated memory and resources.</summary>
    /// <remarks>Use ony at end of application or test. To resuse, call SetEphePath().</remarks>
    public static void CloseEphemeris()
    {
        ext_swe_close();
    }
    [DllImport("swedll64.dll", CharSet = CharSet.Unicode, EntryPoint = "swe_close")]
    private extern static void ext_swe_close();



    /// <summary>Define Ayanamsha for calculation of sidereal positions.</summary>
    /// <param name="idAyanamsha">The id for the Ayanamsha as used by the SE.</param>
    /// <remarks>Run this method if sidereal calculations will be used. If this method has not run during the current session, Fagan/Bradley is used as default ayanamsha.</remarks>
    public static void SetAyanamsha(int idAyanamsha)
    {
        ext_swe_set_sid_mode(idAyanamsha);
    }
    [DllImport("swedll64.dll", CharSet = CharSet.Unicode, EntryPoint = "swe_set_sid_mode")]
    private extern static void ext_swe_set_sid_mode(int idAyanamsha);
}
