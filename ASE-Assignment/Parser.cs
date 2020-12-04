using System;
using System.Collections.Generic;

namespace ASE_Assignment
{
	public class Parser
	{
		static string[] commandArray;
        public static List<Variable> variableList = new List<Variable>();
		public static bool isIf(string command)
        {
			commandArray = command.Split(" "); // Split the command entered into parts to determine args given.

			// [0] IF, [1] X, [2] OPERATION, [3] Y
			if (commandArray[0].Equals("if", StringComparison.InvariantCultureIgnoreCase) && (commandArray.Length == 4))
			{ 
				return true;
			}
			else { return false; }
        }
		public static bool isVar(string command)
		{
			commandArray = command.Split(" "); // Split the command entered into parts to determine args given.
			if (commandArray[0].Equals("var", StringComparison.InvariantCultureIgnoreCase ) && (commandArray.Length == 3))
			{
				variableList.Add(new Variable
				{
					label = commandArray[1],
					value = commandArray[2]
				});
				Console.WriteLine("Variable: " + commandArray[1]+" added with value: "+commandArray[2]);
				return true;
			}
			else { return false; }
		}
		public static bool isLoop(string command)
		{
			commandArray = command.Split(" "); // Split the command entered into parts to determine args given.
			if (commandArray[0].Equals("loop", StringComparison.InvariantCultureIgnoreCase))
			{

				return true;
			}
			else { return false; }
		}
		public static bool isEnd(string command)
		{
			commandArray = command.Split(" "); // Split the command entered into parts to determine args given.
			if (commandArray[0].Equals("end", StringComparison.InvariantCultureIgnoreCase))
			{

				return true;
			}
			else { return false; }
		}
	}
}