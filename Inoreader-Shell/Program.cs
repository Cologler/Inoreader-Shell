using Jasily.Framework.ConsoleEngine;
using System;
using System.Reflection;

namespace InoreaderShell
{
    class Program
    {
        static void Main(string[] args)
        {
            var engine = new JasilyConsoleEngine();
            engine.RegistAssembly(Assembly.GetExecutingAssembly());
            using (var session = engine.StartSession())
            {
                while (true)
                {
                    var line = Console.ReadLine() ?? string.Empty;
                    if (line.ToLower() == "exit") return;
                    session.Execute(line);
                }
            }
        }
    }
}
