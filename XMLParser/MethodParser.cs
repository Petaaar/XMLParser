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

        private readonly string argument;

        private readonly System.Collections.Specialized.StringCollection argumentsList;

        private readonly bool isGeneric;

        private string classType = XMLParser.ClassType;
        #endregion Private

        #region Encapsulation
        public string Type => type;

        public string ReturnType => returnType;

        public string Name => name;

        public bool IsPrivate => isPrivate;

        public string ProtectionLevel => protectionLevel;

        public bool IsGeneric => isGeneric;
        #endregion Encapsulation

        public MethodParser() : this(null,null,null,true, null) { }

        public MethodParser(string returnType, string name, bool isPrivate) : this(null,returnType,name,isPrivate, null) { }

        public MethodParser(string returnType, string name) : this(null, returnType, name, true, null) { }

        public MethodParser(string type, string returnType, string name) : this(type, returnType, name, true, null) { }

        /// <param name="isPrivate">Determines if the given method is private. TRUE by default.</param>
        public MethodParser(string type, string returnType, string name, bool isPrivate, int placeholder, string argument)
        {
            this.type = type;
            this.returnType = returnType;
            this.name = name;
            this.isPrivate = isPrivate;
            this.argument = argument;

            if (name.EndsWith("{T}"))
            {
                this.name = name.Remove(name.Length - 3, 3);
                isGeneric = true;
            }
            if (this.isPrivate)
                this.protectionLevel = "private";
            else this.protectionLevel = "public";
        }

        public MethodParser(string type, string returnType, string name, bool isPrivate, System.Collections.Specialized.StringCollection argumentsList)
        {

            this.type = type;
            this.returnType = returnType;
            this.name = name;
            this.isPrivate = isPrivate;
            this.argumentsList = argumentsList;

            if (name.EndsWith("{T}"))
            {
                this.name = name.Remove(name.Length - 3, 3);
                isGeneric = true;
            }


            if (this.isPrivate)
                this.protectionLevel = "private";
            else this.protectionLevel = "public";
        }

        /// <summary>
        /// Implements the <see cref="IParser"/>. Creates the  current method.
        /// </summary>
        /// <returns>New method as string.</returns>
        public string Parse()
        {
            //the method contains N in itself if it's private..
            char fieldLevel = (protectionLevel == "private") ? fieldLevel = 'N' : fieldLevel = 'P';
            
            if (argument == null && argumentsList == null)
            {
                if (type == null) //no type
                    if ((classType == null || classType == "abstract") && !isGeneric)
                        return $"        {fieldLevel}M{protectionLevel} {returnType} {name}()";
                    else if ((classType == null || classType == "abstract") && isGeneric)
                        return $"        {fieldLevel}M{protectionLevel} {returnType} {name}<T>()";
                    else return $"        {fieldLevel}M{protectionLevel} {returnType} {name}()";
            }
            
            else if (argument != null && argumentsList == null) //we have only one argument
            {
                if (type == null) //no type
                    if (!isGeneric)
                        return $"        {fieldLevel}M{protectionLevel} {returnType} {name}({argument})";
                    else return $"        {fieldLevel}M{protectionLevel} {returnType} {name}<T>({argument})";
                else if (!isGeneric)
                    return $"        {fieldLevel}M{protectionLevel} {type} {returnType} {name}({argument})";
                else return $"        {fieldLevel}M{protectionLevel} {type} {returnType} {name}<T>({argument})";
            }

            else if (argument == null && argumentsList != null)
            {
                var builder = new System.Text.StringBuilder();

                foreach (var listItem in argumentsList)
                    if (listItem != null && listItem != string.Empty)
                        if (listItem != argumentsList[argumentsList.Count - 2])
                            builder.Append(listItem + ',');
                        else builder.Append(listItem);


                if (type != null)
                    if (!isGeneric)
                        return $"        {fieldLevel}M{protectionLevel} {type} {returnType} {name}({builder.ToString()})";
                    else return $"        {fieldLevel}M{protectionLevel} {type} {returnType} {name}<T>({builder.ToString()})";//generic
                else if (!isGeneric)
                    return $"        {fieldLevel}M{protectionLevel} {returnType} {name}({builder.ToString()})";
                else return $"        {fieldLevel}M{protectionLevel} {returnType} {name}<T>({builder.ToString()})"; //generic
            }
            if (!isGeneric)
                return $"        {fieldLevel}M{protectionLevel} {type} {returnType} {name}()";
            return $"        {fieldLevel}M{protectionLevel} {type} {returnType} {name}<T>()"; //generic
        }
    }
}
