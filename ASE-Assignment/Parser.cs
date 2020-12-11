using System;
using System.Collections.Generic;

namespace ASE_Assignment
{
    /// <summary>
    /// Parser: Class parses all operation commands such as IF, VAR, LOOP, METHOD, END.
    /// </summary>
    public class Parser
    {
        static string[] commandArray; // Array of command pieces
        public static List<Variable> variableList = new List<Variable>(); // List of all variables assigned in current runtime
        public static List<Method> methodList = new List<Method>(); // List of all methods created in current runtime

        /// <summary>
        /// Check if command is a method and method already exists
        /// </summary>
        /// <param name="command">The command entered</param>
        /// <param name="methodName">Output the existing method or creates new one</param>
        /// <returns>Syntax validity of method command</returns>
        public static bool IsMethod(string command, out string methodName)
        {
            commandArray = command.Split(" ");

            if (commandArray[0].Equals("method", StringComparison.InvariantCultureIgnoreCase) && (commandArray.Length == 2))
            {
                Method m = FindMethod(commandArray[1]); // Find method
                if(m == null) // If method not found
                {
                    methodList.Add(new Method
                    {
                        Label = commandArray[1],
                    }); // Add the new method to the method list
                    Pointer.AddConsoleBox("Created new method: " + commandArray[1]); // Data logging
                }
                methodName = commandArray[1]; // Return with method name
                return true;
            }
            else // Invalid syntax
            {
                methodName = null;
                return false;
            }
        }
        /// <summary>
        /// Check if command is a valid if statement.
        /// </summary>
        /// <param name="command">The command entered</param>
        /// <returns>Syntax validity of if command</returns>
        public static bool IsIf(string command) // Checks to see if command is an IF statement
        {
            commandArray = command.Split(" "); // Split the command entered into parts to determine args given.

            if (commandArray[0].Equals("if", StringComparison.InvariantCultureIgnoreCase) && (commandArray.Length == 4)) // Check to see if command is correct syntax: [0] IF, [1] X, [2] OPERATION, [3] Y
            {
                return true;
            }
            else { return false; }
        }

        /// <summary>
        /// Checks syntax and creates or modifys variable
        /// </summary>
        /// <param name="command">The command entered</param>
        /// <returns>Syntax validity of var command</returns>
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
                        Pointer.AddInvalidBox(command);
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
                        Pointer.AddInvalidBox(command);
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
                Pointer.AddInvalidBox(command);
                return true;
            }
            else { return false; }
        }

        /// <summary>
        /// See if command entered is a run function and process it accordingly
        /// </summary>
        /// <param name="command">The command entered</param>
        /// <returns>Syntax validity of runMethod command</returns>
        public static bool IsRunMethod(string command)
        {
            commandArray = command.Split(" "); // Split the command entered into parts to determine args given.
            if (commandArray[0].Equals("runMethod", StringComparison.InvariantCultureIgnoreCase))
            {
                Method m = FindMethod(commandArray[1]);
                if(m != null)
                {
                    m.methodRun();
                }
                else
                {
                    Pointer.AddConsoleBox("ERROR-19: Method not found");
                    Pointer.AddInvalidBox(command);
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Syntax parsing method for modifcation of variable values
        /// </summary>
        /// <param name="command">The command entered</param>
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
                    Pointer.AddInvalidBox(command);
                }
            }
            else
            {
                Pointer.AddConsoleBox("ERROR-17: Variable must already be defined with an int parameter to be modified in this way."); // Data logging
                Pointer.AddInvalidBox(command);
            }
        }
        /// <summary>
        /// Method for the modification of variables values with data passed from ModifyVar function
        /// </summary>
        /// <param name="v1">Integer 1</param>
        /// <param name="v2">Integer 2</param>
        /// <param name="v">The variable being modified</param>
        /// <param name="op">Numerical operator to use in equation</param>
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
        /// <summary>
        /// Method for finding existing variable from variableList
        /// </summary>
        /// <param name="s">Variable name</param>
        /// <returns>Variable found</returns>
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

        /// <summary>
        /// Function for finding existing method from variableList
        /// </summary>
        /// <param name="s">Method name</param>
        /// <returns>Method found</returns>
        public static Method FindMethod(string s) // Find a variable with the given label to determine if it exists
        {
            foreach (Method m in methodList)
            {
                if (m.Label.Equals(s, StringComparison.InvariantCultureIgnoreCase)) // Check if var already exists
                {
                    return m;
                }
            }
            return null;
        }
        /// <summary>
        /// Checks loop command syntax
        /// </summary>
        /// <param name="command">The command entered</param>
        /// <returns>Syntax validity of loop command</returns>
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
                    Pointer.AddInvalidBox(command);
                    return true;
                }
            }
            else { return false; }
        }
        /// <summary>
        /// Checks for end command
        /// </summary>
        /// <param name="command">Command entered</param>
        /// <returns>Syntax validity</returns>
        public static bool IsEndIf(string command)
        {
            commandArray = command.Split(" "); // Split the command entered into parts to determine args given.
            if (commandArray[0].Equals("endif", StringComparison.InvariantCultureIgnoreCase)) // check syntax
            {
                return true;
            }
            else { return false; }
        }
        /// <summary>
        /// Checks for end command
        /// </summary>
        /// <param name="command">Command entered</param>
        /// <returns>Syntax validity</returns>
        public static bool IsEndLoop(string command)
        {
            commandArray = command.Split(" "); // Split the command entered into parts to determine args given.
            if (commandArray[0].Equals("endloop", StringComparison.InvariantCultureIgnoreCase)) // check syntax
            {
                return true;
            }
            else { return false; }
        }
        /// <summary>
        /// Checks for end command
        /// </summary>
        /// <param name="command">Command entered</param>
        /// <returns>Syntax validity</returns>
        public static bool IsEndMethod(string command)
        {
            commandArray = command.Split(" "); // Split the command entered into parts to determine args given.
            if (commandArray[0].Equals("endmethod", StringComparison.InvariantCultureIgnoreCase)) // check syntax
            {
                return true;
            }
            else { return false; }
        }

        /// <summary>
        /// Clears the variable list
        /// </summary>
        public static void ClearVar()
        {
            variableList.Clear();
        }
        /// <summary>
        /// Clears the method list
        /// </summary>
        public static void ClearMethods()
        {
            methodList.Clear();
        }
        /// <summary>
        /// Function derived from Int.tryParse. Checks if string is either integer or an existing variable
        /// </summary>
        /// <param name="argument">The string to check</param>
        /// <param name="result">Output the integer or variable value</param>
        /// <returns>Possibility of finding integer</returns>
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