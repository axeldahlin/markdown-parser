using System.Text.RegularExpressions;
namespace dotnet_visitor
{
    public abstract class AbstractVisitor
    {
        public abstract string VisitRoot(Root root);
        public abstract string VisitParagraph(Paragraph paragraph);
        public abstract string VisitBold(Bold bold);
        public abstract string VisitItalic(Italic italic);
        public abstract string VisitHeaderLevelOne(HeaderLevelOne headerLevelOne);
        public abstract string VisitHeaderLevelTwo(HeaderLevelTwo headerLevelTwo);
        public abstract string VisitHeaderLevelThree(HeaderLevelThree headerLevelThree);
        public abstract string VisitHeaderLevelFour(HeaderLevelFour headerLevelFour);
        public abstract string VisitHeaderLevelFive(HeaderLevelFive headerLevelFive);
        public abstract string VisitHeaderLevelSix(HeaderLevelSix headerLevelSix);
        public abstract string VisitPlainText(PlainText plainText);
        public abstract string VisitUnOrderedListItem(UnOrderedListItem unOrderedListItem);
        public abstract string VisitOrderedListItem(OrderedListItem orderedListItem);
        public abstract string VisitOrderedList(OrderedList orderedList);
        public abstract string VisitUnOrderedList(UnOrderedList unOrderedList);
        public abstract string VisitCodeBlock(CodeBlock codeBlock);
        public abstract string VisitInlineCode(InlineCode inlineCode);
        public abstract string VisitEmptyLine(EmptyLine emptyLine);
        
    }
}