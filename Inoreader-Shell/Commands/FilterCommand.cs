using Inoreader.Dto;
using Jasily.Framework.ConsoleEngine;
using Jasily.Framework.ConsoleEngine.Attributes;
using Jasily.Framework.ConsoleEngine.Commands;
using System;
using System.Linq;

namespace InoreaderShell.Commands
{
    [Command("filter")]
    [Alias("f")]
    [Desciption("filter current list")]
    public sealed class FilterCommand : ICommand
    {
        public void Execute(Session session, CommandLine line)
        {
            if (session.IsFeedsInitialized())
            {
                var feed = session.GetStreamItems();

                var blocks = line.Blocks.ToArray();
                if (blocks.Length > 2)
                {
                    session.Write("title");
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
                    var kw = blocks[2].OriginText;
                    session.WriteLine($"filter {selector.Item1} {filter.Item1} {kw}");
                    feed.Items = feed.Items.Where(z => filter.Item2(selector.Item2(z), blocks[2].OriginText)).ToList();
                }
                session.PrintItem();
            }
        }

        private Tuple<string, Func<Item, string>> GetSelectorFunc(string field)
        {
            switch (field.ToLower())
            {
                case "title":
                case "t":
                    return Tuple.Create("title", new Func<Item, string>(z => z.Title));
            }
            return null;
        }

        private Tuple<string, Func<string, string, bool>> GetFilterFunc(string action)
        {
            switch (action.ToLower())
            {
                case "c":
                case "contain":
                    return Tuple.Create("contain",
                        new Func<string, string, bool>((z, x) => z.ToLower().Contains(x.ToLower())));

                case "e":
                case "eq":
                    return Tuple.Create("equal", new Func<string, string, bool>((z, x)
                        => string.Equals(z, x, StringComparison.OrdinalIgnoreCase)));
            }
            return null;
        }
    }
}