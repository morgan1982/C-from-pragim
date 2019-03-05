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

        public static string FontSize { get; } = "font-size([^px]*px;)";
        public static string FontWeight { get; } = "font-weight([^px]*px;)";

        public static string Color { get; } = "(?<!-)color.*?[;\"]";
        public static string BackGroundColor { get; } = "background-color:(.*?);";
        public static string FontFamily { get; } = "font-family:(.*?);";
        public static string PickParentAttributes { get; } = "<(.*?)>";
        public static string PickStyle { get; } = "(?<=style=\")(.*)(?=\")";
        
        public static Regex CreateRegex(string args) => new Regex(args);
    }

    internal static class InvokeRegex
    {
        static InvokeRegex()
        { 
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

    internal class InnerTags : RegexFactory
    {
        public static Dictionary<string, string> RegexStrings(string args)
        {
            return new Dictionary<string, string>() {
                { "background-color", RegexFactory.BackGroundColor },
                { "color", RegexFactory.Color },  
                { "font-family", RegexFactory.FontFamily },  
                { "font-size", RegexFactory.FontSize },
                { "font-weight", RegexFactory.FontWeight }
            };

        }
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
//   private Dictionary<int,string> _map;
//   public Dictionary<int,string> Map { get { return _map; } }
//   public Example() { _map = new Dictionary<int,string>(); }
// }
//     }


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
                styles.Append(element.GetAttribute("style") + "; ");
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
                StringBuilder parentStyleBuilder = new StringBuilder(element.GetAttribute("style")); // style of the parent
                if (parentStyleBuilder.Length != 0)
                {
                    string[] attributes = new string[] { 
                        "font-size",
                        "background-color",
                        "color",
                        "font-family" };

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
                    attributesBuilder.Insert(0, parentStyleBuilder); 
                }
                element.SetAttribute("style", attributesBuilder.ToString()); // sets the styleoftheParent
                

                // Clear the inner of the element
                var contentRegex = new Regex("__[^__]*__");
                string contentFinal = contentRegex.Match(element.OuterHtml).Value; // use this in next method to replace children with this string as content
                element.InnerHtml = contentFinal;
            }
        }


        // Sanitizer MainEngine
        private static void MainEngine(List<IElement> elements)
        {
            var attributesBuilder = new StringBuilder();
            foreach (var element in elements)
            {

                //  PICK THE STYLE OF THE OUTER ELEMENT
                var parentAttributes = element.Attributes;

                // convert to a method
                string style = "";
                foreach (var attribute in parentAttributes)
                {
                    if (attribute.Name == "style")
                    {
                        style = attribute.Value;
                    }
                }

                // APPEND THE STYLE ATTRIBUTES TO THE BUILDER
                attributesBuilder.Append(style);

                // GRAP ALL THE CHILDREN STYLES
                attributesBuilder.Clear();
                var children = element.Children.AsEnumerable();
                var childrenStyles = new StringBuilder();
                foreach (var child in children)
                {
                    var attributes = child.Attributes;
                    foreach (var attribute in attributes)
                    {
                        if (attribute.Name == "style")
                        {
                            childrenStyles.Append(attribute.Value);
                        }
                    }
                }
                string inner = element.InnerHtml;

                // pick the parent attributes
                var pickTheParentRegex = RegexFactory.CreateRegex(RegexFactory.PickParentAttributes);
                string parentElement = pickTheParentRegex.Match(element.OuterHtml).Value;

                bool parentHasStyle = parentElement.Contains("style");

                if (!parentHasStyle)
                {
                    element.SetAttribute("style", "");
                }

                string newParent = pickTheParentRegex.Match(element.OuterHtml).Value; // Outer
                // pick the parent

                var PickStyle = RegexFactory.CreateRegex(RegexFactory.PickStyle);
                string parentStyle = PickStyle.Match(newParent).Value;

                attributesBuilder.Append(parentStyle); // has to pick only the values contains an empty string or the values of the style attribute


                //Refactor checkers

                Dictionary <string, string> cssAtrributes = new Dictionary<string, string>()
                {

                    { "<b>", "font-weight: bold;" },
                    { "<u>", "text-decoration: underline" },
                    { "<i>", "font-style: italics" },
                    { "<strike>", "text-decoration: strike-through" }         
                };

                foreach(KeyValuePair<string, string> entry in cssAtrributes)
                {
                    if (inner.Contains(entry.Key))
                    {
                        if (entry.Key == "<b>" | entry.Key == "<u>" | entry.Key == "<strike>" | entry.Key == "<i>")
                        {
                            // assumes that the entry does not exists to the parent element css attributes!
                            attributesBuilder.Insert(0, entry.Value);
                        }else
                        {
                            var regex = RegexFactory.CreateRegex(entry.Value);

                            string outerHtmlCssAttribute = regex.Match(parentStyle).Value;
                            string innerHtmlCssAttribute = regex.Match(inner).Value;
                            if (outerHtmlCssAttribute == "")
                            {
                                attributesBuilder.Insert(0, innerHtmlCssAttribute);
                            }else
                            {
                                attributesBuilder.Replace(outerHtmlCssAttribute, innerHtmlCssAttribute);
                            }
                        }

                    }
                }
                element.SetAttribute("style", attributesBuilder.ToString());
                element.InnerHtml = "";
            }
        }
    }

}
