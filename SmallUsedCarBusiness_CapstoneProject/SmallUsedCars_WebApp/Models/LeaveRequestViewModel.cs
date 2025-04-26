namespace SmallUsedCars_WebApp.Models
{
    public class LeaveRequestViewModel
    {
        public int HRId { get; set; }
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public DateTime LeaveStartDate { get; set; }
        public DateTime LeaveEndDate { get; set; }
        public string LeaveType { get; set; }
        public string Reason { get; set; }
        public string LeaveStatus { get; set; }
    }
}
