using System.Linq;
using System.Threading.Tasks;
using Inoreader;
using Inoreader.Dto;
using Jasily.Framework.ConsoleEngine;
using Jasily.Framework.ConsoleEngine.Attributes;

namespace InoreaderShell.Commands
{
    [Command("make-all-read")]
    [Alias("mar")]
    [Desciption("filter current list")]
    public sealed class MakeAllReadCommand : ItemCommand
    {
        protected override void Execute(Variables variables, StreamItems feed, Proxy inoreader, Session session, CommandLine line)
        {
            inoreader.MarkAsRead(feed.Items.Select(z => z.Id).ToArray());
            session.WriteLine("done.");
        }
    }
}