using System.Runtime.InteropServices;

namespace XMLParser
{
    /// <summary>
    /// This class generates the encapsulations for the private fields in the new class.
    /// </summary>
    [System.Security.SecurityCritical]
    [ComVisible(true)]
    [Guid("f1f8ca04-77f6-46bd-80f5-0dc025fe823b")]
    static class Encapsulator
    {
        private static string protection;

        private static string type;

        private static string returnType;

        private static string name;

        private static uint count;

        private static System.Text.StringBuilder encapsulatedBuilder;

        private static void SetPrivates(string field)
        {
            string[] fieldContent = field.Split(new char[] { ' ' }, System.StringSplitOptions.None);

            count = (uint)fieldContent.Length;
            
            if (fieldContent.Length >= 6 && fieldContent[4] == "=") // only fields with values could be encapsulated..
            {
                protection = fieldContent[0];
                type = fieldContent[1];
                returnType = fieldContent[2];
                name = fieldContent[3];
            }
        }

        /// <summary>
        /// Encapsulates a given <paramref name="fieldToEncapsulate"/>.
        /// </summary>
        /// <param name="fieldToEncapsulate">A field to generate encapsulation for.</param>
        /// <returns>string</returns>
        public static string Encapsulate(string fieldToEncapsulate)
        {
            SetPrivates(fieldToEncapsulate.Remove(0,8));
            if (count >= 6) //we have actual value to encapsulate..
            {
                encapsulatedBuilder = new System.Text.StringBuilder();
                encapsulatedBuilder.Append($"        public {returnType} {name.FirstUpper()} ");
                encapsulatedBuilder.Append('{' + $" get => {name}; set => {name} = value; " + '}');
                return encapsulatedBuilder.ToString();
            }
            return string.Empty;
        }

        /// <summary>
        /// An extension method to make the first Letter of given string Upper-Case.
        /// </summary>
        /// <param name="str">String to recreate.</param>
        /// <returns></returns>
        public static string FirstUpper(this string str) => str[0].ToString().ToUpper() + str.Substring(1);
    }
}
