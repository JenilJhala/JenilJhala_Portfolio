namespace SmallUsedCars_WebApp.Entities
{
    public class VehicleMaintenance
    {
        public int VehicleMaintenanceId { get; set; }
        public string MaintenanceType { get; set; }
        public DateTime LastServiceDate { get; set; }
        public decimal Cost { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public string MaintenanceDescription { get; set; }

        // Foreign Key
        public int VehicleId { get; set; }

        // Navigation Property
        public Vehicle Vehicle { get; set; }
    }
}
