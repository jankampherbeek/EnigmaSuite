﻿// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Frontend.Ui.Support.Conversions;
using Enigma.Frontend.Ui.Support.Parsers;
using Enigma.Frontend.Ui.Support.Validations;
using FakeItEasy;

namespace Enigma.Test.Frontend.Ui.Support.InputParsers;

[TestFixture]

public class TestDateInputParser
{
    private const char SEPARATOR = '/';
    private const Calendars CAL = Calendars.Gregorian;
    private const YearCounts YEAR_COUNT = YearCounts.CE;


    [Test]
    public void HappyFlow()
    {
        const string dateInput = "2022/6/8";
        int[] dateValues = { 2022, 6, 8 };
        var valueRangeConverterFake = A.Fake<IValueRangeConverter>();
        (int[] numbers, bool success) rangeResult = (dateValues, true);
        A.CallTo(() => valueRangeConverterFake.ConvertStringRangeToIntRange(dateInput, SEPARATOR)).Returns(rangeResult);
        var dateValidatorFake = A.Fake<IDateValidator>();
        FullDate? fullDate;
        A.CallTo(() => dateValidatorFake.CreateCheckedDate(dateValues, CAL, YEAR_COUNT, out fullDate)).Returns(true);
        var parser = new DateInputParser(valueRangeConverterFake, dateValidatorFake);
        Assert.That(parser.HandleDate(dateInput, CAL, YEAR_COUNT, out fullDate), Is.True);
    }

    [Test]
    public void SyntaxError()
    {
        const string dateInput = "2022/a/8";
        int[] dateValues = Array.Empty<int>();
        var valueRangeConverterFake = A.Fake<IValueRangeConverter>();
        (int[] numbers, bool success) rangeResult = (dateValues, false);
        A.CallTo(() => valueRangeConverterFake.ConvertStringRangeToIntRange(dateInput, SEPARATOR)).Returns(rangeResult);
        var dateValidatorFake = A.Fake<IDateValidator>();
        var parser = new DateInputParser(valueRangeConverterFake, dateValidatorFake);
        Assert.That(parser.HandleDate(dateInput, CAL, YEAR_COUNT, out FullDate? _), Is.False);
    }

    [Test]
    public void DateError()
    {
        const string dateInput = "2022/13/8";
        int[] dateValues = { 2022, 13, 8 };
        var valueRangeConverterFake = A.Fake<IValueRangeConverter>();
        (int[] numbers, bool success) rangeResult = (dateValues, true);
        A.CallTo(() => valueRangeConverterFake.ConvertStringRangeToIntRange(dateInput, SEPARATOR)).Returns(rangeResult);
        var dateValidatorFake = A.Fake<IDateValidator>();
        FullDate? fullDate;
        A.CallTo(() => dateValidatorFake.CreateCheckedDate(dateValues, CAL, YEAR_COUNT, out fullDate)).Returns(false);
        var parser = new DateInputParser(valueRangeConverterFake, dateValidatorFake);
        Assert.That(parser.HandleDate(dateInput, CAL, YEAR_COUNT, out fullDate), Is.False);
    }

}
