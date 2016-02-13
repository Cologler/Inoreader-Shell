using Inoreader;
using Jasily.Framework.ConsoleEngine;
using Jasily.Framework.ConsoleEngine.Attributes;

namespace InoreaderShell.Commands
{
    [Command("list-sub")]
    [Alias("ls")]
    [Desciption("get subscriptions list")]
    [Static]
    public sealed class ListSubscriptionsCommand : AuthedCommand
    {
        protected override void Execute(Variables variables, Proxy inoreader, Session session, CommandLine line)
        {
            var subscriptions = inoreader.GetSubscriptions();
            variables.Subscriptions = subscriptions;
            for (var i = 0; i < subscriptions.Count; i++)
            {
                var item = subscriptions[i];
                session.WriteLine($"[{i}] {item.Title}");
            }
        }
    }
}