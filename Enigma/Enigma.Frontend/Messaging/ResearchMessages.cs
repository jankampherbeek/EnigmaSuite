// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using CommunityToolkit.Mvvm.Messaging.Messages;
using Enigma.Domain.Dtos;

namespace Enigma.Frontend.Ui.Messaging;

// Messages that are specific for the Research module..

public class ResearchPointSelectionMessage: ValueChangedMessage<ResearchPointSelection>
{
        public ResearchPointSelectionMessage(ResearchPointSelection selection) : base(selection)
        {
        }
}

