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
        public List<Command> commands = new List<Command>();
        public static List<Command> ifCommands = new List<Command>();
        public static List<Command> loopCommands = new List<Command>();
        String[] commandArray;
        string ifLine, loopLine;
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
            Pointer.Init(turtle, g, invalidBox, consoleBox); // Initialise the pointer
        }

        private void Button2_Click(object sender, EventArgs e)
        { //When the "enter" button or key is pressed
            if (textBox1.Text.Equals("run", StringComparison.InvariantCultureIgnoreCase)) // Check if run command entered
            {
                Execute();
            }
            else if (textBox1.Text.Equals("clearcommands", StringComparison.InvariantCultureIgnoreCase)) // Check if clear command entered
            {
                commands.Clear(); // Empty the Commands list
                SetCommands("Commands:"); // Reset textbox
                invalidBox.Text = "Invalid Commands:"; // Reset command box
            }
            else
            {
                commands.Add(new Command
                {
                    line = textBox1.Text,
                    operation = false
                }); // Add command to the list of commands
                SetCommands(commandBox.Text + "\n" + textBox1.Text); // Show command entered on screen
            }
            textBox1.Text = ""; // empty the text box
        }

        /// <summary>
        /// Takes the commands from the command list and runs them through the correct function based on type.
        /// </summary>
        public void Execute()
        {
            Pointer.ClearConsoleBox();
            Pointer.AddConsoleBox("PROGRAM RAN");
            Boolean ifEnabled = false; // Check to see if the commands are encapsulated by an if statement
            Boolean loopEnabled = false;
            for (int i = 0; i < commands.Count; i++) // Iterate through command list
            {
                if (ifEnabled && (Parser.IsEnd(commands[i].line) == false)) // check if command is encapsulated in if statement
                {
                    operationChecker(commands[i]); // Check if command is operation or not
                    ifCommands.Add(commands[i]); // add to list of commands in if statement
                    Pointer.AddConsoleBox("IF command added: " + commands[i].line); // Data logging
                }
                else if (Parser.IsIf(commands[i].line)) // Check if command is an If statement
                {
                    Pointer.AddConsoleBox("If Began"); // Data Logging
                    ifEnabled = true; // enable if encapsulation
                    ifLine = commands[i].line; // Save the line for the syntax checking
                    commands[i].operation = true;
                }
                else if (ifEnabled && Parser.IsEnd(commands[i].line)) // Check if reached end of if statement
                {
                    ifEnabled = false; // disable encapsulation
                    Pointer.AddConsoleBox("IF finished"); // data logging
                    ifChecker(ifLine); // Run ifChecker method to check syntax and outcome of IF statement
                }
                else if (loopEnabled && (Parser.IsEnd(commands[i].line) == false)) // check if command is encapsulated in loop statement
                {
                    operationChecker(commands[i]); // Check if command is operation or not
                    loopCommands.Add(commands[i]); // add to list of commands in if statement
                    Pointer.AddConsoleBox("LOOP command added: " + commands[i].line); // Data logging
                }
                else if (Parser.IsLoop(commands[i].line))
                {
                    loopEnabled = true;
                    Pointer.AddConsoleBox("loop began"); // data logging
                    loopLine = commands[i].line; // Save the line for the syntax checking
                    commands[i].operation = true;
                }
                else if (Parser.IsVar(commands[i].line))
                {
                    commands[i].operation = true;
                }
                else if (loopEnabled && Parser.IsEnd(commands[i].line)) // Check if reached end of loop statement
                {
                    loopEnabled = false; // disable encapsulation
                    Pointer.AddConsoleBox("loop finished"); // data logging
                    Looper.LoopChecker(loopLine); // Run ifChecker method to check syntax and outcome of loop statement
                }
                else
                {
                    Pointer.Instruct(commands[i].line); // Command is not an operation or encapsulated by one, run as normal
                }
            }
        }
        /// <summary>
        /// Check the command to see if it is an operational commmand or a drawing command.
        /// </summary>
        /// <param name="c"></param>
        public void operationChecker(Command c) // Check encapsulated commands to see if they are operational or not
        {
            commandArray = c.line.Split(" ");
            if (commandArray[0].Equals("if", StringComparison.InvariantCultureIgnoreCase) || commandArray[0].Equals("loop", StringComparison.InvariantCultureIgnoreCase) || commandArray[0].Equals("var", StringComparison.InvariantCultureIgnoreCase) || commandArray[0].Equals("end", StringComparison.InvariantCultureIgnoreCase))
            {
                c.operation = true;
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ifCommand"></param>
        public void ifChecker(string ifCommand)
        {
            commandArray = ifCommand.Split(" ");  // split command into arguments
            int x = 0; // assign default values
            int y = 0;

            try
            {
                x = Int32.Parse(commandArray[1]); // Try and parse an integer from the argument
            }
            catch (FormatException) // Catch the exception if an int cannot be parsed
            {
                foreach (Variable v in Parser.variableList)
                { // Iterate through varaible list
                    if (v.Label.Equals(commandArray[1], StringComparison.InvariantCultureIgnoreCase))
                    { // compare each variable label to the the argument
                        x = v.Value;
                    }
                }
                Pointer.AddConsoleBox("ERROR-13: No such variable could be found."); // Data logging
                Pointer.label.Text = Pointer.label.Text + "\n" + ifCommand; // Add command to invalid list on screen
            }

            try
            {
                y = Int32.Parse(commandArray[3]); // Try and parse an integer from the argument
            }
            catch (FormatException) // Catch the exception if an int cannot be parsed
            {
                foreach (Variable v in Parser.variableList) // Iterate through varaible list
                {
                    if (v.Label.Equals(commandArray[3], StringComparison.InvariantCultureIgnoreCase)) // compare each variable label to the the argument
                    {
                        y = v.Value; // Attempt to parse integer from value string, this should not fail.
                    }
                }
                Console.Write("No var found"); // Data logging
                Pointer.label.Text = Pointer.label.Text + "\n" + ifCommand; // Add command to invalid list on screen
            }

            // EQUALS
            if (commandArray[2].Equals("=", StringComparison.InvariantCultureIgnoreCase))  // Check which operator is used
            {
                if (x == y) // Check if if statement is true
                {
                    foreach (Command i in ifCommands) // iterate through encapsulated commands
                    {
                        if (i.operation) // if command is an operational command
                        {
                            Parser.IsVar(i.line); // check type
                            Parser.IsLoop(i.line);
                            Parser.IsEnd(i.line);
                        }
                        else
                        {
                            Pointer.Instruct(i.line); // Iterate through the drawing commands encapsulated.
                        }
                    }
                }
            }
            // GREATER THAN
            else if (commandArray[2].Equals(">", StringComparison.InvariantCultureIgnoreCase)) // Check which operator is used
            {
                if (x > y) // Check if if statement is true
                {
                    foreach (Command i in ifCommands) // iterate through encapsulated commands
                    {
                        if (i.operation) // if command is an operational command
                        {
                            Parser.IsVar(i.line); // check type
                            Parser.IsLoop(i.line);
                            Parser.IsEnd(i.line);
                        }
                        else
                        {
                            Pointer.Instruct(i.line); // Iterate through the drawing commands encapsulated.
                        }
                    }
                }
            }
            // LESS THAN
            else if (commandArray[2].Equals("<", StringComparison.InvariantCultureIgnoreCase)) // Check which operator is used
            {
                if (x < y) // Check if if statement is true
                {
                    foreach (Command i in ifCommands) // iterate through encapsulated commands
                    {
                        if (i.operation) // if command is an operational command
                        {
                            Parser.IsVar(i.line); // check type
                            Parser.IsLoop(i.line);
                            Parser.IsEnd(i.line);
                        }
                        else
                        {
                            Pointer.Instruct(i.line); // Iterate through the drawing commands encapsulated.
                        }
                    }
                }
            }
            // NOT EQUAL
            else if (commandArray[2].Equals("!=", StringComparison.InvariantCultureIgnoreCase)) // Check which operator is used
            {
                if (x != y) // Check if if statement is true
                {
                    foreach (Command i in ifCommands) // iterate through encapsulated commands
                    {
                        if (i.operation) // if command is an operational command
                        {
                            Parser.IsVar(i.line); // check type
                            Parser.IsLoop(i.line);
                            Parser.IsEnd(i.line);
                        }
                        else
                        {
                            Pointer.Instruct(i.line); // Iterate through the drawing commands encapsulated.
                        }
                    }
                }
            }
            else
            {
                Pointer.AddConsoleBox("ERROR-14: If syntax invalid"); // Post error to console
            }
        }

        private void SetCommands(String command) // Setter for the commands label
        {
            commandBox.Text = command;
        }

        private void LoadButton_Clicked(object sender, EventArgs e) //LOAD BUTTON PRESSED
        {
            openFileDialog1.ShowDialog(); // Start the file opener
            List<string> commandstring = new List<string>();
            commandstring = File.ReadAllLines(Path.GetFullPath(openFileDialog1.FileName)).ToList(); // Add each line of the .txt to the commands list
            foreach (string s in commandstring)
            {
                commands.Add(new Command
                {
                    line = s,
                    operation = false
                }); // Add command to the list of commands
            }
            SetCommands("Commands:"); // Reset textbox
            for (int i = 0; i < commands.Count; i++) // Iterate through all lines
            {
                SetCommands(commandBox.Text + "\n" + commands[i].line); // Show opened file on screen
            }

        }
        private void SaveButton_Clicked(object sender, EventArgs e) // SAVE BUTTON PRESSED
        {
            List<string> commandstring = new List<string>();
            foreach (Command c in commands)
            {
                commandstring.Add(c.line);
            }
            saveFileDialog1.ShowDialog(); // Start the file saver
            System.IO.File.WriteAllLines(Path.GetFullPath(saveFileDialog1.FileName), commandstring); // Write all the commands in the list into individual lines in the txt.
        }
        private void quitButton_Clicked(object sender, EventArgs e) // QUIT BUTTON PRESSED
        {
            Application.Exit(); // Exit the applciation
        }

        private void executeButton_Click(object sender, EventArgs e)
        {
            Execute(); // Call execute function
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            commands.Clear(); // Empty the Commands list
            ifCommands.Clear();
            loopCommands.Clear();
            SetCommands("Commands:"); // Reset textbox
            Pointer.ClearInvalidBox(); // Reset command box
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void clearVars_Click(object sender, EventArgs e)
        {
            Parser.ClearVar();
            Pointer.AddConsoleBox("All Variables Cleared");
        }
    }
}
