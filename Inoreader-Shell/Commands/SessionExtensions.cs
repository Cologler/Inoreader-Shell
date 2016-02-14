using Inoreader;
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
    }
}