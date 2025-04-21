// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.Dtos;

/// <summary>DTO for settings in the database</summary>
public class SettingsDto
{
    /// <summary>Name of the setting</summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>Value of the setting</summary>
    public string Value { get; set; } = string.Empty;
} 