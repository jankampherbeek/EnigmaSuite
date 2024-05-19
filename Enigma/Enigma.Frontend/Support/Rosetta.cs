// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;
using System.Collections.Generic;
using Enigma.Api;
using Microsoft.Extensions.DependencyInjection;

namespace Enigma.Frontend.Ui.Support;

/// <summary>Keep track of the current language and retrieve texts for this language.</summary>
/// <remarks>Implemented as singleton, based on code by Jon Skeet: https://csharpindepth.com/articles/singleton .</remarks>
/// <remarks>The language should be either English or Dutch.</remarks>
public sealed class Rosetta
{
    private static Rosetta _instance = new();
    private const string TEXT_NOT_FOUND = "-- Text not found --";
    private string _currentLanguage = "en";
    private Dictionary<string, string?> _texts = new();
    private readonly IResourceBundleApi _api = App.ServiceProvider.GetRequiredService<IResourceBundleApi>();
  
    // Explicit static constructor to tell C# compiler not to mark type as beforefieldinit
    static Rosetta()
    {
    }

    private Rosetta()
    {
    }

    // ReSharper disable once ConvertToAutoProperty
    public static Rosetta Instance => _instance;   // instance is a singleton
    
    /// <summary>Set the preferred language.</summary>
    /// <param name="newLanguage">Indicator for the new language, either 'en' for English or 'nl' for Dutch.</param>
    /// <remarks>IF an invalid language is used, Enigma will not change the existing language.</remarks>
    public void SetLanguage(string newLanguage)
    {
        if ((newLanguage != "en") && (newLanguage != "nl")) return;
        _currentLanguage = newLanguage;
        ReadTextsForLanguage();
    }

    /// <summary>Return indication for currently selected language.</summary>
    /// <returns>String for the language, either 'en' or 'nl'.</returns>
    public string GetLanguage()
    {
        return _currentLanguage;
    }

    /// <summary>Get text in current active language.</summary>
    /// <param name="rbKey">The key to search for in the Resource Bundle.</param>
    /// <returns>If found, the text, otherwise and indication that the text was not found.</returns>
    public string GetText(string rbKey)
    {
        return _texts.GetValueOrDefault(rbKey, TEXT_NOT_FOUND);
    }


    private void ReadTextsForLanguage()
    {
        string currentDir = AppDomain.CurrentDomain.BaseDirectory;
        string relativePath = currentDir + @"res\lang\rb-" + _currentLanguage + ".txt";
        _texts = _api.RetrieveTexts(relativePath);
    }
    
}