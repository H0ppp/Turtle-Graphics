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

        public static bool IsIf(string command) // Checks to see if command is an IF statement
        {
            commandArray = command.Split(" "); // Split the command entered into parts to determine args given.

            if (commandArray[0].Equals("if", StringComparison.InvariantCultureIgnoreCase) && (commandArray.Length == 4)) // Check to see if command is correct syntax: [0] IF, [1] X, [2] OPERATION, [3] Y
            {
                return true;
            }
            else { return false; }
        }
        public static bool IsVar(string command) // Checks to see if command is a VAR statement
        {
            commandArray = command.Split(" "); // Split the command entered into parts to determine args given.
            if (commandArray[0].Equals("var", StringComparison.InvariantCultureIgnoreCase) && (commandArray.Length == 4)) // Check the syntax, [0] VAR, [1] LABEL,[2] equals, [3] VALUE
            {
                Variable v = FindVar(commandArray[1]);
                if (v != null) // See if variable already exists to overwrite
                {
                    if (int.TryParse(commandArray[3], out int result))
                    {
                        v.Value = result; // overwrite value
                        Pointer.AddConsoleBox("Attempted to overwrite Variable: " + commandArray[1] + " with value: " + commandArray[3]); // Data logging
                        return true;
                    }
                    else
                    {
                        Pointer.AddConsoleBox("ERROR-18: Variable value must be int.");
                        Pointer.addInvalidBox(command);
                        return true;
                    }
                }
                else // Create new variable if it doesnt exist
                {
                    if (int.TryParse(commandArray[3], out int result))
                    {
                        variableList.Add(new Variable
                        {
                            Label = commandArray[1],
                            Value = result
                        }); // Add the new variable to the variable list
                        Pointer.AddConsoleBox("Attempted to add Variable: " + commandArray[1] + " with value: " + commandArray[3]); // Data logging
                        return true;
                    }
                    else
                    {
                        Pointer.AddConsoleBox("ERROR-18: Variable value must be int.");
                        Pointer.addInvalidBox(command);
                        return true;
                    }
                }
            }
            else if (commandArray[0].Equals("var", StringComparison.InvariantCultureIgnoreCase) && (commandArray.Length == 6)) // Check the syntax, [0] VAR, [1] LABEL, [2] equals, [3] VALUE, [4]OPERATOR, [5] NEW VALUE
            {
                ModifyVar(command);
                return true;
            }
            else if (commandArray[0].Equals("var", StringComparison.InvariantCultureIgnoreCase))
            {
                Pointer.AddConsoleBox("ERROR-19: Incorrect amount of arguments");
                Pointer.addInvalidBox(command);
                return true;
            }
            else { return false; }
        }

        public static void ModifyVar(string command)
        {
            commandArray = command.Split(" "); // Split the command entered into parts to determine args given.
            Variable modifiedVariable = FindVar(commandArray[1]);
            Variable v2 = FindVar(commandArray[3]); // 2nd potential variable
            Variable v3 = FindVar(commandArray[5]); // 3rd potential variable
            if (modifiedVariable != null) // Ensure primary variable exists and value in an integer
            {
                if (v2 != null) // check if argument 3 is a variable that has an integer value 
                {
                    if (v3 != null) // check if argument 5 is a variable with int value
                    {
                        ComplexModifyVar(v2.Value, v3.Value, modifiedVariable, commandArray[4]);
                    }
                    else
                    {
                        int.TryParse(commandArray[5], out int a5); // Check if argument is an int
                        ComplexModifyVar(v2.Value, a5, modifiedVariable, commandArray[4]);
                    }
                }
                else if (int.TryParse(commandArray[3], out int a3)) // argument 3 is not a variable so treat it as a number
                {
                    int.TryParse(commandArray[5], out int a5); // Check if argument is an int
                    ComplexModifyVar(a3, a5, modifiedVariable, commandArray[4]);
                }
                else
                {
                    Pointer.AddConsoleBox("ERROR-16: Neither an integer or variable was given in arguments");
                    Pointer.addInvalidBox(command);
                }
            }
            else
            {
                Pointer.AddConsoleBox("ERROR-17: Variable must already be defined with an int parameter to be modified in this way."); // Data logging
                Pointer.addInvalidBox(command);
            }
        }

        public static void ComplexModifyVar(int v1, int v2, Variable v, string op)
        {
            switch (op)
            {
                case "+":
                    v.Value = (v1 + v2);
                    Pointer.AddConsoleBox("Attempted to modify Variable: " + commandArray[1] + " with value: " + v.Value); // Data logging
                    break;
                case "-":
                    v.Value = (v1 - v2);
                    Pointer.AddConsoleBox("Attempted to modify Variable: " + commandArray[1] + " with value: " + v.Value); // Data logging
                    break;
                case "/":
                    v.Value = (v1 / v2);
                    Pointer.AddConsoleBox("Attempted to modify Variable: " + commandArray[1] + " with value: " + v.Value); // Data logging
                    break;
                case "*":
                    v.Value = (v1 * v2);
                    Pointer.AddConsoleBox("Attempted to modify Variable: " + commandArray[1] + " with value: " + v.Value); // Data logging
                    break;
                default:
                    Pointer.AddConsoleBox("ERROR-15: Operator expected to modify variable");
                    break;
            }
        }

        public static Variable FindVar(string s) // Find a variable with the given label to determine if it exists
        {
            foreach (Variable v in variableList)
            {
                if (v.Label.Equals(s, StringComparison.InvariantCultureIgnoreCase)) // Check if var already exists
                {
                    return v;
                }
            }
            return null;
        }

        public static bool IsLoop(string command) // Checks to see if command is a LOOP statement
        {
            commandArray = command.Split(" "); // Split the command entered into parts to determine args given.
            if (commandArray[0].Equals("loop", StringComparison.InvariantCultureIgnoreCase)) // check syntax [0] loop, [1] var/value, [2] operator, [3] var/value
            {
                if (commandArray.Length == 4)
                {
                    return true;
                }
                else
                {
                    Pointer.AddConsoleBox("ERROR-19: Incorrect amount of arguments");
                    Pointer.addInvalidBox(command);
                    return true;
                }
            }
            else { return false; }
        }

        public static bool IsEnd(string command)
        {
            commandArray = command.Split(" "); // Split the command entered into parts to determine args given.
            if (commandArray[0].Equals("end", StringComparison.InvariantCultureIgnoreCase)) // check syntax
            {
                return true;
            }
            else { return false; }
        }

        public static void ClearVar()
        {
            variableList.Clear();
        }

        public static bool checkVarInt(String argument, out Int32 result)
        {
            Variable v = FindVar(argument);
            if (int.TryParse(argument, out int intArg))
            {
                result = intArg;
                return true;
            }

            else if(v != null)
            {
                result = v.Value;
                return true;
            }
            else
            {
                result = -1;
                return false;
            }
        }
    }
}