namespace LerkaPr.Models
{
    public class RoomViewModel
    {
        public int Id { get; set; }

        public int Number { get; set; }

        public List<StudentViewModel> Students { get; set; } = new();


        public List<int> SelectedStudentIds { get; set; } = new();
    }
}
