using System;
using System.Xml;
using System.Runtime.InteropServices;
using System.Windows.Forms;

//CURRENTLY 1414 lines of code - 02.06.1018;
//if the program uses System.Collections.Specialized.StringCollection instead of System.Collections.Generic.List<T>, it's more than 10 times faster!
namespace XMLParser
{
    /// <summary>
    /// A class to parse from XML to C# code.
    /// </summary>
    [System.Security.SecurityCritical]
    [ComVisible(true)]
    [Guid("f1f8ca04-77f6-46bd-80f5-0dc025fe823b")]
    public class XMLParser
    {
        #region Private

        #region NO
        //A private booleans to determine if anything listed down is equal to NULL
        private readonly bool noDependencies = true;

        private readonly bool noInherits = true;

        private readonly bool noPrivateFields = true;

        private readonly bool noPublicFields = true;

        private readonly bool noPrivateMethods = true;

        private readonly bool noPublicMethods = true;
        #endregion NO

        /// <summary>
        /// <see cref="System.Collections.Specialized.StringCollection"/> containing everything from the .sashs file.
        /// </summary>
        private readonly System.Collections.Specialized.StringCollection xmlContent;
        
        private const string error = "//ERROR:";

        private const string tab = "    ";

        private static string Namespace(string value) => $"namespace {value}";

        private static string Using(string collection) => $"{tab}Rusing {collection};";

        private static int refferences;

        private static uint privateFieldsCount;

        private static uint publicFieldsCount;

        private static uint privateMethodsCount;

        private static uint publicMethodsCount;

        private static string pathToWrite;

        public static System.Collections.Specialized.StringCollection inheritsList = new System.Collections.Specialized.StringCollection();

        public static string ClassType { get; private set; }

        /// <summary>
        /// Separates the constructor parameters.
        /// </summary>
        /// <param name="parameters">Parameters.</param>
        /// <returns><see cref="System.Collections.Generic.List{T}"/></returns>
        public static System.Collections.Specialized.StringCollection GetConstructorParameters(string parameters)
        {
            var paramsArr = parameters.Split(new char[] { '{', '}', ',' },StringSplitOptions.None);
            var returnCollection = new System.Collections.Specialized.StringCollection();

            foreach (var item in paramsArr)
            {
                if (item.StartsWith("{"))
                    returnCollection.Add(item.Remove(0, 1));
                if (item.EndsWith("}"))
                    returnCollection.Add(item.Remove(item.Length - 1, 1));
                if (item.EndsWith(","))
                    returnCollection.Add(item.Remove(item.Length - 1, 1) + ", ");
                else returnCollection.Add(item);
            }

            return returnCollection;
        }

        public static string SetPathForCreatingFile()
        {
            return pathToWrite;
        }

