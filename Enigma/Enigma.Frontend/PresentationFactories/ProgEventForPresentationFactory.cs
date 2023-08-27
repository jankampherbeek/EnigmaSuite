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
public class ProgEventForPresentationFactory: IProgEventForPresentationFactory
{

    /// <inheritdoc/>
    public List<PresentableProgresData> CreatePresentableProgresData(IEnumerable<ProgEvent> progEvents)
    {
        return (from actEvent in progEvents 
            let id = actEvent.Id.ToString() 
            let description = actEvent.Description 
            select new PresentableProgresData(id, description)).ToList();
    }
}