using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace ASE_Assignment
{
    class Pointer
    {
        static PictureBox p;
        public static void Init(PictureBox picture)
        {
            p = picture;
        }

        public static void Instruct(String command, Graphics g)
        {
            String[] commandArray;
            Color shapeColour;
            Console.WriteLine(command); // Print to console for logging
            commandArray = command.Split(" "); // Split the command entered into parts to determine args given.

            if (commandArray[0].Equals("circle", StringComparison.InvariantCultureIgnoreCase))  // CIRCLE DRAWING
            { // Verify if actual command and ignore case
                if (int.TryParse(commandArray[1], out int radius))
                { //See if arg given for radius is valid integer
                    shapeColour = Color.FromName(commandArray[2]); // See if colour can be derived from text given - if not, will be transparent
                    Circle c = new Circle(shapeColour, p.Location.X, p.Location.Y, radius);
                    c.Draw(g);
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
            else if (commandArray[0].Equals("move", StringComparison.InvariantCultureIgnoreCase))
            {
                if (int.TryParse(commandArray[1], out int newx))
                { //See if arg given for radius is valid integer
                    if (int.TryParse(commandArray[2], out int newy))
                    { //See if arg given for radius is valid integer
                        Move(newx, newy);
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
        }
        static void Move(int x, int y)
        {
            p.Location = new System.Drawing.Point(x, y);
        }
    }
}