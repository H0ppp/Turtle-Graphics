using Microsoft.VisualStudio.TestTools.UnitTesting;
using ASE_Assignment;
using System.Drawing;
using System.Windows.Forms;
using System;
using System.Diagnostics;
using System.IO;

namespace ASE_Assignment_Testing
{
    /// <summary>
    /// PointerTest: This class tests various commands that are parsed by the Pointer class to see if the output is correct
    /// </summary>
    [TestClass]
    public class PointerTest
    {
        /// <summary>
        /// TestTurtleMovement: See if the turtle is moved to the correct location when MoveTo is called
        /// </summary>
        [TestMethod]
        public void TestTurtleMovement()
        {
            var w = new Window(); // Create window for graphics
            var p = new PictureBox(); // Create turtle 
            var l = new Label(); // Create label to satisfy arguments
            var t = new TextBox(); // Create textbox to represent console
            var g = w.CreateGraphics(); // Create graphics from window
            Pointer.Init(p, g, l, t); // Initialise point object
            Pointer.Instruct("moveto 200 300"); // Execute move to command
            Assert.AreEqual(200, p.Location.X); // Check if turtle is in right place
            Assert.AreEqual(300, p.Location.Y); // Check if turtle is in right place
        }

        /// <summary>
        /// TestCircleError: Test if the command is added to the invalid list when a string is given for radius
        /// </summary>
        [TestMethod]
        public void TestCircleError()
        {
            var w = new Window(); // Create window for graphics
            var p = new PictureBox(); // Create turtle 
            var l = new Label(); // Create label to satisfy arguments
            var t = new TextBox(); // Create textbox to represent console
            var g = w.CreateGraphics(); // Create graphics from window
            Pointer.Init(p, g, l, t); // Initialise point object
            Pointer.Instruct("circle wrong"); // Execute circle command with wrong argument
            Assert.IsTrue(l.Text.Contains("circle wrong")); // Check if invalid command has been added to list of invalids
        }

        /// <summary>
        /// TestUnknownCommand: Test if an unrecognised command is added to the invalid list
        /// </summary>
        [TestMethod]
        public void TestUnknownCommand()
        {
            var w = new Window(); // Create window for graphics
            var p = new PictureBox(); // Create turtle 
            var l = new Label(); // Create label to satisfy arguments
            var g = w.CreateGraphics(); // Create graphics from window
            var t = new TextBox(); // Create textbox to represent console
            Pointer.Init(p, g, l, t); // Initialise point object
            Pointer.Instruct("Incorrect command"); // Execute unknown command
            Assert.IsTrue(l.Text.Contains("Incorrect command")); // Check if invalid command has been added to list of invalids
        }
        /// <summary>
        /// TestLoopCommand: Test if the loop command works correctly.
        /// </summary>
        [TestMethod]
        public void TestLoopCommand()
        {
            var w = new Window(); // Create window for graphics
            var p = new PictureBox(); // Create turtle 
            var l = new Label(); // Create label to satisfy arguments
            var g = w.CreateGraphics(); // Create graphics from window
            var t = new TextBox(); // Create textbox to represent console
            Pointer.Init(p, g, l, t); // Initialise point object
            Pointer.Instruct(""); // Execute loop command
                                  // Not yet implemented

        }
    }
}
