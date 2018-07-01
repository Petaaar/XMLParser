using System.Runtime.InteropServices;

namespace XMLParser
{
    /// <summary>
    /// Generates a constructor for the given class.
    /// </summary>    
    [System.Security.SecurityCritical]
    [ComVisible(true)]
    [Guid("f1f8ca04-77f6-46bd-80f5-0dc025fe823b")]
    sealed class ConstuctorGenerator : IParser
    {

        private string parameter;

        private string protectionLevel;

        private System.Collections.Specialized.StringCollection parametersList;

        private bool isList;

        private static readonly ClassParser classParser = new ClassParser();

        private string className = classParser.ClassName;

        public ConstuctorGenerator(string parameter) : this(null, parameter) { }

        public ConstuctorGenerator(string protectionLevel, string parameter)
        {
            this.protectionLevel = protectionLevel;
            this.parameter = parameter;
        }

        public ConstuctorGenerator(System.Collections.Specialized.StringCollection parametersList) : this(null, parametersList) { }

        public ConstuctorGenerator(string protectionLevel, System.Collections.Specialized.StringCollection parametersList)
        {
            this.protectionLevel = protectionLevel;
            this.parametersList = parametersList;
            this.isList = true;
        }
        
        /// <summary>
        /// Implements <see cref="IParser"/>. Generates the constructor.
        /// </summary>
        /// <returns>A new constructor for the class as string.</returns>
        public string Parse()
        {
            var output = string.Empty;
            var builder = new System.Text.StringBuilder(output);

            if (protectionLevel == null)
                protectionLevel = "public";

            builder.Append($"{protectionLevel} {className}(");
            if (parameter != string.Empty)
            {
                if (isList) //and has no protection level modifier
                {
                    foreach (var listItem in parametersList)
                    {
                        if (listItem != null && listItem != string.Empty)
                            if (listItem != parametersList[parametersList.Count - 2])
                                builder.Append(listItem + ',');
                            else builder.Append(listItem);
                    }

                    builder.Append(')');
                }
                else if (parameter[0] == '{' && parameter[parameter.Length - 1] == '}') //isList and has protection modifier
                {
                    string[] parameters = parameter.Split(new char[] { '{', ',', '}' }, System.StringSplitOptions.None);

                    for (int i = 0; i < parameters.Length; i++)
                        if (parameters[i] != null && parameters[i] != string.Empty)
                            if (parameters[i] != parameters[parameters.Length - 2]) //idk how this works, but won't gonna complain about it..
                                builder.Append($"{parameters[i]},");
                            else builder.Append(parameters[i]);

                    builder.Append(')');
                }
                else builder.Append(parameter + ')');
            }
            else builder.Append(parameter + ')');
            output = builder.ToString();
            builder.Clear();
            
            return $"        CTOR{output}";
        }   
    }
}
