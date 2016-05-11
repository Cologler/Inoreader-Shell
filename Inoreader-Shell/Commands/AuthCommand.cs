using Inoreader;
using Jasily.Framework.ConsoleEngine;
using Jasily.Framework.ConsoleEngine.Attributes;
using Jasily.Framework.ConsoleEngine.Commands;
using System.Diagnostics;

namespace InoreaderShell.Commands
{
    [Command("auth")]
    [Desciption("authentication using google email and password.")]
    public sealed class AuthCommand : IDesciptionCommand, IGroupingCommand
    {
        [PropertyParameter("user")]
        [Alias("u")]
        [Desciption("google email")]
        [MethodParameterGrouping(0)]
        public string UserName { get; set; }

        [PropertyParameter("pwd")]
        [Alias("p")]
        [Desciption("google account password")]
        [MethodParameterGrouping(0)]
        public string Password { get; set; }

        [PropertyParameter("token")]
        [Alias("t")]
        [Desciption("access token")]
        [MethodParameterGrouping(1)]
        public string Token { get; set; }

        public string Desciption => "auth\t\t\t\t\tauthentication using google email and password.";

        public void Execute(Session session, CommandLine line, int[] matchedGroupId)
        {
            var variables = session.GetVariables();

            Proxy inoreader = null;

            foreach (var id in matchedGroupId)
            {
                switch (id)
                {
                    case 0:
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

                        inoreader = new Proxy("1000001033", "P2OzUTKYMooEz2aYTnB9qS98MIHWNj4B", this.UserName, this.Password);
                        inoreader.Authenticate();
                        break;

                    case 1:
                        if (string.IsNullOrWhiteSpace(this.Token))
                        {
                            session.WriteLine("token can not be white space.");
                            return;
                        }

                        inoreader = new Proxy("1000001033", "P2OzUTKYMooEz2aYTnB9qS98MIHWNj4B", this.Token);
                        break;
                }
            }

            Debug.Assert(inoreader != null);

            if (string.IsNullOrWhiteSpace(inoreader.Token))
            {
                session.WriteLine("get token failed.");
                return;
            }
            else
            {
                session.WriteLine($"got token: {inoreader.Token}");
                variables.Inoreader = inoreader;
            }
        }

        public void Execute(Session session, CommandLine line)
        {
            return;
        }
    }
}