using SmallUsedCars_WebApp.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmallUsedCars_WebApp.ViewModels
{
    public class UpdateAppointmentViewModel
    {
        public int AppointmentId { get; set; }

        [Required]
        public int CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public List<Customer> Customers { get; set; } = new();

        [Required]
        public string EmployeeId { get; set; }
        public List<Employee> Employees { get; set; } = new();

        public int? VehicleId { get; set; }
        public List<Vehicle> Vehicles { get; set; } = new();

        [Required]
        public string Status { get; set; }

        [Required]
        public string AppointmentType { get; set; }

        [Required]
        public DateTime AppointmentDate { get; set; } = DateTime.Today;

        public string Description { get; set; }
    }
}
