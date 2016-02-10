using Inoreader;

namespace InoreaderShell.Models
{
    public class InoreaderShellItem
    {
        public bool CanFilter => false;

        public static implicit operator InoreaderShellItem(Inoreader.Dto.Item item)
            => new InoreaderShellFeedItem(item);

        public virtual void MarkAsRead(Proxy inoreader)
        {
        }

        public virtual string Id => string.Empty;

        public virtual string Title => string.Empty;

        public override string ToString()
        {
            return $"[{this.Id}] {this.Title}";
        }
    }
}