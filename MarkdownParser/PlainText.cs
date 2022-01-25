namespace dotnet_visitor
{
    public class PlainText : IHtmlTag
    {
        public List<IHtmlTag> Children { get; set; }
        public string? TextContent { get; set; }
        public string? TextToParse { get; set; }
        public PlainText(string textToParse)
        {
            TextToParse = textToParse;
            Children = new List<IHtmlTag>();
        }

        public PlainText(string textToParse, string textContent)
        {
            TextToParse = textToParse;
            TextContent = textContent;
            Children = new List<IHtmlTag>();
        }

        public PlainText(string textToParse, string textContent, List<IHtmlTag> children)
        {
            TextToParse = textToParse;
            TextContent = textContent;
            Children = children;
        }
        public string Accept(AbstractVisitor visitor)
        {
            return visitor.VisitPlainText(this);
        }
        public void AddChild(IHtmlTag child)
        {
            this.Children.Add(child);
        }
        public void SetTextContent(string textContent)
        {
            this.TextContent = textContent;
        }
        public void SetTextToParse(string textToParse)
        {
            this.TextToParse = textToParse;
        }
    }
}