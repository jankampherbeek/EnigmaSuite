// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.


using E4C.domain.shared.reqresp;
using E4C.Models.Domain;

namespace E4C.calc.seph;

/// <summary>
/// Definitons for flags.
/// </summary>
public interface IFlagDefinitions
{
    /// <summary>
    /// Define flags for a given FullChartRequest.
    /// </summary>
    /// <param name="request">Request for which the flags need to be defined.</param>
    /// <returns>Combined value for flags.</returns>
    public int DefineFlags(FullChartRequest request);

    /// <summary>
    /// Define flags for a given FullMundanePosRequest.
    /// </summary>
    /// <param name="request">Request for which the flags need to be defined.</param>
    /// <returns>Combined value for flags.</returns>
    public int DefineFlags(FullMundanePosRequest request);


    /// <summary>
    /// Changes existing flags to suppport equatorial calculations. 
    /// </summary>
    /// <param name="eclipticFlags"></param>
    /// <returns>Value for equatorial flags.</returns>
    public int AddEquatorial(int eclipticFlags);
}



/// <inheritdoc/>
public class FlagDefinitions : IFlagDefinitions
{
    /// <inheritdoc/>
    public int DefineFlags(FullChartRequest request)
    {
        int flags = Constants.SEFLG_SWIEPH | Constants.SEFLG_SPEED;
        if (request.ObserverPosition == ObserverPositions.HelioCentric)
        {
            flags |= Constants.SEFLG_HELCTR;
        }
        if (request.ObserverPosition == ObserverPositions.TopoCentric)
        {
            flags |= Constants.SEFLG_TOPOCTR;
        }
        if (request.ZodiacType == ZodiacTypes.Sidereal)
        {
            flags |= Constants.SEFLG_SIDEREAL;
        }
        return flags;
    }

    /// <inheritdoc/>
    public int DefineFlags(FullMundanePosRequest request)
    {
        return Constants.SEFLG_SWIEPH;


    }
    /// <inheritdoc/>
    public int AddEquatorial(int eclipticFlags)
    {
        return eclipticFlags | Constants.SEFLG_EQUATORIAL;
    }

}