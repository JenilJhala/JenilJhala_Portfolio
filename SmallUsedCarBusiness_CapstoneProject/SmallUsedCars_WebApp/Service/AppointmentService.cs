using Microsoft.EntityFrameworkCore;
using SmallUsedCars_WebApp.Database;
using SmallUsedCars_WebApp.Entities;
using SmallUsedCars_WebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SmallUsedCars_WebApp.Service
{
    public class AppointmentService
    {
        private readonly ApplicationDbContext _context;
        private readonly CustomerService _customerService;

        public AppointmentService(ApplicationDbContext context, CustomerService customerService)
        {
            _context = context;
            _customerService = customerService;
        }

        // Create an appointment using AppointmentViewModel
        public Appointment CreateAppointment(AppointmentViewModel viewModel)
        {
            if (viewModel.CustomerId == 0 && viewModel.IsNewCustomer)
            {
                var newCustomer = _customerService.CreateCustomer(new Customer
                {
                    FirstName = viewModel.FirstName,
                    LastName = viewModel.LastName,
                    PhoneNumber = viewModel.PhoneNumber,
                    Email = viewModel.Email,
                    Province = viewModel.Province,
                    City = viewModel.City,
                    Street = viewModel.Street,
                    UnitNumber = viewModel.UnitNumber,
                    PostalCode = viewModel.PostalCode
                });

                viewModel.CustomerId = newCustomer.CustomerId;
            }

            var appointment = new Appointment
            {
                CustomerId = viewModel.CustomerId ?? 0,  // int? 타입인 viewModel.CustomerId를 int 타입으로 할당하려면 이것 사용. viewModel.CustomerId 가 null 이면 0을 할당
                AppointmentDate = viewModel.AppointmentDate,
                AppointmentType = viewModel.AppointmentType,
                Description = viewModel.Description,
                VehicleId = viewModel.VehicleId == 0 ? null : viewModel.VehicleId,
                EmployeeId = viewModel.EmployeeId,
                Status = viewModel.Status
            };

            _context.Appointments.Add(appointment);
            _context.SaveChanges();

            return appointment;
        }

        // Update an existing appointment using AppointmentViewModel
        public void UpdateAppointment(Appointment appointment)
        {
            var existingAppointment = _context.Appointments.Find(appointment.AppointmentId);
            if (existingAppointment != null)
            {
                existingAppointment.CustomerId = appointment.CustomerId;
                existingAppointment.EmployeeId = appointment.EmployeeId;
                existingAppointment.VehicleId = appointment.VehicleId;
                existingAppointment.Status = appointment.Status;
                existingAppointment.AppointmentType = appointment.AppointmentType;
                existingAppointment.AppointmentDate = appointment.AppointmentDate;
                existingAppointment.Description = appointment.Description;

                _context.SaveChanges();
            }
        }

        // Delete an appointment
        public void DeleteAppointment(int appointmentId)
        {
            var appointment = _context.Appointments.Find(appointmentId);
            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
                _context.SaveChanges();
            }
        }

        // Get all appointments with related data
        public List<Appointment> GetAllAppointments()
        {
            return _context.Appointments
                .Include(a => a.Customer)
                .Include(a => a.Vehicle)
                .Include(a => a.Employee)
                .ToList();
        }

        // Get appointment by ID
        public Appointment GetAppointmentById(int appointmentId)
        {
            return _context.Appointments
                .Include(a => a.Customer)
                .Include(a => a.Vehicle)
                .Include(a => a.Employee)
                .FirstOrDefault(a => a.AppointmentId == appointmentId);
        }

        // Get appointments by customer
        public List<Appointment> GetAppointmentByCustomer(int customerId)
        {
            return _context.Appointments
                .Where(a => a.CustomerId == customerId)
                .Include(a => a.Customer)
                .Include(a => a.Vehicle)
                .Include(a => a.Employee)
                .ToList();
        }
    }
}
