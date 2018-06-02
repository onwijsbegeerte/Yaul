using System;
using System.IO;

namespace main
{
    public interface ILox
    {
        void runPrompt();
        void runFile(string path);
        void Run(string source);
    }
    public class Lox : ILox
    {
        private readonly IScanner _scanner;
        public Lox(IScanner scanner)
        {
            _scanner = scanner;
        }

        public void runPrompt()
        {
            var input = string.Empty;

            while (!input.ToUpper().Equals("EXIT"))
            {
                Console.WriteLine(">> ");
                input = Console.ReadLine();
                Run(input);
                ErrorLogger.hadError = false;
            }
        }

        public void runFile(string path)
        {
            try
            {
                var bytes = File.ReadAllBytes(path);
                Run(System.Text.Encoding.UTF8.GetString(bytes));
                if (ErrorLogger.hadError) Environment.Exit(65);
            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.WriteLine($"Failed retrieving file,{ex.Message}");
            }
        }
        
        public static void error(Token token, String message) {
            if (token.TokenType == TokenType.EOF) {
                report(token.Line, " at end", message);
            } else {
                report(token.Line, " at '" + token.Lexeme + "'", message);
            }
        }

        private static void report(int line, string bla2, string bla3)
        {
            
        }

        public void Run(string source)
        {
            var tokens = _scanner.tokens();

            tokens.ForEach(x => Console.WriteLine(x.Value));
        }
    }
}
