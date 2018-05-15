namespace XMLParser
{
    /// <summary>
    /// Parses all methods, written in the XML(.sashs) file. Cannot be inherited.
    /// </summary>
    sealed class MethodParser
    {
        private readonly string type;

        private readonly string returnType;

        private readonly string name;

        private readonly bool isPrivate;

        private readonly string protectionLevel;

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
    }
}
