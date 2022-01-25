namespace dotnet_visitor
{
    public interface IHtmlTag
    {
        public List<IHtmlTag> Children { get; set; }
        public string? TextToParse { get; set; }
        public string? TextContent { get; set; }
        public void AddChild(IHtmlTag child);
        public void SetTextContent(string textContent);
        public void SetTextToParse(string textToParse);
        public string Accept(AbstractVisitor visitor);
    }
}