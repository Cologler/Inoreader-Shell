using Jasily.Framework.ConsoleEngine;
using Jasily.Framework.ConsoleEngine.Attributes;
using Jasily.Framework.ConsoleEngine.Commands;
using System.Linq;

namespace InoreaderShell.Commands
{
    [Command("make-all-read")]
    [Alias("mar")]
    [Desciption("filter current list")]
    public sealed class MakeAllReadCommand : ICommand
    {
        public void Execute(Session session, CommandLine line)
        {
            if (session.IsAuthed() && session.IsFeedsInitialized())
            {
                var inoreader = session.GetInoreader();
                var variables = session.GetVariables();
                inoreader.MarkAsRead(variables.FiltedItems.Select(z => z.Id).ToArray());
                variables.FiltedItems = null;
                session.WriteLine("done.");
            }
        }
    }
}