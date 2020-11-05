using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace ASE_Assignment
{
    public class Pointer
    {
        static PictureBox p;
        static Pen pen;
        static Color penColour = Color.Black;
        static Color shapeColour = Color.Red;
        static bool fill = true;
        public static void Init(PictureBox picture)
        {
            p = picture; // Assign the drawing area.
            pen = new Pen(penColour); // Set default pen colour
        }

        public static void Instruct(String command, Graphics g)  // Parse the commands given
        {
            String[] commandArray;
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

                        if (fill)
                        {
                            Triangle t = new Triangle(shapeColour, penColour, p.Location.X, p.Location.Y, x2, y2, x3, y3);
                            t.Draw(g);
                        }
                        else
                        {
                            Triangle t = new Triangle(Color.Transparent, penColour, p.Location.X, p.Location.Y, x2, y2, x3, y3);
                            t.Draw(g);
                        }
                    }
                    else
                    {
                        Console.WriteLine("One or more arguments given were not valid numbers");
                    }
                }
                else if (commandArray[0].Equals("rectangle", StringComparison.InvariantCultureIgnoreCase))  // RECTANGLE DRAWING
                { // Verify if actual command and ignore case
                    if (int.TryParse(commandArray[1], out int width))
                    { //See if arg given is valid integer
                        if (int.TryParse(commandArray[2], out int height))
                        { //See if arg given is valid integer
                            if (fill)
                            {
                                Rect r = new Rect(shapeColour, penColour, p.Location.X, p.Location.Y, width, height);
                                r.Draw(g);
                            }
                            else
                            {
                                Rect r = new Rect(Color.Transparent, penColour, p.Location.X, p.Location.Y, width, height);
                                r.Draw(g);
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid argument, expected a number for radius");
                    }
                }
                else if (commandArray[0].Equals("fill", StringComparison.InvariantCultureIgnoreCase))  // Fill Boolean
                { // Verify if actual command and ignore case
                    if (commandArray[1].Equals("on", StringComparison.InvariantCultureIgnoreCase))
                    {
                        fill = true;
                    }
                    else if (commandArray[1].Equals("off", StringComparison.InvariantCultureIgnoreCase))
                    {
                        fill = false;
                    }
                }

                else if (commandArray[0].Equals("circle", StringComparison.InvariantCultureIgnoreCase))  // CIRCLE DRAWING
                { // Verify if actual command and ignore case
                    if (int.TryParse(commandArray[1], out int radius))
                    { //See if arg given for radius is valid integer
                        if (fill)
                        {
                            Circle c = new Circle(shapeColour, penColour, p.Location.X, p.Location.Y, radius);
                            c.Draw(g);
                        }
                        else
                        {
                            Circle c = new Circle(Color.Transparent, penColour, p.Location.X, p.Location.Y, radius);
                            c.Draw(g);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid argument, expected a number for radius");
                    }
                }

                else if (commandArray[0].Equals("clear", StringComparison.InvariantCultureIgnoreCase))
                {
                    g.Clear(Color.White); // clear the screen
                }
                else if (commandArray[0].Equals("moveto", StringComparison.InvariantCultureIgnoreCase))
                {
                    if (int.TryParse(commandArray[1], out int newx))
                    { //See if arg given for radius is valid integer
                        if (int.TryParse(commandArray[2], out int newy))
                        { //See if arg given for radius is valid integer
                            Move(newx, newy);
                        }
                    }
                }
                else if (commandArray[0].Equals("drawto", StringComparison.InvariantCultureIgnoreCase))
                {
                    if (int.TryParse(commandArray[1], out int newx))
                    { //See if arg given for radius is valid integer
                        if (int.TryParse(commandArray[2], out int newy))
                        { //See if arg given for radius is valid integer
                            Draw(newx, newy, g);
                        }
                    }
                }
                else if (commandArray[0].Equals("reset", StringComparison.InvariantCultureIgnoreCase))
                {
                    Move(12, 12); // move the pointer back to original pos
                }
                else
                {
                    Console.WriteLine("Invalid command!"); // inform user of wrong commmand
                }

            } catch (IndexOutOfRangeException e)
            {
                Console.WriteLine("Not enough arguments were given");
            }
        }
        static void Move(int x, int y) // Move the turtle to the given X & Y coordinate
        {
            p.Location = new System.Drawing.Point(x, y);
            p.BackColor = Color.Transparent;
        }

        static void Draw(int x, int y, Graphics g) // Draw a line between the current and new positions
        {
            g.DrawLine(pen, p.Location, new System.Drawing.Point(x, y));
            Move(x, y);
        }
    }
}