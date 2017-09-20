using System;
using System.IO;

namespace main
{
    public interface ILox{
        void runPrompt();
        void runFile(string path);
        void Run(string source);
        void error(int line, string message);
    }
    public class Lox: ILox
    {
        private readonly IScanner _scanner;
        public Lox(IScanner scanner)
        {
            _scanner = scanner;
        }
        public bool hadError { get; set; }

        public void runPrompt()
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

        public void runFile(string path)
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

        public void Run(string source)
        {
           
            var tokens = _scanner.tokens();

            tokens.ForEach(x => Console.WriteLine(x.Value));
        }

        public void error(int line, string message)
        {
            report(line, "", message);
        }

        private void report(int line, string where, string message)
        {
            Console.WriteLine($"Error at [line: {line}]   {where} :   {message}");
            hadError = true;
        }
    }
}
