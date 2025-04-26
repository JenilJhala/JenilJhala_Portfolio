using SmallUsedCars_WebApp.Database;
using SmallUsedCars_WebApp.Entities;
using SmallUsedCars_WebApp.Models;
using SmallUsedCars_WebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SmallUsedCars_WebApp.Service
{
    public class ReportService
    {
        private readonly ApplicationDbContext _context;

        public ReportService(ApplicationDbContext context)
        {
            _context = context;
        }

        // 특정 기간 동안의 매출 조회
        public List<PeriodReport> GetSalesByPeriod(List<string> employeeIds, DateTime? startDate, DateTime? endDate)
        {
            var query = _context.SalesRecords.AsQueryable();

            if (employeeIds != null && employeeIds.Any())
            {
                query = query.Where(s => employeeIds.Contains(s.EmployeeId));
            }

            if (startDate.HasValue)
            {
                query = query.Where(s => s.SaleDate >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                query = query.Where(s => s.SaleDate <= endDate.Value.AddMonths(1).AddDays(-1));
            }

            var salesData = query.ToList();
            if (!salesData.Any()) return new List<PeriodReport>();

            return GroupByMonth(salesData, s => s.SalePrice);
        }



        // 특정 기간 동안의 커미션 조회
        public List<PeriodReport> GetCommissionByPeriod(List<string> employeeIds, DateTime? startDate, DateTime? endDate)
        {
            var query = _context.SalesRecords.AsQueryable();

            if (employeeIds != null && employeeIds.Any())
            {
                query = query.Where(s => employeeIds.Contains(s.EmployeeId));
            }

            if (startDate.HasValue)
            {
                query = query.Where(s => s.SaleDate >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                query = query.Where(s => s.SaleDate <= endDate.Value.AddMonths(1).AddDays(-1));
            }

            var commissionData = query.ToList();
            if (!commissionData.Any()) return new List<PeriodReport>();

            return commissionData
                .GroupBy(s => new { s.SaleDate.Year, s.SaleDate.Month, s.Employee.EmployeeName })
                .Select(g => new PeriodReport
                {
                    Period = $"{g.Key.Year}-{g.Key.Month:D2}",
                    Amount = g.Sum(s => s.SalePrice * s.CommissionRate),
                    EmployeeName = g.Key.EmployeeName
                })
                .OrderBy(r => r.Period)
                .ToList();
        }


        private List<PeriodReport> GroupByMonth(List<SalesRecord> records, Func<SalesRecord, decimal> selector)
        {
            return records
                .GroupBy(s => new
                {
                    Year = s.SaleDate.Year,
                    Month = s.SaleDate.Month,
                    EmployeeName = s.Employee.EmployeeName
                })
                .Select(g => new PeriodReport
                {
                    Period = $"{g.Key.Year}-{g.Key.Month:D2}", // "YYYY-MM" 형식
                    Amount = g.Sum(selector),
                    EmployeeName = g.Key.EmployeeName
                })
                .OrderBy(r => r.Period)
                .ToList();
        }



        private List<PeriodReport> EnsureAllMonthsIncluded(List<PeriodReport> reports, DateTime? startDate, DateTime? endDate)
        {
            if (!startDate.HasValue || !endDate.HasValue)
                return reports;

            var allMonths = new List<PeriodReport>();
            DateTime current = new DateTime(startDate.Value.Year, startDate.Value.Month, 1);
            DateTime lastMonth = new DateTime(endDate.Value.Year, endDate.Value.Month, 1);

            while (current <= lastMonth)
            {
                string period = $"{current.Year}-{current.Month:D2}";
                if (!reports.Any(r => r.Period == period))
                {
                    allMonths.Add(new PeriodReport { Period = period, Amount = 0, EmployeeName = "" });
                }
                else
                {
                    allMonths.Add(reports.First(r => r.Period == period));
                }
                current = current.AddMonths(1);
            }

            return allMonths.OrderBy(r => r.Period).ToList();
        }



        // 직원별 총 매출 내림차순 정렬
        public List<ReportRankingViewModel> GetSalesRanking(DateTime startDate, DateTime endDate)
        {
            return _context.SalesRecords
                .Where(s => s.SaleDate >= startDate && s.SaleDate <= endDate)
                .GroupBy(s => s.Employee.EmployeeName)
                .Select(g => new ReportRankingViewModel
                {
                    EmployeeName = g.Key,
                    TotalAmount = g.Sum(s => s.SalePrice)
                })
                .OrderByDescending(r => r.TotalAmount)
                .ToList();
        }

    }
}
