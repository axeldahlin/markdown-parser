using System.Text.RegularExpressions;

namespace dotnet_visitor
{
    public class TopDownParser
    {
        public TopDownParser()
        {

        }

        public Root Parse(string textToParse)
        {
            var arrayOfText = textToParse.Split("\n");
            List<IHtmlTag> withFirstLayerParsed = arrayOfText.Select(text => GenerateIHtmlTagsForFirstLayer(text)).ToList();
            List<IHtmlTag>  withOrderedListItemsInOrderedLists= PutOrderedListItemsInOrderedLists(withFirstLayerParsed);
            List<IHtmlTag>  withUnOrderedListItemsInUnOrderedLists= PutUnOrderedListItemsInUnOrderedLists(withOrderedListItemsInOrderedLists);


            return new Root("");
        }


        private IHtmlTag GenerateIHtmlTagsForFirstLayer(string text)
        {
            var orderedListPattern = @"^\d{1,9}\. .*";
            Match match = Regex.Match(text, orderedListPattern, RegexOptions.IgnoreCase);
            if (match.Success)
            {
                var indexOfSpace = text.IndexOf(" ");
                var listNumber = Int16.Parse(text.Substring(0, indexOfSpace - 1));
                var textContent = text.Substring(indexOfSpace + 1, text.Length - indexOfSpace - 1);
                return new OrderedListItem(listNumber: listNumber, textContent: textContent);
            }
            else if (text.Length >= 7 && text.Substring(0, 7) == "###### ")
            {
                return new HeaderLevelSix(textContent: text.Substring(7, text.Length - 7));
            }
            else if (text.Length >= 6 && text.Substring(0, 6) == "##### ")
            {
                return new HeaderLevelFive(textContent: text.Substring(6, text.Length - 6));
            }
            else if (text.Length >= 5 && text.Substring(0, 5) == "#### ")
            {
                return new HeaderLevelFour(textContent: text.Substring(5, text.Length - 5));
            }
            else if (text.Length >= 4 && text.Substring(0, 4) == "### ")
            {
                return new HeaderLevelThree(textContent: text.Substring(4, text.Length - 4));
            }
            else if (text.Length >= 3 && text.Substring(0, 3) == "## ")
            {
                return new HeaderLevelTwo(textContent: text.Substring(3, text.Length - 3));
            }
            else if (text.Length >= 2 && text.Substring(0, 2) == "# ")
            {
                return new HeaderLevelOne(textContent: text.Substring(2, text.Length - 2));
            }
            else if (text == "")
            {
                return new EmptyLine();
            }
            else if (text.Length >= 2 && text.Substring(0, 2) == "* ")
            {
                return new UnOrderedListItem(textContent: text.Substring(2, text.Length - 2));
            }
            else
            {
                return new PlainText(textToParse: text);
            }
        }

        private List<IHtmlTag> PutOrderedListItemsInOrderedLists(List<IHtmlTag> htmlTags)
        {
            var foundList = false;
            int indexOfNewOrderedList;

            List<IHtmlTag> updatedListOfHtmlTags = new List<IHtmlTag>();

            for (int i = 0; i < htmlTags.Count(); i++)
            {
                if (htmlTags[i].GetType().FullName == "dotnet_visitor.OrderedListItem")
                {
                    var copyOfItem = htmlTags.GetRange(i, 1);

                    if (foundList) 
                    {
                        updatedListOfHtmlTags.Last().AddChild(copyOfItem[0]);
                    }
                    
                    if (!foundList)
                    {
                        updatedListOfHtmlTags.Add(new OrderedList(children: copyOfItem));
                        foundList = true;
                        indexOfNewOrderedList = i;
                    }

                    if (htmlTags.Count() > (i + 1)
                    && htmlTags[i + 1].GetType().FullName != "dotnet_visitor.OrderedListItem")
                    {
                        foundList = false;
                    }
                } 
                else 
                {
                    updatedListOfHtmlTags.Add(htmlTags[i]);
                } 
            }
            return updatedListOfHtmlTags;
        }


        private List<IHtmlTag> PutUnOrderedListItemsInUnOrderedLists(List<IHtmlTag> htmlTags)
        {
            var foundList = false;
            int indexOfNewUnOrderedList;

            List<IHtmlTag> updatedListOfHtmlTags = new List<IHtmlTag>();

            for (int i = 0; i < htmlTags.Count(); i++)
            {
                if (htmlTags[i].GetType().FullName == "dotnet_visitor.UnOrderedListItem")
                {
                    var copyOfItem = htmlTags.GetRange(i, 1);

                    if (foundList) 
                    {
                        updatedListOfHtmlTags.Last().AddChild(copyOfItem[0]);
                    }
                    
                    if (!foundList)
                    {
                        updatedListOfHtmlTags.Add(new UnOrderedList(children: copyOfItem));
                        foundList = true;
                        indexOfNewUnOrderedList = i;
                    }

                    if (htmlTags.Count() > (i + 1)
                    && htmlTags[i + 1].GetType().FullName != "dotnet_visitor.UnOrderedListItem")
                    {
                        foundList = false;
                    }
                } 
                else 
                {
                    updatedListOfHtmlTags.Add(htmlTags[i]);
                } 
            }
            return updatedListOfHtmlTags;
        }

        // method for creating code blocks


        // method for recursive parsing of the rest

    }
}