﻿namespace XMLParser
{
    sealed class ClassWtriter
    {
        #region Private

        #region fields
        private readonly static string pathTo = XMLParser.SetPathForCreatingFile();

        private const string tabulation = "    ";

        private const string doubleTab = "        "; //double tabulation

        private static bool encapsulate;

        private static uint refferenceCount;

        private static uint refferenceIteration;

        private static uint privateFieldsCount;

        private static uint privateFieldsIteration;

        private static uint publicFieldsCount;

        private static uint publicFieldsIteration;

        private static uint privateMehtodsCount;

        private static uint privateMethodsIteration;

        private static uint publicMethodsCount;

        private static uint publicMethodsIteration;
        
        private static string classType = XMLParser.ClassType;

        /// <summary>
        /// Contains the entire class.
        /// </summary>
        private static System.Collections.Generic.List<string> fullClass = new System.Collections.Generic.List<string>();

        #endregion fields

        private static void Error(string message)
        {
            System.Console.ForegroundColor = System.ConsoleColor.Red;
            System.Console.WriteLine(message);
            System.Console.ForegroundColor = System.ConsoleColor.Gray;
        }

        

        /// <summary>
        /// Writes a given <paramref name="fileContent"/> into a file.
        /// </summary>
        /// <param name="fileContent">The content to be written.</param>
        private static void WriteFile(System.Collections.Generic.List<string> fileContent)
        {
            refferenceIteration = 0;
            privateFieldsIteration = 0;
            publicFieldsIteration = 0;
            privateMethodsIteration = 0;
            publicMethodsIteration = 0;

            using (System.IO.StreamWriter writer = new System.IO.StreamWriter(pathTo))
            {
                writer.WriteLine("/*This class is generated by the XMLParser program.");
                writer.WriteLine("Author: Petar Angelov (Petaaar). GitHub: https://github.com/Petaaar.");
                writer.WriteLine("The project source code: https://github.com/Petaaar/XMLParser.");
                writer.WriteLine("Thanks for using my parser!*/");

                foreach (string item in fileContent)
                {
                    if (item.EndsWith("\n{")) //namespace <NAME> \n {
                    {
                        writer.WriteLine(item.Remove(item.Length - 2, 2));
                        writer.WriteLine("{");
                    }

                    else if (item.StartsWith($"{tabulation}using")) //using <ASSEMBLY_REFFERENCE>
                    {
                        refferenceIteration++;
                        if (refferenceIteration == 1)
                            writer.WriteLine($"{tabulation}#region Dependencies");
                        if (refferenceIteration == refferenceCount)
                        {
                            writer.WriteLine(item);
                            writer.WriteLine($"{tabulation}#endregion Dependencies");
                            writer.WriteLine();
                        }
                        else writer.WriteLine(item);
                    }
                    else if (item.EndsWith(tabulation + "{") && !item.EndsWith(tabulation + "}}")) // class <NAME> \n {
                    {
                        writer.WriteLine($"{tabulation}///<summary>A class, generated automatically via XMLParser. WRITE YOUR SUMMARY HERE!</summary>");
                        writer.Write(item.Remove(item.Length - 5, 5));
                        writer.WriteLine(tabulation + "{");
                    }

                    else if (item.StartsWith($"{doubleTab}N") || item.StartsWith($"{doubleTab}P")) //FIELDS AND METHODS
                    {
                        if (!item.StartsWith($"{doubleTab}NM") && !item.StartsWith($"{doubleTab}PM")) //FIELDS
                        {
                            if (item.StartsWith($"{doubleTab}N") && !item.StartsWith($"{doubleTab}P")) //private fields
                            {
                                privateFieldsIteration++;
                                if (privateFieldsIteration == 1)
                                {
                                    writer.WriteLine($"{doubleTab}#region Private Fields");
                                    writer.WriteLine();
                                }
                                if (privateFieldsIteration == privateFieldsCount)
                                {
                                    writer.WriteLine(item.Remove(8, 1));
                                    writer.WriteLine($"{doubleTab}#endregion Private Fields");
                                    writer.WriteLine();
                                }
                                else writer.WriteLine(item.Remove(8, 1));
                            }
                            if (item.StartsWith($"{doubleTab}P") && !item.StartsWith($"{doubleTab}N")) //public fields
                            {
                                publicFieldsIteration++;
                                if (publicFieldsIteration == 1)
                                {
                                    writer.WriteLine($"{doubleTab}#region Public Fields");
                                    writer.WriteLine();
                                }
                                if (publicFieldsIteration == publicFieldsCount)
                                {
                                    writer.WriteLine(item.Remove(8, 1));
                                    writer.WriteLine($"{doubleTab}#endregion Public Fields");
                                    writer.WriteLine();
                                }
                                else writer.WriteLine(item.Remove(8, 1));
                            }
                        }
                        else
                        {
                            if (item.StartsWith($"{doubleTab}NM") && !item.StartsWith($"{doubleTab}PM"))//private method
                            {
                                privateMethodsIteration++;

                                if (privateMethodsIteration == 1)
                                {
                                    writer.WriteLine($"{doubleTab}#region Private Methods");
                                    writer.WriteLine();
                                }
                                if (privateMethodsIteration == privateMehtodsCount)
                                {
                                    writer.WriteLine(item.Remove(8, 2));
                                    writer.WriteLine($"{doubleTab}#endregion Private Methods");
                                    writer.WriteLine();
                                }
                                else writer.WriteLine(item.Remove(8, 2));
                            }
                            if (item.StartsWith($"{doubleTab}PM") && !item.StartsWith($"{doubleTab}NM")) //public method
                            {
                                publicMethodsIteration++;

                                if (publicMethodsIteration == 1)
                                {
                                    writer.WriteLine($"{doubleTab}#region Public Methods");
                                    writer.WriteLine();
                                }
                                if (publicMethodsIteration == publicMethodsCount)
                                {
                                    writer.WriteLine(item.Remove(8, 2));
                                    writer.WriteLine($"{doubleTab}#endregion Public Methods");
                                    writer.WriteLine();
                                    writer.WriteLine();
                                }
                                else writer.WriteLine(item.Remove(8, 2));
                            }
                        }
                    }

                    else if (item.EndsWith("}\n}")) //}\n} -> } }
                    {
                        writer.WriteLine(item.Remove(item.Length - 1, 1));
                        writer.WriteLine('}');
                    }

                    else if (item.EndsWith("\n{\n}\n"))
                    {
                        writer.WriteLine(item.Remove(item.Length - 5, 5));
                        writer.WriteLine(doubleTab + '{');
                        writer.WriteLine($"{doubleTab}//Write your code here!");
                        writer.WriteLine(doubleTab + '}');
                        writer.WriteLine();
                    }

                    else writer.WriteLine(item);
                }
            }
        }
        #endregion Private

        #region Public

        /// <summary>
        /// Checks and sets the encapsulation of the private nodes.
        /// </summary>
        /// <param name="TrueFalse"></param>
        public static void SetEncapsulation(string TrueFalse)
        {
            if (TrueFalse == "true")
                encapsulate = true;
        }

        /// <summary>
        /// The count of the assembly references used.
        /// </summary>
        /// <param name="count"></param>
        public static void SetRefferenceCount(uint count) => refferenceCount = count;

        /// <summary>
        /// Sets the count of the private fields in the class
        /// </summary>
        /// <param name="count"></param>
        public static void SetPrivateFieldsCount(uint count) => privateFieldsCount = count;

        /// <summary>
        /// Sets the count of the public fields in the class.
        /// </summary>
        /// <param name="count"></param>
        public static void SetPublicFieldsCount(uint count) => publicFieldsCount = count;

        /// <summary>
        /// Sets the count of the private methods in the class
        /// </summary>
        /// <param name="count"></param>
        public static void SetPrivateMethodsCount(uint count) => privateMehtodsCount = count;

        /// <summary>
        /// Sets the count if public methods in the class
        /// </summary>
        /// <param name="count"></param>
        public static void SetPublicMethodsCount(uint count) => publicMethodsCount = count;

        /// <summary>
        /// Saves the current item into the fullClass list.
        /// </summary>
        /// <param name="item">Current item.</param>
        public static void SaveFileContent(string item)
        {
            CheckDir(pathTo);

            item = CheckItem(item);

            fullClass.Add(item);
        }

        /// <summary>
        /// Checks if the new file exists or not. If not - creates it.
        /// </summary>
        /// <param name="path"></param>
        public static void CheckDir(string path)
        {
            if (!System.IO.File.Exists(path))
            {
                System.IO.FileStream file;
                try
                {
                    file = System.IO.File.Create(path);
                    file.Close(); 
                }
                catch (System.IO.DirectoryNotFoundException) { Error("Please, specify a filename within the path!"); System.Environment.Exit(1); }
                catch (System.UnauthorizedAccessException) { Error("Could not access the given path!");  System.Environment.Exit(1); }
            }      
        }

        /// <summary>
        /// Method to check the given node/item.
        /// </summary>
        /// <param name="item">Item to check.</param>
        /// <returns>string</returns>
        public static string CheckItem(string item)
        {
            if (item == "true")
                encapsulate = true;

            if (item.StartsWith("namespace"))
                return Namespace(item);

            if (item.StartsWith($"{tabulation}R"))
            {
                refferenceIteration++;

                if (refferenceIteration == refferenceCount)
                    return $"{item.Remove(4, 1)}\n";
                return $"{item.Remove(4,1)}";
            }

            if (item.StartsWith($"{tabulation}C")) //class checking
            {

                item = item.Remove(0, 5); //remove 'C' from the item :D

                var properties = item.Split(new char[] {' '}, System.StringSplitOptions.None);

                var list = new System.Collections.Generic.List<string>();

                foreach (string innerItem in properties)
                    if (innerItem == string.Empty)
                        continue;
                    else list.Add(innerItem);

                var props = list.ToArray();

                list.Clear();

                if (props.Length == 1)
                    return Class(item);
               
                if (props.Length == 2)
                {
                    if (props[1] == string.Empty) throw new System.ArgumentException("EMPTY CLASS NAME!");
                    else if (props[0] == "private" || props[0] == "public" || props[0] == "internal")
                        return Class(props[0], props[1]); // public class className
                    else if (props[0] == "static" || props[0] == "sealed" || props[0] == "abstract")
                        return Class(props[0], props[1], 0); //static class className
                }

                if (props.Length == 3)
                    return Class(props[0], props[1], props[2]);
            }


            #region Field and method checking
            if (item.StartsWith($"{doubleTab}N") && !item.StartsWith($"{doubleTab}NM")) //private field
                return item + ';' + "\n";

            if (item.StartsWith($"{doubleTab}P") && !item.StartsWith($"{doubleTab}PM")) //public field
                return item + ';' + "\n";

            else if (item.StartsWith($"{doubleTab}NM") && classType == "abstract") //private method
                return item + ';' + "\n";

            else if (item.StartsWith($"{doubleTab}NM") && classType != "abstract") //private method
                return item + "\n" + doubleTab + "{ \n"+ $"{doubleTab}//Write your code here! \n" + doubleTab + "}\n";

            else if (item.StartsWith($"{doubleTab}PM") && classType == "abstract") //public method
                return item + ';' + "\n";

            else if (item.StartsWith($"{doubleTab}PM") && classType != "abstract") //public method
                return item + "\n" + doubleTab + "{ \n"+$"{doubleTab}//Write your code here! \n" + doubleTab + "}\n";

            #endregion Field and method checking

            #region Constructors checking

            if (item.StartsWith($"{doubleTab}CTOR"))
                return item.Remove(8, 4) + "\n{\n}\n";

            #endregion Constructors checking

            return item;
        }

        #region Checkers

        private static string Namespace(string item)
            => item + "\n{";

        #region CLASS
        private static string Class(string className)
        {
            if (className.Length == 0)
                throw new System.ArgumentNullException("EMPTY CLASS NAME!");
            return $"{tabulation}class {className}" + "\n" + tabulation + '{';
        }

        private static string Class(string protectionLevel, string className)
        {
            if (className.Length == 0)
                throw new System.ArgumentNullException("EMPTY CLASS NAME!");
            return $"{tabulation}{protectionLevel} class {className}" + "\n" + tabulation + '{';
        }

        private static string Class(string classType, string className, int placeholder)
        {
            if (className.Length == 0)
                throw new System.ArgumentNullException("EMPTY CLASS NAME!");
            return $"{tabulation}{classType} class {className}" + "\n" + tabulation + '{';
        }

        private static string Class(string protectionLevel, string type, string className)
        {
            if (className.Length == 0)
                throw new System.ArgumentNullException("EMPTY CLASS NAME!");
            return $"{tabulation}{protectionLevel} {type} class {className}" + "\n" + tabulation + '{';
        }
        #endregion CLASS
        

        #endregion Checkers

        /// <summary>
        /// Implementation of <see cref="IParser"/>. Basically runs the class.
        /// </summary>
        /// <param name="item">Item to be parsed.</param>
        public static void Parse(string item)
        {
            if (item is string && item.Length != 0)
            {
                SaveFileContent(item);
                WriteFile(fullClass);
            }
            else System.Console.WriteLine("ERROR!");
        }

        #endregion Public
    }
}