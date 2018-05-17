namespace XMLParser
{
    static class ClassWtriter
    {
        private const string pathTo = @"C:\Users\petar\source\repos\XMLParser\XMLParser\testFile.txt";

        private const string tabulation = "    ";

        public static void Parse(string item)
        {
            if (item is string && item.Length != 0)
                WriteInFile(item);
            else System.Console.WriteLine("ERROR!");
        }

        public static void WriteInFile(string item)
        {
            CheckDir(pathTo);

            System.Console.WriteLine(CheckItem(item));

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
            if (item.StartsWith("namespace"))
                return Namespace(item);
            return item;
        }

        #region Checkers

        private static string Namespace(string item)
            => item + "\n{";

        private static string ok() => "";

        #endregion
    }
}