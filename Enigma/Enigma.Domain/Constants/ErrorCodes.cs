// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.Constants;

/// <summary>CommonFormula errcodes.</summary>
public static class ErrorCodes
{
    /// <summary>Indication that no error occurred.</summary>
    public const int None = 0;

    public const int DirCouldNotBeCreated = 1013;

    // errors for research project
    /// <summary>Error: projectfolder already exists.</summary>
    public const int ResearchProjfolderExists = 1014;
    /// <summary>Error: could not create projectfolder.</summary>
    public const int ResearchCannotCreateProjfolder = 1015;
    /// <summary>Error: could not parse project to JSON.</summary>
    public const int ResearchCannotParseProject2Json = 1016;
    /// <summary>Error: could not write JSON for project to file.</summary>
    public const int ResearchCannotWriteJson4Project = 1017;
    /// <summary>Error: could not copy datafile to project.</summary>
    public const int ResearchCannotCopyDatafile = 1018;
    /// <summary>Error: could not create results folder as subfolder in projectfolder.</summary>
    public const int ResearchCannotCreateResultsfolder = 1019;

}