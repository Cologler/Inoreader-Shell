using Jasily.Framework.ConsoleEngine;
using Jasily.Framework.ConsoleEngine.Attributes;

namespace InoreaderShell.Commands
{
    [Command("list")]
    public class ListCommand
    {
        [Command("feed")]
        [SubCommand]
        public void Feed(Session session, CommandLine line)
        {
            
        }

        [Command("sub")]
        [SubCommand]
        [Desciption("get subscriptions list")]
        public void Subscriptions(Session session, CommandLine line)
        {
            if (session.IsAuthed())
            {
                var inoreader = session.GetInoreader();
                var subscriptions = inoreader.GetSubscriptions();
                session.GetVariables().Subscriptions = subscriptions;
                for (var i = 0; i < subscriptions.Count; i++)
                {
                    var item = subscriptions[i];
                    session.WriteLine($"[{i}] {item.Title}");
                }
            }
        }
    }
}