namespace dotnet_visitor
{
    public class Paragraph : IHtmlTag
    {
        public List<IHtmlTag> Children { get; set; }
        public string? TextContent { get; set; }
        public string? TextToParse { get; set; }
        public Paragraph(string textToParse)
        {
            TextToParse = textToParse;
            Children = new List<IHtmlTag>();
        }

        public Paragraph(string textToParse, string textContent)
        {
            TextToParse = textToParse;
            TextContent = textContent;
            Children = new List<IHtmlTag>();
        }

        public Paragraph(string textToParse, string textContent, List<IHtmlTag> children)
        {
            TextToParse = textToParse;
            TextContent = textContent;
            Children = children;
        }
        public string Accept(AbstractVisitor visitor)
        {
            return visitor.VisitParagraph(this);
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