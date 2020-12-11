using System;

namespace ASE_Assignment
{
    /// <summary>
    /// 
    /// </summary>
	public class Looper
    {
        static string[] commandArray;
        /// <summary>
        /// Check the syntax of a loop command and derive the correct values from the arguments
        /// </summary>
        /// <param name="loopCommand">The given command containing the loop</param>
        public static void LoopChecker(string loopCommand)
        {
            commandArray = loopCommand.Split(" ");
            Variable v1 = Parser.FindVar(commandArray[1]);
            Variable v2 = Parser.FindVar(commandArray[3]);
            if (v1 != null)
            {
                if (v2 != null)
                {
                    LoopParse(v1, v2, commandArray[2], loopCommand);
                }
                else
                {
                    int.TryParse(commandArray[3], out int a2); // Check if argument is an int
                    LoopParse(v1, a2, commandArray[2], loopCommand);
                }
            }
            else
            {
                int.TryParse(commandArray[1], out int a1); // Check if argument is an int
                int.TryParse(commandArray[3], out int a2); // Check if argument is an int
                LoopParse(a1, a2, commandArray[2], loopCommand);
            }
        }
        /// <summary>
        /// Run the loop based on the type of operation it uses.
        /// </summary>
        /// <param name="v1">First value for while statement</param>
        /// <param name="v2">Second value for while statement</param>
        /// <param name="op">The operator used (=, <, >, !=)</param>
        /// <param name="command">The command containing the loop.</param>
        public static void LoopParse(int v1, int v2, string op, string command)
        {
            switch (op)
            {
                case "=":
                    while (v1 == v2)
                    {
                        LoopRun();
                    }
                    break;
                case ">":
                    while (v1 > v2)
                    {
                        LoopRun();
                    }
                    break;
                case "<":
                    while (v1 < v2)
                    {
                        LoopRun();
                    }
                    break;
                case "!=":
                    while (v1 != v2)
                    {
                        LoopRun();
                    }
                    break;
                default:
                    Pointer.AddConsoleBox("ERROR-15: Operator expected");
                    Pointer.addInvalidBox(command);
                    break;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <param name="op"></param>
        /// <param name="command"></param>
        public static void LoopParse(Variable v1, int v2, string op, string command)
        {
            Console.WriteLine(v1.Value);
            Console.WriteLine(v2);
            Console.WriteLine(op);
            switch (op)
            {
                case "=":
                    while (v1.Value == v2)
                    {
                        LoopRun();
                    }
                    break;
                case ">":
                    while (v1.Value > v2)
                    {
                        LoopRun();
                    }
                    break;
                case "<":
                    while (v1.Value < v2)
                    {
                        LoopRun();
                    }
                    break;
                case "!=":
                    while (v1.Value != v2)
                    {
                        LoopRun();
                    }
                    break;
                default:
                    Pointer.AddConsoleBox("ERROR-15: Operator expected");
                    Pointer.addInvalidBox(command);
                    break;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <param name="op"></param>
        /// <param name="command"></param>
        public static void LoopParse(Variable v1, Variable v2, string op, string command)
        {
            switch (op)
            {
                case "=":
                    while (v1.Value == v2.Value)
                    {
                        LoopRun();
                    }
                    break;
                case ">":
                    while (v1.Value > v2.Value)
                    {
                        LoopRun();
                    }
                    break;
                case "<":
                    while (v1.Value < v2.Value)
                    {
                        LoopRun();
                    }
                    break;
                case "!=":
                    while (v1.Value != v2.Value)
                    {
                        LoopRun();
                    }
                    break;
                default:
                    Pointer.AddConsoleBox("ERROR-15: Operator expected");
                    Pointer.addInvalidBox(command);
                    break;
            }
        }


        /// <summary>
        /// The function ran during the while loop.
        /// </summary>
        public static void LoopRun()
        {
            foreach (Command i in Window.loopCommands) // iterate through encapsulated commands
            {
                if (i.operation) // if command is an operational command
                {
                    Parser.IsVar(i.line); // check type
                    Parser.IsLoop(i.line);
                    Parser.IsEndIf(i.line);
                    Parser.IsEndLoop(i.line);
                    Parser.IsEndMethod(i.line);
                }
                else
                {
                    Pointer.Instruct(i.line); // Iterate through the drawing commands encapsulated.
                }
            }
        }
    }
}