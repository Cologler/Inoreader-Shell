using Inoreader;
using Inoreader.Dto;
using Jasily.Framework.ConsoleEngine;
using Jasily.Framework.ConsoleEngine.Attributes;

namespace InoreaderShell.Commands
{
    [Command("make-all-read", Desciption = "filter current list")]
    public sealed class MakeAllReadCommand : ItemCommand
    {
        protected override void Execute(Variables variables, StreamItems feed, Proxy inoreader, Session session, CommandLine line)
        {
            foreach (var item in feed.Items)
            {
                inoreader.MarkAsRead(item.Id);
            }
        }
    }
}