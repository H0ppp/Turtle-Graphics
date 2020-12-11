using System;
using System.Drawing;
using ASE_Assignment;

namespace ASE_Assignment
{
    /// <summary>
    /// Triangle: Used to instanciate triangle objects with valid parameters
    /// </summary>
    public class Triangle : Shape
    {
        PointF[] points; // Variable not included in shape class

        /// <summary>
        /// Triangle object constructor
        /// </summary>
        /// <param name="colour">Fill colour</param>
        /// <param name="penColour">Outline colour</param>
        /// <param name="x">X1 Location</param>
        /// <param name="y">Y1 Location</param>
        /// <param name="x2">Point 2 X</param>
        /// <param name="y2">Point 2 Y</param>
        /// <param name="x3">Point 3 X</param>
        /// <param name="y3">Point 3 Y</param>
        public Triangle(Color colour, Color penColour, int x, int y, int x2, int y2, int x3, int y3)
        {
            this.colour = colour;
            this.penColour = penColour;
            this.x = x;
            this.y = y;
            Point point1 = new Point(x, y); // Create point based on turtle location
            Point point2 = new Point(x2, y2); // Create points based on user input
            Point point3 = new Point(x3, y3);
            points = new PointF[] { point1, point2, point3 }; // Assign points to Point array
        }
        public override double calcArea()
        {
            return 0; // NEEDS IMPLEMENTING
        }

        public override double calcPerimeter()
        {
            return 0; // NEEDS IMPLEMENTING
        }

        /// <summary>
        /// Draw given triangle onto a panel
        /// </summary>
        /// <param name="g">Graphics panel to draw onto</param>
        public override void Draw(Graphics g)
        {
            Pen p = new Pen(penColour, 2); // Create new pen based on colour assigned by user
            SolidBrush b = new SolidBrush(colour); // create new brush based on colour assigned by user
            g.DrawPolygon(p, points); //draw fill colour
            g.FillPolygon(b, points); // draw outline
        }
    }
}