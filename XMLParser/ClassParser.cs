namespace XMLParser
{
    /// <summary>
    /// Parses the "class" property of the XML file.
    /// </summary>
    class ClassParser : IParser
    {
        #region Private

        private string protectionLevel;
        private string type;
        private static string className;

        #endregion Private

        #region Public

        public string ProtectionLevel
        {
            get
            {
                if (protectionLevel != null)
                    return protectionLevel;
                return string.Empty;
            }
            private set
            {
                if (value != null)
                    protectionLevel = value;
            }
        }

        public string Type
        {
            get
            {
                if (type != null)
                    return type;
                return string.Empty;
            }

            private set
            {
                if (value != null)
                    type = value;
            }
        }

        public string ClassName
        {
            get => className; 

            private set
            {
                if (value != null && value != string.Empty)
                    className = value;
            }
        }

        public ClassParser() : this(className) { }
        
        public ClassParser(string className) : this(null,null,className) { } //for only one argument given

        public ClassParser(string protectionLevel, string className) : this(protectionLevel, null, className) { }
        
        public ClassParser(string type, int placeholder, string className) : this(null,type,className) { }

        public ClassParser(string protectionLevel, string type, string className2)
        {
            this.protectionLevel = protectionLevel;
            this.type = type;
            if (className2 != null && className2 != string.Empty)
            {
                className = className2;
                System.Console.WriteLine(className2);
            }
            if (className == null) System.Console.WriteLine("NULL");
            if (className == string.Empty) System.Console.WriteLine("EMPTY");
        }

        public string Parse()
        {
            if (type == null && protectionLevel != null) //has only protection level
                return $"C{this.protectionLevel} {className}";
            if (type == null && protectionLevel == null) //has only name
                return $"C{className}";
            if (type != null && protectionLevel == null) //has only type
                return $"C{type} {className}";
            return $"C{protectionLevel} {type} {className}"; //have everything;
        }

        #endregion Public
    }
}
