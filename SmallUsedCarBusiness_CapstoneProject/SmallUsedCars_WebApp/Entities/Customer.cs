namespace SmallUsedCars_WebApp.Entities
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string? UnitNumber { get; set; }
        public string PostalCode { get; set; }

        // Navigation Properties
        public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
