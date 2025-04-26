using SmallUsedCars_WebApp.Models;
using System;
using System.Collections.Generic;

namespace SmallUsedCars_WebApp.ViewModels
{
    public class ReportViewModel
    {
        public List<string> SelectedEmployeeIds { get; set; } = new List<string>();

        public DateTime? SalesStartDate { get; set; }
        public DateTime? SalesEndDate { get; set; }
        public DateTime? CommissionStartDate { get; set; }
        public DateTime? CommissionEndDate { get; set; }

        public List<EmployeeViewModel> Employees { get; set; } = new List<EmployeeViewModel>();
        public List<PeriodReport> SalesRecords { get; set; } = new List<PeriodReport>();
        public List<PeriodReport> CommissionRecords { get; set; } = new List<PeriodReport>();

        public decimal TotalSales { get; set; }
        public decimal TotalCommission { get; set; }
        public string? EmployeeId { get; set; }
        public string? EmployeeName { get; set; }



      


        public string? ErrorMessage { get; set; }

    }
}
