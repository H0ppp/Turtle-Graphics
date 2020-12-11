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

    /// <summary>
    /// Window: This Class manages all interaction from the use with the applciation.
    /// </summary>
    public partial class Window : Form
    {
        public Graphics g;
        public List<Command> commands = new List<Command>();
        public static List<Command> ifCommands = new List<Command>();
        public static List<Command> loopCommands = new List<Command>();
        public static List<Command> methodCommands = new List<Command>();
        String[] commandArray;
        string ifLine, loopLine, currentMethod;

        /// <summary>
        /// Initalise window object.
        /// </summary>
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
        /// <summary>
        /// Called when enter button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void enterButton_Click(object sender, EventArgs e)
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
            Pointer.ClearInvalidBox();
            Pointer.ClearConsoleBox();
            Pointer.AddConsoleBox("PROGRAM RAN");
            Boolean ifEnabled = false; // Check to see if the commands are encapsulated by an if statement
            Boolean loopEnabled = false;
            Boolean methodEnabled = false;
            for (int i = 0; i < commands.Count; i++) // Iterate through command list
            {
                // METHOD PARSING
                if (methodEnabled && (Parser.IsEndMethod(commands[i].line) == false))
                {
                    operationChecker(commands[i]);
                    methodCommands.Add(commands[i]);
                    Pointer.AddConsoleBox("Method command added: " + commands[i].line);
                }
                else if (Parser.IsMethod(commands[i].line, out string methodName))
                {
                    Pointer.AddConsoleBox("Method Began");
                    methodEnabled = true;
                    currentMethod = methodName;
                    commands[i].operation = true;
                }
                else if (methodEnabled && Parser.IsEndMethod(commands[i].line))
                {
                    methodEnabled = false;
                    foreach (Method m in Parser.methodList)
                    {
                        if (m.Label.Equals(currentMethod, StringComparison.InvariantCultureIgnoreCase))
                        {
                            m.Commands = methodCommands;
                        }
                    }
                    Pointer.AddConsoleBox("Method ended"); // data logging
                }

                // METHOD RUNNING PARSING
                else if (Parser.IsRunMethod(commands[i].line))
                {
                    Pointer.AddConsoleBox("Method Ran"); // data logging
                }

                // IF PARSING
                else if (ifEnabled && (Parser.IsEndIf(commands[i].line) == false)) // check if command is encapsulated in if statement
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
                else if (ifEnabled && Parser.IsEndIf(commands[i].line)) // Check if reached end of if statement
                {
                    ifEnabled = false; // disable encapsulation
                    Pointer.AddConsoleBox("IF finished"); // data logging
                    ifChecker(ifLine); // Run ifChecker method to check syntax and outcome of IF statement
                }


                // LOOP PARSING
                else if (loopEnabled && (Parser.IsEndLoop(commands[i].line) == false)) // check if command is encapsulated in loop statement
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
                else if (loopEnabled && Parser.IsEndLoop(commands[i].line)) // Check if reached end of loop statement
                {
                    loopEnabled = false; // disable encapsulation
                    Pointer.AddConsoleBox("loop finished"); // data logging
                    Looper.LoopChecker(loopLine); // Run ifChecker method to check syntax and outcome of loop statement
                }

                // VAR PARSING
                else if (Parser.IsVar(commands[i].line))
                {
                    commands[i].operation = true;
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
        /// Checks the syntax of if commands
        /// </summary>
        /// <param name="ifCommand"></param>
        public void ifChecker(string ifCommand)
        {
            try
            {
                commandArray = ifCommand.Split(" ");  // split command into arguments

                if (Parser.checkVarInt(commandArray[1], out int x) && Parser.checkVarInt(commandArray[3], out int y)) // Check for valid variables or integers
                {
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
                                    Parser.IsEndIf(i.line);
                                    Parser.IsEndLoop(i.line);
                                    Parser.IsEndMethod(i.line);
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
                                    Parser.IsEndIf(i.line);
                                    Parser.IsEndLoop(i.line);
                                    Parser.IsEndMethod(i.line);
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
                                    Parser.IsEndIf(i.line);
                                    Parser.IsEndLoop(i.line);
                                    Parser.IsEndMethod(i.line);
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
                                    Parser.IsEndIf(i.line);
                                    Parser.IsEndLoop(i.line);
                                    Parser.IsEndMethod(i.line);
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
                        throw new IfSyntaxException(ifCommand); // Throw if line syntax is incorrect
                    }
                }
                else
                {
                    throw new NoSuchVariableException(ifCommand); // Throw if variable cannot be found
                }

            }
            catch (InternalException) { }
        }

        /// <summary>
        /// Sets the text in the command box
        /// </summary>
        /// <param name="command"></param>
        private void SetCommands(String command) // Setter for the commands label
        {
            commandBox.Text = command;
        }
        /// <summary>
        /// Called on load button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadButton_Clicked(object sender, EventArgs e) //LOAD BUTTON PRESSED
        {
            openFileDialog1.ShowDialog(); // Start the file opener
            List<string> commandstring = new List<string>();
            try
            {
                commandstring = File.ReadAllLines(Path.GetFullPath(openFileDialog1.FileName)).ToList(); // Add each line of the .txt to the commands list
            }
            catch (FileNotFoundException)
            {
                Pointer.AddConsoleBox("ERROR-15: Invalid file path"); // Post error to console
            }
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
        /// <summary>
        /// Called on save button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveButton_Clicked(object sender, EventArgs e) // SAVE BUTTON PRESSED
        {
            List<string> commandstring = new List<string>();
            foreach (Command c in commands)
            {
                commandstring.Add(c.line);
            }
            saveFileDialog1.ShowDialog(); // Start the file saver
            try
            {
                System.IO.File.WriteAllLines(Path.GetFullPath(saveFileDialog1.FileName), commandstring); // Write all the commands in the list into individual lines in the txt.
            }
            catch (FileNotFoundException)
            {
                Pointer.AddConsoleBox("ERROR-15: Invalid file path"); // Post error to console
            }
        }

        /// <summary>
        /// Called when quit button clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void quitButton_Clicked(object sender, EventArgs e) // QUIT BUTTON PRESSED
        {
            Application.Exit(); // Exit the applciation
        }


        /// <summary>
        /// Called when execute button clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void executeButton_Click(object sender, EventArgs e)
        {
            Execute(); // Call execute function
        }
        /// <summary>
        /// Called when clear button clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clearButton_Click(object sender, EventArgs e)
        {
            commands.Clear(); // Empty the Commands lists
            ifCommands.Clear();
            loopCommands.Clear();
            SetCommands("Commands:"); // Reset textbox
            Pointer.ClearInvalidBox(); // Reset command box
            Pointer.ClearConsoleBox(); // Clear console box
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        /// <summary>
        /// Called when clear method clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clearMethod_Click(object sender, EventArgs e)
        {
            Parser.ClearMethods();
            Pointer.AddConsoleBox("All Methods Cleared");
        }

        /// <summary>
        /// Called when clear screen clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clearScreen_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White); // Set background colour to white
            Pointer.AddConsoleBox("Screen Cleared");
        }

        /// <summary>
        /// Called when clear variables is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clearVars_Click(object sender, EventArgs e)
        {
            Parser.ClearVar(); // Clear variables list
            Pointer.AddConsoleBox("All Variables Cleared");
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
