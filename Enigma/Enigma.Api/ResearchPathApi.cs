// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Ardalis.GuardClauses;
using Enigma.Core.Handlers;

namespace Enigma.Api;

public interface IResearchPathApi
{
    /// <summary>Path to data files.</summary>
    /// <param name="projName">Name for project.</param>
    /// <param name="useControlGroup">True if data contains a controlgroup, false if data contains testcases.</param>
    /// <returns>String with full path to the required data, including the filename.</returns>
    public string DataPath(string projName, bool useControlGroup);

    /// <summary>Path to result files with positions.</summary>
    /// <param name="projName">Name for project.</param>
    /// <param name="methodName">Name for method.</param>
    /// <param name="useControlGroup">True if result is based on a controlgroup, false if result is based on testcases.</param>
    /// <returns>String with full path for the results, including the filename.</returns>
    public string ResultPath(string projName, string methodName, bool useControlGroup);

    /// <summary>Path to result files with countings.</summary>
    /// <param name="projName">Name for project.</param>
    /// <param name="methodName">Name for method.</param>
    /// <param name="useControlGroup">True if result is based on a controlgroup, false if result is based on testcases.</param>
    /// <returns>String with full path for the results, including the filename.</returns>
    public string CountResultsPath(string projName, string methodName, bool useControlGroup);

    /// <summary>Path to result files with the sums of all countings.</summary>
    /// <param name="projName">Name for project.</param>
    /// <param name="methodName">Name for method.</param>
    /// <param name="useControlGroup">True if result is based on a controlgroup, false if result is based on testcases.</param>
    /// <returns>String with full path for the summed totals, including the filename.</returns>
    public string SummedResultsPath(string projName, string methodName, bool useControlGroup);

}

public sealed class ResearchPathApi : IResearchPathApi
{
    private readonly IResearchPathHandler _researchPathHandler;

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