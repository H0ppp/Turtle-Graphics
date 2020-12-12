using System;
using System.Collections.Generic;
using System.Text;
using ASE_Assignment;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ASE_Assignment_Testing
{
    /// <summary>
    /// Test class for testing Parser.cs functions
    /// </summary>
    [TestClass]
    public class ParserTest
    {
        /// <summary>
        /// Test if the IsIf method can detect valid if statements
        /// </summary>
        [TestMethod]
        public void TestIf()
        {
            Assert.IsTrue(Parser.IsIf("if 1 = 1"));
        }

        /// <summary>
        /// Test if methods can be added to and found from the methodList
        /// </summary>
        [TestMethod]
        public void TestMethod()
        {
            Parser.methodList.Add(new Method
            {
                Label = "TESTMETHOD"
            }); // Add the test method to the method list
            Method m = Parser.FindMethod("TESTMETHOD");
            Assert.IsNotNull(m);
        }
        /// <summary>
        /// Test if variable can be added and found in list
        /// </summary>
        [TestMethod]
        public void TestVariable()
        {
            Parser.variableList.Add(new Variable
            {
                Label = "test",
                Value = 3
            });
            Variable v = Parser.FindVar("test");
            Assert.IsNotNull(v);
            Assert.AreEqual(3, v.Value);
        }

        /// <summary>
        /// Test if runMethod can acknowledge an invalid command
        /// </summary>
        [TestMethod]
        public void TestRunMethod()
        {
            Assert.IsFalse(Parser.IsRunMethod("run method"));
        }

        /// <summary>
        /// Test if the is loop function can parse a loop
        /// </summary>
        [TestMethod]
        public void TestIsLoop()
        {
            Assert.IsTrue(Parser.IsLoop("loop 1 = 1"));
        }
    }
}
