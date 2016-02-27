using System.Collections.Generic;
using Inoreader;
using Inoreader.Dto;
using Jasily.Framework.ConsoleEngine;
using Jasily.Framework.ConsoleEngine.Extensions;

namespace InoreaderShell.Commands
{
    public static class SessionExtensions
    {
        public static Variables GetVariables(this Session session)
            => (Variables)session.State.GetOrSetValue("environment", new Variables());

        public static Proxy GetInoreader(this Session session)
            => session.GetVariables().Inoreader;

        public static bool IsAuthed(this Session session)
        {
            if (session.GetInoreader() == null)
            {
                session.WriteLine($"must auth before.");
                return false;
            }

            return true;
        }

        public static bool IsFeedsInitialized(this Session session)
        {
            if (session.GetVariables().FiltedItems == null)
            {
                session.WriteLine("none item.");
                return false;
            }

            return true;
        }

        public static void PrintItem(this Session session)
        {
            var items = session.GetVariables().FiltedItems;
            if (items?.Count > 0)
            {
                for (var i = 0; i < items.Count; i++)
                {
                    var item = items[i];
                    session.WriteLine($"[{i}] {item.Title}");
                }
            }
            else
            {
                session.WriteLine($"there are no item.");
            }
        }
    }
}