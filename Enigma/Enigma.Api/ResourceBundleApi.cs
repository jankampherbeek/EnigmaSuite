// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers;

namespace Enigma.Api;

/// <summary>Retrieval for texts from a resource bundle.</summary>
public interface IResourceBundleApi
{
    /// <summary>Get all texts from a resource bundle.</summary>
    /// <param name="rbPath">Path of the file with the resourcebundle, including the filename.</param>
    /// <returns>All content from the resource bundle.</returns>
    public Dictionary<string, string?> RetrieveTexts(string rbPath);
}


/// <inheritdoc/>
public class ResourceBundleApi(IResourceBundleHandler handler) : IResourceBundleApi
{
    /// <inheritdoc/>
    public Dictionary<string, string?> RetrieveTexts(string rbPath)
    {
        return handler.GetAllTextsFromResourceBundle(rbPath);
    }
}