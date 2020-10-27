using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ASE_Assignment
{
    public partial class Form1 : Form
    {
        String command;
        Graphics g;
        public Form1()
        {
           
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            g = splitContainer1.Panel1.CreateGraphics(); // init the graphics from the drawing panel and allow the command line to access them.
        }

        private void _Panel2_Paint(object sender, PaintEventArgs e)
        {
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e) //When the "enter" button or key is pressed
        {
            command = textBox1.Text; // Copy text out of command box
            Console.WriteLine(command); // Print to console for logging
            if (command.Equals("circle"))  // Verify if actual command
            {
                Circle c = new Circle(Color.Blue, 0, 0, 6);
                c.Draw(g);
            }
            else
            {
                Console.WriteLine("Invalid command!"); // inform user of wrong commmand
            }
            textBox1.Text = ""; // empty the text box
        }
    }
}
