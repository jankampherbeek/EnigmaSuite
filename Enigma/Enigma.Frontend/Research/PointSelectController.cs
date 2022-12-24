// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.Configuration;
using Enigma.Domain.Enums;
using Enigma.Frontend.Helpers.Support;
using Enigma.Frontend.Ui.State;
using System.Collections.Generic;

namespace Enigma.Frontend.Ui.Research;
public sealed class PointSelectController
{


    private AstroConfig? _astroConfig;
    private readonly Rosetta _rosetta = Rosetta.Instance;
    private List<SelectableCelPointDetails> _selCPDetails = new();
    private List<SelectableMundanePointDetails> _selMPDetails = new();


    public List<SelectableCelPointDetails> GetAllCelPointDetails()
    {
        DefineCelPoints();
        return _selCPDetails;
    }

    public List<SelectableMundanePointDetails> GetAllMundanePointDetails()
    {
        DefineMundanePoints();
        return _selMPDetails;
    }

    public class SelectableCelPointDetails
    {
        public CelPoints CelPoint { get; set; }
        public string? Glyph { get; set; }
        public string? Name { get; set; }
    }

    public class SelectableMundanePointDetails
    {
        public MundanePoints MundanePoint { get; set; }
        public string? Name { get; set; }
    }


    private void DefineCelPoints()
    {
        string resourceBundlePrefix = "ref.enum.celpoint.";
        _astroConfig = CurrentConfig.Instance.GetConfig();
        _selCPDetails = new();
        foreach (CelPointSpecs currentCPSpec in _astroConfig.CelPoints)
        {
            if (currentCPSpec.IsUsed)
            {
                CelPointDetails cpDetails = currentCPSpec.CelPoint.GetDetails();
                _selCPDetails.Add(new SelectableCelPointDetails() { CelPoint = cpDetails.CelPoint, Glyph = cpDetails.DefaultGlyph, Name = _rosetta.TextForId(resourceBundlePrefix + cpDetails.TextId) });
            }
        }
    }

    private void DefineMundanePoints()
    {
        _astroConfig = CurrentConfig.Instance.GetConfig();
        _selMPDetails = new();
        foreach (MundanePointSpecs currentMPSpec in _astroConfig.MundanePoints)
        {
            if (currentMPSpec.IsUsed)
            {
                MundanePointDetails mpDetails = currentMPSpec.MundanePoint.GetDetails();
                _selMPDetails.Add(new SelectableMundanePointDetails() { MundanePoint = mpDetails.MundanePoint, Name = _rosetta.TextForId(mpDetails.TextId) });
            }
        }
    }

}