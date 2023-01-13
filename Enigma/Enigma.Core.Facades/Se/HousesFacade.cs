// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Facades.Se.Interfaces;
using Enigma.Domain.Exceptions;
using System.Runtime.InteropServices;

namespace Enigma.Facades.Se;


/// <inheritdoc/>
public class HousesFacade : IHousesFacade
{
    /// <inheritdoc/>
    /// <remarks>Throws a SwissEphException if the CelPointSE returns an error.</remarks>
    public double[][] RetrieveHouses(double jdUt, int flags, double geoLat, double geoLon, char houseSystem)
    {
        int _nrOfCusps = houseSystem == 'G' ? 37 : 13;
        double[] cusps = new double[_nrOfCusps];
        double[] mundanePoints = new double[10];

        int result = ext_swe_houses_ex(jdUt, flags, geoLat, geoLon, houseSystem, cusps, mundanePoints);
        if (result < 0)
        {
            string paramsSummary = string.Format("jdUt: {0}, flags: {1}, geoLat: {2}, geoLon: {3}, houseSystem: {4}.", jdUt, flags, geoLat, geoLon, houseSystem);
            throw new SwissEphException(string.Format("{0}/{1}/{2}", result, "SePosHousesFacade.PosHousesFromSe", paramsSummary));
        }

        double[][] positions = { cusps, mundanePoints };
        return positions;

    }
    [DllImport("swedll64.dll", CharSet = CharSet.Ansi, EntryPoint = "swe_houses_ex")]
    private extern static int ext_swe_houses_ex(double tjdut, int flags, double geolat, double geolon, int hsys, double[] hcusp0, double[] ascmc0);
}