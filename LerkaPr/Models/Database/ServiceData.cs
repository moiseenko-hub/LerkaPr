namespace LerkaPr.Models.Database
{
    public class ServiceData : BaseModel
    {
        public string Type { get; set; }
        public string Description { get; set; }
        public DateTime DateTime { get; set; } = DateTime.UtcNow;
    }
}
