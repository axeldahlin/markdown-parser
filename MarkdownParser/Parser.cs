using System.Text.RegularExpressions;

namespace dotnet_visitor
{
    public class Parser
    {
        public Root Result { get; set; }

        public Parser()
        {
            Result = new Root("");
        }

        // Göra om DoesHaveXXXAsteriks metoder 
        // till 1 metod som med nummer 1-3 som 
        // parameter
        public bool DoesHaveThreeAsteriks(string text)
        {
            string strRegex = @"\*{3}.*\*{3}";
            var re = new Regex(strRegex);
            var result = re.Match(text);
            return result.Success;
        }

        public bool DoesHaveTwoAsteriks(string text)
        {
            string strRegex = @"\*{2}.*\*{2}";
            var re = new Regex(strRegex);
            var result = re.Match(text);
            return result.Success;
        }
        
        public bool DoesHaveOneAsteriks(string text)
        {
            string strRegex = @"\*{1}.*\*{1}";
            var re = new Regex(strRegex);
            var result = re.Match(text);
            return result.Success;
        }


        public void Parse(string textToParse)
        {
            Result = new Root(textToParse);
            while (Result.TextToParse.Length > 0)
            {
                if (DoesHaveThreeAsteriks(Result.TextToParse))
                {
                    AddBoldAndItalic(Result);
                } 
                else 
                {
                    var newParagraph = new Paragraph("");
                    newParagraph.SetTextContent(Result.TextToParse);
                    Result.AddChild(newParagraph);
                    Result.SetTextToParse("");
                }
            }
        }
        private void AddBoldAndItalic(IHtmlTag node)
        {
            int indexOfOpening = node.TextToParse.IndexOf("***");
            var textBeforeOpening = node.TextToParse.Substring(0, indexOfOpening);
            var newParagraph = new Paragraph("");
            newParagraph.SetTextContent(textBeforeOpening);
            node.AddChild(newParagraph);

            var textAfterOpening = node.TextToParse.Substring(indexOfOpening + 3);

            var indexOfClosing = textAfterOpening.IndexOf("***");

            var toBoldAndItalic = textAfterOpening.Substring(0, indexOfClosing);

            var textContinueToParse = textAfterOpening.Substring(indexOfClosing + 3);

            var italic = new Italic("");
            var bold = new Bold("");

            bold.SetTextContent(toBoldAndItalic);
            italic.AddChild(bold);

            node.AddChild(italic);
            node.SetTextToParse(textContinueToParse);
        }
    }
}