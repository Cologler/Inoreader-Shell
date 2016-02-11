using Inoreader;
using Jasily.Framework.ConsoleEngine;
using Jasily.Framework.ConsoleEngine.Attributes;

namespace InoreaderShell.Commands
{
    [Command("get-userinfo", IsStatic = true)]
    [Desciption("get user info")]
    public sealed class GetGetUserInfoCommand : AuthedCommand
    {
        protected override void Execute(Variables variables, Proxy inoreader, Session session, CommandLine line)
        {
            var user = inoreader.GetUserInfo();
            session.WriteLine(user.UserId);
        }
    }
}