namespace SmallUsedCars_WebApp.Entities
{
    public class Inventory
    {
        public int InventoryId { get; set; }
        public string Location { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }
        public DateTime StockInDate { get; set; }
        public DateTime? StockOutDate { get; set; }

        // Foreign Key
        public int VehicleId { get; set; }

        // Navigation Property
        public Vehicle Vehicle { get; set; }
    }
}
