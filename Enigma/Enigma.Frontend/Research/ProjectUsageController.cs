// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.AstronCalculations;
using Enigma.Domain.Configuration;
using Enigma.Domain.Enums;
using Enigma.Domain.Research;
using Enigma.Frontend.Helpers.Interfaces;
using Enigma.Frontend.Helpers.Support;
using Enigma.Frontend.Ui.State;
using System.Collections.Generic;

namespace Enigma.Frontend.Ui.Research;


public class ProjectUsageController
{
    private readonly AstroConfig _currentAstroConfig;
    private readonly IDoubleToDmsConversions _doubleToDmsConversions;
    private readonly Rosetta _rosetta = Rosetta.Instance;
    private ResearchProject _currentProject;

    public ProjectUsageController(IDoubleToDmsConversions doubleToDmsConversions)
    {
        _currentAstroConfig = CurrentConfig.Instance.GetConfig();
        _doubleToDmsConversions = doubleToDmsConversions;
    }

    public void SetProject(ResearchProject project)
    {
        _currentProject = project;
    }

    public bool IsProjectSelected()
    {
        return _currentProject != null;
    }


    public List<PresentableProjectDetails> GetAllProjectDetails()
    {
        List<PresentableProjectDetails> details = new();
        if (IsProjectSelected())
        {
            details.Add(new PresentableProjectDetails() { Name = _rosetta.TextForId("projectusagewindow.details.name"), Value = _currentProject.Name });
            details.Add(new PresentableProjectDetails() { Name = _rosetta.TextForId("projectusagewindow.details.description"), Value = _currentProject.Description });
            details.Add(new PresentableProjectDetails() { Name = _rosetta.TextForId("projectusagewindow.details.date"), Value = _currentProject.CreationDate });
            details.Add(new PresentableProjectDetails() { Name = _rosetta.TextForId("projectusagewindow.details.dataname"), Value = _currentProject.DataName });
            details.Add(new PresentableProjectDetails() { Name = _rosetta.TextForId("projectusagewindow.details.controlgrouptype"), Value = _rosetta.TextForId(_currentProject.ControlGroupType.GetDetails().TextId) });
            details.Add(new PresentableProjectDetails() { Name = _rosetta.TextForId("projectusagewindow.details.multiplication"), Value = _currentProject.ControlGroupMultiplication.ToString() });
        }
        return details;
    }



    public class PresentableProjectDetails
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}  

