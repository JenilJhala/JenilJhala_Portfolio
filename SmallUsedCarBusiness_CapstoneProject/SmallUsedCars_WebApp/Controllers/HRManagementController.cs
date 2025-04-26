using Microsoft.AspNetCore.Mvc;
using SmallUsedCars_WebApp.Service;
using SmallUsedCars_WebApp.ViewModels;
using SmallUsedCars_WebApp.Authorization; // Include our custom attribute
using System;
using System.Linq;
using SmallUsedCars_WebApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace SmallUsedCars_WebApp.Controllers
{
    [Authorize]
    public class HRManagementController : Controller
    {
        private readonly HRService _hrService;
        private readonly EmployeeService _employeeService;

        public HRManagementController(HRService hrService, EmployeeService employeeService)
        {
            _hrService = hrService;
            _employeeService = employeeService;
        }

        // Accessible to all logged-in users: List employees
        public IActionResult Index()
        {
            var employees = _employeeService.GetAllEmployees();
            var model = new HRManagementIndexViewModel
            {
                Employees = employees
            };
            return View(model);
        }

        // Manager-only: View leave requests
        [ManagerOnly]
        public IActionResult LeaveRequests()
        {
            var leaveRecords = _hrService.GetAllLeaves()
                .Select(hr => new LeaveRequestViewModel
                {
                    HRId = hr.HRId,
                    EmployeeId = hr.EmployeeId,
                    EmployeeName = hr.Employee?.EmployeeName,
                    LeaveStartDate = hr.LeaveStartDate,
                    LeaveEndDate = hr.LeaveEndDate,
                    LeaveType = hr.LeaveType,
                    Reason = hr.Reason,
                    LeaveStatus = hr.LeaveStatus
                }).ToList();

            return View(leaveRecords);
        }

        // Manager-only: Approve leave requests.
        [HttpPost]
        [ManagerOnly]
        public IActionResult ApproveLeave(int hrId)
        {
            try
            {
                string managerId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                _hrService.ApproveLeaveRequest(hrId, managerId);
                TempData["SuccessMessage"] = "Leave request approved successfully.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return RedirectToAction("LeaveRequests");
        }

        // Manager-only: Deny leave requests.
        [HttpPost]
        [ManagerOnly]
        public IActionResult DenyLeave(int hrId)
        {
            try
            {
                string managerId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                _hrService.DenyLeaveRequest(hrId, managerId);
                TempData["SuccessMessage"] = "Leave request denied successfully.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return RedirectToAction("LeaveRequests");
        }

        // Accessible by any employee: Request leave form.
        public IActionResult RequestLeave(string employeeId)
        {
            if (string.IsNullOrEmpty(employeeId))
                return BadRequest("Employee ID is required.");

            var employee = _employeeService.GetEmployeeById(employeeId);
            if (employee == null)
                return NotFound("Employee not found.");

            var model = new RequestLeaveViewModel
            {
                EmployeeId = employee.Id,
                EmployeeName = employee.EmployeeName,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1)
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RequestLeave(RequestLeaveViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                _hrService.RequestLeave(model.EmployeeId, model.StartDate, model.EndDate, model.LeaveType, model.Reason);
                TempData["SuccessMessage"] = "Leave requested successfully.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }

        // Manager-only: Promote employee form.
        [ManagerOnly]
        public IActionResult PromoteEmployee(string employeeId)
        {
            if (string.IsNullOrEmpty(employeeId))
                return BadRequest("Employee ID is required.");

            var employee = _employeeService.GetEmployeeById(employeeId);
            if (employee == null)
                return NotFound("Employee not found.");

            bool isEligible = _hrService.CheckPromotionEligibility(employee.Id);
            var model = new PromoteEmployeeViewModel
            {
                EmployeeId = employee.Id,
                EmployeeName = employee.EmployeeName,
                CurrentPosition = employee.Position,
                EligibleForPromotion = isEligible
            };

            return View(model);
        }

        // Manager-only: Process promotion.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ManagerOnly]
        public IActionResult PromoteEmployee(PromoteEmployeeViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                _hrService.PromoteEmployee(model.EmployeeId, model.NewPosition);
                TempData["SuccessMessage"] = "Employee promoted successfully.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }

        // Placeholder for performance board (accessible to all)
        public IActionResult PerformanceBoard()
        {
            return View();
        }

        // Placeholder for HRM report (accessible to all)
        public IActionResult HRMReport()
        {
            return View();
        }
    }
}
