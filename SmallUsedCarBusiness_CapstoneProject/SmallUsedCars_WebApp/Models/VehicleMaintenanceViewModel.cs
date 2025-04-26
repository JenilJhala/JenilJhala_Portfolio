namespace SmallUsedCars_WebApp.Models
{
    public class VehicleMaintenanceViewModel
    {
        public int? CustomerId { get; set; }
        public int VehicleId { get; set; }
        public string VehicleModel { get; set; }
        public string CustomerEmail { get; set; }
        public string MaintenanceType { get; set; }
        public DateTime LastServiceDate { get; set; }
        public string Cost { get; set; }
        public string MaintenanceDescription { get; set; }
        public bool IsDueForReplacement { get; set; }
    }
}
