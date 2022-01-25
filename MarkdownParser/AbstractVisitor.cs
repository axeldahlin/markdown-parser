using System.Text.RegularExpressions;
namespace dotnet_visitor
{
    public abstract class AbstractVisitor
    {
        public abstract string VisitParagraph(Paragraph paragraph);
        public abstract string VisitBold(Bold bold);
        public abstract string VisitItalic(Italic italic);
        public abstract string VisitRoot(Root root);
        public abstract string VisitHeaderLevelOne(HeaderLevelOne headerLevelOne);
    }
}