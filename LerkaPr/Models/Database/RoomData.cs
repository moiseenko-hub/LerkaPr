namespace LerkaPr.Models.Database
{
    public class RoomData : BaseModel
    {
        public int Number { get; set; }
        public List<StudentData> Students { get; set; } = [];
    }
}
