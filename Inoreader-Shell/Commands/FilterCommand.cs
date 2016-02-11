using Inoreader;
using Inoreader.Dto;
using Jasily.Framework.ConsoleEngine;
using Jasily.Framework.ConsoleEngine.Attributes;
using System;
using System.Linq;

namespace InoreaderShell.Commands
{
    [Command("filter")]
    [Alias("f")]
    [Desciption("filter current list")]
    public sealed class FilterCommand : ItemCommand
    {
        protected override void Execute(Variables variables, StreamItems feed, Proxy inoreader,
            Session session, CommandLine line)
        {
            var blocks = line.Blocks.ToArray();
            if (blocks.Length > 2)
            {
                var selector = this.GetSelectorFunc(blocks[0].OriginText);
                if (selector == null)
                {
                    return;
                }
                var filter = this.GetFilterFunc(blocks[1].OriginText);
                if (filter == null)
                {
                    return;
                }
                feed.Items = feed.Items.Where(z => filter(selector(z), blocks[2].OriginText)).ToList();
            }
            this.Print(session);
        }

        private Func<Item, string> GetSelectorFunc(string field)
        {
            switch (field.ToLower())
            {
                case "title":
                case "t":
                    return z => z.Title;
            }
            return null;
        }

        private Func<string, string, bool> GetFilterFunc(string action)
        {
            switch (action.ToLower())
            {
                case "c":
                case "contain":
                    return (z, x) => z.ToLower().Contains(x.ToLower());
                case "e":
                case "eq":
                    return (z, x) => z == x;
            }
            return null;
        }
    }
}