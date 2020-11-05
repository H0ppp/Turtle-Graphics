using System;
using System.Drawing;

namespace ASE_Assignment {
    class Circle : Shape {
        int radius;

        public Circle(Color colour,Color penColour, int x, int y, int radius) {
            this.radius = radius;
            this.colour = colour;
            this.x = x;
            this.y = y;
            this.penColour = penColour;
        }
        public override double calcArea() {
            return Math.PI * (radius ^ 2);
        }

        public override double calcPerimeter() {
            return 2 * Math.PI * radius;
        }

        public override void Draw(Graphics g) {
            Pen p = new Pen(penColour, 2);
            SolidBrush b = new SolidBrush(colour);
            g.FillEllipse(b, x, y, radius * 2, radius * 2);
            g.DrawEllipse(p, x, y, radius * 2, radius * 2);
        }
    }
}
