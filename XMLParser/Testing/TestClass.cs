/*This class is generated by the XMLParser program.
Author: Petar Angelov (Petaaar). GitHub: https://github.com/Petaaar.
The project source code: https://github.com/Petaaar/XMLParser.
Thanks for using my parser!*/
namespace MyNamespace
{
    #region Dependencies
    //this region contains all assembly references used in the class
    using System;
    using System.IO;
    using System.Collections.Generic;

    #endregion Dependencies

    ///<summary>A class, generated automatically via XMLParser. WRITE YOUR SUMMARY HERE!</summary>
    public sealed class Class<T>
         : IComparable, ICloneable, IList<T>, Array
    {
        #region Private Fields

        ///<summary>Add your summary for the field here!</summary>
        private static string path = "HELLOO";

        ///<summary>Add your summary for the field here!</summary>
        private int myNumber;

        ///<summary>Add your summary for the field here!</summary>
        private readonly uint item;

        ///<summary>Add your summary for the field here!</summary>
        private const string TAB = "    ";

        #endregion Private Fields

        #region Encapsulated

        //this will be displayed ONLY is the <encapsulate> property is set to "true"!
        public string Path { get => path; set => path = value; }

        public string TAB { get => TAB; set => TAB = value; }

        #endregion Encapsulated

        #region Public Fields

        ///<summary>Add your summary for the field here!</summary>
        public readonly int intField = 5;

        ///<summary>Add your summary for the field here!</summary>
        public  double sixAndAHalf = 6.5;

        ///<summary>Add your summary for the field here!</summary>
        public static bool isItTrue = true;

        ///<summary>Add your summary for the field here!</summary>
        public  bool ItIsNotTrue = false;

        #endregion Public Fields

        #region Private Methods

        ///<summary>Write your summary for the method here!</summary>
        private void MyPrivateMethod()
        { 
        //Write your code here! 
        }

        ///<summary>Write your summary for the method here!</summary>
        private virtual bool IsItTrue(bool testArgument)
        { 
        //Write your code here! 
        }

        ///<summary>Write your summary for the method here!</summary>
        private void MyMethod(FileStream fileStream)
        { 
        //Write your code here! 
        }

        ///<summary>Write your summary for the method here!</summary>
        private static void MultiParams(uint num1, File file)
        { 
        //Write your code here! 
        }

        ///<summary>Write your summary for the method here!</summary>
        private int Add(int a, int b)
        { 
        //Write your code here! 
        }

        ///<summary>Write your summary for the method here!</summary>
        private void GenericPrivateMethod<T>(string first, string second)
        { 
        //Write your code here! 
        }

        #endregion Private Methods

        #region Public Methods

        ///<summary>Write your summary for the method here!</summary>
        public override string ToString()
        { 
        //Write your code here! 
        }

        ///<summary>Write your summary for the method here!</summary>
        public void VoidMethod()
        { 
        //Write your code here! 
        }

        ///<summary>Write your summary for the method here!</summary>
        public static void TestWParam(int testArgument)
        { 
        //Write your code here! 
        }

        ///<summary>Write your summary for the method here!</summary>
        public double PARAMS(double one, double two)
        { 
        //Write your code here! 
        }

        ///<summary>Write your summary for the method here!</summary>
        public void GenericPublicMethod<T>(string first, string second)
        { 
        //Write your code here! 
        }

        #endregion Public Methods


        ///<summary>Constructor. Generates a new instance of the class.</summary>
        public Class()
        {
        //Write your code here!
        }

        ///<summary>Constructor. Generates a new instance of the class.</summary>
        public Class(string parameter)
        {
        //Write your code here!
        }

        ///<summary>Constructor. Generates a new instance of the class.</summary>
        protected Class(int param)
        {
        //Write your code here!
        }

        ///<summary>Constructor. Generates a new instance of the class.</summary>
        public Class(int param1, uint param2)
        {
        //Write your code here!
        }

        ///<summary>Constructor. Generates a new instance of the class.</summary>
        protected Class(uint param1, string param2)
        {
        //Write your code here!
        }

    }

}
