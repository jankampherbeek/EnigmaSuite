// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Analysis;

namespace Enigma.Core.Analysis.Midpoints;

/// <summary>
/// Construct an orb for the calculation of midpoints.
/// </summary>
public interface IMidpointOrbConstructor
{
    /// <summary>
    /// Define orb for specific midpoint dial type.
    /// </summary>
    /// <param name="midpointDetails">midpoint</param>
    /// <returns>Orb.</returns>
    public double DefineOrb(MidpointDetails midpointDetails);
}

/// <inheritdoc/>
public class MidpointOrbConstructor : IMidpointOrbConstructor
{

    private readonly double _baseOrb = 1.6;   // todo make baseorb configurable

    /// <inheritdoc/>
    public double DefineOrb(MidpointDetails midpointDetails)
    {
        return _baseOrb * midpointDetails.OrbFactor;
    }

}