namespace PostGreTestAPI.Domain
{
    public class Potato
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string PotatoStatus { get; set; }
    }
}
