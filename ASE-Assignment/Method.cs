using System.Collections.Generic;

namespace ASE_Assignment
{
    /// <summary>
    /// Method object, contains method name and list of commands
    /// </summary>
    public class Method
    {
        public string Label { get; set; }
        public List<Command> Commands { get; set; }

        public  void methodRun()
        {
            foreach (Command i in Commands) // iterate through encapsulated commands
            {
                if (i.operation) // if command is an operational command
                {
                    Parser.IsVar(i.line); // check type
                    Parser.IsIf(i.line);
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