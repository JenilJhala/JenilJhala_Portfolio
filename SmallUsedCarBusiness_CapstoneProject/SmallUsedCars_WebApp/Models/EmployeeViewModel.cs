namespace SmallUsedCars_WebApp.Models
{
    public class EmployeeViewModel
    {
        public string EmployeeId { get; set; }  // IdentityUser에서 제공하는 기본 ID (string)
        public string EmployeeName { get; set; }
        public string? Position { get; set; }
        public string? Department { get; set; }
        public string? ContactNumber { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime CurrentPositionStartDate { get; set; }
    }
}
