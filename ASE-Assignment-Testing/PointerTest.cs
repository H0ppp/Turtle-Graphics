using Microsoft.VisualStudio.TestTools.UnitTesting;
using ASE_Assignment;
using System.Drawing;

namespace ASE_Assignment_Testing
{
    [TestClass]
    public class PointerTest
    {
        static Graphics g;

        [TestMethod]
        public void TestMethod1()
        {
            Pointer.Instruct("test");
        }
    }
}
