using Inoreader;
using Inoreader.Dto;
using Inoreader.Enum;
using Jasily.Framework.ConsoleEngine;
using Jasily.Framework.ConsoleEngine.Attributes;

namespace InoreaderShell.Commands
{
    [Command("list-feed", Desciption = "get subscriptions list")]
    public sealed class ListFeedCommand : ItemCommand
    {
        [Parameter(false, "id", Desciption = "id of subscription")]
        public int SubscriptionId { get; set; }

        [Parameter(true, "count", Desciption = "count of feeds")]
        public int Count { get; set; } = 200;

        [Parameter(true, "filter", Desciption = "count of feeds")]
        public ItemsFilterEnum Filter { get; set; } = ItemsFilterEnum.OnlyUnread;

        protected override void Execute(Variables variables, Proxy inoreader, Session session, CommandLine line)
        {
            if (variables.Subscriptions == null)
            {
                session.WriteLine("list sub before list feed.");
                return;
            }

            if (this.SubscriptionId < 0 || this.SubscriptionId >= variables.Subscriptions.Count)
            {
                session.WriteLine("invaild id.");
                return;
            }

            var id = variables.Subscriptions[this.SubscriptionId].Id;

            if (this.Count < 1)
            {
                session.WriteLine("count must > 0.");
                return;
            }

            var items = inoreader.GetItems(id, filter: this.Filter, count: this.Count);
            variables.Feed = items;
            this.Print(session);
        }

        protected override void Execute(Variables variables,
            StreamItems feed, Proxy inoreader, Session session, CommandLine line)
        {
        }
    }
}