﻿// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Research;

namespace Enigma.Core.Handlers.Interfaces;


/// <summary>Handler for the creation of a research project.</summary>
public interface IProjectCreationHandler
{
    /// <summary>Handles the creation of a research project and the accompanying controlgroup.</summary>
    /// <param name="project">Definition of the project.</param>
    /// <param name="errorCode">Resulting errorcode.</param>
    /// <returns>True if no error occurred.</returns>
    public bool CreateProject(ResearchProject project, out int errorCode);
}


