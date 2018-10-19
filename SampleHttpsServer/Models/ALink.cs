namespace SampleHttpsServer
{
    public class ALink
    {
        public ALink(string linkName, string linkUrl)
        {
            this.LinkName = linkName;
            this.LinkUrl = linkUrl;
        }

        public string LinkName { private set; get; }
        public string LinkUrl { private set; get; }
    }
}