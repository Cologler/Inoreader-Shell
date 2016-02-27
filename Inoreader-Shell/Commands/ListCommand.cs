using Inoreader.Enum;
using Jasily.Framework.ConsoleEngine;
using Jasily.Framework.ConsoleEngine.Attributes;

namespace InoreaderShell.Commands
{
    [Command("list")]
    public class ListCommand
    {
        [Command("feed")]
        [SubCommand]
        public void Feed(Session session, CommandLine line,
            [MethodParameter("id")][Alias("i")][Desciption("id of subscription")]
            int subscriptionId,
            [MethodParameter("count")][Alias("c")][Alias("n")][Desciption("count of feeds")]
            int? c = null,
            [MethodParameter("filter")][Alias("f")][Desciption("filter of feeds")]
            ItemsFilterEnum filter = ItemsFilterEnum.OnlyUnread)
        {
            if (session.IsAuthed())
            {
                var inoreader = session.GetInoreader();
                var variables = session.GetVariables();
                if (variables.Subscriptions == null)
                {
                    session.WriteLine("list sub before list feed.");
                    return;
                }
                if (subscriptionId < 0 || subscriptionId >= variables.Subscriptions.Count)
                {
                    session.WriteLine("invaild id.");
                    return;
                }
                var id = variables.Subscriptions[subscriptionId].Id;

                var count = c ?? 200;
                if (count < 1)
                {
                    session.WriteLine("count must > 0.");
                    return;
                }

                var items = inoreader.GetItems(id, filter: filter, count: count);
                variables.FiltedItems = items.Items;
                session.PrintItem();
            }
        }

        [Command("sub")]
        [SubCommand]
        [Desciption("get subscriptions list")]
        public void Subscriptions(Session session, CommandLine line)
        {
            if (session.IsAuthed())
            {
                var inoreader = session.GetInoreader();
                var subscriptions = inoreader.GetSubscriptions();
                session.GetVariables().Subscriptions = subscriptions;
                for (var i = 0; i < subscriptions.Count; i++)
                {
                    var item = subscriptions[i];
                    session.WriteLine($"[{i}] {item.Title}");
                }
            }
        }
    }
}