// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using System.Linq;
using Enigma.Domain.Dtos;
using Enigma.Domain.Presentables;

namespace Enigma.Frontend.Ui.PresentationFactories;

/// <summary>Prepare event to be shown in UI.</summary>
public interface IProgDatesForPresentationFactory
{
    /// <summary>Builds a presentable version of progressive dates to be shown in the UI.</summary>
    /// <param name="progDates">The dates to convert.</param>
    /// <returns>Presentable progressive dates.</returns>
    public List<PresentableProgresData> CreatePresentableProgresData(IEnumerable<ProgDates> progDates);
}

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