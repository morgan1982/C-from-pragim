using AngleSharp;
using AngleSharp.Html.Dom;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using AngleSharp.Html.Parser;
using System.Text.RegularExpressions;
using AngleSharp.Dom;

namespace AngleShardDemo1
{
    internal class RegexFactory
    {
        public static string FontSize { get; } = "font-size[^px]*(px|;)";
        public static string FontWeight { get; } = "font-weight[^px]*(px|;)";

        public static string Color { get; } = "(?<!-)color.*?(;|$)";
        public static string BackGroundColor { get; } = "background-color:.*?(;|$)";
        public static string FontFamily { get; } = "font-family:.*?(;|$)";
    }

    internal static class InvokeRegex
    {
        static InvokeRegex()
        { 
            // does not invoke regexes rather than the string that is used to inovce the appropriate regex
            _attributes = new Dictionary<string,string>() {
                { "background-color", RegexFactory.BackGroundColor },
                { "color", RegexFactory.Color },  
                { "font-family", RegexFactory.FontFamily },  
                { "font-size", RegexFactory.FontSize },
                { "font-weight", RegexFactory.FontWeight }                
            }; 
        }
        private static Dictionary<string,string> _attributes;
        public static Dictionary<string,string> Attributes { get { return _attributes; } } 

        ///<summary>
        ///creates the regex using the atribute in the constructor 
        ///</summary>
        ///<example>
        ///<code>
        ///getRegex("background-color")
        ///</code>
        ///</example>

        public static Regex getRegex(string valueString)
        {
            string reg = "";
            foreach (var regexString in _attributes)
            {
                if(valueString == regexString.Key)
                {
                    reg = regexString.Value;
                }
            }
            return new Regex(reg);
        }
    }

    internal class InnerTags
    {
        public static Dictionary<string, string> GetTags()
        {
            return new Dictionary<string, string>() {
                { "<b>", "font-weight: bold;" },
                { "<u>", "text-decoration: underline;" },
                { "<i>", "font-style: italics;" },
                { "<strike>", "text-decoration: strike-through;" }    
            };
        }
    } 

    class Program
    {
        static void Main(string[] args)
        {

            using (StreamReader reader = new StreamReader("C:\\templates\\courtyard.html"))
            {
                string content = reader.ReadToEnd();

                var parser = new HtmlParser();
                var document = parser.ParseDocument(content);

                var elements = ListElementForModification(document);
                
                // TESTING GROUND
                // var descriptions = document.GetElementsByName("__DESCRIPTION__");
                // foreach (var el in descriptions)
                // {
                //     var style = el.GetAttribute("style");
                //     var regex = InvokeRegex.getRegex("font-size");
                //     string color = regex.Match(style).Value;
                // }
                RecursiveEngine(elements);
                // MainEngine(elements);


                var final = document.DocumentElement.OuterHtml;
              
                Console.WriteLine(final);
            }

            Console.ReadLine();

        }

        private static List<IElement> ListElementForModification(IHtmlDocument document)
        {
            // use a main StringBuilder and add and remove string to it.
            var ElementsForModification = new List<IElement>()
            {
                document.GetElementById("__DESCRIPTION__"),
                document.GetElementById("__MANUFACTURER__")
            };

            var tags = new String[] { "__DESCRIPTION__", "__FEATURES__", "__MANUFACTURER__" };
            foreach(var tag in tags)
            {
                ElementsForModification.AddRange(document.GetElementsByName(tag).AsEnumerable());
            }
            ElementsForModification.RemoveAll(element => element == null);
            return ElementsForModification;
        }

        private static void RecursiveCore(StringBuilder styles, IElement parent)
        { 
            foreach (var element in parent.Children)
            {
                var styleString = element.GetAttribute("style");
                if (styleString != null)
                {
                    var lastIndex = styleString[styleString.Length -1];
                    styles.Append(lastIndex == ';' ? styleString + " " : styleString + "; ");
                }
                RecursiveCore(styles, element);
            }
        }

        private static void RecursiveEngine(List<IElement> elements)
        {
            var attributesBuilder = new StringBuilder();
            foreach (var element in elements)
            {
                attributesBuilder.Clear();
                string innerHtml = element.InnerHtml;
                // Popoulates the attributtes builder with all style attributes inside children for each element
                RecursiveCore(attributesBuilder, element);

                // Checks the children for the existance of b, i, u, or stike tag and populates the builder with the corresponding attribute
                Dictionary<string,string> tags =  InnerTags.GetTags();
                foreach (var item in tags)
                {
                    if(innerHtml.Contains(item.Key))
                    {
                        attributesBuilder.Insert(0, item.Value);
                    }                                       
                }

                //check parent for style
                string parentStyle = element.GetAttribute("style");
                var parentStyleBuilder = new StringBuilder(parentStyle);
                  // style of the parent
                if (parentStyleBuilder.Length != 0)
                {
                    string[] attributes = new string[] { 
                        "font-size",
                        "background-color",
                        "color",
                        "font-family",
                        "font-weight" };

                    // clean the parent styles if the same attributes exists to childs
                    foreach (var attr in attributes)
                    {
                        string innerAttributes = attributesBuilder.ToString();
                        if(parentStyle.Contains(attr) && innerAttributes.Contains(attr))
                        {
                            // create regex for the corrensponing attr
                            var attributeRegex = InvokeRegex.getRegex(attr);
                            string parentCssAttribute = attributeRegex.Match(parentStyle).Value;
                            // string childCssAttribute = attributeRegex.Match(innerAttributes).Value;
                            parentStyleBuilder.Replace(parentCssAttribute, ""); // removes the current attribute from parent                           
                        }
                    }
                    // inserts remaining parentStyles to the mainAttributesBuilder
                    attributesBuilder.Append(parentStyleBuilder); 
                }
                element.SetAttribute("style", attributesBuilder.ToString()); // append the new parrent attributes to the element
                
                // Clear the inner of the element
                var contentRegex = new Regex("__[^__]*__");
                element.InnerHtml = contentRegex.Match(element.OuterHtml).Value; // use this in next method to replace children with this string as content
            }
        }
    }
}
