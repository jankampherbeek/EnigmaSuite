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
public class ProgDatesForPresentationFactory: IProgDatesForPresentationFactory
{

    /// <inheritdoc/>
    public List<PresentableProgresData> CreatePresentableProgresData(IEnumerable<ProgDates> progDates)
    {

        return (from actualDate in progDates
            let dateType = actualDate is ProgEvent ? "p" : "e" 
            let id = actualDate.Id.ToString() 
            let description = actualDate.Description 
            select new PresentableProgresData(dateType,id, description)).ToList();
    }
}