using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SmallUsedCars_WebApp.Database;
using SmallUsedCars_WebApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SmallUsedCars_WebApp.Service
{
    public class HRService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Employee> _userManager; // Injected UserManager

        public HRService(ApplicationDbContext context, UserManager<Employee> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        /// <summary>
        /// Determines if an employee is eligible for promotion (5+ years in current position).
        /// </summary>
        public bool CheckPromotionEligibility(string employeeId)
        {
            var employee = _userManager.Users.SingleOrDefault(e => e.Id == employeeId);
            if (employee == null)
                return false;

            return (DateTime.Now - employee.CurrentPositionStartDate).TotalDays / 365 >= 5;
        }

        /// <summary>
        /// Promotes an employee to a new position if eligible.
        /// </summary>
        public void PromoteEmployee(string employeeId, string newPosition)
        {
            try
            {
                var employee = _userManager.Users.SingleOrDefault(e => e.Id == employeeId);
                if (employee == null)
                    throw new Exception("Employee not found.");

                if (!CheckPromotionEligibility(employeeId))
                    throw new Exception("Employee is not eligible for promotion.");

                employee.Position = newPosition;
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Employee requests leave; creates a new HR record with pending status.
        /// </summary>
        public void RequestLeave(string employeeId, DateTime startDate, DateTime endDate, string leaveType, string reason)
        {
            var hrRecord = new HR
            {
                EmployeeId = employeeId,
                LeaveStartDate = startDate,
                LeaveEndDate = endDate,
                LeaveType = leaveType,
                Reason = reason,
                LeaveStatus = "Pending" // Default status
            };

            _context.HRs.Add(hrRecord);
            _context.SaveChanges();
        }

        /// <summary>
        /// Retrieves all HR (leave) records for a specific employee.
        /// </summary>
        public List<HR> GetHRRecord(string employeeId)
        {
            return _context.HRs
                .Where(hr => hr.EmployeeId == employeeId)
                .ToList();
        }

        /// <summary>
        /// Retrieves all HR (leave) records from the database, including associated Employee data.
        /// </summary>
        public List<HR> GetAllLeaves()
        {
            return _context.HRs
                .Include(hr => hr.Employee)
                .ToList();
        }

        /// <summary>
        /// Approves a leave request if the record exists and the manager is valid.
        /// </summary>
        public void ApproveLeaveRequest(int hrId, string managerId)
        {
            var hrRecord = _context.HRs.Find(hrId);
            var manager = _userManager.Users.SingleOrDefault(m => m.Id == managerId && m.Position == "Manager");

            if (hrRecord == null || manager == null)
                throw new Exception("Leave record or manager not found.");

            hrRecord.LeaveStatus = "Approved";
            _context.SaveChanges();
        }

        /// <summary>
        /// Denies a leave request if the record exists and the manager is valid.
        /// </summary>
        public void DenyLeaveRequest(int hrId, string managerId)
        {
            var hrRecord = _context.HRs.Find(hrId);
            var manager = _userManager.Users.SingleOrDefault(m => m.Id == managerId && m.Position == "Manager");

            if (hrRecord == null || manager == null)
                throw new Exception("Leave record or manager not found.");

            hrRecord.LeaveStatus = "Denied";
            _context.SaveChanges();
        }
    }
}
