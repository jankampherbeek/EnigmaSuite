// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;
using System.Collections.Generic;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Support;

namespace Enigma.Frontend.Ui.Models;

public class PreNatalAspectSelectionModel
{

    public int MinimalNrOfAspects = 1;
    private AstroConfig? _astroConfig;
    private List<SelectableAspectDetails> _selAspDetails = new();
    

    public List<SelectableAspectDetails> GetAllAspectDetails()
    {
        
        _astroConfig = CurrentConfig.Instance.GetConfig();
        _selAspDetails = new List<SelectableAspectDetails>();
        foreach (KeyValuePair<AspectTypes, AspectConfigSpecs?> currentAspSpec in _astroConfig.Aspects)
        {
            if (!currentAspSpec.Value.IsUsed) continue;
            AspectDetails aspDetails = currentAspSpec.Key.GetDetails();
            char glyph = currentAspSpec.Value.Glyph;
            var aspName = Rosetta.Instance.GetText(aspDetails.RbKey);
            _selAspDetails.Add(new SelectableAspectDetails() { Selected = false, Aspect = aspDetails.Aspect, Glyph = glyph, Name = aspName});
        }
        return _selAspDetails;
    }
}