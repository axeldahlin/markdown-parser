namespace dotnet_visitor
{
    public enum TagType
    {
        Bold,
        Italic
    }
    public class TagFactory
    {
        public IHtmlTag GetTag(TagType type)
        {
            switch (type)
            {
                case TagType.Bold:
                    return new Bold("");
                case TagType.Italic:
                    return new Italic("");
                default:
                    throw new NotSupportedException();
            }
        }
    }
}