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
            g = Form1.g;
            Pointer.Instruct("test", g);
        }
    }
}
