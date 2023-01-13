// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers.Research.Interfaces;

namespace Enigma.Core.Handlers.Research;

/// <inherit/>
public sealed class ResearchPathHandler : IResearchPathHandler
{
    private readonly IResearchPaths _researchPaths;

    public ResearchPathHandler(IResearchPaths researchPaths)
    {
        _researchPaths = researchPaths;
    }

    /// <inherit/>
    public string DataPath(string projName, bool useControlGroup)
    {
        return _researchPaths.DataPath(projName, useControlGroup);
    }

    /// <inherit/>
    public string ResultPath(string projName, string methodName, bool useControlGroup)
    {
        return _researchPaths.ResultPath(projName, methodName, useControlGroup);
    }

    /// <inherit/>
    public string CountResultsPath(string projName, string methodName, bool useControlGroup)
    {
        return _researchPaths.CountResultsPath(projName, methodName, useControlGroup);
    }

    /// <inherit/>
    public string SummedResultsPath(string projName, string methodName, bool useControlGroup)
    {
        return _researchPaths.SummedResultsPath(projName, methodName, useControlGroup);
    }
}
