using System.ComponentModel.DataAnnotations;

namespace SmallUsedCars_WebApp.Models
{
    public class VehicleAddViewModel
    {
        public string? Vin { get; set; }
        public string? PlateNumber { get; set; }
        public string? Mileage { get; set; }
        public string? Type { get; set; }
        public string? Powertrain { get; set; }
        public string? Model { get; set; }
        public int Year { get; set; } // `int`는 `0`이 기본값이므로 문제 없음
        public string? Manufacturer { get; set; }
        public string? Status { get; set; }
        public decimal MarketValue { get; set; }
    }

}

