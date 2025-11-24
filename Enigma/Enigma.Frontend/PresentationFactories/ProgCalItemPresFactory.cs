// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;
using System.Collections.Generic;
using Enigma.Core.Calc;
using Enigma.Core.Slices.ProgCalendar;
using Enigma.Domain.References;
using Enigma.Domain.Requests;
using Enigma.Frontend.Ui.Support;
using Enigma.Frontend.Ui.Support.Conversions;

namespace Enigma.Frontend.Ui.PresentationFactories;


/// <summary>
/// Presentation factory for declination events.
/// </summary>
public class ProgCalItemPresFactory(IDateTimeCalc dateTimeCalc)
{
    private DoubleToDmsConversions _doubleToDmsConversions = new();
    
    public List<PresentableProgCalItem> CreatePresProgCalItems(List<ProgCalMatch> progCalItems)
    {
        var cal = Calendars.Gregorian;   // TODO handle different calendars
        var presEvents = new List<PresentableProgCalItem>();
        foreach (var pcItem in progCalItems)
        {
            var progPointGlyph = GlyphsForChartPoints.FindGlyph(pcItem.ProgPoint);
            var progType = pcItem.ProgType == ProgressionTypes.Transit ? "Transit" : "Secundary";
            var dateTime = JdToDateTimeString(pcItem.Jd, cal);
            switch (pcItem)
            {
                case ProgCalAspectMatch:
                    presEvents.Add(CreateAspect(pcItem, progType, dateTime, progPointGlyph));
                    break;
                case ProgCalDeclinationParallelMatch:
                    presEvents.Add(CreateDeclParallel(pcItem, progType, dateTime, progPointGlyph));
                    break;
            }          
        }
        return presEvents;
    }

    public List<PresentableProgCalPeriod> CreatePresProgCalPeriods(List<ProgCalPeriodMatch> progCalPeriods)
    {
        var cal = Calendars.Gregorian;
        var presPeriods = new List<PresentableProgCalPeriod>();
        foreach (var period in progCalPeriods)
        {
            presPeriods.Add(CreatePeriod(period));
        }
        return presPeriods;
    }
    

    private PresentableProgCalItem CreateAspect(ProgCalMatch pcItem, string progType, string dateTime, char progPointGlyph)
    {
        string eventType = "Aspect";
        var aspectItem = pcItem as ProgCalAspectMatch;
        var aspectGlyph = aspectItem.Aspect.GetDetails().Glyph;
        var radixGlyph = GlyphsForChartPoints.FindGlyph(aspectItem.RadixPoint);
        var (progPointPosition, signGlyph) = _doubleToDmsConversions.ConvertDoubleToDmsWithGlyph(aspectItem.ProgPosition);
        var presItem = new PresentableProgCalItem(dateTime, progType, progPointGlyph, aspectGlyph, radixGlyph,
            progPointPosition, signGlyph);
        return presItem;
    }

    private PresentableProgCalItem CreateDeclParallel(ProgCalMatch pcItem, string progType, string dateTime, char progPointGlyph)
    {
        var parallelItem = pcItem as ProgCalDeclinationParallelMatch;
        var eventType = parallelItem.DeclParallel == DeclinationParallels.Parallel ? "Parallel" : "Contra-parallel";
        var aspectGlyph = parallelItem.DeclParallel == DeclinationParallels.Parallel ? 'O' : 'P';
        var radixGlyph = GlyphsForChartPoints.FindGlyph(parallelItem.RadixPoint);
        var progPointPosition = _doubleToDmsConversions.ConvertDoubleToPositionsDmsText(parallelItem.ProgPosition);
        var signGlyph = ' ';
        var presItem = new PresentableProgCalItem(dateTime, progType, progPointGlyph, aspectGlyph, radixGlyph,
            progPointPosition, signGlyph);
        return presItem;
    }

    private PresentableProgCalPeriod CreatePeriod(ProgCalPeriodMatch period)
    {
        var progGlyph = GlyphsForChartPoints.FindGlyph(period.ProgPoint);
        var radixGlyph = GlyphsForChartPoints.FindGlyph(period.RadixPoint);
        var aspectGlyph = ' ';
        var dateTimeStart = JdToDateTime(period.JdStart, Calendars.Gregorian);
        var dateTimeEnd = JdToDateTime(period.JdEnd, Calendars.Gregorian);
        if (period is ProgCalAspectPeriodMatch)
        {
            var pMatch = period as ProgCalAspectPeriodMatch;
            aspectGlyph = pMatch.Aspect.GetDetails().Glyph;
        } else if (period is ProgCalDeclinationParallelPeriodMatch)
        {
            var pMatch = period as ProgCalDeclinationParallelPeriodMatch;
            aspectGlyph = pMatch.DeclParallel == DeclinationParallels.Parallel ? 'O' : 'P';
        }

        return new PresentableProgCalPeriod(dateTimeStart, dateTimeEnd, period.ProgType, progGlyph, aspectGlyph, radixGlyph);
    }
    
    
    private string JdToDateTimeString(double jd, Calendars cal)
    {
        var request = new DateTimeRequest(jd, true, cal);
        var (yearTxt, month, day, ut, _) = dateTimeCalc.CalcDateTime(request.JulDay, request.Calendar);
        var monthTxt = month > 9 ? month.ToString() : "0" + month;
        var dayTxt = day > 9 ? day.ToString() : "0" + day;
        var hour = (int)ut;
        var remainingFromHour = ut - hour;
        var minuteDbl = remainingFromHour * 60;
        var minute = (int)minuteDbl;
        var remainingFromMinute = minuteDbl - minute;
        var second = (int)(remainingFromMinute * 60);
        var hourTxt = hour > 9 ? hour.ToString() : "0" + hour;
        var minuteTxt = minute > 9 ? minute.ToString() : "0" + minute;
        var secondTxt = second > 9 ? second.ToString() : "0" + second;
        var dateTimeTxt = $"{yearTxt}/{monthTxt}/{dayTxt} {hourTxt}:{minuteTxt}:{secondTxt}";
        return dateTimeTxt;
    }

    private DateTime JdToDateTime(double jd, Calendars cal)
    {
        var request = new DateTimeRequest(jd, true, cal);
        var (year, month, day, ut, _) = dateTimeCalc.CalcDateTime(request.JulDay, request.Calendar);
        var hour = (int)ut;
        var remainingFromHour = ut - hour;
        var minuteDbl = remainingFromHour * 60;
        var minute = (int)minuteDbl;
        var remainingFromMinute = minuteDbl - minute;
        var second = (int)(remainingFromMinute * 60);
        var dateTime = new DateTime(year, month, day, hour, minute, second);
        return dateTime;

    }
}
