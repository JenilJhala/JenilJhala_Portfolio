namespace SmallUsedCars_WebApp.Entities
{
    public class HR
    {
        public int HRId { get; set; }
        public DateTime LeaveStartDate { get; set; }
        public DateTime LeaveEndDate { get; set; }
        public string LeaveType { get; set; }
        public string Reason { get; set; }
        public string LeaveStatus { get; set; }

        // Foreign Key
        public string? EmployeeId { get; set; }

        // Navigation Property
        public Employee Employee { get; set; }
    }
}
