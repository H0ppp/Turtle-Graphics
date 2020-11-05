using System;
using System.Drawing;
using ASE_Assignment;

namespace ASE_Assignment
{
    class Triangle : Shape
    {
        PointF[] points;

        public Triangle(Color colour, Color penColour, int x, int y, int x2, int y2, int x3, int y3)
        {
            this.colour = colour;
            this.penColour = penColour;
            this.x = x;
            this.y = y;
            Point point1 = new Point(x, y);
            Point point2 = new Point(x2, y2);
            Point point3 = new Point(x3, y3);
            points = new PointF[] { point1, point2, point3 };
        }
        public override double calcArea()
        {
            return 0;
        }

        public override double calcPerimeter()
        {
            return 0;
        }

        public override void Draw(Graphics g)
        {
            Pen p = new Pen(penColour, 2);
            SolidBrush b = new SolidBrush(colour);
            g.DrawPolygon(p, points);
            g.FillPolygon(b, points);
        }
    }
}