using Inoreader;
using Jasily.Framework.ConsoleEngine;
using Jasily.Framework.ConsoleEngine.Attributes;
using Jasily.Framework.ConsoleEngine.Commands;

namespace InoreaderShell.Commands
{
    [Command("auth")]
    [Desciption("authentication using google email and password.")]
    public sealed class AuthCommand : BaseCommand, IDesciptionCommand
    {
        [PropertyParameter("user")]
        [Alias("u")]
        [Desciption("google email")]
        public string UserName { get; set; }

        [PropertyParameter("pwd")]
        [Alias("p")]
        [Desciption("google account password")]
        public string Password { get; set; }

        public string Desciption => "auth\t\t\t\t\tauthentication using google email and password.";

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