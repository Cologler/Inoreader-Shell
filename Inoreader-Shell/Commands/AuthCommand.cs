using Inoreader;
using Jasily.Framework.ConsoleEngine;
using Jasily.Framework.ConsoleEngine.Attributes;

namespace InoreaderShell.Commands
{
    [Command("auth", IsStatic = true, Desciption = "authentication using google email and password.")]
    public sealed class AuthCommand : BaseCommand
    {
        [Parameter(true, "u", "user")]
        public string UserName { get; set; }

        [Parameter(true, "p", "pwd")]
        public string Password { get; set; }

        protected override void Execute(Variables variables, Session session, CommandLine line)
        {
            if (string.IsNullOrWhiteSpace(this.UserName))
            {
                session.WriteLine("username can not be white space.");
                return;
            }

            if (string.IsNullOrWhiteSpace(this.Password))
            {
                session.WriteLine("password can not be white space.");
                return;
            }

            var inoreader = new Proxy("1000001033", "P2OzUTKYMooEz2aYTnB9qS98MIHWNj4B", this.UserName, this.Password);
            var token = inoreader.Authenticate();

            if (string.IsNullOrWhiteSpace(token))
            {
                session.WriteLine("get token failed.");
                return;
            }
            else
            {
                session.WriteLine($"got token: {token}.");
                variables.Inoreader = inoreader;
            }
        }
    }
}