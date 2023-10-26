// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Frontend.Ui.State;

namespace Enigma.Frontend.Ui.Models;

/// <summary>Model for midpoint details in research</summary>
public class ResearchMidpointDetailsModel
{
    public static int MaxOrbMidpoints => 10;


    public static void SaveOrbMidpoints(double orb)
    {
        DataVaultResearch.Instance.ResearchMidpointOrb = orb;
    }

    public static void SaveDialDivision(int dialDivision)
    {
        DataVaultResearch.Instance.ResearchMidpointDialDivision = dialDivision;
    }
}