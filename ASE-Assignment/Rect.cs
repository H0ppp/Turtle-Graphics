using System;
using System.Drawing;
using ASE_Assignment;

namespace ASE_Assignment
{
    class Rect : Shape
    {
        int  width, height;

        public Rect(Color colour,Color penColour, int x, int y, int width, int height)
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
            return width * height;
        }

        public override double calcPerimeter()
        {
            return (2 * width) + (2 * height);
        }

        public override void Draw(Graphics g)
        {
            Pen p = new Pen(penColour, 2);
            SolidBrush b = new SolidBrush(colour);
            g.DrawRectangle(p, new Rectangle(x,y,width,height));
            g.FillRectangle(b, new Rectangle(x, y, width, height));
        }
    }
}