using System;
using System.Xml;

namespace XMLParser
{
    /// <summary>
    /// A class to parse from XML to C# code.
    /// </summary>
    class XMLParser
    {
        #region Private

        #region NO
        //A private booleans to determine if anything listed down is equal to NULL
        private readonly bool noDependencies = true;

        private readonly bool noPrivateFields = true;

        private readonly bool noPublicFields = true;

        private readonly bool noPrivateMethods = true;

        private readonly bool noPublicMethods = true;
        #endregion NO

        private const string tab = "    ";

        private static string Namespace(string value) => $"namespace {value}" + "\n{";

        private static string Using(string collection) => $"{tab}using {collection};";

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
            if (node.Name == "ref" && !this.noDependencies) //non-empty "dependencies" tag..
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
            if (node.Name == "item" && !this.noPrivateFields) //parsing PRIVATE fields
                if (node.Attributes["type"] != null && node.Attributes["returnType"] != null && node.InnerText != null)
                    return new ItemParser(node.Attributes["type"].Value, node.Attributes["returnType"].Value, node.InnerText).Parse();
                else if (node.Attributes["type"] == null && node.Attributes["returnType"] != null && node.InnerText != null)
                    return new ItemParser(node.Attributes["returnType"].Value, node.InnerText).Parse();
            #endregion Item

            #region Encapsulation
            if (node.Name == "encapsulate") //encapsulation property
                if (node.InnerText != null)
                    if (node.InnerText.ToLower() == "true" || node.InnerText.ToLower() == "false")
                        Console.WriteLine(node.InnerText);
                    else Console.WriteLine("NO ENCAPSULATION!");
            #endregion Encapsulation

            #region PublicItem
            if (node.Name == "publicItem" && !this.noPublicFields)//parsing publicItems property
                if (node.Attributes["type"] != null && node.Attributes["returnType"] != null && node.InnerText != null)
                    return new ItemParser(node.Attributes["type"].Value, node.Attributes["returnType"].Value, node.InnerText, false).Parse();
                else if (node.Attributes["type"] == null && node.Attributes["returnType"] != null && node.InnerText != null)
                    return new ItemParser(node.Attributes["returnType"].Value, node.InnerText, false).Parse();
            #endregion PublicItem

            #region Method
            if (node.Name == "method" && !this.noPrivateMethods)
                if (node.Attributes["type"] != null && node.Attributes["returnType"] != null && node.InnerText != null)
                    if (node.Attributes["type"].Value != " " && node.Attributes["returnType"].Value != " " && node.InnerText != " ")
                        Console.WriteLine($"private {node.Attributes["type"].Value} {node.Attributes["returnType"].Value}" +
                            $" {node.InnerText}");
            #endregion Method

            #region PublicMethod
            if (node.Name == "publicMethod" && !this.noPublicMethods)
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

            #region No-set
            //setting the "NO"-region variables

            if (doc.GetElementsByTagName("dependencies")[0] != null &&
                doc.GetElementsByTagName("dependencies")[0].HasChildNodes)
                this.noDependencies = false;

            if (doc.GetElementsByTagName("privateFields")[0] != null &&
                doc.GetElementsByTagName("privateFields")[0].HasChildNodes)
                this.noPrivateFields = false;

            if (doc.GetElementsByTagName("publicFields")[0] != null &&
                doc.GetElementsByTagName("publicFields")[0].HasChildNodes)
                this.noPublicFields = false;

            if (doc.GetElementsByTagName("privateMethods")[0] != null &&
                doc.GetElementsByTagName("privateMethods")[0].HasChildNodes)
                this.noPrivateMethods = false;

            if (doc.GetElementsByTagName("publicMethods")[0] != null &&
                doc.GetElementsByTagName("publicMethods")[0].HasChildNodes)
                this.noPublicMethods = false;

            #endregion

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
                if (searchRes != string.Empty)
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
