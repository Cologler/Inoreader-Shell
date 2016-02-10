using Inoreader;
using Inoreader.Dto;
using System.Collections.Generic;

namespace InoreaderShell.Commands
{
    public class Variables
    {
        public Proxy Inoreader { get; set; }

        public List<Subscription> Subscriptions { get; set; }

        public StreamItems Feed { get; set; }
    }
}