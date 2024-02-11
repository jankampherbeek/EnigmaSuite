// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Research;

namespace Enigma.Core.Handlers;


/// <summary>Handler for paths for files that are used for research.</summary>
public interface IResearchPathHandler
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
