namespace dotnet_visitor
{
    public class ListItem : IHtmlTag
    {
        public List<IHtmlTag> Children { get; set; }
        public string? TextContent { get; set; }
        public string? TextToParse { get; set; }
        public ListItem(string textContent)
        {
            TextContent = textContent;
            Children = new List<IHtmlTag>();
        }
        public string Accept(AbstractVisitor visitor)
        {
            return visitor.VisitListItem(this);
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