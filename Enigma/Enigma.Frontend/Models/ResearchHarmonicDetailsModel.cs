// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Frontend.Ui.State;

namespace Enigma.Frontend.Ui.Models;

/// <summary>Model for harmonic details in research</summary>
public class ResearchHarmonicDetailsModel
{

    public static int MaxOrbHarmonic => 10;


    public static void SaveOrbHarmonics(double orb)
    {
        DataVaultResearch.Instance.ResearchHarmonicOrb = orb;
    }

    public static void SaveHarmonicNr(double harmonicNr)
    {
        DataVaultResearch.Instance.ResearchHarmonicValue = harmonicNr;
    }



}