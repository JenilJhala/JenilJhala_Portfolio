using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SmallUsedCars_WebApp.Entities
{
    public class Employee : IdentityUser
    {
        //public int EmployeeId { get; set; } Employee 엔터티가 IdentityUser를 상속받고, IdentityUser는 기본적으로 Id(string) 필드를 가지고 있기 때문에 EmployeeId 불필요
        [Required]
        public string EmployeeName { get; set; }
        public string? Position { get; set; }
        public string? Department { get; set; } 
        public string? ContactNumber { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime CurrentPositionStartDate { get; set; }
        
        public int AppointmentId { get; set; }
        public int SalesRecordId { get; set; }
        public int HRId { get; set; }
 

        // Navigation Properties
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public ICollection<SalesRecord> SalesRecords { get; set; } = new List<SalesRecord>();
        public ICollection<HR> HRRecords { get; set; } = new List<HR>();
        public ICollection<PayrollRecord> PayrollRecords { get; set; } = new List<PayrollRecord>();
        public ICollection<VehicleTransaction> VehicleTransactions { get; set; } = new List<VehicleTransaction>();

    }
}
