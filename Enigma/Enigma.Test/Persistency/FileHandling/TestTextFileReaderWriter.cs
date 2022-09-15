// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Persistency.FileHandling;

namespace Enigma.Text.Persistency.FileHandling;

[TestFixture]
public class TestTextFileReaderWriter
{

    private ITextFileReader _textFileReader;
    private ITextFileWriter _textFileWriter;
    private string _singleline;
    private string _multiplelines;
    private readonly string _locationSingle = @"c:\temp\testfiles.txt";
    private readonly string _locationMultiple = @"c:\temp\testfilem.txt";

    [SetUp]
    public void SetUp()
    {
        _textFileReader = new TextFileReader();
        _textFileWriter = new TextFileWriter();
        _singleline = "A single line for testing purposes.";
        _multiplelines = "Multiple lines\nfor testing purposes.\nLast line.";
    }

    [Test]
    public void TestWritingOneLine()
    {
        _textFileWriter.WriteFile(_locationSingle, _singleline);
        string readingResult = _textFileReader.ReadFile(_locationSingle);
        Assert.That(readingResult, Is.EqualTo(_singleline));
    }

    [Test]
    public void TestWritingMultipleLines()
    {
        _textFileWriter.WriteFile(_locationMultiple, _multiplelines);
        string readingResult = _textFileReader.ReadFile(_locationMultiple);
        Assert.That(readingResult, Is.EqualTo(_multiplelines));
    }

}