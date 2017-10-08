using System;
using System.Collections.Generic;
using System.IO;

namespace CreateExpsessionClasses
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting Class Creation!");

            if (args.Length != 1)
            {
                Console.WriteLine("Please input valid directory");
                Environment.Exit(0);
            }

            string outputDir = args[0];

            defineAst(outputDir, "Expr", new List<string> { "Binary   : Expr left, Token operator, Expr right", "Grouping : Expr expression", "Literal  : Object value", "Unary    : Token operator, Expr right" });

        }

        private static void defineAst(string outputDir, string baseName, List<string> types)
        {
            string path = $"{outputDir}/{baseName}.cs";

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(path))
            {

                // If the line doesn't contain the word 'Second', write the line to the file. 
                file.WriteLine("namespace main.Parser");
                file.WriteLine("{");
                    file.WriteLine($"abstract class {baseName} {{");
                    file.WriteLine("}");
                file.WriteLine("}");
                foreach (var type in types)
                {
                    string className = type.Split(":")[0].Trim();
                    string fields = type.Split(":")[1].Trim();
                    defineType(file, baseName, className, fields);
                }
            }
        }

        private static void defineType(StreamWriter file, string baseName, string className, string fieldList)
        {
            file.WriteLine("");
            file.WriteLine($"    static class {className} : {baseName}{{");
            file.WriteLine($"{className} ({fieldList} {{");
            string[] fields = fieldList.Split(",");
            foreach(string field in fields)
            {
                string name = field.Split(" ")[1];
                file.WriteLine($"        this.{name} = {name};");
            }
            file.WriteLine("}");

            file.WriteLine();
            foreach(string field in fields)
            {
                file.WriteLine($"public {field} ;");
            }

            file.WriteLine("}");
        }
    }
}
