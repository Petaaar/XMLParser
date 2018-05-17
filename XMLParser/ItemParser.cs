namespace XMLParser
{
    /// <summary>
    /// A class that parses every type of field. Cannot be inherited.
    /// </summary>
    sealed class ItemParser : IParser
    {
        #region Private
        private string type;

        private string returnType;

        private string name;

        private readonly bool isPrivate;

        private readonly string protectionLevel;
        #endregion

        #region Encapsulations
        public string Type { get => type; private set => type = value; }

        public string ReturnType { get => returnType; set => returnType = value; }

        public string Name { get => name; set => name = value; }

        public bool IsPrivate => isPrivate;

        public string ProtectionLevel => protectionLevel;
        #endregion Encapsulations


        public ItemParser() : this(null, null, null, true) { }

        public ItemParser(string returnType, string name) : this(null, returnType, name, true) { }
        
        /// <param name="isPrivate">IF this is empty it's assigned to true.</param>
        public ItemParser(string returnType, string name, bool isPrivate) : this(null, returnType, name, isPrivate) { }

        public ItemParser(string type, string returnType, string name) : this(type, returnType, name, true) { }

        /// <param name="isPrivate">If this parameter is null it's assigned to true.</param>
        public ItemParser(string type, string returnType, string name, bool isPrivate)
        {
            this.type = type;
            this.returnType = returnType;
            this.name = name;
            this.isPrivate = isPrivate;

            if (this.isPrivate)
                protectionLevel = "private";
            else protectionLevel = "public";
        }

        public string Parse()
        {
            char fieldLevel = (protectionLevel == "private") ? fieldLevel = 'N' : fieldLevel = 'P';

            if (type == null) //no type
                return $"        {fieldLevel}{protectionLevel} {returnType} {name}";
            return $"        {fieldLevel}{protectionLevel} {type} {returnType} {name}";
        }
    }
}
