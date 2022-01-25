using Xunit;
using dotnet_visitor;

namespace MarkdownParserTests;

public class ParsingTests
{
    [Fact]
    public void CanParseTextWithThreeAsteriks()
    {
        // Arrange
        const string twoOpeningAndClosingThreeAsteriks = "Morbi egestas nisl non libero ***sagittis***, eget consectetur diam eleifend. ***Vivamus*** tempus leo luctus blandit vestibulum.";
        var parser = new Parser();

        // Act
        var treeOfTags = parser.Parse(twoOpeningAndClosingThreeAsteriks);

        // Assert
        Assert.Equal(treeOfTags.Children[0].GetType().FullName, "dotnet_visitor.Paragraph");
        Assert.Equal(treeOfTags.Children[1].Children[0].GetType().FullName, "dotnet_visitor.Bold");
        Assert.Equal(treeOfTags.Children[3].Children[0].TextContent, "Vivamus");
        Assert.Equal(treeOfTags.Children[4].TextContent, " tempus leo luctus blandit vestibulum.");
    }
}