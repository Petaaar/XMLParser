namespace XMLParser
{
    /// <summary>
    /// A class that parses every type of field. Cannot be inherited.
    /// </summary>
    sealed class ItemParser
    {
        private string type;

        private string returnType;

        private string name;

        private readonly bool isPrivate;

        public ItemParser() : this(null, null, null, true) { }

        public ItemParser(string returnType, string name, bool isPrivate) : this(null, returnType, name, isPrivate) { }

        public ItemParser(string type, string returnType, string name, bool isPrivate)
        {
            this.type = type;
            this.returnType = returnType;
            this.name = name;
            this.isPrivate = isPrivate;
        }
    }
}
