using System.ComponentModel.DataAnnotations.Schema;

namespace SmallUsedCars_WebApp.Entities
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Status { get; set; }
        public string AppointmentType { get; set; }
        public string Description { get; set; }

        // Foreign Keys
        public int CustomerId { get; set; }

        [ForeignKey("Vehicle")]
        public int? VehicleId { get; set; }

        [ForeignKey("Employee")]
        public string? EmployeeId { get; set; }

        // Navigation Properties
        public Customer Customer { get; set; }
        public Vehicle Vehicle { get; set; }
        public Employee Employee { get; set; }
    }
}