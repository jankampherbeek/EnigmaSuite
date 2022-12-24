// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Ardalis.GuardClauses;
using Enigma.Api.Interfaces;
using Enigma.Core.Handlers.Research.Interfaces;

namespace Enigma.Api.Research;

public class ResearchPathApi : IResearchPathApi
{
    private IResearchPathHandler _researchPathHandler;

    public ResearchPathApi(IResearchPathHandler researchPathHandler)
    {
        _researchPathHandler = researchPathHandler;
    }


    /// <inherit/>
    public string DataPath(string projName, bool useControlGroup)
    {
        Guard.Against.NullOrEmpty(projName);
        return _researchPathHandler.DataPath(projName, useControlGroup);
    }

    /// <inherit/>
    public string ResultPath(string projName, string methodName, bool useControlGroup)
    {
        Guard.Against.NullOrEmpty(projName);
        Guard.Against.NullOrEmpty(methodName);
        return _researchPathHandler.ResultPath(projName, methodName, useControlGroup);
    }

    /// <inherit/>
    public string CountResultsPath(string projName, string methodName, bool useControlGroup)
    {
        Guard.Against.NullOrEmpty(projName);
        Guard.Against.NullOrEmpty(methodName);
        return _researchPathHandler.CountResultsPath(projName, methodName, useControlGroup);
    }

    /// <inherit/>
    public string SummedResultsPath(string projName, string methodName, bool useControlGroup)
    {
        Guard.Against.NullOrEmpty(projName);
        Guard.Against.NullOrEmpty(methodName);
        return _researchPathHandler.SummedResultsPath(projName, methodName, useControlGroup);
    }

}