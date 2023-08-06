using Enigma.Frontend.Ui.State;

namespace Enigma.Frontend.Ui.Models;

public class ResearchHarmonicDetailsModel
{

    public int MaxOrbHarmonic { get; } = 10;


    public static void SaveOrbHarmonics(double orb)
    {
        DataVault.Instance.ResearchHarmonicOrb = orb;
    }

    public static void SaveHarmonicNr(double harmonicNr)
    {
        DataVault.Instance.ResearchHarmonicValue = harmonicNr;
    }



}