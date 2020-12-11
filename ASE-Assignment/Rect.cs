using System;
using System.Drawing;
using ASE_Assignment;

namespace ASE_Assignment
{
    /// <summary>
    /// Rect: Used to instanciate rectangle objects with valid parameters
    /// </summary>
    public class Rect : Shape
    {
        int width, height; // Variables not included in shape


        /// <summary>
        /// Rectangle object constructor
        /// </summary>
        /// <param name="colour">Fill colour</param>
        /// <param name="penColour">Outline colour</param>
        /// <param name="x">X Location</param>
        /// <param name="y">Y Location</param>
        /// <param name="width">Width of rectangle</param>
        /// <param name="height">Height of rectangle</param>
        public Rect(Color colour, Color penColour, int x, int y, int width, int height)
        {
            this.width = width;
            this.height = height;
            this.colour = colour;
            this.penColour = penColour;
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// Calculates the area of the rectangle
        /// </summary>
        /// <returns>Area as double</returns>
        public override double calcArea()
        {
            return width * height; // Rectangle area is w*h
        }

        /// <summary>
        /// Calculates the Perimeter of the rectangle
        /// </summary>
        /// <returns>Perimeter as double</returns>
        public override double calcPerimeter()
        {
            return (2 * width) + (2 * height); // Rectangle perimeter is all sides added together
        }

        /// <summary>
        /// Draw given rectangle onto a panel
        /// </summary>
        /// <param name="g">Graphics panel to draw onto</param>
        public override void Draw(Graphics g)
        {
            Pen p = new Pen(penColour, 2); // Create new pen based on colour assigned by user
            SolidBrush b = new SolidBrush(colour); // create new brush based on colour assigned by user
            g.DrawRectangle(p, new Rectangle(x, y, width, height)); //draw fill colour
            g.FillRectangle(b, new Rectangle(x, y, width, height)); //draw outline
        }
    }
}