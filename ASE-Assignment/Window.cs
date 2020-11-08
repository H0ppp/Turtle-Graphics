using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ASE_Assignment
{

    /*
     * Window: This Class manages all interaction from the use with the applciation.
     */
    public partial class Window : Form
    {
        public Graphics g;
        List<string> commands = new List<string>();
        public Window()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true); // Allow transparent backgrounds
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            g = splitContainer1.Panel1.CreateGraphics(); // init the graphics from the drawing panel and allow the command line to access them.
            g.Clear(Color.White); // clear the screen
            turtle.Parent = splitContainer1.Panel1;
            Pointer.Init(turtle, g, invalidBox); // Initialise the pointer
        }

        private void button2_Click(object sender, EventArgs e) { //When the "enter" button or key is pressed
            if(textBox1.Text.Equals("run", StringComparison.InvariantCultureIgnoreCase)) // Check if run command entered
            {
                Console.WriteLine("PROGRAM RAN - INVALID COMMANDS WILL SHOW ERRORS HERE");
                for (int i = 0; i < commands.Count; i++) // Iterate through command list
                {
                    Pointer.Instruct(commands[i]); // Run all commands from list
                }
            } else if (textBox1.Text.Equals("clearcommands", StringComparison.InvariantCultureIgnoreCase)) // Check if clear command entered
            {
                commands.Clear(); // Empty the Commands list
                SetCommands("Commands:"); // Reset textbox
                invalidBox.Text = "Invalid Commands:"; // Reset command box
            }
            else
            {
                commands.Add(textBox1.Text); // Add command to the list of commands
                SetCommands(commandBox.Text + "\n" + textBox1.Text); // Show command entered on screen
            }
            textBox1.Text = ""; // empty the text box
        }


        private void SetCommands(String command) // Setter for the commands label
        {
            commandBox.Text = command; 
        }

        private void LoadButton_Clicked(object sender, EventArgs e) //LOAD BUTTON PRESSED
        {
            openFileDialog1.ShowDialog(); // Start the file opener
            commands = File.ReadAllLines(Path.GetFullPath(openFileDialog1.FileName)).ToList(); // Add each line of the .txt to the commands lsit
            SetCommands("Commands:"); // Reset textbox
            for(int i = 0; i < commands.Count; i++) // Iterate through all lines
            {
                SetCommands(commandBox.Text + "\n" + commands[i]); // Show opened file on screen
            }

        }
        private void SaveButton_Clicked(object sender, EventArgs e) // SAVE BUTTON PRESSED
        {
            saveFileDialog1.ShowDialog(); // Start the file saver

            System.IO.File.WriteAllLines(Path.GetFullPath(saveFileDialog1.FileName), commands); // Write all the commands in the list into individual lines in the txt.

        }
        private void quitButton_Clicked(object sender, EventArgs e) // QUIT BUTTON PRESSED
        {
            Application.Exit(); // Exit the applciation
        }

        private void executeButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("PROGRAM RAN - INVALID COMMANDS WILL SHOW ERRORS HERE");
            for (int i = 0; i < commands.Count; i++) // Iterate through command list
            {
                Pointer.Instruct(commands[i]); // Run all commands from list
            }
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            commands.Clear(); // Empty the Commands list
            SetCommands("Commands:"); // Reset textbox
            invalidBox.Text = "Invalid Commands:"; // Reset command box
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {
            //turtle.Invalidate();
        }
    }
}
