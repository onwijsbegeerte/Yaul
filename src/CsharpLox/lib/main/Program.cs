using System;
using System.IO;

namespace main
{
    public class Program
    {
        static void Main(string[] args)
        {
            Expr expression = new Expr.Binary(
                new Expr.Unary(
                    new Token(TokenType.MINUS, "-", null, 1),
                    new Expr.Literal(123)),
                new Token(TokenType.STAR, "*", null, 1),
                new Expr.Grouping(
                    new Expr.Literal(45.67)));
            
            Console.WriteLine(new AstPrinter().print(expression));
            
            Scanner scanner = new Scanner("asd");
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
