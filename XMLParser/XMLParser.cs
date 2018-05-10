using System;
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

            //GetProps(doc);

            ParseXML(doc.GetElementsByTagName("nameSpace")[0]);
        }

        /// <summary>
        /// Recursively parses a given <see cref="XmlNode"/>.
        /// </summary>
        /// <param name="root"><see cref="XmlNode"/> node to start from.</param>
        public static void ParseXML(XmlNode root)
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
        private static void Search(XmlNode node)
        {
            if (node.Name == "nameSpace")
                if (node.Attributes["name"] != null)
                    if (node.Attributes["name"].Value == "SASH.Custom")
                        Console.WriteLine(node.Attributes["name"].Value);

            if (node.Name == "ref")
                if (node.Attributes["using"] != null)
                    Console.WriteLine(node.Attributes["using"].Value);

            if (node.Name == "class")
                if (node.Attributes["protectionLevel"] != null && node.Attributes["type"] != null &&
                    node.Attributes["name"] != null)
                    if (node.Attributes["protectionLevel"].Value != "" && node.Attributes["type"].Value != "" &&
                        node.Attributes["name"].Value != "")
                        Console.WriteLine($"{node.Attributes["protectionLevel"].Value} {node.Attributes["type"].Value} " +
                            $"{node.Attributes["name"].Value}");
        }
    }
}
