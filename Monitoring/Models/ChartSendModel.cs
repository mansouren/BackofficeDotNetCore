namespace Monitoring.Models
{
    public class ChartSendModel
    {
        public string gref { get; set; }
        public string gid { get; set; }
        public List<CacheModel> ItemData { get; set; }
    }
    public class CacheModel
    {
        public long Time { get; set; }

        public List<Points> Points { get; set; }
    }
}
