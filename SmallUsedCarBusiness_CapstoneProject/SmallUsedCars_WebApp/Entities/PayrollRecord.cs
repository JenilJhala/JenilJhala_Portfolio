namespace SmallUsedCars_WebApp.Entities
{
    public class PayrollRecord
    {
        public int PayrollRecordId { get; set; }

        public decimal BaseSalary { get; set; }
        public decimal SalePrice { get; set; }
        public decimal Commission { get; set; }
        public decimal TaxRate { get; set; }
        public decimal TotalBeforeTax { get; set; }
        public decimal Tax { get; set; }
        public decimal TotalPay { get; set; }

        public DateTime PayDate { get; set; }

        // Foreign Keys
        public string? EmployeeId { get; set; }
        public int SalesRecordId { get; set; }

        // Navigation
        public Employee? Employee { get; set; }
        public List<SalesRecord> SalesRecords { get; set; } = new();
    }
}
