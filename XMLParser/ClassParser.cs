using System.Runtime.InteropServices;

namespace XMLParser
{
    /// <summary>
    /// Parses the "class" property of the XML file.
    /// </summary>
    [System.Security.SecurityCritical]
    [ComVisible(true)]
    [Guid("f1f8ca04-77f6-46bd-80f5-0dc025fe823b")]
    class ClassParser : IParser
    {
        #region Private

        private string protectionLevel;
        private string type;
        private static string className;
        private bool isGeneric;

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

        public ClassParser(string className) : this(null, null, className) { } //for only one argument given

        public ClassParser(string protectionLevel, string className) : this(protectionLevel, null, className) { }

        public ClassParser(string type, int placeholder, string className) : this(null, type, className) { }

        public ClassParser(string protectionLevel, string type, string className2)
        {
            try
            {

                this.protectionLevel = protectionLevel;
                this.type = type;

                if (className2 != null && className2 != string.Empty)
                    className = className2;

                if (className2.EndsWith("{T}"))
                {
                    className = className2.Remove(className2.Length - 3, 3);
                    isGeneric = true;
                }
                if (className == null) System.Console.WriteLine("NO CLASS NAME!");
                if (className == string.Empty) System.Console.WriteLine("EMPTY CLASS NAME!");
            }
            catch (System.NullReferenceException )
            {
                className = "MyClass";
            }
        }

        public string Parse()
        {
            if (type == null && protectionLevel != null && !isGeneric) //has only protection level and isn't generic
                return $"C{this.protectionLevel} {className}";
            else if (type == null && protectionLevel != null && isGeneric) //has only protection level and IS generic
                return $"C{this.protectionLevel} {className}<T>";

            if (type == null && protectionLevel == null && !isGeneric) //has only name and isn't generic
                return $"C{className}";
            else if (type == null && protectionLevel == null && isGeneric) //has only name and IS generic
                return $"C{className}<T>";
            if (type != null && protectionLevel == null && !isGeneric) //has only type and isn't generic
                return $"C{type} {className}";
            else if (type != null && protectionLevel == null && isGeneric) //has only type and IS generic
                return $"C{type} {className}<T>";
            if (type != null && protectionLevel != null && className != null && isGeneric) //have everything and IS generic
                return $"C{protectionLevel} {type} {className}<T>";
            return $"C{protectionLevel} {type} {className}"; //have everything and isn't generic;
        }

        #endregion Public
    }
}