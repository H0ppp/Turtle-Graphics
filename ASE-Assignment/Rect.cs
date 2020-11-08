using System;
using System.Drawing;
using ASE_Assignment;

namespace ASE_Assignment
{    /*
     * Rect: Used to instanciate rectangle objects with valid parameters
     */
    public class Rect : Shape
    {
        int  width, height; // Variables not included in shape

        public Rect(Color colour,Color penColour, int x, int y, int width, int height) // Constructor
        {
            this.width = width;
            this.height = height;
            this.colour = colour;
            this.penColour = penColour;
            this.x = x;
            this.y = y;
        }
        public override double calcArea()
        {
            return width * height; // Rectangle area is w*h
        }

        public override double calcPerimeter()
        {
            return (2 * width) + (2 * height); // Rectangle perimeter is all sides added together
        }

        public override void Draw(Graphics g)
        {
            Pen p = new Pen(penColour, 2); // Create new pen based on colour assigned by user
            SolidBrush b = new SolidBrush(colour); // create new brush based on colour assigned by user
            g.DrawRectangle(p, new Rectangle(x,y,width,height)); //draw fill colour
            g.FillRectangle(b, new Rectangle(x, y, width, height)); //draw outline
        }
    }
}