using Xunit;
using dotnet_visitor;
using System.Collections.Generic;

namespace MarkdownParserTests;

public class HtmlVisitorAndIHtmlTagTests
{

    [Fact]
    public void CanVisitTreeWithPargraphAndBoldAndItalic()
    {
        // Arrange
        var visitor = new HtmlVisitor();
        var treeToVisit = new Root();
        treeToVisit.AddChild(new Paragraph(textToParse: null, textContent: "this is a paragraph"));
        treeToVisit.AddChild(new Bold(textToParse: null, textContent: "this is a bold"));
        treeToVisit.AddChild(new Italic(textToParse: null, textContent: "this is a italic"));

        // Act
        var result = visitor.VisitRoot(treeToVisit);

        // Assert
        const string expectedResult = "<div><p>this is a paragraph</p><b>this is a bold</b><i>this is a italic</i></div>";
        Assert.Equal(result, expectedResult);
    }

    [Fact]
    public void CanVisitHeaderLevelOne()
    {
        // Arrange
        var visitor = new HtmlVisitor();
        var treeToVisit = new Root();
        treeToVisit.AddChild(new HeaderLevelOne(textContent: "this is a h1"));

        // Act
        var result = visitor.VisitRoot(treeToVisit);

        // Assert
        const string expectedResult = "<div><h1>this is a h1</h1></div>";
        Assert.Equal(result, expectedResult);
    }

    [Fact]
    public void CanVisitOrderedLists()
    {
        // Arrange
        var visitor = new HtmlVisitor();
        var treeToVisit = new Root();
        var orderedList = new OrderedList();
        orderedList.AddChild(new OrderedListItem(textContent: "this is list item 1", listNumber: 1));
        orderedList.AddChild(new OrderedListItem(textContent: "this is list item 2", listNumber: 2));
        orderedList.AddChild(new OrderedListItem(textContent: "this is list item 3", listNumber: 3));
        treeToVisit.AddChild(orderedList);

        // Act
        var result = visitor.VisitRoot(treeToVisit);

        // Assert
        const string expectedResult = "<div><ol><li>this is list item 1</li><li>this is list item 2</li><li>this is list item 3</li></ol></div>";
        Assert.Equal(result, expectedResult);
    }

     [Fact]
    public void CanVisitUnOrderedLists()
    {
        // Arrange
         var visitor = new HtmlVisitor();
        var treeToVisit = new Root();
        var unOrderedList = new UnOrderedList();
        unOrderedList.AddChild(new UnOrderedListItem("this is list item 1"));
        unOrderedList.AddChild(new UnOrderedListItem("this is list item 2"));
        unOrderedList.AddChild(new UnOrderedListItem("this is list item 3"));
        treeToVisit.AddChild(unOrderedList);

        // Act
        var result = visitor.VisitRoot(treeToVisit);

        // Assert
        const string expectedResult = "<div><ul><li>this is list item 1</li><li>this is list item 2</li><li>this is list item 3</li></ul></div>";
        Assert.Equal(result, expectedResult);
    }
}