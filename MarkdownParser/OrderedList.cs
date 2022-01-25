namespace dotnet_visitor
{
    public class OrderedList : IHtmlTag
    {
        public List<IHtmlTag> Children { get; set; }
        public string? TextContent { get; set; }
        public string? TextToParse { get; set; }
        public OrderedList()
        {
            Children = new List<IHtmlTag>();
        }

        public OrderedList(List<IHtmlTag> children)
        {
            Children = children;
        }
        public string Accept(AbstractVisitor visitor)
        {
            return visitor.VisitOrderedList(this);
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