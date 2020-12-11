using System;
using System.Drawing;

namespace ASE_Assignment
{
	/// <summary>
	/// Shape: Basic shape class that is used by other classes for inheritance
	/// </summary>
	public abstract class Shape
	{
		//Vairables used in all shapes
		protected Color colour, penColour;
		protected int x, y;

		/// <summary>
		/// Generic shape constructor
		/// </summary>
		public Shape()
		{
			colour = Color.Red;
			penColour = Color.Black;
			x = 100;
			y = 100;
		}

		public Shape(Color colour, int x,int y) //Constructor
        {
			this.colour = colour; // Assign the colour
			this.x = x; // Assign the x position
			this.y = y; // Assign the y position
        }
		public abstract void Draw(Graphics g); // Draw function
		public abstract double calcArea(); // Area function
		public abstract double calcPerimeter(); // Perim function

	}

}
