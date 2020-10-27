using System;
using System.Drawing;

namespace ASE_Assignment
{
	abstract class Shape
	{
		protected Color colour;
		protected int x, y;
		public Shape()
		{
			colour = Color.Red;
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

		//set is declared as virtual so it can be overridden by a more specific child version
		//but is here so it can be called by that child version to do the generic stuff
		//note the use of the param keyword to provide a variable parameter list to cope with some shapes having more setup information than others
		public virtual void set(Color colour, params int[] list)
		{
			this.colour = colour;
			this.x = list[0];
			this.y = list[1];
		}
	}

}
