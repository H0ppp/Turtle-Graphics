using System;
namespace ASE_Assignment
{
    /// <summary>
    /// All exceptions are based off of this to allow catching of internal exceptions but not software breaking ones.
    /// </summary>
    class InternalException : Exception
    {

    }
    /// <summary>
    /// Exception thrown if triangle command does not receive integers or variables
    /// </summary>
    class TrianglePosException : InternalException
    {
        public TrianglePosException(string command)
        {
            Pointer.AddConsoleBox("ERROR-01: One or more triangle co-ordinates given were not valid numbers"); // Inform user that there command is incorrect
            Pointer.AddInvalidBox(command); // Add command to invalid list on screen
        }
    }
    /// <summary>
    /// Exception thrown if rectangle command does not receive integers or variables
    /// </summary>
    class RectangleArgsException : InternalException
    {
        public RectangleArgsException(string command)
        {
            Pointer.AddConsoleBox("ERROR-02: Invalid argument, expected a number or variable for rectangle dimension"); // Inform user that there command is incorrect
            Pointer.AddInvalidBox(command); // Add command to invalid list on screen
        }
    }
    /// <summary>
    /// Exception thrown if fill command does not receive on/off
    /// </summary>
    class FillArgsException : InternalException
    {
        public FillArgsException(string command)
        {
            Pointer.AddConsoleBox("ERROR-03: Invalid argument, expected on/off for fill."); // Inform user that there command is incorrect
            Pointer.AddInvalidBox(command); // Add command to invalid list on screen
        }
    }
    /// <summary>
    /// Exception thrown if circle command does not receive integers or variables
    /// </summary>
    class CircleArgsException : InternalException
    {
        public CircleArgsException(string command)
        {
            Pointer.AddConsoleBox("ERROR-04: Invalid argument, expected a number or variable for circle radius"); // Inform user that there command is incorrect
            Pointer.AddInvalidBox(command); // Add command to invalid list on screen
        }
    }
    /// <summary>
    /// Exception thrown if move command does not receive integers or variables
    /// </summary>
    class MoveArgsException : InternalException
    {
        public MoveArgsException(string command)
        {
            Pointer.AddConsoleBox("ERROR-05: Invalid argument, expected a number for move to x/y co-ord"); // Inform user that there command is incorrect
            Pointer.AddInvalidBox(command); // Add command to invalid list on screen
        }
    }
    /// <summary>
    /// Exception thrown if draw command does not receive integers or variables
    /// </summary>
    class DrawArgsException : InternalException
    {
        public DrawArgsException(string command)
        {
            Pointer.AddConsoleBox("ERROR-06: Invalid argument, expected a number for draw to x/y co-ord"); // Inform user that there command is incorrect
            Pointer.AddInvalidBox(command); // Add command to invalid list on screen
        }
    }
    /// <summary>
    /// Exception thrown when an unknown command is entered.
    /// </summary>
    class InvalidCommandException : InternalException
    {
        public InvalidCommandException(string command)
        {
            Pointer.AddConsoleBox("ERROR-07: Invalid command"); // Inform user that there command is incorrect
            Pointer.AddInvalidBox(command); // Add command to invalid list on screen
        }
    }

    /*
     * Error 08 Is an IndexOutOfRangeException so does not need implementing
     */

    /// <summary>
    /// Exception thrown when a variable does not exist
    /// </summary>
    class NoSuchVariableException : InternalException
    {
        public NoSuchVariableException(string command)
        {
            Pointer.AddConsoleBox("ERROR-09: No such variable could be found"); // Inform user that there command is incorrect
            Pointer.AddInvalidBox(command); // Add command to invalid list on screen
        }
    }
    /// <summary>
    /// Exception thrown when if syntax is incorrect
    /// </summary>
    class IfSyntaxException : InternalException
    {
        public IfSyntaxException(string command)
        {
            Pointer.AddConsoleBox("ERROR-10: If syntax invalid"); // Inform user that there command is incorrect
            Pointer.AddInvalidBox(command); // Add command to invalid list on screen
        }
    }
    /// <summary>
    /// Exception thrown when variable value is set to data type other than integer
    /// </summary>
    class VariableValueException : InternalException
    {
        public VariableValueException(string command)
        {
            Pointer.AddConsoleBox("ERROR-11: Variable value must be integer"); // Inform user that there command is incorrect
            Pointer.AddInvalidBox(command); // Add command to invalid list on screen
        }
    }
    /// <summary>
    /// Exception thrown when an operation command does not contain enough arguments
    /// </summary>
    class OperationArgsException : InternalException
    {
        public OperationArgsException(string command)
        {
            Pointer.AddConsoleBox("ERROR-12: Incorrect amount of arguments"); // Inform user that there command is incorrect
            Pointer.AddInvalidBox(command); // Add command to invalid list on screen
        }
    }
    /// <summary>
    /// Exception thrown when a method that doesn't exist is accessed
    /// </summary>
    class MethodNotFoundException : InternalException
    {
        public MethodNotFoundException(string command)
        {
            Pointer.AddConsoleBox("ERROR-13: No method could be found"); // Inform user that there command is incorrect
            Pointer.AddInvalidBox(command); // Add command to invalid list on screen
        }
    }
    /// <summary>
    /// Exception thrown when an invalid operator is entered
    /// </summary>
    class InvalidOperatorException : InternalException
    {
        public InvalidOperatorException()
        {
            Pointer.AddConsoleBox("ERROR-14: Operator expected to modify variable"); // Inform user that there command is incorrect
        }
    }
}