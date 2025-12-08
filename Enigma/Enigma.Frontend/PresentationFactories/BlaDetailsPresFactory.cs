// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using Enigma.Core.Slices.BlaSchema;

namespace Enigma.Frontend.Ui.PresentationFactories;


public record PresentableBlaDetails(string Name, string Text, string Glyphs);


public class BlaDetailsPresFactory
{

    public List<PresentableBlaDetails> CreateBlaDetails(BlaDetailsData detailsData)
    {
        var presDetails = new List<PresentableBlaDetails>();
        presDetails.Add(HandleSisterSignAsc(detailsData.SisterSignAsc));
        presDetails.Add(HandleClampedHouses(detailsData.ClampedHouses));
        presDetails.Add(HandleInterceptedSigns(detailsData.InterceptedSigns));
        presDetails.Add(HandleGroundNote(detailsData.GroundNote));
        presDetails.Add(HandleLordAscInHouses(detailsData.LordAscInHouses));
        presDetails.Add(HandleMoonInHouse(detailsData.MoonInHouse));
        return presDetails;
    }

    private PresentableBlaDetails HandleSisterSignAsc(int sign)
    {
        var glyph = DefineGlyph(sign);
        return new PresentableBlaDetails("Sister Sign Asc.", sign.ToString(), glyph);
    }

    private PresentableBlaDetails HandleClampedHouses(List<int> clampedHouses)
    {
        string ch = "";
        foreach (var house in clampedHouses)
        {
            if (ch != "") ch += " - ";
            ch+= house.ToString();
        }
        return new PresentableBlaDetails("Clamped Houses", ch, "");
    }

    private PresentableBlaDetails HandleInterceptedSigns(List<int> iSigns)
    {
        string isText = "";
        string glyphs = "";
        foreach (var iSign in iSigns)
        {
            if (isText != "") isText += " - ";
            if (glyphs != "") glyphs += " ";
            isText += iSign.ToString();
            glyphs += DefineGlyph(iSign);
        }
        return new PresentableBlaDetails("Intercepted Signs", isText, glyphs);
    }

    private PresentableBlaDetails HandleGroundNote(List<int> groundTone)
    {
        string gt = "";
        foreach (var gTone in groundTone)
        {
            if (gt != "") gt += " - ";
            gt += gTone.ToString();
        }
        return new PresentableBlaDetails("Ground note", gt, "");
    }

    private PresentableBlaDetails HandleLordAscInHouses(List<int> lords)
    {
        string la = "";
        foreach (var lord in lords)
        {
            if (la != "") la += " - ";
            la += lord.ToString();
        }
        return new PresentableBlaDetails("Lords Asc. in houses", la, "");
    }

    private PresentableBlaDetails HandleMoonInHouse(int house)
    {
        return new PresentableBlaDetails("Moon in house", house.ToString(), "");
    }

    private string DefineGlyph(int sign)
    {
        string[] glyphs = ["1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "-", "="];
        return glyphs[sign-1];
    }
    
}