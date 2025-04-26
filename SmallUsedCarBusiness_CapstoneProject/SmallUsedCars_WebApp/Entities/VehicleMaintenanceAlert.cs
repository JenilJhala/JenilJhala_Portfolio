namespace SmallUsedCars_WebApp.Entities
{
    public class VehicleMaintenanceAlert
    {
        public int VehicleMaintenanceAlertId { get; set; }
        public string MaintenanceType { get; set; }
        public DateTime AlertDate { get; set; }

        // Foreign Key
        public int VehicleId { get; set; }

        // Navigation Property
        public Vehicle Vehicle { get; set; }
    }
}
