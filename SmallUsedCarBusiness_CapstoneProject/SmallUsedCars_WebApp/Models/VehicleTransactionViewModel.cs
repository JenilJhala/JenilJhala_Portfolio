using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SmallUsedCars_WebApp.Entities;

namespace SmallUsedCars_WebApp.Models
{
    public class VehicleTransactionViewModel
    {
        //  Vehicle 엔터티의 속성 (읽기 전용)
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

        //  VehicleTransaction 엔터티의 속성 (입력 필드)
        public string TransactionType { get; set; }

        public DateTime TransactionDate { get; set; } = DateTime.Now;

        //  `[Required]` 제거, 대신 기본값을 0으로 설정하여 필수 입력이 아니도록 변경
        [Range(0, double.MaxValue, ErrorMessage = "Purchase Price cannot be negative.")]
        public decimal PurchasePrice { get; set; } = 0;

        [Range(0, double.MaxValue, ErrorMessage = "Sales Price cannot be negative.")]
        public decimal SalesPrice { get; set; } = 0;

        [Range(0, double.MaxValue, ErrorMessage = "Trade-In Value cannot be negative.")]
        public decimal TradeInValue { get; set; } = 0;

        public string? EmployeeId { get; set; }

        //  직원 선택 리스트
        public List<EmployeeTransactionViewModel> AvailableEmployees { get; set; } = new List<EmployeeTransactionViewModel>();
    }


    public class EmployeeTransactionViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
