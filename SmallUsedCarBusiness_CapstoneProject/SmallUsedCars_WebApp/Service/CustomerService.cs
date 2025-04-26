using SmallUsedCars_WebApp.Database;
using SmallUsedCars_WebApp.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SmallUsedCars_WebApp.Service
{
    public class CustomerService
    {
        private readonly ApplicationDbContext _context;

        public CustomerService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get a list of all customers (Used for dropdown selection in appointment creation)
        public List<Customer> GetAllCustomers()
        {
            return _context.Customers
                .Include(c => c.Vehicles) // Include related vehicles
                .ToList();
        }

        // Get customers based on filters
        public List<Customer> GetCustomer(int? customerId, string name, string email, string plateNumber, string vin)
        {
            var query = _context.Customers
                .Include(c => c.Vehicles) // Include related Vehicles
                .AsQueryable();

            if (customerId.HasValue)
            {
                return query.Where(c => c.CustomerId == customerId.Value).ToList();
            }

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(c => c.FirstName.Contains(name) || c.LastName.Contains(name));
            }
            if (!string.IsNullOrEmpty(email))
            {
                query = query.Where(c => c.Email == email);
            }
            if (!string.IsNullOrEmpty(plateNumber))
            {
                query = query.Where(c => c.Vehicles.Any(v => v.PlateNumber == plateNumber));
            }
            if (!string.IsNullOrEmpty(vin))
            {
                query = query.Where(c => c.Vehicles.Any(v => v.Vin == vin));
            }

            return query.ToList();
        }

        // Add a new customer to the database (Used when creating an appointment with a new customer)
        public Customer CreateCustomer(Customer customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException(nameof(customer), "Customer data cannot be null.");
            }

            _context.Customers.Add(customer);
            _context.SaveChanges();

            return customer; // Return the created customer with its generated CustomerId
        }
    }
}
