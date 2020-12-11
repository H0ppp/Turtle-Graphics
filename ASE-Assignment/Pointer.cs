using System;
using System.Drawing;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace ASE_Assignment
{
    /// <summary>
    /// Pointer: This class parses the commands given by the user and instructs the turtle pointer how to react accordingly.
    /// </summary>
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
        /// <summary>
        /// Initialises the pointer object.
        /// </summary>
        /// <param name="picture">The pointer on the graphics screen</param>
        /// <param name="graphics">The graphics screen itself</param>
        /// <param name="invalid">Invalid commands box</param>
        /// <param name="cb">Console output box.</param>
        public static void Init(PictureBox picture, Graphics graphics, Label invalid, TextBox cb) //Initial assignment of external objects to allow interaction
        {
            p = picture; // Assign the turtle object.
            label = invalid; // Label is the invalid command box
            consoleBox = cb;
            pen = new Pen(penColour); // Set default pen colour
            g = graphics; // Graphics controller from split container (The drawing area)
        }
        /// <summary>
        /// Function for parsing commands through the pointer.
        /// </summary>
        /// <param name="command">The command to parse.</param>
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
                    pen.Color = penColour;
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
                    { throw new TrianglePosException(command); }
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
                    else { throw new RectangleArgsException(command); }

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
                    else { throw new FillArgsException(command); }
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
                    else { throw new CircleArgsException(command); }
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
                    else { throw new MoveArgsException(command); }
                }

                else if (commandArray[0].Equals("drawto", StringComparison.InvariantCultureIgnoreCase))
                {
                    if (Parser.checkVarInt(commandArray[1], out int newx) && Parser.checkVarInt(commandArray[2], out int newy))
                    { //See if arg given for X is valid integer
                        Draw(newx, newy, g); // BOTH INTS
                    }
                    else { throw new DrawArgsException(command); }
                }

                else if (commandArray[0].Equals("reset", StringComparison.InvariantCultureIgnoreCase))
                {
                    Move(20, 20); // move the pointer back to original pos
                }
                else { throw new InvalidCommandException(command); }

            }
            catch (IndexOutOfRangeException)
            {
                AddConsoleBox("ERROR-08: Not enough arguments were given");
                AddInvalidBox(command);

            }
            catch (InternalException) { } // Catch any internal exceptions thrown

            p.Visible = true;
            p.Invalidate();
        }
        /// <summary>
        /// Move the pointer to the given co-ord
        /// </summary>
        /// <param name="x">X Position</param>
        /// <param name="y">Y Position</param>
        static void Move(int x, int y) // Move the turtle to the given X & Y coordinate
        {
            p.Location = new System.Drawing.Point(x, y); // Assign turtles location to new point
        }
        /// <summary>
        /// Draw a line from the pointer to the co-ord given.
        /// </summary>
        /// <param name="x">X Postion</param>
        /// <param name="y">Y Position</param>
        /// <param name="g">Graphics panel to draw the line on</param>
        static void Draw(int x, int y, Graphics g) // Draw a line between the current and new positions
        {
            Point original = new Point(p.Location.X + (p.Width / 2), p.Location.Y + (p.Height / 2)); // Determine the centre of the Turtle to draw from
            Point next = new Point(x + (p.Width / 2), y + (p.Height / 2)); // Determine where the centre will be at the new point
            g.DrawLine(pen, original, next); // Draw line
            Move(x, y); // execute move function
        }
        /// <summary>
        /// Adds a line to the console box
        /// </summary>
        /// <param name="line">Adds a line to the console box.</param>
        public static void AddConsoleBox(string line)
        {
            consoleBox.Text = consoleBox.Text + "\r\n" + line;
        }
        /// <summary>
        /// Clears the console box
        /// </summary>
        public static void ClearConsoleBox()
        {
            consoleBox.Text = "CONSOLE OUTPUT \r\n ------------------- \r\n";
        }
        /// <summary>
        /// Adds a line to the invalid command box.
        /// </summary>
        /// <param name="line">The line to add to the text box</param>
        public static void AddInvalidBox(string line)
        {
            label.Text = label.Text + "\r\n" + line;
        }
        /// <summary>
        /// Clears the invalid command box
        /// </summary>
        public static void ClearInvalidBox()
        {
            label.Text = "Invalid Commands: \r\n ";
        }
    }
}