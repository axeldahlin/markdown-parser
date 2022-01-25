namespace dotnet_visitor
{
    public class InlineCode : IHtmlTag
    {
        public List<IHtmlTag> Children { get; set; }
        public string? TextContent { get; set; }
        public string? TextToParse { get; set; }
        public InlineCode(string textContent)
        {
            TextContent = textContent;
            Children = new List<IHtmlTag>();
        }
        public string Accept(AbstractVisitor visitor)
        {
            return visitor.VisitInlineCode(this);
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