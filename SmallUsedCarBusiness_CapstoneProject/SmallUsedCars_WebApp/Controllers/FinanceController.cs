using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmallUsedCars_WebApp.Database;
using SmallUsedCars_WebApp.Models;
using SmallUsedCars_WebApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SmallUsedCars_WebApp.Controllers
{
    [Authorize]
    public class FinanceController : Controller
    {
        private readonly PayrollService _payrollService;
        private readonly EmployeeService _employeeService;

        public FinanceController(PayrollService payrollService, EmployeeService employeeService)
        {
            _payrollService = payrollService;
            _employeeService = employeeService;
        }

        [HttpGet]
        public IActionResult List(DateTime? startDate, DateTime? endDate, List<string>? selectedEmployees)
        {
            DateTime today = DateTime.Now;
            DateTime lastMonthStart = new DateTime(today.Year, today.Month - 1, 1);
            DateTime lastMonthEnd = lastMonthStart.AddMonths(1).AddDays(-1);

            DateTime start = startDate ?? lastMonthStart;
            DateTime end = endDate ?? lastMonthEnd;

            var allEmployees = _employeeService.GetAllEmployees();
            var payrollRecords = _payrollService.GetPayrollForPeriod(start, end, selectedEmployees);

            var model = new PayrollListViewModel
            {
                StartDate = start,
                EndDate = end,
                PayrollRecords = payrollRecords,
                Employees = allEmployees.Select(e => new EmployeeSelectViewModel
                {
                    EmployeeId = e.Id,
                    EmployeeName = e.EmployeeName
                }).ToList(),
                SelectedEmployeeIds = selectedEmployees ?? new List<string>()
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult Create(List<string> selectedEmployees)
        {
            // Pull employees from your DB
            var employees = _employeeService.GetAllEmployees()
                .Where(e => selectedEmployees.Contains(e.Id))
                .ToList();

            var model = new PayrollListViewModel
            {
                Employees = employees.Select(e => new EmployeeSelectViewModel
                {
                    EmployeeId = e.Id,
                    EmployeeName = e.EmployeeName
                }).ToList(),
                PayrollEntries = new List<PayrollEntryViewModel>()
            };

            // For each chosen employee, add an empty payroll entry
            foreach (var emp in model.Employees)
            {
                model.PayrollEntries.Add(new PayrollEntryViewModel
                {
                    EmployeeId = emp.EmployeeId,
                    EmployeeName = emp.EmployeeName,
                    TaxRate = 0.13m // default
                });
            }

            return View("Payroll", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PayrollListViewModel model)
        {
            if (model.PayrollEntries == null || !model.PayrollEntries.Any())
            {
                ModelState.AddModelError("", "No payroll entries were provided.");
                return View("Payroll", model);
            }

            // Save each entry
            foreach (var entry in model.PayrollEntries)
            {
                _payrollService.SavePayroll(entry);
            }

            return RedirectToAction("List");
        }
    }
}
