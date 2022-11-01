namespace senddatatest.Models
{
    public class ClickMonth
    {
        public String Link { get; set; }
        public int Count { get; set; }

        public ClickMonth(String link, int count)
        {
            this.Link = link;
            this.Count = count;
        }
    }
}
