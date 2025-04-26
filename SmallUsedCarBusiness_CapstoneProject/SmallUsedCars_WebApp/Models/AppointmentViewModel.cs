using SmallUsedCars_WebApp.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmallUsedCars_WebApp.ViewModels
{
    public class AppointmentViewModel
    {
        public int AppointmentId { get; set; }

        public int? CustomerId { get; set; } // Nullable로 변경하여 검증 오류 방지
        public List<Customer> Customers { get; set; } = new();

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string EmployeeId { get; set; }
        public string? EmployeeName { get; set; }
        public List<Employee> Employees { get; set; } = new();

        public int? VehicleId { get; set; }
        public string? VehicleModel { get; set; }
        public string? VehiclePlateNumber { get; set; }
        public List<Vehicle> Vehicles { get; set; } = new();

        [Required]
        public string Status { get; set; }

        [Required]
        public string AppointmentType { get; set; }

        [Required]
        public DateTime AppointmentDate { get; set; } = DateTime.Today;

        public string Description { get; set; }

        public bool IsNewCustomer { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public string? Email { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string? UnitNumber { get; set; }
        public string PostalCode { get; set; }
    }
}
