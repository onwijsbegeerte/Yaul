using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

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

            defineAst(outputDir, "Expr", new List<string> { "Binary   : Expr Left, Token TokenOperator, Expr Right", "Grouping : Expr Expression", "Literal  : object Value", "Unary    : Token TokenOperator, Expr Right" });

        }

        private static void defineVisitor(StreamWriter file, string baseName, List<string> types)
        {
            file.WriteLine("public interface Visitor<R>");
            file.WriteLine("    {");

            foreach (var type in types)
            {
                var typeName = type.Split(":")[0].Trim();
                file.WriteLine($"        R visit{typeName.Trim()}{baseName.Trim()} ({typeName} {baseName.ToLower()});");
                
                
            }
            
            file.WriteLine("    }");
        }

        private static void defineAst(string outputDir, string baseName, List<string> types)
        {
            string path = $"{outputDir}/{baseName}.cs";

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(path))
            {

                // If the line doesn't contain the word 'Second', write the line to the file. 
                file.WriteLine("namespace main");
                file.WriteLine("{");
                    file.WriteLine($"    public abstract class {baseName}");
                    file.WriteLine("{");
                file.WriteLine("    public abstract R accept<R>(Visitor<R> visitor);");
                defineVisitor(file, baseName, types);

                    file.WriteLine("}");

                
                foreach (var type in types)
                {
                    string className = type.Split(":")[0].Trim();
                    string fields = type.Split(":")[1].Trim();
                    defineType(file, baseName, className, fields);
                }
                file.WriteLine("}");
            }
        }

        private static void defineType(StreamWriter file, string baseName, string className, string fieldList)
        {
            file.WriteLine("");
            file.WriteLine($"    public class {className} : {baseName}{{");
            file.WriteLine($"{className} ({fieldList}) {{");
            string[] fields = fieldList.Split(",");
            foreach(string field in fields)
            {
                var field2 = field.Trim();
                string name = field2.Split(" ")[1];
                file.WriteLine($"        this.{name} = {name};");
            }
            file.WriteLine("}");
            
            file.WriteLine("public override R accept<R>(Visitor<R> visitor){");            
            file.WriteLine($"    return visitor.visit{className}{baseName.Trim()}(this);");            

            file.WriteLine("}");            
            file.WriteLine();
            foreach(string field in fields)
            {
                file.WriteLine($"public {field} {{ get; set; }}");
            }
            file.WriteLine("}");
        }
    }
}
