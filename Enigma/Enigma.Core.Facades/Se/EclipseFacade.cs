// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Runtime.InteropServices;
using System.Text;
using Enigma.Domain.Constants;

namespace Enigma.Facades.Se;

public class EclipseFacade
{

    /// <summary>
    /// Find next solar eclipse
    /// </summary>
    /// <param name="jd">Start JD nr in UT</param>
    /// <returns>JD of next eclipse</returns>
    public double NextSolarEclipse(double jd)
    {
        var tret = new double[10];
        const int ephType = 0;   // Use standard SE
        const int eclipseFlag = 0;  // all types of eclipses
        StringBuilder resultValue = new(256);
        _ = ext_swe_sol_eclipse_when_glob(jd, ephType, eclipseFlag, tret, false, resultValue);
        return tret[0];   // time of maximum eclipse
    }

    /// <summary>
    /// Find next lunar eclipse
    /// </summary>
    /// <param name="jd">Start JD nr in UT</param>
    /// <returns>JD of next eclipse</returns>
    public double NextLunarEclipse(double jd)
    {
        var tret = new double[10];
        const int ephType = 0;   // Use standard SE
        const int eclipseFlag = 0;  // all types of eclipses
        StringBuilder resultValue = new(256);
        _ = ext_swe_lun_eclipse_when(jd, ephType, eclipseFlag, tret, false, resultValue);
        return tret[0];   // time of maximum eclipse
    }
    
    /// <summary>
    /// Access SE dll to calculate next solar eclipse
    /// </summary>
    /// <param name="tjdStart">JD number in UT to start the calculation</param>
    /// <param name="ephType">Always 0, for SE</param>
    /// <param name="eclipseFlag">Combined values for type of eclipse, 0 for all eclipses</param>
    /// <param name="tret">Calculated result, array of 10 doubles</param>
    /// <param name="backwards">True for backwards search, otherwise false</param>
    /// <param name="serr"></param>
    /// <returns></returns>
    [DllImport("swedll64.dll", CharSet = CharSet.Ansi, EntryPoint = "swe_sol_eclipse_when_glob")]
    private static extern int ext_swe_sol_eclipse_when_glob(double tjdStart, int ephType, int eclipseFlag, double[] tret, 
        bool backwards, StringBuilder serr);
    
    /// <summary>
    /// Access SE dll to calculate next lunar eclipse
    /// </summary>
    /// <param name="tjdStart">JD number in UT to start the calculation</param>
    /// <param name="ephType">Always 0, for SE</param>
    /// <param name="eclipseFlag">Combined values for type of eclipse, 0 for all eclipses</param>
    /// <param name="tret">Calculated result, array of 10 doubles</param>
    /// <param name="backwards">True for backwards search, otherwise false</param>
    /// <param name="serr"></param>
    /// <returns></returns>
    [DllImport("swedll64.dll", CharSet = CharSet.Ansi, EntryPoint = "swe_lun_eclipse_when")]
    private static extern int ext_swe_lun_eclipse_when(double tjdStart, int ephType, int eclipseFlag, double[] tret, 
        bool backwards, StringBuilder serr);
    
}
