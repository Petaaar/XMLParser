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
        public static void Main()
        {
            new XMLParser();
        }

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
        /// Recursively parses a given <see cref="XmlNode"/>.
        /// </summary>
        /// <param name="root"><see cref="XmlNode"/> node to start from.</param>
        public void ParseXML(XmlNode root)
        {
            if (root is XmlElement)
            {
                Search(root);
            }

            if (root.HasChildNodes)
                ParseXML(root.FirstChild);
            if (root.NextSibling != null)
                ParseXML(root.NextSibling);

        }

        /// <summary>
        /// Searches a given <see cref="XmlNode"/> and checks it's attributes.
        /// </summary>
        /// <param name="node"><see cref="XmlNode"/> to search and parse.</param>
        private void Search(XmlNode node)
        {
            if (node.Name == "nameSpace") //parsing the working namespace
                if (node.Attributes["name"] != null)
                    if (node.Attributes["name"].Value == "SASH.Custom")
                        Console.WriteLine(node.Attributes["name"].Value);

            if (node.Name == "ref")
                if (node.Attributes["using"] != null) //parsing all "using" directives
                    Console.WriteLine(node.Attributes["using"].Value);

            if (node.Name == "class") // parsing the class name/type/protection level
                if (node.Attributes["protectionLevel"] != null && node.Attributes["type"] != null &&
                    node.Attributes["name"] != null)
                    if (node.Attributes["name"].Value != "")
                        Console.WriteLine($"{node.Attributes["protectionLevel"].Value} {node.Attributes["type"].Value} " +
                            $"{node.Attributes["name"].Value}");

            if (node.Name == "item") //parsing PRIVATE fields
                if (node.Attributes["type"] != null && node.Attributes["returnType"] != null && node.InnerText != null)
                    if (node.InnerText != " ")
                        Console.WriteLine($"private {node.Attributes["type"].Value} {node.Attributes["returnType"].Value} " + 
                            $"{node.InnerText}");

            if (node.Name == "encapsulate") //encapsulation property
                if (node.InnerText != null)
                    if (node.InnerText.ToLower() == "true" || node.InnerText.ToLower() == "false")
                        Console.WriteLine(node.InnerText);
                    else Console.WriteLine("NO ENCAPSULATION!");

            if (node.Name == "publicItem")//parsing publicItems property
                if (node.Attributes["type"] != null && node.Attributes["returnType"] != null && node.InnerText != null)
                    if (node.Attributes["type"].Value != " " && node.Attributes["returnType"].Value != " " && node.InnerText != " ")
                        Console.WriteLine($"public {node.Attributes["type"].Value} {node.Attributes["returnType"].Value}" +
                            $" {node.InnerText}");

            if (node.Name == "method")
                if (node.Attributes["type"] != null && node.Attributes["returnType"] != null && node.InnerText != null)
                    if (node.Attributes["type"].Value != " " && node.Attributes["returnType"].Value != " " && node.InnerText != " ")
                        Console.WriteLine($"private {node.Attributes["type"].Value} {node.Attributes["returnType"].Value}" +
                            $" {node.InnerText}");

            if (node.Name == "publicMethod")
                if (node.Attributes["type"] != null && node.Attributes["returnType"] != null && node.InnerText != null)
                    if (node.Attributes["type"].Value != " " && node.Attributes["returnType"].Value != " " && node.InnerText != " ")
                        Console.WriteLine($"private {node.Attributes["type"].Value} {node.Attributes["returnType"].Value}" +
                            $" {node.InnerText}");
        }
    }
}
