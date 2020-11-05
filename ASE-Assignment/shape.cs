using System;
using System.Drawing;

namespace ASE_Assignment
{
	abstract class Shape
	{
		protected Color colour, penColour;
		protected int x, y;
		public Shape()
		{
			colour = Color.Red;
			penColour = Color.Black;
			x = 100;
			y = 100;
		}

		public Shape(Color colour, int x,int y)
        {
			this.colour = colour; // Assign the colour
			this.x = x; // Assign the x position
			this.y = y; // Assign the y position
        }
		public abstract void Draw(Graphics g);
		public abstract double calcArea();
		public abstract double calcPerimeter();

		public virtual void set(Color colour, params int[] list)
		{
			this.colour = colour;
			this.x = list[0];
			this.y = list[1];
		}
	}

}
