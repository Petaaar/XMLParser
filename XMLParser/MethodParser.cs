using System.Runtime.InteropServices;

namespace XMLParser
{
    /// <summary>
    /// Parses all methods, written in the XML(.sashs) file. Cannot be inherited.
    /// </summary>
    [System.Security.SecurityCritical]
    [ComVisible(true)]
    [Guid("f1f8ca04-77f6-46bd-80f5-0dc025fe823b")]
    sealed class MethodParser : IParser
    {
        #region Private

        private readonly string argument;

        private readonly System.Collections.Specialized.StringCollection argumentsList;

        private string classType = XMLParser.ClassType;

        #endregion Private

        #region Encapsulation
        public string Type { get; }

        public string ReturnType { get; }

        public string Name { get; }
        public bool IsPrivate { get; }

        public string ProtectionLevel { get; }

        public bool IsGeneric { get; }
        #endregion Encapsulation

        public MethodParser() : this(null,null,null,true, null) { }

        public MethodParser(string returnType, string name, bool isPrivate) : this(null,returnType,name,isPrivate, null) { }

        public MethodParser(string returnType, string name) : this(null, returnType, name, true, null) { }

        public MethodParser(string type, string returnType, string name) : this(type, returnType, name, true, null) { }

        /// <param name="isPrivate">Determines if the given method is private. TRUE by default.</param>
        public MethodParser(string type, string returnType, string name, bool isPrivate, int placeholder, string argument)
        {
            this.Type = type;
            this.ReturnType = returnType;
            this.Name = name;
            this.IsPrivate = isPrivate;
            this.argument = argument;

            if (name.EndsWith("{T}"))
            {
                this.Name = name.Remove(name.Length - 3, 3);
                IsGeneric = true;
            }
            if (this.IsPrivate)
                this.ProtectionLevel = "private";
            else this.ProtectionLevel = "public";
        }

        public MethodParser(string type, string returnType, string name, bool isPrivate, System.Collections.Specialized.StringCollection argumentsList)
        {

            this.Type = type;
            this.ReturnType = returnType;
            this.Name = name;
            this.IsPrivate = isPrivate;
            this.argumentsList = argumentsList;

            if (name.EndsWith("{T}"))
            {
                this.Name = name.Remove(name.Length - 3, 3);
                IsGeneric = true;
            }


            if (this.IsPrivate)
                this.ProtectionLevel = "private";
            else this.ProtectionLevel = "public";
        }

        /// <summary>
        /// Implements the <see cref="IParser"/>. Creates the  current method.
        /// </summary>
        /// <returns>New method as string.</returns>
        public string Parse()
        {
            //the method contains N in itself if it's private..
            char fieldLevel = (ProtectionLevel == "private") ? fieldLevel = 'N' : fieldLevel = 'P';
            
            if (argument == null && argumentsList == null)
            {
                if (Type == null) //no type
                    if ((classType == null || classType == "abstract") && !IsGeneric)
                        return $"        {fieldLevel}M{ProtectionLevel} {ReturnType} {Name}()";
                    else if ((classType == null || classType == "abstract") && IsGeneric)
                        return $"        {fieldLevel}M{ProtectionLevel} {ReturnType} {Name}<T>()";
                    else return $"        {fieldLevel}M{ProtectionLevel} {ReturnType} {Name}()";
            }
            
            else if (argument != null && argumentsList == null) //we have only one argument
            {
                if (Type == null) //no type
                    if (!IsGeneric)
                        return $"        {fieldLevel}M{ProtectionLevel} {ReturnType} {Name}({argument})";
                    else return $"        {fieldLevel}M{ProtectionLevel} {ReturnType} {Name}<T>({argument})";
                else if (!IsGeneric)
                    return $"        {fieldLevel}M{ProtectionLevel} {Type} {ReturnType} {Name}({argument})";
                else return $"        {fieldLevel}M{ProtectionLevel} {Type} {ReturnType} {Name}<T>({argument})";
            }

            else if (argument == null && argumentsList != null)
            {
                var builder = new System.Text.StringBuilder();

                foreach (var listItem in argumentsList)
                    if (listItem != null && listItem != string.Empty)
                        if (listItem != argumentsList[argumentsList.Count - 2])
                            builder.Append(listItem + ',');
                        else builder.Append(listItem);


                if (Type != null)
                    if (!IsGeneric)
                        return $"        {fieldLevel}M{ProtectionLevel} {Type} {ReturnType} {Name}({builder.ToString()})";
                    else return $"        {fieldLevel}M{ProtectionLevel} {Type} {ReturnType} {Name}<T>({builder.ToString()})";//generic
                else if (!IsGeneric)
                    return $"        {fieldLevel}M{ProtectionLevel} {ReturnType} {Name}({builder.ToString()})";
                else return $"        {fieldLevel}M{ProtectionLevel} {ReturnType} {Name}<T>({builder.ToString()})"; //generic
            }
            if (!IsGeneric)
                return $"        {fieldLevel}M{ProtectionLevel} {Type} {ReturnType} {Name}()";
            return $"        {fieldLevel}M{ProtectionLevel} {Type} {ReturnType} {Name}<T>()"; //generic
        }
    }
}
