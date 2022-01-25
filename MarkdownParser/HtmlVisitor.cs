namespace dotnet_visitor
{
    public class HtmlVisitor : AbstractVisitor
    {
        public string Result = "";
        public override string VisitRoot(Root root)
        {
            return $"<div>{VisitAndReturnChildred(root)}</div>";
        }
        public override string VisitParagraph(Paragraph paragraph)
        {
            return $"<p>{ReturnTextContentOrEmptyString(paragraph)}{VisitAndReturnChildred(paragraph)}</p>";
        }

        public override string VisitBold(Bold bold)
        {
            return $"<b>{ReturnTextContentOrEmptyString(bold)}{VisitAndReturnChildred(bold)}</b>";
        }

        public override string VisitItalic(Italic italic)
        {
            return $"<i>{ReturnTextContentOrEmptyString(italic)}{VisitAndReturnChildred(italic)}</i>";
        }

        public override string VisitHeaderLevelOne(HeaderLevelOne headerLevelOne)
        {
            return $"<h1>{ReturnTextContentOrEmptyString(headerLevelOne)}</h1>";
        }

        public override string VisitHeaderLevelTwo(HeaderLevelTwo headerLevelTwo)
        {
            return $"<h2>{ReturnTextContentOrEmptyString(headerLevelTwo)}</h2>";
        }

        public override string VisitHeaderLevelThree(HeaderLevelThree headerLevelThree)
        {
            return $"<h3>{ReturnTextContentOrEmptyString(headerLevelThree)}</h3>";
        }

        public override string VisitHeaderLevelFour(HeaderLevelFour headerLevelFour)
        {
            return $"<h4>{ReturnTextContentOrEmptyString(headerLevelFour)}</h4>";
        }

        public override string VisitHeaderLevelFive(HeaderLevelFive headerLevelFive)
        {
            return $"<h5>{ReturnTextContentOrEmptyString(headerLevelFive)}</h5>";
        }

        public override string VisitHeaderLevelSix(HeaderLevelSix headerLevelSix)
        {
            return $"<h6>{ReturnTextContentOrEmptyString(headerLevelSix)}</h6>";
        }

        public override string VisitPlainText(PlainText plainText)
        {
            return ReturnTextContentOrEmptyString(plainText);
        }

        public override string VisitListItem(ListItem listItem)
        {
            return $"<li>{ReturnTextContentOrEmptyString(listItem)}</li>";
        }

        public override string VisitOrderedList(OrderedList orderedList)
        {
            return $"<ol>{VisitAndReturnChildred(orderedList)}</ol>";
        }

        public override string VisitUnOrderedList(UnOrderedList unorderedList)
        {
            return $"<ul>{VisitAndReturnChildred(unorderedList)}</ul>";
        }

        public override string VisitCodeBlock(CodeBlock codeBlock)
        {
            return $"<pre><code>{ReturnTextContentOrEmptyString(codeBlock)}</code><pre>";
        }

        public override string VisitInlineCode(InlineCode inlineCode)
        {
            return $"<code>{ReturnTextContentOrEmptyString(inlineCode)}</code>";
        }

        public override string VisitEmptyLine(EmptyLine emptyLine)
        {
            return $"<br></br>";
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

        private string ReturnTextContentOrEmptyString(IHtmlTag tag)
        {
            return tag.TextContent is not null ? tag.TextContent : "";
        }
    }
}