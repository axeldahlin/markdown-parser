using Xunit;
using dotnet_visitor;

namespace MarkdownParserTests;

public class UnitTest1
{

    [Fact]
    public void CanConvertTreeOfIHtmlTagsToHtml()
    {
        // Arrange
        var visitor = new HtmlVisitor();

        var treeToParse = new Root("");
        var paragraph = new Paragraph("");
        paragraph.SetTextContent("this is a paragraph");
        var bold = new Bold("");
        bold.SetTextContent("this is a bold");
        var italic = new Italic("");
        italic.SetTextContent("this is a italic");

        treeToParse.AddChild(paragraph);
        treeToParse.AddChild(bold);
        treeToParse.AddChild(italic);

        // Act
        var result = "";
        foreach (var child in treeToParse.Children)
        {
            result += child.Accept(visitor);
        }

        // Assert
        const string expectedResult = "<p>this is a paragraph</p><b>this is a bold</b><i>this is a italic</i>";
        Assert.Equal(result, expectedResult);
    }


    [Fact]
    public void CanParseTextWithThreeAsteriks()
    {
        // Arrange
        const string twoOpeningAndClosingThreeAsteriks = "Morbi egestas nisl non libero ***sagittis***, eget consectetur diam eleifend. ***Vivamus*** tempus leo luctus blandit vestibulum.";
        var parser = new Parser();
        parser.Parse(twoOpeningAndClosingThreeAsteriks);

        var visitor = new HtmlVisitor();
        var result = "";

        // Act
        foreach (var child in parser.Result.Children)
        {
            result += child.Accept(visitor);
        }

        // Assert
        const string expectedResult = "<p>Morbi egestas nisl non libero </p><i><b>sagittis</b></i><p>, eget consectetur diam eleifend. </p><i><b>Vivamus</b></i><p> tempus leo luctus blandit vestibulum.</p>";
        Assert.Equal(result, expectedResult);
    }
}