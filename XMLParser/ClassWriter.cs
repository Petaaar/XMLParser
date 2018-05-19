﻿namespace XMLParser
{
    sealed class ClassWtriter
    {

        private const string pathTo = @"C:\Users\petar\source\repos\XMLParser\XMLParser\TestClass.cs";

        private const string tabulation = "    ";

        private const string doubleTab = "        "; //double tabulation

        private static bool encapsulate;

        private static uint refferenceCount;

        private static uint refferenceIteration;

        private static System.Collections.Generic.List<string> privateFields;

        /// <summary>
        /// Contains the entire class.
        /// </summary>
        private static System.Collections.Generic.List<string> fullClass = new System.Collections.Generic.List<string>();

        public static void Parse(string item)
        {
            if (item is string && item.Length != 0)
            {
                SaveFileContent(item);
                WriteFile(fullClass);
            }
            else System.Console.WriteLine("ERROR!");
        }

        private static void WriteFile(System.Collections.Generic.List<string> fileContent)
        {
            refferenceIteration = 0;
            using (System.IO.StreamWriter writer = new System.IO.StreamWriter(pathTo))
            {
                foreach (string item in fileContent)
                {
                    if (item.EndsWith("\n{")) //namespace <NAME> \n {
                    {
                        writer.WriteLine(item.Remove(item.Length - 2, 2));
                        writer.WriteLine("{");
                    }
                    else if (item.StartsWith($"{tabulation}using"))
                    {
                        refferenceIteration++;

                        if (refferenceIteration == refferenceCount)
                        {
                            writer.WriteLine(item);
                            writer.WriteLine();
                        }
                        else writer.WriteLine(item);
                    }
                    else if (item.EndsWith(tabulation + "{") && !item.EndsWith(tabulation + "}}"))
                    {
                        writer.WriteLine(item.Remove(item.Length - 5, 5));
                        writer.WriteLine(tabulation + "{");
                    }
                    else if (item.EndsWith("}\n}"))
                    {
                        writer.WriteLine(item.Remove(item.Length - 1, 1));
                        writer.WriteLine('}');
                    }
                    else  writer.WriteLine(item);
                }
            }
        }

        public static void SetEncapsulation(string TrueFalse)
        {
            if (TrueFalse == "true")
                encapsulate = true;
        }

        public static void SetRefferenceCount(uint count) => refferenceCount = count;


        public static void SaveFileContent(string item)
        {
            CheckDir(pathTo);

            item = CheckItem(item);

            fullClass.Add(item);
        }

        public static void CheckDir(string path)
        {
            if (!System.IO.File.Exists(path))
                System.IO.File.Create(path);
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

            privateFields = new System.Collections.Generic.List<string>();

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

            if (item.StartsWith($"{doubleTab}N") && !item.StartsWith($"{doubleTab}NM")) //private field
                return item.Remove(8, 1) + ';' + "\n";

            if (item.StartsWith($"{doubleTab}P") && !item.StartsWith($"{doubleTab}PM")) //public field
                return item.Remove(8, 1) + ';' + "\n";

            else if (item.StartsWith($"{doubleTab}NM")) //private method
                return item.Remove(8, 2) + ';' + "\n";

            else if (item.StartsWith($"{doubleTab}PM")) //public method
                return item.Remove(8, 2) + ';' + "\n";

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
    }
}