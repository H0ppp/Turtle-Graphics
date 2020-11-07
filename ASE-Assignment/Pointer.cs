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
        static Label label;
        static Color penColour = Color.Black;
        static Color shapeColour = Color.Red;
        static bool fill = true;
        public static void Init(PictureBox picture, Graphics graphics, Label invalid) //Initial assignment of external objects to allow interaction
        {
            p = picture; // Assign the turtle object.
            label = invalid; // Label is the invalid command box
            pen = new Pen(penColour); // Set default pen colour
            g = graphics; // Graphics controller from split container (The drawing area)
        }

        public static void Instruct(String command)  // Parse the commands given
        {
            String[] commandArray; // The array to store the parts of the command
            Console.WriteLine(command); // Print to console for logging
            commandArray = command.Split(" "); // Split the command entered into parts to determine args given.
            try
            {
                if (commandArray[0].Equals("colour", StringComparison.InvariantCultureIgnoreCase))// COLOUR ASSIGNmENT
                {
                    penColour = Color.FromName(commandArray[1]);
                }
                else if (commandArray[0].Equals("fillcolour", StringComparison.InvariantCultureIgnoreCase)) // FILL COLOUR ASSIGNMENT
                {
                    shapeColour = Color.FromName(commandArray[1]);
                }
                else if (commandArray[0].Equals("triangle", StringComparison.InvariantCultureIgnoreCase)) // TRIANGLE DRAWING
                {
                    if (int.TryParse(commandArray[1], out int x2) && int.TryParse(commandArray[2], out int y2) && int.TryParse(commandArray[3], out int x3) && int.TryParse(commandArray[3], out int y3))  // Ensure value given is an integer
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
                        Console.WriteLine("One or more triangle co-ordinates given were not valid numbers"); // Inform user that there command is incorrect
                        label.Text = label.Text + "\n" + command; // Add command to invalid list on screen

                    }
                }
                else if (commandArray[0].Equals("rectangle", StringComparison.InvariantCultureIgnoreCase))  // RECTANGLE DRAWING
                { // Verify if actual command and ignore case
                    if (int.TryParse(commandArray[1], out int width))
                    { //See if arg given is valid integer
                        if (int.TryParse(commandArray[2], out int height))
                        { //See if arg given is valid integer
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
                            Console.WriteLine("Invalid argument, expected a number for rectangle height"); // Inform user that there command is incorrect
                            label.Text = label.Text + "\n" + command; // Add command to invalid list on screen

                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid argument, expected a number for rectangle width"); // Inform user that there command is incorrect
                        label.Text = label.Text + "\n" + command; // Add command to invalid list on screen

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
                        Console.WriteLine("Invalid argument, expected on/off for fill."); // Inform user that there command is incorrect
                        label.Text = label.Text + "\n" + command; // Add command to invalid list on screen
                    }
                }

                else if (commandArray[0].Equals("circle", StringComparison.InvariantCultureIgnoreCase))  // CIRCLE DRAWING
                { // Verify if actual command and ignore case
                    if (int.TryParse(commandArray[1], out int radius))
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
                        Console.WriteLine("Invalid argument, expected a number for  circle radius"); // Inform user that there command is incorrect
                        label.Text = label.Text + "\n" + command; // Add command to invalid list on screen

                    }
                }

                else if (commandArray[0].Equals("clear", StringComparison.InvariantCultureIgnoreCase))
                {
                    g.Clear(Color.White); // clear the screen
                }
                else if (commandArray[0].Equals("moveto", StringComparison.InvariantCultureIgnoreCase))
                {
                    if (int.TryParse(commandArray[1], out int newx))
                    { //See if arg given for X is valid integer
                        if (int.TryParse(commandArray[2], out int newy))
                        { //See if arg given for Y is valid integer
                            Move(newx, newy);
                        }
                        else
                        {
                            Console.WriteLine("Invalid argument, expected a number for move to y co-ord"); // Inform user that there command is incorrect
                            label.Text = label.Text + "\n" + command; // Add command to invalid list on screen

                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid argument, expected a number for move to x co-ord"); // Inform user that there command is incorrect
                        label.Text = label.Text + "\n" + command; // Add command to invalid list on screen

                    }
                }
                else if (commandArray[0].Equals("drawto", StringComparison.InvariantCultureIgnoreCase))
                {
                    if (int.TryParse(commandArray[1], out int newx))
                    { //See if arg given for radius is valid integer
                        if (int.TryParse(commandArray[2], out int newy))
                        { //See if arg given for radius is valid integer
                            Draw(newx, newy, g); // Execute draw function
                        }
                        else
                        {
                            Console.WriteLine("Invalid argument, expected a number for draw to y co-ord"); // Inform user that there command is incorrect
                            label.Text = label.Text + "\n" + command; // Add command to invalid list on screen

                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid argument, expected a number for draw to x co-ord"); // Inform user that there command is incorrect
                        label.Text = label.Text + "\n" + command; // Add command to invalid list on screen

                    }
                }
                else if (commandArray[0].Equals("reset", StringComparison.InvariantCultureIgnoreCase))
                {
                    Move(12, 12); // move the pointer back to original pos
                }
                else
                {
                    Console.WriteLine("Invalid command!"); // inform user of wrong commmand
                    label.Text = label.Text + "\n" + command;

                }

            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine("Not enough arguments were given");
                label.Text = label.Text + "\n" + command;

            }
        }

        static void Move(int x, int y) // Move the turtle to the given X & Y coordinate
        {
            p.Location = new System.Drawing.Point(x, y); // Assign turtles location to new point
            p.BackColor = Color.Transparent;
        }

        static void Draw(int x, int y, Graphics g) // Draw a line between the current and new positions
        { 
            g.DrawLine(pen, p.Location, new System.Drawing.Point(x, y)); // Draw line
            Move(x, y); // execute move function
        }
    }
}