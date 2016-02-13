using Inoreader;
using Inoreader.Dto;
using Inoreader.Enum;
using Jasily.Framework.ConsoleEngine;
using Jasily.Framework.ConsoleEngine.Attributes;
using System;

namespace InoreaderShell.Commands
{
    [Command("list-feed")]
    [Alias("lf")]
    [Desciption("get subscriptions list")]
    public sealed class ListFeedCommand : ItemCommand
    {
        [PropertyParameter("id")]
        [Alias("i")]
        [Desciption("id of subscription")]
        public int SubscriptionId { get; set; }

        [PropertyParameter("count", IsOptional = true)]
        [Alias("c")]
        [Alias("n")]
        [Desciption("count of feeds")]
        public int? Count { get; set; }

        [PropertyParameter("filter", IsOptional = true)]
        [Alias("f")]
        [Desciption("filter of feeds")]
        public ItemsFilterEnum Filter { get; set; } = ItemsFilterEnum.OnlyUnread;

        protected override void Execute(
            Variables variables, Proxy inoreader, Session session, CommandLine line)
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

            var count = this.Count ?? 200;
            if (count < 1)
            {
                session.WriteLine("count must > 0.");
                return;
            }

            var items = inoreader.GetItems(id, filter: this.Filter, count: count);
            variables.Feed = items;
            this.Print(session);
        }

        protected override void Execute(
            Variables variables, StreamItems feed, Proxy inoreader, Session session, CommandLine line)
        {
        }

        [Command("feed")]
        public void Feed(
            [Alias("i")][Desciption("id of subscription")]
            [MethodParameter("id")] int subscriptionId,
            Session session, CommandLine line)
        {
            Console.WriteLine(subscriptionId);
        }
    }
}