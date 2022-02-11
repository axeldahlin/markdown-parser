using System.Text.RegularExpressions;
using System.Text;

namespace dotnet_visitor
{
    public class TopDownParser
    {
        public TagFactory TagFactory { get; set; }
        public TopDownParser()
        {
            TagFactory = new TagFactory();
        }

        public Root Parse(string textToParse)
        {
            var arrayOfText = textToParse.Split("\n");
            List<IHtmlTag> withFirstLayerParsed = arrayOfText.Select(text => GenerateIHtmlTagsForFirstLayer(text)).ToList();
            List<IHtmlTag> withOrderedListItemsInOrderedLists = PutOrderedListItemsInOrderedLists(withFirstLayerParsed);
            List<IHtmlTag> withUnOrderedListItemsInUnOrderedLists = PutUnOrderedListItemsInUnOrderedLists(withOrderedListItemsInOrderedLists);
            List<IHtmlTag> withCodeBlocks = FindAndCreateCodeBlocks(withUnOrderedListItemsInUnOrderedLists);
            List<IHtmlTag> fullyParsed = withCodeBlocks.Select(tag =>
            {
                if (tag.GetType().FullName == "dotnet_visitor.PlainText")
                {
                    return RecursivlyParsePlainTextTags(tag);
                }
                else
                {
                    return tag;
                }
            }).ToList();


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


        private List<IHtmlTag> FindAndCreateCodeBlocks(List<IHtmlTag> htmlTags)
        {
            var foundOpeningCodeBlock = false;
            int indexOfNewCodeBlock = 1;

            List<IHtmlTag> updatedListOfHtmlTags = new List<IHtmlTag>();
            List<IHtmlTag> listOfTagsToPutInCodeBlock = new List<IHtmlTag>();

            for (int i = 0; i < htmlTags.Count(); i++)
            {
                var copyOfItem = htmlTags.GetRange(i, 1);

                // if closing code block element is found
                // put text of all elements in listOfTagsToPutInCodeBlock
                // in the prevously created CodeBlock
                if (htmlTags[i].GetType().FullName == "dotnet_visitor.PlainText"
                && htmlTags[i].TextToParse == "```"
                && foundOpeningCodeBlock)
                {
                    StringBuilder sb = new StringBuilder();
                    listOfTagsToPutInCodeBlock.ForEach(tag =>
                    {
                        if (tag.TextContent is not null) sb.AppendLine(tag.TextContent);
                        if (tag.TextToParse is not null) sb.AppendLine(tag.TextToParse);
                    });
                    updatedListOfHtmlTags[indexOfNewCodeBlock].SetTextContent(sb.ToString());
                    listOfTagsToPutInCodeBlock.Clear();
                    foundOpeningCodeBlock = false;
                    continue;
                }

                if (foundOpeningCodeBlock)
                {
                    listOfTagsToPutInCodeBlock.Add(copyOfItem[0]);
                    continue;
                }

                if (htmlTags[i].GetType().FullName == "dotnet_visitor.PlainText"
                && htmlTags[i].TextToParse == "```"
                && !foundOpeningCodeBlock)
                {
                    updatedListOfHtmlTags.Add(new CodeBlock());
                    foundOpeningCodeBlock = true;
                    indexOfNewCodeBlock = updatedListOfHtmlTags.Count() - 1;
                    continue;
                }

                if (!foundOpeningCodeBlock)
                {
                    updatedListOfHtmlTags.Add(copyOfItem[0]);
                    continue;
                }

            }

            // if foundOpeningCodeBlock after iteration is finished 
            // make the created code block into a PlainText
            // and add listOfTagsToPutInCodeBlock to updatedListOfHtmlTags
            if (foundOpeningCodeBlock)
            {
                updatedListOfHtmlTags[indexOfNewCodeBlock] = new PlainText(textContent: "```", textToParse: "");
                foreach (var item in listOfTagsToPutInCodeBlock)
                {
                    updatedListOfHtmlTags.Add(item);
                }
            }

            return updatedListOfHtmlTags;
        }


        private IHtmlTag RecursivlyParsePlainTextTags(IHtmlTag tag)
        {

            // Bold
            if (DoesHaveTwoAsteriks(text: tag.TextToParse))
            {
               return FixEverything(tag: tag, tagType: TagType.Bold, syntaxToFind: "**");


            }
            else if (DoesHaveOneAsteriks(text: tag.TextToParse))
            {

               return FixEverything(tag: tag, tagType: TagType.Italic, syntaxToFind: "*");

                // var plainTextToReturn = new PlainText(textToParse: "");
                // int indexOfOpening = tag.TextToParse.IndexOf("*");
                // var textBeforeOpening = tag.TextToParse.Substring(0, indexOfOpening);
                // var textBeforeOpeningTag = new PlainText(textContent: textBeforeOpening, textToParse: "");

                // var textAfterOpening = tag.TextToParse.Substring(indexOfOpening + 1);

                // var indexOfClosing = textAfterOpening.IndexOf("*");

                // var toBold = textAfterOpening.Substring(0, indexOfClosing);

                // var boldChild = new Bold(textContent: toBold, textToParse: "");


                // var textContinueToParse = textAfterOpening.Substring(indexOfClosing + 1);

                // var plainTextToBeParsed = RecursivlyParsePlainTextTags(new PlainText(textToParse: textContinueToParse));


                // if (textBeforeOpeningTag.TextContent != "") { plainTextToReturn.AddChild(textBeforeOpeningTag); }
                // plainTextToReturn.AddChild(boldChild);
                // if (plainTextToBeParsed.TextContent != "") { plainTextToReturn.AddChild(plainTextToBeParsed); }


                // return plainTextToReturn;

            }
            else
            {
                return new PlainText(textContent: tag.TextToParse, textToParse: "");
            }


        }

        private IHtmlTag FixEverything(IHtmlTag tag, TagType tagType, string syntaxToFind)
        {
            int syntaxLength = syntaxToFind.Length;

            var plainTextToReturn = new PlainText(textToParse: "");
            int indexOfOpening = tag.TextToParse.IndexOf(syntaxToFind);
            var textBeforeOpening = tag.TextToParse.Substring(0, indexOfOpening);
            var textBeforeOpeningTag = new PlainText(textContent: textBeforeOpening, textToParse: "");

            var textAfterOpening = tag.TextToParse.Substring(indexOfOpening + 2);

            var indexOfClosing = textAfterOpening.IndexOf(syntaxToFind);

            var syntaxToCreate =  TagFactory.GetTag(type: tagType);

            var textContentOfSyntax = textAfterOpening.Substring(0, indexOfClosing);

            syntaxToCreate.SetTextContent(textContentOfSyntax);

           
            var textContinueToParse = textAfterOpening.Substring(indexOfClosing + 2);

            var plainTextToBeParsed = RecursivlyParsePlainTextTags(new PlainText(textToParse: textContinueToParse));

            if (textBeforeOpeningTag.TextContent != "") { plainTextToReturn.AddChild(textBeforeOpeningTag); }
            plainTextToReturn.AddChild(syntaxToCreate);
            if (plainTextToBeParsed.TextContent != "") { plainTextToReturn.AddChild(plainTextToBeParsed); }


            return plainTextToReturn;

        }





        public bool DoesHaveTwoAsteriks(string text)
        {
            string strRegex = @"\*{2}.*\*{2}";
            var re = new Regex(strRegex);
            var result = re.Match(text);
            return result.Success;
        }

        public bool DoesHaveOneAsteriks(string text)
        {
            string strRegex = @"\*{1}.*\*{1}";
            var re = new Regex(strRegex);
            var result = re.Match(text);
            return result.Success;
        }








        // method for recursive parsing of the rest

    }
}