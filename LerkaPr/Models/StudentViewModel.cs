namespace LerkaPr.Models
{
    public class StudentViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime LastVisit {  get; set; } = DateTime.Now;
        public int RoomNumber { get; set; }
    }
}
