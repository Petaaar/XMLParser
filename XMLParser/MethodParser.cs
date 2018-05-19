namespace XMLParser
{
    /// <summary>
    /// Parses all methods, written in the XML(.sashs) file. Cannot be inherited.
    /// </summary>
    sealed class MethodParser : IParser
    {
        #region Private
        private readonly string type;

        private readonly string returnType;

        private readonly string name;

        private readonly bool isPrivate;

        private readonly string protectionLevel;

        private string classType = XMLParser.ClassType;
        #endregion Private

        #region Encapsulation
        public string Type => type;

        public string ReturnType => returnType;

        public string Name => name;

        public bool IsPrivate => isPrivate;

        public string ProtectionLevel => protectionLevel;
        #endregion Encapsulation

        public MethodParser() : this(null,null,null,true) { }

        public MethodParser(string returnType, string name, bool isPrivate) : this(null,returnType,name,isPrivate) { }

        public MethodParser(string returnType, string name) : this(null, returnType, name, true) { }

        public MethodParser(string type, string returnType, string name) : this(type, returnType, name, true) { }

        /// <param name="isPrivate">Determines if the given method is private. TRUE by default.</param>
        public MethodParser(string type, string returnType, string name, bool isPrivate)
        {
            this.type = type;
            this.returnType = returnType;
            this.name = name;
            this.isPrivate = isPrivate;

            if (this.isPrivate)
                this.protectionLevel = "private";
            else this.protectionLevel = "public";
        }

        public string Parse()
        {
            char fieldLevel = (protectionLevel == "private") ? fieldLevel = 'N' : fieldLevel = 'P';

            if (type == null) //no type
                if (classType == null || classType == "abstract")
                    return $"        {fieldLevel}M{protectionLevel} {returnType} {name}()";
                else return $"        {fieldLevel}M{protectionLevel} {returnType} {name}()";
            if (classType != "abstract" || classType != null)
                return $"        {fieldLevel}M{protectionLevel} {returnType} {name}()";
            return $"        {fieldLevel}M{protectionLevel} {type} {returnType} {name}()";
        }
    }
}
