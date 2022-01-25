namespace dotnet_visitor
{
    public class HtmlVisitor : AbstractVisitor
    {
        public string Result = "";
        public override string VisitRoot(Root root)
        {
            return "";
        }
        public override string VisitParagraph(Paragraph paragraph)
        {
            var textContent = paragraph.TextContent is not null ? paragraph.TextContent : "";
         
            return $"<p>{textContent}{VisitAndReturnChildred(paragraph)}</p>";
        }

        public override string VisitBold(Bold bold)
        {
            var textContent = bold.TextContent is not null ? bold.TextContent : "";

            return $"<b>{textContent}{VisitAndReturnChildred(bold)}</b>";
        }

        public override string VisitItalic(Italic italic)
        {
            var textContent = italic.TextContent is not null ? italic.TextContent : "";

            return $"<i>{textContent}{VisitAndReturnChildred(italic)}</i>";
        }

        private string VisitAndReturnChildred(IHtmlTag tag)
        {
            var children = "";
            foreach (var child in tag.Children)
            {
                children += child.Accept(this);
            }
            return children;
        }
    }
}