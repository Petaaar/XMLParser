using System.Runtime.InteropServices;

namespace XMLParser
{
    /// <summary>
    /// A class that parses every type of field. Cannot be inherited.
    /// </summary>
    [System.Security.SecurityCritical]
    [ComVisible(true)]
    [Guid("f1f8ca04-77f6-46bd-80f5-0dc025fe823b")]
    sealed class ItemParser : IParser
    {
        #region Encapsulations
        public string Type { get; private set; }

        public string ReturnType { get; set; }

        public string Value { get; set; }

        public string Name { get; set; }

        public bool IsPrivate { get; }

        public string ProtectionLevel { get; }
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
            this.Type = type;
            this.ReturnType = returnType;
            this.Name = name;
            this.Value = value;
            this.IsPrivate = isPrivate;

            if (this.IsPrivate)
                ProtectionLevel = "private";
            else ProtectionLevel = "public";
        }

        /// <summary>
        /// Implements the <see cref="IParser"/>. Creates the  current field.
        /// </summary>
        /// <returns>New field as string.</returns>
        public string Parse()
        {
            char fieldLevel = (ProtectionLevel == "private") ? fieldLevel = 'N' : fieldLevel = 'P';

            if (Type == null) //no type
                if (ReturnType == "void" && Value != null)
                    return $"        {fieldLevel}{ProtectionLevel} {ReturnType} {Name} = {Value}";
                else if (ReturnType == "void" && Value == null)
                    return $"        {fieldLevel}{ProtectionLevel} {ReturnType} {Name}";
            if (ReturnType == "void")
                return $"        {fieldLevel}{ProtectionLevel} {Type} {ReturnType} {Name}";
            else if (ReturnType == "string" && Value != null) //MUST swap value and name...idk why..
                return $"        {fieldLevel}{ProtectionLevel} {Type} {ReturnType} {Value} = \"{Name}\"";
            else if (ReturnType == "bool" && Value != null)
                return $"        {fieldLevel}{ProtectionLevel} {Type} {ReturnType} {Name} = {Value}";
            else if (ReturnType == "int" && Value != null)
                return $"        {fieldLevel}{ProtectionLevel} {Type} {ReturnType} {Name} = {Value}";
            else if (ReturnType == "double" && Value != null)
                return $"        {fieldLevel}{ProtectionLevel} {Type} {ReturnType} {Name} = {Value}";
            if (Value == null && Type != null)
                return $"        {fieldLevel}{ProtectionLevel} {Type} {ReturnType} {Name}";
            if (Value == null && Type == null)
                return $"        {fieldLevel}{ProtectionLevel} {ReturnType} {Name}";

            return $"        {fieldLevel}{ProtectionLevel} {Type} {ReturnType} {Name} = {Value}";
        }
    }
}