        /// <summary>
        /// Searches a given <see cref="XmlNode"/> and checks it's attributes.
        /// </summary>
        /// <param name="node"><see cref="XmlNode"/> to search and parse.</param>
        private string Search(XmlNode node)
        {
            #region Namespace
            if (node.Name == "nameSpace") //parsing the working namespace
                if (node.Attributes["name"] != null)
                    return Namespace(node.Attributes["name"].Value);
            #endregion Namespace

            #region Reference
            if (node.Name == "ref" && !this.noDependencies) //non-empty "dependencies" tag..
                if (node.Attributes["using"] != null) //parsing all "using" directives
                { refferences++; return Using(node.Attributes["using"].Value); }
                else return $"{error} Missing \"using\" argument in \"<ref/>\" tag.";
            #endregion Reference

            #region Inherits

            if (node.Name == "inherit" && !this.noInherits)
                if (node.Attributes["name"] != null)
                {
                    var toInherit = node.Attributes["name"].Value;
                    bool generic = false;
                    if (toInherit.EndsWith("{T}"))
                    {
                        toInherit = toInherit.Remove(toInherit.Length - 3, 3);
                        generic = true;
                    }
                    if (generic)
                        inheritsList.Add($"{toInherit}<T>");
                    else inheritsList.Add(toInherit);
                }
                else return $"Missing \"name\" argument in tag \"<inherit>\"!";

            #endregion Inherits

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

            if (node.Name == "class" && node.Attributes["type"] != null) ClassType = node.Attributes["type"].Value;

            #region Item
            if (node.Name == "item" && !this.noPrivateFields) //parsing PRIVATE fields
            {
                privateFieldsCount++;
                if (node.Attributes["type"] != null && node.Attributes["returnType"] != null
                   & node.Attributes["value"] != null && node.InnerText != null)
                    return new ItemParser(node.Attributes["type"].Value, node.Attributes["returnType"].Value,
                        node.Attributes["value"].Value, node.InnerText).Parse();
                else if (node.Attributes["type"] == null && node.Attributes["returnType"] != null &&
                        node.Attributes["value"] == null && node.InnerText != null)
                    return new ItemParser(node.Attributes["returnType"].Value, node.InnerText).Parse();
                else if (node.Attributes["type"] == null && node.Attributes["returnType"] != null &&
                        node.Attributes["value"] != null && node.InnerText != null)
                    return new ItemParser(node.Attributes["returnType"].Value, node.Attributes["value"].Value, node.InnerText).Parse();
                else if (node.Attributes["type"] != null && node.Attributes["returnType"] != null &&
                        node.Attributes["value"] == null && node.InnerText != null)
                    return new ItemParser(node.Attributes["type"].Value, node.Attributes["returnType"].Value, node.InnerText, 0).Parse();

                else return $"{error} Invalid or missing XML argument in tag \"{node.Name}\"!";
            }
            #endregion Item

            #region Encapsulation
            if (node.Name == "encapsulate") //encapsulation property
                if (node.InnerText != null)
                    if (node.InnerText.ToLower() == "true" || node.InnerText.ToLower() == "false")
                        ClassWriter.SetEncapsulation(node.InnerText);
                    else Console.WriteLine("WRONG \"ENCAPSULATION\" TAG!");
            #endregion Encapsulation

            #region PublicItem
            if (node.Name == "publicItem" && !this.noPublicFields)//parsing publicItems property
            {
                publicFieldsCount++;
                if (node.Attributes["type"] != null && node.Attributes["returnType"] != null
                   & node.Attributes["value"] != null && node.InnerText != null)
                    return new ItemParser(node.Attributes["type"].Value, node.Attributes["returnType"].Value,
                        node.Attributes["value"].Value, node.InnerText, false).Parse();
                else if (node.Attributes["type"] == null && node.Attributes["returnType"] != null &&
                        node.Attributes["value"] == null && node.InnerText != null)
                    return new ItemParser(null,node.Attributes["returnType"].Value,null, node.InnerText, false).Parse();
                else if (node.Attributes["type"] == null && node.Attributes["returnType"] != null &&
                        node.Attributes["value"] != null && node.InnerText != null)
                    return new ItemParser(node.Attributes["returnType"].Value, node.Attributes["value"].Value, node.InnerText,false).Parse();

                else return $"{error} Invalid or missing XML argument in tag \"{node.Name}\"!";
            }
            #endregion PublicItem

            #region Method
            if (node.Name == "method" && !this.noPrivateMethods)
            {
                privateMethodsCount++;
                #region No arguments
                if (node.Attributes["type"] != null && node.Attributes["returnType"] != null && node.Attributes["param"] == null
                    && node.Attributes["params"] == null && node.InnerText != null) //HAS NO PARAMETERS
                    return new MethodParser(node.Attributes["type"].Value, node.Attributes["returnType"].Value, node.InnerText).Parse();
                else if (node.Attributes["type"] == null && node.Attributes["returnType"] != null && node.Attributes["param"] == null
                    && node.Attributes["params"] == null && node.InnerText != null) //HAS NO PARAMETERS!
                    return new MethodParser(node.Attributes["returnType"].Value, node.InnerText).Parse();
                #endregion No arguments

                #region Single argument
                else if (node.Attributes["type"] != null && node.Attributes["returnType"] != null && node.Attributes["param"] != null
                    && node.Attributes["params"] == null && node.InnerText != null) //have ONE parameter and type!
                    return new MethodParser(node.Attributes["type"].Value, node.Attributes["returnType"].Value, node.InnerText, true,
                        0, node.Attributes["param"].Value).Parse();
                else if (node.Attributes["type"] == null && node.Attributes["returnType"] != null && node.Attributes["param"] != null
                    && node.Attributes["params"] == null && node.InnerText != null) //have ONE parameter and NO type!
                    return new MethodParser(null, node.Attributes["returnType"].Value, node.InnerText, true, 0, node.Attributes["param"].Value).Parse();
                #endregion Single argument

                #region Many Arguments
                else if (node.Attributes["type"] != null && node.Attributes["returnType"] != null && node.Attributes["param"] == null
                    && node.Attributes["params"] != null && node.InnerText != null) //have MANY parameters and type!
                    return new MethodParser(node.Attributes["type"].Value, node.Attributes["returnType"].Value, node.InnerText, true,
                        GetConstructorParameters(node.Attributes["params"].Value)).Parse();
                else if (node.Attributes["type"] == null && node.Attributes["returnType"] != null && node.Attributes["param"] == null
                    && node.Attributes["params"] != null && node.InnerText != null) //have MANY parameters and NO type!
                    return new MethodParser(null, node.Attributes["returnType"].Value, node.InnerText, true,
                        GetConstructorParameters(node.Attributes["params"].Value)).Parse();

                #endregion Many Arguments

                else return $"{error} Invalid or missing XML argument in tag \"{node.Name}\"!";
            }
            

            #endregion Method

            #region PublicMethod

            if (node.Name == "publicMethod" && !this.noPublicMethods)
            {
                publicMethodsCount++;
                #region No arguments
                if (node.Attributes["type"] != null && node.Attributes["returnType"] != null && node.Attributes["param"] == null
                    && node.Attributes["params"] == null && node.InnerText != null)
                    return new MethodParser(node.Attributes["type"].Value, node.Attributes["returnType"].Value, node.InnerText, false, 0, null).Parse();
                else if (node.Attributes["type"] == null && node.Attributes["returnType"] != null && node.Attributes["param"] == null
                    && node.Attributes["params"] == null && node.InnerText != null)
                    return new MethodParser(node.Attributes["returnType"].Value, node.InnerText, false).Parse();
            
                #endregion No arguments

                #region Single argument
                else if (node.Attributes["type"] != null && node.Attributes["returnType"] != null && node.Attributes["param"] != null
                    && node.Attributes["params"] == null && node.InnerText != null) //have ONE parameter and type!
                    return new MethodParser(node.Attributes["type"].Value, node.Attributes["returnType"].Value, node.InnerText, false,
                        0, node.Attributes["param"].Value).Parse();
                else if (node.Attributes["type"] == null && node.Attributes["returnType"] != null && node.Attributes["param"] != null
                    && node.Attributes["params"] == null && node.InnerText != null) //have ONE parameter and NO type!
                    return new MethodParser(null, node.Attributes["returnType"].Value, node.InnerText, false, 0, node.Attributes["param"].Value).Parse();
                #endregion Single argument

                #region Many Arguments
                else if (node.Attributes["type"] != null && node.Attributes["returnType"] != null && node.Attributes["param"] == null
                    && node.Attributes["params"] != null && node.InnerText != null) //have MANY parameters and type!
                    return new MethodParser(node.Attributes["type"].Value, node.Attributes["returnType"].Value, node.InnerText, false,
                        GetConstructorParameters(node.Attributes["params"].Value)).Parse();
                else if (node.Attributes["type"] == null && node.Attributes["returnType"] != null && node.Attributes["param"] == null
                    && node.Attributes["params"] != null && node.InnerText != null) //have MANY parameters and NO type!
                    return new MethodParser(null, node.Attributes["returnType"].Value, node.InnerText, false,
                        GetConstructorParameters(node.Attributes["params"].Value)).Parse();

                #endregion Many Arguments

                else return $"{error} Invalid or missing XML argument in tag \"{node.Name}\"!";
            }
            #endregion PublicMethod

            #region Constructor/s

            if (node.Name == "constructor")
                if (node.Attributes != null && node.Attributes["protectionLevel"] != null && node.Attributes["param"] != null)
                    return new ConstuctorGenerator(node.Attributes["protectionLevel"].Value, node.Attributes["param"].Value).Parse();
                else if (node.Attributes != null && node.Attributes["protectionLevel"] == null && node.Attributes["param"] != null)
                    return new ConstuctorGenerator(node.Attributes["param"].Value).Parse();
                else if (node.Attributes != null && node.Attributes["protectionLevel"] != null && node.Attributes["params"] != null)
                    return new ConstuctorGenerator(node.Attributes["protectionLevel"].Value,
                        GetConstructorParameters(node.Attributes["params"].Value)).Parse();
                else if (node.Attributes != null && node.Attributes["protectionLevel"] == null && node.Attributes["params"] != null)
                    return new ConstuctorGenerator(GetConstructorParameters(node.Attributes["params"].Value)).Parse();

            #endregion Constructor/s
                        
            return string.Empty;
        }

