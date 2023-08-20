// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using Enigma.Domain.Progressive;

namespace Enigma.Frontend.Ui.Models;

public class ProgressiveMainModel
{
    public List<PresentableProgresData> AvailableEvents { get; set; }
    public List<PresentableProgresData> AvailablePeriods { get; set; }
}