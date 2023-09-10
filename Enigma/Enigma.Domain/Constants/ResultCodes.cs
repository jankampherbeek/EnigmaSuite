// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.Constants;

/// <summary>CommonFormula errcodes.</summary>
public static class ResultCodes
{
    /// <summary>Indication that no error occurred.</summary>
    public const int OK = 0;

    public const int GENERAL_ERROR = 100;
    public const int WRONG_ARGUMENTS = 101;
    
    public const int DIR_COULD_NOT_BE_CREATED = 1013;

    // errors for research project
    /// <summary>Error: projectfolder already exists.</summary>
    public const int RESEARCH_PROJFOLDER_EXISTS = 1014;
    /// <summary>Error: could not create projectfolder.</summary>
    public const int RESEARCH_CANNOT_CREATE_PROJFOLDER = 1015;
    /// <summary>Error: could not parse project to JSON.</summary>
    public const int RESEARCH_CANNOT_PARSE_PROJECT2_JSON = 1016;
    /// <summary>Error: could not write JSON for project to file.</summary>
    public const int RESEARCH_CANNOT_WRITE_JSON4_PROJECT = 1017;
    /// <summary>Error: could not copy datafile to project.</summary>
    public const int RESEARCH_CANNOT_COPY_DATAFILE = 1018;
    /// <summary>Error: could not create results folder as subfolder in projectfolder.</summary>
    public const int RESEARCH_CANNOT_CREATE_RESULTSFOLDER = 1019;

}