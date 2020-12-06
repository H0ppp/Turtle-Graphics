using System;
using System.Collections.Generic;

namespace ASE_Assignment
{
    /*
	 * Parser: Class parses all operation commands such as IF, VAR, LOOP, END.
	*/
    public class Parser
    {
        static string[] commandArray; // Array of command pieces
        public static List<Variable> variableList = new List<Variable>(); // List of all variables assigned in current runtime

        public static bool isIf(string command) // Checks to see if command is an IF statement
        {
            commandArray = command.Split(" "); // Split the command entered into parts to determine args given.

            if (commandArray[0].Equals("if", StringComparison.InvariantCultureIgnoreCase) && (commandArray.Length == 4)) // Check to see if command is correct syntax: [0] IF, [1] X, [2] OPERATION, [3] Y
            {
                return true;
            }
            else { return false; }
        }
        public static bool isVar(string command) // Checks to see if command is a VAR statement
        {
            commandArray = command.Split(" "); // Split the command entered into parts to determine args given.
            if (commandArray[0].Equals("var", StringComparison.InvariantCultureIgnoreCase) && (commandArray.Length == 4)) // Check the syntax, [0] VAR, [1] LABEL,[2] equals, [3] VALUE
            {
                Variable v = findVar(commandArray[1]);
                if (v != null)
                {
                    v.value = commandArray[3]; // overwrite value
                    Console.WriteLine("Attempted to overwrite Variable: " + commandArray[1] + " with value: " + commandArray[3]); // Data logging
                    return true;
                }
                else { 
                variableList.Add(new Variable
                {
                    label = commandArray[1],
                    value = commandArray[3]
                }); // Add the new variable to the variable list
                Console.WriteLine("Attempted to add Variable: " + commandArray[1] + " with value: " + commandArray[3]); // Data logging
                return true;
            }
            }
            else if (commandArray[0].Equals("var", StringComparison.InvariantCultureIgnoreCase) && (commandArray.Length == 5)) // Check the syntax, [0] VAR, [1] LABEL, [2] VALUE, [3] OPERATOR, [4] NEW VALUE
            {
                // NEEDS IMPLEMENTING
                return true;
            }
            else { return false; }
        }

        public static Variable findVar(string label) // Find a variable with the given label to determine if it exists
        {
            foreach (Variable v in variableList)
            {
                if (v.label.Equals(label, StringComparison.InvariantCultureIgnoreCase)) // Check if var already exists
                {
                    return v;
                }
                else
                {
                    return null;
                }
            }
            return null;
        }

        public static bool isLoop(string command) // Checks to see if command is a LOOP statement
        {
            commandArray = command.Split(" "); // Split the command entered into parts to determine args given.
            if (commandArray[0].Equals("loop", StringComparison.InvariantCultureIgnoreCase)) // check syntax
            {

                return true;
            }
            else { return false; }
        }
        public static bool isEnd(string command)
        {
            commandArray = command.Split(" "); // Split the command entered into parts to determine args given.
            if (commandArray[0].Equals("end", StringComparison.InvariantCultureIgnoreCase)) // check syntax
            {

                return true;
            }
            else { return false; }
        }
    }
}