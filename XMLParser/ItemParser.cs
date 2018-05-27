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

        private string value;

        private readonly bool isPrivate;

        private readonly string protectionLevel;
        #endregion

        #region Encapsulations
        public string Type { get => type; private set => type = value; }

        public string ReturnType { get => returnType; set => returnType = value; }

        public string Value { get => value; set => this.value = value; }

        public string Name { get => name; set => name = value; }

        public bool IsPrivate => isPrivate;

        public string ProtectionLevel => protectionLevel;
        #endregion Encapsulations

        public ItemParser() : this(null, null, null, true) { }

        public ItemParser(string returnType, string name) : this(null, returnType, null, name, true) { }

        public ItemParser(string returnType, string value, string name) : this(null, returnType, value, name, true) { }

        /// <param name="isPrivate">IF this is empty it's assigned to true.</param>
        public ItemParser(string returnType, string value, string name, bool isPrivate) : this(null, returnType, value, name, isPrivate) { }

        public ItemParser(string type, string returnType, string name, uint placeholder) : this(type, returnType, null, name, true) { }

        public ItemParser(string type, string returnType, string value, string name) : this(type, returnType, name, value, true) { }

        /// <param name="isPrivate">If this parameter is null it's assigned to true.</param>
        public ItemParser(string type, string returnType, string value, string name, bool isPrivate)
        {
            this.type = type;
            this.returnType = returnType;
            this.name = name;
            this.value = value;
            this.isPrivate = isPrivate;

            if (this.isPrivate)
                protectionLevel = "private";
            else protectionLevel = "public";
        }

        /// <summary>
        /// Implements the <see cref="IParser"/>. Creates the  current field.
        /// </summary>
        /// <returns>New field as string.</returns>
        public string Parse()
        {
            char fieldLevel = (protectionLevel == "private") ? fieldLevel = 'N' : fieldLevel = 'P';

            if (type == null) //no type
                if (returnType == "void" && value != null)
                    return $"        {fieldLevel}{protectionLevel} {returnType} {name} = {value}";
                else if (returnType == "void" && value == null)
                    return $"        {fieldLevel}{protectionLevel} {returnType} {name}";
            if (returnType == "void")
                return $"        {fieldLevel}{protectionLevel} {type} {returnType} {name}";
            else if (returnType == "string" && value != null) //MUST swap value and name...idk why..
                return $"        {fieldLevel}{protectionLevel} {type} {returnType} {value} = \"{name}\"";
            else if (returnType == "bool" && value != null)
                return $"        {fieldLevel}{protectionLevel} {type} {returnType} {name} = {value}";
            else if (returnType == "int" && value != null)
                return $"        {fieldLevel}{protectionLevel} {type} {returnType} {name} = {value}";
            else if (returnType == "double" && value != null)
                return $"        {fieldLevel}{protectionLevel} {type} {returnType} {name} = {value}";
            if (value == null && type != null)
                return $"        {fieldLevel}{protectionLevel} {type} {returnType} {name}";
            if (value == null && type == null)
                return $"        {fieldLevel}{protectionLevel} {returnType} {name}";

            return $"        {fieldLevel}{protectionLevel} {type} {returnType} {name} = {value}";
        }
    }
}