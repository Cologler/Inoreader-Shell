using Inoreader.Dto;
using Jasily.Framework.ConsoleEngine;
using Jasily.Framework.ConsoleEngine.Attributes;
using System;
using System.Linq;

namespace InoreaderShell.Commands
{
    public sealed class LinqCommand
    {
        [Command("where")]
        [Desciption("filter current list")]
        public void Where(Session session, CommandLine line)
        {
            if (session.IsFeedsInitialized())
            {
                var var = session.GetVariables();
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
                    var kws = blocks[2].OriginText.Split(new[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                    session.WriteLine($"filter {selector.Item1} {filter.Item1} {string.Join(" or ", kws)}");
                    var.FiltedItems = var.FiltedItems.Where(z => kws.Any(k => filter.Item2(selector.Item2(z), k))).ToList();
                }
                session.PrintItem();
            }
        }

        [Command("ignore")]
        [Desciption("filter current list")]
        public void Ignore(Session session, CommandLine line)
        {
            if (session.IsFeedsInitialized())
            {
                var var = session.GetVariables();
                var blocks = line.Blocks.ToArray();
                if (blocks.Length > 2)
                {
                    var selector = this.GetSelectorFunc(blocks[0].OriginText);
                    if (selector == null) return;
                    var filter = this.GetFilterFunc(blocks[1].OriginText);
                    if (filter == null) return;
                    var kw = blocks[2].OriginText;
                    session.WriteLine($"ignore {selector.Item1} {filter.Item1} {kw}");
                    var.FiltedItems = var.FiltedItems.Where(z => !filter.Item2(selector.Item2(z), kw)).ToList();
                }
                session.PrintItem();
            }
        }

        [Command("skip")]
        [Desciption("filter current list")]
        public void Skip(Session session, CommandLine line, [MethodParameter("id")][Alias("i")] int id = -1)
        {
            if (session.IsFeedsInitialized())
            {
                var var = session.GetVariables();
                if (id >= 0 && id < var.FiltedItems.Count)
                {
                    var.FiltedItems.RemoveAt(id);
                    session.PrintItem();
                }
                else
                {
                    session.WriteLine("no such index : " + id);
                }
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