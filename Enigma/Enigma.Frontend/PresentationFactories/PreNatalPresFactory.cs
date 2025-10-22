// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using Enigma.Core.Calc;
using Enigma.Core.Slices.PreNatal;
using Enigma.Domain.Constants;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Domain.Requests;
using Enigma.Frontend.Ui.Support;
using Enigma.Frontend.Ui.Support.Conversions;
using Microsoft.VisualBasic;

namespace Enigma.Frontend.Ui.PresentationFactories;

/// <summary>
/// Presentable moment for PreNatal, covers various astronomical positions
/// </summary>
/// <param name="RealDateTime">Actual date and time, in the format yyyy/mm/dd hh:mi:ss</param>
/// <param name="PreNatalDateTime">Pre Natal date and time, in the format yyyy/mm/dd hh:mi:ss</param>   
/// <param name="TypeGlyph">Glyph for the type, either eclipse, ingress, retrodirect or mutual aspect</param>
/// <param name="Factor1Glyph">Glyph for the first factor</param>
/// <param name="Position1">Position of the first factor, without the sign</param>
/// <param name="SignGlyph1">Glyph for the sign of the first factor</param>
/// <param name="Factor2Glyph">Optional: glyph for the second factor</param>
/// <param name="Position2">Optional: the position of the second factor, without the sign</param>
/// <param name="SignGlyph2">Optional: glyph fr the sign of the second factor</param>
public record PresentablePreNatalMoment(
    string RealDateTime,
    string PreNatalDateTime,
    char TypeGlyph,
    char Factor1Glyph,
    string Position1,
    char SignGlyph1,
    char Factor2Glyph,
    string Position2,
    char SignGlyph2);

/// <summary>
/// Presentable event for PreNatal
/// </summary>
/// <param name="Description">Description of event</param>
/// <param name="DateTime">Date and time of the event, in the format yyyy/mm/dd hh:mi:ss</param>
public record PresentablePreNatalEvent(string Description, string DateTime);



/// <summary>
/// Factory for presentable pre natal moments and events
/// </summary>
public class PreNatalPresFactory(IDateTimeCalc dateTimeCalc) {
    
    private DoubleToDmsConversions _doubleToDmsConversions = new();

    public List<PresentablePreNatalMoment> GetPreNatalMoments(List<PreNatalParent> moments, double jdConception, Calendars cal)
    {
        var presMoments = new List<PresentablePreNatalMoment>();
        foreach (var moment in moments)
        {
            switch (moment)
            {
                case Eclipse eclipse:
                    presMoments.Add(HandleEclipse(eclipse, jdConception, cal));
                    break;
                case Ingress ingress:
                    presMoments.Add(HandleIngress(ingress, jdConception, cal));
                    break;
                case RetroDirect retroDirect:
                    presMoments.Add(HandleRetrodirect(retroDirect, jdConception, cal));   
                    break;
                case MutualAspect mutualAspect:
                    presMoments.Add(HandleMutualAspect(mutualAspect, jdConception, cal));   
                    break;
            }
        }
        return presMoments;
    }

    
    public List<PresentablePreNatalEvent> GetPreNatalEvents(List<ProgEvent> progEvents, Calendars cal)
    {
        var presEvents = new List<PresentablePreNatalEvent>();

        foreach (var progEvent in progEvents)
        {
            presEvents.Add(HandleProgEvent (progEvent, cal));
        }
        return presEvents;
    }

    private PresentablePreNatalMoment HandleEclipse(Eclipse eclipse, double jdConception, Calendars cal)
    {

        var preNatalDateTimeTxt = JdToDateTimeString(eclipse.PreNatalJd, cal);
        var actualDateTime = PreNatalOrchestrator.JdPreNatalToActual(jdConception, eclipse.PreNatalJd);
        var actualDateTimeTxt = JdToDateString(actualDateTime, cal);
        var typeGlyph = ' ';
        var factor1Glyph = ' ';
        if (eclipse.LunarSolar == 'S')
        {
            factor1Glyph = GlyphsForChartPoints.FindGlyph(ChartPoints.Sun);
            typeGlyph = '[';     // use glyph for Black Sun for solar eclipse
        }
        else
        {
            factor1Glyph = GlyphsForChartPoints.FindGlyph(ChartPoints.Moon);
            typeGlyph = ',';    // use glyph for Black Moon for lunar eclipse
        }
        var position1 = _doubleToDmsConversions.ConvertDoubleToDmsWithGlyph(eclipse.Longitude).longTxt;
        var signGlyph1 = _doubleToDmsConversions.ConvertDoubleToDmsWithGlyph(eclipse.Longitude).glyph;
        const char factor2Glyph = ' ';
        const string position2 = "";
        const char signGlyph2 = ' ';
        return new PresentablePreNatalMoment(actualDateTimeTxt, preNatalDateTimeTxt, typeGlyph, factor1Glyph, position1, 
            signGlyph1, factor2Glyph, position2, signGlyph2);    
        
    }

