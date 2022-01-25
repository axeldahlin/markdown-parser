namespace dotnet_visitor
{
    public class Root : IHtmlTag
    {
        public List<IHtmlTag> Children { get; set; }
        public string? TextContent { get; set; }
        public string? TextToParse { get; set; }

        public Root()
        {
            Children = new List<IHtmlTag>();
        } 
        public Root(string textToParse)
        {
            TextToParse = textToParse;
            Children = new List<IHtmlTag>();
        }        
        public Root(List<IHtmlTag> children)
        {
            Children = children;
        }
        public string Accept(AbstractVisitor visitor)
        {
            return visitor.VisitRoot(this);
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