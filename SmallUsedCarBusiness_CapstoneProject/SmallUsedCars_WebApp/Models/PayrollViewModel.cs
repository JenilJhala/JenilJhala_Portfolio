namespace SmallUsedCars_WebApp.Models
{
    public class PayrollViewModel
    {
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public DateTime PayDate { get; set; }
        public decimal BaseSalary { get; set; }
        public decimal Tax { get; set; }
        public decimal TotalPay { get; set; }
    }

    public class PayrollEntryViewModel
    {
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }

        public decimal BaseSalary { get; set; }
        public decimal SalePrice { get; set; }
        public decimal TaxRate { get; set; } = 0.13m;

        // Computed
        public decimal Commission => SalePrice * 0.05m;
        public decimal TotalBeforeTax => BaseSalary + Commission;
        public decimal TaxAmount => TotalBeforeTax * TaxRate;
        public decimal TotalPay => TotalBeforeTax - TaxAmount;
    }

    // RENAME the old EmployeeViewModel to avoid conflict:
    public class EmployeeSelectViewModel
    {
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
    }

    public class PayrollListViewModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // For displaying existing payroll records
        public List<PayrollViewModel> PayrollRecords { get; set; } = new();

        // For creating new payroll entries
        public List<PayrollEntryViewModel> PayrollEntries { get; set; } = new();

        // For employee selection
        public List<EmployeeSelectViewModel> Employees { get; set; } = new();
        public List<string> SelectedEmployeeIds { get; set; } = new();
    }
}
