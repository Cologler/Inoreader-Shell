using System.IO;
using System.Reflection;
using Jasily.Framework.ConsoleEngine;

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
            using (var session = engine.StartSession("inoreader", new Program()))
            {
                if (args.Length > 0 && Path.GetFileName(args[0]) == "cmd.txt")
                {
                    var cmds = File.ReadAllLines(args[0]);
                    foreach (var cmd in cmds)
                    {
                        session.WriteLine(cmd);
                        session.Execute(cmd);
                    }
                }

                if (!session.IsShutdowned) session.StartUp();
            }
        }
    }
}
