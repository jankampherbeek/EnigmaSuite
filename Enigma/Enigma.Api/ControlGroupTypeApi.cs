// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Persistency;

namespace Enigma.Api;

/// <summary>API for controlgroup types</summary>
public interface IControlGroupTypeApi
{
    /// <summary>Find ID for a specific controlgroup type</summary>
    /// <param name="name">Name of the controlgroup type</param>
    /// <returns>If found: the id of the controlgroup type, otherwise -1</returns>
    public int GetIdForControlGroupType(string name);

    /// <summary>Find resource bundle key for a specific controlgroup type</summary>
    /// <param name="id">The id of the controlgroup type</param>
    /// <returns>If found: the rbKey, otherwise an empty string</returns>
    public string GetRbKeyForControlGroupType(int id);

}


/// <inheritdoc/>
public class ControlGroupTypeApi(IControlGroupTypeDao controlGroupTypeDao) : IControlGroupTypeApi
{
    /// <inheritdoc/>
    public int GetIdForControlGroupType(string name)
    {
        return controlGroupTypeDao.ReadIdForControlGroupType(name);
    }

    public string GetRbKeyForControlGroupType(int id)
    {
        return controlGroupTypeDao.ReadRbKeyForControlGroupType(id);
    }
}