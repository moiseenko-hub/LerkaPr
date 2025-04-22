namespace LerkaPr.Models.Database
{
    public class StudentData : BaseModel
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime LastVisit { get; set; } = DateTime.Now;
        public int RoomId { get; set; }
        public virtual RoomData Room { get; set; }

    }
}
