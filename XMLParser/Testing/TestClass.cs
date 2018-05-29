/*This class is generated by the XMLParser program.
Author: Petar Angelov (Petaaar). GitHub: https://github.com/Petaaar.
The project source code: https://github.com/Petaaar/XMLParser.
Thanks for using my parser!*/
namespace MyNamespace
{
    #region Dependencies
    using System;
    using System.IO;
    using System.Collections.Generic;

    #endregion Dependencies

    ///<summary>A class, generated automatically via XMLParser. WRITE YOUR SUMMARY HERE!</summary>
    public sealed class MyClass<T>
    {
        #region Private Fields

        private static string path = "HELLOO";

        private int myNumber;

        private readonly uint item;

        private const string TAB = "    ";

        #endregion Private Fields

        #region Encapsulated

        public string Path { get => path; set => path = value; }

        public string TAB { get => TAB; set => TAB = value; }

        #endregion Encapsulated

        #region Public Fields

        public readonly int intField = 5;

        public  double sixAndAHalf = 6.5;

        public static bool isItTrue = true;

        public  bool ItIsNotTrue = false;

        #endregion Public Fields

        #region Private Methods

        private void MyPrivateMethod()
        { 
        //Write your code here! 
        }

        private virtual bool IsItTrue(bool testArgument)
        { 
        //Write your code here! 
        }

        private void MyMethod(FileStream fileStream)
        { 
        //Write your code here! 
        }

        private static void MultiParams(uint num1, File file)
        { 
        //Write your code here! 
        }

        private int Add(int a, int b)
        { 
        //Write your code here! 
        }

        private void GenericPrivateMethod<T>(string first, string second)
        { 
        //Write your code here! 
        }

        #endregion Private Methods

        #region Public Methods

        public override string ToString()
        { 
        //Write your code here! 
        }

        public void VoidMethod()
        { 
        //Write your code here! 
        }

        public static void TestWParam(int testArgument)
        { 
        //Write your code here! 
        }

        public double PARAMS(double one, double two)
        { 
        //Write your code here! 
        }

        public void GenericPublicMethod<T>(string first, string second)
        { 
        //Write your code here! 
        }

        #endregion Public Methods


        public MyClass()
        {
        //Write your code here!
        }

        public MyClass(string parameter)
        {
        //Write your code here!
        }

        protected MyClass(int param)
        {
        //Write your code here!
        }

        public MyClass(int param1, uint param2)
        {
        //Write your code here!
        }

        protected MyClass(uint param1, string param2)
        {
        //Write your code here!
        }

    }

}
