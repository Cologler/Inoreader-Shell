using Jasily.Framework.ConsoleEngine;
using System;
using System.Reflection;

namespace InoreaderShell
{
    class Program : IApplicationDescription
    {
        public string ApplicationName => "Inoreader shell";

        public string Copyright => "Copyright (C) 2016-9999, all rights reserved.";

        public string Description => "shell for inoreader.";

        public string Version => "v1.0";

        static void Main(string[] args)
        {
            var engine = new JasilyConsoleEngine();
            engine.MapperManager.RegistAssembly(Assembly.GetExecutingAssembly());
            using (var session = engine.StartSession())
            {
                session.Description = new Program();
                session.ShowDescription();
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
