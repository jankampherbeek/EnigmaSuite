// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Ardalis.GuardClauses;
using Enigma.Core.Persistency;
using Enigma.Domain.Dtos;

namespace Enigma.Api.Persistency;

/// <summary>API for managing settings</summary>
public interface ISettingsApi
{
    /// <summary>Inserts a new setting into the database.</summary>
    /// <param name="name">Name of the setting.</param>
    /// <param name="value">Value of the setting.</param>
    /// <returns>True if the insert was successful, false otherwise.</returns>
    public bool InsertSetting(string name, string value);

    /// <summary>Updates an existing setting in the database.</summary>
    /// <param name="name">Name of the setting to update.</param>
    /// <param name="value">New value for the setting.</param>
    /// <returns>True if the update was successful, false otherwise.</returns>
    public bool UpdateSetting(string name, string value);

    /// <summary>Checks if a setting exists in the database.</summary>
    /// <param name="name">Name of the setting to check.</param>
    /// <returns>True if the setting exists, false otherwise.</returns>
    public bool SettingExists(string name);

    /// <summary>Reads the value of a setting from the database.</summary>
    /// <param name="name">Name of the setting to read.</param>
    /// <returns>The value of the setting, or null if the setting doesn't exist.</returns>
    public string? ReadSetting(string name);
}

/// <inheritdoc/>
public class SettingsApi: ISettingsApi
{
    private readonly ISettingsDao _settingsDao;

    /// <summary>Creates a new instance of the SettingsApi.</summary>
    /// <param name="settingsDao">The DAO to use for database operations.</param>
    public SettingsApi(ISettingsDao settingsDao)
    {
        _settingsDao = settingsDao;
    }

    /// <inheritdoc/>
    public bool InsertSetting(string name, string value)
    {
        Guard.Against.NullOrEmpty(name);
        Guard.Against.NullOrEmpty(value);

        var setting = new SettingsDto
        {
            Name = name,
            Value = value
        };
        return _settingsDao.InsertSetting(setting);
    }

    /// <inheritdoc/>
    public bool UpdateSetting(string name, string value)
    {
        Guard.Against.NullOrEmpty(name, nameof(name));
        Guard.Against.NullOrEmpty(value, nameof(value));

        var setting = new SettingsDto
        {
            Name = name,
            Value = value
        };

        return _settingsDao.UpdateSetting(setting);
    }

    /// <inheritdoc/>
    public bool SettingExists(string name)
    {
        Guard.Against.NullOrEmpty(name, nameof(name));
        return _settingsDao.SettingExists(name);
    }

    /// <inheritdoc/>
    public string? ReadSetting(string name)
    {
        Guard.Against.NullOrEmpty(name, nameof(name));
        return _settingsDao.ReadSetting(name);
    }
} 