    private PresentablePreNatalMoment HandleIngress(Ingress ingress, double jdConception, Calendars cal)
    {
        var preNatalDateTimeTxt = JdToDateTimeString(ingress.PreNatalJd, cal);
        var actualDateTime = PreNatalOrchestrator.JdPreNatalToActual(jdConception, ingress.PreNatalJd);
        var actualDateTimeTxt = JdToDateString(actualDateTime, cal);
        var typeGlyph = GlyphsForChartPoints.FindGlyph(ingress.Factor);
        var position1 = "00" + EnigmaConstants.DEGREE_SIGN + "00" + EnigmaConstants.MINUTE_SIGN + "00" + EnigmaConstants.SECOND_SIGN;
        var signGlyph1 = GlyphsForSigns.FindGlyph(ingress.Sign);
        var factor1Glyph = signGlyph1;
        const char factor2Glyph = ' ';
        const string position2 = "";
        const char signGlyph2 = ' ';
        return new PresentablePreNatalMoment(actualDateTimeTxt, preNatalDateTimeTxt, typeGlyph, factor1Glyph, position1, 
            signGlyph1, factor2Glyph, position2, signGlyph2);
    }

    private PresentablePreNatalMoment HandleRetrodirect(RetroDirect retroDirect, double jdConception, Calendars cal)
    {
        var preNatalDateTimeTxt = JdToDateTimeString(retroDirect.PreNatalJd, cal);
        var actualDateTime = PreNatalOrchestrator.JdPreNatalToActual(jdConception, retroDirect.PreNatalJd);
        var actualDateTimeTxt = JdToDateString(actualDateTime, cal);
        var typeGlyph = retroDirect.Direction == 'R' ? 'R' : 'Ô';   // use  retrograde glyph and glyph for decile aspect for direct movement
        var position1 = _doubleToDmsConversions.ConvertDoubleToDmsWithGlyph(retroDirect.Longitude).longTxt;
        var signGlyph1 = _doubleToDmsConversions.ConvertDoubleToDmsWithGlyph(retroDirect.Longitude).glyph;
        var factor1Glyph = GlyphsForChartPoints.FindGlyph(retroDirect.Factor);
        const char factor2Glyph = ' ';
        const string position2 = "";
        const char signGlyph2 = ' ';
        return new PresentablePreNatalMoment(actualDateTimeTxt, preNatalDateTimeTxt, typeGlyph, factor1Glyph, position1, 
            signGlyph1, factor2Glyph, position2, signGlyph2);
    }

    private PresentablePreNatalMoment HandleMutualAspect(MutualAspect mutualAspect, double jdConception, Calendars cal)
    {
        var preNatalDateTimeTxt = JdToDateTimeString(mutualAspect.PreNatalJd, cal);
        var actualDateTime = PreNatalOrchestrator.JdPreNatalToActual(jdConception, mutualAspect.PreNatalJd);
        var actualDateTimeTxt = JdToDateString(actualDateTime, cal);
        var typeGlyph = mutualAspect.Aspect.GetDetails().Glyph;
        var position1 = _doubleToDmsConversions.ConvertDoubleToDmsWithGlyph(mutualAspect.Position1).longTxt;
        var signGlyph1 = _doubleToDmsConversions.ConvertDoubleToDmsWithGlyph(mutualAspect.Position2).glyph;
        var factor1Glyph = GlyphsForChartPoints.FindGlyph(mutualAspect.Factor1);
        var position2 = _doubleToDmsConversions.ConvertDoubleToDmsWithGlyph(mutualAspect.Position2).longTxt;
        var signGlyph2 = _doubleToDmsConversions.ConvertDoubleToDmsWithGlyph(mutualAspect.Position2).glyph;
        var factor2Glyph = GlyphsForChartPoints.FindGlyph(mutualAspect.Factor2);
        return new PresentablePreNatalMoment(actualDateTimeTxt, preNatalDateTimeTxt, typeGlyph, factor1Glyph, position1, 
            signGlyph1, factor2Glyph, position2, signGlyph2);
    }
    
    
    private PresentablePreNatalEvent HandleProgEvent(ProgEvent progEvent, Calendars cal) {
        var description = progEvent.Description;
        var jd = progEvent.DateTime.JulianDayForEt;
        var dateTxt = JdToDateString(jd, cal);   
        return new PresentablePreNatalEvent(description, dateTxt);
    }
    
    private string JdToDateTimeString(double jd, Calendars cal)
    {
        var request = new DateTimeRequest(jd, true, cal);
        var (yearTxt, month, day, ut, _) = dateTimeCalc.CalcDateTime(request.JulDay, request.Calendar);
        var monthTxt = month > 9 ? month.ToString() : "0" + month;
        var dayTxt = day > 9 ? day.ToString() : "0" + day;
        var hour = (int)ut;
        var remaining = ut - hour;
        var minute = (int)(remaining * 60);
        remaining -= minute / 60.0;
        var second = (int)(remaining * 60);
        var hourTxt = hour > 9 ? hour.ToString() : "0" + hour;
        var minuteTxt = minute > 9 ? minute.ToString() : "0" + minute;
        var secondTxt = second > 9 ? second.ToString() : "0" + second;
        var dateTimeTxt = $"{yearTxt}/{monthTxt}/{dayTxt} {hourTxt}:{minuteTxt}:{secondTxt}";
        return dateTimeTxt;
    }

    private string JdToDateString(double jd, Calendars cal)
    {
        var request = new DateTimeRequest(jd, true, cal);
        var (yearTxt, month, day, ut, _) = dateTimeCalc.CalcDateTime(request.JulDay, request.Calendar);
        var monthTxt = month > 9 ? month.ToString() : "0" + month;
        var dayTxt = day > 9 ? day.ToString() : "0" + day;
        var dateTxt = $"{yearTxt}/{monthTxt}/{dayTxt}";
        return dateTxt;
    }
}