using SmallUsedCars_WebApp.Database;
using SmallUsedCars_WebApp.Entities;
using SmallUsedCars_WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SmallUsedCars_WebApp.Service
{
    public class PayrollService
    {
        private readonly ApplicationDbContext _context;

        public PayrollService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void SavePayroll(PayrollEntryViewModel model)
        {
            // Server-side recalc for safety:
            decimal commission = model.SalePrice * 0.05m;
            decimal totalBeforeTax = model.BaseSalary + commission;
            decimal taxAmount = totalBeforeTax * model.TaxRate;
            decimal totalPay = totalBeforeTax - taxAmount;

            DateTime payDate = DateTime.Now; // or pick the relevant date

            var payroll = new PayrollRecord
            {
                EmployeeId = model.EmployeeId,
                BaseSalary = model.BaseSalary,
                SalePrice = model.SalePrice,
                Commission = commission,
                TaxRate = model.TaxRate,
                TotalBeforeTax = totalBeforeTax,
                Tax = taxAmount,
                TotalPay = totalPay,
                PayDate = payDate
            };

            _context.PayrollRecords.Add(payroll);
            _context.SaveChanges();
        }

        public List<PayrollViewModel> GetPayrollForPeriod(DateTime startDate, DateTime endDate, List<string>? selectedEmployees)
        {
            var payrollQuery = _context.PayrollRecords.AsQueryable();

            if (selectedEmployees != null && selectedEmployees.Any())
            {
                payrollQuery = payrollQuery.Where(p => selectedEmployees.Contains(p.EmployeeId));
            }

            return payrollQuery
                .Where(p => p.PayDate >= startDate && p.PayDate <= endDate)
                .Select(pr => new PayrollViewModel
                {
                    EmployeeId = pr.EmployeeId,
                    EmployeeName = pr.Employee != null ? pr.Employee.EmployeeName : "Unknown",
                    PayDate = pr.PayDate,
                    BaseSalary = pr.BaseSalary,
                    Tax = pr.Tax,
                    TotalPay = pr.TotalPay
                })
                .ToList();
        }
    }
}
