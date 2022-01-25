using System.Text.RegularExpressions;

namespace dotnet_visitor
{
    public class TopDownParser
    {
        public TopDownParser()
        {

        }

        public Root Parse(string textToParse)
        {
            var arrayOfText = textToParse.Split("\n");

            var nyArray = arrayOfText.Select(text => GenerateIHtmlTagsForFirstLayer(text)).ToArray();



            return new Root("");
        }


        private IHtmlTag GenerateIHtmlTagsForFirstLayer(string text)
        {

            return new Paragraph("");

        }
    }
}