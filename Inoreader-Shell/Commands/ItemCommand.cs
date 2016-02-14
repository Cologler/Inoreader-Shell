using Inoreader;
using Inoreader.Dto;
using Jasily.Framework.ConsoleEngine;

namespace InoreaderShell.Commands
{
    public abstract class ItemCommand : AuthedCommand
    {
        protected override void Execute(Variables variables, Proxy inoreader, Session session, CommandLine line)
        {
            if (variables.Feed == null)
            {
                session.WriteLine($"none item.");
                return;
            }

            this.Execute(variables, variables.Feed, inoreader, session, line);
        }

        protected abstract void Execute(
            Variables variables, StreamItems feed, Proxy inoreader,
            Session session, CommandLine line);

        protected void Print(Session session)
        {
            var items = this.Variables.Feed?.Items;
            if (items?.Count != 0)
            {
                for (var i = 0; i < items.Count; i++)
                {
                    var item = items[i];
                    session.WriteLine($"[{i}] {item.Title}");
                }
            }
            else
            {
                session.WriteLine($"there are no item.");
            }
        }
    }
}