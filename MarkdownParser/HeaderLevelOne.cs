namespace dotnet_visitor
{
    public class HeaderLevelOne : IHtmlTag
    {
        public List<IHtmlTag> Children { get; set; }
        public string TextContent { get; set; }
        public string TextToParse { get; set; }
        public HeaderLevelOne(String textToParse)
        {
            TextToParse = textToParse;
            Children = new List<IHtmlTag>();
        }
        public string Accept(AbstractVisitor visitor)
        {
            return visitor.VisitHeaderLevelOne(this);
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