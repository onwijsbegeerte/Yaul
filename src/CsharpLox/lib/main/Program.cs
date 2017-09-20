using System;
using System.IO;

namespace main
{
    public class Lox
    {
        public static bool hadError { get; private set; }

        static void Main(string[] args)
        {
            if (args.Length > 1)
            {
                Console.WriteLine("Usage: CsharpLox [script]");
            }
            else if (args.Length == 1)
            {
                runFile(args[0]);
            }
            else
            {
                runPrompt();
            }
        }

        private static void runPrompt()
        {
            var input = string.Empty;

            while (!input.ToUpper().Equals("EXIT"))
            {
                Console.WriteLine(">> ");
                input = Console.ReadLine();
                Run(input);
                hadError = false;
            }
        }

        private static void runFile(string path)
        {
            try
            {
                var bytes = File.ReadAllBytes(path);
                Run(System.Text.Encoding.UTF8.GetString(bytes));
                if (hadError) Environment.Exit(65);
            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.WriteLine($"Failed retrieving file,{ex.Message}");
            }
        }

        private static void Run(string source)
        {
            Scanner scanner = new Scanner(source);
            var tokens = scanner.tokens();

            tokens.ForEach(x => Console.WriteLine(x.Value));
        }

        static void error(int line, string message)
        {
            report(line, "", message);
        }

        private static void report(int line, string where, string message)
        {
            Console.WriteLine($"Error at [line: {line}]   {where} :   {message}");
            hadError = true;
        }
    }
}
