using Inoreader;
using Inoreader.Dto;

namespace InoreaderShell.Models
{
    public sealed class InoreaderShellFeedItem : InoreaderShellItem
    {
        private readonly Item item;

        public InoreaderShellFeedItem(Inoreader.Dto.Item item)
        {
            this.item = item;
        }

        public override void MarkAsRead(Proxy inoreader) => inoreader.MarkAsRead(this.item.Id);

        public override string Id => this.item.Id;

        public override string Title => this.item.Title;
    }
}