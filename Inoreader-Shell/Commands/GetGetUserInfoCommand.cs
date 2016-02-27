using Inoreader;
using Jasily.Framework.ConsoleEngine;
using Jasily.Framework.ConsoleEngine.Attributes;
using Jasily.Framework.ConsoleEngine.Commands;

namespace InoreaderShell.Commands
{
    [Command("get-userinfo")]
    [Desciption("get user info")]
    [Static]
    public sealed class GetGetUserInfoCommand : ICommand
    {
        public void Execute(Session session, CommandLine line)
        {
            if (session.IsAuthed())
            {
                var user = session.GetVariables().Inoreader.GetUserInfo();
                session.WriteLine(user.UserId);
            }
        }
    }
}