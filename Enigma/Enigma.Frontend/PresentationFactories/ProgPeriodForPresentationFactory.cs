// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using System.Linq;
using Enigma.Domain.Progressive;
using Enigma.Frontend.Ui.Interfaces;

namespace Enigma.Frontend.Ui.PresentationFactories;

/// <inheritdoc/>
public class ProgPeriodForPresentationFactory: IProgPeriodForPresentationFactory
{
    /// <inheritdoc/>
    public List<PresentableProgresData> CreatePresentableProgresData(IEnumerable<ProgPeriod> progPeriods)
    {
        return (from actPeriod in progPeriods 
            let id = actPeriod.Id.ToString() 
            let description = actPeriod.Description 
            select new PresentableProgresData(id, description)).ToList();
    }
}