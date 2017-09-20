using System;
using System.IO;

namespace main
{
    public class Program
    {
        static void Main(string[] args)
        {
            Scanner scanner = new Scanner();
            Lox lox = new Lox(scanner);

            if (args.Length > 1)
            {
                Console.WriteLine("Usage: CsharpLox [script]");
            }
            else if (args.Length == 1)
            {
                lox.runFile(args[0]);
            }
            else
            {
                lox.runPrompt();
            }
        }
    }
}
