using Inoreader;
using Inoreader.Dto;
using Jasily.Framework.ConsoleEngine;
using Jasily.Framework.ConsoleEngine.Attributes;
using System;
using System.Linq;

namespace InoreaderShell.Commands
{
    [Command("filter", Desciption = "filter current list")]
    public sealed class FilterCommand : ItemCommand
    {
        protected override void Execute(Variables variables, StreamItems feed, Proxy inoreader,
            Session session, CommandLine line)
        {
            var blocks = line.Blocks.ToArray();
            if (blocks.Length > 2)
            {
                var f = this.GetFieldFunc(blocks[0].OriginText);
                if (f == null)
                {
                    return;
                }
                var a = this.GetFilterFunc(blocks[1].OriginText);
                if (a == null)
                {
                    return;
                }
                this.Variables.Feed.Items = this.Variables.Feed.Items.Where(z => a(f(z), blocks[2].OriginText)).ToList();
                this.Print(session);
            }
        }

        private Func<Item, string> GetFieldFunc(string field)
        {
            switch (field.ToLower())
            {
                case "title":
                    return z => z.Title;
            }
            return null;
        }

        private Func<string, string, bool> GetFilterFunc(string action)
        {
            switch (action.ToLower())
            {
                case "contain":
                    return (z, x) => z.Contains(x);
            }
            return null;
        }
    }
}