namespace SmallUsedCars_WebApp.Entities
{
    
    public class Vehicle
    {
        public int VehicleId { get; set; }
        public string Vin { get; set; }
        public string PlateNumber { get; set; }
        public string Mileage { get; set; }
        public string Type { get; set; }
        public string Powertrain { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Manufacturer { get; set; }
        public string Status { get; set; }
        public decimal MarketValue { get; set; }
        public string? ImageFileName { get; set; }

        // Foreign Key
        public int? CustomerId { get; set; }

        // Navigation Properties
        public Customer? Customer { get; set; }
        public ICollection<VehicleMaintenance>? MaintenanceRecords { get; set; } = new List<VehicleMaintenance>();
        public ICollection<VehicleTransaction>? Transactions { get; set; } = new List<VehicleTransaction>();
        public Inventory? Inventory { get; set; }
    }
}
