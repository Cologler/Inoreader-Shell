using Inoreader;
using Jasily.Framework.ConsoleEngine;

namespace InoreaderShell.Commands
{
    public abstract class AuthedCommand : BaseCommand
    {
        protected override void Execute(Variables variables, Session session, CommandLine line)
        {
            if (variables.Inoreader == null)
            {
                session.WriteLine($"must auth before.");
                return;
            }

            this.Execute(variables, variables.Inoreader, session, line);
        }

        protected abstract void Execute(Variables variables, Proxy inoreader, Session session, CommandLine line);
    }
}