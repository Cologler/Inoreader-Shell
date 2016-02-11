using System.Linq;
using Inoreader;
using Inoreader.Dto;
using Jasily.Framework.ConsoleEngine;
using Jasily.Framework.ConsoleEngine.Attributes;

namespace InoreaderShell.Commands
{
    [Command("skip")]
    public sealed class SkipCommand : ItemCommand
    {
        [Parameter("id", IsOptional = true)]
        [Alias("i")]
        public int Id { get; set; } = -1;

        protected override void Execute(Variables variables, StreamItems feed, Proxy inoreader,
            Session session, CommandLine line)
        {
            var blocks = line.Blocks.ToArray();
            if (this.Id >= 0 && this.Id < feed.Items.Count)
            {
                feed.Items.RemoveAt(this.Id);
            }
            else if (blocks.Length == 1)
            {
                int n;
                if (int.TryParse(blocks[0].OriginText, out n))
                {
                    feed.Items = feed.Items.Skip(n).ToList();
                }
            }
            this.Print(session);
        }
    }
}