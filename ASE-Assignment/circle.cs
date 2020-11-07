using System;
using System.Drawing;

namespace ASE_Assignment {
    /*
     * Circle: Used to instanciate circle objects with valid parameters
     */
    class Circle : Shape {
        int radius; // The only extra variable required from the shape class

        public Circle(Color colour,Color penColour, int x, int y, int radius) { // Constructor
            this.radius = radius;
            this.colour = colour;
            this.x = x;
            this.y = y;
            this.penColour = penColour;
        }
        public override double calcArea() {
            return Math.PI * (radius ^ 2); // Circle area is pi*r^2
        }

        public override double calcPerimeter() {
            return 2 * Math.PI * radius; // Circle circumference is pi*diameter
        }

        public override void Draw(Graphics g) {
            Pen p = new Pen(penColour, 2); // Create new pen based on colour assigned by user
            SolidBrush b = new SolidBrush(colour); // create new brush based on colour assigned by user
            g.FillEllipse(b, x, y, radius * 2, radius * 2); //draw fill colour
            g.DrawEllipse(p, x, y, radius * 2, radius * 2); // draw outline
        }
    }
}
