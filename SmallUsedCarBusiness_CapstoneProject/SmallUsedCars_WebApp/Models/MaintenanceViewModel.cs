namespace SmallUsedCars_WebApp.Models
{
    public class MaintenanceViewModel
    {
        public int VehicleId { get; set; }
        public string VehicleModel { get; set; }
        public DateTime LastServiceDate { get; set; }
        public string MaintenanceType { get; set; }
        public string MaintenanceDescription { get; set; }
        public decimal Cost { get; set; }
    }
}
