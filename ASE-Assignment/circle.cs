using System;
using System.Drawing;

namespace ASE_Assignment {
    /// <summary>
    /// Circle: Used to instanciate circle objects with valid parameters
    /// </summary>
    public class Circle : Shape {
        int radius; // The only extra variable required from the shape class

        /// <summary>
        /// Circle object constructor
        /// </summary>
        /// <param name="colour">Fill colour</param>
        /// <param name="penColour">Outline colour</param>
        /// <param name="x">X Location</param>
        /// <param name="y">Y Location</param>
        /// <param name="radius">radius integer</param>
        public Circle(Color colour,Color penColour, int x, int y, int radius) {
            this.radius = radius;
            this.colour = colour;
            this.x = x;
            this.y = y;
            this.penColour = penColour;
        }

        /// <summary>
        /// Calculates the area of the circle
        /// </summary>
        /// <returns>Area as double</returns>
        public override double calcArea() {
            return Math.PI * (radius ^ 2); // Circle area is pi*r^2
        }
        /// <summary>
        /// Calculates the Perimeter of the circle
        /// </summary>
        /// <returns>Perimeter as double</returns>
        public override double calcPerimeter() {
            return 2 * Math.PI * radius; // Circle circumference is pi*diameter
        }


        /// <summary>
        /// Draw given circle onto a panel
        /// </summary>
        /// <param name="g">Graphics panel to draw onto</param>
        public override void Draw(Graphics g) {
            Pen p = new Pen(penColour, 2); // Create new pen based on colour assigned by user
            SolidBrush b = new SolidBrush(colour); // create new brush based on colour assigned by user
            g.FillEllipse(b, x, y, radius * 2, radius * 2); //draw fill colour
            g.DrawEllipse(p, x, y, radius * 2, radius * 2); // draw outline
        }
    }
}
