namespace LerkaPr.Models.Database
{
    public class EmployeeData : BaseModel
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string JobTitle { get; set; } = string.Empty;
    }
}
