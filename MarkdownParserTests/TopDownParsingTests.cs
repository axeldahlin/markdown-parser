using Xunit;
using dotnet_visitor;
using System.IO;
using System;
using System.Reflection;


namespace MarkdownParserTests;

public class TopDownParsingTests
{
    [Fact]
    public void FirstTest()
    {
        var path = TestUtils.GetTestPath("markdown.md");
        string textToParse = File.ReadAllText(path);

        var parser = new TopDownParser();

        var result = parser.Parse(textToParse);



    }
}