using System;

namespace MathParserDemo
{
    class Program
    {
        /// <summary>
        /// Main method
        /// </summary>
        /// <param name="args">Start up arguments</param>
        static void Main(string[] args)
        {
            Console.WriteLine("This program parses simple mathematical expressions.");
            Console.WriteLine("Program supports the following commands:");
            Console.WriteLine("Define a variable: let x = 3");
            Console.WriteLine("Calculate an expression: calc 2 + 4 - x");
            Console.WriteLine("Set a variable: set x = 6");
            Console.WriteLine("Print value of a variable: print x");
            Console.WriteLine("Exit program: exit");
            Console.WriteLine("--- Press any key to start ---");
            Console.ReadKey();
            Console.Clear();
            Run();
        }

        /// <summary>
        /// Run the main program, take input and handle it.
        /// </summary>
        static void Run()
        {
            bool isRunning = true;

            while (isRunning)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(">");
                Console.ForegroundColor = ConsoleColor.White;
                string input = Console.ReadLine();

                if (input.Length > 0)
                {
                    isRunning = Engine.Parser.Parse(input);
                }
            }
        }
    }
}
