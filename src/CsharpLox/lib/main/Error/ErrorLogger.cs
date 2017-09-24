using System;

namespace main{
    public static class ErrorLogger
    {
        public static bool hadError {get; set;}
         public static void Error(int line, string message)
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