        /// <summary>
        /// Essentially calls the <see cref="ClassWtriter"/> class and writes the content in the file.
        /// </summary>
        private void TraceContent()
        {
            ClassWriter.SetRefferenceCount((uint)refferences);
            ClassWriter.SetPrivateFieldsCount(privateFieldsCount);
            ClassWriter.SetPublicFieldsCount(publicFieldsCount);
            ClassWriter.SetPrivateMethodsCount(privateMethodsCount);
            ClassWriter.SetPublicMethodsCount(publicMethodsCount);
            ClassWriter.SetInheritsList(inheritsList);
            foreach (string item in xmlContent)
                ClassWriter.Parse(item);
        }
        #endregion Private

        #region Public

        public static void Main()
        {
            var start = DateTime.Now.Millisecond;
            new XMLParser();
            var end = DateTime.Now.Millisecond;
            var lifeSpan = end - start;
            Console.ForegroundColor = ConsoleColor.Green;
            var message = "The program was completed successfully in ";
            if (lifeSpan < 0) message += (1000 - (lifeSpan * -1) + 1);
            else message += lifeSpan + 1;
            message += " milliseconds!";
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.Gray;
            Environment.Exit(0);
        }

        public XMLParser()
        {
            var doc = new XmlDocument();

            xmlContent = new System.Collections.Specialized.StringCollection();

            
            var path = Console.ReadLine();
            if (!path.EndsWith(".sashs"))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The file must have the extension \".sashs\"!");
                Console.ForegroundColor = ConsoleColor.Gray;
                Main();
            }
            else
            {
                try
                {
                    doc.Load(path);
                }
                catch (XmlException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid XML in the given path.");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Main();
                }
                catch (System.IO.IOException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Cannot find the file or path \"{path}\"!");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Main();
                }
                catch (UnauthorizedAccessException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Cannot access file or path \"{path}\"!");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Main();
                }
            }

            #region No-set
            //setting the "NO"-region variables

            if (doc.GetElementsByTagName("dependencies")[0] != null &&
                doc.GetElementsByTagName("dependencies")[0].HasChildNodes)
                this.noDependencies = false;

            if (doc.GetElementsByTagName("inherits")[0] != null &&
                doc.GetElementsByTagName("inherits")[0].HasChildNodes)
                this.noInherits = false;

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

            pathToWrite = doc.GetElementsByTagName("writeInFile")[0].Attributes["path"].Value;

            ParseXML(doc.GetElementsByTagName("nameSpace")[0]);
            
            xmlContent.Add(tab+"}\n}"); //add the finishing 2 closing braces ;)
            TraceContent();
            
        }

        /// <summary>
        /// This constructor must be used in XMLParser_GUI!
        /// </summary>
        /// <param name="path"></param>
        public XMLParser(string path)
        {
            var doc = new XmlDocument();

            if (!path.EndsWith(".sashs"))
            {
                MessageBox.Show("The file must have the extension \".sashs\"!");
                Main();
            }
            else
            {
                try
                {
                    doc.Load(path);
                }
                catch (XmlException)
                {
                    MessageBox.Show("Invalid XML in the given path.");
                    Main();
                }
                catch (System.IO.IOException)
                {
                    MessageBox.Show($"Cannot find the file or path \"{path}\"!");
                    Main();
                }
                catch (UnauthorizedAccessException)
                {
                    MessageBox.Show($"Cannot access file or path \"{path}\"!");
                    Main();
                }
            }

            MessageBox.Show("File parsed successfully!");
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
                    if (!searchRes.StartsWith(tab) && !searchRes.StartsWith("namespace"))
                        xmlContent.Add(tab + searchRes);
                    else xmlContent.Add(searchRes);
            }

            if (root.HasChildNodes)
                ParseXML(root.FirstChild);
            if (root.NextSibling != null)
                ParseXML(root.NextSibling);
        }
        #endregion
        
    }
}
