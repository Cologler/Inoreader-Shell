using Jasily.Framework.ConsoleEngine;
using Jasily.Framework.ConsoleEngine.Commands;
using Jasily.Framework.ConsoleEngine.Extensions;

namespace InoreaderShell.Commands
{
    public abstract class BaseCommand : ICommand
    {
        public Variables Variables { get; private set; }

        public void Execute(Session session, CommandLine line)
        {
            this.Variables = (Variables)session.State.GetOrSetValue("environment", new Variables());
            this.Execute(this.Variables, session, line);
        }

        protected abstract void Execute(Variables variables, Session session, CommandLine line);
    }
}