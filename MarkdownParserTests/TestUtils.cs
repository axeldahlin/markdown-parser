using System;
using System.IO;
using System.Reflection;


namespace MarkdownParserTests
{
    public static class TestUtils
{
    public static string GetTestPath(string relativePath)
    {
        var codeBaseUrl = new Uri(Assembly.GetExecutingAssembly().Location);
        var codeBasePath = Uri.UnescapeDataString(codeBaseUrl.AbsolutePath);
        var dirPath = Path.GetDirectoryName(codeBasePath);
        return Path.Combine(dirPath, "TestFiles", relativePath);
    }
}
}