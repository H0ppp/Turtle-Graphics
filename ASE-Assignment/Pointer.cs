using System;
using System.Drawing;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace ASE_Assignment
{
    /*
     * Pointer: This class parses the commands given by the user and instructs the turtle pointer how to react accordingly.
     */
    public class Pointer
    {
        static Graphics g;
        static PictureBox p;
        static Pen pen;
        public static Label label;
        public static TextBox consoleBox;
        static Color penColour = Color.Black;
        static Color shapeColour = Color.Red;
        static bool fill = true;
        public static void Init(PictureBox picture, Graphics graphics, Label invalid, TextBox cb) //Initial assignment of external objects to allow interaction
        {
            p = picture; // Assign the turtle object.
            label = invalid; // Label is the invalid command box
            consoleBox = cb;
            pen = new Pen(penColour); // Set default pen colour
            g = graphics; // Graphics controller from split container (The drawing area)
        }

        public static void Instruct(String command)  // Parse the commands given
        {
            String[] commandArray; // The array to store the parts of the command
            AddConsoleBox(command); // Print to console for logging
            commandArray = command.Split(" "); // Split the command entered into parts to determine args given.
            p.Visible = false;
            p.Invalidate();
            try
            {
                if (commandArray[0].Equals("colour", StringComparison.InvariantCultureIgnoreCase))// COLOUR ASSIGNmENT
                {
                    penColour = Color.FromName(commandArray[1]);
                    pen.Color = Color.FromName(commandArray[1]);
                }
                else if (commandArray[0].Equals("fillcolour", StringComparison.InvariantCultureIgnoreCase)) // FILL COLOUR ASSIGNMENT
                {
                    shapeColour = Color.FromName(commandArray[1]);
                }
                else if (commandArray[0].Equals("triangle", StringComparison.InvariantCultureIgnoreCase)) // TRIANGLE DRAWING
                {
                    if (Parser.checkVarInt(commandArray[1], out int x2) && Parser.checkVarInt(commandArray[2], out int y2) && Parser.checkVarInt(commandArray[3], out int x3) && Parser.checkVarInt(commandArray[3], out int y3))  // Ensure value given is an integer
                    {

                        if (fill) // Fill the triangle with the correct colour if fill is enabled
                        {
                            Triangle t = new Triangle(shapeColour, penColour, p.Location.X, p.Location.Y, x2, y2, x3, y3);
                            t.Draw(g);
                        }
                        else // else draw transparent triangle
                        {
                            Triangle t = new Triangle(Color.Transparent, penColour, p.Location.X, p.Location.Y, x2, y2, x3, y3);
                            t.Draw(g);
                        }
                    }
                    else
                    {
                        AddConsoleBox("ERROR-01: One or more triangle co-ordinates given were not valid numbers"); // Inform user that there command is incorrect
                        addInvalidBox(command); // Add command to invalid list on screen

                    }
                }
                else if (commandArray[0].Equals("rectangle", StringComparison.InvariantCultureIgnoreCase))  // RECTANGLE DRAWING
                {
                    if (Parser.checkVarInt(commandArray[1], out int width) && Parser.checkVarInt(commandArray[2], out int height))
                    { //See if arg given for width is valid integer

                        if (fill) // Fill the rectangle with the correct colour if fill is enabled
                        {
                            Rect r = new Rect(shapeColour, penColour, p.Location.X, p.Location.Y, width, height);
                            r.Draw(g);
                        }
                        else // else draw transparent rectangle
                        {
                            Rect r = new Rect(Color.Transparent, penColour, p.Location.X, p.Location.Y, width, height);
                            r.Draw(g);
                        }
                    }
                    else
                    {
                        AddConsoleBox("ERROR-02: Invalid argument, expected a number or variable for rectangle dimension"); // Inform user that there command is incorrect
                        addInvalidBox(command); // Add command to invalid list on screen
                    }

                }
                else if (commandArray[0].Equals("fill", StringComparison.InvariantCultureIgnoreCase))  // Fill Boolean
                { // Verify if actual command and ignore case
                    if (commandArray[1].Equals("on", StringComparison.InvariantCultureIgnoreCase)) // Enable fill
                    {
                        fill = true;
                    }
                    else if (commandArray[1].Equals("off", StringComparison.InvariantCultureIgnoreCase)) // Disable fill
                    {
                        fill = false;
                    }
                    else
                    {
                        AddConsoleBox("ERROR-04: Invalid argument, expected on/off for fill."); // Inform user that there command is incorrect
                        addInvalidBox(command); // Add command to invalid list on screen
                    }
                }

                else if (commandArray[0].Equals("circle", StringComparison.InvariantCultureIgnoreCase))  // CIRCLE DRAWING
                { // Verify if actual command and ignore case
                    if (Parser.checkVarInt(commandArray[1], out int radius))
                    { //See if arg given for radius is valid integer
                        if (fill) // Fill the circle with the correct colour if fill is enabled
                        {
                            Circle c = new Circle(shapeColour, penColour, p.Location.X, p.Location.Y, radius);
                            c.Draw(g);
                        }
                        else // Else draw transparent circle
                        {
                            Circle c = new Circle(Color.Transparent, penColour, p.Location.X, p.Location.Y, radius);
                            c.Draw(g);
                        }
                    }
                    else
                    {
                        AddConsoleBox("ERROR-05: Invalid argument, expected a number or variable for circle radius"); // Inform user that there command is incorrect
                        addInvalidBox(command); // Add command to invalid list on screen
                    }
                }

                else if (commandArray[0].Equals("clear", StringComparison.InvariantCultureIgnoreCase))
                {
                    g.Clear(Color.White); // clear the screen
                }
                else if (commandArray[0].Equals("moveto", StringComparison.InvariantCultureIgnoreCase))
                {
                    if (Parser.checkVarInt(commandArray[1], out int newx) && Parser.checkVarInt(commandArray[2], out int newy))
                    { //See if arg given for X is valid integer
                        Move(newx, newy); // BOTH INTS
                    }
                    else
                    {

                        AddConsoleBox("ERROR-07: Invalid argument, expected a number for move to x/y co-ord"); // Inform user that there command is incorrect
                        addInvalidBox(command); // Add command to invalid list on screen

                    }
                }

                else if (commandArray[0].Equals("drawto", StringComparison.InvariantCultureIgnoreCase))
                {
                    if (Parser.checkVarInt(commandArray[1], out int newx) && Parser.checkVarInt(commandArray[2], out int newy))
                    { //See if arg given for X is valid integer
                        Draw(newx, newy.g); // BOTH INTS
                    }
                    else
                    {

                        AddConsoleBox("ERROR-08: Invalid argument, expected a number for draw to x/y co-ord"); // Inform user that there command is incorrect
                        addInvalidBox(command); // Add command to invalid list on screen

                    }
                }

                else if (commandArray[0].Equals("reset", StringComparison.InvariantCultureIgnoreCase))
                {
                    Move(20, 20); // move the pointer back to original pos
                }
                else
                {
                    AddConsoleBox("ERROR-10: Invalid command!"); // inform user of wrong commmand
                    addInvalidBox(command);

                }

            }
            catch (IndexOutOfRangeException)
            {
                AddConsoleBox("ERROR-11: Not enough arguments were given");
                addInvalidBox(command);

            }
            p.Visible = true;
            p.Invalidate();
        }

        static void Move(int x, int y) // Move the turtle to the given X & Y coordinate
        {
            p.Location = new System.Drawing.Point(x, y); // Assign turtles location to new point
        }

        static void Draw(int x, int y, Graphics g) // Draw a line between the current and new positions
        {
            Point original = new Point(p.Location.X + (p.Width / 2), p.Location.Y + (p.Height / 2)); // Determine the centre of the Turtle to draw from
            Point next = new Point(x + (p.Width / 2), y + (p.Height / 2)); // Determine where the centre will be at the new point
            g.DrawLine(pen, original, next); // Draw line
            Move(x, y); // execute move function
        }
        public static void AddConsoleBox(string line)
        {
            consoleBox.Text = consoleBox.Text + "\r\n" + line;
        }

        public static void ClearConsoleBox()
        {
            consoleBox.Text = "CONSOLE OUTPUT \r\n ------------------- \r\n";
        }

        public static void addInvalidBox(string line)
        {
            label.Text = label.Text + "\r\n" + line;
        }

        public static void ClearInvalidBox()
        {
            consoleBox.Text = "Invalid Commands: \r\n ";
        }
    }
}