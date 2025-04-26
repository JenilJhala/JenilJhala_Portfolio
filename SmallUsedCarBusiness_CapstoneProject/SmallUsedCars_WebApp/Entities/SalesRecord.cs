namespace SmallUsedCars_WebApp.Entities
{
    public class SalesRecord
    {
        public int SalesRecordId { get; set; }
        public DateTime SaleDate { get; set; } // Sale date also represents the commission payment date
        public decimal SalePrice { get; set; }
        public decimal CommissionEarned { get; set; } // Commission amount
        public decimal CommissionRate { get; set; } // Store the commission rate per transaction

        // Foreign Keys
        public string? EmployeeId { get; set; }
        public int VehicleId { get; set; }

        // Navigation Properties
        public Employee Employee { get; set; }
        public Vehicle Vehicle { get; set; }
    }
}
