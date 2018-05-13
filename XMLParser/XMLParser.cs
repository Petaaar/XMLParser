using System;
using System.IO;
using System.Xml;

namespace XMLParser
{
    /// <summary>
    /// A class to parse from XML to C# code.
    /// </summary>
    class XMLParser
    {
        #region Private

        private const string tab = "    ";

        private string Namespace(string value) => $"namespace {value}" + "\n{";

        private string Using(string collection) => $"{tab}using {collection};";

        /// <summary>
        /// Searches a given <see cref="XmlNode"/> and checks it's attributes.
        /// </summary>
        /// <param name="node"><see cref="XmlNode"/> to search and parse.</param>
        private string Search(XmlNode node)
        {
            #region Namespace
            if (node.Name == "nameSpace") //parsing the working namespace
                if (node.Attributes["name"] != null)
                    if (node.Attributes["name"].Value == "SASH.Custom")
                        return Namespace(node.Attributes["name"].Value);
                    else return ("Invalid namespace!");
            #endregion Namespace

            #region Reference
            if (node.Name == "ref")
                if (node.Attributes["using"] != null) //parsing all "using" directives
                    return Using(node.Attributes["using"].Value);
            #endregion Reference

            #region Class
            if (node.Name == "class") // parsing the class name/type/protection level
                if (node.Attributes["protectionLevel"] != null && node.Attributes["type"] != null && node.Attributes["name"] != null)
                    return new ClassParser(node.Attributes["protectionLevel"].Value, node.Attributes["type"].Value,
                            node.Attributes["name"].Value).Parse();
                else if (node.Attributes["protectionLevel"] != null && node.Attributes["name"] != null)
                    return new ClassParser(node.Attributes["protectionLevel"].Value, node.Attributes["name"].Value).Parse();
                else if (node.Attributes["type"] != null && node.Attributes["name"] != null)
                    return new ClassParser(node.Attributes["type"].Value, 0, node.Attributes["name"].Value).Parse();
                else return new ClassParser(node.Attributes["name"].Value).Parse();
            #endregion Class

            #region Item
            if (node.Name == "item") //parsing PRIVATE fields
                if (node.Attributes["type"] != null && node.Attributes["returnType"] != null && node.InnerText != null)
                    if (node.InnerText != " ")
                        Console.WriteLine($"private {node.Attributes["type"].Value} {node.Attributes["returnType"].Value} " + 
                            $"{node.InnerText}");
            #endregion Item

            #region Encapsulation
            if (node.Name == "encapsulate") //encapsulation property
                if (node.InnerText != null)
                    if (node.InnerText.ToLower() == "true" || node.InnerText.ToLower() == "false")
                        Console.WriteLine(node.InnerText);
                    else Console.WriteLine("NO ENCAPSULATION!");
            #endregion Encapsulation

            #region PublicItem
            if (node.Name == "publicItem")//parsing publicItems property
                if (node.Attributes["type"] != null && node.Attributes["returnType"] != null && node.InnerText != null)
                    if (node.Attributes["type"].Value != " " && node.Attributes["returnType"].Value != " " && node.InnerText != " ")
                        Console.WriteLine($"public {node.Attributes["type"].Value} {node.Attributes["returnType"].Value}" +
                            $" {node.InnerText}");
            #endregion PublicItem

            #region Method
            if (node.Name == "method")
                if (node.Attributes["type"] != null && node.Attributes["returnType"] != null && node.InnerText != null)
                    if (node.Attributes["type"].Value != " " && node.Attributes["returnType"].Value != " " && node.InnerText != " ")
                        Console.WriteLine($"private {node.Attributes["type"].Value} {node.Attributes["returnType"].Value}" +
                            $" {node.InnerText}");
            #endregion Method

            #region PublicMethod
            if (node.Name == "publicMethod")
                if (node.Attributes["type"] != null && node.Attributes["returnType"] != null && node.InnerText != null)
                    if (node.Attributes["type"].Value != " " && node.Attributes["returnType"].Value != " " && node.InnerText != " ")
                        Console.WriteLine($"private {node.Attributes["type"].Value} {node.Attributes["returnType"].Value}" +
                            $" {node.InnerText}");
            #endregion PublicMethod

            return "";
        }

        #endregion

        #region Public
        public static void Main() => new XMLParser();

        public XMLParser() : this(@"c:\Users\petar\Desktop\Example.sashs")
        {

        }

        public XMLParser(string path)
        {
            XmlDocument doc = new XmlDocument();

            doc.Load(path);

            ParseXML(doc.GetElementsByTagName("nameSpace")[0]);
        }

        /// <summary>
        /// Recursively parses <see cref="XmlDocument"/> by given <see cref="XmlNode"/> - <paramref name="root"/>.
        /// </summary>
        /// <param name="root"><see cref="XmlNode"/> node to start from.</param>
        public void ParseXML(XmlNode root)
        {
            if (root is XmlElement)
            {
                var searchRes = Search(root);
                if (searchRes != "")
                    Console.WriteLine(searchRes);
            }

            if (root.HasChildNodes)
                ParseXML(root.FirstChild);
            if (root.NextSibling != null)
                ParseXML(root.NextSibling);

        }
        #endregion
        
    }
}
